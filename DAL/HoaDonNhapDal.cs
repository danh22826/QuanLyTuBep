using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public class HoaDonNhapDal : HoaDonDalBase
    {
        protected override string Bang { get { return "HoaDonNhap"; } }
        protected override string CotKhoa { get { return "SoHDN"; } }
        protected override string TienTo { get { return "HDN"; } }

        private const string SqlChon =
            "SELECT h.SoHDN, h.MaNV, h.NgayNhap, h.MaNCC, h.TongTien, n.TenNV, c.TenNCC " +
            "FROM HoaDonNhap h LEFT JOIN NhanVien n ON h.MaNV = n.MaNV LEFT JOIN NhaCungCap c ON h.MaNCC = c.MaNCC";

        public List<HoaDonNhap> LayTatCa()
        {
            return DocDuLieu.DanhSach<HoaDonNhap>(Db.Query(SqlChon + " ORDER BY h.NgayNhap DESC, h.SoHDN DESC"));
        }

        public HoaDonNhap TimTheoSo(string so)
        {
            DataTable dt = Db.Query(SqlChon + " WHERE h.SoHDN = @so", Db.P("@so", so));
            if (dt.Rows.Count == 0) return null;

            HoaDonNhap hd = DocDuLieu.Dong<HoaDonNhap>(dt.Rows[0]);
            hd.ChiTiet.AddRange(DocDuLieu.DanhSach<ChiTietHoaDonNhap>(Db.Query(
                "SELECT c.SoHDN, c.MaHang, h.TenHang, k.TenKichThuoc, cl.TenChatLieu, m.TenMau, nsx.TenNuocSX, " +
                "c.SoLuong, c.DonGia, ISNULL(c.GiamGia, 0) AS GiamGia, c.ThanhTien " +
                "FROM ChiTietHoaDonNhap c JOIN DMHangHoa h ON c.MaHang = h.MaHang " +
                "LEFT JOIN KichThuoc k ON h.MaKichThuoc = k.MaKichThuoc " +
                "LEFT JOIN ChatLieu cl ON h.MaChatLieu = cl.MaChatLieu " +
                "LEFT JOIN MauSac m ON h.MaMau = m.MaMau " +
                "LEFT JOIN NuocSanXuat nsx ON h.MaNuocSX = nsx.MaNuocSX " +
                "WHERE c.SoHDN = @so ORDER BY c.MaHang",
                Db.P("@so", so))));
            return hd;
        }

        public KetQuaGhiHoaDon Luu(HoaDonNhap hd)
        {
            KetQuaGhiHoaDon kq = new KetQuaGhiHoaDon();
            Db.GiaoDich(delegate (SqlConnection conn, SqlTransaction tran)
            {
                // Kiểm tra và khóa mặt hàng trước khi ghi phiếu nhập để chặn trường hợp vừa ngừng kinh doanh.
                List<ChiTietHoaDonNhap> dsKiemTra = new List<ChiTietHoaDonNhap>(hd.ChiTiet);
                dsKiemTra.Sort((a, b) => string.Compare(a.MaHang, b.MaHang, StringComparison.OrdinalIgnoreCase));
                foreach (ChiTietHoaDonNhap d in dsKiemTra)
                {
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT h.TenHang, m.TenMau, k.TenKichThuoc, cl.TenChatLieu, nsx.TenNuocSX, ISNULL(h.NgungKinhDoanh, 0) " +
                        "FROM DMHangHoa h WITH (UPDLOCK, HOLDLOCK) " +
                        "LEFT JOIN MauSac m ON h.MaMau = m.MaMau " +
                        "LEFT JOIN KichThuoc k ON h.MaKichThuoc = k.MaKichThuoc " +
                        "LEFT JOIN ChatLieu cl ON h.MaChatLieu = cl.MaChatLieu " +
                        "LEFT JOIN NuocSanXuat nsx ON h.MaNuocSX = nsx.MaNuocSX WHERE h.MaHang = @ma", conn, tran))
                    {
                        cmd.Parameters.Add(Db.P("@ma", d.MaHang));
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (r.Read() && Convert.ToBoolean(r.GetValue(5)))
                            {
                                string tenDayDu = HangHoa.TaoTenDayDu(
                                    Convert.ToString(r.GetValue(0)), Convert.ToString(r.GetValue(1)),
                                    Convert.ToString(r.GetValue(2)), Convert.ToString(r.GetValue(3)),
                                    Convert.ToString(r.GetValue(4)));
                                kq.HangNgungKinhDoanh.Add(new HangNgungKinhDoanh { MaHang = d.MaHang, TenDayDu = tenDayDu });
                            }
                        }
                    }
                }
                if (kq.HangNgungKinhDoanh.Count > 0) return;

                Db.NonQuery(conn, tran,
                    "INSERT INTO HoaDonNhap (SoHDN, MaNV, NgayNhap, MaNCC, TongTien) VALUES (@so, @nv, @ngay, @ncc, 0)",
                    Db.P("@so", hd.SoHDN), Db.P("@nv", hd.MaNV), Db.P("@ngay", hd.NgayNhap.Date), Db.P("@ncc", hd.MaNCC));

                foreach (ChiTietHoaDonNhap d in hd.ChiTiet)
                {
                    Db.NonQuery(conn, tran,
                        "INSERT INTO ChiTietHoaDonNhap (SoHDN, MaHang, SoLuong, DonGia, GiamGia) VALUES (@so, @mh, @sl, @dg, @gg)",
                        Db.P("@so", hd.SoHDN), Db.P("@mh", d.MaHang), Db.P("@sl", d.SoLuong),
                        Db.P("@dg", d.DonGia), Db.P("@gg", d.GiamGia));
                }
                kq.DaGhi = true;
            });
            return kq;
        }

        protected override void KiemTraTruocKhiXoa(SqlConnection conn, SqlTransaction tran, string so, KetQuaGhiHoaDon kq)
        {
            SortedDictionary<string, int> soLuongNhap = new SortedDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            using (SqlCommand cmd = new SqlCommand(
                "SELECT MaHang, SUM(SoLuong) FROM ChiTietHoaDonNhap WITH (UPDLOCK, HOLDLOCK) " +
                "WHERE SoHDN = @so GROUP BY MaHang ORDER BY MaHang", conn, tran))
            {
                cmd.Parameters.Add(Db.P("@so", so));
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read()) soLuongNhap.Add(Convert.ToString(r.GetValue(0)), Convert.ToInt32(r.GetValue(1)));
                }
            }

            foreach (KeyValuePair<string, int> item in soLuongNhap)
            {
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT ISNULL(h.SoLuong, 0), h.TenHang, m.TenMau, k.TenKichThuoc, cl.TenChatLieu, nsx.TenNuocSX " +
                    "FROM DMHangHoa h WITH (UPDLOCK, HOLDLOCK) " +
                    "LEFT JOIN MauSac m ON h.MaMau = m.MaMau " +
                    "LEFT JOIN KichThuoc k ON h.MaKichThuoc = k.MaKichThuoc " +
                    "LEFT JOIN ChatLieu cl ON h.MaChatLieu = cl.MaChatLieu " +
                    "LEFT JOIN NuocSanXuat nsx ON h.MaNuocSX = nsx.MaNuocSX WHERE h.MaHang = @ma", conn, tran))
                {
                    cmd.Parameters.Add(Db.P("@ma", item.Key));
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        int ton = 0;
                        string ten = item.Key;
                        string tenDayDu = item.Key;
                        if (r.Read())
                        {
                            ton = Convert.ToInt32(r.GetValue(0));
                            ten = Convert.ToString(r.GetValue(1));
                            tenDayDu = HangHoa.TaoTenDayDu(ten, Convert.ToString(r.GetValue(2)), Convert.ToString(r.GetValue(3)), Convert.ToString(r.GetValue(4)), Convert.ToString(r.GetValue(5)));
                        }
                        if (ton < item.Value)
                        {
                            kq.HangThieuTon.Add(new HangThieuTon
                            {
                                MaHang = item.Key, TenHang = ten, TenDayDu = tenDayDu,
                                SoLuongCan = item.Value, TonHienTai = ton
                            });
                        }
                    }
                }
            }
        }
    }
}

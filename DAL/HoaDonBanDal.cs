using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public class HoaDonBanDal : HoaDonDalBase
    {
        protected override string Bang { get { return "HoaDonBan"; } }
        protected override string CotKhoa { get { return "SoHDB"; } }
        protected override string TienTo { get { return "HDB"; } }

        private const string SqlChon =
            "SELECT h.SoHDB, h.MaNV, h.NgayBan, h.MaKhach, h.TongTien, n.TenNV, ISNULL(k.TenKhach, N'Khách lẻ') AS TenKhach, k.DiaChi AS DiaChiKhach, k.DienThoai AS DienThoaiKhach " +
            "FROM HoaDonBan h LEFT JOIN NhanVien n ON h.MaNV = n.MaNV LEFT JOIN KhachHang k ON h.MaKhach = k.MaKhach";

        public List<HoaDonBan> LayTatCa()
        {
            return DocDuLieu.DanhSach<HoaDonBan>(Db.Query(SqlChon + " ORDER BY h.NgayBan DESC, h.SoHDB DESC"));
        }

        public List<HoaDonBan> LayGanDay(int soLuong)
        {
            return DocDuLieu.DanhSach<HoaDonBan>(Db.Query(
                "SELECT TOP (@n) h.SoHDB, h.NgayBan, ISNULL(k.TenKhach, N'Khách lẻ') AS TenKhach, h.TongTien " +
                "FROM HoaDonBan h LEFT JOIN KhachHang k ON h.MaKhach = k.MaKhach " +
                "ORDER BY h.NgayBan DESC, h.SoHDB DESC", Db.P("@n", soLuong)));
        }

        public HoaDonBan TimTheoSo(string so)
        {
            DataTable dt = Db.Query(SqlChon + " WHERE h.SoHDB = @so", Db.P("@so", so));
            if (dt.Rows.Count == 0) return null;

            HoaDonBan hd = DocDuLieu.Dong<HoaDonBan>(dt.Rows[0]);
            // Bảng chi tiết lưu giá bán chốt lúc lập; dữ liệu cũ chưa có thì suy ngược từ thành tiền.
            hd.ChiTiet.AddRange(DocDuLieu.DanhSach<ChiTietHoaDonBan>(Db.Query(
                "SELECT c.SoHDB, c.MaHang, h.TenHang, k.TenKichThuoc, cl.TenChatLieu, m.TenMau, nsx.TenNuocSX, c.SoLuong, " +
                "COALESCE(c.DonGia, CASE WHEN c.SoLuong * (1 - ISNULL(c.GiamGia, 0) / 100.0) = 0 THEN 0 " +
                "ELSE c.ThanhTien / (c.SoLuong * (1 - ISNULL(c.GiamGia, 0) / 100.0)) END) AS DonGia, " +
                "ISNULL(c.GiamGia, 0) AS GiamGia, c.ThanhTien " +
                "FROM ChiTietHoaDonBan c JOIN DMHangHoa h ON c.MaHang = h.MaHang " +
                "LEFT JOIN KichThuoc k ON h.MaKichThuoc = k.MaKichThuoc " +
                "LEFT JOIN ChatLieu cl ON h.MaChatLieu = cl.MaChatLieu " +
                "LEFT JOIN MauSac m ON h.MaMau = m.MaMau " +
                "LEFT JOIN NuocSanXuat nsx ON h.MaNuocSX = nsx.MaNuocSX " +
                "WHERE c.SoHDB = @so ORDER BY c.MaHang",
                Db.P("@so", so))));
            return hd;
        }

        public DateTime? NgayGanNhat()
        {
            object o = Db.Scalar("SELECT MAX(NgayBan) FROM HoaDonBan");
            if (o == null || o is DBNull) return null;
            return Convert.ToDateTime(o);
        }

        public List<HoaDonBan> TimKiem(string maHang, DateTime? ngayBan, decimal? tongTienTu, decimal? tongTienDen)
        {
            return DocDuLieu.DanhSach<HoaDonBan>(Db.Proc("sp_TimKiemHoaDonBan",
                Db.P("@MaHang", maHang), Db.PNgay("@NgayBan", ngayBan),
                Db.P("@TongTienTu", tongTienTu), Db.P("@TongTienDen", tongTienDen)));
        }

        public List<HoaDonBan> Top5NhoNhatTheoQuy(int nam, int quy)
        {
            return DocDuLieu.DanhSach<HoaDonBan>(Db.Proc("sp_BaoCaoTop5HDBanNhoNhatTheoQuy",
                Db.P("@Nam", nam), Db.P("@Quy", quy)));
        }

        public KetQuaGhiHoaDon Luu(HoaDonBan hd)
        {
            KetQuaGhiHoaDon kq = new KetQuaGhiHoaDon();
            Db.GiaoDich(delegate (SqlConnection conn, SqlTransaction tran)
            {
                KiemTraTonVaGia(conn, tran, hd, kq);
                if (kq.HangNgungKinhDoanh.Count > 0 || kq.HangThieuTon.Count > 0 || kq.HangVuotLo.Count > 0 || kq.HangDoiGia.Count > 0) return;

                Db.NonQuery(conn, tran,
                    "INSERT INTO HoaDonBan (SoHDB, MaNV, NgayBan, MaKhach, TongTien) VALUES (@so, @nv, @ngay, @kh, 0)",
                    Db.P("@so", hd.SoHDB), Db.P("@nv", hd.MaNV), Db.P("@ngay", hd.NgayBan.Date), Db.P("@kh", hd.MaKhach));

                foreach (ChiTietHoaDonBan d in hd.ChiTiet)
                {
                    Db.NonQuery(conn, tran,
                        "INSERT INTO ChiTietHoaDonBan (SoHDB, MaHang, SoLuong, DonGia, GiamGia) VALUES (@so, @mh, @sl, @dg, @gg)",
                        Db.P("@so", hd.SoHDB), Db.P("@mh", d.MaHang), Db.P("@sl", d.SoLuong),
                        Db.P("@dg", d.DonGia), Db.P("@gg", d.GiamGia));
                }
                kq.DaGhi = true;
            });
            return kq;
        }

        private static void KiemTraTonVaGia(SqlConnection conn, SqlTransaction tran, HoaDonBan hd, KetQuaGhiHoaDon kq)
        {
            // Khoá các mặt hàng theo thứ tự cố định để các hoá đơn đồng thời không khoá chéo nhau.
            List<ChiTietHoaDonBan> dong = new List<ChiTietHoaDonBan>(hd.ChiTiet);
            dong.Sort((a, b) => string.Compare(a.MaHang, b.MaHang, StringComparison.OrdinalIgnoreCase));

            foreach (ChiTietHoaDonBan d in dong)
            {
                int ton;
                string ten;
                string tenDayDu;
                decimal gia;
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT ISNULL(h.SoLuong, 0), h.TenHang, ISNULL(h.DonGiaBan, 0), m.TenMau, k.TenKichThuoc, cl.TenChatLieu, nsx.TenNuocSX, ISNULL(h.NgungKinhDoanh, 0) " +
                    "FROM DMHangHoa h WITH (UPDLOCK, HOLDLOCK) " +
                    "LEFT JOIN MauSac m ON h.MaMau = m.MaMau " +
                    "LEFT JOIN KichThuoc k ON h.MaKichThuoc = k.MaKichThuoc " +
                    "LEFT JOIN ChatLieu cl ON h.MaChatLieu = cl.MaChatLieu " +
                    "LEFT JOIN NuocSanXuat nsx ON h.MaNuocSX = nsx.MaNuocSX WHERE h.MaHang = @ma", conn, tran))
                {
                    cmd.Parameters.Add(Db.P("@ma", d.MaHang));
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (!r.Read())
                        {
                            kq.HangThieuTon.Add(new HangThieuTon
                            {
                                MaHang = d.MaHang, TenHang = d.MaHang, SoLuongCan = d.SoLuong, KhongTonTai = true
                            });
                            continue;
                        }

                        ton = Convert.ToInt32(r.GetValue(0));
                        ten = Convert.ToString(r.GetValue(1));
                        gia = Convert.ToDecimal(r.GetValue(2));
                        tenDayDu = HangHoa.TaoTenDayDu(ten, Convert.ToString(r.GetValue(3)), Convert.ToString(r.GetValue(4)), Convert.ToString(r.GetValue(5)), Convert.ToString(r.GetValue(6)));
                        if (Convert.ToBoolean(r.GetValue(7)))
                        {
                            kq.HangNgungKinhDoanh.Add(new HangNgungKinhDoanh { MaHang = d.MaHang, TenDayDu = tenDayDu });
                            continue;
                        }
                    }
                }

                if (ton < d.SoLuong)
                {
                    kq.HangThieuTon.Add(new HangThieuTon
                    {
                        MaHang = d.MaHang, TenHang = ten, TenDayDu = tenDayDu,
                        SoLuongCan = d.SoLuong, TonHienTai = ton
                    });
                }
                if (d.DonGia != gia) kq.HangDoiGia.Add(new HangDoiGia { MaHang = d.MaHang, TenDayDu = tenDayDu });

                using (SqlCommand cmdLo = new SqlCommand(
                    "SELECT dbo.fn_ConLaiLoDangBan(@ma)", conn, tran))
                {
                    cmdLo.Parameters.Add(Db.P("@ma", d.MaHang));
                    int conLaiLo = Convert.ToInt32(cmdLo.ExecuteScalar());
                    if (ton >= d.SoLuong && d.SoLuong > conLaiLo)
                    {
                        kq.HangVuotLo.Add(new HangVuotLo
                        {
                            MaHang = d.MaHang, TenHang = ten, TenDayDu = tenDayDu, SoLuongCan = d.SoLuong,
                            ConLaiLo = conLaiLo, TonHienTai = ton
                        });
                    }
                }
            }
        }

    }
}


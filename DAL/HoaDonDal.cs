using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public static class HoaDonDal
    {
        private class Meta
        {
            public string Bang;
            public string Khoa;
            public string Ngay;
            public string DoiTac;
            public string BangCt;
            public string TienTo;
        }

        private static Meta Lay(LoaiHoaDon loai)
        {
            if (loai == LoaiHoaDon.Ban)
            {
                return new Meta
                {
                    Bang = "HoaDonBan", Khoa = "SoHDB", Ngay = "NgayBan", DoiTac = "MaKhach",
                    BangCt = "ChiTietHoaDonBan", TienTo = "HDB"
                };
            }
            return new Meta
            {
                Bang = "HoaDonNhap", Khoa = "SoHDN", Ngay = "NgayNhap", DoiTac = "MaNCC",
                BangCt = "ChiTietHoaDonNhap", TienTo = "HDN"
            };
        }

        public static DataTable DanhSach(LoaiHoaDon loai)
        {
            Meta m = Lay(loai);
            return Db.Query(
                "SELECT " + m.Khoa + ", " + m.Ngay + ", TongTien FROM " + m.Bang +
                " ORDER BY " + m.Ngay + " DESC, " + m.Khoa + " DESC");
        }

        public static DataRow Dau(LoaiHoaDon loai, string so)
        {
            Meta m = Lay(loai);
            DataTable dt = Db.Query("SELECT * FROM " + m.Bang + " WHERE " + m.Khoa + " = @so", Db.P("@so", so));
            return dt.Rows.Count == 0 ? null : dt.Rows[0];
        }

        public static DataTable ChiTiet(LoaiHoaDon loai, string so)
        {
            if (loai == LoaiHoaDon.Ban)
            {
                return Db.Query(
                    "SELECT c.MaHang, h.TenHang, c.SoLuong, " +
                    "CASE WHEN c.SoLuong * (1 - ISNULL(c.GiamGia, 0) / 100.0) = 0 THEN 0 " +
                    "ELSE c.ThanhTien / (c.SoLuong * (1 - ISNULL(c.GiamGia, 0) / 100.0)) END AS DonGia, " +
                    "ISNULL(c.GiamGia, 0) AS GiamGia, c.ThanhTien " +
                    "FROM ChiTietHoaDonBan c JOIN DMHangHoa h ON c.MaHang = h.MaHang " +
                    "WHERE c.SoHDB = @so ORDER BY c.MaHang",
                    Db.P("@so", so));
            }
            return Db.Query(
                "SELECT c.MaHang, h.TenHang, c.SoLuong, c.DonGia, ISNULL(c.GiamGia, 0) AS GiamGia, c.ThanhTien " +
                "FROM ChiTietHoaDonNhap c JOIN DMHangHoa h ON c.MaHang = h.MaHang " +
                "WHERE c.SoHDN = @so ORDER BY c.MaHang",
                Db.P("@so", so));
        }

        public static bool TonTai(LoaiHoaDon loai, string so)
        {
            Meta m = Lay(loai);
            int n = Convert.ToInt32(Db.Scalar(
                "SELECT COUNT(*) FROM " + m.Bang + " WHERE " + m.Khoa + " = @so", Db.P("@so", so)));
            return n > 0;
        }

        public static int? TonKho(string maHang)
        {
            object o = Db.Scalar("SELECT ISNULL(SoLuong, 0) FROM DMHangHoa WHERE MaHang = @m", Db.P("@m", maHang));
            if (o == null || o is DBNull) return null;
            return Convert.ToInt32(o);
        }

        public static string TenHang(string maHang)
        {
            object o = Db.Scalar("SELECT TenHang FROM DMHangHoa WHERE MaHang = @m", Db.P("@m", maHang));
            return (o == null || o is DBNull) ? maHang : Convert.ToString(o);
        }

        public static string SinhMa(LoaiHoaDon loai)
        {
            Meta m = Lay(loai);
            object o = Db.Scalar(
                "SELECT MAX(TRY_CAST(SUBSTRING(" + m.Khoa + ", 4, 20) AS INT)) FROM " + m.Bang +
                " WHERE " + m.Khoa + " LIKE '" + m.TienTo + "%'");
            int max = (o == null || o is DBNull) ? 0 : Convert.ToInt32(o);
            return m.TienTo + (max + 1).ToString("00");
        }

        public static DataTable HangNhapKhongDuTon(string soHDN)
        {
            return Db.Query(
                "SELECT c.MaHang, h.TenHang, c.SoLuong AS SoLuongNhap, ISNULL(h.SoLuong, 0) AS TonHienTai " +
                "FROM ChiTietHoaDonNhap c JOIN DMHangHoa h ON c.MaHang = h.MaHang " +
                "WHERE c.SoHDN = @so AND ISNULL(h.SoLuong, 0) < c.SoLuong",
                Db.P("@so", soHDN));
        }

        public static void Luu(HoaDon hd)
        {
            Meta m = Lay(hd.Loai);

            Db.GiaoDich(delegate (SqlConnection conn, SqlTransaction tran)
            {
                Db.NonQuery(conn, tran,
                    "INSERT INTO " + m.Bang + " (" + m.Khoa + ", MaNV, " + m.Ngay + ", " + m.DoiTac + ", TongTien) " +
                    "VALUES (@so, @nv, @ngay, @dt, 0)",
                    Db.P("@so", hd.So), Db.P("@nv", hd.MaNV), Db.P("@ngay", hd.Ngay.Date), Db.P("@dt", hd.MaDoiTac));

                foreach (DongHoaDon d in hd.Dong)
                {
                    if (hd.Loai == LoaiHoaDon.Ban)
                    {
                        Db.NonQuery(conn, tran,
                            "INSERT INTO ChiTietHoaDonBan (SoHDB, MaHang, SoLuong, GiamGia) VALUES (@so, @mh, @sl, @gg)",
                            Db.P("@so", hd.So), Db.P("@mh", d.MaHang), Db.P("@sl", d.SoLuong), Db.P("@gg", d.GiamGia));
                    }
                    else
                    {
                        Db.NonQuery(conn, tran,
                            "INSERT INTO ChiTietHoaDonNhap (SoHDN, MaHang, SoLuong, DonGia, GiamGia) " +
                            "VALUES (@so, @mh, @sl, @dg, @gg)",
                            Db.P("@so", hd.So), Db.P("@mh", d.MaHang), Db.P("@sl", d.SoLuong),
                            Db.P("@dg", d.DonGia), Db.P("@gg", d.GiamGia));
                    }
                }
            });
        }

        public static void Xoa(LoaiHoaDon loai, string so)
        {
            Meta m = Lay(loai);
            Db.NonQuery("DELETE FROM " + m.Bang + " WHERE " + m.Khoa + " = @so", Db.P("@so", so));
        }
    }
}

using System;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public  class ThongTinPhien
    {
        public string MaTaiKhoan { get; private set; }
        public string MaNV { get; private set; }
        public string TenNV { get; private set; }
        public string TenDangNhap { get; private set; }
        public string VaiTro { get; private set; }
        public bool DaDangNhap { get { return true; } }

        internal ThongTinPhien(TaiKhoan t)
        {
            MaTaiKhoan = t.MaTaiKhoan;
            MaNV = t.MaNV;
            TenNV = t.TenNV;
            TenDangNhap = t.TenDangNhap;
            VaiTro = t.VaiTro;
        }
    }

    public static class PhienDangNhap
    {
        private static ThongTinPhien hienTai;
        public static ThongTinPhien HienTai { get { return hienTai; } }
        public static bool DaDangNhap { get { return hienTai != null; } }

        internal static void ThietLap(TaiKhoan taiKhoan)
        {
            if (taiKhoan == null || !taiKhoan.TrangThai) throw new InvalidOperationException("Tài khoản đăng nhập không hợp lệ.");
            hienTai = new ThongTinPhien(taiKhoan);
        }

        public static void DangXuat() { hienTai = null; }

        public static ThongTinPhien YeuCauHieuLuc()
        {
            ThongTinPhien p = hienTai;
            if (p == null) throw new NghiepVuException("Phiên đăng nhập đã kết thúc. Vui lòng đăng nhập lại.");
            TaiKhoan db = new TaiKhoanDal().KiemTraPhien(p.MaTaiKhoan);
            if (db == null || !string.Equals(db.MaNV, p.MaNV, StringComparison.Ordinal) ||
                !string.Equals(db.VaiTro, p.VaiTro, StringComparison.OrdinalIgnoreCase))
            {
                DangXuat();
                throw new NghiepVuException("Tài khoản đã bị khóa hoặc thay đổi. Vui lòng đăng nhập lại.");
            }
            return p;
        }
    }
}

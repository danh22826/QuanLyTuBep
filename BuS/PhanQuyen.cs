using System;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public enum Quyen
    {
        XemTrangChu, BanHang, XemLichSuBan, TimSanPham, XemTonKho,
        QuanLyKhachHang, XoaKhachHang, XuatKhachHang, XemDanhMuc,
        NhapHang, QuanLyNhaCungCap, QuanLyNhanVien, QuanLyDanhMuc,
        QuanLyHangHoa, XemGiaVon, BaoCaoDoanhThu, QuanLyTaiKhoan,
        DoiMatKhau, DangXuat
    }

    public static class PhanQuyen
    {
        public static bool CoQuyen(Quyen quyen)
        {
            ThongTinPhien p = PhienDangNhap.HienTai;
            if (p == null) return false;
            if (string.Equals(p.VaiTro, "ADMIN", StringComparison.OrdinalIgnoreCase)) return true;
            if (!string.Equals(p.VaiTro, "STAFF", StringComparison.OrdinalIgnoreCase)) return false;
            switch (quyen)
            {
                case Quyen.XemTrangChu:
                case Quyen.BanHang:
                case Quyen.XemLichSuBan:
                case Quyen.TimSanPham:
                case Quyen.XemTonKho:
                case Quyen.XemDanhMuc:
                case Quyen.QuanLyKhachHang:
                case Quyen.DoiMatKhau:
                case Quyen.DangXuat:
                    return true;
                default:
                    return false;
            }
        }

        public static void YeuCau(Quyen quyen)
        {
            PhienDangNhap.YeuCauHieuLuc();
            if (!CoQuyen(quyen)) throw new NghiepVuException("Tài khoản hiện tại không có quyền thực hiện chức năng này.");
        }

        public static void YeuCauQuanTri()
        {
            YeuCau(Quyen.QuanLyTaiKhoan);
        }

        internal static Quyen QuyenBang(Type loai, string hanhDong)
        {
            if (loai == typeof(HangHoa)) return Quyen.QuanLyHangHoa;
            if (loai == typeof(NhanVien)) return Quyen.QuanLyNhanVien;
            if (loai == typeof(NhaCungCap)) return Quyen.QuanLyNhaCungCap;
            if (loai == typeof(KhachHang)) return hanhDong == "Xoa" ? Quyen.XoaKhachHang : Quyen.QuanLyKhachHang;
            return hanhDong == "Xem" ? Quyen.XemDanhMuc : Quyen.QuanLyDanhMuc;
        }
    }
}

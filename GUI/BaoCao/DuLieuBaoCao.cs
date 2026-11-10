using System;
using System.Collections.Generic;
using System.Data;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    internal static class DuLieuBaoCao
    {
        internal static DataTable Top3SanPham(IEnumerable<ThongKeSanPham> sanPham)
        {
            if (sanPham == null) throw new ArgumentNullException(nameof(sanPham));

            var bang = new DataTable("Top3SanPham");
            bang.Columns.Add(nameof(ThongKeSanPham.TenHang), typeof(string));
            bang.Columns.Add(nameof(ThongKeSanPham.TongSoLuongBan), typeof(int));
            bang.Columns.Add(nameof(ThongKeSanPham.TongDoanhThu), typeof(decimal));

            foreach (ThongKeSanPham dong in sanPham)
                bang.Rows.Add(dong.TenHang, dong.TongSoLuongBan, dong.TongDoanhThu);

            return bang;
        }

        internal static DataTable KhachHangTheoThang(IEnumerable<ThongKeKhachHang> khachHang)
        {
            var bang = new DataTable("KhachHangTheoThang");
            bang.Columns.Add(nameof(ThongKeKhachHang.TenKhach), typeof(string));
            bang.Columns.Add(nameof(ThongKeKhachHang.DienThoai), typeof(string));
            bang.Columns.Add(nameof(ThongKeKhachHang.SoLuotMua), typeof(int));
            bang.Columns.Add(nameof(ThongKeKhachHang.TongTienMua), typeof(decimal));
            foreach (ThongKeKhachHang dong in khachHang)
                bang.Rows.Add(dong.TenKhach, dong.DienThoai, dong.SoLuotMua, dong.TongTienMua);
            return bang;
        }

        internal static DataTable HoaDonNhapTheoNV(IEnumerable<DongHoaDonNhapTheoNV> hoaDon)
        {
            var bang = new DataTable("HoaDonNhapTheoNV");
            bang.Columns.Add(nameof(DongHoaDonNhapTheoNV.SoHDN), typeof(string));
            bang.Columns.Add(nameof(DongHoaDonNhapTheoNV.NgayNhap), typeof(DateTime));
            bang.Columns.Add(nameof(DongHoaDonNhapTheoNV.TenNCC), typeof(string));
            bang.Columns.Add(nameof(DongHoaDonNhapTheoNV.TenHang), typeof(string));
            bang.Columns.Add(nameof(DongHoaDonNhapTheoNV.SoLuong), typeof(int));
            bang.Columns.Add(nameof(DongHoaDonNhapTheoNV.ThanhTien), typeof(decimal));
            foreach (DongHoaDonNhapTheoNV dong in hoaDon)
                bang.Rows.Add(dong.SoHDN, dong.NgayNhap, dong.TenNCC, dong.TenHang, dong.SoLuong, dong.ThanhTien);
            return bang;
        }

        internal static DataTable Top5HoaDonBan(IEnumerable<HoaDonBan> hoaDon)
        {
            var bang = new DataTable("Top5HoaDonBan");
            bang.Columns.Add(nameof(HoaDonBan.SoHDB), typeof(string));
            bang.Columns.Add(nameof(HoaDonBan.NgayBan), typeof(DateTime));
            bang.Columns.Add(nameof(HoaDonBan.TenKhach), typeof(string));
            bang.Columns.Add(nameof(HoaDonBan.TongTien), typeof(decimal));
            foreach (HoaDonBan dong in hoaDon)
                bang.Rows.Add(dong.SoHDB, dong.NgayBan, dong.TenKhach, dong.TongTien);
            return bang;
        }
    }
}

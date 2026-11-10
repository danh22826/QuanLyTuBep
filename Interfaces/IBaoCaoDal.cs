using System.Collections.Generic;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public interface IBaoCaoDal
    {
        List<ThongKeSanPham> Top3SanPhamTheoQuy(int nam, int quy);
        List<DongHoaDonNhapTheoNV> HoaDonNhapTheoNhanVien(string maNV);
        List<ThongKeKhachHang> KhachHangTheoThang(int nam, int thang);
        TongQuan TongQuan();
    }
}

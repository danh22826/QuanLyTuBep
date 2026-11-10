using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public interface INhanVienDal : IBangDal<NhanVien>
    {
        string SinhMaTuDong(string tienTo);
        int DemHoaDonBan(string ma);
        int DemHoaDonNhap(string ma);
    }
}

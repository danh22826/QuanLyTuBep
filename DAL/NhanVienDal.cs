using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public class NhanVienDal : BangDal<NhanVien>
    {
        protected override string Bang { get { return "NhanVien"; } }
        protected override string CotKhoa { get { return "MaNV"; } }
        protected override string[] CotGhi { get { return new[] { "MaNV", "TenNV", "GioiTinh", "NgaySinh", "DienThoai", "DiaChi", "MaCV" }; } }

        protected override string TruyVanChon
        {
            get
            {
                return "SELECT n.MaNV, n.TenNV, n.GioiTinh, n.NgaySinh, n.DienThoai, n.DiaChi, n.MaCV, v.TenCV " +
                       "FROM NhanVien n LEFT JOIN CongViec v ON n.MaCV = v.MaCV";
            }
        }

        protected override string KhoaTruyVan { get { return "n.MaNV"; } }

        public int DemHoaDonBan(string ma) { return Dem("HoaDonBan", "MaNV", ma); }
        public int DemHoaDonNhap(string ma) { return Dem("HoaDonNhap", "MaNV", ma); }
    }
}

using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class NuocSanXuatBll : BangBll<NuocSanXuat>
    {
        private readonly NuocSanXuatDal dal = new NuocSanXuatDal();

        protected override BangDal<NuocSanXuat> Dal { get { return dal; } }
        public override string TenDoiTuong { get { return "nước sản xuất"; } }
        public override string TruongKhoa { get { return "MaNuocSX"; } }
        public override string SinhMaMoi() { return dal.SinhMaTuDong("NSX"); }

        protected override void KiemTra(NuocSanXuat x, bool themMoi)
        {
            KiemTraDuLieu.Ma(x.MaNuocSX, "Mã nước SX", "MaNuocSX");
            KiemTraDuLieu.Chu(x.TenNuocSX, "Tên nước SX", 50, true, "TenNuocSX");
        }

        protected override void KiemTraTruocKhiXoa(string ma)
        {
            CamNeuCon(dal.DemHangHoa(ma), "hàng hoá", ma);
        }
    }
}


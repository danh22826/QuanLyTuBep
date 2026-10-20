using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class MauSacBll : BangBll<MauSac>
    {
        private readonly MauSacDal dal = new MauSacDal();

        protected override BangDal<MauSac> Dal { get { return dal; } }
        public override string TenDoiTuong { get { return "màu sắc"; } }
        public override string TruongKhoa { get { return "MaMau"; } }
        public override string SinhMaMoi() { return dal.SinhMaTuDong("M"); }

        protected override void KiemTra(MauSac x, bool themMoi)
        {
            KiemTraDuLieu.Ma(x.MaMau, "Mã màu", "MaMau");
            KiemTraDuLieu.Chu(x.TenMau, "Tên màu", 50, true, "TenMau");
        }

        protected override void KiemTraTruocKhiXoa(string ma)
        {
            CamNeuCon(dal.DemHangHoa(ma), "hàng hoá", ma);
        }
    }
}


using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class KichThuocBll : BangBll<KichThuoc>
    {
        private readonly KichThuocDal dal = new KichThuocDal();

        protected override BangDal<KichThuoc> Dal { get { return dal; } }
        public override string TenDoiTuong { get { return "kích thước"; } }
        public override string TruongKhoa { get { return "MaKichThuoc"; } }
        public override string SinhMaMoi() { return dal.SinhMaTuDong("KT"); }

        protected override void KiemTra(KichThuoc x, bool themMoi)
        {
            KiemTraDuLieu.Ma(x.MaKichThuoc, "Mã kích thước", "MaKichThuoc");
            KiemTraDuLieu.Chu(x.TenKichThuoc, "Tên kích thước", 50, true, "TenKichThuoc");
        }

        protected override void KiemTraTruocKhiXoa(string ma)
        {
            CamNeuCon(dal.DemHangHoa(ma), "hàng hoá", ma);
        }
    }
}


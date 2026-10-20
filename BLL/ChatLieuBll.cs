using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class ChatLieuBll : BangBll<ChatLieu>
    {
        private readonly ChatLieuDal dal = new ChatLieuDal();

        protected override BangDal<ChatLieu> Dal { get { return dal; } }
        public override string TenDoiTuong { get { return "chất liệu"; } }
        public override string TruongKhoa { get { return "MaChatLieu"; } }
        public override string SinhMaMoi() { return dal.SinhMaTuDong("CL"); }

        protected override void KiemTra(ChatLieu x, bool themMoi)
        {
            KiemTraDuLieu.Ma(x.MaChatLieu, "Mã chất liệu", "MaChatLieu");
            KiemTraDuLieu.Chu(x.TenChatLieu, "Tên chất liệu", 50, true, "TenChatLieu");
        }

        protected override void KiemTraTruocKhiXoa(string ma)
        {
            CamNeuCon(dal.DemHangHoa(ma), "hàng hoá", ma);
        }
    }
}


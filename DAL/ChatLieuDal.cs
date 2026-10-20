using System.Collections.Generic;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public class ChatLieuDal : BangDal<ChatLieu>
    {
        protected override string Bang { get { return "ChatLieu"; } }
        protected override string CotKhoa { get { return "MaChatLieu"; } }
        protected override string[] CotGhi { get { return new[] { "MaChatLieu", "TenChatLieu" }; } }

        public int DemHangHoa(string ma)
        {
            return Dem("DMHangHoa", "MaChatLieu", ma);
        }
    }
}

using System.ComponentModel;

namespace QuanLyBanTuBep.Models
{
    public class ChatLieu
    {
        [DisplayName("Mã chất liệu")]
        public string MaChatLieu { get; set; }

        [DisplayName("Chất liệu")]
        public string TenChatLieu { get; set; }
    }
}

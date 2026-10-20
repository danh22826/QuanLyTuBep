using System.ComponentModel;

namespace QuanLyBanTuBep.Models
{
    public class MauSac
    {
        [DisplayName("Mã màu")]
        public string MaMau { get; set; }

        [DisplayName("Màu sắc")]
        public string TenMau { get; set; }
    }
}

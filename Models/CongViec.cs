using System.ComponentModel;

namespace QuanLyBanTuBep.Models
{
    public class CongViec
    {
        [DisplayName("Mã công việc")]
        public string MaCV { get; set; }

        [DisplayName("Tên công việc")]
        public string TenCV { get; set; }

        [DisplayName("Mức lương")]
        public decimal MucLuong { get; set; }
    }
}

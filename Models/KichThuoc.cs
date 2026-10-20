using System.ComponentModel;

namespace QuanLyBanTuBep.Models
{
    public class KichThuoc
    {
        [DisplayName("Mã kích thước")]
        public string MaKichThuoc { get; set; }

        [DisplayName("Kích thước")]
        public string TenKichThuoc { get; set; }
    }
}

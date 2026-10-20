using System.ComponentModel;

namespace QuanLyBanTuBep.Models
{
    public class NuocSanXuat
    {
        [DisplayName("Mã nước SX")]
        public string MaNuocSX { get; set; }

        [DisplayName("Nước sản xuất")]
        public string TenNuocSX { get; set; }
    }
}

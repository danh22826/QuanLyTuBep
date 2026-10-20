using System.ComponentModel;

namespace QuanLyBanTuBep.Models
{
    public class NhaCungCap
    {
        [DisplayName("Mã NCC")]
        public string MaNCC { get; set; }

        [DisplayName("Nhà cung cấp")]
        public string TenNCC { get; set; }

        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [DisplayName("Điện thoại")]
        public string DienThoai { get; set; }

        public string TenHienThi
        {
            get { return MaNCC + " - " + TenNCC; }
        }
    }
}

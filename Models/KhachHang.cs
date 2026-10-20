using System;
using System.ComponentModel;

namespace QuanLyBanTuBep.Models
{
    public class KhachHang
    {
        public const string MaKhachLe = "KL00";
        [DisplayName("Mã khách")]
        public string MaKhach { get; set; }

        [DisplayName("Tên khách hàng")]
        public string TenKhach { get; set; }

        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [DisplayName("Điện thoại")]
        public string DienThoai { get; set; }

        public string TenHienThi
        {
            get
            {
                if (string.Equals(MaKhach, MaKhachLe, StringComparison.OrdinalIgnoreCase)) return "KL00 - Khách lẻ";
                string ten = (TenKhach ?? "").Trim();
                string dienThoai = (DienThoai ?? "").Trim();
                string hienThi = MaKhach + " - " + ten;
                return dienThoai.Length == 0 ? hienThi : hienThi + " - " + dienThoai;
            }
        }
    }
}

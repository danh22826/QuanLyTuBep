using System;
using System.ComponentModel;

namespace QuanLyBanTuBep.Models
{
    public class NhanVien
    {
        [DisplayName("Mã NV")]
        public string MaNV { get; set; }

        [DisplayName("Nhân viên")]
        public string TenNV { get; set; }

        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; }

        [DisplayName("Ngày sinh")]
        public DateTime? NgaySinh { get; set; }

        [DisplayName("Điện thoại")]
        public string DienThoai { get; set; }

        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        public string MaCV { get; set; }

        [DisplayName("Công việc")]
        public string TenCV { get; set; }

        public string TenHienThi
        {
            get { return MaNV + " - " + TenNV; }
        }
    }
}

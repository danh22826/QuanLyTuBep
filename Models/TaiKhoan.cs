using System;

namespace QuanLyBanTuBep.Models
{
    public sealed class TaiKhoan
    {
        public string MaTaiKhoan { get; set; }
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhauHash { get; set; }
        public string VaiTro { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? LanDangNhapCuoi { get; set; }
    }
}

using System;
using System.ComponentModel;

namespace QuanLyBanTuBep.Models
{
    public class ThongKeSanPham
    {
        [DisplayName("Mã hàng")]
        public string MaHang { get; set; }

        [DisplayName("Tên hàng")]
        public string TenHang { get; set; }

        [DisplayName("Kích thước")]
        public string TenKichThuoc { get; set; }

        [DisplayName("Chất liệu")]
        public string TenChatLieu { get; set; }

        [DisplayName("Màu sắc")]
        public string TenMau { get; set; }

        [DisplayName("Nước sản xuất")]
        public string TenNuocSX { get; set; }

        

        [DisplayName("Tổng SL bán")]
        public int TongSoLuongBan { get; set; }

        [DisplayName("Doanh thu")]
        public decimal TongDoanhThu { get; set; }
    }

    public class ThongKeKhachHang
    {
        [DisplayName("Mã khách")]
        public string MaKhach { get; set; }

        [DisplayName("Tên khách hàng")]
        public string TenKhach { get; set; }

        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [DisplayName("Điện thoại")]
        public string DienThoai { get; set; }

        [DisplayName("Số lượt mua")]
        public int SoLuotMua { get; set; }

        [DisplayName("Tổng tiền mua")]
        public decimal TongTienMua { get; set; }
    }

    public class DongHoaDonNhapTheoNV
    {
        [DisplayName("Số HĐ nhập")]
        public string SoHDN { get; set; }

        [DisplayName("Ngày nhập")]
        public DateTime NgayNhap { get; set; }

        [DisplayName("Mã NV")]
        public string MaNV { get; set; }

        [DisplayName("Nhân viên")]
        public string TenNV { get; set; }

        [DisplayName("Nhà cung cấp")]
        public string TenNCC { get; set; }

        [DisplayName("Mã hàng")]
        public string MaHang { get; set; }

        [DisplayName("Tên hàng")]
        public string TenHang { get; set; }

        [DisplayName("Kích thước")]
        public string TenKichThuoc { get; set; }

        [DisplayName("Chất liệu")]
        public string TenChatLieu { get; set; }

        [DisplayName("Màu sắc")]
        public string TenMau { get; set; }

        [DisplayName("Nước sản xuất")]
        public string TenNuocSX { get; set; }

        [DisplayName("Số lượng")]
        public int SoLuong { get; set; }

        [DisplayName("Đơn giá")]
        public decimal DonGia { get; set; }

        [DisplayName("Giảm giá (%)")]
        public double GiamGia { get; set; }

        [DisplayName("Thành tiền")]
        public decimal ThanhTien { get; set; }

        [DisplayName("Tổng tiền HĐ")]
        public decimal TongTienHoaDon { get; set; }
    }

    public class TongQuan
    {
        public int SoMatHang { get; set; }
        public int TongTon { get; set; }
        public int SoHoaDonBan { get; set; }
        public decimal DoanhThu { get; set; }
    }
}


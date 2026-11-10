using System.ComponentModel;

namespace QuanLyBanTuBep.Models
{
    public interface IChiTietHoaDon
    {
        [DisplayName("Mã hàng")] string MaHang { get; }
        [DisplayName("Tên hàng")] string TenHang { get; }
        [DisplayName("Kích thước")] string TenKichThuoc { get; }
        [DisplayName("Chất liệu")] string TenChatLieu { get; }
        [DisplayName("Màu sắc")] string TenMau { get; }
        [DisplayName("Nước sản xuất")] string TenNuocSX { get; }
        [DisplayName("Số lượng")] int SoLuong { get; }
        [DisplayName("Đơn giá")] decimal DonGia { get; }
        [DisplayName("Giảm giá (%)")] double GiamGia { get; }
        [DisplayName("Thành tiền")] decimal ThanhTien { get; }
    }
}

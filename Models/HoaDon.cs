using System;
using System.Collections.Generic;
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

    public interface IHoaDon
    {
        [DisplayName("Số hoá đơn")] string So { get; }
        [DisplayName("Ngày")] DateTime Ngay { get; }
        string MaNV { get; }
        string MaDoiTac { get; }
        [DisplayName("Tổng tiền")] decimal TongTien { get; }
    }

    public class ChiTietHoaDonBan : IChiTietHoaDon
    {
        public string SoHDB { get; set; }

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
    }

    public class ChiTietHoaDonNhap : IChiTietHoaDon
    {
        public string SoHDN { get; set; }

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
    }

    public class HoaDonBan : IHoaDon
    {
        [DisplayName("Số HĐ bán")]
        public string SoHDB { get; set; }

        public string MaNV { get; set; }

        [DisplayName("Ngày bán")]
        public DateTime NgayBan { get; set; }

        public string MaKhach { get; set; }

        [DisplayName("Tổng tiền")]
        public decimal TongTien { get; set; }

        [DisplayName("Nhân viên")]
        public string TenNV { get; set; }

        [DisplayName("Tên khách hàng")]
        public string TenKhach { get; set; }

        public string DiaChiKhach { get; set; }

        public string DienThoaiKhach { get; set; }

        public List<ChiTietHoaDonBan> ChiTiet { get; private set; }

        public HoaDonBan()
        {
            ChiTiet = new List<ChiTietHoaDonBan>();
            NgayBan = DateTime.Today;
        }

        string IHoaDon.So { get { return SoHDB; } }
        DateTime IHoaDon.Ngay { get { return NgayBan; } }
        string IHoaDon.MaDoiTac { get { return MaKhach; } }
    }

    public class HoaDonNhap : IHoaDon
    {
        [DisplayName("Số HĐ nhập")]
        public string SoHDN { get; set; }

        public string MaNV { get; set; }

        [DisplayName("Ngày nhập")]
        public DateTime NgayNhap { get; set; }

        public string MaNCC { get; set; }

        [DisplayName("Tổng tiền")]
        public decimal TongTien { get; set; }

        [DisplayName("Nhân viên")]
        public string TenNV { get; set; }

        [DisplayName("Nhà cung cấp")]
        public string TenNCC { get; set; }

        public List<ChiTietHoaDonNhap> ChiTiet { get; private set; }

        public HoaDonNhap()
        {
            ChiTiet = new List<ChiTietHoaDonNhap>();
            NgayNhap = DateTime.Today;
        }

        string IHoaDon.So { get { return SoHDN; } }
        DateTime IHoaDon.Ngay { get { return NgayNhap; } }
        string IHoaDon.MaDoiTac { get { return MaNCC; } }
    }

    public class HangThieuTon
    {
        public string MaHang { get; set; }
        public string TenHang { get; set; }
        public string TenDayDu { get; set; }
        public int SoLuongCan { get; set; }
        public int TonHienTai { get; set; }
        public bool KhongTonTai { get; set; }
    }

    public class HangVuotLo
    {
        public string MaHang { get; set; }
        public string TenHang { get; set; }
        public string TenDayDu { get; set; }
        public int SoLuongCan { get; set; }
        public int ConLaiLo { get; set; }
        public int TonHienTai { get; set; }
    }

    public class HangDoiGia
    {
        public string MaHang { get; set; }
        public string TenDayDu { get; set; }
    }

    public class HangNgungKinhDoanh
    {
        public string MaHang { get; set; }
        public string TenDayDu { get; set; }
    }

    public class KetQuaGhiHoaDon
    {
        public bool HoaDonTonTai { get; set; }
        public bool DaGhi { get; set; }
        public List<HangThieuTon> HangThieuTon { get; private set; }
        public List<HangVuotLo> HangVuotLo { get; private set; }
        public List<HangDoiGia> HangDoiGia { get; private set; }
        public List<HangNgungKinhDoanh> HangNgungKinhDoanh { get; private set; }

        public KetQuaGhiHoaDon()
        {
            HangThieuTon = new List<HangThieuTon>();
            HangVuotLo = new List<HangVuotLo>();
            HangDoiGia = new List<HangDoiGia>();
            HangNgungKinhDoanh = new List<HangNgungKinhDoanh>();
        }
    }
}


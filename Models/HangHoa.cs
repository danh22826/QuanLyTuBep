using System.ComponentModel;
using System.Linq;

namespace QuanLyBanTuBep.Models
{
    public class HangHoa
    {
        [DisplayName("Mã hàng")]
        public string MaHang { get; set; }

        [DisplayName("Tên hàng")]
        public string TenHang { get; set; }

        public string MaKichThuoc { get; set; }
        public string MaChatLieu { get; set; }
        public string MaMau { get; set; }
        public string MaNuocSX { get; set; }

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

        // Số lượng còn lại của lô FIFO hiện đang bán; không phải cột trong DMHangHoa.
        public int ConLaiLo { get; set; }

        [DisplayName("Giá nhập")]
        public decimal GiaNhap { get; set; }

        [DisplayName("Đơn giá bán")]
        public decimal DonGiaBan { get; set; }

        [DisplayName("Bảo hành")]
        public string ThoiGianBaoHanh { get; set; }

        public string Anh { get; set; }

        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }

        [DisplayName("Trạng thái")]
        public bool NgungKinhDoanh { get; set; }

        public string TrangThai
        {
            get { return NgungKinhDoanh ? "Ngừng kinh doanh" : "Đang kinh doanh"; }
        }

        public string AnhNguon { get; set; }

        public string MoTaThuocTinh
        {
            get { return GhepChuoi(" – ", TenMau, TenKichThuoc, TenChatLieu, TenNuocSX); }
        }

        public string MoTaThuocTinhCoNhan
        {
            get
            {
                return GhepChuoi(" | ",
                    GanNhan("Màu", TenMau), GanNhan("Kích thước", TenKichThuoc),
                    GanNhan("Chất liệu", TenChatLieu), GanNhan("Nước SX", TenNuocSX));
            }
        }

        public string TenDayDu
        {
            get { return TaoTenDayDu(TenHang, TenMau, TenKichThuoc, TenChatLieu, TenNuocSX); }
        }

        public string TenHienThi
        {
            get
            {
                string ten = GhepChuoi(" – ", TenChonHang, MoTaThuocTinh);
                return NgungKinhDoanh ? ten + " (ngừng KD)" : ten;
            }
        }

        public string TenChonHang
        {
            get { return GhepChuoi(" - ", MaHang, TenHang); }
        }

        // Dùng chung cách bỏ giá trị trống khi dựng tên hàng ở combo, thông báo và truy vấn DAL.
        public static string GhepChuoi(string dauPhanCach, params string[] cacPhan)
        {
            return string.Join(dauPhanCach, (cacPhan ?? new string[0])
                .Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()));
        }

        public static string TaoTenDayDu(string tenHang, string mau, string kichThuoc, string chatLieu, string nuocSX)
        {
            string moTa = GhepChuoi(", ", mau, kichThuoc, chatLieu, nuocSX);
            return string.IsNullOrEmpty(moTa) ? tenHang : tenHang + " (" + moTa + ")";
        }

        private static string GanNhan(string nhan, string giaTri)
        {
            return string.IsNullOrWhiteSpace(giaTri) ? null : nhan + ": " + giaTri.Trim();
        }
    }
}

using System.Collections.Generic;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public class HangHoaDal : BangDal<HangHoa>
    {
        protected override string Bang { get { return "DMHangHoa"; } }
        protected override string CotKhoa { get { return "MaHang"; } }

        // DonGiaBan không bao giờ ghi từ ứng dụng: trigger của CSDL tự tính = 110% giá nhập.
        protected override string[] CotGhi
        {
            get
            {
                return new[] { "MaHang", "TenHang", "MaKichThuoc", "MaChatLieu", "MaMau", "MaNuocSX",
                               "SoLuong", "GiaNhap", "ThoiGianBaoHanh", "Anh", "GhiChu", "NgungKinhDoanh" };
            }
        }

        // Tồn kho và giá nhập được khởi tạo bằng 0; sau đó chỉ trigger hóa đơn cập nhật.
        protected override string[] CotChiKhiThem { get { return new[] { "SoLuong", "GiaNhap", "NgungKinhDoanh" }; } }

        protected override string TruyVanChon
        {
            get
            {
                return "SELECT h.MaHang, h.TenHang, h.MaKichThuoc, h.MaChatLieu, h.MaMau, h.MaNuocSX, " +
                       "k.TenKichThuoc, l.TenChatLieu, m.TenMau, n.TenNuocSX, " +
                       "h.SoLuong, h.GiaNhap, h.DonGiaBan, dbo.fn_ConLaiLoDangBan(h.MaHang) AS ConLaiLo, " +
                       "h.ThoiGianBaoHanh, h.Anh, h.GhiChu, h.NgungKinhDoanh " +
                       "FROM DMHangHoa h " +
                       "LEFT JOIN KichThuoc k ON h.MaKichThuoc = k.MaKichThuoc " +
                       "LEFT JOIN ChatLieu l ON h.MaChatLieu = l.MaChatLieu " +
                       "LEFT JOIN MauSac m ON h.MaMau = m.MaMau " +
                       "LEFT JOIN NuocSanXuat n ON h.MaNuocSX = n.MaNuocSX";
            }
        }

        protected override string KhoaTruyVan { get { return "h.MaHang"; } }

        public List<HangHoa> LayDangKinhDoanh()
        {
            return DocDuLieu.DanhSach<HangHoa>(Db.Query(
                TruyVanChon + " WHERE h.NgungKinhDoanh = 0 ORDER BY h.MaHang"));
        }

        public int DatTrangThai(string ma, bool ngung)
        {
            return Db.NonQuery("UPDATE DMHangHoa SET NgungKinhDoanh = @v WHERE MaHang = @ma",
                Db.P("@v", ngung), Db.P("@ma", ma));
        }

        public string SinhMaTuDong()
        {
            object o = Db.Scalar("SELECT MAX(TRY_CAST(SUBSTRING(MaHang, 3, 18) AS INT)) FROM DMHangHoa WHERE MaHang LIKE 'TB[0-9]%'");
            int max = (o == null || o is System.DBNull) ? 0 : System.Convert.ToInt32(o);
            return "TB" + (max + 1).ToString("00");
        }

        public int DemChiTietBan(string ma) { return Dem("ChiTietHoaDonBan", "MaHang", ma); }
        public int DemChiTietNhap(string ma) { return Dem("ChiTietHoaDonNhap", "MaHang", ma); }

        public List<HangHoa> TimKiem(string maChatLieu, string maNuocSX, string baoHanh)
        {
            return DocDuLieu.DanhSach<HangHoa>(Db.Proc("sp_TimKiemSanPham",
                Db.P("@MaChatLieu", maChatLieu), Db.P("@MaNuocSX", maNuocSX), Db.P("@ThoiGianBaoHanh", baoHanh)));
        }

        public List<HangHoa> TimKiemQuanLy(string maChatLieu, string maNuocSX, string baoHanh, bool? ngungKinhDoanh)
        {
            return DocDuLieu.DanhSach<HangHoa>(Db.Proc("sp_TimKiemSanPham",
                Db.P("@MaChatLieu", maChatLieu), Db.P("@MaNuocSX", maNuocSX),
                Db.P("@ThoiGianBaoHanh", baoHanh), Db.P("@NgungKinhDoanh", ngungKinhDoanh)));
        }

        public int DemNgungKinhDoanh()
        {
            return System.Convert.ToInt32(Db.Scalar(
                "SELECT COUNT(*) FROM DMHangHoa WHERE NgungKinhDoanh = @ngung",
                Db.P("@ngung", true)));
        }

        public List<HangHoa> TonDuoi(int nguong)
        {
            return DocDuLieu.DanhSach<HangHoa>(Db.Query(
                "SELECT h.MaHang, h.TenHang, k.TenKichThuoc, cl.TenChatLieu, m.TenMau, nsx.TenNuocSX, ISNULL(h.SoLuong, 0) AS SoLuong " +
                "FROM DMHangHoa h " +
                "LEFT JOIN KichThuoc k ON h.MaKichThuoc = k.MaKichThuoc " +
                "LEFT JOIN ChatLieu cl ON h.MaChatLieu = cl.MaChatLieu " +
                "LEFT JOIN MauSac m ON h.MaMau = m.MaMau " +
                "LEFT JOIN NuocSanXuat nsx ON h.MaNuocSX = nsx.MaNuocSX " +
                       "WHERE ISNULL(h.SoLuong, 0) <= @n AND h.NgungKinhDoanh = 0 ORDER BY h.SoLuong, h.MaHang", Db.P("@n", nguong)));
        }
    }
}




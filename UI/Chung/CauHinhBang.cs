using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanTuBep.UI
{
    public enum KieuTruong
    {
        Chu,
        Ngay,
        So,
        Combo,
        Anh
    }

    public class Truong
    {
        public string Cot { get; set; }
        public string Nhan { get; set; }
        public KieuTruong Kieu { get; set; }
        public bool BatBuoc { get; set; }
        public int DoDai { get; set; }
        public bool KhoaChinh { get; set; }

        public bool ChiKhiThem { get; set; }

        public bool ChiDoc { get; set; }

        public string NguonCombo { get; set; }

        public string BieuThuc { get; set; }
        public string BaoSai { get; set; }
    }

    public class PhuThuoc
    {
        public string Bang { get; set; }
        public string Cot { get; set; }
        public string Ten { get; set; }
    }

    public class CauHinhBang
    {
        public string Bang { get; set; }
        public string TenDoiTuong { get; set; }

        public string SqlLuoi { get; set; }

        public List<Truong> DsTruong { get; private set; }
        public List<PhuThuoc> DsPhuThuoc { get; private set; }

        public CauHinhBang()
        {
            DsTruong = new List<Truong>();
            DsPhuThuoc = new List<PhuThuoc>();
        }

        public string CotKhoa
        {
            get { return DsTruong.First(t => t.KhoaChinh).Cot; }
        }
    }

    public static class CauHinh
    {
        private const string MaChuSo = "^[A-Za-z0-9_]+$";
        private const string BaoSaiMa = "Mã chỉ gồm chữ không dấu, số và dấu gạch dưới (không có khoảng trắng).";
        private const string DienThoai = "^[0-9]{9,11}$";
        private const string BaoSaiDt = "Số điện thoại gồm 9 đến 11 chữ số.";

        private static Truong Ma(string cot, string nhan)
        {
            return new Truong
            {
                Cot = cot, Nhan = nhan, Kieu = KieuTruong.Chu, KhoaChinh = true, BatBuoc = true,
                DoDai = 20, BieuThuc = MaChuSo, BaoSai = BaoSaiMa
            };
        }

        private static Truong Chu(string cot, string nhan, int doDai, bool batBuoc)
        {
            return new Truong { Cot = cot, Nhan = nhan, Kieu = KieuTruong.Chu, DoDai = doDai, BatBuoc = batBuoc };
        }

        private static Truong Dt()
        {
            return new Truong
            {
                Cot = "DienThoai", Nhan = "Điện thoại", Kieu = KieuTruong.Chu, DoDai = 20,
                BieuThuc = DienThoai, BaoSai = BaoSaiDt
            };
        }

        private static Truong Combo(string cot, string nhan, string nguon, bool batBuoc)
        {
            return new Truong { Cot = cot, Nhan = nhan, Kieu = KieuTruong.Combo, NguonCombo = nguon, BatBuoc = batBuoc };
        }

        private static PhuThuoc Pt(string bang, string cot, string ten)
        {
            return new PhuThuoc { Bang = bang, Cot = cot, Ten = ten };
        }

        private static CauHinhBang DanhMucDon(string bang, string cotMa, string cotTen, string ten, string nhanMa, string nhanTen, string cotHangHoa)
        {
            CauHinhBang c = new CauHinhBang();
            c.Bang = bang;
            c.TenDoiTuong = ten;
            c.SqlLuoi = "SELECT " + cotMa + ", " + cotTen + " FROM " + bang + " ORDER BY " + cotMa;
            c.DsTruong.Add(Ma(cotMa, nhanMa));
            c.DsTruong.Add(Chu(cotTen, nhanTen, 50, true));
            c.DsPhuThuoc.Add(Pt("DMHangHoa", cotHangHoa, "hàng hoá"));
            return c;
        }

        public static CauHinhBang KichThuoc()
        {
            return DanhMucDon("KichThuoc", "MaKichThuoc", "TenKichThuoc", "kích thước", "Mã kích thước", "Tên kích thước", "MaKichThuoc");
        }

        public static CauHinhBang ChatLieu()
        {
            return DanhMucDon("ChatLieu", "MaChatLieu", "TenChatLieu", "chất liệu", "Mã chất liệu", "Tên chất liệu", "MaChatLieu");
        }

        public static CauHinhBang MauSac()
        {
            return DanhMucDon("MauSac", "MaMau", "TenMau", "màu sắc", "Mã màu", "Tên màu", "MaMau");
        }

        public static CauHinhBang NuocSanXuat()
        {
            return DanhMucDon("NuocSanXuat", "MaNuocSX", "TenNuocSX", "nước sản xuất", "Mã nước SX", "Tên nước SX", "MaNuocSX");
        }

        public static CauHinhBang CongViec()
        {
            CauHinhBang c = new CauHinhBang();
            c.Bang = "CongViec";
            c.TenDoiTuong = "công việc";
            c.SqlLuoi = "SELECT MaCV, TenCV, MucLuong FROM CongViec ORDER BY MaCV";
            c.DsTruong.Add(Ma("MaCV", "Mã công việc"));
            c.DsTruong.Add(Chu("TenCV", "Tên công việc", 100, true));
            c.DsTruong.Add(new Truong { Cot = "MucLuong", Nhan = "Mức lương (đồng)", Kieu = KieuTruong.So });
            c.DsPhuThuoc.Add(Pt("NhanVien", "MaCV", "nhân viên"));
            return c;
        }

        public static CauHinhBang NhanVien()
        {
            CauHinhBang c = new CauHinhBang();
            c.Bang = "NhanVien";
            c.TenDoiTuong = "nhân viên";
            c.SqlLuoi =
                "SELECT n.MaNV, n.TenNV, n.GioiTinh, n.NgaySinh, n.DienThoai, n.DiaChi, v.TenCV " +
                "FROM NhanVien n LEFT JOIN CongViec v ON n.MaCV = v.MaCV ORDER BY n.MaNV";
            c.DsTruong.Add(Ma("MaNV", "Mã nhân viên"));
            c.DsTruong.Add(Chu("TenNV", "Tên nhân viên", 100, true));
            c.DsTruong.Add(Combo("GioiTinh", "Giới tính",
                "SELECT N'Nam' AS Ma, N'Nam' AS Ten UNION ALL SELECT N'Nữ', N'Nữ'", false));
            c.DsTruong.Add(new Truong { Cot = "NgaySinh", Nhan = "Ngày sinh", Kieu = KieuTruong.Ngay });
            c.DsTruong.Add(Dt());
            c.DsTruong.Add(Chu("DiaChi", "Địa chỉ", 200, false));
            c.DsTruong.Add(Combo("MaCV", "Công việc",
                "SELECT MaCV AS Ma, TenCV AS Ten FROM CongViec ORDER BY MaCV", false));
            c.DsPhuThuoc.Add(Pt("HoaDonBan", "MaNV", "hoá đơn bán"));
            c.DsPhuThuoc.Add(Pt("HoaDonNhap", "MaNV", "hoá đơn nhập"));
            return c;
        }

        public static CauHinhBang KhachHang()
        {
            CauHinhBang c = new CauHinhBang();
            c.Bang = "KhachHang";
            c.TenDoiTuong = "khách hàng";
            c.SqlLuoi = "SELECT MaKhach, TenKhach, DiaChi, DienThoai FROM KhachHang ORDER BY MaKhach";
            c.DsTruong.Add(Ma("MaKhach", "Mã khách hàng"));
            c.DsTruong.Add(Chu("TenKhach", "Tên khách hàng", 100, true));
            c.DsTruong.Add(Dt());
            c.DsTruong.Add(Chu("DiaChi", "Địa chỉ", 200, false));
            c.DsPhuThuoc.Add(Pt("HoaDonBan", "MaKhach", "hoá đơn bán"));
            return c;
        }

        public static CauHinhBang NhaCungCap()
        {
            CauHinhBang c = new CauHinhBang();
            c.Bang = "NhaCungCap";
            c.TenDoiTuong = "nhà cung cấp";
            c.SqlLuoi = "SELECT MaNCC, TenNCC, DiaChi, DienThoai FROM NhaCungCap ORDER BY MaNCC";
            c.DsTruong.Add(Ma("MaNCC", "Mã nhà cung cấp"));
            c.DsTruong.Add(Chu("TenNCC", "Tên nhà cung cấp", 150, true));
            c.DsTruong.Add(Dt());
            c.DsTruong.Add(Chu("DiaChi", "Địa chỉ", 200, false));
            c.DsPhuThuoc.Add(Pt("HoaDonNhap", "MaNCC", "hoá đơn nhập"));
            return c;
        }

        public static CauHinhBang HangHoa()
        {
            CauHinhBang c = new CauHinhBang();
            c.Bang = "DMHangHoa";
            c.TenDoiTuong = "hàng hoá";
            c.SqlLuoi =
                "SELECT h.MaHang, h.TenHang, k.TenKichThuoc, l.TenChatLieu, m.TenMau, n.TenNuocSX, " +
                "h.SoLuong, h.GiaNhap, h.DonGiaBan, h.ThoiGianBaoHanh, h.GhiChu " +
                "FROM DMHangHoa h " +
                "LEFT JOIN KichThuoc k ON h.MaKichThuoc = k.MaKichThuoc " +
                "LEFT JOIN ChatLieu l ON h.MaChatLieu = l.MaChatLieu " +
                "LEFT JOIN MauSac m ON h.MaMau = m.MaMau " +
                "LEFT JOIN NuocSanXuat n ON h.MaNuocSX = n.MaNuocSX " +
                "ORDER BY h.MaHang";

            c.DsTruong.Add(Ma("MaHang", "Mã hàng"));
            c.DsTruong.Add(Chu("TenHang", "Tên hàng hoá", 150, true));
            c.DsTruong.Add(Combo("MaKichThuoc", "Kích thước",
                "SELECT MaKichThuoc AS Ma, TenKichThuoc AS Ten FROM KichThuoc ORDER BY MaKichThuoc", true));
            c.DsTruong.Add(Combo("MaChatLieu", "Chất liệu",
                "SELECT MaChatLieu AS Ma, TenChatLieu AS Ten FROM ChatLieu ORDER BY MaChatLieu", true));
            c.DsTruong.Add(Combo("MaMau", "Màu sắc",
                "SELECT MaMau AS Ma, TenMau AS Ten FROM MauSac ORDER BY MaMau", true));
            c.DsTruong.Add(Combo("MaNuocSX", "Nước sản xuất",
                "SELECT MaNuocSX AS Ma, TenNuocSX AS Ten FROM NuocSanXuat ORDER BY MaNuocSX", true));

            c.DsTruong.Add(new Truong { Cot = "SoLuong", Nhan = "Số lượng tồn (nhập khi thêm mới)", Kieu = KieuTruong.So, ChiKhiThem = true });

            c.DsTruong.Add(new Truong { Cot = "GiaNhap", Nhan = "Giá nhập (đồng)", Kieu = KieuTruong.So });

            c.DsTruong.Add(new Truong { Cot = "DonGiaBan", Nhan = "Giá bán (tự động 110%)", Kieu = KieuTruong.So, ChiDoc = true });

            c.DsTruong.Add(Chu("ThoiGianBaoHanh", "Thời gian bảo hành (vd: 24 tháng)", 50, false));
            c.DsTruong.Add(new Truong { Cot = "Anh", Nhan = "Ảnh sản phẩm", Kieu = KieuTruong.Anh });
            c.DsTruong.Add(Chu("GhiChu", "Ghi chú", 500, false));

            c.DsPhuThuoc.Add(Pt("ChiTietHoaDonBan", "MaHang", "hoá đơn bán"));
            c.DsPhuThuoc.Add(Pt("ChiTietHoaDonNhap", "MaHang", "hoá đơn nhập"));
            return c;
        }
    }
}

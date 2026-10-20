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
        Anh,
        CheckBox
    }

    public class Truong
    {
        public string ThuocTinh { get; set; }
        public string Nhan { get; set; }
        public KieuTruong Kieu { get; set; }
        public bool KhoaChinh { get; set; }

        public bool ChiKhiThem { get; set; }

        public bool ChiDoc { get; set; }

        public bool TuDongSinh { get; set; }

        public bool ChoPhepRong { get; set; }
    }

    public class CauHinhBang
    {
        public string TenFileCsv { get; set; }
        public string[] CotLuoi { get; set; }
        public bool DoiMauChuNutTheoCheDo { get; set; }
        public bool DungNgungKinhDoanh { get; set; }
        public List<Truong> DsTruong { get; private set; }

        public CauHinhBang()
        {
            DsTruong = new List<Truong>();
            DoiMauChuNutTheoCheDo = true;
        }

        public Truong TruongKhoa
        {
            get { return DsTruong.First(t => t.KhoaChinh); }
        }
    }

    public static class CauHinh
    {
        private static Truong Ma(string thuocTinh, string nhan)
        {
            return new Truong { ThuocTinh = thuocTinh, Nhan = nhan, Kieu = KieuTruong.Chu, KhoaChinh = true };
        }

        private static Truong MaTuDong(string thuocTinh, string nhan)
        {
            return new Truong { ThuocTinh = thuocTinh, Nhan = nhan + " (tự động)", Kieu = KieuTruong.Chu, KhoaChinh = true, TuDongSinh = true };
        }

        private static Truong Chu(string thuocTinh, string nhan)
        {
            return new Truong { ThuocTinh = thuocTinh, Nhan = nhan, Kieu = KieuTruong.Chu };
        }

        private static Truong So(string thuocTinh, string nhan)
        {
            return new Truong { ThuocTinh = thuocTinh, Nhan = nhan, Kieu = KieuTruong.So };
        }

        private static Truong Combo(string thuocTinh, string nhan, bool choPhepRong)
        {
            return new Truong { ThuocTinh = thuocTinh, Nhan = nhan, Kieu = KieuTruong.Combo, ChoPhepRong = choPhepRong };
        }

        private static CauHinhBang DanhMucDon(string file, string ma, string ten, string nhanMa, string nhanTen)
        {
            CauHinhBang c = new CauHinhBang { TenFileCsv = file, CotLuoi = new[] { ma, ten } };
            c.DsTruong.Add(MaTuDong(ma, nhanMa));
            c.DsTruong.Add(Chu(ten, nhanTen));
            return c;
        }

        public static CauHinhBang KichThuoc() { return DanhMucDon("KichThuoc", "MaKichThuoc", "TenKichThuoc", "Mã kích thước", "Tên kích thước"); }
        public static CauHinhBang ChatLieu() { return DanhMucDon("ChatLieu", "MaChatLieu", "TenChatLieu", "Mã chất liệu", "Tên chất liệu"); }
        public static CauHinhBang MauSac() { return DanhMucDon("MauSac", "MaMau", "TenMau", "Mã màu", "Tên màu"); }
        public static CauHinhBang NuocSanXuat() { return DanhMucDon("NuocSanXuat", "MaNuocSX", "TenNuocSX", "Mã nước SX", "Tên nước SX"); }

        public static CauHinhBang CongViec()
        {
            CauHinhBang c = new CauHinhBang { TenFileCsv = "CongViec", CotLuoi = new[] { "MaCV", "TenCV", "MucLuong" } };
            c.DsTruong.Add(MaTuDong("MaCV", "Mã công việc"));
            c.DsTruong.Add(Chu("TenCV", "Tên công việc"));
            c.DsTruong.Add(So("MucLuong", "Mức lương (đồng)"));
            return c;
        }

        public static CauHinhBang NhanVien()
        {
            CauHinhBang c = new CauHinhBang { TenFileCsv = "NhanVien", CotLuoi = new[] { "MaNV", "TenNV", "GioiTinh", "NgaySinh", "DienThoai", "DiaChi", "TenCV" } };
            c.DsTruong.Add(MaTuDong("MaNV", "Mã nhân viên"));
            c.DsTruong.Add(Chu("TenNV", "Tên nhân viên"));
            c.DsTruong.Add(Combo("GioiTinh", "Giới tính", true));
            c.DsTruong.Add(new Truong { ThuocTinh = "NgaySinh", Nhan = "Ngày sinh", Kieu = KieuTruong.Ngay });
            c.DsTruong.Add(Chu("DienThoai", "Điện thoại"));
            c.DsTruong.Add(Chu("DiaChi", "Địa chỉ"));
            c.DsTruong.Add(Combo("MaCV", "Công việc", true));
            return c;
        }

        public static CauHinhBang KhachHang()
        {
            CauHinhBang c = new CauHinhBang { TenFileCsv = "KhachHang", CotLuoi = new[] { "MaKhach", "TenKhach", "DiaChi", "DienThoai" } };
            c.DsTruong.Add(MaTuDong("MaKhach", "Mã khách hàng"));
            c.DsTruong.Add(Chu("TenKhach", "Tên khách hàng"));
            c.DsTruong.Add(Chu("DienThoai", "Điện thoại"));
            c.DsTruong.Add(Chu("DiaChi", "Địa chỉ"));
            return c;
        }

        public static CauHinhBang NhaCungCap()
        {
            CauHinhBang c = new CauHinhBang { TenFileCsv = "NhaCungCap", CotLuoi = new[] { "MaNCC", "TenNCC", "DiaChi", "DienThoai" } };
            c.DsTruong.Add(MaTuDong("MaNCC", "Mã nhà cung cấp"));
            c.DsTruong.Add(Chu("TenNCC", "Tên nhà cung cấp"));
            c.DsTruong.Add(Chu("DienThoai", "Điện thoại"));
            c.DsTruong.Add(Chu("DiaChi", "Địa chỉ"));
            return c;
        }

        public static CauHinhBang HangHoa()
        {
            CauHinhBang c = new CauHinhBang
            {
                TenFileCsv = "HangHoa",
                CotLuoi = new[] { "MaHang", "TenHang", "TenKichThuoc", "TenChatLieu", "TenMau", "TenNuocSX", "SoLuong", "GiaNhap", "DonGiaBan", "ThoiGianBaoHanh", "TrangThai", "GhiChu" },
                DungNgungKinhDoanh = true
            };
            c.DsTruong.Add(new Truong { ThuocTinh = "MaHang", Nhan = "Mã hàng (tự động)", Kieu = KieuTruong.Chu, KhoaChinh = true, TuDongSinh = true });
            c.DsTruong.Add(Chu("TenHang", "Tên hàng hoá"));
            c.DsTruong.Add(new Truong { ThuocTinh = "MaKichThuoc", Nhan = "Kích thước", Kieu = KieuTruong.Combo, ChiKhiThem = true });
            c.DsTruong.Add(new Truong { ThuocTinh = "MaChatLieu", Nhan = "Chất liệu", Kieu = KieuTruong.Combo, ChiKhiThem = true });
            c.DsTruong.Add(new Truong { ThuocTinh = "MaMau", Nhan = "Màu sắc", Kieu = KieuTruong.Combo, ChiKhiThem = true });
            c.DsTruong.Add(new Truong { ThuocTinh = "MaNuocSX", Nhan = "Nước sản xuất", Kieu = KieuTruong.Combo, ChiKhiThem = true });
            c.DsTruong.Add(new Truong { ThuocTinh = "SoLuong", Nhan = "Số lượng tồn", Kieu = KieuTruong.So, ChiDoc = true });
            c.DsTruong.Add(new Truong { ThuocTinh = "GiaNhap", Nhan = "Giá nhập (lô đang bán)", Kieu = KieuTruong.So, ChiDoc = true });
            c.DsTruong.Add(new Truong { ThuocTinh = "DonGiaBan", Nhan = "Giá bán (tự động 110%)", Kieu = KieuTruong.So, ChiDoc = true });
            c.DsTruong.Add(new Truong { ThuocTinh = "NgungKinhDoanh", Nhan = "Ngừng kinh doanh", Kieu = KieuTruong.CheckBox, ChiDoc = true });
            c.DsTruong.Add(Chu("ThoiGianBaoHanh", "Thời gian bảo hành (vd: 24 tháng)"));
            c.DsTruong.Add(new Truong { ThuocTinh = "Anh", Nhan = "Ảnh sản phẩm", Kieu = KieuTruong.Anh });
            c.DsTruong.Add(Chu("GhiChu", "Ghi chú"));
            return c;
        }
    }
}
















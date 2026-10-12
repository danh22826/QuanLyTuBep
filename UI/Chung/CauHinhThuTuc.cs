using System;
using System.Collections.Generic;

namespace QuanLyBanTuBep.UI
{
    public enum KieuThamSo
    {
        Nam,
        Quy,
        Thang,
        ComboDb,
        Chu,
        Ngay,
        Tien
    }

    public class ThamSo
    {
        public string Ten { get; set; }
        public string Nhan { get; set; }
        public KieuThamSo Kieu { get; set; }

        public string Nguon { get; set; }

        public bool ChoPhepTatCa { get; set; }
    }

    public class CauHinhThuTuc
    {
        public string Proc { get; set; }
        public string TenFile { get; set; }
        public List<ThamSo> DsThamSo { get; private set; }

        public CauHinhThuTuc()
        {
            DsThamSo = new List<ThamSo>();
        }
    }

    public static class CauHinhThuTucs
    {
        private const string NguonHang =
            "SELECT MaHang AS Ma, MaHang + N' - ' + TenHang AS Ten FROM DMHangHoa ORDER BY MaHang";

        public static CauHinhThuTuc TimSanPham()
        {
            CauHinhThuTuc c = new CauHinhThuTuc();
            c.Proc = "sp_TimKiemSanPham";
            c.TenFile = "TimKiemSanPham";
            c.DsThamSo.Add(new ThamSo
            {
                Ten = "@MaChatLieu", Nhan = "Chất liệu", Kieu = KieuThamSo.ComboDb, ChoPhepTatCa = true,
                Nguon = "SELECT MaChatLieu AS Ma, TenChatLieu AS Ten FROM ChatLieu ORDER BY MaChatLieu"
            });
            c.DsThamSo.Add(new ThamSo
            {
                Ten = "@MaNuocSX", Nhan = "Nước sản xuất", Kieu = KieuThamSo.ComboDb, ChoPhepTatCa = true,
                Nguon = "SELECT MaNuocSX AS Ma, TenNuocSX AS Ten FROM NuocSanXuat ORDER BY MaNuocSX"
            });
            c.DsThamSo.Add(new ThamSo { Ten = "@ThoiGianBaoHanh", Nhan = "Bảo hành (vd: 24)", Kieu = KieuThamSo.Chu });
            return c;
        }

        public static CauHinhThuTuc TimHoaDonBan()
        {
            CauHinhThuTuc c = new CauHinhThuTuc();
            c.Proc = "sp_TimKiemHoaDonBan";
            c.TenFile = "TimKiemHoaDonBan";
            c.DsThamSo.Add(new ThamSo { Ten = "@MaHang", Nhan = "Mã hàng", Kieu = KieuThamSo.ComboDb, ChoPhepTatCa = true, Nguon = NguonHang });
            c.DsThamSo.Add(new ThamSo { Ten = "@NgayBan", Nhan = "Ngày bán (tick để lọc)", Kieu = KieuThamSo.Ngay });
            c.DsThamSo.Add(new ThamSo { Ten = "@TongTienTu", Nhan = "Tổng tiền từ", Kieu = KieuThamSo.Tien });
            c.DsThamSo.Add(new ThamSo { Ten = "@TongTienDen", Nhan = "Tổng tiền đến", Kieu = KieuThamSo.Tien });
            return c;
        }

        public static CauHinhThuTuc Top3SanPhamTheoQuy()
        {
            CauHinhThuTuc c = new CauHinhThuTuc();
            c.Proc = "sp_BaoCaoTop3SanPhamTheoQuy";
            c.TenFile = "Top3SanPhamTheoQuy";
            c.DsThamSo.Add(new ThamSo { Ten = "@Nam", Nhan = "Năm", Kieu = KieuThamSo.Nam });
            c.DsThamSo.Add(new ThamSo { Ten = "@Quy", Nhan = "Quý", Kieu = KieuThamSo.Quy });
            return c;
        }

        public static CauHinhThuTuc HoaDonNhapTheoNhanVien()
        {
            CauHinhThuTuc c = new CauHinhThuTuc();
            c.Proc = "sp_BaoCaoHoaDonNhapTheoNV";
            c.TenFile = "HoaDonNhapTheoNhanVien";
            c.DsThamSo.Add(new ThamSo
            {
                Ten = "@MaNV", Nhan = "Nhân viên nhập", Kieu = KieuThamSo.ComboDb, ChoPhepTatCa = false,
                Nguon = "SELECT MaNV AS Ma, MaNV + N' - ' + TenNV AS Ten FROM NhanVien ORDER BY MaNV"
            });
            return c;
        }

        public static CauHinhThuTuc Top5HoaDonBanNhoNhat()
        {
            CauHinhThuTuc c = new CauHinhThuTuc();
            c.Proc = "sp_BaoCaoTop5HDBanNhoNhatTheoQuy";
            c.TenFile = "Top5HoaDonBanNhoNhat";
            c.DsThamSo.Add(new ThamSo { Ten = "@Nam", Nhan = "Năm", Kieu = KieuThamSo.Nam });
            c.DsThamSo.Add(new ThamSo { Ten = "@Quy", Nhan = "Quý", Kieu = KieuThamSo.Quy });
            return c;
        }

        public static CauHinhThuTuc KhachHangTheoThang()
        {
            CauHinhThuTuc c = new CauHinhThuTuc();
            c.Proc = "sp_BaoCaoKhachHangTheoThang";
            c.TenFile = "KhachHangTheoThang";
            c.DsThamSo.Add(new ThamSo { Ten = "@Nam", Nhan = "Năm", Kieu = KieuThamSo.Nam });
            c.DsThamSo.Add(new ThamSo { Ten = "@Thang", Nhan = "Tháng", Kieu = KieuThamSo.Thang });
            return c;
        }
    }
}

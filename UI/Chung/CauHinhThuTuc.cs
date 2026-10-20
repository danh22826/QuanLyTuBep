using System;
using System.Collections.Generic;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

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

        public Func<IList<LuaChon>> Nguon { get; set; }

        public bool ChoPhepTatCa { get; set; }
    }

    public class CauHinhThuTuc
    {
        public string TenFile { get; set; }
        public string[] CotLuoi { get; set; }
        public List<ThamSo> DsThamSo { get; private set; }

        public CauHinhThuTuc()
        {
            DsThamSo = new List<ThamSo>();
        }
    }

    public static class CauHinhThuTucs
    {
        public static CauHinhThuTuc TimSanPham(BaoCaoBll bll)
        {
            CauHinhThuTuc c = new CauHinhThuTuc
            {
                TenFile = "TimKiemSanPham",
                CotLuoi = new[] { "MaHang", "TenHang", "TenKichThuoc", "TenChatLieu", "TenMau", "TenNuocSX", "SoLuong", "GiaNhap", "DonGiaBan", "ThoiGianBaoHanh", "TrangThai", "GhiChu" }
            };
            c.DsThamSo.Add(new ThamSo { Ten = "MaChatLieu", Nhan = "Chất liệu", Kieu = KieuThamSo.ComboDb, ChoPhepTatCa = true, Nguon = bll.DsChatLieu });
            c.DsThamSo.Add(new ThamSo { Ten = "MaNuocSX", Nhan = "Nước sản xuất", Kieu = KieuThamSo.ComboDb, ChoPhepTatCa = true, Nguon = bll.DsNuocSanXuat });
            c.DsThamSo.Add(new ThamSo { Ten = "ThoiGianBaoHanh", Nhan = "Bảo hành (vd: 24)", Kieu = KieuThamSo.Chu });
            return c;
        }

        public static CauHinhThuTuc TimHoaDonBan(BaoCaoBll bll)
        {
            CauHinhThuTuc c = new CauHinhThuTuc { TenFile = "TimKiemHoaDonBan", CotLuoi = new[] { "SoHDB", "NgayBan", "TenNV", "TenKhach", "TongTien" } };
            c.DsThamSo.Add(new ThamSo { Ten = "MaHang", Nhan = "Mã hàng", Kieu = KieuThamSo.ComboDb, ChoPhepTatCa = true, Nguon = bll.DsHangHoa });
            c.DsThamSo.Add(new ThamSo { Ten = "NgayBan", Nhan = "Ngày bán (tick để lọc)", Kieu = KieuThamSo.Ngay });
            c.DsThamSo.Add(new ThamSo { Ten = "TongTienTu", Nhan = "Tổng tiền từ", Kieu = KieuThamSo.Tien });
            c.DsThamSo.Add(new ThamSo { Ten = "TongTienDen", Nhan = "Tổng tiền đến", Kieu = KieuThamSo.Tien });
            return c;
        }

        public static CauHinhThuTuc Top3SanPhamTheoQuy()
        {
            CauHinhThuTuc c = new CauHinhThuTuc { TenFile = "Top3SanPhamTheoQuy" };
            c.DsThamSo.Add(new ThamSo { Ten = "Nam", Nhan = "Năm", Kieu = KieuThamSo.Nam });
            c.DsThamSo.Add(new ThamSo { Ten = "Quy", Nhan = "Quý", Kieu = KieuThamSo.Quy });
            return c;
        }

        public static CauHinhThuTuc HoaDonNhapTheoNhanVien(BaoCaoBll bll)
        {
            CauHinhThuTuc c = new CauHinhThuTuc
            {
                TenFile = "HoaDonNhapTheoNhanVien",
                CotLuoi = new[] { "SoHDN", "NgayNhap", "MaNV", "TenNV", "TenNCC" }
            };
            c.DsThamSo.Add(new ThamSo { Ten = "MaNV", Nhan = "Nhân viên nhập", Kieu = KieuThamSo.ComboDb, ChoPhepTatCa = false, Nguon = bll.DsNhanVien });
            return c;
        }

        public static CauHinhThuTuc Top5HoaDonBanNhoNhat()
        {
            CauHinhThuTuc c = new CauHinhThuTuc { TenFile = "Top5HoaDonBanNhoNhat", CotLuoi = new[] { "SoHDB", "NgayBan", "TenNV", "TenKhach", "TongTien" } };
            c.DsThamSo.Add(new ThamSo { Ten = "Nam", Nhan = "Năm", Kieu = KieuThamSo.Nam });
            c.DsThamSo.Add(new ThamSo { Ten = "Quy", Nhan = "Quý", Kieu = KieuThamSo.Quy });
            return c;
        }

        public static CauHinhThuTuc KhachHangTheoThang()
        {
            CauHinhThuTuc c = new CauHinhThuTuc { TenFile = "KhachHangTheoThang" };
            c.DsThamSo.Add(new ThamSo { Ten = "Nam", Nhan = "Năm", Kieu = KieuThamSo.Nam });
            c.DsThamSo.Add(new ThamSo { Ten = "Thang", Nhan = "Tháng", Kieu = KieuThamSo.Thang });
            return c;
        }
    }
}

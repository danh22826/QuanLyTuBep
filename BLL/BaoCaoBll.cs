using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class BaoCaoBll
    {
        private readonly BaoCaoDal dal = new BaoCaoDal();
        private readonly HangHoaDal hangDal = new HangHoaDal();
        private readonly HoaDonBanDal banDal = new HoaDonBanDal();
        private readonly ChatLieuDal chatLieuDal = new ChatLieuDal();
        private readonly NuocSanXuatDal nuocDal = new NuocSanXuatDal();
        private readonly NhanVienDal nhanVienDal = new NhanVienDal();

        public DateTime NgayMacDinh()
        {
            return banDal.NgayGanNhat() ?? DateTime.Today;
        }

        // ---------- Dữ liệu cho các ô chọn ----------

        public IList<LuaChon> DsChatLieu() { PhienDangNhap.YeuCauHieuLuc(); return chatLieuDal.LayTatCa().Select(x => new LuaChon(x.MaChatLieu, x.TenChatLieu)).ToList(); }
        public IList<LuaChon> DsNuocSanXuat() { PhienDangNhap.YeuCauHieuLuc(); return nuocDal.LayTatCa().Select(x => new LuaChon(x.MaNuocSX, x.TenNuocSX)).ToList(); }
        public IList<LuaChon> DsNhanVien() { PhanQuyen.YeuCau(Quyen.BaoCaoDoanhThu); return nhanVienDal.LayTatCa().Select(x => new LuaChon(x.MaNV, x.TenHienThi)).ToList(); }
        public IList<LuaChon> DsHangHoa() { PhienDangNhap.YeuCauHieuLuc(); return hangDal.LayTatCa().Select(x => new LuaChon(x.MaHang, x.TenHienThi)).ToList(); }

        // ---------- Tìm kiếm / báo cáo ----------

        public List<HangHoa> TimSanPham(string maChatLieu, string maNuocSX, string baoHanh)
        {
            PhanQuyen.YeuCau(Quyen.TimSanPham);
            List<HangHoa> ds = hangDal.TimKiem(maChatLieu, maNuocSX, baoHanh);
            if (!PhanQuyen.CoQuyen(Quyen.XemGiaVon)) foreach (HangHoa h in ds) h.GiaNhap = 0;
            return ds;
        }

        public List<HoaDonBan> TimHoaDonBan(string maHang, DateTime? ngayBan, decimal? tongTienTu, decimal? tongTienDen)
        {
            PhanQuyen.YeuCau(Quyen.XemLichSuBan);
            if (tongTienTu.HasValue && tongTienDen.HasValue && tongTienTu > tongTienDen)
                throw new NghiepVuException("\"Tổng tiền từ\" không được lớn hơn \"Tổng tiền đến\".");
            return banDal.TimKiem(maHang, ngayBan, tongTienTu, tongTienDen);
        }

        public List<ThongKeSanPham> Top3SanPhamTheoQuy(int nam, int quy)
        {
            PhanQuyen.YeuCau(Quyen.BaoCaoDoanhThu);
            KiemTraQuy(nam, quy);
            return dal.Top3SanPhamTheoQuy(nam, quy);
        }

        public List<DongHoaDonNhapTheoNV> HoaDonNhapTheoNhanVien(string maNV)
        {
            PhanQuyen.YeuCau(Quyen.BaoCaoDoanhThu);
            if (string.IsNullOrEmpty(maNV)) throw new NghiepVuException("Vui lòng chọn nhân viên nhập.");
            return dal.HoaDonNhapTheoNhanVien(maNV);
        }

        public List<HoaDonBan> Top5HoaDonBanNhoNhat(int nam, int quy)
        {
            PhanQuyen.YeuCau(Quyen.BaoCaoDoanhThu);
            KiemTraQuy(nam, quy);
            return banDal.Top5NhoNhatTheoQuy(nam, quy);
        }

        public List<ThongKeKhachHang> KhachHangTheoThang(int nam, int thang)
        {
            PhanQuyen.YeuCau(Quyen.BaoCaoDoanhThu);
            if (thang < 1 || thang > 12) throw new NghiepVuException("Tháng phải từ 1 đến 12.");
            return dal.KhachHangTheoThang(nam, thang);
        }

        private static void KiemTraQuy(int nam, int quy)
        {
            if (quy < 1 || quy > 4) throw new NghiepVuException("Quý phải từ 1 đến 4.");
        }
    }
}

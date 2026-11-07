using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class TaiKhoanBll
    {
        private readonly TaiKhoanDal dal = new TaiKhoanDal();

        public IList<TaiKhoan> LayDanhSach(string tuKhoa)
        {
            PhanQuyen.YeuCau(Quyen.QuanLyTaiKhoan);
            return dal.LayDanhSach(tuKhoa).Select(AnHash).ToList();
        }

        public IList<LuaChon> NhanVienChuaCoTaiKhoan()
        {
            PhanQuyen.YeuCau(Quyen.QuanLyTaiKhoan);
            return dal.NhanVienChuaCoTaiKhoan();
        }

        public void TaoTaiKhoan(string maNV, string tenDangNhap, string matKhau, string vaiTro)
        {
            PhanQuyen.YeuCau(Quyen.QuanLyTaiKhoan);
            string ten = DangNhapBll.ChuanHoaTen(tenDangNhap);
            DangNhapBll.KiemTraTen(ten);
            DangNhapBll.KiemTraMatKhau(matKhau);
            KiemTraVaiTro(vaiTro);
            if (!new NhanVienDal().TonTai(maNV)) throw new NghiepVuException("Nhân viên được chọn không còn tồn tại.");
            try { dal.Them(maNV, ten, BamMatKhau.Tao(matKhau), vaiTro); }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                    throw new NghiepVuException("Nhân viên này đã có tài khoản hoặc tên đăng nhập đã được sử dụng.");
                throw;
            }
        }

        public void DoiMatKhau(string matKhauCu, string matKhauMoi)
        {
            ThongTinPhien p = PhienDangNhap.YeuCauHieuLuc();
            TaiKhoan tk = new TaiKhoanDal().KiemTraPhien(p.MaTaiKhoan);
            if (tk == null || !BamMatKhau.KiemTra(matKhauCu ?? "", tk.MatKhauHash))
                throw new NghiepVuException("Mật khẩu hiện tại không đúng.");
            if (string.Equals(matKhauCu, matKhauMoi, StringComparison.Ordinal))
                throw new NghiepVuException("Mật khẩu mới phải khác mật khẩu hiện tại.");
            DangNhapBll.KiemTraMatKhau(matKhauMoi);
            dal.CapNhatMatKhau(p.MaTaiKhoan, BamMatKhau.Tao(matKhauMoi));
        }

        public void DatLaiMatKhau(string maTaiKhoan, string matKhauMoi)
        {
            PhanQuyen.YeuCau(Quyen.QuanLyTaiKhoan);
            DangNhapBll.KiemTraMatKhau(matKhauMoi);
            if (dal.LayTheoMa(maTaiKhoan) == null) throw new NghiepVuException("Tài khoản không còn tồn tại.");
            dal.CapNhatMatKhau(maTaiKhoan, BamMatKhau.Tao(matKhauMoi));
        }

        public void DatTrangThai(string maTaiKhoan, bool hoatDong)
        {
            PhanQuyen.YeuCau(Quyen.QuanLyTaiKhoan);
            ThongTinPhien p = PhienDangNhap.HienTai;
            if (p != null && string.Equals(p.MaTaiKhoan, maTaiKhoan, StringComparison.Ordinal) && !hoatDong)
                throw new NghiepVuException("Không thể tự khóa tài khoản đang đăng nhập.");
            dal.CapNhatTrangThai(maTaiKhoan, hoatDong);
        }

        public void DatVaiTro(string maTaiKhoan, string vaiTro)
        {
            PhanQuyen.YeuCau(Quyen.QuanLyTaiKhoan);
            KiemTraVaiTro(vaiTro);
            ThongTinPhien p = PhienDangNhap.HienTai;
            if (p != null && string.Equals(p.MaTaiKhoan, maTaiKhoan, StringComparison.Ordinal) && !string.Equals(vaiTro, "ADMIN", StringComparison.OrdinalIgnoreCase))
                throw new NghiepVuException("Không thể tự hạ quyền tài khoản đang đăng nhập.");
            dal.CapNhatVaiTro(maTaiKhoan, vaiTro);
        }

        private static void KiemTraVaiTro(string vaiTro)
        {
            if (!string.Equals(vaiTro, "ADMIN", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(vaiTro, "STAFF", StringComparison.OrdinalIgnoreCase))
                throw new NghiepVuException("Vai trò chỉ nhận ADMIN hoặc STAFF.");
        }

        private static TaiKhoan AnHash(TaiKhoan t)
        {
            t.MatKhauHash = null;
            return t;
        }
    }
}

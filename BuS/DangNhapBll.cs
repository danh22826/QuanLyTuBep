using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    internal static class BamMatKhau
    {
        private const int SoVong = 210000;
        private const int DoDaiSalt = 16;
        private const int DoDaiHash = 32;

        public static string Tao(string matKhau)
        {
            byte[] salt = new byte[DoDaiSalt];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(matKhau, salt, SoVong))
            {
                byte[] hash = pbkdf2.GetBytes(DoDaiHash);
                return "pbkdf2-sha1$" + SoVong.ToString(CultureInfo.InvariantCulture) + "$" +
                       Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(hash);
            }
        }

        public static bool KiemTra(string matKhau, string duLieu)
        {
            if (string.IsNullOrEmpty(duLieu)) return false;
            string[] phan = duLieu.Split('$');
            if (phan.Length != 4 || phan[0] != "pbkdf2-sha1") return false;
            int vong;
            byte[] salt, mongDoi;
            try
            {
                if (!int.TryParse(phan[1], NumberStyles.None, CultureInfo.InvariantCulture, out vong) || vong < 100000 || vong > 1000000) return false;
                salt = Convert.FromBase64String(phan[2]);
                mongDoi = Convert.FromBase64String(phan[3]);
            }
            catch (FormatException) { return false; }
            if (salt.Length < 16 || mongDoi.Length < 32) return false;
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(matKhau, salt, vong))
                return SoSanhHangSo(mongDoi, pbkdf2.GetBytes(mongDoi.Length));
        }

        private static bool SoSanhHangSo(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++) diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }

    public class DangNhapBll
    {
        private readonly TaiKhoanDal dal = new TaiKhoanDal();

        public ThongTinPhien DangNhap(string tenDangNhap, string matKhau)
        {
            string ten = (tenDangNhap ?? "").Trim().ToLowerInvariant();
            TaiKhoan tk = dal.TimTheoTenDangNhap(ten);
            if (tk == null) throw new NghiepVuException("Tên đăng nhập hoặc mật khẩu không đúng.");
            if (!tk.TrangThai) throw new NghiepVuException("Tài khoản này đang bị khóa. Hãy liên hệ ADMIN.");
            if (string.IsNullOrEmpty(matKhau) || !BamMatKhau.KiemTra(matKhau, tk.MatKhauHash))
                throw new NghiepVuException("Tên đăng nhập hoặc mật khẩu không đúng.");
            dal.CapNhatLanDangNhap(tk.MaTaiKhoan);
            PhienDangNhap.ThietLap(tk);
            return PhienDangNhap.HienTai;
        }

        internal static string ChuanHoaTen(string ten) { return (ten ?? "").Trim().ToLowerInvariant(); }

        internal static void KiemTraTen(string ten)
        {
            if (ten == null || !Regex.IsMatch(ten, "^[a-z0-9._-]{4,50}$"))
                throw new NghiepVuException("Tên đăng nhập gồm 4–50 ký tự a-z, số, dấu chấm, gạch dưới hoặc gạch nối.");
        }

        internal static void KiemTraMatKhau(string matKhau)
        {
            if (matKhau == null || matKhau.Length < 10 || matKhau.Length > 256)
                throw new NghiepVuException("Mật khẩu phải dài từ 10 đến 256 ký tự.");
            int nhom = 0;
            if (Regex.IsMatch(matKhau, "[a-z]")) nhom++;
            if (Regex.IsMatch(matKhau, "[A-Z]")) nhom++;
            if (Regex.IsMatch(matKhau, "[0-9]")) nhom++;
            if (Regex.IsMatch(matKhau, "[^a-zA-Z0-9]")) nhom++;
            if (nhom < 3) throw new NghiepVuException("Mật khẩu cần có ít nhất 3 nhóm: chữ thường, chữ hoa, số và ký tự đặc biệt.");
        }
    }
}

using System.Text.RegularExpressions;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public static class KiemTraDuLieu
    {
        private static readonly Regex MauMa = new Regex("^[A-Za-z0-9_]+$");
        private static readonly Regex MauDienThoai = new Regex("^[0-9]{9,11}$");

        public static void Ma(string giaTri, string nhan, string truong)
        {
            Chu(giaTri, nhan, 20, true, truong);
            if (!MauMa.IsMatch(giaTri))
                throw new NghiepVuException("Mã chỉ gồm chữ không dấu, số và dấu gạch dưới (không có khoảng trắng).", truong);
        }

        public static void Chu(string giaTri, string nhan, int doDaiToiDa, bool batBuoc, string truong)
        {
            if (string.IsNullOrEmpty(giaTri))
            {
                if (batBuoc) throw new NghiepVuException(nhan + " không được để trống.", truong);
                return;
            }
            if (giaTri.Length > doDaiToiDa)
                throw new NghiepVuException(nhan + " tối đa " + doDaiToiDa + " ký tự.", truong);
        }

        public static void DienThoai(string giaTri, string truong)
        {
            Chu(giaTri, "Điện thoại", 20, false, truong);
            if (!string.IsNullOrEmpty(giaTri) && !MauDienThoai.IsMatch(giaTri))
                throw new NghiepVuException("Số điện thoại gồm 9 đến 11 chữ số.", truong);
        }

        public static void Chon(string giaTri, string nhan, string truong)
        {
            if (string.IsNullOrEmpty(giaTri))
                throw new NghiepVuException("Vui lòng chọn " + nhan.ToLower() + ".", truong);
        }
    }
}

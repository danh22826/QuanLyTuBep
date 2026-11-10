using System;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public static class LoiHeThong
    {
        public static bool LaLoiNghiepVu(Exception ex)
        {
            return ex is NghiepVuException;
        }

        public static string ThongDiep(Exception ex)
        {
            if (ex is NghiepVuException) return ex.Message;
            return LoiCsdl.MoTa(ex) ?? "Đã xảy ra lỗi: " + ex.Message;
        }
    }
}

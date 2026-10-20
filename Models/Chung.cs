using System;

namespace QuanLyBanTuBep.Models
{
    public enum LoaiHoaDon
    {
        Ban,
        Nhap
    }

    public class LuaChon
    {
        public string Ma { get; set; }
        public string Ten { get; set; }

        public LuaChon()
        {
        }

        public LuaChon(string ma, string ten)
        {
            Ma = ma;
            Ten = ten;
        }
    }

    public class NghiepVuException : Exception
    {
        public string Truong { get; private set; }

        public NghiepVuException(string thongBao) : base(thongBao)
        {
        }

        public NghiepVuException(string thongBao, string truong) : base(thongBao)
        {
            Truong = truong;
        }
    }
}

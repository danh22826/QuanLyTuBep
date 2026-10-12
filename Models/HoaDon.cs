using System;
using System.Collections.Generic;

namespace QuanLyBanTuBep.Models
{
    public enum LoaiHoaDon
    {
        Ban,
        Nhap
    }

    public class DongHoaDon
    {
        public string MaHang { get; set; }
        public int SoLuong { get; set; }

        public decimal DonGia { get; set; }

        public double GiamGia { get; set; }
    }

    public class HoaDon
    {
        public LoaiHoaDon Loai { get; set; }
        public string So { get; set; }
        public DateTime Ngay { get; set; }
        public string MaNV { get; set; }

        public string MaDoiTac { get; set; }

        public List<DongHoaDon> Dong { get; private set; }

        public HoaDon()
        {
            Dong = new List<DongHoaDon>();
        }
    }

    public class NghiepVuException : Exception
    {
        public NghiepVuException(string thongBao) : base(thongBao)
        {
        }
    }
}

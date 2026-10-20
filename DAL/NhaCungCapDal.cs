using System.Collections.Generic;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public class NhaCungCapDal : BangDal<NhaCungCap>
    {
        protected override string Bang { get { return "NhaCungCap"; } }
        protected override string CotKhoa { get { return "MaNCC"; } }
        protected override string[] CotGhi { get { return new[] { "MaNCC", "TenNCC", "DienThoai", "DiaChi" }; } }

        public int DemHoaDonNhap(string ma)
        {
            return Dem("HoaDonNhap", "MaNCC", ma);
        }
    }
}

using System.Collections.Generic;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public class CongViecDal : BangDal<CongViec>
    {
        protected override string Bang { get { return "CongViec"; } }
        protected override string CotKhoa { get { return "MaCV"; } }
        protected override string[] CotGhi { get { return new[] { "MaCV", "TenCV", "MucLuong" }; } }

        public int DemNhanVien(string ma)
        {
            return Dem("NhanVien", "MaCV", ma);
        }
    }
}

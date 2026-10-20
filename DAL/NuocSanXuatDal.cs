using System.Collections.Generic;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public class NuocSanXuatDal : BangDal<NuocSanXuat>
    {
        protected override string Bang { get { return "NuocSanXuat"; } }
        protected override string CotKhoa { get { return "MaNuocSX"; } }
        protected override string[] CotGhi { get { return new[] { "MaNuocSX", "TenNuocSX" }; } }

        public int DemHangHoa(string ma)
        {
            return Dem("DMHangHoa", "MaNuocSX", ma);
        }
    }
}

using System.Collections.Generic;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public class MauSacDal : BangDal<MauSac>
    {
        protected override string Bang { get { return "MauSac"; } }
        protected override string CotKhoa { get { return "MaMau"; } }
        protected override string[] CotGhi { get { return new[] { "MaMau", "TenMau" }; } }

        public int DemHangHoa(string ma)
        {
            return Dem("DMHangHoa", "MaMau", ma);
        }
    }
}

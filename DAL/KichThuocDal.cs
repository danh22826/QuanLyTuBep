using System.Collections.Generic;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public class KichThuocDal : BangDal<KichThuoc>
    {
        protected override string Bang { get { return "KichThuoc"; } }
        protected override string CotKhoa { get { return "MaKichThuoc"; } }
        protected override string[] CotGhi { get { return new[] { "MaKichThuoc", "TenKichThuoc" }; } }

        public int DemHangHoa(string ma)
        {
            return Dem("DMHangHoa", "MaKichThuoc", ma);
        }
    }
}

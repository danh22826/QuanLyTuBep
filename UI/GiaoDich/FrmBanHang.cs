using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public class FrmBanHang : FrmHoaDon
    {
        public FrmBanHang() : base(LoaiHoaDon.Ban)
        {
            Text = "Bán hàng";
        }
    }
}

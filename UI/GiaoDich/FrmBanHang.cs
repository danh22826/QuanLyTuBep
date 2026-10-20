using QuanLyBanTuBep.BLL;

namespace QuanLyBanTuBep.UI
{
    public class FrmBanHang : FrmHoaDon
    {
        public FrmBanHang() : base(new BanHangBll())
        {
            Text = "Bán hàng";
        }
    }
}

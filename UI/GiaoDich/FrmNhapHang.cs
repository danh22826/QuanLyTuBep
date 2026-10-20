using QuanLyBanTuBep.BLL;

namespace QuanLyBanTuBep.UI
{
    public class FrmNhapHang : FrmHoaDon
    {
        public FrmNhapHang() : base(new NhapHangBll())
        {
            Text = "Nhập hàng";
        }
    }
}

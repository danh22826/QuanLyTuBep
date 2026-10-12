using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public class FrmNhapHang : FrmHoaDon
    {
        public FrmNhapHang() : base(LoaiHoaDon.Nhap)
        {
            Text = "Nhập hàng";
        }
    }
}

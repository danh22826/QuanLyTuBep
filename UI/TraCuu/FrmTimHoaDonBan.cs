using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmTimHoaDonBan : Form
    {
        private readonly ThuTucView<HoaDonBan> dieuKhien;

        public FrmTimHoaDonBan()
        {
            InitializeComponent();
            BaoCaoBll bll = new BaoCaoBll();
            dieuKhien = new ThuTucView<HoaDonBan>(this, CauHinhThuTucs.TimHoaDonBan(bll), bll, p => bll.TimHoaDonBan(p.Chu("MaHang"), p.Ngay("NgayBan"), p.Tien("TongTienTu"), p.Tien("TongTienDen")));
        }
    }
}

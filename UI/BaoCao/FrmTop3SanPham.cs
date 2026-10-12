using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmTop3SanPham : Form
    {
        private readonly ThuTucController dieuKhien;

        public FrmTop3SanPham()
        {
            InitializeComponent();
            dieuKhien = new ThuTucController(this, CauHinhThuTucs.Top3SanPhamTheoQuy());
        }
    }
}

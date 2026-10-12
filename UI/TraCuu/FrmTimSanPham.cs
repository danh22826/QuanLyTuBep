using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmTimSanPham : Form
    {
        private readonly ThuTucController dieuKhien;

        public FrmTimSanPham()
        {
            InitializeComponent();
            dieuKhien = new ThuTucController(this, CauHinhThuTucs.TimSanPham());
        }
    }
}

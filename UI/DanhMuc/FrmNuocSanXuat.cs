using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmNuocSanXuat : Form
    {
        private readonly BangController dieuKhien;

        public FrmNuocSanXuat()
        {
            InitializeComponent();
            dieuKhien = new BangController(this, CauHinh.NuocSanXuat());
        }
    }
}

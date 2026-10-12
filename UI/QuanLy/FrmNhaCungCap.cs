using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmNhaCungCap : Form
    {
        private readonly BangController dieuKhien;

        public FrmNhaCungCap()
        {
            InitializeComponent();
            dieuKhien = new BangController(this, CauHinh.NhaCungCap());
        }
    }
}

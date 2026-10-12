using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmMauSac : Form
    {
        private readonly BangController dieuKhien;

        public FrmMauSac()
        {
            InitializeComponent();
            dieuKhien = new BangController(this, CauHinh.MauSac());
        }
    }
}

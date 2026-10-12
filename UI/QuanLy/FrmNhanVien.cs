using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmNhanVien : Form
    {
        private readonly BangController dieuKhien;

        public FrmNhanVien()
        {
            InitializeComponent();
            dieuKhien = new BangController(this, CauHinh.NhanVien());
        }
    }
}

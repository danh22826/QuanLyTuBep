using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmKhachHang : Form
    {
        private readonly BangController dieuKhien;

        public FrmKhachHang()
        {
            InitializeComponent();
            dieuKhien = new BangController(this, CauHinh.KhachHang());
        }
    }
}

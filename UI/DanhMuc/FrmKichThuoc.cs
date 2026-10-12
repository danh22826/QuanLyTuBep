using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmKichThuoc : Form
    {
        private readonly BangController dieuKhien;

        public FrmKichThuoc()
        {
            InitializeComponent();
            dieuKhien = new BangController(this, CauHinh.KichThuoc());
        }
    }
}

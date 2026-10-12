using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmCongViec : Form
    {
        private readonly BangController dieuKhien;

        public FrmCongViec()
        {
            InitializeComponent();
            dieuKhien = new BangController(this, CauHinh.CongViec());
        }
    }
}

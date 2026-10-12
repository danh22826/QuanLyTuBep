using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmHangHoa : Form
    {
        private readonly BangController dieuKhien;

        public FrmHangHoa()
        {
            InitializeComponent();
            dieuKhien = new BangController(this, CauHinh.HangHoa());
        }
    }
}

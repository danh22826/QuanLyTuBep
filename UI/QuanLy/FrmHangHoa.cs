using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmHangHoa : Form
    {
        private readonly BangView<HangHoa> dieuKhien;

        public FrmHangHoa()
        {
            InitializeComponent();
            dieuKhien = new BangView<HangHoa>(this, new HangHoaBll(), CauHinh.HangHoa());
        }
    }
}

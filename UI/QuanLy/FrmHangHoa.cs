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
            HangHoaBll bll = new HangHoaBll();
            dieuKhien = new BangView<HangHoa>(this, bll, CauHinh.HangHoa(),
                bll.TimKiemQuanLy, bll.DemNgungKinhDoanh);
        }
    }
}

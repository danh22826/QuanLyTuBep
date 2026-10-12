using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmTop5HoaDonBan : Form
    {
        private readonly ThuTucController dieuKhien;

        public FrmTop5HoaDonBan()
        {
            InitializeComponent();
            dieuKhien = new ThuTucController(this, CauHinhThuTucs.Top5HoaDonBanNhoNhat());
        }
    }
}

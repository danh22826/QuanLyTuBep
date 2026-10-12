using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmKhachHangTheoThang : Form
    {
        private readonly ThuTucController dieuKhien;

        public FrmKhachHangTheoThang()
        {
            InitializeComponent();
            dieuKhien = new ThuTucController(this, CauHinhThuTucs.KhachHangTheoThang());
        }
    }
}

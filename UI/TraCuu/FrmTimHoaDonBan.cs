using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmTimHoaDonBan : Form
    {
        private readonly ThuTucController dieuKhien;

        public FrmTimHoaDonBan()
        {
            InitializeComponent();
            dieuKhien = new ThuTucController(this, CauHinhThuTucs.TimHoaDonBan());
        }
    }
}

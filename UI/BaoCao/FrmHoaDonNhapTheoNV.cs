using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmHoaDonNhapTheoNV : Form
    {
        private readonly ThuTucController dieuKhien;

        public FrmHoaDonNhapTheoNV()
        {
            InitializeComponent();
            dieuKhien = new ThuTucController(this, CauHinhThuTucs.HoaDonNhapTheoNhanVien());
        }
    }
}

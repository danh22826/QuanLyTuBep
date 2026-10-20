using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmKhachHangTheoThang : Form
    {
        private readonly ThuTucView<ThongKeKhachHang> dieuKhien;

        public FrmKhachHangTheoThang()
        {
            InitializeComponent();
            BaoCaoBll bll = new BaoCaoBll();
            dieuKhien = new ThuTucView<ThongKeKhachHang>(this, CauHinhThuTucs.KhachHangTheoThang(), bll, p => bll.KhachHangTheoThang(p.Int("Nam"), p.Int("Thang")));
        }
    }
}

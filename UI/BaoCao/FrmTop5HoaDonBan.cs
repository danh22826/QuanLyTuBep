using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmTop5HoaDonBan : Form
    {
        private readonly ThuTucView<HoaDonBan> dieuKhien;

        public FrmTop5HoaDonBan()
        {
            InitializeComponent();
            BaoCaoBll bll = new BaoCaoBll();
            dieuKhien = new ThuTucView<HoaDonBan>(this, CauHinhThuTucs.Top5HoaDonBanNhoNhat(), bll, p => bll.Top5HoaDonBanNhoNhat(p.Int("Nam"), p.Int("Quy")));
        }
    }
}

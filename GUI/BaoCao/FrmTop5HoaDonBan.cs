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
            PhanQuyen.YeuCau(Quyen.BaoCaoDoanhThu);
            InitializeComponent();
            BaoCaoBll bll = new BaoCaoBll();
            dieuKhien = new ThuTucView<HoaDonBan>(this, CauHinhThuTucs.Top5HoaDonBanNhoNhat(), bll, p => bll.Top5HoaDonBanNhoNhat(p.Int("Nam"), p.Int("Quy")));
            dieuKhien.ThemNutIn(DuLieuBaoCao.Top5HoaDonBan, p => $"Quý {p.Int("Quy")} năm {p.Int("Nam")}");
        }
    }
}

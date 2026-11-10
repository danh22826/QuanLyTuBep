using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmTop3SanPham : Form
    {
        private readonly ThuTucView<ThongKeSanPham> dieuKhien;

        public FrmTop3SanPham()
        {
            PhanQuyen.YeuCau(Quyen.BaoCaoDoanhThu);
            InitializeComponent();
            BaoCaoBll bll = new BaoCaoBll();
            dieuKhien = new ThuTucView<ThongKeSanPham>(this, CauHinhThuTucs.Top3SanPhamTheoQuy(), bll, p => bll.Top3SanPhamTheoQuy(p.Int("Nam"), p.Int("Quy")));
            dieuKhien.ThemNutIn(DuLieuBaoCao.Top3SanPham, p => $"Quý {p.Int("Quy")} năm {p.Int("Nam")}");
        }
    }
}

using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmHoaDonNhapTheoNV : Form
    {
        private readonly ThuTucView<DongHoaDonNhapTheoNV> dieuKhien;

        public FrmHoaDonNhapTheoNV()
        {
            PhanQuyen.YeuCau(Quyen.BaoCaoDoanhThu);
            InitializeComponent();
            BaoCaoBll bll = new BaoCaoBll();
            dieuKhien = new ThuTucView<DongHoaDonNhapTheoNV>(this, CauHinhThuTucs.HoaDonNhapTheoNhanVien(bll), bll, p => bll.HoaDonNhapTheoNhanVien(p.Chu("MaNV")));
            dieuKhien.ThemNutIn(DuLieuBaoCao.HoaDonNhapTheoNV, p => "Nhân viên: " + cboMaNV.Text);
        }
    }
}

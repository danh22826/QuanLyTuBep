using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmTimSanPham : Form
    {
        private readonly ThuTucView<HangHoa> dieuKhien;

        public FrmTimSanPham()
        {
            InitializeComponent();
            BaoCaoBll bll = new BaoCaoBll();
            dieuKhien = new ThuTucView<HangHoa>(this, CauHinhThuTucs.TimSanPham(bll), bll, p => bll.TimSanPham(p.Chu("MaChatLieu"), p.Chu("MaNuocSX"), p.Chu("ThoiGianBaoHanh")));
        }
    }
}

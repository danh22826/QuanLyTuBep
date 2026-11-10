using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmNhanVien : Form
    {
        private readonly BangView<NhanVien> dieuKhien;

        public FrmNhanVien()
        {
            PhanQuyen.YeuCau(Quyen.QuanLyNhanVien);
            InitializeComponent();
            dieuKhien = new BangView<NhanVien>(this, new NhanVienBll(), CauHinh.NhanVien());
        }
    }
}

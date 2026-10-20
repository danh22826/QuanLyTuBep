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
            InitializeComponent();
            dieuKhien = new BangView<NhanVien>(this, new NhanVienBll(), CauHinh.NhanVien());
        }
    }
}

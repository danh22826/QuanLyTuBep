using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmNuocSanXuat : Form
    {
        private readonly BangView<NuocSanXuat> dieuKhien;

        public FrmNuocSanXuat()
        {
            InitializeComponent();
            dieuKhien = new BangView<NuocSanXuat>(this, new NuocSanXuatBll(), CauHinh.NuocSanXuat());
        }
    }
}

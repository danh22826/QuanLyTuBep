using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmMauSac : Form
    {
        private readonly BangView<MauSac> dieuKhien;

        public FrmMauSac()
        {
            InitializeComponent();
            dieuKhien = new BangView<MauSac>(this, new MauSacBll(), CauHinh.MauSac());
        }
    }
}

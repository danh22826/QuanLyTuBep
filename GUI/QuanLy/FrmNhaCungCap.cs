using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmNhaCungCap : Form
    {
        private readonly BangView<NhaCungCap> dieuKhien;

        public FrmNhaCungCap()
        {
            PhanQuyen.YeuCau(Quyen.QuanLyNhaCungCap);
            InitializeComponent();
            dieuKhien = new BangView<NhaCungCap>(this, new NhaCungCapBll(), CauHinh.NhaCungCap());
        }
    }
}

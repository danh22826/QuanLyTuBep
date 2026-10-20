using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmKhachHang : Form
    {
        private readonly BangView<KhachHang> dieuKhien;

        public FrmKhachHang()
        {
            InitializeComponent();
            dieuKhien = new BangView<KhachHang>(this, new KhachHangBll(), CauHinh.KhachHang());
        }
    }
}

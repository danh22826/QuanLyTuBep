using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmCongViec : Form
    {
        private readonly BangView<CongViec> dieuKhien;

        public FrmCongViec()
        {
            PhanQuyen.YeuCau(Quyen.QuanLyDanhMuc);
            InitializeComponent();
            dieuKhien = new BangView<CongViec>(this, new CongViecBll(), CauHinh.CongViec());
        }
    }
}

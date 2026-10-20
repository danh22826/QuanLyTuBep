using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmKichThuoc : Form
    {
        private readonly BangView<KichThuoc> dieuKhien;

        public FrmKichThuoc()
        {
            InitializeComponent();
            dieuKhien = new BangView<KichThuoc>(this, new KichThuocBll(), CauHinh.KichThuoc());
        }
    }
}

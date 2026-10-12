using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmChatLieu : Form
    {
        private readonly BangController dieuKhien;

        public FrmChatLieu()
        {
            InitializeComponent();
            dieuKhien = new BangController(this, CauHinh.ChatLieu());
        }
    }
}

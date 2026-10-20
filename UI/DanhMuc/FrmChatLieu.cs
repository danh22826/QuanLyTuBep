using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmChatLieu : Form
    {
        private readonly BangView<ChatLieu> dieuKhien;

        public FrmChatLieu()
        {
            InitializeComponent();
            dieuKhien = new BangView<ChatLieu>(this, new ChatLieuBll(), CauHinh.ChatLieu());
        }
    }
}

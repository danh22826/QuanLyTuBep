using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;

namespace QuanLyBanTuBep.UI
{
    public sealed class FrmDoiMatKhau : Form
    {
        private readonly TextBox cu = new TextBox { UseSystemPasswordChar = true, MaxLength = 256 };
        private readonly TextBox moi = new TextBox { UseSystemPasswordChar = true, MaxLength = 256 };
        private readonly TextBox xacNhan = new TextBox { UseSystemPasswordChar = true, MaxLength = 256 };
        public FrmDoiMatKhau()
        {
            PhanQuyen.YeuCau(Quyen.DoiMatKhau);
            Text = "Đổi mật khẩu"; StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog; MinimizeBox = false; MaximizeBox = false;
            ClientSize = new Size(430, 300); BackColor = Color.White;
            ThemTruong("Mật khẩu hiện tại", cu, 28);
            ThemTruong("Mật khẩu mới", moi, 92);
            ThemTruong("Xác nhận mật khẩu mới", xacNhan, 156);
            Button btn = new Button { Text = "Cập nhật mật khẩu", BackColor = Color.FromArgb(0, 135, 137), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btn.SetBounds(28, 236, 190, 38); btn.Click += delegate { Luu(); }; Controls.Add(btn);
            Button huy = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel }; huy.SetBounds(230, 236, 90, 38); Controls.Add(huy);
            CancelButton = huy; AcceptButton = btn;
        }
        private void ThemTruong(string t, TextBox o, int y) { Controls.Add(new Label { Text = t, AutoSize = true, Location = new Point(28, y) }); o.SetBounds(28, y + 22, 370, 30); Controls.Add(o); }
        private void Luu()
        {
            try
            {
                if (moi.Text != xacNhan.Text) throw new Models.NghiepVuException("Mật khẩu xác nhận không khớp.");
                new TaiKhoanBll().DoiMatKhau(cu.Text, moi.Text);
                MessageBox.Show(this, "Đã đổi mật khẩu.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, "Không đổi được mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
    }
}

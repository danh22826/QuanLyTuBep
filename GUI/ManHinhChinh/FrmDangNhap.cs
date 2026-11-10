using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public sealed class FrmDangNhap : Form
    {
        private readonly DangNhapBll bll = new DangNhapBll();
        private readonly TextBox txtTen = new TextBox();
        private readonly TextBox txtMatKhau = new TextBox();
        private readonly CheckBox chkHien = new CheckBox();
        private readonly Label lblThongBao = new Label();

        public FrmDangNhap()
        {
            Text = "Đăng nhập - Quản lý bán tủ bếp";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false; MaximizeBox = false; ShowInTaskbar = true;
            ClientSize = new Size(420, 330); BackColor = Color.White;

            Label title = new Label { Text = "ĐĂNG NHẬP", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.FromArgb(0, 135, 137), AutoSize = true, Location = new Point(130, 22) };
            Label lblTen = new Label { Text = "Tên đăng nhập", AutoSize = true, Location = new Point(34, 80) };
            txtTen.SetBounds(34, 103, 352, 31); txtTen.MaxLength = 50;
            Label lblMatKhau = new Label { Text = "Mật khẩu", AutoSize = true, Location = new Point(34, 148) };
            txtMatKhau.SetBounds(34, 171, 352, 31); txtMatKhau.UseSystemPasswordChar = true; txtMatKhau.MaxLength = 256;
            chkHien.Text = "Hiện mật khẩu"; chkHien.AutoSize = true; chkHien.Location = new Point(34, 208);
            chkHien.CheckedChanged += delegate { txtMatKhau.UseSystemPasswordChar = !chkHien.Checked; };
            Button btnDangNhap = new Button { Text = "Đăng nhập", BackColor = Color.FromArgb(0, 135, 137), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnDangNhap.SetBounds(34, 241, 170, 38); btnDangNhap.Click += delegate { ThuDangNhap(); };
            lblThongBao.SetBounds(34, 286, 352, 34); lblThongBao.ForeColor = Color.Firebrick; lblThongBao.AutoSize = false;
            Controls.AddRange(new Control[] { title, lblTen, txtTen, lblMatKhau, txtMatKhau, chkHien, btnDangNhap, lblThongBao });
            AcceptButton = btnDangNhap;
            Shown += delegate { txtTen.Focus(); };
        }

        private void ThuDangNhap()
        {
            lblThongBao.Text = "";
            try { bll.DangNhap(txtTen.Text, txtMatKhau.Text); DialogResult = DialogResult.OK; Close(); }
            catch (NghiepVuException ex) { lblThongBao.Text = ex.Message; txtMatKhau.SelectAll(); txtMatKhau.Focus(); }
            catch (Exception ex) { lblThongBao.Text = "Không đăng nhập được: " + ex.Message; }
        }
    }
}

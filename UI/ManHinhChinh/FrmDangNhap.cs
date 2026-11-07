using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.DAL;
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
        private readonly Button btnKhoiTao = new Button();

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
            btnKhoiTao.Text = "Khởi tạo ADMIN đầu tiên"; btnKhoiTao.FlatStyle = FlatStyle.Flat;
            btnKhoiTao.Visible = false;
            btnKhoiTao.SetBounds(214, 241, 172, 38); btnKhoiTao.Click += delegate { MoKhoiTao(); };
            lblThongBao.SetBounds(34, 286, 352, 34); lblThongBao.ForeColor = Color.Firebrick; lblThongBao.AutoSize = false;
            Controls.AddRange(new Control[] { title, lblTen, txtTen, lblMatKhau, txtMatKhau, chkHien, btnDangNhap, btnKhoiTao, lblThongBao });
            AcceptButton = btnDangNhap;
            Shown += delegate { KiemTraKhoiTao(); txtTen.Focus(); };
        }

        private void KiemTraKhoiTao()
        {
            try { btnKhoiTao.Visible = bll.CanKhoiTaoAdminDauTien(); }
            catch (Exception ex) { btnKhoiTao.Visible = false; lblThongBao.Text = "Chưa thể kiểm tra tài khoản. Cần cài đặt SQL\\TaoBangTaiKhoan.sql.\n" + ex.Message; }
        }

        private void ThuDangNhap()
        {
            lblThongBao.Text = "";
            try { bll.DangNhap(txtTen.Text, txtMatKhau.Text); DialogResult = DialogResult.OK; Close(); }
            catch (NghiepVuException ex) { lblThongBao.Text = ex.Message; txtMatKhau.SelectAll(); txtMatKhau.Focus(); }
            catch (Exception ex) { lblThongBao.Text = "Không đăng nhập được: " + ex.Message; }
        }

        private void MoKhoiTao()
        {
            try
            {
                if (!bll.CanKhoiTaoAdminDauTien()) { btnKhoiTao.Visible = false; lblThongBao.Text = "Đã có tài khoản; hãy đăng nhập."; return; }
                using (FrmKhoiTaoAdmin f = new FrmKhoiTaoAdmin(bll))
                {
                    if (f.ShowDialog(this) == DialogResult.OK) { btnKhoiTao.Visible = false; lblThongBao.ForeColor = Color.DarkGreen; lblThongBao.Text = "Đã tạo ADMIN đầu tiên. Hãy đăng nhập."; }
                }
            }
            catch (Exception ex) { lblThongBao.Text = ex.Message; }
        }
    }

    public sealed class FrmKhoiTaoAdmin : Form
    {
        private readonly DangNhapBll bll;
        private readonly ComboBox cboNhanVien = new ComboBox();
        private readonly TextBox txtTen = new TextBox();
        private readonly TextBox txtMatKhau = new TextBox();
        private readonly TextBox txtXacNhan = new TextBox();

        public FrmKhoiTaoAdmin(DangNhapBll bll)
        {
            this.bll = bll;
            Text = "Khởi tạo ADMIN đầu tiên"; StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog; MinimizeBox = false; MaximizeBox = false;
            ClientSize = new Size(470, 350); BackColor = Color.White;
            int y = 24;
            AddLabel("Chọn nhân viên quản trị", 28, y); cboNhanVien.SetBounds(28, y + 24, 410, 30); cboNhanVien.DropDownStyle = ComboBoxStyle.DropDownList; y += 72;
            AddLabel("Tên đăng nhập (4–50 ký tự)", 28, y); txtTen.SetBounds(28, y + 24, 410, 30); y += 72;
            AddLabel("Mật khẩu (ít nhất 10 ký tự, 3 nhóm ký tự)", 28, y); txtMatKhau.SetBounds(28, y + 24, 410, 30); txtMatKhau.UseSystemPasswordChar = true; y += 72;
            AddLabel("Nhập lại mật khẩu", 28, y); txtXacNhan.SetBounds(28, y + 24, 410, 30); txtXacNhan.UseSystemPasswordChar = true;
            Button save = new Button { Text = "Tạo ADMIN", BackColor = Color.FromArgb(0, 135, 137), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            save.SetBounds(28, 305, 130, 34); save.Click += delegate { Tao(); };
            Button cancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel }; cancel.SetBounds(168, 305, 90, 34);
            Controls.AddRange(new Control[] { cboNhanVien, txtTen, txtMatKhau, txtXacNhan, save, cancel });
            CancelButton = cancel; AcceptButton = save;
            Load += delegate { cboNhanVien.DisplayMember = "Ten"; cboNhanVien.ValueMember = "Ma"; cboNhanVien.DataSource = new NhanVienDal().LayTatCa().ConvertAll(n => new LuaChon(n.MaNV, n.TenNV)); if (cboNhanVien.Items.Count == 0) throw new InvalidOperationException("Cần tạo ít nhất một nhân viên trước khi khởi tạo ADMIN."); };
        }

        private void AddLabel(string text, int x, int y) { Controls.Add(new Label { Text = text, AutoSize = true, Location = new Point(x, y) }); }

        private void Tao()
        {
            try
            {
                if (txtMatKhau.Text != txtXacNhan.Text) throw new NghiepVuException("Mật khẩu xác nhận không khớp.");
                bll.KhoiTaoAdminDauTien(Convert.ToString(cboNhanVien.SelectedValue), txtTen.Text, txtMatKhau.Text);
                DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, "Không tạo được ADMIN", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
    }
}

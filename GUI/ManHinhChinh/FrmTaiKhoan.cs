using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public sealed class FrmTaiKhoan : Form
    {
        private const string DinhDangNgay = "dd/MM/yyyy HH:mm";
        private static readonly Color XanhNgoc = Color.FromArgb(0, 135, 137);
        private static readonly Color XanhDam = Color.FromArgb(48, 50, 73);
        private readonly TaiKhoanBll bll = new TaiKhoanBll();
        private readonly DataGridView grid = new DataGridView();
        private readonly TextBox txtTim = new TextBox();
        private readonly ComboBox locVaiTro = DanhSach("Tất cả", "ADMIN", "STAFF");
        private readonly ComboBox locTrangThai = DanhSach("Tất cả", "Hoạt động", "Đã khóa");
        private readonly Label[] thongKe = new Label[5];
        private readonly Label lblLoi = new Label { AutoSize = true, ForeColor = Color.Firebrick };
        private readonly TextBox txtMa = OChiDoc();
        private readonly TextBox txtMaNV = OChiDoc();
        private readonly TextBox txtTenNV = OChiDoc();
        private readonly TextBox txtTen = OChiDoc();
        private readonly ComboBox cboRole = DanhSach("ADMIN", "STAFF");
        private readonly TextBox txtTrangThai = OChiDoc();
        private readonly TextBox txtNgayTao = OChiDoc();
        private readonly TextBox txtDangNhapCuoi = OChiDoc();
        private readonly Button btnVaiTro = Nut("Cập nhật vai trò");
        private readonly Button btnMatKhau = Nut("Đặt lại mật khẩu");
        private readonly Button btnTrangThai = Nut("Khóa / Mở khóa tài khoản");
        private TaiKhoan chon;

        public FrmTaiKhoan()
        {
            PhanQuyen.YeuCau(Quyen.QuanLyTaiKhoan);
            Text = "Quản lý tài khoản";
            BackColor = Color.White;
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 420);
            Size = new Size(1180, 720);

            var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, Padding = new Padding(12) };
            for (int i = 0; i < 5; i++)
                root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.Controls.Add(new Label { Text = Text, AutoSize = true, ForeColor = XanhNgoc, Font = new Font(Font, FontStyle.Bold) });
            root.Controls.Add(new Label { Text = "Quản lý thông tin đăng nhập và phân quyền nhân viên.", AutoSize = true, ForeColor = XanhDam });
            root.Controls.Add(TaoThongKe());
            root.Controls.Add(TaoThanhLoc());
            root.Controls.Add(lblLoi);

            var noiDung = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            noiDung.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            noiDung.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            noiDung.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            noiDung.Controls.Add(TaoDanhSach(), 0, 0);
            noiDung.Controls.Add(TaoChiTiet(), 1, 0);
            root.Controls.Add(noiDung, 0, 5);
            Controls.Add(root);

            txtTim.TextChanged += delegate { TaiDanhSach(); };
            locVaiTro.SelectedIndexChanged += delegate { TaiDanhSach(); };
            locTrangThai.SelectedIndexChanged += delegate { TaiDanhSach(); };
            grid.CurrentCellChanged += delegate { ChonTaiKhoan(); };
            ChonTaiKhoan();
            Load += delegate { TaiDanhSach(true); };
        }

        private Control TaoThongKe()
        {
            var panel = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 5 };
            string[] nhan = { "Tổng tài khoản", "ADMIN", "STAFF", "Đang hoạt động", "Đã khóa" };
            for (int i = 0; i < nhan.Length; i++)
            {
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
                thongKe[i] = new Label
                {
                    Text = nhan[i] + "\n—", Tag = nhan[i], Dock = DockStyle.Fill, Height = 48,
                    TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.White,
                    BackColor = i % 2 == 0 && i != 4 ? XanhNgoc : XanhDam
                };
                panel.Controls.Add(thongKe[i], i, 0);
            }
            return panel;
        }

        private Control TaoThanhLoc()
        {
            var panel = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 4 };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105));
            ThemO(panel, "Tên đăng nhập, mã hoặc tên nhân viên", txtTim, 0, 0);
            ThemO(panel, "Vai trò", locVaiTro, 1, 0);
            ThemO(panel, "Trạng thái", locTrangThai, 2, 0);
            var lamMoi = Nut("Làm mới", mauPhu: true);
            lamMoi.Click += delegate { TaiDanhSach(true); };
            panel.Controls.Add(lamMoi, 3, 1);
            return panel;
        }

        private Control TaoDanhSach()
        {
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panel.Controls.Add(new Label { Text = "Danh sách tài khoản", AutoSize = true });
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoGenerateColumns = false;
            grid.BackgroundColor = SystemColors.Window;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ThemCot("", "STT", 40);
            ThemCot("MaTaiKhoan", "Mã tài khoản", 82);
            ThemCot("TenDangNhap", "Tên đăng nhập", 99);
            ThemCot("MaNV", "Mã nhân viên", 84);
            ThemCot("TenNV", "Tên nhân viên", 97);
            ThemCot("VaiTro", "Vai trò", 58);
            ThemCot("TrangThai", "Trạng thái", 80);
            ThemCot("NgayTao", "Ngày tạo", 111).DefaultCellStyle.Format = DinhDangNgay;
            grid.CellFormatting += delegate(object sender, DataGridViewCellFormattingEventArgs e)
            {
                if (e.ColumnIndex == 0)
                    e.Value = (e.RowIndex + 1).ToString();
                else if (e.ColumnIndex == 6 && e.Value is bool)
                    e.Value = TrangThai((bool)e.Value);
            };
            panel.Controls.Add(grid, 0, 1);
            var tao = Nut("Tạo tài khoản");
            tao.Dock = DockStyle.None;
            tao.Width = 148;
            tao.Click += delegate { Tao(); };
            panel.Controls.Add(tao, 0, 2);
            return panel;
        }

        private Control TaoChiTiet()
        {
            var group = new GroupBox { Text = "Thông tin tài khoản", Dock = DockStyle.Fill };
            var scroll = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            var fields = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 1 };
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            ThemO(fields, "Mã tài khoản", txtMa, 0, 0);
            ThemO(fields, "Mã nhân viên", txtMaNV, 0, 2);
            ThemO(fields, "Tên nhân viên", txtTenNV, 0, 4);
            ThemO(fields, "Tên đăng nhập", txtTen, 0, 6);
            ThemO(fields, "Vai trò", cboRole, 0, 8);
            ThemO(fields, "Trạng thái", txtTrangThai, 0, 10);
            ThemO(fields, "Ngày tạo", txtNgayTao, 0, 12);
            ThemO(fields, "Lần đăng nhập cuối", txtDangNhapCuoi, 0, 14);
            fields.Controls.Add(btnVaiTro, 0, 16);
            fields.Controls.Add(btnMatKhau, 0, 17);
            fields.Controls.Add(btnTrangThai, 0, 18);
            btnVaiTro.Click += delegate { DoiVaiTro(); };
            btnMatKhau.Click += delegate { DatLai(); };
            btnTrangThai.Click += delegate { DoiTrangThai(); };
            scroll.Controls.Add(fields);
            group.Controls.Add(scroll);
            return group;
        }

        private DataGridViewColumn ThemCot(string prop, string title, int width)
        {
            var cot = new DataGridViewTextBoxColumn
            {
                DataPropertyName = prop, HeaderText = title, MinimumWidth = width, FillWeight = width,
                SortMode = prop.Length == 0 ? DataGridViewColumnSortMode.NotSortable : DataGridViewColumnSortMode.Automatic
            };
            grid.Columns.Add(cot);
            return cot;
        }

        private static void ThemO(TableLayoutPanel parent, string title, Control input, int column, int row)
        {
            parent.Controls.Add(new Label { Text = title, AutoSize = true, Margin = new Padding(3, 4, 3, 0) }, column, row);
            input.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            parent.Controls.Add(input, column, row + 1);
        }

        private static ComboBox DanhSach(params string[] values)
        {
            var combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            combo.Items.AddRange(values);
            combo.SelectedIndex = 0;
            return combo;
        }

        private static TextBox OChiDoc()
        {
            return new TextBox { ReadOnly = true, TabStop = false };
        }

        private static Button Nut(string text, bool mauPhu = false)
        {
            var button = new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                Height = 32,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false,
                BackColor = mauPhu ? XanhDam : XanhNgoc,
                ForeColor = Color.White
            };
            button.FlatAppearance.BorderSize = 0;
            return button;
        }

        private static string TrangThai(bool hoatDong)
        {
            return hoatDong ? "Hoạt động" : "Đã khóa";
        }

        private void TaiDanhSach(bool capNhatThongKe = false)
        {
            try
            {
                string maDaChon = chon?.MaTaiKhoan;
                var tatCa = capNhatThongKe ? bll.LayDanhSach("") : null;
                var ketQua = tatCa != null && string.IsNullOrWhiteSpace(txtTim.Text)
                    ? tatCa : bll.LayDanhSach(txtTim.Text);
                if (tatCa != null)
                {
                    int[] soLuong = { tatCa.Count, tatCa.Count(t => t.VaiTro == "ADMIN"), tatCa.Count(t => t.VaiTro == "STAFF"),
                        tatCa.Count(t => t.TrangThai), tatCa.Count(t => !t.TrangThai) };
                    for (int i = 0; i < soLuong.Length; i++)
                        thongKe[i].Text = thongKe[i].Tag + "\n" + soLuong[i];
                }

                grid.DataSource = ketQua.Where(t =>
                    (locVaiTro.SelectedIndex == 0 || Equals(locVaiTro.SelectedItem, t.VaiTro)) &&
                    (locTrangThai.SelectedIndex == 0 || Equals(locTrangThai.SelectedItem, TrangThai(t.TrangThai)))).ToList();
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (((TaiKhoan)row.DataBoundItem).MaTaiKhoan != maDaChon)
                        continue;
                    grid.CurrentCell = row.Cells[0];
                    break;
                }
                ChonTaiKhoan();
                lblLoi.Text = "";
            }
            catch (Exception ex) { lblLoi.Text = ex.Message; }
        }

        private void ChonTaiKhoan()
        {
            chon = grid.CurrentRow?.DataBoundItem as TaiKhoan;
            txtMa.Text = chon?.MaTaiKhoan ?? "";
            txtMaNV.Text = chon?.MaNV ?? "";
            txtTenNV.Text = chon?.TenNV ?? "";
            txtTen.Text = chon?.TenDangNhap ?? "";
            cboRole.SelectedItem = chon?.VaiTro;
            txtTrangThai.Text = chon == null ? "" : TrangThai(chon.TrangThai);
            txtNgayTao.Text = chon?.NgayTao.ToString(DinhDangNgay) ?? "";
            txtDangNhapCuoi.Text = chon == null ? "" : chon.LanDangNhapCuoi?.ToString(DinhDangNgay) ?? "Chưa đăng nhập";
            btnVaiTro.Enabled = btnMatKhau.Enabled = btnTrangThai.Enabled = cboRole.Enabled = chon != null;
            btnTrangThai.Text = chon == null ? "Khóa / Mở khóa tài khoản" :
                chon.TrangThai ? "Khóa tài khoản" : "Mở khóa tài khoản";
        }

        private void Tao()
        {
            const string loi = "Không tạo được tài khoản";
            try
            {
                var nhanVien = bll.NhanVienChuaCoTaiKhoan().ToList();
                if (nhanVien.Count == 0)
                    throw new NghiepVuException("Không còn nhân viên nào chưa có tài khoản.");
                var cboNV = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, DisplayMember = "Ten", ValueMember = "Ma", DataSource = nhanVien };
                var ten = new TextBox();
                var vaiTro = DanhSach("STAFF", "ADMIN");
                if (HopThoaiMatKhau("Tạo tài khoản", "Lưu", loi, "Mật khẩu ban đầu",
                    matKhau => bll.TaoTaiKhoan(Convert.ToString(cboNV.SelectedValue), ten.Text, matKhau, Convert.ToString(vaiTro.SelectedItem)),
                    ("Nhân viên chưa có tài khoản", cboNV), ("Tên đăng nhập", ten), ("Vai trò", vaiTro)))
                {
                    TaiDanhSach(true);
                    MessageBox.Show(this, "Đã tạo tài khoản.");
                }
            }
            catch (Exception ex) { BaoLoi(this, loi, ex); }
        }

        private void DatLai()
        {
            var taiKhoan = chon;
            if (taiKhoan == null) return;
            var ten = OChiDoc();
            ten.Text = taiKhoan.TenDangNhap;
            if (HopThoaiMatKhau("Đặt lại mật khẩu", "Xác nhận", "Không đặt lại được", "Mật khẩu mới",
                matKhau => bll.DatLaiMatKhau(taiKhoan.MaTaiKhoan, matKhau), ("Tài khoản", ten)))
                MessageBox.Show(this, "Đã đặt lại mật khẩu.");
        }

        private bool HopThoaiMatKhau(string tieuDe, string tenNut, string loi, string nhanMatKhau,
            Action<string> luu, params (string Nhan, Control O)[] cacO)
        {
            using (var dialog = new Form
            {
                Text = tieuDe, StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false, MaximizeBox = false, ShowInTaskbar = false, AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            })
            {
                var panel = new TableLayoutPanel { AutoSize = true, ColumnCount = 1, Padding = new Padding(12) };
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 340));
                int row = 0;
                foreach (var o in cacO)
                {
                    ThemO(panel, o.Nhan, o.O, 0, row);
                    row += 2;
                }
                var matKhau = new TextBox { UseSystemPasswordChar = true };
                var xacNhan = new TextBox { UseSystemPasswordChar = true };
                ThemO(panel, nhanMatKhau, matKhau, 0, row);
                ThemO(panel, "Xác nhận mật khẩu", xacNhan, 0, row + 2);

                var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, FlowDirection = FlowDirection.RightToLeft };
                var huy = Nut("Hủy", mauPhu: true);
                huy.DialogResult = DialogResult.Cancel;
                var ok = Nut(tenNut);
                huy.Dock = ok.Dock = DockStyle.None;
                huy.Width = ok.Width = 96;
                buttons.Controls.Add(huy);
                buttons.Controls.Add(ok);
                panel.Controls.Add(buttons, 0, row + 4);
                dialog.Controls.Add(panel);
                dialog.AcceptButton = ok;
                dialog.CancelButton = huy;
                ok.Click += delegate
                {
                    try
                    {
                        if (!string.Equals(matKhau.Text, xacNhan.Text, StringComparison.Ordinal))
                            throw new NghiepVuException("Xác nhận mật khẩu không khớp.");
                        luu(matKhau.Text);
                        dialog.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex) { BaoLoi(dialog, loi, ex); }
                };
                return dialog.ShowDialog(this) == DialogResult.OK;
            }
        }

        private void DoiVaiTro()
        {
            if (chon == null) return;
            try { bll.DatVaiTro(chon.MaTaiKhoan, Convert.ToString(cboRole.SelectedItem)); TaiDanhSach(true); }
            catch (Exception ex) { BaoLoi(this, "Không cập nhật được", ex); }
        }

        private void DoiTrangThai()
        {
            var taiKhoan = chon;
            if (taiKhoan == null) return;
            string thaoTac = taiKhoan.TrangThai ? "khóa" : "mở khóa";
            if (MessageBox.Show(this, "Bạn có muốn " + thaoTac + " tài khoản " + taiKhoan.TenDangNhap + "?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;
            try { bll.DatTrangThai(taiKhoan.MaTaiKhoan, !taiKhoan.TrangThai); TaiDanhSach(true); }
            catch (Exception ex) { BaoLoi(this, "Không cập nhật được", ex); }
        }

        private static void BaoLoi(IWin32Window owner, string title, Exception ex)
        {
            MessageBox.Show(owner, ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

namespace QuanLyBanTuBep.UI
{
    partial class FrmHoaDon
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlPhai = new System.Windows.Forms.Panel();
            this.lvDong = new System.Windows.Forms.ListView();
            this.pnlDuoi = new System.Windows.Forms.Panel();
            this.pnlNut = new System.Windows.Forms.Panel();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnXoaHD = new System.Windows.Forms.Button();
            this.lblTong = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cach2 = new System.Windows.Forms.Panel();
            this.pnlNhapDong = new System.Windows.Forms.Panel();
            this.lblMatHang = new System.Windows.Forms.Label();
            this.cboHang = new System.Windows.Forms.ComboBox();
            this.lblTon = new System.Windows.Forms.Label();
            this.lblMau = new System.Windows.Forms.Label();
            this.txtMau = new System.Windows.Forms.TextBox();
            this.lblKichThuoc = new System.Windows.Forms.Label();
            this.txtKichThuoc = new System.Windows.Forms.TextBox();
            this.lblChatLieu = new System.Windows.Forms.Label();
            this.txtChatLieu = new System.Windows.Forms.TextBox();
            this.lblNuocSX = new System.Windows.Forms.Label();
            this.txtNuocSX = new System.Windows.Forms.TextBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.nudSL = new System.Windows.Forms.NumericUpDown();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.nudDonGia = new System.Windows.Forms.NumericUpDown();
            this.lblGiamGia = new System.Windows.Forms.Label();
            this.nudGiam = new System.Windows.Forms.NumericUpDown();
            this.btnThemDong = new System.Windows.Forms.Button();
            this.btnXoaDong = new System.Windows.Forms.Button();
            this.cach1 = new System.Windows.Forms.Panel();
            this.pnlThongTin = new System.Windows.Forms.Panel();
            this.lblSoHD = new System.Windows.Forms.Label();
            this.txtSo = new System.Windows.Forms.TextBox();
            this.lblNgay = new System.Windows.Forms.Label();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboNV = new System.Windows.Forms.ComboBox();
            this.lblDoiTac = new System.Windows.Forms.Label();
            this.cboDoiTac = new System.Windows.Forms.ComboBox();
            this.lblMaKhach = new System.Windows.Forms.Label();
            this.txtMaKhach = new System.Windows.Forms.TextBox();
            this.lblTenKhach = new System.Windows.Forms.Label();
            this.txtTenKhach = new System.Windows.Forms.TextBox();
            this.lblDiaChiKhach = new System.Windows.Forms.Label();
            this.txtDiaChiKhach = new System.Windows.Forms.TextBox();
            this.lblDienThoaiKhach = new System.Windows.Forms.Label();
            this.txtDienThoaiKhach = new System.Windows.Forms.TextBox();
            this.pnlTrai = new System.Windows.Forms.Panel();
            this.lvDs = new System.Windows.Forms.ListView();
            this.pnlTraiTren = new System.Windows.Forms.Panel();
            this.btnMoi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudSL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiam)).BeginInit();
            this.SuspendLayout();
            this.pnlPhai.SuspendLayout();
            this.pnlDuoi.SuspendLayout();
            this.pnlNut.SuspendLayout();
            this.pnlNhapDong.SuspendLayout();
            this.pnlThongTin.SuspendLayout();
            this.pnlTrai.SuspendLayout();
            this.pnlTraiTren.SuspendLayout();

            this.pnlPhai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPhai.Name = "pnlPhai";
            this.pnlPhai.Controls.Add(this.lvDong);
            this.pnlPhai.Controls.Add(this.pnlDuoi);
            this.pnlPhai.Controls.Add(this.cach2);
            this.pnlPhai.Controls.Add(this.pnlNhapDong);
            this.pnlPhai.Controls.Add(this.cach1);
            this.pnlPhai.Controls.Add(this.pnlThongTin);

            this.lvDong.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.lvDong.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvDong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvDong.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lvDong.FullRowSelect = true;
            this.lvDong.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDong.HideSelection = false;
            this.lvDong.MultiSelect = false;
            this.lvDong.UseCompatibleStateImageBehavior = false;
            this.lvDong.View = System.Windows.Forms.View.Details;
            this.lvDong.Name = "lvDong";

            this.pnlDuoi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDuoi.Size = new System.Drawing.Size(0, 64);
            this.pnlDuoi.Name = "pnlDuoi";
            this.pnlDuoi.Controls.Add(this.pnlNut);
            this.pnlDuoi.Controls.Add(this.lblTong);
            this.pnlDuoi.Controls.Add(this.lblTrangThai);

            this.pnlNut.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlNut.Size = new System.Drawing.Size(250, 0);
            this.pnlNut.Name = "pnlNut";
            this.pnlNut.Controls.Add(this.btnLuu);
            this.pnlNut.Controls.Add(this.btnXoaHD);

            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(0, 135, 137);
            this.btnLuu.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.Location = new System.Drawing.Point(0, 16);
            this.btnLuu.Size = new System.Drawing.Size(120, 38);
            this.btnLuu.Text = "Lưu hoá đơn";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);

            this.btnXoaHD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaHD.FlatAppearance.BorderSize = 0;
            this.btnXoaHD.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnXoaHD.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnXoaHD.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaHD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaHD.Location = new System.Drawing.Point(128, 16);
            this.btnXoaHD.Size = new System.Drawing.Size(120, 38);
            this.btnXoaHD.Text = "Xoá hoá đơn";
            this.btnXoaHD.UseVisualStyleBackColor = false;
            this.btnXoaHD.Name = "btnXoaHD";
            this.btnXoaHD.Click += new System.EventHandler(this.btnXoaHD_Click);

            this.lblTong.AutoSize = true;
            this.lblTong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTong.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTong.Location = new System.Drawing.Point(0, 30);
            this.lblTong.Text = "";
            this.lblTong.Name = "lblTong";

            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblTrangThai.Location = new System.Drawing.Point(0, 8);
            this.lblTrangThai.Text = "";
            this.lblTrangThai.Name = "lblTrangThai";

            this.cach2.Dock = System.Windows.Forms.DockStyle.Top;
            this.cach2.Size = new System.Drawing.Size(0, 8);
            this.cach2.Name = "cach2";

            this.pnlNhapDong.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNhapDong.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.pnlNhapDong.Size = new System.Drawing.Size(0, 174);
            this.pnlNhapDong.Name = "pnlNhapDong";
            this.pnlNhapDong.Controls.Add(this.lblMatHang);
            this.pnlNhapDong.Controls.Add(this.cboHang);
            this.pnlNhapDong.Controls.Add(this.lblTon);
            this.pnlNhapDong.Controls.Add(this.lblMau);
            this.pnlNhapDong.Controls.Add(this.txtMau);
            this.pnlNhapDong.Controls.Add(this.lblKichThuoc);
            this.pnlNhapDong.Controls.Add(this.txtKichThuoc);
            this.pnlNhapDong.Controls.Add(this.lblChatLieu);
            this.pnlNhapDong.Controls.Add(this.txtChatLieu);
            this.pnlNhapDong.Controls.Add(this.lblNuocSX);
            this.pnlNhapDong.Controls.Add(this.txtNuocSX);
            this.pnlNhapDong.Controls.Add(this.lblSoLuong);
            this.pnlNhapDong.Controls.Add(this.nudSL);
            this.pnlNhapDong.Controls.Add(this.lblDonGia);
            this.pnlNhapDong.Controls.Add(this.nudDonGia);
            this.pnlNhapDong.Controls.Add(this.lblGiamGia);
            this.pnlNhapDong.Controls.Add(this.nudGiam);
            this.pnlNhapDong.Controls.Add(this.btnThemDong);
            this.pnlNhapDong.Controls.Add(this.btnXoaDong);

            this.lblMatHang.AutoSize = true;
            this.lblMatHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatHang.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblMatHang.Location = new System.Drawing.Point(16, 10);
            this.lblMatHang.Text = "Mặt hàng";
            this.lblMatHang.Name = "lblMatHang";

            this.cboHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHang.FormattingEnabled = true;
            this.cboHang.Location = new System.Drawing.Point(16, 32);
            this.cboHang.Size = new System.Drawing.Size(540, 29);
            this.cboHang.DropDownWidth = 760;
            this.cboHang.Name = "cboHang";
            this.cboHang.SelectedIndexChanged += new System.EventHandler(this.cboHang_SelectedIndexChanged);

            this.lblTon.AutoSize = true;
            this.lblTon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTon.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblTon.Location = new System.Drawing.Point(570, 38);
            this.lblTon.Text = "";
            this.lblTon.Name = "lblTon";

            this.lblMau.AutoSize = true;
            this.lblMau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMau.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblMau.Location = new System.Drawing.Point(16, 66);
            this.lblMau.Text = "Màu sắc";
            this.lblMau.Name = "lblMau";
            this.txtMau.Location = new System.Drawing.Point(16, 86);
            this.txtMau.Size = new System.Drawing.Size(190, 25);
            this.txtMau.ReadOnly = true;
            this.txtMau.BackColor = System.Drawing.Color.White;
            this.txtMau.Name = "txtMau";

            this.lblKichThuoc.AutoSize = true;
            this.lblKichThuoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKichThuoc.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblKichThuoc.Location = new System.Drawing.Point(220, 66);
            this.lblKichThuoc.Text = "Kích thước";
            this.lblKichThuoc.Name = "lblKichThuoc";
            this.txtKichThuoc.Location = new System.Drawing.Point(220, 86);
            this.txtKichThuoc.Size = new System.Drawing.Size(190, 25);
            this.txtKichThuoc.ReadOnly = true;
            this.txtKichThuoc.BackColor = System.Drawing.Color.White;
            this.txtKichThuoc.Name = "txtKichThuoc";

            this.lblChatLieu.AutoSize = true;
            this.lblChatLieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChatLieu.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblChatLieu.Location = new System.Drawing.Point(424, 66);
            this.lblChatLieu.Text = "Chất liệu";
            this.lblChatLieu.Name = "lblChatLieu";
            this.txtChatLieu.Location = new System.Drawing.Point(424, 86);
            this.txtChatLieu.Size = new System.Drawing.Size(190, 25);
            this.txtChatLieu.ReadOnly = true;
            this.txtChatLieu.BackColor = System.Drawing.Color.White;
            this.txtChatLieu.Name = "txtChatLieu";

            this.lblNuocSX.AutoSize = true;
            this.lblNuocSX.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNuocSX.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblNuocSX.Location = new System.Drawing.Point(628, 66);
            this.lblNuocSX.Text = "Nước sản xuất";
            this.lblNuocSX.Name = "lblNuocSX";
            this.txtNuocSX.Location = new System.Drawing.Point(628, 86);
            this.txtNuocSX.Size = new System.Drawing.Size(190, 25);
            this.txtNuocSX.ReadOnly = true;
            this.txtNuocSX.BackColor = System.Drawing.Color.White;
            this.txtNuocSX.Name = "txtNuocSX";

            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuong.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSoLuong.Location = new System.Drawing.Point(16, 116);
            this.lblSoLuong.Text = "Số lượng";
            this.lblSoLuong.Name = "lblSoLuong";

            this.nudSL.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudSL.Location = new System.Drawing.Point(16, 138);
            this.nudSL.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.nudSL.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSL.Size = new System.Drawing.Size(90, 29);
            this.nudSL.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSL.Name = "nudSL";

            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDonGia.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblDonGia.Location = new System.Drawing.Point(120, 116);
            this.lblDonGia.Text = "Đơn giá";
            this.lblDonGia.Name = "lblDonGia";

            this.nudDonGia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudDonGia.Location = new System.Drawing.Point(120, 138);
            this.nudDonGia.Maximum = new decimal(new int[] { 1316134911, 2328, 0, 0 });
            this.nudDonGia.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudDonGia.Size = new System.Drawing.Size(150, 29);
            this.nudDonGia.ThousandsSeparator = true;
            this.nudDonGia.Name = "nudDonGia";

            this.lblGiamGia.AutoSize = true;
            this.lblGiamGia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiamGia.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblGiamGia.Location = new System.Drawing.Point(284, 116);
            this.lblGiamGia.Text = "Giảm giá (%)";
            this.lblGiamGia.Name = "lblGiamGia";

            this.nudGiam.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudGiam.Location = new System.Drawing.Point(284, 138);
            this.nudGiam.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.nudGiam.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudGiam.Size = new System.Drawing.Size(80, 29);
            this.nudGiam.DecimalPlaces = 1;
            this.nudGiam.Name = "nudGiam";

            this.btnThemDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemDong.FlatAppearance.BorderSize = 0;
            this.btnThemDong.BackColor = System.Drawing.Color.FromArgb(0, 135, 137);
            this.btnThemDong.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnThemDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemDong.Location = new System.Drawing.Point(380, 132);
            this.btnThemDong.Size = new System.Drawing.Size(100, 36);
            this.btnThemDong.Text = "Thêm dòng";
            this.btnThemDong.UseVisualStyleBackColor = false;
            this.btnThemDong.Name = "btnThemDong";
            this.btnThemDong.Click += new System.EventHandler(this.btnThemDong_Click);

            this.btnXoaDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaDong.FlatAppearance.BorderSize = 0;
            this.btnXoaDong.BackColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnXoaDong.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnXoaDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaDong.Location = new System.Drawing.Point(488, 132);
            this.btnXoaDong.Size = new System.Drawing.Size(90, 36);
            this.btnXoaDong.Text = "Xoá dòng";
            this.btnXoaDong.UseVisualStyleBackColor = false;
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.Click += new System.EventHandler(this.btnXoaDong_Click);

            this.cach1.Dock = System.Windows.Forms.DockStyle.Top;
            this.cach1.Size = new System.Drawing.Size(0, 8);
            this.cach1.Name = "cach1";

            this.pnlThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTin.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.pnlThongTin.Size = new System.Drawing.Size(0, 132);
            this.pnlThongTin.Name = "pnlThongTin";
            this.pnlThongTin.Controls.Add(this.lblSoHD);
            this.pnlThongTin.Controls.Add(this.txtSo);
            this.pnlThongTin.Controls.Add(this.lblNgay);
            this.pnlThongTin.Controls.Add(this.dtpNgay);
            this.pnlThongTin.Controls.Add(this.lblNhanVien);
            this.pnlThongTin.Controls.Add(this.cboNV);
            this.pnlThongTin.Controls.Add(this.lblDoiTac);
            this.pnlThongTin.Controls.Add(this.cboDoiTac);
            this.pnlThongTin.Controls.Add(this.lblMaKhach);
            this.pnlThongTin.Controls.Add(this.txtMaKhach);
            this.pnlThongTin.Controls.Add(this.lblTenKhach);
            this.pnlThongTin.Controls.Add(this.txtTenKhach);
            this.pnlThongTin.Controls.Add(this.lblDiaChiKhach);
            this.pnlThongTin.Controls.Add(this.txtDiaChiKhach);
            this.pnlThongTin.Controls.Add(this.lblDienThoaiKhach);
            this.pnlThongTin.Controls.Add(this.txtDienThoaiKhach);

            this.lblSoHD.AutoSize = true;
            this.lblSoHD.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoHD.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSoHD.Location = new System.Drawing.Point(16, 10);
            this.lblSoHD.Text = "Số hoá đơn";
            this.lblSoHD.Name = "lblSoHD";

            this.txtSo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSo.Location = new System.Drawing.Point(16, 32);
            this.txtSo.ReadOnly = true;
            this.txtSo.Size = new System.Drawing.Size(250, 29);
            this.txtSo.Name = "txtSo";

            this.lblNgay.AutoSize = true;
            this.lblNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgay.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblNgay.Location = new System.Drawing.Point(290, 10);
            this.lblNgay.Text = "Ngày";
            this.lblNgay.Name = "lblNgay";

            this.dtpNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpNgay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgay.Location = new System.Drawing.Point(290, 32);
            this.dtpNgay.Size = new System.Drawing.Size(250, 29);
            this.dtpNgay.Name = "dtpNgay";

            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhanVien.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblNhanVien.Location = new System.Drawing.Point(16, 70);
            this.lblNhanVien.Text = "Nhân viên *";
            this.lblNhanVien.Name = "lblNhanVien";

            this.cboNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNV.FormattingEnabled = true;
            this.cboNV.Location = new System.Drawing.Point(16, 92);
            this.cboNV.Size = new System.Drawing.Size(250, 29);
            this.cboNV.Name = "cboNV";

            this.lblDoiTac.AutoSize = true;
            this.lblDoiTac.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoiTac.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblDoiTac.Location = new System.Drawing.Point(290, 70);
            this.lblDoiTac.Text = "Đối tác *";
            this.lblDoiTac.Name = "lblDoiTac";

            this.cboDoiTac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoiTac.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDoiTac.FormattingEnabled = true;
            this.cboDoiTac.Location = new System.Drawing.Point(290, 92);
            this.cboDoiTac.Size = new System.Drawing.Size(250, 29);
            this.cboDoiTac.Name = "cboDoiTac";

            this.lblMaKhach.AutoSize = true;
            this.lblMaKhach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaKhach.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblMaKhach.Location = new System.Drawing.Point(290, 70);
            this.lblMaKhach.Text = "Mã khách hàng";
            this.lblMaKhach.Name = "lblMaKhach";

            this.txtMaKhach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaKhach.Location = new System.Drawing.Point(290, 92);
            this.txtMaKhach.ReadOnly = true;
            this.txtMaKhach.Size = new System.Drawing.Size(120, 29);
            this.txtMaKhach.Name = "txtMaKhach";

            this.lblTenKhach.AutoSize = true;
            this.lblTenKhach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenKhach.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblTenKhach.Location = new System.Drawing.Point(420, 70);
            this.lblTenKhach.Text = "Tên khách hàng (tuỳ chọn)";
            this.lblTenKhach.Name = "lblTenKhach";

            this.txtTenKhach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenKhach.Location = new System.Drawing.Point(420, 92);
            this.txtTenKhach.Size = new System.Drawing.Size(180, 29);
            this.txtTenKhach.MaxLength = 100;
            this.txtTenKhach.Name = "txtTenKhach";

            this.lblDiaChiKhach.AutoSize = true;
            this.lblDiaChiKhach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiaChiKhach.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblDiaChiKhach.Location = new System.Drawing.Point(605, 70);
            this.lblDiaChiKhach.Text = "Địa chỉ (tuỳ chọn)";
            this.lblDiaChiKhach.Name = "lblDiaChiKhach";

            this.txtDiaChiKhach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiaChiKhach.Location = new System.Drawing.Point(605, 92);
            this.txtDiaChiKhach.Size = new System.Drawing.Size(130, 29);
            this.txtDiaChiKhach.MaxLength = 200;
            this.txtDiaChiKhach.Name = "txtDiaChiKhach";

            this.lblDienThoaiKhach.AutoSize = true;
            this.lblDienThoaiKhach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDienThoaiKhach.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblDienThoaiKhach.Location = new System.Drawing.Point(750, 70);
            this.lblDienThoaiKhach.Text = "Điện thoại (tuỳ chọn)";
            this.lblDienThoaiKhach.Name = "lblDienThoaiKhach";

            this.txtDienThoaiKhach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDienThoaiKhach.Location = new System.Drawing.Point(750, 92);
            this.txtDienThoaiKhach.Size = new System.Drawing.Size(130, 29);
            this.txtDienThoaiKhach.MaxLength = 11;
            this.txtDienThoaiKhach.Name = "txtDienThoaiKhach";
            this.pnlTrai.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTrai.Size = new System.Drawing.Size(356, 0);
            this.pnlTrai.Padding = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.pnlTrai.Name = "pnlTrai";
            this.pnlTrai.Controls.Add(this.lvDs);
            this.pnlTrai.Controls.Add(this.pnlTraiTren);

            this.lvDs.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.lvDs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvDs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDs.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvDs.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lvDs.FullRowSelect = true;
            this.lvDs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDs.HideSelection = false;
            this.lvDs.MultiSelect = false;
            this.lvDs.UseCompatibleStateImageBehavior = false;
            this.lvDs.View = System.Windows.Forms.View.Details;
            this.lvDs.Name = "lvDs";
            this.lvDs.SelectedIndexChanged += new System.EventHandler(this.lvDs_SelectedIndexChanged);

            this.pnlTraiTren.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTraiTren.Size = new System.Drawing.Size(0, 56);
            this.pnlTraiTren.Name = "pnlTraiTren";
            this.pnlTraiTren.Controls.Add(this.btnMoi);

            this.btnMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoi.FlatAppearance.BorderSize = 0;
            this.btnMoi.BackColor = System.Drawing.Color.FromArgb(0, 135, 137);
            this.btnMoi.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoi.Location = new System.Drawing.Point(0, 10);
            this.btnMoi.Size = new System.Drawing.Size(340, 36);
            this.btnMoi.Text = "+  Hoá đơn mới";
            this.btnMoi.UseVisualStyleBackColor = false;
            this.btnMoi.Name = "btnMoi";
            this.btnMoi.Click += new System.EventHandler(this.btnMoi_Click);

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.pnlPhai);
            this.Controls.Add(this.pnlTrai);
            this.Name = "FrmHoaDon";
            ((System.ComponentModel.ISupportInitialize)(this.nudSL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiam)).EndInit();
            this.pnlTraiTren.ResumeLayout(false);
            this.pnlTraiTren.PerformLayout();
            this.pnlTrai.ResumeLayout(false);
            this.pnlTrai.PerformLayout();
            this.pnlThongTin.ResumeLayout(false);
            this.pnlThongTin.PerformLayout();
            this.pnlNhapDong.ResumeLayout(false);
            this.pnlNhapDong.PerformLayout();
            this.pnlNut.ResumeLayout(false);
            this.pnlNut.PerformLayout();
            this.pnlDuoi.ResumeLayout(false);
            this.pnlDuoi.PerformLayout();
            this.pnlPhai.ResumeLayout(false);
            this.pnlPhai.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlPhai;
        private System.Windows.Forms.ListView lvDong;
        private System.Windows.Forms.Panel pnlDuoi;
        private System.Windows.Forms.Panel pnlNut;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoaHD;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Panel cach2;
        private System.Windows.Forms.Panel pnlNhapDong;
        private System.Windows.Forms.Label lblMatHang;
        private System.Windows.Forms.ComboBox cboHang;
        private System.Windows.Forms.Label lblTon;
        private System.Windows.Forms.Label lblMau;
        private System.Windows.Forms.TextBox txtMau;
        private System.Windows.Forms.Label lblKichThuoc;
        private System.Windows.Forms.TextBox txtKichThuoc;
        private System.Windows.Forms.Label lblChatLieu;
        private System.Windows.Forms.TextBox txtChatLieu;
        private System.Windows.Forms.Label lblNuocSX;
        private System.Windows.Forms.TextBox txtNuocSX;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown nudSL;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.NumericUpDown nudDonGia;
        private System.Windows.Forms.Label lblGiamGia;
        private System.Windows.Forms.NumericUpDown nudGiam;
        private System.Windows.Forms.Button btnThemDong;
        private System.Windows.Forms.Button btnXoaDong;
        private System.Windows.Forms.Panel cach1;
        private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.Label lblSoHD;
        private System.Windows.Forms.TextBox txtSo;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.ComboBox cboNV;
        private System.Windows.Forms.Label lblDoiTac;
        private System.Windows.Forms.ComboBox cboDoiTac;
        private System.Windows.Forms.Label lblMaKhach;
        private System.Windows.Forms.TextBox txtMaKhach;
        private System.Windows.Forms.Label lblTenKhach;
        private System.Windows.Forms.TextBox txtTenKhach;
        private System.Windows.Forms.Label lblDiaChiKhach;
        private System.Windows.Forms.TextBox txtDiaChiKhach;
        private System.Windows.Forms.Label lblDienThoaiKhach;
        private System.Windows.Forms.TextBox txtDienThoaiKhach;
        private System.Windows.Forms.Panel pnlTrai;
        private System.Windows.Forms.ListView lvDs;
        private System.Windows.Forms.Panel pnlTraiTren;
        private System.Windows.Forms.Button btnMoi;
    }
}



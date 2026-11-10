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
            this.pnlPhai.SuspendLayout();
            this.pnlDuoi.SuspendLayout();
            this.pnlNut.SuspendLayout();
            this.pnlNhapDong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiam)).BeginInit();
            this.pnlThongTin.SuspendLayout();
            this.pnlTrai.SuspendLayout();
            this.pnlTraiTren.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPhai
            // 
            this.pnlPhai.Controls.Add(this.lvDong);
            this.pnlPhai.Controls.Add(this.pnlDuoi);
            this.pnlPhai.Controls.Add(this.cach2);
            this.pnlPhai.Controls.Add(this.pnlNhapDong);
            this.pnlPhai.Controls.Add(this.cach1);
            this.pnlPhai.Controls.Add(this.pnlThongTin);
            this.pnlPhai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPhai.Location = new System.Drawing.Point(356, 0);
            this.pnlPhai.Name = "pnlPhai";
            this.pnlPhai.Size = new System.Drawing.Size(1262, 600);
            this.pnlPhai.TabIndex = 0;
            // 
            // lvDong
            // 
            this.lvDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lvDong.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvDong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lvDong.FullRowSelect = true;
            this.lvDong.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDong.HideSelection = false;
            this.lvDong.Location = new System.Drawing.Point(0, 322);
            this.lvDong.MultiSelect = false;
            this.lvDong.Name = "lvDong";
            this.lvDong.Size = new System.Drawing.Size(1262, 214);
            this.lvDong.TabIndex = 0;
            this.lvDong.UseCompatibleStateImageBehavior = false;
            this.lvDong.View = System.Windows.Forms.View.Details;
            // 
            // pnlDuoi
            // 
            this.pnlDuoi.Controls.Add(this.pnlNut);
            this.pnlDuoi.Controls.Add(this.lblTong);
            this.pnlDuoi.Controls.Add(this.lblTrangThai);
            this.pnlDuoi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDuoi.Location = new System.Drawing.Point(0, 536);
            this.pnlDuoi.Name = "pnlDuoi";
            this.pnlDuoi.Size = new System.Drawing.Size(1262, 64);
            this.pnlDuoi.TabIndex = 1;
            // 
            // pnlNut
            // 
            this.pnlNut.Controls.Add(this.btnLuu);
            this.pnlNut.Controls.Add(this.btnXoaHD);
            this.pnlNut.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlNut.Location = new System.Drawing.Point(1012, 0);
            this.pnlNut.Name = "pnlNut";
            this.pnlNut.Size = new System.Drawing.Size(250, 64);
            this.pnlNut.TabIndex = 0;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(135)))), ((int)(((byte)(137)))));
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnLuu.Location = new System.Drawing.Point(0, 16);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(120, 38);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "Lưu hoá đơn";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnXoaHD
            // 
            this.btnXoaHD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnXoaHD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaHD.FlatAppearance.BorderSize = 0;
            this.btnXoaHD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaHD.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaHD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnXoaHD.Location = new System.Drawing.Point(128, 16);
            this.btnXoaHD.Name = "btnXoaHD";
            this.btnXoaHD.Size = new System.Drawing.Size(120, 38);
            this.btnXoaHD.TabIndex = 1;
            this.btnXoaHD.Text = "Xoá hoá đơn";
            this.btnXoaHD.UseVisualStyleBackColor = false;
            this.btnXoaHD.Click += new System.EventHandler(this.btnXoaHD_Click);
            // 
            // lblTong
            // 
            this.lblTong.AutoSize = true;
            this.lblTong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTong.Location = new System.Drawing.Point(0, 30);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(0, 38);
            this.lblTong.TabIndex = 1;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblTrangThai.Location = new System.Drawing.Point(0, 8);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(0, 25);
            this.lblTrangThai.TabIndex = 2;
            // 
            // cach2
            // 
            this.cach2.Dock = System.Windows.Forms.DockStyle.Top;
            this.cach2.Location = new System.Drawing.Point(0, 314);
            this.cach2.Name = "cach2";
            this.cach2.Size = new System.Drawing.Size(1262, 8);
            this.cach2.TabIndex = 2;
            // 
            // pnlNhapDong
            // 
            this.pnlNhapDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
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
            this.pnlNhapDong.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNhapDong.Location = new System.Drawing.Point(0, 140);
            this.pnlNhapDong.Name = "pnlNhapDong";
            this.pnlNhapDong.Size = new System.Drawing.Size(1262, 174);
            this.pnlNhapDong.TabIndex = 3;
            // 
            // lblMatHang
            // 
            this.lblMatHang.AutoSize = true;
            this.lblMatHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblMatHang.Location = new System.Drawing.Point(16, 10);
            this.lblMatHang.Name = "lblMatHang";
            this.lblMatHang.Size = new System.Drawing.Size(88, 25);
            this.lblMatHang.TabIndex = 0;
            this.lblMatHang.Text = "Mặt hàng";
            // 
            // cboHang
            // 
            this.cboHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHang.DropDownWidth = 760;
            this.cboHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHang.FormattingEnabled = true;
            this.cboHang.Location = new System.Drawing.Point(16, 32);
            this.cboHang.Name = "cboHang";
            this.cboHang.Size = new System.Drawing.Size(540, 36);
            this.cboHang.TabIndex = 1;
            this.cboHang.SelectedIndexChanged += new System.EventHandler(this.cboHang_SelectedIndexChanged);
            // 
            // lblTon
            // 
            this.lblTon.AutoSize = true;
            this.lblTon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblTon.Location = new System.Drawing.Point(570, 38);
            this.lblTon.Name = "lblTon";
            this.lblTon.Size = new System.Drawing.Size(0, 28);
            this.lblTon.TabIndex = 2;
            // 
            // lblMau
            // 
            this.lblMau.AutoSize = true;
            this.lblMau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblMau.Location = new System.Drawing.Point(16, 66);
            this.lblMau.Name = "lblMau";
            this.lblMau.Size = new System.Drawing.Size(77, 25);
            this.lblMau.TabIndex = 3;
            this.lblMau.Text = "Màu sắc";
            // 
            // txtMau
            // 
            this.txtMau.BackColor = System.Drawing.Color.White;
            this.txtMau.Location = new System.Drawing.Point(16, 86);
            this.txtMau.Name = "txtMau";
            this.txtMau.ReadOnly = true;
            this.txtMau.Size = new System.Drawing.Size(190, 26);
            this.txtMau.TabIndex = 4;
            // 
            // lblKichThuoc
            // 
            this.lblKichThuoc.AutoSize = true;
            this.lblKichThuoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKichThuoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblKichThuoc.Location = new System.Drawing.Point(220, 66);
            this.lblKichThuoc.Name = "lblKichThuoc";
            this.lblKichThuoc.Size = new System.Drawing.Size(95, 25);
            this.lblKichThuoc.TabIndex = 5;
            this.lblKichThuoc.Text = "Kích thước";
            // 
            // txtKichThuoc
            // 
            this.txtKichThuoc.BackColor = System.Drawing.Color.White;
            this.txtKichThuoc.Location = new System.Drawing.Point(220, 86);
            this.txtKichThuoc.Name = "txtKichThuoc";
            this.txtKichThuoc.ReadOnly = true;
            this.txtKichThuoc.Size = new System.Drawing.Size(190, 26);
            this.txtKichThuoc.TabIndex = 6;
            // 
            // lblChatLieu
            // 
            this.lblChatLieu.AutoSize = true;
            this.lblChatLieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChatLieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblChatLieu.Location = new System.Drawing.Point(424, 66);
            this.lblChatLieu.Name = "lblChatLieu";
            this.lblChatLieu.Size = new System.Drawing.Size(80, 25);
            this.lblChatLieu.TabIndex = 7;
            this.lblChatLieu.Text = "Chất liệu";
            // 
            // txtChatLieu
            // 
            this.txtChatLieu.BackColor = System.Drawing.Color.White;
            this.txtChatLieu.Location = new System.Drawing.Point(424, 86);
            this.txtChatLieu.Name = "txtChatLieu";
            this.txtChatLieu.ReadOnly = true;
            this.txtChatLieu.Size = new System.Drawing.Size(190, 26);
            this.txtChatLieu.TabIndex = 8;
            // 
            // lblNuocSX
            // 
            this.lblNuocSX.AutoSize = true;
            this.lblNuocSX.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNuocSX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblNuocSX.Location = new System.Drawing.Point(628, 66);
            this.lblNuocSX.Name = "lblNuocSX";
            this.lblNuocSX.Size = new System.Drawing.Size(125, 25);
            this.lblNuocSX.TabIndex = 9;
            this.lblNuocSX.Text = "Nước sản xuất";
            // 
            // txtNuocSX
            // 
            this.txtNuocSX.BackColor = System.Drawing.Color.White;
            this.txtNuocSX.Location = new System.Drawing.Point(628, 86);
            this.txtNuocSX.Name = "txtNuocSX";
            this.txtNuocSX.ReadOnly = true;
            this.txtNuocSX.Size = new System.Drawing.Size(190, 26);
            this.txtNuocSX.TabIndex = 10;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblSoLuong.Location = new System.Drawing.Point(16, 116);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(85, 25);
            this.lblSoLuong.TabIndex = 11;
            this.lblSoLuong.Text = "Số lượng";
            // 
            // nudSL
            // 
            this.nudSL.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudSL.Location = new System.Drawing.Point(16, 138);
            this.nudSL.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudSL.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSL.Name = "nudSL";
            this.nudSL.Size = new System.Drawing.Size(90, 34);
            this.nudSL.TabIndex = 12;
            this.nudSL.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDonGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblDonGia.Location = new System.Drawing.Point(120, 116);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(75, 25);
            this.lblDonGia.TabIndex = 13;
            this.lblDonGia.Text = "Đơn giá";
            // 
            // nudDonGia
            // 
            this.nudDonGia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudDonGia.Location = new System.Drawing.Point(120, 138);
            this.nudDonGia.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.nudDonGia.Name = "nudDonGia";
            this.nudDonGia.Size = new System.Drawing.Size(150, 34);
            this.nudDonGia.TabIndex = 14;
            this.nudDonGia.ThousandsSeparator = true;
            // 
            // lblGiamGia
            // 
            this.lblGiamGia.AutoSize = true;
            this.lblGiamGia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiamGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblGiamGia.Location = new System.Drawing.Point(284, 116);
            this.lblGiamGia.Name = "lblGiamGia";
            this.lblGiamGia.Size = new System.Drawing.Size(112, 25);
            this.lblGiamGia.TabIndex = 15;
            this.lblGiamGia.Text = "Giảm giá (%)";
            // 
            // nudGiam
            // 
            this.nudGiam.DecimalPlaces = 1;
            this.nudGiam.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudGiam.Location = new System.Drawing.Point(284, 138);
            this.nudGiam.Name = "nudGiam";
            this.nudGiam.Size = new System.Drawing.Size(80, 34);
            this.nudGiam.TabIndex = 16;
            // 
            // btnThemDong
            // 
            this.btnThemDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(135)))), ((int)(((byte)(137)))));
            this.btnThemDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemDong.FlatAppearance.BorderSize = 0;
            this.btnThemDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnThemDong.Location = new System.Drawing.Point(380, 132);
            this.btnThemDong.Name = "btnThemDong";
            this.btnThemDong.Size = new System.Drawing.Size(100, 36);
            this.btnThemDong.TabIndex = 17;
            this.btnThemDong.Text = "Thêm dòng";
            this.btnThemDong.UseVisualStyleBackColor = false;
            this.btnThemDong.Click += new System.EventHandler(this.btnThemDong_Click);
            // 
            // btnXoaDong
            // 
            this.btnXoaDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.btnXoaDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaDong.FlatAppearance.BorderSize = 0;
            this.btnXoaDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnXoaDong.Location = new System.Drawing.Point(488, 132);
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.Size = new System.Drawing.Size(90, 36);
            this.btnXoaDong.TabIndex = 18;
            this.btnXoaDong.Text = "Xoá dòng";
            this.btnXoaDong.UseVisualStyleBackColor = false;
            this.btnXoaDong.Click += new System.EventHandler(this.btnXoaDong_Click);
            // 
            // cach1
            // 
            this.cach1.Dock = System.Windows.Forms.DockStyle.Top;
            this.cach1.Location = new System.Drawing.Point(0, 132);
            this.cach1.Name = "cach1";
            this.cach1.Size = new System.Drawing.Size(1262, 8);
            this.cach1.TabIndex = 4;
            // 
            // pnlThongTin
            // 
            this.pnlThongTin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
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
            this.pnlThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTin.Location = new System.Drawing.Point(0, 0);
            this.pnlThongTin.Name = "pnlThongTin";
            this.pnlThongTin.Size = new System.Drawing.Size(1262, 132);
            this.pnlThongTin.TabIndex = 5;
            // 
            // lblSoHD
            // 
            this.lblSoHD.AutoSize = true;
            this.lblSoHD.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoHD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblSoHD.Location = new System.Drawing.Point(16, 10);
            this.lblSoHD.Name = "lblSoHD";
            this.lblSoHD.Size = new System.Drawing.Size(105, 25);
            this.lblSoHD.TabIndex = 0;
            this.lblSoHD.Text = "Số hoá đơn";
            // 
            // txtSo
            // 
            this.txtSo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSo.Location = new System.Drawing.Point(16, 32);
            this.txtSo.Name = "txtSo";
            this.txtSo.ReadOnly = true;
            this.txtSo.Size = new System.Drawing.Size(250, 34);
            this.txtSo.TabIndex = 1;
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblNgay.Location = new System.Drawing.Point(290, 10);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(54, 25);
            this.lblNgay.TabIndex = 2;
            this.lblNgay.Text = "Ngày";
            // 
            // dtpNgay
            // 
            this.dtpNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpNgay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgay.Location = new System.Drawing.Point(290, 32);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(250, 34);
            this.dtpNgay.TabIndex = 3;
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblNhanVien.Location = new System.Drawing.Point(16, 70);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(104, 25);
            this.lblNhanVien.TabIndex = 4;
            this.lblNhanVien.Text = "Nhân viên *";
            // 
            // cboNV
            // 
            this.cboNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNV.FormattingEnabled = true;
            this.cboNV.Location = new System.Drawing.Point(16, 92);
            this.cboNV.Name = "cboNV";
            this.cboNV.Size = new System.Drawing.Size(250, 36);
            this.cboNV.TabIndex = 5;
            // 
            // lblDoiTac
            // 
            this.lblDoiTac.AutoSize = true;
            this.lblDoiTac.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoiTac.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblDoiTac.Location = new System.Drawing.Point(290, 70);
            this.lblDoiTac.Name = "lblDoiTac";
            this.lblDoiTac.Size = new System.Drawing.Size(81, 25);
            this.lblDoiTac.TabIndex = 6;
            this.lblDoiTac.Text = "Đối tác *";
            // 
            // cboDoiTac
            // 
            this.cboDoiTac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoiTac.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDoiTac.FormattingEnabled = true;
            this.cboDoiTac.Location = new System.Drawing.Point(290, 92);
            this.cboDoiTac.Name = "cboDoiTac";
            this.cboDoiTac.Size = new System.Drawing.Size(250, 36);
            this.cboDoiTac.TabIndex = 7;
            // 
            // lblMaKhach
            // 
            this.lblMaKhach.AutoSize = true;
            this.lblMaKhach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaKhach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblMaKhach.Location = new System.Drawing.Point(290, 70);
            this.lblMaKhach.Name = "lblMaKhach";
            this.lblMaKhach.Size = new System.Drawing.Size(133, 25);
            this.lblMaKhach.TabIndex = 8;
            this.lblMaKhach.Text = "Mã khách hàng";
            // 
            // txtMaKhach
            // 
            this.txtMaKhach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaKhach.Location = new System.Drawing.Point(290, 92);
            this.txtMaKhach.Name = "txtMaKhach";
            this.txtMaKhach.ReadOnly = true;
            this.txtMaKhach.Size = new System.Drawing.Size(120, 34);
            this.txtMaKhach.TabIndex = 9;
            // 
            // lblTenKhach
            // 
            this.lblTenKhach.AutoSize = true;
            this.lblTenKhach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenKhach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblTenKhach.Location = new System.Drawing.Point(420, 70);
            this.lblTenKhach.Name = "lblTenKhach";
            this.lblTenKhach.Size = new System.Drawing.Size(218, 25);
            this.lblTenKhach.TabIndex = 10;
            this.lblTenKhach.Text = "Tên khách hàng (tuỳ chọn)";
            // 
            // txtTenKhach
            // 
            this.txtTenKhach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenKhach.Location = new System.Drawing.Point(420, 92);
            this.txtTenKhach.MaxLength = 100;
            this.txtTenKhach.Name = "txtTenKhach";
            this.txtTenKhach.Size = new System.Drawing.Size(180, 34);
            this.txtTenKhach.TabIndex = 11;
            // 
            // lblDiaChiKhach
            // 
            this.lblDiaChiKhach.AutoSize = true;
            this.lblDiaChiKhach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiaChiKhach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblDiaChiKhach.Location = new System.Drawing.Point(605, 70);
            this.lblDiaChiKhach.Name = "lblDiaChiKhach";
            this.lblDiaChiKhach.Size = new System.Drawing.Size(149, 25);
            this.lblDiaChiKhach.TabIndex = 12;
            this.lblDiaChiKhach.Text = "Địa chỉ (tuỳ chọn)";
            // 
            // txtDiaChiKhach
            // 
            this.txtDiaChiKhach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiaChiKhach.Location = new System.Drawing.Point(605, 92);
            this.txtDiaChiKhach.MaxLength = 200;
            this.txtDiaChiKhach.Name = "txtDiaChiKhach";
            this.txtDiaChiKhach.Size = new System.Drawing.Size(130, 34);
            this.txtDiaChiKhach.TabIndex = 13;
            // 
            // lblDienThoaiKhach
            // 
            this.lblDienThoaiKhach.AutoSize = true;
            this.lblDienThoaiKhach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDienThoaiKhach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblDienThoaiKhach.Location = new System.Drawing.Point(750, 70);
            this.lblDienThoaiKhach.Name = "lblDienThoaiKhach";
            this.lblDienThoaiKhach.Size = new System.Drawing.Size(177, 25);
            this.lblDienThoaiKhach.TabIndex = 14;
            this.lblDienThoaiKhach.Text = "Điện thoại (tuỳ chọn)";
            // 
            // txtDienThoaiKhach
            // 
            this.txtDienThoaiKhach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDienThoaiKhach.Location = new System.Drawing.Point(750, 92);
            this.txtDienThoaiKhach.MaxLength = 11;
            this.txtDienThoaiKhach.Name = "txtDienThoaiKhach";
            this.txtDienThoaiKhach.Size = new System.Drawing.Size(130, 34);
            this.txtDienThoaiKhach.TabIndex = 15;
            // 
            // pnlTrai
            // 
            this.pnlTrai.Controls.Add(this.lvDs);
            this.pnlTrai.Controls.Add(this.pnlTraiTren);
            this.pnlTrai.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTrai.Location = new System.Drawing.Point(0, 0);
            this.pnlTrai.Name = "pnlTrai";
            this.pnlTrai.Padding = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.pnlTrai.Size = new System.Drawing.Size(356, 600);
            this.pnlTrai.TabIndex = 1;
            // 
            // lvDs
            // 
            this.lvDs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lvDs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvDs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDs.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvDs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lvDs.FullRowSelect = true;
            this.lvDs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDs.HideSelection = false;
            this.lvDs.Location = new System.Drawing.Point(0, 56);
            this.lvDs.MultiSelect = false;
            this.lvDs.Name = "lvDs";
            this.lvDs.Size = new System.Drawing.Size(340, 544);
            this.lvDs.TabIndex = 0;
            this.lvDs.UseCompatibleStateImageBehavior = false;
            this.lvDs.View = System.Windows.Forms.View.Details;
            this.lvDs.SelectedIndexChanged += new System.EventHandler(this.lvDs_SelectedIndexChanged);
            // 
            // pnlTraiTren
            // 
            this.pnlTraiTren.Controls.Add(this.btnMoi);
            this.pnlTraiTren.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTraiTren.Location = new System.Drawing.Point(0, 0);
            this.pnlTraiTren.Name = "pnlTraiTren";
            this.pnlTraiTren.Size = new System.Drawing.Size(340, 56);
            this.pnlTraiTren.TabIndex = 1;
            // 
            // btnMoi
            // 
            this.btnMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(135)))), ((int)(((byte)(137)))));
            this.btnMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoi.FlatAppearance.BorderSize = 0;
            this.btnMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMoi.Location = new System.Drawing.Point(0, 10);
            this.btnMoi.Name = "btnMoi";
            this.btnMoi.Size = new System.Drawing.Size(340, 36);
            this.btnMoi.TabIndex = 0;
            this.btnMoi.Text = "+  Hoá đơn mới";
            this.btnMoi.UseVisualStyleBackColor = false;
            this.btnMoi.Click += new System.EventHandler(this.btnMoi_Click);
            // 
            // FrmHoaDon
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1618, 600);
            this.Controls.Add(this.pnlPhai);
            this.Controls.Add(this.pnlTrai);
            this.Name = "FrmHoaDon";
            this.pnlPhai.ResumeLayout(false);
            this.pnlDuoi.ResumeLayout(false);
            this.pnlDuoi.PerformLayout();
            this.pnlNut.ResumeLayout(false);
            this.pnlNhapDong.ResumeLayout(false);
            this.pnlNhapDong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiam)).EndInit();
            this.pnlThongTin.ResumeLayout(false);
            this.pnlThongTin.PerformLayout();
            this.pnlTrai.ResumeLayout(false);
            this.pnlTraiTren.ResumeLayout(false);
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



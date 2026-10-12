namespace QuanLyBanTuBep.UI
{
    partial class FrmHangHoa
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
            this.lv = new System.Windows.Forms.ListView();
            this.pnlTim = new System.Windows.Forms.Panel();
            this.pnlPhai = new System.Windows.Forms.Panel();
            this.btnCsv = new System.Windows.Forms.Button();
            this.lblDem = new System.Windows.Forms.Label();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.lblTim = new System.Windows.Forms.Label();
            this.pnlNhap = new System.Windows.Forms.Panel();
            this.pnlNut = new System.Windows.Forms.Panel();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.pnlTruong = new System.Windows.Forms.Panel();
            this.lblMaHang = new System.Windows.Forms.Label();
            this.txtMaHang = new System.Windows.Forms.TextBox();
            this.lblTenHang = new System.Windows.Forms.Label();
            this.txtTenHang = new System.Windows.Forms.TextBox();
            this.lblMaKichThuoc = new System.Windows.Forms.Label();
            this.cboMaKichThuoc = new System.Windows.Forms.ComboBox();
            this.lblMaChatLieu = new System.Windows.Forms.Label();
            this.cboMaChatLieu = new System.Windows.Forms.ComboBox();
            this.lblMaMau = new System.Windows.Forms.Label();
            this.cboMaMau = new System.Windows.Forms.ComboBox();
            this.lblMaNuocSX = new System.Windows.Forms.Label();
            this.cboMaNuocSX = new System.Windows.Forms.ComboBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblGiaNhap = new System.Windows.Forms.Label();
            this.nudGiaNhap = new System.Windows.Forms.NumericUpDown();
            this.lblDonGiaBan = new System.Windows.Forms.Label();
            this.nudDonGiaBan = new System.Windows.Forms.NumericUpDown();
            this.lblThoiGianBaoHanh = new System.Windows.Forms.Label();
            this.txtThoiGianBaoHanh = new System.Windows.Forms.TextBox();
            this.lblAnh = new System.Windows.Forms.Label();
            this.txtAnh = new System.Windows.Forms.TextBox();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.picAnh = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiaNhap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDonGiaBan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).BeginInit();
            this.SuspendLayout();
            this.pnlTim.SuspendLayout();
            this.pnlPhai.SuspendLayout();
            this.pnlNhap.SuspendLayout();
            this.pnlNut.SuspendLayout();
            this.pnlTruong.SuspendLayout();

            this.lv.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.lv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lv.FullRowSelect = true;
            this.lv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv.HideSelection = false;
            this.lv.MultiSelect = false;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.Name = "lv";

            this.pnlTim.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTim.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.pnlTim.Size = new System.Drawing.Size(0, 56);
            this.pnlTim.Name = "pnlTim";
            this.pnlTim.Controls.Add(this.pnlPhai);
            this.pnlTim.Controls.Add(this.lblDem);
            this.pnlTim.Controls.Add(this.txtTim);
            this.pnlTim.Controls.Add(this.lblTim);

            this.pnlPhai.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPhai.Size = new System.Drawing.Size(150, 0);
            this.pnlPhai.Name = "pnlPhai";
            this.pnlPhai.Controls.Add(this.btnCsv);

            this.btnCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCsv.FlatAppearance.BorderSize = 0;
            this.btnCsv.BackColor = System.Drawing.Color.FromArgb(85, 211, 218);
            this.btnCsv.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnCsv.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCsv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCsv.Location = new System.Drawing.Point(30, 10);
            this.btnCsv.Size = new System.Drawing.Size(120, 36);
            this.btnCsv.Text = "Xuất CSV";
            this.btnCsv.UseVisualStyleBackColor = false;
            this.btnCsv.Name = "btnCsv";

            this.lblDem.AutoSize = true;
            this.lblDem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDem.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblDem.Location = new System.Drawing.Point(410, 19);
            this.lblDem.Text = "";
            this.lblDem.Name = "lblDem";

            this.txtTim.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTim.Location = new System.Drawing.Point(72, 14);
            this.txtTim.Size = new System.Drawing.Size(320, 29);
            this.txtTim.Name = "txtTim";

            this.lblTim.AutoSize = true;
            this.lblTim.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTim.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblTim.Location = new System.Drawing.Point(0, 19);
            this.lblTim.Text = "Tìm kiếm";
            this.lblTim.Name = "lblTim";

            this.pnlNhap.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNhap.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.pnlNhap.Size = new System.Drawing.Size(0, 332);
            this.pnlNhap.Name = "pnlNhap";
            this.pnlNhap.Controls.Add(this.pnlNut);
            this.pnlNhap.Controls.Add(this.pnlTruong);
            this.pnlNhap.Controls.Add(this.picAnh);

            this.pnlNut.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNut.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.pnlNut.Size = new System.Drawing.Size(0, 56);
            this.pnlNut.Name = "pnlNut";
            this.pnlNut.Controls.Add(this.btnThem);
            this.pnlNut.Controls.Add(this.btnSua);
            this.pnlNut.Controls.Add(this.btnXoa);
            this.pnlNut.Controls.Add(this.btnLamMoi);

            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(0, 135, 137);
            this.btnThem.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.Location = new System.Drawing.Point(16, 0);
            this.btnThem.Size = new System.Drawing.Size(120, 36);
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Name = "btnThem";

            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(50, 52, 77);
            this.btnSua.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.Location = new System.Drawing.Point(144, 0);
            this.btnSua.Size = new System.Drawing.Size(120, 36);
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Name = "btnSua";

            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnXoa.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.Location = new System.Drawing.Point(272, 0);
            this.btnXoa.Size = new System.Drawing.Size(120, 36);
            this.btnXoa.Text = "Xoá";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Name = "btnXoa";

            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.Location = new System.Drawing.Point(400, 0);
            this.btnLamMoi.Size = new System.Drawing.Size(120, 36);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Name = "btnLamMoi";

            this.pnlTruong.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTruong.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.pnlTruong.Size = new System.Drawing.Size(0, 276);
            this.pnlTruong.Name = "pnlTruong";
            this.pnlTruong.Controls.Add(this.lblMaHang);
            this.pnlTruong.Controls.Add(this.txtMaHang);
            this.pnlTruong.Controls.Add(this.lblTenHang);
            this.pnlTruong.Controls.Add(this.txtTenHang);
            this.pnlTruong.Controls.Add(this.lblMaKichThuoc);
            this.pnlTruong.Controls.Add(this.cboMaKichThuoc);
            this.pnlTruong.Controls.Add(this.lblMaChatLieu);
            this.pnlTruong.Controls.Add(this.cboMaChatLieu);
            this.pnlTruong.Controls.Add(this.lblMaMau);
            this.pnlTruong.Controls.Add(this.cboMaMau);
            this.pnlTruong.Controls.Add(this.lblMaNuocSX);
            this.pnlTruong.Controls.Add(this.cboMaNuocSX);
            this.pnlTruong.Controls.Add(this.lblSoLuong);
            this.pnlTruong.Controls.Add(this.nudSoLuong);
            this.pnlTruong.Controls.Add(this.lblGiaNhap);
            this.pnlTruong.Controls.Add(this.nudGiaNhap);
            this.pnlTruong.Controls.Add(this.lblDonGiaBan);
            this.pnlTruong.Controls.Add(this.nudDonGiaBan);
            this.pnlTruong.Controls.Add(this.lblThoiGianBaoHanh);
            this.pnlTruong.Controls.Add(this.txtThoiGianBaoHanh);
            this.pnlTruong.Controls.Add(this.lblAnh);
            this.pnlTruong.Controls.Add(this.txtAnh);
            this.pnlTruong.Controls.Add(this.btnChonAnh);
            this.pnlTruong.Controls.Add(this.lblGhiChu);
            this.pnlTruong.Controls.Add(this.txtGhiChu);

            this.lblMaHang.AutoSize = true;
            this.lblMaHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaHang.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblMaHang.Location = new System.Drawing.Point(16, 12);
            this.lblMaHang.Text = "Mã hàng *";
            this.lblMaHang.Name = "lblMaHang";

            this.txtMaHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaHang.Location = new System.Drawing.Point(16, 34);
            this.txtMaHang.TabIndex = 0;
            this.txtMaHang.Size = new System.Drawing.Size(234, 29);
            this.txtMaHang.MaxLength = 20;
            this.txtMaHang.Name = "txtMaHang";

            this.lblTenHang.AutoSize = true;
            this.lblTenHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenHang.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblTenHang.Location = new System.Drawing.Point(266, 12);
            this.lblTenHang.Text = "Tên hàng hoá *";
            this.lblTenHang.Name = "lblTenHang";

            this.txtTenHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenHang.Location = new System.Drawing.Point(266, 34);
            this.txtTenHang.TabIndex = 1;
            this.txtTenHang.Size = new System.Drawing.Size(234, 29);
            this.txtTenHang.MaxLength = 150;
            this.txtTenHang.Name = "txtTenHang";

            this.lblMaKichThuoc.AutoSize = true;
            this.lblMaKichThuoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaKichThuoc.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblMaKichThuoc.Location = new System.Drawing.Point(516, 12);
            this.lblMaKichThuoc.Text = "Kích thước *";
            this.lblMaKichThuoc.Name = "lblMaKichThuoc";

            this.cboMaKichThuoc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMaKichThuoc.Location = new System.Drawing.Point(516, 34);
            this.cboMaKichThuoc.TabIndex = 2;
            this.cboMaKichThuoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaKichThuoc.FormattingEnabled = true;
            this.cboMaKichThuoc.Size = new System.Drawing.Size(234, 29);
            this.cboMaKichThuoc.Name = "cboMaKichThuoc";

            this.lblMaChatLieu.AutoSize = true;
            this.lblMaChatLieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaChatLieu.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblMaChatLieu.Location = new System.Drawing.Point(16, 78);
            this.lblMaChatLieu.Text = "Chất liệu *";
            this.lblMaChatLieu.Name = "lblMaChatLieu";

            this.cboMaChatLieu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMaChatLieu.Location = new System.Drawing.Point(16, 100);
            this.cboMaChatLieu.TabIndex = 3;
            this.cboMaChatLieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaChatLieu.FormattingEnabled = true;
            this.cboMaChatLieu.Size = new System.Drawing.Size(234, 29);
            this.cboMaChatLieu.Name = "cboMaChatLieu";

            this.lblMaMau.AutoSize = true;
            this.lblMaMau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaMau.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblMaMau.Location = new System.Drawing.Point(266, 78);
            this.lblMaMau.Text = "Màu sắc *";
            this.lblMaMau.Name = "lblMaMau";

            this.cboMaMau.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMaMau.Location = new System.Drawing.Point(266, 100);
            this.cboMaMau.TabIndex = 4;
            this.cboMaMau.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaMau.FormattingEnabled = true;
            this.cboMaMau.Size = new System.Drawing.Size(234, 29);
            this.cboMaMau.Name = "cboMaMau";

            this.lblMaNuocSX.AutoSize = true;
            this.lblMaNuocSX.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaNuocSX.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblMaNuocSX.Location = new System.Drawing.Point(516, 78);
            this.lblMaNuocSX.Text = "Nước sản xuất *";
            this.lblMaNuocSX.Name = "lblMaNuocSX";

            this.cboMaNuocSX.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMaNuocSX.Location = new System.Drawing.Point(516, 100);
            this.cboMaNuocSX.TabIndex = 5;
            this.cboMaNuocSX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaNuocSX.FormattingEnabled = true;
            this.cboMaNuocSX.Size = new System.Drawing.Size(234, 29);
            this.cboMaNuocSX.Name = "cboMaNuocSX";

            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuong.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSoLuong.Location = new System.Drawing.Point(16, 144);
            this.lblSoLuong.Text = "Số lượng tồn (nhập khi thêm mới)";
            this.lblSoLuong.Name = "lblSoLuong";

            this.nudSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudSoLuong.Location = new System.Drawing.Point(16, 166);
            this.nudSoLuong.TabIndex = 6;
            this.nudSoLuong.Maximum = new decimal(new int[] { 1316134911, 2328, 0, 0 });
            this.nudSoLuong.Size = new System.Drawing.Size(234, 29);
            this.nudSoLuong.ThousandsSeparator = true;
            this.nudSoLuong.Name = "nudSoLuong";

            this.lblGiaNhap.AutoSize = true;
            this.lblGiaNhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaNhap.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblGiaNhap.Location = new System.Drawing.Point(266, 144);
            this.lblGiaNhap.Text = "Giá nhập (đồng)";
            this.lblGiaNhap.Name = "lblGiaNhap";

            this.nudGiaNhap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudGiaNhap.Location = new System.Drawing.Point(266, 166);
            this.nudGiaNhap.TabIndex = 7;
            this.nudGiaNhap.Maximum = new decimal(new int[] { 1316134911, 2328, 0, 0 });
            this.nudGiaNhap.Size = new System.Drawing.Size(234, 29);
            this.nudGiaNhap.ThousandsSeparator = true;
            this.nudGiaNhap.Name = "nudGiaNhap";

            this.lblDonGiaBan.AutoSize = true;
            this.lblDonGiaBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDonGiaBan.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblDonGiaBan.Location = new System.Drawing.Point(516, 144);
            this.lblDonGiaBan.Text = "Giá bán (tự động 110%)";
            this.lblDonGiaBan.Name = "lblDonGiaBan";

            this.nudDonGiaBan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudDonGiaBan.Location = new System.Drawing.Point(516, 166);
            this.nudDonGiaBan.TabIndex = 8;
            this.nudDonGiaBan.Maximum = new decimal(new int[] { 1316134911, 2328, 0, 0 });
            this.nudDonGiaBan.Size = new System.Drawing.Size(234, 29);
            this.nudDonGiaBan.ThousandsSeparator = true;
            this.nudDonGiaBan.Name = "nudDonGiaBan";

            this.lblThoiGianBaoHanh.AutoSize = true;
            this.lblThoiGianBaoHanh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThoiGianBaoHanh.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblThoiGianBaoHanh.Location = new System.Drawing.Point(16, 210);
            this.lblThoiGianBaoHanh.Text = "Thời gian bảo hành (vd: 24 tháng)";
            this.lblThoiGianBaoHanh.Name = "lblThoiGianBaoHanh";

            this.txtThoiGianBaoHanh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThoiGianBaoHanh.Location = new System.Drawing.Point(16, 232);
            this.txtThoiGianBaoHanh.TabIndex = 9;
            this.txtThoiGianBaoHanh.Size = new System.Drawing.Size(234, 29);
            this.txtThoiGianBaoHanh.MaxLength = 50;
            this.txtThoiGianBaoHanh.Name = "txtThoiGianBaoHanh";

            this.lblAnh.AutoSize = true;
            this.lblAnh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnh.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblAnh.Location = new System.Drawing.Point(266, 210);
            this.lblAnh.Text = "Ảnh sản phẩm";
            this.lblAnh.Name = "lblAnh";

            this.txtAnh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnh.Location = new System.Drawing.Point(266, 232);
            this.txtAnh.TabIndex = 10;
            this.txtAnh.ReadOnly = true;
            this.txtAnh.Size = new System.Drawing.Size(152, 29);
            this.txtAnh.Name = "txtAnh";

            this.btnChonAnh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChonAnh.FlatAppearance.BorderSize = 0;
            this.btnChonAnh.BackColor = System.Drawing.Color.FromArgb(0, 135, 137);
            this.btnChonAnh.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnChonAnh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChonAnh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChonAnh.Location = new System.Drawing.Point(424, 232);
            this.btnChonAnh.Size = new System.Drawing.Size(76, 29);
            this.btnChonAnh.Text = "Chọn...";
            this.btnChonAnh.UseVisualStyleBackColor = false;
            this.btnChonAnh.Name = "btnChonAnh";

            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblGhiChu.Location = new System.Drawing.Point(516, 210);
            this.lblGhiChu.Text = "Ghi chú";
            this.lblGhiChu.Name = "lblGhiChu";

            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGhiChu.Location = new System.Drawing.Point(516, 232);
            this.txtGhiChu.TabIndex = 11;
            this.txtGhiChu.Size = new System.Drawing.Size(234, 29);
            this.txtGhiChu.MaxLength = 500;
            this.txtGhiChu.Name = "txtGhiChu";

            this.picAnh.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.picAnh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAnh.Dock = System.Windows.Forms.DockStyle.Right;
            this.picAnh.Size = new System.Drawing.Size(180, 332);
            this.picAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAnh.TabStop = false;
            this.picAnh.Name = "picAnh";

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text = "Quản lý hàng hoá";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Controls.Add(this.lv);
            this.Controls.Add(this.pnlTim);
            this.Controls.Add(this.pnlNhap);
            this.Name = "FrmHangHoa";
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiaNhap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDonGiaBan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).EndInit();
            this.pnlTruong.ResumeLayout(false);
            this.pnlTruong.PerformLayout();
            this.pnlNut.ResumeLayout(false);
            this.pnlNut.PerformLayout();
            this.pnlNhap.ResumeLayout(false);
            this.pnlNhap.PerformLayout();
            this.pnlPhai.ResumeLayout(false);
            this.pnlPhai.PerformLayout();
            this.pnlTim.ResumeLayout(false);
            this.pnlTim.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.Panel pnlTim;
        private System.Windows.Forms.Panel pnlPhai;
        private System.Windows.Forms.Button btnCsv;
        private System.Windows.Forms.Label lblDem;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.Label lblTim;
        private System.Windows.Forms.Panel pnlNhap;
        private System.Windows.Forms.Panel pnlNut;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Panel pnlTruong;
        private System.Windows.Forms.Label lblMaHang;
        private System.Windows.Forms.TextBox txtMaHang;
        private System.Windows.Forms.Label lblTenHang;
        private System.Windows.Forms.TextBox txtTenHang;
        private System.Windows.Forms.Label lblMaKichThuoc;
        private System.Windows.Forms.ComboBox cboMaKichThuoc;
        private System.Windows.Forms.Label lblMaChatLieu;
        private System.Windows.Forms.ComboBox cboMaChatLieu;
        private System.Windows.Forms.Label lblMaMau;
        private System.Windows.Forms.ComboBox cboMaMau;
        private System.Windows.Forms.Label lblMaNuocSX;
        private System.Windows.Forms.ComboBox cboMaNuocSX;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Label lblGiaNhap;
        private System.Windows.Forms.NumericUpDown nudGiaNhap;
        private System.Windows.Forms.Label lblDonGiaBan;
        private System.Windows.Forms.NumericUpDown nudDonGiaBan;
        private System.Windows.Forms.Label lblThoiGianBaoHanh;
        private System.Windows.Forms.TextBox txtThoiGianBaoHanh;
        private System.Windows.Forms.Label lblAnh;
        private System.Windows.Forms.TextBox txtAnh;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.PictureBox picAnh;
    }
}

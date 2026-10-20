namespace QuanLyBanTuBep.UI
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.pnlNoiDung = new System.Windows.Forms.Panel();
            this.statusChinh = new System.Windows.Forms.StatusStrip();
            this.lblGoiY = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblGio = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.toolChinh = new System.Windows.Forms.ToolStrip();
            this.tsBanHang = new System.Windows.Forms.ToolStripButton();
            this.tsNhapHang = new System.Windows.Forms.ToolStripButton();
            this.tsHangHoa = new System.Windows.Forms.ToolStripButton();
            this.tsTimSanPham = new System.Windows.Forms.ToolStripButton();
            this.tsBaoCao = new System.Windows.Forms.ToolStripButton();
            this.menuChinh = new System.Windows.Forms.MenuStrip();
            this.mnuTrangChu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHangHoa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhaCungCap = new System.Windows.Forms.ToolStripMenuItem();
            this.sepDanhMuc = new System.Windows.Forms.ToolStripSeparator();
            this.mnuKichThuoc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChatLieu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMauSac = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNuocSanXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCongViec = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGiaoDich = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhapHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBanHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTraCuu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTimKiem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTimSanPham = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTimHoaDonBan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCao = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTop3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHoaDonNhapNV = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTop5HoaDonBan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKhachHangTheoThang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.timGio = new System.Windows.Forms.Timer(this.components);
            this.statusChinh.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.toolChinh.SuspendLayout();
            this.menuChinh.SuspendLayout();
            this.SuspendLayout();

            this.pnlNoiDung.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.pnlNoiDung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNoiDung.Name = "pnlNoiDung";
            this.pnlNoiDung.Padding = new System.Windows.Forms.Padding(16);
            this.pnlNoiDung.Size = new System.Drawing.Size(1280, 570);
            this.pnlNoiDung.TabIndex = 0;

            this.statusChinh.BackColor = System.Drawing.Color.FromArgb(50, 52, 77);
            this.statusChinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusChinh.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblGoiY,
                this.lblGio});
            this.statusChinh.Name = "statusChinh";
            this.statusChinh.SizingGrip = false;
            this.statusChinh.Size = new System.Drawing.Size(1280, 26);
            this.statusChinh.TabIndex = 1;

            this.lblGoiY.ForeColor = System.Drawing.Color.FromArgb(184, 187, 209);
            this.lblGoiY.Name = "lblGoiY";
            this.lblGoiY.Spring = true;
            this.lblGoiY.Text = "Phím tắt:  Ctrl+B  Bán hàng   |   Ctrl+N  Nhập hàng   |   Ctrl+H  Hàng hoá";
            this.lblGoiY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblGio.ForeColor = System.Drawing.Color.FromArgb(184, 187, 209);
            this.lblGio.Name = "lblGio";
            this.lblGio.Text = "";

            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(0, 135, 137);
            this.pnlHeader.Controls.Add(this.lblTieuDe);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1280, 56);
            this.pnlHeader.TabIndex = 2;

            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(253, 255, 255);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(1280, 56);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.toolChinh.BackColor = System.Drawing.Color.White;
            this.toolChinh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolChinh.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolChinh.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.tsBanHang,
                this.tsNhapHang,
                this.tsHangHoa,
                this.tsTimSanPham,
                this.tsBaoCao});
            this.toolChinh.Name = "toolChinh";
            this.toolChinh.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.toolChinh.Size = new System.Drawing.Size(1280, 40);
            this.toolChinh.TabIndex = 3;

            this.tsBanHang.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBanHang.Margin = new System.Windows.Forms.Padding(0, 1, 8, 2);
            this.tsBanHang.Name = "tsBanHang";
            this.tsBanHang.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.tsBanHang.Text = "Bán hàng";
            this.tsBanHang.Click += new System.EventHandler(this.mnuMuc_Click);

            this.tsNhapHang.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsNhapHang.Margin = new System.Windows.Forms.Padding(0, 1, 8, 2);
            this.tsNhapHang.Name = "tsNhapHang";
            this.tsNhapHang.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.tsNhapHang.Text = "Nhập hàng";
            this.tsNhapHang.Click += new System.EventHandler(this.mnuMuc_Click);

            this.tsHangHoa.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsHangHoa.Margin = new System.Windows.Forms.Padding(0, 1, 8, 2);
            this.tsHangHoa.Name = "tsHangHoa";
            this.tsHangHoa.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.tsHangHoa.Text = "Hàng hoá";
            this.tsHangHoa.Click += new System.EventHandler(this.mnuMuc_Click);

            this.tsTimSanPham.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsTimSanPham.Margin = new System.Windows.Forms.Padding(0, 1, 8, 2);
            this.tsTimSanPham.Name = "tsTimSanPham";
            this.tsTimSanPham.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.tsTimSanPham.Text = "Tìm sản phẩm";
            this.tsTimSanPham.Click += new System.EventHandler(this.mnuMuc_Click);

            this.tsBaoCao.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBaoCao.Margin = new System.Windows.Forms.Padding(0, 1, 8, 2);
            this.tsBaoCao.Name = "tsBaoCao";
            this.tsBaoCao.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.tsBaoCao.Text = "Báo cáo";
            this.tsBaoCao.Click += new System.EventHandler(this.mnuMuc_Click);

            this.menuChinh.BackColor = System.Drawing.Color.FromArgb(50, 52, 77);
            this.menuChinh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuChinh.ForeColor = System.Drawing.Color.FromArgb(184, 187, 209);
            this.menuChinh.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuTrangChu,
                this.mnuDanhMuc,
                this.mnuGiaoDich,
                this.mnuTraCuu,
                this.mnuHeThong});
            this.menuChinh.Name = "menuChinh";
            this.menuChinh.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.menuChinh.Size = new System.Drawing.Size(1280, 40);
            this.menuChinh.TabIndex = 4;
            this.menuChinh.Text = "menuChinh";

            this.mnuTrangChu.Name = "mnuTrangChu";
            this.mnuTrangChu.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.mnuTrangChu.Text = "Trang chủ";
            this.mnuTrangChu.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuHangHoa,
                this.mnuNhanVien,
                this.mnuKhachHang,
                this.mnuNhaCungCap,
                this.sepDanhMuc,
                this.mnuKichThuoc,
                this.mnuChatLieu,
                this.mnuMauSac,
                this.mnuNuocSanXuat,
                this.mnuCongViec});
            this.mnuDanhMuc.Name = "mnuDanhMuc";
            this.mnuDanhMuc.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.mnuDanhMuc.Text = "Danh mục";

            this.mnuHangHoa.Name = "mnuHangHoa";
            this.mnuHangHoa.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuHangHoa.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.mnuHangHoa.Text = "Hàng hoá";
            this.mnuHangHoa.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuNhanVien.Name = "mnuNhanVien";
            this.mnuNhanVien.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuNhanVien.Text = "Nhân viên";
            this.mnuNhanVien.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuKhachHang.Name = "mnuKhachHang";
            this.mnuKhachHang.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuKhachHang.Text = "Khách hàng";
            this.mnuKhachHang.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuNhaCungCap.Name = "mnuNhaCungCap";
            this.mnuNhaCungCap.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuNhaCungCap.Text = "Nhà cung cấp";
            this.mnuNhaCungCap.Click += new System.EventHandler(this.mnuMuc_Click);

            this.sepDanhMuc.Name = "sepDanhMuc";
            this.sepDanhMuc.Size = new System.Drawing.Size(250, 6);

            this.mnuKichThuoc.Name = "mnuKichThuoc";
            this.mnuKichThuoc.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuKichThuoc.Text = "Kích thước";
            this.mnuKichThuoc.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuChatLieu.Name = "mnuChatLieu";
            this.mnuChatLieu.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuChatLieu.Text = "Chất liệu";
            this.mnuChatLieu.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuMauSac.Name = "mnuMauSac";
            this.mnuMauSac.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuMauSac.Text = "Màu sắc";
            this.mnuMauSac.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuNuocSanXuat.Name = "mnuNuocSanXuat";
            this.mnuNuocSanXuat.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuNuocSanXuat.Text = "Nước sản xuất";
            this.mnuNuocSanXuat.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuCongViec.Name = "mnuCongViec";
            this.mnuCongViec.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuCongViec.Text = "Công việc";
            this.mnuCongViec.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuGiaoDich.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuNhapHang,
                this.mnuBanHang});
            this.mnuGiaoDich.Name = "mnuGiaoDich";
            this.mnuGiaoDich.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.mnuGiaoDich.Text = "Giao dịch";

            this.mnuNhapHang.Name = "mnuNhapHang";
            this.mnuNhapHang.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuNhapHang.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuNhapHang.Text = "Nhập hàng";
            this.mnuNhapHang.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuBanHang.Name = "mnuBanHang";
            this.mnuBanHang.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuBanHang.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.mnuBanHang.Text = "Bán hàng";
            this.mnuBanHang.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuTraCuu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuTimKiem,
                this.mnuBaoCao});
            this.mnuTraCuu.Name = "mnuTraCuu";
            this.mnuTraCuu.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.mnuTraCuu.Text = "Tra cứu";

            this.mnuTimKiem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuTimSanPham,
                this.mnuTimHoaDonBan});
            this.mnuTimKiem.Name = "mnuTimKiem";
            this.mnuTimKiem.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuTimKiem.Text = "Tìm kiếm";

            this.mnuTimSanPham.Name = "mnuTimSanPham";
            this.mnuTimSanPham.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuTimSanPham.Text = "Sản phẩm";
            this.mnuTimSanPham.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuTimHoaDonBan.Name = "mnuTimHoaDonBan";
            this.mnuTimHoaDonBan.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuTimHoaDonBan.Text = "Hoá đơn bán";
            this.mnuTimHoaDonBan.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuBaoCao.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuTop3,
                this.mnuHoaDonNhapNV,
                this.mnuTop5HoaDonBan,
                this.mnuKhachHangTheoThang});
            this.mnuBaoCao.Name = "mnuBaoCao";
            this.mnuBaoCao.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuBaoCao.Text = "Báo cáo";

            this.mnuTop3.Name = "mnuTop3";
            this.mnuTop3.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuTop3.Text = "Top 3 sản phẩm bán chạy / quý";
            this.mnuTop3.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuHoaDonNhapNV.Name = "mnuHoaDonNhapNV";
            this.mnuHoaDonNhapNV.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuHoaDonNhapNV.Text = "Hoá đơn nhập theo nhân viên";
            this.mnuHoaDonNhapNV.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuTop5HoaDonBan.Name = "mnuTop5HoaDonBan";
            this.mnuTop5HoaDonBan.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuTop5HoaDonBan.Text = "5 hoá đơn bán thấp nhất / quý";
            this.mnuTop5HoaDonBan.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuKhachHangTheoThang.Name = "mnuKhachHangTheoThang";
            this.mnuKhachHangTheoThang.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuKhachHangTheoThang.Text = "Khách hàng mua theo tháng";
            this.mnuKhachHangTheoThang.Click += new System.EventHandler(this.mnuMuc_Click);

            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuThoat});
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.mnuHeThong.Text = "Hệ thống";

            this.mnuThoat.Name = "mnuThoat";
            this.mnuThoat.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mnuThoat.Text = "Thoát";
            this.mnuThoat.ShortcutKeyDisplayString = "Alt+F4";
            this.mnuThoat.Click += new System.EventHandler(this.mnuThoat_Click);

            this.timGio.Enabled = true;
            this.timGio.Interval = 1000;
            this.timGio.Tick += new System.EventHandler(this.timGio_Tick);

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.pnlNoiDung);
            this.Controls.Add(this.statusChinh);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.toolChinh);
            this.Controls.Add(this.menuChinh);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuChinh;
            this.MinimumSize = new System.Drawing.Size(1200, 720);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý bán tủ bếp";
            this.statusChinh.ResumeLayout(false);
            this.statusChinh.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.toolChinh.ResumeLayout(false);
            this.toolChinh.PerformLayout();
            this.menuChinh.ResumeLayout(false);
            this.menuChinh.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlNoiDung;
        private System.Windows.Forms.StatusStrip statusChinh;
        private System.Windows.Forms.ToolStripStatusLabel lblGoiY;
        private System.Windows.Forms.ToolStripStatusLabel lblGio;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.ToolStrip toolChinh;
        private System.Windows.Forms.ToolStripButton tsBanHang;
        private System.Windows.Forms.ToolStripButton tsNhapHang;
        private System.Windows.Forms.ToolStripButton tsHangHoa;
        private System.Windows.Forms.ToolStripButton tsTimSanPham;
        private System.Windows.Forms.ToolStripButton tsBaoCao;
        private System.Windows.Forms.MenuStrip menuChinh;
        private System.Windows.Forms.ToolStripMenuItem mnuTrangChu;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem mnuHangHoa;
        private System.Windows.Forms.ToolStripMenuItem mnuNhanVien;
        private System.Windows.Forms.ToolStripMenuItem mnuKhachHang;
        private System.Windows.Forms.ToolStripMenuItem mnuNhaCungCap;
        private System.Windows.Forms.ToolStripSeparator sepDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem mnuKichThuoc;
        private System.Windows.Forms.ToolStripMenuItem mnuChatLieu;
        private System.Windows.Forms.ToolStripMenuItem mnuMauSac;
        private System.Windows.Forms.ToolStripMenuItem mnuNuocSanXuat;
        private System.Windows.Forms.ToolStripMenuItem mnuCongViec;
        private System.Windows.Forms.ToolStripMenuItem mnuGiaoDich;
        private System.Windows.Forms.ToolStripMenuItem mnuNhapHang;
        private System.Windows.Forms.ToolStripMenuItem mnuBanHang;
        private System.Windows.Forms.ToolStripMenuItem mnuTraCuu;
        private System.Windows.Forms.ToolStripMenuItem mnuTimKiem;
        private System.Windows.Forms.ToolStripMenuItem mnuTimSanPham;
        private System.Windows.Forms.ToolStripMenuItem mnuTimHoaDonBan;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCao;
        private System.Windows.Forms.ToolStripMenuItem mnuTop3;
        private System.Windows.Forms.ToolStripMenuItem mnuHoaDonNhapNV;
        private System.Windows.Forms.ToolStripMenuItem mnuTop5HoaDonBan;
        private System.Windows.Forms.ToolStripMenuItem mnuKhachHangTheoThang;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuThoat;
        private System.Windows.Forms.Timer timGio;
    }
}

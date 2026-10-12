namespace QuanLyBanTuBep.UI
{
    partial class FrmTimHoaDonBan
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
            this.pnlCach = new System.Windows.Forms.Panel();
            this.pnlLoc = new System.Windows.Forms.Panel();
            this.lblMaHang = new System.Windows.Forms.Label();
            this.cboMaHang = new System.Windows.Forms.ComboBox();
            this.lblNgayBan = new System.Windows.Forms.Label();
            this.dtpNgayBan = new System.Windows.Forms.DateTimePicker();
            this.lblTongTienTu = new System.Windows.Forms.Label();
            this.txtTongTienTu = new System.Windows.Forms.TextBox();
            this.lblTongTienDen = new System.Windows.Forms.Label();
            this.txtTongTienDen = new System.Windows.Forms.TextBox();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnCsv = new System.Windows.Forms.Button();
            this.lblKetQua = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this.pnlLoc.SuspendLayout();

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

            this.pnlCach.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCach.Size = new System.Drawing.Size(0, 12);
            this.pnlCach.Name = "pnlCach";

            this.pnlLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLoc.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.pnlLoc.Size = new System.Drawing.Size(0, 110);
            this.pnlLoc.Name = "pnlLoc";
            this.pnlLoc.Controls.Add(this.lblMaHang);
            this.pnlLoc.Controls.Add(this.cboMaHang);
            this.pnlLoc.Controls.Add(this.lblNgayBan);
            this.pnlLoc.Controls.Add(this.dtpNgayBan);
            this.pnlLoc.Controls.Add(this.lblTongTienTu);
            this.pnlLoc.Controls.Add(this.txtTongTienTu);
            this.pnlLoc.Controls.Add(this.lblTongTienDen);
            this.pnlLoc.Controls.Add(this.txtTongTienDen);
            this.pnlLoc.Controls.Add(this.btnXem);
            this.pnlLoc.Controls.Add(this.btnCsv);
            this.pnlLoc.Controls.Add(this.lblKetQua);

            this.lblMaHang.AutoSize = true;
            this.lblMaHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaHang.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblMaHang.Location = new System.Drawing.Point(16, 12);
            this.lblMaHang.Text = "Mã hàng";
            this.lblMaHang.Name = "lblMaHang";

            this.cboMaHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMaHang.Location = new System.Drawing.Point(16, 34);
            this.cboMaHang.TabIndex = 0;
            this.cboMaHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaHang.FormattingEnabled = true;
            this.cboMaHang.Size = new System.Drawing.Size(240, 29);
            this.cboMaHang.Name = "cboMaHang";

            this.lblNgayBan.AutoSize = true;
            this.lblNgayBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayBan.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblNgayBan.Location = new System.Drawing.Point(272, 12);
            this.lblNgayBan.Text = "Ngày bán (tick để lọc)";
            this.lblNgayBan.Name = "lblNgayBan";

            this.dtpNgayBan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayBan.Location = new System.Drawing.Point(272, 34);
            this.dtpNgayBan.TabIndex = 1;
            this.dtpNgayBan.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayBan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayBan.ShowCheckBox = true;
            this.dtpNgayBan.Checked = false;
            this.dtpNgayBan.Size = new System.Drawing.Size(170, 29);
            this.dtpNgayBan.Name = "dtpNgayBan";

            this.lblTongTienTu.AutoSize = true;
            this.lblTongTienTu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTienTu.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblTongTienTu.Location = new System.Drawing.Point(458, 12);
            this.lblTongTienTu.Text = "Tổng tiền từ";
            this.lblTongTienTu.Name = "lblTongTienTu";

            this.txtTongTienTu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongTienTu.Location = new System.Drawing.Point(458, 34);
            this.txtTongTienTu.TabIndex = 2;
            this.txtTongTienTu.Size = new System.Drawing.Size(150, 29);
            this.txtTongTienTu.Name = "txtTongTienTu";

            this.lblTongTienDen.AutoSize = true;
            this.lblTongTienDen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTienDen.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblTongTienDen.Location = new System.Drawing.Point(624, 12);
            this.lblTongTienDen.Text = "Tổng tiền đến";
            this.lblTongTienDen.Name = "lblTongTienDen";

            this.txtTongTienDen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongTienDen.Location = new System.Drawing.Point(624, 34);
            this.txtTongTienDen.TabIndex = 3;
            this.txtTongTienDen.Size = new System.Drawing.Size(150, 29);
            this.txtTongTienDen.Name = "txtTongTienDen";

            this.btnXem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXem.FlatAppearance.BorderSize = 0;
            this.btnXem.BackColor = System.Drawing.Color.FromArgb(0, 135, 137);
            this.btnXem.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnXem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXem.Location = new System.Drawing.Point(16, 66);
            this.btnXem.Size = new System.Drawing.Size(130, 36);
            this.btnXem.Text = "Xem kết quả";
            this.btnXem.UseVisualStyleBackColor = false;
            this.btnXem.Name = "btnXem";

            this.btnCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCsv.FlatAppearance.BorderSize = 0;
            this.btnCsv.BackColor = System.Drawing.Color.FromArgb(85, 211, 218);
            this.btnCsv.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnCsv.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCsv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCsv.Location = new System.Drawing.Point(154, 66);
            this.btnCsv.Size = new System.Drawing.Size(110, 36);
            this.btnCsv.Text = "Xuất CSV";
            this.btnCsv.UseVisualStyleBackColor = false;
            this.btnCsv.Name = "btnCsv";

            this.lblKetQua.AutoSize = true;
            this.lblKetQua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKetQua.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblKetQua.Location = new System.Drawing.Point(278, 76);
            this.lblKetQua.Text = "";
            this.lblKetQua.Name = "lblKetQua";

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text = "Tìm kiếm hoá đơn bán";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Controls.Add(this.lv);
            this.Controls.Add(this.pnlCach);
            this.Controls.Add(this.pnlLoc);
            this.Name = "FrmTimHoaDonBan";
            this.pnlLoc.ResumeLayout(false);
            this.pnlLoc.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.Panel pnlCach;
        private System.Windows.Forms.Panel pnlLoc;
        private System.Windows.Forms.Label lblMaHang;
        private System.Windows.Forms.ComboBox cboMaHang;
        private System.Windows.Forms.Label lblNgayBan;
        private System.Windows.Forms.DateTimePicker dtpNgayBan;
        private System.Windows.Forms.Label lblTongTienTu;
        private System.Windows.Forms.TextBox txtTongTienTu;
        private System.Windows.Forms.Label lblTongTienDen;
        private System.Windows.Forms.TextBox txtTongTienDen;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnCsv;
        private System.Windows.Forms.Label lblKetQua;
    }
}

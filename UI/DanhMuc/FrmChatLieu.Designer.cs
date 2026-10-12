namespace QuanLyBanTuBep.UI
{
    partial class FrmChatLieu
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
            this.lblMaChatLieu = new System.Windows.Forms.Label();
            this.txtMaChatLieu = new System.Windows.Forms.TextBox();
            this.lblTenChatLieu = new System.Windows.Forms.Label();
            this.txtTenChatLieu = new System.Windows.Forms.TextBox();
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
            this.pnlNhap.Size = new System.Drawing.Size(0, 134);
            this.pnlNhap.Name = "pnlNhap";
            this.pnlNhap.Controls.Add(this.pnlNut);
            this.pnlNhap.Controls.Add(this.pnlTruong);

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
            this.pnlTruong.Size = new System.Drawing.Size(0, 78);
            this.pnlTruong.Name = "pnlTruong";
            this.pnlTruong.Controls.Add(this.lblMaChatLieu);
            this.pnlTruong.Controls.Add(this.txtMaChatLieu);
            this.pnlTruong.Controls.Add(this.lblTenChatLieu);
            this.pnlTruong.Controls.Add(this.txtTenChatLieu);

            this.lblMaChatLieu.AutoSize = true;
            this.lblMaChatLieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaChatLieu.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblMaChatLieu.Location = new System.Drawing.Point(16, 12);
            this.lblMaChatLieu.Text = "Mã chất liệu *";
            this.lblMaChatLieu.Name = "lblMaChatLieu";

            this.txtMaChatLieu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaChatLieu.Location = new System.Drawing.Point(16, 34);
            this.txtMaChatLieu.TabIndex = 0;
            this.txtMaChatLieu.Size = new System.Drawing.Size(264, 29);
            this.txtMaChatLieu.MaxLength = 20;
            this.txtMaChatLieu.Name = "txtMaChatLieu";

            this.lblTenChatLieu.AutoSize = true;
            this.lblTenChatLieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenChatLieu.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblTenChatLieu.Location = new System.Drawing.Point(296, 12);
            this.lblTenChatLieu.Text = "Tên chất liệu *";
            this.lblTenChatLieu.Name = "lblTenChatLieu";

            this.txtTenChatLieu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenChatLieu.Location = new System.Drawing.Point(296, 34);
            this.txtTenChatLieu.TabIndex = 1;
            this.txtTenChatLieu.Size = new System.Drawing.Size(264, 29);
            this.txtTenChatLieu.MaxLength = 50;
            this.txtTenChatLieu.Name = "txtTenChatLieu";

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text = "Chất liệu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Controls.Add(this.lv);
            this.Controls.Add(this.pnlTim);
            this.Controls.Add(this.pnlNhap);
            this.Name = "FrmChatLieu";
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
        private System.Windows.Forms.Label lblMaChatLieu;
        private System.Windows.Forms.TextBox txtMaChatLieu;
        private System.Windows.Forms.Label lblTenChatLieu;
        private System.Windows.Forms.TextBox txtTenChatLieu;
    }
}

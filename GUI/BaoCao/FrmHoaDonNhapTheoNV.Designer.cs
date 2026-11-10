namespace QuanLyBanTuBep.UI
{
    partial class FrmHoaDonNhapTheoNV
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
            this.lblMaNV = new System.Windows.Forms.Label();
            this.cboMaNV = new System.Windows.Forms.ComboBox();
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
            this.pnlLoc.Controls.Add(this.lblMaNV);
            this.pnlLoc.Controls.Add(this.cboMaNV);
            this.pnlLoc.Controls.Add(this.btnXem);
            this.pnlLoc.Controls.Add(this.btnCsv);
            this.pnlLoc.Controls.Add(this.lblKetQua);

            this.lblMaNV.AutoSize = true;
            this.lblMaNV.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaNV.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblMaNV.Location = new System.Drawing.Point(16, 12);
            this.lblMaNV.Text = "Nhân viên nhập";
            this.lblMaNV.Name = "lblMaNV";

            this.cboMaNV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMaNV.Location = new System.Drawing.Point(16, 34);
            this.cboMaNV.TabIndex = 0;
            this.cboMaNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaNV.FormattingEnabled = true;
            this.cboMaNV.Size = new System.Drawing.Size(240, 29);
            this.cboMaNV.Name = "cboMaNV";

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
            this.Text = "Hoá đơn nhập theo nhân viên";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Controls.Add(this.lv);
            this.Controls.Add(this.pnlCach);
            this.Controls.Add(this.pnlLoc);
            this.Name = "FrmHoaDonNhapTheoNV";
            this.pnlLoc.ResumeLayout(false);
            this.pnlLoc.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.Panel pnlCach;
        private System.Windows.Forms.Panel pnlLoc;
        private System.Windows.Forms.Label lblMaNV;
        private System.Windows.Forms.ComboBox cboMaNV;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnCsv;
        private System.Windows.Forms.Label lblKetQua;
    }
}

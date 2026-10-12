namespace QuanLyBanTuBep.UI
{
    partial class FrmKhachHangTheoThang
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
            this.lblNam = new System.Windows.Forms.Label();
            this.nudNam = new System.Windows.Forms.NumericUpDown();
            this.lblThang = new System.Windows.Forms.Label();
            this.cboThang = new System.Windows.Forms.ComboBox();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnCsv = new System.Windows.Forms.Button();
            this.lblKetQua = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).BeginInit();
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
            this.pnlLoc.Controls.Add(this.lblNam);
            this.pnlLoc.Controls.Add(this.nudNam);
            this.pnlLoc.Controls.Add(this.lblThang);
            this.pnlLoc.Controls.Add(this.cboThang);
            this.pnlLoc.Controls.Add(this.btnXem);
            this.pnlLoc.Controls.Add(this.btnCsv);
            this.pnlLoc.Controls.Add(this.lblKetQua);

            this.lblNam.AutoSize = true;
            this.lblNam.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNam.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblNam.Location = new System.Drawing.Point(16, 12);
            this.lblNam.Text = "Năm";
            this.lblNam.Name = "lblNam";

            this.nudNam.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudNam.Location = new System.Drawing.Point(16, 34);
            this.nudNam.TabIndex = 0;
            this.nudNam.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            this.nudNam.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
            this.nudNam.Size = new System.Drawing.Size(90, 29);
            this.nudNam.Value = new decimal(new int[] { 2026, 0, 0, 0 });
            this.nudNam.Name = "nudNam";

            this.lblThang.AutoSize = true;
            this.lblThang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThang.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblThang.Location = new System.Drawing.Point(122, 12);
            this.lblThang.Text = "Tháng";
            this.lblThang.Name = "lblThang";

            this.cboThang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboThang.Location = new System.Drawing.Point(122, 34);
            this.cboThang.TabIndex = 1;
            this.cboThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThang.FormattingEnabled = true;
            this.cboThang.Items.AddRange(new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
            this.cboThang.SelectedIndex = 0;
            this.cboThang.Size = new System.Drawing.Size(80, 29);
            this.cboThang.Name = "cboThang";

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
            this.Text = "Khách hàng mua theo tháng";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Controls.Add(this.lv);
            this.Controls.Add(this.pnlCach);
            this.Controls.Add(this.pnlLoc);
            this.Name = "FrmKhachHangTheoThang";
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).EndInit();
            this.pnlLoc.ResumeLayout(false);
            this.pnlLoc.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.Panel pnlCach;
        private System.Windows.Forms.Panel pnlLoc;
        private System.Windows.Forms.Label lblNam;
        private System.Windows.Forms.NumericUpDown nudNam;
        private System.Windows.Forms.Label lblThang;
        private System.Windows.Forms.ComboBox cboThang;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnCsv;
        private System.Windows.Forms.Label lblKetQua;
    }
}

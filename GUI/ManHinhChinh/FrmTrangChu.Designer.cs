namespace QuanLyBanTuBep.UI
{
    partial class FrmTrangChu
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
            this.tlpDuoi = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSapHet = new System.Windows.Forms.Panel();
            this.lvSapHet = new System.Windows.Forms.ListView();
            this.lblTieuDeSapHet = new System.Windows.Forms.Label();
            this.pnlGanDay = new System.Windows.Forms.Panel();
            this.lvGanDay = new System.Windows.Forms.ListView();
            this.lblTieuDeGanDay = new System.Windows.Forms.Label();
            this.pnlCach = new System.Windows.Forms.Panel();
            this.tlpThe = new System.Windows.Forms.TableLayoutPanel();
            this.pnlThe1 = new System.Windows.Forms.Panel();
            this.lblThe1 = new System.Windows.Forms.Label();
            this.lblSoMatHang = new System.Windows.Forms.Label();
            this.pnlThe2 = new System.Windows.Forms.Panel();
            this.lblThe2 = new System.Windows.Forms.Label();
            this.lblSoTonKho = new System.Windows.Forms.Label();
            this.pnlThe3 = new System.Windows.Forms.Panel();
            this.lblThe3 = new System.Windows.Forms.Label();
            this.lblSoHoaDon = new System.Windows.Forms.Label();
            this.pnlThe4 = new System.Windows.Forms.Panel();
            this.lblThe4 = new System.Windows.Forms.Label();
            this.lblSoDoanhThu = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this.tlpDuoi.SuspendLayout();
            this.pnlSapHet.SuspendLayout();
            this.pnlGanDay.SuspendLayout();
            this.tlpThe.SuspendLayout();
            this.pnlThe1.SuspendLayout();
            this.pnlThe2.SuspendLayout();
            this.pnlThe3.SuspendLayout();
            this.pnlThe4.SuspendLayout();

            this.tlpDuoi.ColumnCount = 2;
            this.tlpDuoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDuoi.RowCount = 1;
            this.tlpDuoi.Name = "tlpDuoi";
            this.tlpDuoi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDuoi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDuoi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDuoi.Controls.Add(this.pnlSapHet, 0, 0);
            this.tlpDuoi.Controls.Add(this.pnlGanDay, 1, 0);

            this.pnlSapHet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSapHet.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.pnlSapHet.Name = "pnlSapHet";
            this.pnlSapHet.Controls.Add(this.lvSapHet);
            this.pnlSapHet.Controls.Add(this.lblTieuDeSapHet);

            this.lvSapHet.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.lvSapHet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvSapHet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSapHet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSapHet.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lvSapHet.FullRowSelect = true;
            this.lvSapHet.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSapHet.HideSelection = false;
            this.lvSapHet.MultiSelect = false;
            this.lvSapHet.UseCompatibleStateImageBehavior = false;
            this.lvSapHet.View = System.Windows.Forms.View.Details;
            this.lvSapHet.Name = "lvSapHet";

            this.lblTieuDeSapHet.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeSapHet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDeSapHet.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTieuDeSapHet.Size = new System.Drawing.Size(0, 32);
            this.lblTieuDeSapHet.Text = "Hàng sắp hết";
            this.lblTieuDeSapHet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTieuDeSapHet.Name = "lblTieuDeSapHet";

            this.pnlGanDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGanDay.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.pnlGanDay.Name = "pnlGanDay";
            this.pnlGanDay.Controls.Add(this.lvGanDay);
            this.pnlGanDay.Controls.Add(this.lblTieuDeGanDay);

            this.lvGanDay.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.lvGanDay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvGanDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGanDay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvGanDay.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lvGanDay.FullRowSelect = true;
            this.lvGanDay.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvGanDay.HideSelection = false;
            this.lvGanDay.MultiSelect = false;
            this.lvGanDay.UseCompatibleStateImageBehavior = false;
            this.lvGanDay.View = System.Windows.Forms.View.Details;
            this.lvGanDay.Name = "lvGanDay";

            this.lblTieuDeGanDay.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeGanDay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDeGanDay.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTieuDeGanDay.Size = new System.Drawing.Size(0, 32);
            this.lblTieuDeGanDay.Text = "Hoá đơn bán gần đây";
            this.lblTieuDeGanDay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTieuDeGanDay.Name = "lblTieuDeGanDay";

            this.pnlCach.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCach.Size = new System.Drawing.Size(0, 16);
            this.pnlCach.Name = "pnlCach";

            this.tlpThe.ColumnCount = 4;
            this.tlpThe.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpThe.RowCount = 1;
            this.tlpThe.Size = new System.Drawing.Size(0, 104);
            this.tlpThe.Name = "tlpThe";
            this.tlpThe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpThe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpThe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpThe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpThe.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpThe.Controls.Add(this.pnlThe1, 0, 0);
            this.tlpThe.Controls.Add(this.pnlThe2, 1, 0);
            this.tlpThe.Controls.Add(this.pnlThe3, 2, 0);
            this.tlpThe.Controls.Add(this.pnlThe4, 3, 0);

            this.pnlThe1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlThe1.BackColor = System.Drawing.Color.FromArgb(0, 135, 137);
            this.pnlThe1.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.pnlThe1.Name = "pnlThe1";
            this.pnlThe1.Controls.Add(this.lblThe1);
            this.pnlThe1.Controls.Add(this.lblSoMatHang);

            this.lblThe1.AutoSize = true;
            this.lblThe1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThe1.ForeColor = System.Drawing.Color.FromArgb(184, 187, 209);
            this.lblThe1.Location = new System.Drawing.Point(16, 14);
            this.lblThe1.Text = "Số mặt hàng";
            this.lblThe1.Name = "lblThe1";

            this.lblSoMatHang.AutoSize = true;
            this.lblSoMatHang.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoMatHang.ForeColor = System.Drawing.Color.FromArgb(253, 255, 255);
            this.lblSoMatHang.Location = new System.Drawing.Point(16, 44);
            this.lblSoMatHang.Text = "-";
            this.lblSoMatHang.Name = "lblSoMatHang";

            this.pnlThe2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlThe2.BackColor = System.Drawing.Color.FromArgb(50, 52, 77);
            this.pnlThe2.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.pnlThe2.Name = "pnlThe2";
            this.pnlThe2.Controls.Add(this.lblThe2);
            this.pnlThe2.Controls.Add(this.lblSoTonKho);

            this.lblThe2.AutoSize = true;
            this.lblThe2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThe2.ForeColor = System.Drawing.Color.FromArgb(184, 187, 209);
            this.lblThe2.Location = new System.Drawing.Point(16, 14);
            this.lblThe2.Text = "Tổng tồn kho";
            this.lblThe2.Name = "lblThe2";

            this.lblSoTonKho.AutoSize = true;
            this.lblSoTonKho.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoTonKho.ForeColor = System.Drawing.Color.FromArgb(253, 255, 255);
            this.lblSoTonKho.Location = new System.Drawing.Point(16, 44);
            this.lblSoTonKho.Text = "-";
            this.lblSoTonKho.Name = "lblSoTonKho";

            this.pnlThe3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlThe3.BackColor = System.Drawing.Color.FromArgb(0, 135, 137);
            this.pnlThe3.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.pnlThe3.Name = "pnlThe3";
            this.pnlThe3.Controls.Add(this.lblThe3);
            this.pnlThe3.Controls.Add(this.lblSoHoaDon);

            this.lblThe3.AutoSize = true;
            this.lblThe3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThe3.ForeColor = System.Drawing.Color.FromArgb(184, 187, 209);
            this.lblThe3.Location = new System.Drawing.Point(16, 14);
            this.lblThe3.Text = "Số hoá đơn bán";
            this.lblThe3.Name = "lblThe3";

            this.lblSoHoaDon.AutoSize = true;
            this.lblSoHoaDon.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoHoaDon.ForeColor = System.Drawing.Color.FromArgb(253, 255, 255);
            this.lblSoHoaDon.Location = new System.Drawing.Point(16, 44);
            this.lblSoHoaDon.Text = "-";
            this.lblSoHoaDon.Name = "lblSoHoaDon";

            this.pnlThe4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlThe4.BackColor = System.Drawing.Color.FromArgb(50, 52, 77);
            this.pnlThe4.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.pnlThe4.Name = "pnlThe4";
            this.pnlThe4.Controls.Add(this.lblThe4);
            this.pnlThe4.Controls.Add(this.lblSoDoanhThu);

            this.lblThe4.AutoSize = true;
            this.lblThe4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThe4.ForeColor = System.Drawing.Color.FromArgb(184, 187, 209);
            this.lblThe4.Location = new System.Drawing.Point(16, 14);
            this.lblThe4.Text = "Tổng doanh thu (đ)";
            this.lblThe4.Name = "lblThe4";

            this.lblSoDoanhThu.AutoSize = true;
            this.lblSoDoanhThu.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoDoanhThu.ForeColor = System.Drawing.Color.FromArgb(253, 255, 255);
            this.lblSoDoanhThu.Location = new System.Drawing.Point(16, 44);
            this.lblSoDoanhThu.Text = "-";
            this.lblSoDoanhThu.Name = "lblSoDoanhThu";

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.tlpDuoi);
            this.Controls.Add(this.pnlCach);
            this.Controls.Add(this.tlpThe);
            this.Name = "FrmTrangChu";
            this.pnlThe4.ResumeLayout(false);
            this.pnlThe4.PerformLayout();
            this.pnlThe3.ResumeLayout(false);
            this.pnlThe3.PerformLayout();
            this.pnlThe2.ResumeLayout(false);
            this.pnlThe2.PerformLayout();
            this.pnlThe1.ResumeLayout(false);
            this.pnlThe1.PerformLayout();
            this.tlpThe.ResumeLayout(false);
            this.tlpThe.PerformLayout();
            this.pnlGanDay.ResumeLayout(false);
            this.pnlGanDay.PerformLayout();
            this.pnlSapHet.ResumeLayout(false);
            this.pnlSapHet.PerformLayout();
            this.tlpDuoi.ResumeLayout(false);
            this.tlpDuoi.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpDuoi;
        private System.Windows.Forms.Panel pnlSapHet;
        private System.Windows.Forms.ListView lvSapHet;
        private System.Windows.Forms.Label lblTieuDeSapHet;
        private System.Windows.Forms.Panel pnlGanDay;
        private System.Windows.Forms.ListView lvGanDay;
        private System.Windows.Forms.Label lblTieuDeGanDay;
        private System.Windows.Forms.Panel pnlCach;
        private System.Windows.Forms.TableLayoutPanel tlpThe;
        private System.Windows.Forms.Panel pnlThe1;
        private System.Windows.Forms.Label lblThe1;
        private System.Windows.Forms.Label lblSoMatHang;
        private System.Windows.Forms.Panel pnlThe2;
        private System.Windows.Forms.Label lblThe2;
        private System.Windows.Forms.Label lblSoTonKho;
        private System.Windows.Forms.Panel pnlThe3;
        private System.Windows.Forms.Label lblThe3;
        private System.Windows.Forms.Label lblSoHoaDon;
        private System.Windows.Forms.Panel pnlThe4;
        private System.Windows.Forms.Label lblThe4;
        private System.Windows.Forms.Label lblSoDoanhThu;
    }
}

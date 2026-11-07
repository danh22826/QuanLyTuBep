using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    /// <summary>Responsive product summary shared by the import picker and invoice details.</summary>
    internal sealed class SanPhamThongTinControl : UserControl
    {
        private readonly FlowLayoutPanel content;
        private readonly FlowLayoutPanel header;
        private readonly FlowLayoutPanel attributes;
        private readonly Label nameLabel;
        private readonly Label codeLabel;
        private readonly List<Label> chips = new List<Label>();
        private readonly ToolTip toolTip;
        private int lastWidth = -1;
        private bool adjusting;

        public SanPhamThongTinControl(string ma, string ten, string mau, string kichThuoc,
            string chatLieu, string nuocSanXuat, ToolTip toolTip)
        {
            this.toolTip = toolTip;
            AutoScaleMode = AutoScaleMode.None;
            Margin = Padding.Empty;
            Padding = Padding.Empty;
            Height = 62;

            content = new FlowLayoutPanel
            {
                Location = Point.Empty,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

            header = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            codeLabel = new Label
            {
                Text = string.IsNullOrWhiteSpace(ma) ? "(chưa có mã)" : ma,
                AutoSize = true,
                BackColor = Color.FromArgb(0, 135, 137),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(5, 2, 5, 2),
                Margin = new Padding(0, 2, 6, 3)
            };
            nameLabel = new Label
            {
                Text = ten ?? "(chưa có tên)",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 1, 0, 2)
            };
            header.Controls.Add(codeLabel);
            header.Controls.Add(nameLabel);

            attributes = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            AddChip(mau);
            AddChip(kichThuoc);
            AddChip(chatLieu);
            AddChip(nuocSanXuat);

            content.Controls.Add(header);
            content.Controls.Add(attributes);
            Controls.Add(content);
            SizeChanged += OnSummarySizeChanged;
            UpdateContentWidth();

            if (toolTip != null)
            {
                string full = (ma ?? "") + " | " + (ten ?? "") + "\nMàu sắc: " + DisplayValue(mau) +
                    " | Kích thước: " + DisplayValue(kichThuoc) + " | Chất liệu: " + DisplayValue(chatLieu) +
                    " | Nước sản xuất: " + DisplayValue(nuocSanXuat);
                toolTip.SetToolTip(this, full);
                toolTip.SetToolTip(codeLabel, full);
                toolTip.SetToolTip(nameLabel, full);
            }
        }

        private void AddChip(string value)
        {
            string display = DisplayValue(value);
            Label chip = new Label
            {
                Text = display,
                AutoSize = true,
                BackColor = Color.FromArgb(239, 242, 245),
                ForeColor = Color.FromArgb(75, 85, 99),
                Font = new Font("Segoe UI", 7.5F),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(6, 3, 6, 3),
                Margin = new Padding(0, 0, 5, 4)
            };
            chips.Add(chip);
            attributes.Controls.Add(chip);
            if (toolTip != null) toolTip.SetToolTip(chip, display);
        }

        private static string DisplayValue(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "(chưa khai báo)" : value.Trim();
        }

        private void OnSummarySizeChanged(object sender, EventArgs e)
        {
            UpdateContentWidth();
        }

        private void UpdateContentWidth()
        {
            if (adjusting || ClientSize.Width <= 0 || ClientSize.Width == lastWidth) return;
            adjusting = true;
            try
            {
                lastWidth = ClientSize.Width;
                int width = Math.Max(80, ClientSize.Width);
                content.MaximumSize = new Size(width, 0);
                content.Width = width;
                header.MaximumSize = new Size(width, 0);
                header.Width = width;
                attributes.MaximumSize = new Size(width, 0);
                attributes.Width = width;

                int codeWidth = codeLabel.PreferredWidth + 8;
                nameLabel.MaximumSize = new Size(Math.Max(70, width - codeWidth - 8), 0);
                int chipWidth = Math.Max(90, width - 4);
                foreach (Label chip in chips) chip.MaximumSize = new Size(chipWidth, 0);

                content.PerformLayout();
                attributes.PerformLayout();
                header.PerformLayout();
                Height = Math.Max(1, content.PreferredSize.Height);
            }
            finally { adjusting = false; }
        }
    }
}

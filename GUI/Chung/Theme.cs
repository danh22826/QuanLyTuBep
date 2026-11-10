using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    public static class Theme
    {
        public static readonly Color SidebarNen = ColorTranslator.FromHtml("#32344D");
        public static readonly Color LogoNen    = ColorTranslator.FromHtml("#262539");
        public static readonly Color HeaderNen  = ColorTranslator.FromHtml("#008789");
        public static readonly Color NoiDungNen = ColorTranslator.FromHtml("#F0F0F0");
        public static readonly Color ChuSidebar = ColorTranslator.FromHtml("#B8BBD1");
        public static readonly Color ChuHeader  = ColorTranslator.FromHtml("#FDFFFF");
        public static readonly Color IconDong   = ColorTranslator.FromHtml("#55D3DA");

        public static readonly Color SidebarHover  = ColorTranslator.FromHtml("#3C3E5A");
        public static readonly Color SidebarActive = ColorTranslator.FromHtml("#262539");
        public static readonly Color VienActive    = ColorTranslator.FromHtml("#55D3DA");
        public static readonly Color ChuChinh      = ColorTranslator.FromHtml("#1F2937");
        public static readonly Color Xam           = ColorTranslator.FromHtml("#6B7280");
        public static readonly Color Nguy          = ColorTranslator.FromHtml("#DC2626");
        public static readonly Color DongXen       = ColorTranslator.FromHtml("#F3F4F6");
        public static readonly Color DongChon      = ColorTranslator.FromHtml("#CDEDEE");
        public static readonly Color VienNhat      = ColorTranslator.FromHtml("#E5E7EB");

        public const int LeNoiDung      = 16;
        public const int KhoangCach     = 16;

        public static readonly Font ChuMenu   = new Font("Segoe UI", 10F);
        public static readonly Font ChuDam    = new Font("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font ChuNho    = new Font("Segoe UI", 9F);
        public static readonly Font ChuTieuDe = new Font("Segoe UI", 14F, FontStyle.Bold);
        public static readonly Font ChuSo     = new Font("Segoe UI", 20F, FontStyle.Bold);

        public class MenuRenderer : ToolStripProfessionalRenderer
        {
            private static bool LaTrangTo(ToolStrip ts)
            {
                return ts is MenuStrip || ts is ToolStripDropDown || ts is StatusStrip;
            }

            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                using (SolidBrush b = new SolidBrush(LaTrangTo(e.ToolStrip) ? SidebarNen : Color.White))
                    e.Graphics.FillRectangle(b, e.AffectedBounds);
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                Rectangle r = e.ToolStrip.ClientRectangle;
                if (e.ToolStrip is ToolStripDropDown)
                {
                    using (Pen p = new Pen(LogoNen))
                        e.Graphics.DrawRectangle(p, 0, 0, r.Width - 1, r.Height - 1);
                }
                else if (!LaTrangTo(e.ToolStrip))
                {
                    using (Pen p = new Pen(VienNhat))
                        e.Graphics.DrawLine(p, 0, r.Height - 1, r.Width, r.Height - 1);
                }
            }

            protected override void OnRenderImageMargin(ToolStripRenderEventArgs e) { }

            protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e) { }

            protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e) { }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                ToolStripMenuItem mi = e.Item as ToolStripMenuItem;
                if (mi == null) return;
                Rectangle r = new Rectangle(Point.Empty, mi.Size);
                bool chon = mi.Selected || mi.Pressed;

                if (chon || mi.Checked)
                {
                    using (SolidBrush b = new SolidBrush(chon ? SidebarHover : SidebarActive))
                        e.Graphics.FillRectangle(b, r);
                }
                if (mi.Checked)
                {
                    using (SolidBrush b = new SolidBrush(VienActive))
                    {
                        if (mi.IsOnDropDown) e.Graphics.FillRectangle(b, 0, 0, 4, r.Height);
                        else e.Graphics.FillRectangle(b, 0, r.Height - 3, r.Width, 3);
                    }
                }
            }

            protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
            {
                if (e.Item.Selected || e.Item.Pressed)
                {
                    using (SolidBrush b = new SolidBrush(DongChon))
                        e.Graphics.FillRectangle(b, new Rectangle(Point.Empty, e.Item.Size));
                }
            }

            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                int y = e.Item.Height / 2;
                using (Pen p = new Pen(SidebarHover))
                    e.Graphics.DrawLine(p, 8, y, e.Item.Width - 8, y);
            }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                e.ArrowColor = ChuSidebar;
                base.OnRenderArrow(e);
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                ToolStripMenuItem mi = e.Item as ToolStripMenuItem;
                if (!e.Item.Enabled) e.TextColor = Xam;
                else if (!LaTrangTo(e.ToolStrip)) e.TextColor = ChuChinh;
                else if (mi != null && (mi.Selected || mi.Pressed || mi.Checked)) e.TextColor = ChuHeader;
                else e.TextColor = ChuSidebar;
                base.OnRenderItemText(e);
            }
        }

        public static void StyleNut(Button btn, Color nen)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = nen;
            btn.ForeColor = Color.White;
            btn.Font = ChuDam;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        public static Label TaoNhan(string chu, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = chu;
            lbl.AutoSize = true;
            lbl.Left = x;
            lbl.Top = y;
            lbl.Font = ChuNho;
            lbl.ForeColor = Xam;
            return lbl;
        }
    }
}

using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmHoaDon
    {
        private void TaoBoCucGiaoDich()
        {
            // Retain the shared invoice header and footer; replace only the workspace below them.
            foreach (Control c in pnlNhapDong.Controls.Cast<Control>().ToList())
            {
                bool dungLai = c == dgvHang || c == cboLocMau || c == cboLocKichThuoc ||
                    c == cboLocChatLieu || c == cboLocNuocSX || nhanBoLoc.Values.Contains(c);
                if (!dungLai)
                {
                    pnlNhapDong.Controls.Remove(c);
                    c.Dispose();
                }
            }

            pnlNhapDong.Controls.Remove(dgvHang);
            foreach (ComboBox cbo in new[] { cboLocMau, cboLocKichThuoc, cboLocChatLieu, cboLocNuocSX })
                pnlNhapDong.Controls.Remove(cbo);
            foreach (Label lbl in nhanBoLoc.Values) pnlNhapDong.Controls.Remove(lbl);

            pnlPhai.Controls.Remove(lvDong);
            pnlPhai.Controls.Remove(cach2);
            pnlPhai.Controls.Remove(pnlNhapDong);
            lvDong.Dispose();
            cach2.Dispose();
            pnlNhapDong.Dispose();

            splitNhapHang = new SplitContainer
            {
                Name = "splitNhapHang",
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                BorderStyle = BorderStyle.FixedSingle,
                SplitterWidth = 6,
                Size = new Size(pnlPhai.ClientSize.Width, Math.Max(300, pnlPhai.ClientSize.Height - 210)),
                Panel1MinSize = 240,
                Panel2MinSize = 280
            };
            splitNhapHang.SplitterDistance = (splitNhapHang.Width - splitNhapHang.SplitterWidth) / 2;
            splitNhapHang.Resize += splitNhapHang_Resize;
            pnlPhai.Controls.Add(splitNhapHang);
            // Keep the workspace above the removed legacy editor/footer siblings.
            splitNhapHang.BringToFront();

            TaoKhuLocSanPhamNhap();
            TaoBangChiTietNhap();
        }

        private void splitNhapHang_Resize(object sender, EventArgs e)
        {
            if (splitNhapHang == null) return;
            int khaDung = splitNhapHang.ClientSize.Width - splitNhapHang.SplitterWidth;
            if (khaDung < splitNhapHang.Panel1MinSize + splitNhapHang.Panel2MinSize) return;
            int nua = Math.Max(splitNhapHang.Panel1MinSize,
                Math.Min(khaDung / 2, khaDung - splitNhapHang.Panel2MinSize));
            if (splitNhapHang.SplitterDistance != nua) splitNhapHang.SplitterDistance = nua;
        }

        private void TaoKhuLocSanPhamNhap()
        {
            TableLayoutPanel khung = new TableLayoutPanel
            {
                Name = "tlpChonSanPhamNhap", Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4,
                Padding = new Padding(4), Margin = Padding.Empty
            };
            khung.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            khung.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            khung.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            khung.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
            khung.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Label lblTieuDe = new Label
            {
                Text = "CHỌN SẢN PHẨM", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Theme.ChuChinh,
                Padding = new Padding(6, 0, 0, 0), Margin = Padding.Empty
            };
            Panel pnlTim = new Panel { Dock = DockStyle.Fill, Padding = new Padding(6, 2, 6, 2), Margin = Padding.Empty };
            Label lblTim = new Label { Text = "Tìm sản phẩm", Dock = DockStyle.Top, Height = 20, ForeColor = Theme.Xam };
            txtTimHang = new TextBox { Name = "txtTimHang", Dock = DockStyle.Bottom, Font = new Font("Segoe UI", 9F) };
            txtTimHang.TextChanged += delegate { HienThiDanhSachHang(); };
            pnlTim.Controls.Add(txtTimHang);
            pnlTim.Controls.Add(lblTim);

            TableLayoutPanel tlpLoc = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 100,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(2),
                Margin = Padding.Empty
            };
            tlpLoc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpLoc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpLoc.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpLoc.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            ThemOLoc(tlpLoc, cboLocMau, 0, 0);
            ThemOLoc(tlpLoc, cboLocKichThuoc, 1, 0);
            ThemOLoc(tlpLoc, cboLocChatLieu, 0, 1);
            ThemOLoc(tlpLoc, cboLocNuocSX, 1, 1);

            flpSanPhamNhap = new FlowLayoutPanel
            {
                Name = "flpSanPhamNhap", Dock = DockStyle.Fill, AutoScroll = true,
                FlowDirection = FlowDirection.TopDown, WrapContents = false,
                BackColor = Color.FromArgb(246, 247, 249), Padding = new Padding(5),
                Margin = Padding.Empty
            };
            tipSanPhamNhap = new ToolTip { AutoPopDelay = 12000, InitialDelay = 450, ReshowDelay = 100 };
            flpSanPhamNhap.SizeChanged += flpSanPhamNhap_SizeChanged;

            khung.Controls.Add(lblTieuDe, 0, 0);
            khung.Controls.Add(pnlTim, 0, 1);
            khung.Controls.Add(tlpLoc, 0, 2);
            khung.Controls.Add(flpSanPhamNhap, 0, 3);
            splitNhapHang.Panel1.Padding = new Padding(2);
            splitNhapHang.Panel1.Controls.Add(khung);
        }

        private void ThemOLoc(TableLayoutPanel tlp, ComboBox cbo, int cot, int dong)
        {
            Label lbl = nhanBoLoc[cbo];
            Panel pnl = new Panel { Dock = DockStyle.Fill, Padding = new Padding(4, 2, 4, 2) };
            lbl.Dock = DockStyle.Top;
            lbl.Location = Point.Empty;
            cbo.Dock = DockStyle.Bottom;
            cbo.Width = 100;
            pnl.Controls.Add(cbo);
            pnl.Controls.Add(lbl);
            tlp.Controls.Add(pnl, cot, dong);
        }

        private void TaoBangChiTietNhap()
        {
            TableLayoutPanel khung = TaoBangKhung("tlpChiTietNhap", 2, 38F);
            lblTieuDeChiTietNhap = new Label
            {
                Text = (laBan ? "CHI TIẾT HÓA ĐƠN BÁN (" : "CHI TIẾT HÓA ĐƠN NHẬP (") + controller.Dong.Count + ")",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Theme.ChuChinh,
                Padding = new Padding(8, 0, 0, 0),
                Margin = Padding.Empty
            };
            FlowLayoutPanel danhSachChiTiet = new FlowLayoutPanel
            {
                Name = laBan ? "flpChiTietBan" : "flpChiTietNhap", Dock = DockStyle.Fill, AutoScroll = true,
                FlowDirection = FlowDirection.TopDown, WrapContents = false,
                BackColor = Color.FromArgb(246, 247, 249), Padding = new Padding(6),
                Margin = Padding.Empty
            };
            if (laBan) flpChiTietBan = danhSachChiTiet;
            else flpChiTietNhap = danhSachChiTiet;
            danhSachChiTiet.SizeChanged += flpChiTietNhap_SizeChanged;
            tipChiTietNhap = new ToolTip { AutoPopDelay = 12000, InitialDelay = 450, ReshowDelay = 100 };
            khung.Controls.Add(lblTieuDeChiTietNhap, 0, 0);
            khung.Controls.Add(danhSachChiTiet, 0, 1);
            splitNhapHang.Panel2.Padding = new Padding(2);
            splitNhapHang.Panel2.Controls.Add(khung);
            NapDong();
        }

        private TableLayoutPanel TaoBangKhung(string ten, int soHang, float chieuCaoTieuDe)
        {
            TableLayoutPanel khung = new TableLayoutPanel
            {
                Name = ten, Dock = DockStyle.Fill, ColumnCount = 1, RowCount = soHang,
                Padding = new Padding(4), Margin = Padding.Empty
            };
            khung.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            khung.RowStyles.Add(new RowStyle(SizeType.Absolute, chieuCaoTieuDe));
            khung.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            return khung;
        }

        private void flpChiTietNhap_SizeChanged(object sender, EventArgs e)
        {
            FlowLayoutPanel list = sender as FlowLayoutPanel;
            if (list == null) return;
            int chieuRong = Math.Max(250, list.ClientSize.Width - list.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth - 8);
            foreach (Control the in list.Controls) the.Width = chieuRong;
        }

        private void flpSanPhamNhap_SizeChanged(object sender, EventArgs e)
        {
            if (flpSanPhamNhap == null) return;
            int chieuRong = Math.Max(220, flpSanPhamNhap.ClientSize.Width - flpSanPhamNhap.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth - 8);
            foreach (Control the in flpSanPhamNhap.Controls) the.Width = chieuRong;
        }

        private Panel TaoTheChonSanPhamNhap(HangHoa h)
        {
            Panel the = new Panel
            {
                Name = "pick_" + h.MaHang, Height = 80,
                Width = Math.Max(220, flpSanPhamNhap.ClientSize.Width - 28),
                BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(2, 2, 2, 7), Padding = new Padding(9, 8, 8, 8), Tag = h
            };
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2, RowCount = 1, Margin = Padding.Empty, Padding = Padding.Empty
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 72F));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            SanPhamThongTinControl thongTin = new SanPhamThongTinControl(h.MaHang, h.TenHang,
                h.TenMau, h.TenKichThuoc, h.TenChatLieu, h.TenNuocSX, tipSanPhamNhap)
            {
                Dock = DockStyle.Fill, AutoSize = true, Margin = new Padding(0, 0, 5, 0)
            };
            TableLayoutPanel tacVu = new TableLayoutPanel
            {
                AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1, RowCount = 2, Margin = Padding.Empty, Padding = Padding.Empty
            };
            tacVu.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tacVu.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tacVu.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            int soLuongCoTheBan = Math.Max(0, Math.Min(h.SoLuong, h.ConLaiLo));
            int tonKhaDung = laBan ? soLuongCoTheBan : h.SoLuong;
            Label ton = new Label
            {
                Text = (laBan ? "Có thể bán " : "Tồn kho ") + tonKhaDung, AutoSize = true,
                Font = new Font("Segoe UI", 8F), ForeColor = Theme.Xam,
                TextAlign = ContentAlignment.MiddleCenter, AutoEllipsis = true, Margin = Padding.Empty
            };
            Button them = new Button
            {
                Text = "+", Width = 48, Height = 34, Anchor = AnchorStyles.None,
                Cursor = Cursors.Hand, FlatStyle = FlatStyle.Flat,
                BackColor = Theme.HeaderNen, ForeColor = Color.White,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold), Margin = new Padding(4, 5, 4, 0), Tag = h
            };
            them.FlatAppearance.BorderSize = 0;
            bool daHetHang = laBan && soLuongCoTheBan <= 0;
            them.Enabled = !daHetHang;
            them.Click += delegate
            {
                if (laBan) ThemHoacTangMatHangBan(h);
                else ThemHoacTangMatHangNhap(h);
            };
            tacVu.Controls.Add(ton, 0, 0);
            tacVu.Controls.Add(them, 0, 1);
            layout.Controls.Add(thongTin, 0, 0);
            layout.Controls.Add(tacVu, 1, 0);
            the.Controls.Add(layout);
            the.SizeChanged += delegate
            {
                int innerWidth = Math.Max(100, the.ClientSize.Width - the.Padding.Horizontal);
                if (layout.Width != innerWidth) layout.Width = innerWidth;
                int contentHeight = Math.Max(thongTin.Height, tacVu.PreferredSize.Height);
                int height = contentHeight + the.Padding.Vertical;
                if (the.Height != height) the.Height = height;
            };
            the.PerformLayout();
            layout.PerformLayout();
            the.Height = Math.Max(tacVu.PreferredSize.Height, thongTin.Height) + the.Padding.Vertical;
            tipSanPhamNhap.SetToolTip(the, MoTaHangDayDu(h));
            tipSanPhamNhap.SetToolTip(ton, MoTaHangDayDu(h));
            return the;
        }

        private static string MoTaHangDayDu(HangHoa h)
        {
            return h.MaHang + " | " + h.TenHang + "\nMàu: " + h.TenMau + " | Kích thước: " + h.TenKichThuoc +
                " | Chất liệu: " + h.TenChatLieu + " | Nước sản xuất: " + h.TenNuocSX;
        }

        private Panel TaoTheChiTietNhap(ChiTietHoaDonNhap d)
        {
            Panel the = new Panel
            {
                Name = "card_" + d.MaHang, Height = 146, Width = Math.Max(280, flpChiTietNhap.ClientSize.Width - 30),
                BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(2, 2, 2, 8),
                Padding = new Padding(8)
            };
            TableLayoutPanel boCuc = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2, Margin = Padding.Empty
            };
            boCuc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            boCuc.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 68F));
            boCuc.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            boCuc.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Button btnXoa = new Button
            {
                Text = "Xóa dòng", Dock = DockStyle.Fill, FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(220, 53, 69), ForeColor = Color.White, Margin = new Padding(3, 0, 0, 0),
                Tag = d
            };
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.Click += btnXoaTheNhap_Click;
            SanPhamThongTinControl thongTin = new SanPhamThongTinControl(d.MaHang, d.TenHang,
                d.TenMau, d.TenKichThuoc, d.TenChatLieu, d.TenNuocSX, tipChiTietNhap)
            {
                Dock = DockStyle.Top, AutoSize = true,
                Margin = Padding.Empty
            };
            thongTin.SizeChanged += delegate
            {
                int chieuCao = Math.Max(146, thongTin.Height + 96);
                if (the.Height != chieuCao) the.Height = chieuCao;
            };
            TableLayoutPanel nhap = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2, Margin = Padding.Empty, Padding = new Padding(0, 2, 0, 0)
            };
            for (int c = 0; c < 2; c++) nhap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            nhap.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            nhap.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            Panel oSoLuong = TaoONhapThe("Số lượng", out Panel hostSL);
            FlowLayoutPanel dieuKhienSL = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false,
                Margin = Padding.Empty, Padding = Padding.Empty
            };
            Button btnGiam = TaoNutSoLuong("−", d);
            Button btnTang = TaoNutSoLuong("+", d);
            NumericUpDown nudSoLuong = new NumericUpDown
            {
                Name = "nudSL_" + d.MaHang, Minimum = 1, Maximum = 1000000, Value = Math.Max(1, d.SoLuong),
                Width = 52, TextAlign = HorizontalAlignment.Center, Margin = new Padding(2, 1, 2, 0), Tag = d
            };
            btnGiam.Click += delegate { if (nudSoLuong.Value > 1) nudSoLuong.Value--; };
            btnTang.Click += delegate { if (nudSoLuong.Value < nudSoLuong.Maximum) nudSoLuong.Value++; };
            dieuKhienSL.Controls.Add(btnGiam);
            dieuKhienSL.Controls.Add(nudSoLuong);
            dieuKhienSL.Controls.Add(btnTang);
            hostSL.Controls.Add(dieuKhienSL);

            Panel oDonGia = TaoONhapThe("Đơn giá nhập", out Panel hostGia);
            NumericUpDown nudDonGia = new NumericUpDown
            {
                Name = "nudGia_" + d.MaHang, Dock = DockStyle.Fill, Minimum = 0, Maximum = Decimal.MaxValue,
                Value = Math.Max(0, d.DonGia), ThousandsSeparator = true, DecimalPlaces = 0,
                Increment = 1000, TextAlign = HorizontalAlignment.Right, Margin = Padding.Empty, Tag = d
            };
            hostGia.Controls.Add(nudDonGia);

            Panel oGiamGia = TaoONhapThe("Giảm giá (%)", out Panel hostGiam);
            NumericUpDown nudGiamGia = new NumericUpDown
            {
                Name = "nudGiam_" + d.MaHang, Dock = DockStyle.Fill, Minimum = 0, Maximum = 100,
                DecimalPlaces = 1, Increment = 0.1M, Value = Math.Max(0, Math.Min(100, (decimal)d.GiamGia)),
                TextAlign = HorizontalAlignment.Right, Margin = Padding.Empty, Tag = d
            };
            hostGiam.Controls.Add(nudGiamGia);

            Panel oThanhTien = TaoONhapThe("Thành tiền", out Panel hostTien);
            Label lblThanhTien = new Label
            {
                Name = "lblThanhTien_" + d.MaHang, Text = d.ThanhTien.ToString("N0") + " đ", Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Theme.ChuChinh,
                TextAlign = ContentAlignment.MiddleRight, AutoEllipsis = true, Tag = d
            };
            hostTien.Controls.Add(lblThanhTien);

            nudSoLuong.ValueChanged += delegate { CapNhatDongNhap(d, (int)nudSoLuong.Value, nudDonGia.Value, nudGiamGia.Value); };
            nudDonGia.ValueChanged += delegate { CapNhatDongNhap(d, (int)nudSoLuong.Value, nudDonGia.Value, nudGiamGia.Value); };
            nudGiamGia.ValueChanged += delegate { CapNhatDongNhap(d, (int)nudSoLuong.Value, nudDonGia.Value, nudGiamGia.Value); };

            boCuc.Controls.Add(thongTin, 0, 0);
            boCuc.Controls.Add(btnXoa, 1, 0);
            nhap.Controls.Add(oSoLuong, 0, 0);
            nhap.Controls.Add(oDonGia, 1, 0);
            nhap.Controls.Add(oGiamGia, 0, 1);
            nhap.Controls.Add(oThanhTien, 1, 1);
            boCuc.Controls.Add(nhap, 0, 1);
            boCuc.SetColumnSpan(nhap, 2);
            the.Controls.Add(boCuc);
            return the;
        }

        private static Panel TaoONhapThe(string nhan, out Panel noiDung)
        {
            TableLayoutPanel khung = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Padding = new Padding(4, 0, 4, 0), Margin = Padding.Empty
            };
            khung.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            khung.RowStyles.Add(new RowStyle(SizeType.Absolute, 17F));
            khung.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            khung.Controls.Add(new Label { Text = nhan, Dock = DockStyle.Fill, ForeColor = Theme.Xam, Font = new Font("Segoe UI", 8F), Margin = Padding.Empty }, 0, 0);
            noiDung = new Panel { Dock = DockStyle.Fill, Margin = Padding.Empty };
            khung.Controls.Add(noiDung, 0, 1);
            Panel host = new Panel { Dock = DockStyle.Fill, Margin = Padding.Empty };
            host.Controls.Add(khung);
            return host;
        }

        private static Button TaoNutSoLuong(string text, object chiTiet)
        {
            return new Button
            {
                Text = text, Width = 24, Height = 27, FlatStyle = FlatStyle.Flat,
                BackColor = Theme.HeaderNen, ForeColor = Color.White, Margin = new Padding(1, 1, 1, 0), Tag = chiTiet
            };
        }

        private void dgvHang_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHang.Rows.Count) return;
            HangHoa h = dgvHang.Rows[e.RowIndex].Tag as HangHoa;
            if (h != null)
                e.ToolTipText = h.MaHang + " | " + h.TenHang + Environment.NewLine +
                    "Màu: " + h.TenMau + " | Kích thước: " + h.TenKichThuoc + Environment.NewLine +
                    "Chất liệu: " + h.TenChatLieu + " | Nước sản xuất: " + h.TenNuocSX;
        }

        private void HienThiDanhSachNhapHang()
        {
            if (flpSanPhamNhap == null || txtTimHang == null) return;
            string mau = Convert.ToString(cboLocMau.SelectedValue);
            string kichThuoc = Convert.ToString(cboLocKichThuoc.SelectedValue);
            string chatLieu = Convert.ToString(cboLocChatLieu.SelectedValue);
            string nuoc = Convert.ToString(cboLocNuocSX.SelectedValue);
            string tuKhoa = txtTimHang.Text.Trim();
            flpSanPhamNhap.SuspendLayout();
            foreach (Control card in flpSanPhamNhap.Controls.Cast<Control>().ToList())
            {
                flpSanPhamNhap.Controls.Remove(card);
                card.Dispose();
            }

            foreach (HangHoa h in danhSachHang.Where(h =>
                (string.IsNullOrEmpty(mau) || h.MaMau == mau) &&
                (string.IsNullOrEmpty(kichThuoc) || h.MaKichThuoc == kichThuoc) &&
                (string.IsNullOrEmpty(chatLieu) || h.MaChatLieu == chatLieu) &&
                (string.IsNullOrEmpty(nuoc) || h.MaNuocSX == nuoc) &&
                (string.IsNullOrEmpty(tuKhoa) ||
                    (h.TenHang ?? "").IndexOf(tuKhoa, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    (h.MaHang ?? "").IndexOf(tuKhoa, StringComparison.CurrentCultureIgnoreCase) >= 0)))
            {
                flpSanPhamNhap.Controls.Add(TaoTheChonSanPhamNhap(h));
            }
            if (flpSanPhamNhap.Controls.Count == 0)
                flpSanPhamNhap.Controls.Add(new Label
                {
                    Text = "Không tìm thấy sản phẩm phù hợp.",
                    Width = Math.Max(220, flpSanPhamNhap.ClientSize.Width - 24),
                    Height = 54,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Theme.Xam,
                    Margin = new Padding(2, 4, 2, 4)
                });
            flpSanPhamNhap.ResumeLayout(true);
            flpSanPhamNhap_SizeChanged(flpSanPhamNhap, EventArgs.Empty);
        }

        private void ThemHoacTangMatHangNhap(HangHoa hang)
        {
            if (hang == null || dangXem) return;
            ChiTietHoaDonNhap dong = controller.Dong
                .OfType<ChiTietHoaDonNhap>()
                .FirstOrDefault(d => string.Equals(d.MaHang, hang.MaHang, StringComparison.OrdinalIgnoreCase));
            if (dong == null)
            {
                decimal giaMacDinh = controller.LayDonGia(hang);
                dong = new ChiTietHoaDonNhap
                {
                    SoHDN = txtSo.Text.Trim(),
                    MaHang = hang.MaHang,
                    TenHang = hang.TenHang,
                    TenMau = hang.TenMau,
                    TenKichThuoc = hang.TenKichThuoc,
                    TenChatLieu = hang.TenChatLieu,
                    TenNuocSX = hang.TenNuocSX,
                    SoLuong = 1,
                    DonGia = giaMacDinh,
                    GiamGia = 0,
                    ThanhTien = HoaDonBll.TinhThanhTien(1, giaMacDinh, 0)
                };
                controller.Dong.Add(dong);
            }
            else
            {
                dong.SoLuong++;
                dong.ThanhTien = HoaDonBll.TinhThanhTien(dong.SoLuong, dong.DonGia, dong.GiamGia);
            }
            NapDong();
            CapNhatTong();
        }

        private void NapChiTietNhap()
        {
            if (flpChiTietNhap == null) return;
            flpChiTietNhap.SuspendLayout();
            foreach (Control cu in flpChiTietNhap.Controls.Cast<Control>().ToList())
            {
                flpChiTietNhap.Controls.Remove(cu);
                cu.Dispose();
            }
            foreach (ChiTietHoaDonNhap d in controller.Dong.OfType<ChiTietHoaDonNhap>())
                flpChiTietNhap.Controls.Add(TaoTheChiTietNhap(d));
            if (flpChiTietNhap.Controls.Count == 0)
                flpChiTietNhap.Controls.Add(new Label
                {
                    Text = "Chưa có sản phẩm nào trong hóa đơn. Nhấn + bên trái để thêm.",
                    Width = Math.Max(250, flpChiTietNhap.ClientSize.Width - 30),
                    Height = 60, TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Theme.Xam, Margin = new Padding(2, 2, 2, 8)
                });
            flpChiTietNhap.ResumeLayout(true);
            flpChiTietNhap_SizeChanged(flpChiTietNhap, EventArgs.Empty);
            lblTieuDeChiTietNhap.Text = "CHI TIẾT HÓA ĐƠN NHẬP (" +
                controller.Dong.OfType<ChiTietHoaDonNhap>().Count() + ")";
        }

        private void NapChiTietBan()
        {
            if (flpChiTietBan == null) return;
            flpChiTietBan.SuspendLayout();
            foreach (Control cu in flpChiTietBan.Controls.Cast<Control>().ToList())
            {
                flpChiTietBan.Controls.Remove(cu);
                cu.Dispose();
            }
            foreach (ChiTietHoaDonBan d in controller.Dong.OfType<ChiTietHoaDonBan>())
                flpChiTietBan.Controls.Add(TaoTheChiTietBan(d));
            if (flpChiTietBan.Controls.Count == 0)
                flpChiTietBan.Controls.Add(new Label
                {
                    Text = "Chưa có sản phẩm nào trong hóa đơn. Nhấn + bên trái để thêm.",
                    Width = Math.Max(250, flpChiTietBan.ClientSize.Width - 30),
                    Height = 60, TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Theme.Xam, Margin = new Padding(2, 2, 2, 8)
                });
            flpChiTietBan.ResumeLayout(true);
            flpChiTietNhap_SizeChanged(flpChiTietBan, EventArgs.Empty);
            lblTieuDeChiTietNhap.Text = "CHI TIẾT HÓA ĐƠN BÁN (" +
                controller.Dong.OfType<ChiTietHoaDonBan>().Count() + ")";
        }

        private void btnXoaTheNhap_Click(object sender, EventArgs e)
        {
            if (dangXem) return;
            Button nut = sender as Button;
            ChiTietHoaDonNhap d = nut == null ? null : nut.Tag as ChiTietHoaDonNhap;
            if (d != null && controller.XoaDong(d))
            {
                NapChiTietNhap();
                CapNhatTong();
            }
        }

        private void CapNhatDongNhap(ChiTietHoaDonNhap d, int soLuong, decimal donGia, decimal giamGia)
        {
            if (d == null || dangXem) return;
            d.SoLuong = Math.Max(1, soLuong);
            d.DonGia = Math.Max(0, donGia);
            d.GiamGia = (double)Math.Max(0, Math.Min(100, giamGia));
            d.ThanhTien = HoaDonBll.TinhThanhTien(d.SoLuong, d.DonGia, d.GiamGia);
            foreach (Control the in flpChiTietNhap.Controls)
            {
                Label tien = the.Controls.Find("lblThanhTien_" + d.MaHang, true).FirstOrDefault() as Label;
                if (tien != null) { tien.Text = d.ThanhTien.ToString("N0") + " đ"; break; }
            }
            CapNhatTong();
        }
    }
}

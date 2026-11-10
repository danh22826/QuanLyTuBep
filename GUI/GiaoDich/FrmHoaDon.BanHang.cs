using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmHoaDon
    {
        private Panel TaoTheChiTietBan(ChiTietHoaDonBan d)
        {
            Panel the = new Panel
            {
                Name = "saleCard_" + d.MaHang, Height = 146,
                Width = Math.Max(280, flpChiTietBan.ClientSize.Width - 30),
                BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(2, 2, 2, 8), Padding = new Padding(8)
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
                BackColor = Color.FromArgb(220, 53, 69), ForeColor = Color.White,
                Margin = new Padding(3, 0, 0, 0), Tag = d
            };
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.Click += btnXoaTheBan_Click;
            SanPhamThongTinControl thongTin = new SanPhamThongTinControl(d.MaHang, d.TenHang,
                d.TenMau, d.TenKichThuoc, d.TenChatLieu, d.TenNuocSX, tipChiTietNhap)
            {
                Dock = DockStyle.Top, AutoSize = true, Margin = Padding.Empty
            };
            thongTin.SizeChanged += delegate
            {
                int height = Math.Max(146, thongTin.Height + 96);
                if (the.Height != height) the.Height = height;
            };

            TableLayoutPanel nhap = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2,
                Margin = Padding.Empty, Padding = new Padding(0, 2, 0, 0)
            };
            nhap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            nhap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            nhap.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            nhap.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            Panel oSoLuong = TaoONhapThe("Số lượng", out Panel hostSL);
            FlowLayoutPanel dieuKhienSL = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false, Margin = Padding.Empty, Padding = Padding.Empty
            };
            HangHoa hang = TimHangBan(d.MaHang);
            int toiDa = hang == null ? Math.Max(0, d.SoLuong) : Math.Max(0, Math.Min(hang.SoLuong, hang.ConLaiLo));
            NumericUpDown nudSoLuong = new NumericUpDown
            {
                Name = "saleQty_" + d.MaHang, Minimum = 1,
                Maximum = Math.Max(Math.Max(1, d.SoLuong), toiDa), Value = Math.Max(1, d.SoLuong),
                Width = 52, TextAlign = HorizontalAlignment.Center, Margin = new Padding(2, 1, 2, 0), Tag = d
            };
            Button btnGiam = TaoNutSoLuong("−", d);
            Button btnTang = TaoNutSoLuong("+", d);
            btnGiam.Click += delegate { if (nudSoLuong.Value > 1) nudSoLuong.Value--; };
            btnTang.Click += delegate
            {
                int max = GioiHanBan(d.MaHang);
                if (max > 0 && nudSoLuong.Value < Math.Min(nudSoLuong.Maximum, max)) nudSoLuong.Value++;
                else TienIch.Canh("Không thể bán vượt quá tồn kho khả dụng " + max + ".");
            };
            dieuKhienSL.Controls.Add(btnGiam);
            dieuKhienSL.Controls.Add(nudSoLuong);
            dieuKhienSL.Controls.Add(btnTang);
            hostSL.Controls.Add(dieuKhienSL);

            Panel oDonGia = TaoONhapThe("Đơn giá bán", out Panel hostGia);
            NumericUpDown nudDonGia = new NumericUpDown
            {
                Name = "salePrice_" + d.MaHang, Dock = DockStyle.Fill,
                Minimum = 0, Maximum = Decimal.MaxValue, Value = Math.Max(0, d.DonGia),
                ThousandsSeparator = true, DecimalPlaces = 0, Increment = 1000,
                TextAlign = HorizontalAlignment.Right, Margin = Padding.Empty,
                Enabled = controller.ChoSuaDonGia, Tag = d
            };
            hostGia.Controls.Add(nudDonGia);

            Panel oGiamGia = TaoONhapThe("Giảm giá (%)", out Panel hostGiam);
            NumericUpDown nudGiamGia = new NumericUpDown
            {
                Name = "saleDiscount_" + d.MaHang, Dock = DockStyle.Fill,
                Minimum = 0, Maximum = 100, DecimalPlaces = 1, Increment = 0.1M,
                Value = Math.Max(0, Math.Min(100, (decimal)d.GiamGia)),
                TextAlign = HorizontalAlignment.Right, Margin = Padding.Empty, Tag = d
            };
            hostGiam.Controls.Add(nudGiamGia);

            Panel oThanhTien = TaoONhapThe("Thành tiền", out Panel hostTien);
            Label lblThanhTien = new Label
            {
                Name = "lblThanhTienBan_" + d.MaHang, Text = d.ThanhTien.ToString("N0") + " đ",
                Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Theme.ChuChinh, TextAlign = ContentAlignment.MiddleRight,
                AutoEllipsis = true, Tag = d
            };
            hostTien.Controls.Add(lblThanhTien);

            nudSoLuong.ValueChanged += delegate { CapNhatDongBan(d, (int)nudSoLuong.Value, nudDonGia.Value, nudGiamGia.Value, nudSoLuong); };
            nudDonGia.ValueChanged += delegate { CapNhatDongBan(d, (int)nudSoLuong.Value, nudDonGia.Value, nudGiamGia.Value, nudSoLuong); };
            nudGiamGia.ValueChanged += delegate { CapNhatDongBan(d, (int)nudSoLuong.Value, nudDonGia.Value, nudGiamGia.Value, nudSoLuong); };

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

        private HangHoa TimHangBan(string maHang)
        {
            return danhSachHang.FirstOrDefault(h =>
                string.Equals(h.MaHang, maHang, StringComparison.OrdinalIgnoreCase));
        }

        private int GioiHanBan(string maHang)
        {
            HangHoa hang = TimHangBan(maHang);
            return hang == null ? 0 : Math.Max(0, Math.Min(hang.SoLuong, hang.ConLaiLo));
        }

        private void ThemHoacTangMatHangBan(HangHoa hang)
        {
            if (hang == null || dangXem) return;
            int toiDa = Math.Max(0, Math.Min(hang.SoLuong, hang.ConLaiLo));
            if (toiDa <= 0)
            {
                TienIch.Canh(hang.SoLuong <= 0
                    ? "Mặt hàng này hiện đã hết tồn kho."
                    : "Mặt hàng này không còn số lượng khả dụng trong lô có giá bán hiện tại.");
                return;
            }

            ChiTietHoaDonBan dong = controller.Dong.OfType<ChiTietHoaDonBan>()
                .FirstOrDefault(d => string.Equals(d.MaHang, hang.MaHang, StringComparison.OrdinalIgnoreCase));
            if (dong == null)
            {
                string loi = controller.ThemDong(txtSo.Text.Trim(), hang, 1, controller.LayDonGia(hang), 0);
                if (loi != null) { TienIch.Canh(loi); return; }
            }
            else if (dong.SoLuong >= toiDa)
            {
                TienIch.Canh("Chỉ còn " + toiDa + " sản phẩm khả dụng để bán theo tồn kho và lô giá hiện tại.");
                return;
            }
            else
            {
                dong.SoLuong++;
                dong.ThanhTien = HoaDonBll.TinhThanhTien(dong.SoLuong, dong.DonGia, dong.GiamGia);
            }
            NapDong();
            CapNhatTong();
        }

        private void btnXoaTheBan_Click(object sender, EventArgs e)
        {
            if (dangXem) return;
            Button button = sender as Button;
            ChiTietHoaDonBan dong = button == null ? null : button.Tag as ChiTietHoaDonBan;
            if (dong != null && controller.XoaDong(dong))
            {
                NapDong();
                CapNhatTong();
            }
        }

        private void CapNhatDongBan(ChiTietHoaDonBan d, int soLuong, decimal donGia,
            decimal giamGia, NumericUpDown soLuongControl)
        {
            if (d == null || dangXem) return;
            int gioiHan = GioiHanBan(d.MaHang);
            if (soLuong > gioiHan)
            {
                TienIch.Canh("Không thể bán vượt quá tồn kho khả dụng " + gioiHan + ".");
                int giaTriHopLe = gioiHan > 0 ? gioiHan : Math.Max(1, d.SoLuong);
                soLuongControl.Value = Math.Min(soLuongControl.Maximum, giaTriHopLe);
                return;
            }
            d.SoLuong = Math.Max(1, soLuong);
            d.DonGia = Math.Max(0, donGia);
            d.GiamGia = (double)Math.Max(0, Math.Min(100, giamGia));
            d.ThanhTien = HoaDonBll.TinhThanhTien(d.SoLuong, d.DonGia, d.GiamGia);
            foreach (Control card in flpChiTietBan.Controls)
            {
                Label total = card.Controls.Find("lblThanhTienBan_" + d.MaHang, true).FirstOrDefault() as Label;
                if (total != null) { total.Text = d.ThanhTien.ToString("N0") + " đ"; break; }
            }
            CapNhatTong();
        }
    }
}

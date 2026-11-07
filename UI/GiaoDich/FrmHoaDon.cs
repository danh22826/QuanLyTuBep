using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;
namespace QuanLyBanTuBep.UI
{
    public partial class FrmHoaDon : Form
    {
        private readonly HoaDonBll controller;
        private readonly bool laBan;
        private bool dangXem;
        private Button btnKhachMoi;
        private readonly Dictionary<string, int> soLuongChon = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, decimal> giaNhapChon = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
        private List<HangHoa> danhSachHang = new List<HangHoa>();
        private ComboBox cboLocMau, cboLocKichThuoc, cboLocChatLieu, cboLocNuocSX;
        private DataGridView dgvHang;
        private DataGridViewTextBoxColumn colTenHang, colMau, colKichThuoc, colChatLieu, colNuocSX, colTon, colDonGia, colSoLuong;
        private DataGridViewButtonColumn colGiam, colTang;
        private bool dangNapBoLoc;
        private readonly Dictionary<ComboBox, Label> nhanBoLoc = new Dictionary<ComboBox, Label>();
        private SplitContainer splitNhapHang;
        private FlowLayoutPanel flpChiTietNhap;
        private FlowLayoutPanel flpChiTietBan;
        private FlowLayoutPanel flpSanPhamNhap;
        private Label lblTieuDeChiTietNhap;
        private ToolTip tipChiTietNhap;
        private ToolTip tipSanPhamNhap;
        private TextBox txtTimHang;

        public FrmHoaDon()
        {
            InitializeComponent();
        }

        protected FrmHoaDon(HoaDonBll controller) : this()
        {
            this.controller = controller;
            this.laBan = (controller.Loai == LoaiHoaDon.Ban);
            controller.YeuCauSuDung();
            TaoGiaoDienChonHang();
            TaoBoCucGiaoDich();
            btnKhachMoi = new Button
            {
                Name = "btnKhachMoi", Text = "Khách mới", Location = new Point(550, 92),
                Size = new Size(100, 29), FlatStyle = FlatStyle.Flat,
                BackColor = Theme.HeaderNen, ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnKhachMoi.FlatAppearance.BorderSize = 0;
            btnKhachMoi.Click += btnKhachMoi_Click;
            pnlThongTin.Controls.Add(btnKhachMoi);
            ApDungLoai();

            TienIch.GanLuoi(lvDs);
            if (laBan && splitNhapHang == null) TienIch.GanLuoi(lvDong, true);
            NapDong();

            Load += delegate
            {
                try
                {
                    NapDuLieuNen();
                    TaiDanhSach();
                    HoaDonMoi();
                }
                catch (Exception ex)
                {
                    TienIch.XuLyLoi(ex);
                }
            };
        }

        private void ApDungLoai()
        {
            btnMoi.Text = laBan ? "+  Hoá đơn bán mới" : "+  Hoá đơn nhập mới";
            lblNgay.Text = laBan ? "Ngày bán" : "Ngày nhập";
            lblNhanVien.Text = laBan ? "Nhân viên bán *" : "Nhân viên nhập *";
            lblDoiTac.Text = laBan ? "Khách hàng" : "Nhà cung cấp *";
            lblDoiTac.Visible = true;
            cboDoiTac.Visible = true;
            lblMaKhach.Visible = false;
            txtMaKhach.Visible = false;
            lblTenKhach.Visible = false;
            txtTenKhach.Visible = false;
            lblDiaChiKhach.Visible = false;
            txtDiaChiKhach.Visible = false;
            lblDienThoaiKhach.Visible = false;
            txtDienThoaiKhach.Visible = false;
            btnKhachMoi.Visible = laBan;
            if (laBan)
            {
                lblDonGia.Text = "Đơn giá bán (tự động)";
                nudDonGia.Enabled = controller.ChoSuaDonGia;
            }
        }

        private void NapDong()
        {
            if (laBan && flpChiTietBan != null)
            {
                NapChiTietBan();
                return;
            }
            if (!laBan && flpChiTietNhap != null)
            {
                NapChiTietNhap();
                return;
            }
            TienIch.NapLuoi(lvDong, controller.Dong, "MaHang", "TenHang", "TenMau", "TenKichThuoc", "TenChatLieu", "TenNuocSX", "SoLuong", "DonGia", "GiamGia", "ThanhTien");
        }

        private int SoDongHienTai()
        {
            return controller.Dong.Count;
        }

        private void XoaDanhSachDong()
        {
            controller.BoDong();
        }

private void btnMoi_Click(object sender, EventArgs e) { BamHoaDonMoi(); }
        private void cboHang_SelectedIndexChanged(object sender, EventArgs e) { }
        private void btnThemDong_Click(object sender, EventArgs e) { ThemCacDongDaChon(); }
        private void btnXoaDong_Click(object sender, EventArgs e) { XoaDong(); }
        private void btnLuu_Click(object sender, EventArgs e) { Luu(); }
        private void btnXoaHD_Click(object sender, EventArgs e) { XoaHoaDon(); }

        private void NapDuLieuNen()
        {
            cboNV.DisplayMember = "Ten";
            cboNV.ValueMember = "Ma";
            cboNV.DataSource = controller.DanhSachNhanVien();
            ThongTinPhien phien = PhienDangNhap.HienTai;
            if (phien != null) TienIch.ChonGiaTri(cboNV, phien.MaNV);
            cboNV.Enabled = false;

            cboDoiTac.DisplayMember = "Ten";
            cboDoiTac.ValueMember = "Ma";
            cboDoiTac.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDoiTac.DataSource = controller.DanhSachDoiTac();
            cboDoiTac.SelectedIndex = -1;

            NapHang();
        }

        private void NapHang()
        {
            danhSachHang = controller.DanhSachHangHoa().ToList();
            NapBoLocHang();
            HienThiDanhSachHang();
        }

        private void TaoGiaoDienChonHang()
        {
            foreach (Control c in pnlNhapDong.Controls.Cast<Control>().ToList())
            {
                if (c == lblGiamGia || c == nudGiam || c == btnThemDong || c == btnXoaDong) continue;
                pnlNhapDong.Controls.Remove(c);
                c.Dispose();
            }
            pnlNhapDong.Height = 285;
            pnlNhapDong.Controls.AddRange(new Control[] { lblGiamGia, nudGiam, btnThemDong, btnXoaDong });

            cboLocMau = TaoBoLoc("Màu sắc", 16);
            cboLocKichThuoc = TaoBoLoc("Kích thước", 220);
            cboLocChatLieu = TaoBoLoc("Chất liệu", 424);
            cboLocNuocSX = TaoBoLoc("Nước sản xuất", 628);
            pnlNhapDong.Controls.AddRange(new Control[] { cboLocMau, cboLocKichThuoc, cboLocChatLieu, cboLocNuocSX });

            dgvHang = new DataGridView
            {
                Name = "dgvHang",
                Location = new Point(16, 72),
                Size = new Size(pnlNhapDong.ClientSize.Width - 32, 150),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeight = 32,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2,
                EnableHeadersVisualStyles = false,
                Font = new Font("Segoe UI", 9F),
                GridColor = Theme.VienNhat
            };
            dgvHang.ColumnHeadersDefaultCellStyle.BackColor = Theme.HeaderNen;
            dgvHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvHang.AlternatingRowsDefaultCellStyle.BackColor = Theme.DongXen;
            dgvHang.DefaultCellStyle.SelectionBackColor = Theme.DongChon;
            dgvHang.DefaultCellStyle.SelectionForeColor = Theme.ChuChinh;
            TaoCotBangHang();
            dgvHang.CellContentClick += dgvHang_CellContentClick;
            dgvHang.CellEndEdit += dgvHang_CellEndEdit;
            dgvHang.CellValidating += dgvHang_CellValidating;
            pnlNhapDong.Controls.Add(dgvHang);

            lblGiamGia.Text = "Giảm giá chung (%)";
            lblGiamGia.Location = new Point(16, 238);
            nudGiam.Location = new Point(16, 260);
            btnThemDong.Text = "Thêm mặt hàng đã chọn";
            btnThemDong.Size = new Size(210, 36);
            btnThemDong.Location = new Point(pnlNhapDong.ClientSize.Width - 300, 250);
            btnThemDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnXoaDong.Location = new Point(pnlNhapDong.ClientSize.Width - 82, 250);
            btnXoaDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }

        private ComboBox TaoBoLoc(string nhan, int x)
        {
            Label lbl = new Label
            {
                AutoSize = true,
                ForeColor = Theme.Xam,
                Location = new Point(x, 8),
                Text = nhan
            };
            pnlNhapDong.Controls.Add(lbl);
            ComboBox cbo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(x, 30),
                Size = new Size(190, 28),
                Name = "cboLoc" + nhan.Replace(" ", "")
            };
            nhanBoLoc[cbo] = lbl;
            cbo.SelectedValueChanged += delegate { if (!dangNapBoLoc) HienThiDanhSachHang(); };
            return cbo;
        }

        private void TaoCotBangHang()
        {
            colTenHang = CotText("Mặt hàng", "TenHang", 155, true);
            colMau = CotText("Màu sắc", "Mau", 85, true);
            colKichThuoc = CotText("Kích thước", "KichThuoc", 90, true);
            colChatLieu = CotText("Chất liệu", "ChatLieu", 90, true);
            colNuocSX = CotText("Nước SX", "NuocSX", 90, true);
            colTon = CotText(laBan ? "Có thể bán" : "Tồn hiện tại", "Ton", 75, true);
            colDonGia = CotText(laBan ? "Đơn giá bán" : "Đơn giá nhập", "DonGia", 95, laBan);
            colDonGia.DefaultCellStyle.Format = "N0";
            colDonGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuong = CotText("Số lượng", "SoLuong", 65, true);
            colSoLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colGiam = new DataGridViewButtonColumn { Name = "Giam", HeaderText = "", Text = "−", UseColumnTextForButtonValue = true, FillWeight = 28, FlatStyle = FlatStyle.Flat };
            colTang = new DataGridViewButtonColumn { Name = "Tang", HeaderText = "", Text = "+", UseColumnTextForButtonValue = true, FillWeight = 28, FlatStyle = FlatStyle.Flat };
            dgvHang.Columns.AddRange(new DataGridViewColumn[] { colTenHang, colMau, colKichThuoc, colChatLieu, colNuocSX, colTon, colDonGia, colSoLuong, colGiam, colTang });
        }

        private static DataGridViewTextBoxColumn CotText(string tieuDe, string ten, float tyLe, bool chiDoc)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = ten,
                HeaderText = tieuDe,
                FillWeight = tyLe,
                ReadOnly = chiDoc,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
        }

        private void NapBoLocHang()
        {
            dangNapBoLoc = true;
            try
            {
                GanDanhSachBoLoc(cboLocMau, danhSachHang.Select(h => new LuaChon(h.MaMau, h.TenMau)));
                GanDanhSachBoLoc(cboLocKichThuoc, danhSachHang.Select(h => new LuaChon(h.MaKichThuoc, h.TenKichThuoc)));
                GanDanhSachBoLoc(cboLocChatLieu, danhSachHang.Select(h => new LuaChon(h.MaChatLieu, h.TenChatLieu)));
                GanDanhSachBoLoc(cboLocNuocSX, danhSachHang.Select(h => new LuaChon(h.MaNuocSX, h.TenNuocSX)));
            }
            finally { dangNapBoLoc = false; }
        }

        private static void GanDanhSachBoLoc(ComboBox cbo, IEnumerable<LuaChon> luaChon)
        {
            List<LuaChon> ds = new List<LuaChon> { new LuaChon("", "Tất cả") };
            ds.AddRange(luaChon.Where(x => !string.IsNullOrWhiteSpace(x.Ma) && !string.IsNullOrWhiteSpace(x.Ten))
                .GroupBy(x => x.Ma, StringComparer.OrdinalIgnoreCase)
                .Select(g => g.First())
                .OrderBy(x => x.Ten, StringComparer.CurrentCultureIgnoreCase));
            cbo.DisplayMember = "Ten";
            cbo.ValueMember = "Ma";
            cbo.DataSource = ds;
            cbo.SelectedValue = "";
        }

        private void LuuGiaNhapTrenLuoi()
        {
            if (laBan || dgvHang == null || colDonGia == null) return;
            foreach (DataGridViewRow row in dgvHang.Rows)
            {
                HangHoa hang = row.Tag as HangHoa;
                decimal gia;
                if (hang != null && decimal.TryParse(Convert.ToString(row.Cells[colDonGia.Index].Value), NumberStyles.Number, CultureInfo.CurrentCulture, out gia))
                    giaNhapChon[hang.MaHang] = gia;
            }
        }

        private void HienThiDanhSachHang()
        {
            if (flpSanPhamNhap != null)
            {
                HienThiDanhSachNhapHang();
                return;
            }
            if (dgvHang == null) return;
            if (dgvHang.IsCurrentCellInEditMode && !dgvHang.EndEdit()) return;
            LuuGiaNhapTrenLuoi();
            string maMau = Convert.ToString(cboLocMau.SelectedValue);
            string maKichThuoc = Convert.ToString(cboLocKichThuoc.SelectedValue);
            string maChatLieu = Convert.ToString(cboLocChatLieu.SelectedValue);
            string maNuoc = Convert.ToString(cboLocNuocSX.SelectedValue);
            dgvHang.Rows.Clear();

            string tuKhoa = txtTimHang == null ? "" : txtTimHang.Text.Trim();
            foreach (HangHoa hang in danhSachHang.Where(h =>
                (string.IsNullOrEmpty(maMau) || h.MaMau == maMau) &&
                (string.IsNullOrEmpty(maKichThuoc) || h.MaKichThuoc == maKichThuoc) &&
                (string.IsNullOrEmpty(maChatLieu) || h.MaChatLieu == maChatLieu) &&
                (string.IsNullOrEmpty(maNuoc) || h.MaNuocSX == maNuoc) &&
                (string.IsNullOrEmpty(tuKhoa) ||
                    (h.TenHang ?? "").IndexOf(tuKhoa, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    (h.MaHang ?? "").IndexOf(tuKhoa, StringComparison.CurrentCultureIgnoreCase) >= 0)))
            {
                decimal gia = laBan ? controller.LayDonGia(hang) : (giaNhapChon.ContainsKey(hang.MaHang) ? giaNhapChon[hang.MaHang] : controller.LayDonGia(hang));
                int tonHienThi = laBan ? Math.Max(0, Math.Min(hang.SoLuong, hang.ConLaiLo)) : hang.SoLuong;
                int i = dgvHang.Rows.Add(hang.TenHang, hang.TenMau, hang.TenKichThuoc, hang.TenChatLieu, hang.TenNuocSX,
                    laBan ? tonHienThi.ToString() : hang.SoLuong.ToString(), gia, soLuongChon.ContainsKey(hang.MaHang) ? soLuongChon[hang.MaHang] : 0, "−", "+");
                dgvHang.Rows[i].Tag = hang;
                dgvHang.Rows[i].Height = 30;
            }
        }

        private void dgvHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dangXem || e.RowIndex < 0) return;
            if (!laBan)
            {
                return;
            }
            if (e.ColumnIndex != colGiam.Index && e.ColumnIndex != colTang.Index) return;
            DataGridViewRow row = dgvHang.Rows[e.RowIndex];
            HangHoa hang = row.Tag as HangHoa;
            if (hang == null) return;
            if (controller.Dong.Any(d => string.Equals(d.MaHang, hang.MaHang, StringComparison.OrdinalIgnoreCase)))
            {
                TienIch.Canh("Mặt hàng này đã có trong hoá đơn. Hãy xoá dòng cũ trước khi chọn lại số lượng.");
                return;
            }

            int soLuong = Convert.ToInt32(row.Cells[colSoLuong.Index].Value);
            if (e.ColumnIndex == colGiam.Index) soLuong = Math.Max(0, soLuong - 1);
            else
            {
                int toiDa = laBan ? Math.Max(0, Math.Min(hang.SoLuong, hang.ConLaiLo)) : int.MaxValue;
                if (soLuong >= toiDa)
                {
                    if (laBan) TienIch.Canh("Số lượng đã đạt mức tồn có thể bán của mặt hàng này.");
                    return;
                }
                soLuong++;
            }
            soLuongChon[hang.MaHang] = soLuong;
            row.Cells[colSoLuong.Index].Value = soLuong;
        }

        private void dgvHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (laBan || e.RowIndex < 0 || e.ColumnIndex != colDonGia.Index) return;
            HangHoa hang = dgvHang.Rows[e.RowIndex].Tag as HangHoa;
            decimal gia;
            if (hang != null && decimal.TryParse(Convert.ToString(dgvHang.Rows[e.RowIndex].Cells[colDonGia.Index].Value), NumberStyles.Number, CultureInfo.CurrentCulture, out gia))
                giaNhapChon[hang.MaHang] = gia;
        }

        private void dgvHang_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (laBan || e.RowIndex < 0 || e.ColumnIndex != colDonGia.Index) return;
            decimal gia;
            if (!decimal.TryParse(Convert.ToString(e.FormattedValue), NumberStyles.Number, CultureInfo.CurrentCulture, out gia) || gia <= 0)
            {
                TienIch.Canh("Đơn giá nhập phải là số lớn hơn 0.");
                e.Cancel = true;
            }
        }

        private void TaiDanhSach()
        {
            TienIch.NapLuoi(lvDs, controller.DanhSach(), "So", "Ngay", "TongTien");
            lvDs.SelectedItems.Clear();
        }

        private void BamHoaDonMoi()
        {
            if (!XacNhanBoHoaDonDangSoan()) return;
            try
            {
                HoaDonMoi();
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }

        private bool XacNhanBoHoaDonDangSoan()
        {
            if (!dangXem && SoDongHienTai() > 0)
            {
                return TienIch.Hoi("Hoá đơn đang soạn chưa lưu sẽ bị bỏ. Tiếp tục?");
            }
            return true;
        }

        private void HoaDonMoi()
        {
            XoaDanhSachDong();
            NapDong();
            txtSo.Text = controller.SinhMa();
            dtpNgay.Value = DateTime.Today;
            cboNV.SelectedIndex = -1;
            if (PhienDangNhap.HienTai != null) TienIch.ChonGiaTri(cboNV, PhienDangNhap.HienTai.MaNV);
            cboNV.Enabled = false;
            if (laBan)
            {
                TienIch.ChonGiaTri(cboDoiTac, KhachHang.MaKhachLe);
            }
            else cboDoiTac.SelectedIndex = -1;
            soLuongChon.Clear();
            giaNhapChon.Clear();
            HienThiDanhSachHang();
            nudGiam.Value = 0;
            DatCheDoXem(false);
            CapNhatTong();
            lvDs.SelectedItems.Clear();
            lblTrangThai.Text = laBan
                ? "Đang lập hoá đơn bán mới. Lọc mặt hàng, dùng +/− để chọn số lượng; mức bán theo tồn lô."
                : "Đang lập hoá đơn nhập mới. Tìm/lọc mặt hàng, bấm + để thêm; sửa số lượng, đơn giá và giảm giá ở bảng chi tiết.";
        }

        private void DatCheDoXem(bool xem)
        {
            dangXem = xem;
            pnlThongTin.Enabled = !xem;
            if (splitNhapHang != null)
            {
                splitNhapHang.Panel1.Enabled = !xem;
                splitNhapHang.Panel2.Enabled = !xem;
            }
            else pnlNhapDong.Enabled = !xem;
            btnLuu.Enabled = !xem;
            btnXoaHD.Enabled = xem;
            if (xem && laBan && !PhanQuyen.CoQuyen(Quyen.QuanLyTaiKhoan)) btnXoaHD.Enabled = false;
        }

        private void ThemCacDongDaChon()
        {
            if (dangXem) return;
            if (dgvHang.IsCurrentCellInEditMode) dgvHang.EndEdit();
            LuuGiaNhapTrenLuoi();

            List<IChiTietHoaDon> dongDaThem = new List<IChiTietHoaDon>();
            List<string> maDaThem = new List<string>();
            foreach (DataGridViewRow row in dgvHang.Rows)
            {
                HangHoa hang = row.Tag as HangHoa;
                if (hang == null) continue;
                int soLuong = Convert.ToInt32(row.Cells[colSoLuong.Index].Value);
                if (soLuong <= 0) continue;

                decimal donGia = controller.LayDonGia(hang);
                if (!laBan && !decimal.TryParse(Convert.ToString(row.Cells[colDonGia.Index].Value), NumberStyles.Number, CultureInfo.CurrentCulture, out donGia))
                {
                    TienIch.Canh("Đơn giá nhập của mặt hàng " + hang.TenHang + " không hợp lệ.");
                    RollbackDong(dongDaThem);
                    return;
                }

                string loi = controller.ThemDong(txtSo.Text.Trim(), hang, soLuong, donGia, (double)nudGiam.Value);
                if (loi != null)
                {
                    RollbackDong(dongDaThem);
                    TienIch.Canh(loi);
                    return;
                }
                dongDaThem.Add(controller.Dong.Last());
                maDaThem.Add(hang.MaHang);
            }

            if (dongDaThem.Count == 0)
            {
                TienIch.Canh("Hãy tăng số lượng ít nhất một mặt hàng bằng nút + trước khi thêm.");
                return;
            }

            foreach (string ma in maDaThem) soLuongChon[ma] = 0;
            NapDong();
            CapNhatTong();
            nudGiam.Value = 0;
            HienThiDanhSachHang();
        }

        private void RollbackDong(IEnumerable<IChiTietHoaDon> cacDong)
        {
            foreach (IChiTietHoaDon dong in cacDong) controller.XoaDong(dong);
            NapDong();
            CapNhatTong();
        }

        private void XoaDong()
        {
            if (dangXem || lvDong.SelectedItems.Count == 0) { if (!dangXem) TienIch.Canh("Hãy chọn dòng cần xoá."); return; }
            if (!controller.XoaDong(lvDong.SelectedItems[0].Tag as IChiTietHoaDon)) return;
            NapDong();
            CapNhatTong();
        }

        private void CapNhatTong()
        {
            decimal tong = controller.TongTien;
            lblTong.Text = "Tổng tiền: " + tong.ToString("N0") + " đ";
        }

        private bool dangChonDs;

        private void lvDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangChonDs || lvDs.SelectedItems.Count == 0) return;
            if (!XacNhanBoHoaDonDangSoan()) return;

            try
            {
                string so = lvDs.SelectedItems[0].Text;
                XemHoaDon(so);
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }

        private void XemHoaDon(string so)
        {
            IHoaDon hd = controller.Xem(so);
            if (hd == null)
            {
                TienIch.Canh("Hoá đơn không còn tồn tại.");
                TaiDanhSach();
                return;
            }

            txtSo.Text = so;
            dtpNgay.Value = hd.Ngay;
            TienIch.ChonGiaTri(cboNV, hd.MaNV);
            TienIch.ChonGiaTri(cboDoiTac, hd.MaDoiTac);

            NapDong();
            DatCheDoXem(true);
            lblTong.Text = "Tổng tiền: " + hd.TongTien.ToString("N0") + " đ";
            lblTrangThai.Text = "Đang xem hoá đơn " + so + " đã lưu. Bấm \"+ Hoá đơn mới\" để lập hoá đơn khác.";
        }

        private void ChonDongDanhSach(string so)
        {
            foreach (ListViewItem dong in lvDs.Items)
            {
                if (dong.Text == so)
                {
                    dangChonDs = true;
                    try
                    {
                        lvDs.SelectedItems.Clear();
                        dong.Selected = true;
                        dong.EnsureVisible();
                    }
                    finally
                    {
                        dangChonDs = false;
                    }
                    break;
                }
            }
        }

        private void Luu()
        {
            if (dangXem) return;

            try
            {
                controller.Luu(txtSo.Text.Trim(), dtpNgay.Value.Date, TienIch.LayMa(cboNV),
                    TienIch.LayMa(cboDoiTac));

                TaiDanhSach();
                NapHang();
                string soHoaDon = txtSo.Text.Trim();
                XemHoaDon(soHoaDon);
                ChonDongDanhSach(soHoaDon);
                TienIch.ThongBao("Đã lưu hoá đơn " + soHoaDon + ".\n" + lblTong.Text);
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }

        private void btnKhachMoi_Click(object sender, EventArgs e)
        {
            if (!laBan || dangXem) return;
            using (FrmKhachMoi form = new FrmKhachMoi((BanHangBll)controller))
            {
                if (form.ShowDialog(this) != DialogResult.OK) return;
                string ma = form.KhachDuocTao.MaKhach;
                cboDoiTac.DataSource = null;
                cboDoiTac.DisplayMember = "Ten";
                cboDoiTac.ValueMember = "Ma";
                cboDoiTac.DataSource = controller.DanhSachDoiTac();
                TienIch.ChonGiaTri(cboDoiTac, ma);
            }
        }

        private void XoaHoaDon()
        {
            if (!dangXem) return;

            string so = txtSo.Text.Trim();
            if (!TienIch.Hoi("Xoá hoá đơn " + so + "?\n" + (laBan ? "Số lượng hàng của hoá đơn này sẽ được cộng trả lại vào tồn kho." : "Số lượng hàng của hoá đơn này sẽ bị trừ khỏi tồn kho."))) return;

            try
            {
                controller.Xoa(so);
                TaiDanhSach();
                NapHang();
                HoaDonMoi();
                TienIch.ThongBao("Đã xoá hoá đơn " + so + ".");
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }
    }
}








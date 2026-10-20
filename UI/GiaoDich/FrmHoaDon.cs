using System;
using System.Collections.Generic;
using System.Drawing;
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

        public FrmHoaDon()
        {
            InitializeComponent();
        }

        protected FrmHoaDon(HoaDonBll controller) : this()
        {
            this.controller = controller;
            this.laBan = (controller.Loai == LoaiHoaDon.Ban);
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
            TienIch.GanLuoi(lvDong, true);
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
            lblDonGia.Text = laBan ? "Đơn giá bán (tự động)" : "Đơn giá nhập";
            nudDonGia.Enabled = controller.ChoSuaDonGia;
        }

        private void NapDong()
        {
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
        private void cboHang_SelectedIndexChanged(object sender, EventArgs e) { CapNhatThongTinHang(); }
        private void btnThemDong_Click(object sender, EventArgs e) { ThemDong(); }
        private void btnXoaDong_Click(object sender, EventArgs e) { XoaDong(); }
        private void btnLuu_Click(object sender, EventArgs e) { Luu(); }
        private void btnXoaHD_Click(object sender, EventArgs e) { XoaHoaDon(); }

        private void NapDuLieuNen()
        {
            cboNV.DisplayMember = "Ten";
            cboNV.ValueMember = "Ma";
            cboNV.DataSource = controller.DanhSachNhanVien();
            cboNV.SelectedIndex = -1;

            cboDoiTac.DisplayMember = "Ten";
            cboDoiTac.ValueMember = "Ma";
            cboDoiTac.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDoiTac.DataSource = controller.DanhSachDoiTac();
            cboDoiTac.SelectedIndex = -1;

            NapHang();
        }

        private void NapHang()
        {
            cboHang.DisplayMember = "TenChonHang";
            cboHang.ValueMember = "MaHang";
            cboHang.DataSource = controller.DanhSachHangHoa();
            cboHang.SelectedIndex = -1;
            CapNhatThongTinHang();
        }

        private void TaiDanhSach()
        {
            TienIch.NapLuoi(lvDs, controller.DanhSach(), "So", "Ngay", "TongTien");
            lvDs.SelectedItems.Clear();
        }

        private void CapNhatThongTinHang()
        {
            HangHoa r = cboHang.SelectedItem as HangHoa;
            if (r == null)
            {
                lblTon.Text = "";
                txtMau.Text = "";
                txtKichThuoc.Text = "";
                txtChatLieu.Text = "";
                txtNuocSX.Text = "";
                nudDonGia.Value = 0;
                return;
            }

            int ton = r.SoLuong;
            txtMau.Text = r.TenMau ?? "";
            txtKichThuoc.Text = r.TenKichThuoc ?? "";
            txtChatLieu.Text = r.TenChatLieu ?? "";
            txtNuocSX.Text = r.TenNuocSX ?? "";
            lblTon.Text = laBan
                ? "Tồn kho: " + ton + " (lô giá hiện tại còn " + r.ConLaiLo + ")"
                : "Tồn kho: " + ton;
            lblTon.ForeColor = (laBan && ton <= 0) ? Theme.Nguy : Theme.Xam;

            decimal gia = controller.LayDonGia(r);
            if (gia > nudDonGia.Maximum) gia = nudDonGia.Maximum;
            nudDonGia.Value = gia;
            if (!laBan && gia == 0) nudDonGia.Focus();
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
            if (laBan)
            {
                TienIch.ChonGiaTri(cboDoiTac, KhachHang.MaKhachLe);
            }
            else cboDoiTac.SelectedIndex = -1;
            cboHang.SelectedIndex = -1;
            nudSL.Value = 1;
            nudGiam.Value = 0;
            DatCheDoXem(false);
            CapNhatTong();
            lvDs.SelectedItems.Clear();
            lblTrangThai.Text = laBan
                ? "Đang lập hoá đơn bán mới. Giá bán lấy từ danh mục hàng hoá."
                : "Đang lập hoá đơn nhập mới. Lưu xong, giá nhập và giá bán (110%) của hàng hoá sẽ tự cập nhật.";
        }

        private void DatCheDoXem(bool xem)
        {
            dangXem = xem;
            pnlThongTin.Enabled = !xem;
            pnlNhapDong.Enabled = !xem;
            btnLuu.Enabled = !xem;
            btnXoaHD.Enabled = xem;
        }

        private void ThemDong()
        {
            if (dangXem) return;

            HangHoa hang = cboHang.SelectedItem as HangHoa;
            if (hang == null)
            {
                TienIch.Canh("Vui lòng chọn mặt hàng.");
                cboHang.Focus();
                return;
            }

            decimal donGia = nudDonGia.Value;
            string loi = controller.ThemDong(txtSo.Text.Trim(), hang, (int)nudSL.Value, donGia, (double)nudGiam.Value);
            if (loi != null)
            {
                TienIch.Canh(loi);
                if (!laBan && donGia <= 0) nudDonGia.Focus();
                return;
            }

            NapDong();
            CapNhatTong();
            nudSL.Value = 1;
            nudGiam.Value = 0;
            cboHang.Focus();
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








using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmHoaDon : Form
    {
        private readonly LoaiHoaDon loai;
        private readonly bool laBan;
        private readonly DataTable dtDong = new DataTable();
        private bool dangXem;

        public FrmHoaDon()
        {
            InitializeComponent();
        }

        public FrmHoaDon(LoaiHoaDon loai) : this()
        {
            this.loai = loai;
            this.laBan = (loai == LoaiHoaDon.Ban);
            ApDungLoai();

            dtDong.Columns.Add("MaHang", typeof(string));
            dtDong.Columns.Add("TenHang", typeof(string));
            dtDong.Columns.Add("SoLuong", typeof(int));
            dtDong.Columns.Add("DonGia", typeof(decimal));
            dtDong.Columns.Add("GiamGia", typeof(double));
            dtDong.Columns.Add("ThanhTien", typeof(decimal));

            TienIch.GanLuoi(lvDs);
            TienIch.GanLuoi(lvDong);
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
            lblDoiTac.Text = laBan ? "Khách hàng *" : "Nhà cung cấp *";
            lblDonGia.Text = laBan ? "Đơn giá bán (tự động)" : "Đơn giá nhập";
            nudDonGia.Enabled = !laBan;
        }

        private void NapDong()
        {
            TienIch.NapLuoi(lvDong, dtDong.DefaultView);
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
            cboNV.DataSource = Db.Query("SELECT MaNV AS Ma, MaNV + N' - ' + TenNV AS Ten FROM NhanVien ORDER BY MaNV");
            cboNV.SelectedIndex = -1;

            string sqlDoiTac = laBan
                ? "SELECT MaKhach AS Ma, MaKhach + N' - ' + TenKhach AS Ten FROM KhachHang ORDER BY MaKhach"
                : "SELECT MaNCC AS Ma, MaNCC + N' - ' + TenNCC AS Ten FROM NhaCungCap ORDER BY MaNCC";
            cboDoiTac.DisplayMember = "Ten";
            cboDoiTac.ValueMember = "Ma";
            cboDoiTac.DataSource = Db.Query(sqlDoiTac);
            cboDoiTac.SelectedIndex = -1;

            NapHang();
        }

        private void NapHang()
        {
            cboHang.DisplayMember = "Ten";
            cboHang.ValueMember = "Ma";
            cboHang.DataSource = Db.Query(
                "SELECT MaHang AS Ma, MaHang + N' - ' + TenHang AS Ten, TenHang, " +
                "ISNULL(SoLuong, 0) AS Ton, ISNULL(GiaNhap, 0) AS GiaNhap, ISNULL(DonGiaBan, 0) AS DonGiaBan " +
                "FROM DMHangHoa ORDER BY MaHang");
            cboHang.SelectedIndex = -1;
            CapNhatThongTinHang();
        }

        private void TaiDanhSach()
        {
            TienIch.NapLuoi(lvDs, HoaDonDal.DanhSach(loai));
            lvDs.SelectedItems.Clear();
        }

        private static decimal Dec(object v)
        {
            if (v == null || v is DBNull) return 0m;
            return Convert.ToDecimal(v);
        }

        private void CapNhatThongTinHang()
        {
            DataRowView r = cboHang.SelectedItem as DataRowView;
            if (r == null)
            {
                lblTon.Text = "";
                if (laBan) nudDonGia.Value = 0;
                return;
            }

            int ton = Convert.ToInt32(r["Ton"]);
            lblTon.Text = "Tồn kho: " + ton;
            lblTon.ForeColor = (laBan && ton <= 0) ? Theme.Nguy : Theme.Xam;

            decimal gia = Dec(laBan ? r["DonGiaBan"] : r["GiaNhap"]);
            if (gia > nudDonGia.Maximum) gia = nudDonGia.Maximum;
            nudDonGia.Value = gia;
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
            if (!dangXem && dtDong.Rows.Count > 0)
            {
                return TienIch.Hoi("Hoá đơn đang soạn chưa lưu sẽ bị bỏ. Tiếp tục?");
            }
            return true;
        }

        private void HoaDonMoi()
        {
            dtDong.Clear();
            NapDong();
            txtSo.Text = HoaDonBll.SinhMa(loai);
            dtpNgay.Value = DateTime.Today;
            cboNV.SelectedIndex = -1;
            cboDoiTac.SelectedIndex = -1;
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

            DataRowView r = cboHang.SelectedItem as DataRowView;
            if (r == null)
            {
                TienIch.Canh("Vui lòng chọn mặt hàng.");
                cboHang.Focus();
                return;
            }

            string ma = Convert.ToString(r["Ma"]);
            int sl = (int)nudSL.Value;
            decimal donGia = nudDonGia.Value;
            double giam = (double)nudGiam.Value;
            int ton = Convert.ToInt32(r["Ton"]);

            foreach (DataRow d in dtDong.Rows)
            {
                if (string.Equals(Convert.ToString(d["MaHang"]), ma, StringComparison.OrdinalIgnoreCase))
                {
                    TienIch.Canh("Mặt hàng này đã có trong hoá đơn. Hãy xoá dòng cũ trước nếu muốn đổi số lượng.");
                    return;
                }
            }

            if (sl <= 0)
            {
                TienIch.Canh("Số lượng phải lớn hơn 0.");
                return;
            }
            if (laBan && sl > ton)
            {
                TienIch.Canh("Tồn kho chỉ còn " + ton + ", không đủ để bán " + sl + ".");
                return;
            }
            if (!laBan && donGia <= 0)
            {
                TienIch.Canh("Đơn giá nhập phải lớn hơn 0.");
                nudDonGia.Focus();
                return;
            }

            decimal thanhTien = Math.Round(sl * donGia * (1m - (decimal)giam / 100m), 0);
            dtDong.Rows.Add(ma, Convert.ToString(r["TenHang"]), sl, donGia, giam, thanhTien);

            NapDong();
            CapNhatTong();
            nudSL.Value = 1;
            nudGiam.Value = 0;
            cboHang.Focus();
        }

        private void XoaDong()
        {
            if (dangXem) return;

            if (lvDong.SelectedItems.Count == 0)
            {
                TienIch.Canh("Hãy chọn dòng cần xoá.");
                return;
            }
            DataRowView rv = lvDong.SelectedItems[0].Tag as DataRowView;
            if (rv == null) return;

            rv.Row.Delete();
            dtDong.AcceptChanges();
            NapDong();
            CapNhatTong();
        }

        private void CapNhatTong()
        {
            decimal tong = 0m;
            foreach (DataRow r in dtDong.Rows)
            {
                tong += Convert.ToDecimal(r["ThanhTien"]);
            }
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
            DataRow dau = HoaDonDal.Dau(loai, so);
            if (dau == null)
            {
                TienIch.Canh("Hoá đơn không còn tồn tại.");
                TaiDanhSach();
                return;
            }

            txtSo.Text = so;
            dtpNgay.Value = Convert.ToDateTime(dau[laBan ? "NgayBan" : "NgayNhap"]);
            TienIch.ChonGiaTri(cboNV, dau["MaNV"]);
            TienIch.ChonGiaTri(cboDoiTac, dau[laBan ? "MaKhach" : "MaNCC"]);

            dtDong.Clear();
            DataTable ct = HoaDonDal.ChiTiet(loai, so);
            foreach (DataRow r in ct.Rows)
            {
                dtDong.Rows.Add(
                    Convert.ToString(r["MaHang"]),
                    Convert.ToString(r["TenHang"]),
                    Convert.ToInt32(r["SoLuong"]),
                    Math.Round(Dec(r["DonGia"]), 0),
                    Convert.ToDouble(r["GiamGia"]),
                    Dec(r["ThanhTien"]));
            }

            NapDong();
            DatCheDoXem(true);
            decimal tong = Dec(dau["TongTien"]);
            lblTong.Text = "Tổng tiền: " + tong.ToString("N0") + " đ";
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
                HoaDon hd = new HoaDon();
                hd.Loai = loai;
                hd.So = txtSo.Text.Trim();
                hd.Ngay = dtpNgay.Value.Date;
                hd.MaNV = TienIch.LayMa(cboNV);
                hd.MaDoiTac = TienIch.LayMa(cboDoiTac);

                foreach (DataRow r in dtDong.Rows)
                {
                    DongHoaDon d = new DongHoaDon();
                    d.MaHang = Convert.ToString(r["MaHang"]);
                    d.SoLuong = Convert.ToInt32(r["SoLuong"]);
                    d.DonGia = Convert.ToDecimal(r["DonGia"]);
                    d.GiamGia = Convert.ToDouble(r["GiamGia"]);
                    hd.Dong.Add(d);
                }

                HoaDonBll.Luu(hd);

                TaiDanhSach();
                NapHang();
                XemHoaDon(hd.So);
                ChonDongDanhSach(hd.So);
                TienIch.ThongBao("Đã lưu hoá đơn " + hd.So + ".\n" + lblTong.Text);
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }

        private void XoaHoaDon()
        {
            if (!dangXem) return;

            string so = txtSo.Text.Trim();
            string hauQua = laBan
                ? "Số lượng hàng của hoá đơn này sẽ được cộng trả lại vào tồn kho."
                : "Số lượng hàng của hoá đơn này sẽ bị trừ khỏi tồn kho.";
            if (!TienIch.Hoi("Xoá hoá đơn " + so + "?\n" + hauQua)) return;

            try
            {
                HoaDonBll.Xoa(loai, so);
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

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;

namespace QuanLyBanTuBep.UI
{
    public partial class FormMain : Form
    {
        private class MucMenu
        {
            public string TieuDe;
            public Func<Form> TaoTrang;
            public ToolStripMenuItem Item;
        }

        private readonly Dictionary<ToolStripItem, MucMenu> menu = new Dictionary<ToolStripItem, MucMenu>();
        private ToolStripMenuItem dangMo;
        private Form trangHienTai;
        private ToolStripMenuItem mnuQuanLyTaiKhoan, mnuDoiMatKhau, mnuDangXuat;
        public bool YeuCauDangXuat { get; private set; }

        public FormMain()
        {
            PhienDangNhap.YeuCauHieuLuc();
            InitializeComponent();
            // Host each functional form inside the content panel so it cannot
            // overlap the shared menu, header, or status bar.
            ToolStripManager.Renderer = new Theme.MenuRenderer();
            DangKyMenu();
            ThemMenuTaiKhoan();
            ApDungPhanQuyen();
            timGio_Tick(null, EventArgs.Empty);

            MoTrang(mnuTrangChu);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.H))
            {
                MoTrang(mnuHangHoa);
                return true;
            }
            if (keyData == (Keys.Control | Keys.N))
            {
                MoTrang(mnuNhapHang);
                return true;
            }
            if (keyData == (Keys.Control | Keys.B))
            {
                MoTrang(mnuBanHang);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void DangKyMenu()
        {
            Muc(mnuTrangChu, "TRANG CHỦ", delegate { return new FrmTrangChu(); });

            Muc(mnuHangHoa, "QUẢN LÝ HÀNG HOÁ", delegate { return new FrmHangHoa(); });
            Muc(mnuNhanVien, "QUẢN LÝ NHÂN VIÊN", delegate { return new FrmNhanVien(); });
            Muc(mnuKhachHang, "QUẢN LÝ KHÁCH HÀNG", delegate { return new FrmKhachHang(); });
            Muc(mnuNhaCungCap, "QUẢN LÝ NHÀ CUNG CẤP", delegate { return new FrmNhaCungCap(); });
            Muc(mnuKichThuoc, "DANH MỤC KÍCH THƯỚC", delegate { return new FrmKichThuoc(); });
            Muc(mnuChatLieu, "DANH MỤC CHẤT LIỆU", delegate { return new FrmChatLieu(); });
            Muc(mnuMauSac, "DANH MỤC MÀU SẮC", delegate { return new FrmMauSac(); });
            Muc(mnuNuocSanXuat, "DANH MỤC NƯỚC SẢN XUẤT", delegate { return new FrmNuocSanXuat(); });
            Muc(mnuCongViec, "DANH MỤC CÔNG VIỆC", delegate { return new FrmCongViec(); });

            Muc(mnuNhapHang, "NHẬP HÀNG", delegate { return new FrmNhapHang(); });
            Muc(mnuBanHang, "BÁN HÀNG", delegate { return new FrmBanHang(); });

            Muc(mnuTimSanPham, "TÌM KIẾM SẢN PHẨM", delegate { return new FrmTimSanPham(); });
            Muc(mnuTimHoaDonBan, "TÌM KIẾM HOÁ ĐƠN BÁN", delegate { return new FrmTimHoaDonBan(); });
            Muc(mnuTop3, "BÁO CÁO: TOP 3 SẢN PHẨM BÁN CHẠY / QUÝ", delegate { return new FrmTop3SanPham(); });
            Muc(mnuHoaDonNhapNV, "BÁO CÁO: HOÁ ĐƠN NHẬP THEO NHÂN VIÊN", delegate { return new FrmHoaDonNhapTheoNV(); });
            Muc(mnuTop5HoaDonBan, "BÁO CÁO: 5 HOÁ ĐƠN BÁN THẤP NHẤT / QUÝ", delegate { return new FrmTop5HoaDonBan(); });
            Muc(mnuKhachHangTheoThang, "BÁO CÁO: KHÁCH HÀNG MUA THEO THÁNG", delegate { return new FrmKhachHangTheoThang(); });

            Nhanh(tsBanHang, mnuBanHang);
            Nhanh(tsNhapHang, mnuNhapHang);
            Nhanh(tsHangHoa, mnuHangHoa);
            Nhanh(tsTimSanPham, mnuTimSanPham);
            Nhanh(tsBaoCao, mnuTop3);
        }

        private void Muc(ToolStripMenuItem item, string tieuDe, Func<Form> tao)
        {
            MucMenu m = new MucMenu();
            m.TieuDe = tieuDe;
            m.TaoTrang = tao;
            m.Item = item;
            menu[item] = m;
        }

        private void Nhanh(ToolStripItem nut, ToolStripMenuItem item)
        {
            menu[nut] = menu[item];
        }

        private void mnuMuc_Click(object sender, EventArgs e)
        {
            MoTrang((ToolStripItem)sender);
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timGio_Tick(object sender, EventArgs e)
        {
            lblGio.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private static void DanhDau(ToolStripMenuItem item, bool dangMo)
        {
            ToolStripItem x = item;
            while (x != null)
            {
                ToolStripMenuItem m = x as ToolStripMenuItem;
                if (m != null) m.Checked = dangMo;
                x = x.OwnerItem;
            }
        }

        private void MoTrang(ToolStripItem nguon)
        {
            MucMenu muc;
            if (!menu.TryGetValue(nguon, out muc)) return;
            try { PhanQuyen.YeuCau(QuyenCuaMuc(muc.Item)); }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
                return;
            }
            if (muc.Item == dangMo && trangHienTai != null && !trangHienTai.IsDisposed) return;

            // Construct the destination before closing the current screen.
            // This preserves the current screen if its constructor fails.
            Form moi;
            try
            {
                moi = muc.TaoTrang();
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
                return;
            }

            // Only one functional page is shown at a time.
            // Close() respects FormClosing cancellation; unlike Dispose(),
            // it allows a screen to protect unsaved input.
            if (trangHienTai != null && !trangHienTai.IsDisposed)
            {
                Form cu = trangHienTai;
                cu.Close();
                if (!cu.IsDisposed)
                {
                    moi.Dispose();
                    return;
                }
            }

            try
            {
                moi.TopLevel = false;
                moi.FormBorderStyle = FormBorderStyle.None;
                moi.ControlBox = false;
                moi.MinimizeBox = false;
                moi.MaximizeBox = false;
                moi.Dock = DockStyle.Fill;
                moi.FormClosed += Trang_FormClosed;
                pnlNoiDung.Controls.Add(moi);
                trangHienTai = moi;
                moi.Show();

                if (dangMo != null) DanhDau(dangMo, false);
                dangMo = muc.Item;
                DanhDau(dangMo, true);
                lblTieuDe.Text = muc.TieuDe;
            }
            catch (Exception ex)
            {
                moi.Dispose();
                TienIch.XuLyLoi(ex);
            }
        }

        private Quyen QuyenCuaMuc(ToolStripMenuItem item)
        {
            if (item == mnuTrangChu) return Quyen.XemTrangChu;
            if (item == mnuBanHang) return Quyen.BanHang;
            if (item == mnuNhapHang) return Quyen.NhapHang;
            if (item == mnuHangHoa) return Quyen.QuanLyHangHoa;
            if (item == mnuNhanVien) return Quyen.QuanLyNhanVien;
            if (item == mnuKhachHang) return Quyen.QuanLyKhachHang;
            if (item == mnuNhaCungCap) return Quyen.QuanLyNhaCungCap;
            if (item == mnuKichThuoc || item == mnuChatLieu || item == mnuMauSac || item == mnuNuocSanXuat) return Quyen.XemDanhMuc;
            if (item == mnuTimSanPham) return Quyen.TimSanPham;
            if (item == mnuTimHoaDonBan) return Quyen.XemLichSuBan;
            return Quyen.BaoCaoDoanhThu;
        }

        private void ThemMenuTaiKhoan()
        {
            mnuQuanLyTaiKhoan = new ToolStripMenuItem("Quản lý tài khoản");
            mnuDoiMatKhau = new ToolStripMenuItem("Đổi mật khẩu");
            mnuDangXuat = new ToolStripMenuItem("Đăng xuất");
            mnuQuanLyTaiKhoan.Click += delegate { MoHopThoaiAnToan(delegate { return new FrmTaiKhoan(); }); };
            mnuDoiMatKhau.Click += delegate { MoHopThoaiAnToan(delegate { return new FrmDoiMatKhau(); }); };
            mnuDangXuat.Click += delegate { DangXuat(); };
            mnuHeThong.DropDownItems.Insert(0, new ToolStripSeparator());
            mnuHeThong.DropDownItems.Insert(0, mnuDangXuat);
            mnuHeThong.DropDownItems.Insert(0, mnuDoiMatKhau);
            mnuHeThong.DropDownItems.Insert(0, mnuQuanLyTaiKhoan);
        }

        private void ApDungPhanQuyen()
        {
            bool admin = PhanQuyen.CoQuyen(Quyen.QuanLyTaiKhoan);
            mnuNhanVien.Visible = admin; mnuNhaCungCap.Visible = admin; mnuHangHoa.Visible = admin;
            mnuKichThuoc.Visible = true; mnuChatLieu.Visible = true; mnuMauSac.Visible = true;
            mnuNuocSanXuat.Visible = true; mnuCongViec.Visible = admin; sepDanhMuc.Visible = admin;
            mnuNhapHang.Visible = admin; mnuGiaoDich.Visible = admin; mnuBaoCao.Visible = admin;
            tsNhapHang.Visible = admin; tsHangHoa.Visible = admin; tsBaoCao.Visible = admin;
            mnuQuanLyTaiKhoan.Visible = admin;
        }

        private void DangXuat()
        {
            if (trangHienTai != null && !trangHienTai.IsDisposed)
            {
                Form cu = trangHienTai;
                cu.Close();
                if (!cu.IsDisposed) return;
            }
            PhienDangNhap.DangXuat();
            YeuCauDangXuat = true;
            Close();
        }

        private void MoHopThoaiAnToan(Func<Form> tao)
        {
            try { using (Form f = tao()) f.ShowDialog(this); }
            catch (Exception ex) { TienIch.XuLyLoi(ex); }
        }

        private void Trang_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Also handle a child closed independently of navbar navigation.
            if (ReferenceEquals(trangHienTai, sender)) trangHienTai = null;
            if (dangMo != null) DanhDau(dangMo, false);
            dangMo = null;
            lblTieuDe.Text = "";
        }
    }
}

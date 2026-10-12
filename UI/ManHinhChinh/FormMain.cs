using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        public FormMain()
        {
            InitializeComponent();
            ToolStripManager.Renderer = new Theme.MenuRenderer();
            DangKyMenu();
            timGio_Tick(null, EventArgs.Empty);

            MoTrang(mnuTrangChu);
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
            MucMenu muc = menu[nguon];
            if (muc.Item == dangMo) return;

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

            List<Control> cu = new List<Control>();
            foreach (Control c in pnlNoiDung.Controls) cu.Add(c);
            foreach (Control c in cu)
            {
                pnlNoiDung.Controls.Remove(c);
                c.Dispose();
            }

            TienIch.Nhung(moi, pnlNoiDung);

            if (dangMo != null) DanhDau(dangMo, false);
            dangMo = muc.Item;
            DanhDau(dangMo, true);
            lblTieuDe.Text = muc.TieuDe;
        }
    }
}

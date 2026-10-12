using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanTuBep.DAL;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmTrangChu : Form
    {
        private const int NguongTon = 3;

        public FrmTrangChu()
        {
            InitializeComponent();
            TienIch.GanLuoi(lvSapHet);
            TienIch.GanLuoi(lvGanDay);
            lblTieuDeSapHet.Text = "Hàng sắp hết (tồn từ " + NguongTon + " trở xuống)";
            Load += delegate { TaiDuLieu(); };
        }

        public void LamMoi()
        {
            TaiDuLieu();
        }

        private void TaiDuLieu()
        {
            try
            {
                lblSoMatHang.Text = Convert.ToDecimal(Db.Scalar("SELECT COUNT(*) FROM DMHangHoa")).ToString("N0");
                lblSoTonKho.Text = Convert.ToDecimal(Db.Scalar("SELECT ISNULL(SUM(SoLuong), 0) FROM DMHangHoa")).ToString("N0");
                lblSoHoaDon.Text = Convert.ToDecimal(Db.Scalar("SELECT COUNT(*) FROM HoaDonBan")).ToString("N0");
                lblSoDoanhThu.Text = Convert.ToDecimal(Db.Scalar("SELECT ISNULL(SUM(TongTien), 0) FROM HoaDonBan")).ToString("N0");

                TienIch.NapLuoi(lvSapHet, Db.Query(
                    "SELECT MaHang, TenHang, SoLuong FROM DMHangHoa WHERE ISNULL(SoLuong, 0) <= @n ORDER BY SoLuong, MaHang",
                    Db.P("@n", NguongTon)));
                lvSapHet.SelectedItems.Clear();

                TienIch.NapLuoi(lvGanDay, Db.Query(
                    "SELECT TOP 8 h.SoHDB, h.NgayBan, k.TenKhach, h.TongTien " +
                    "FROM HoaDonBan h LEFT JOIN KhachHang k ON h.MaKhach = k.MaKhach " +
                    "ORDER BY h.NgayBan DESC, h.SoHDB DESC"));
                lvGanDay.SelectedItems.Clear();
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }
    }
}

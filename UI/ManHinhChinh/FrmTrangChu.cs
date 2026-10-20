using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmTrangChu : Form
    {
        private readonly TrangChuBll bll = new TrangChuBll();

        public FrmTrangChu()
        {
            InitializeComponent();
            TienIch.GanLuoi(lvSapHet);
            TienIch.GanLuoi(lvGanDay);
            lblTieuDeSapHet.Text = "Hàng sắp hết (tồn từ " + TrangChuBll.NguongSapHet + " trở xuống)";
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
                var tq = bll.TongQuan();
                lblSoMatHang.Text = tq.SoMatHang.ToString("N0");
                lblSoTonKho.Text = tq.TongTon.ToString("N0");
                lblSoHoaDon.Text = tq.SoHoaDonBan.ToString("N0");
                lblSoDoanhThu.Text = tq.DoanhThu.ToString("N0");

                TienIch.NapLuoi(lvSapHet, bll.HangSapHet(), "MaHang", "TenDayDu", "SoLuong");
                lvSapHet.SelectedItems.Clear();

                TienIch.NapLuoi(lvGanDay, bll.HoaDonGanDay(), "SoHDB", "NgayBan", "TenKhach", "TongTien");
                lvGanDay.SelectedItems.Clear();
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }
    }
}

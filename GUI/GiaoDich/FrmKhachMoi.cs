using System;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public sealed partial class FrmKhachMoi : Form
    {
        private readonly BanHangBll bll;

        public KhachHang KhachDuocTao { get; private set; }

        public FrmKhachMoi(BanHangBll bll)
        {
            InitializeComponent();
            this.bll = bll ?? throw new ArgumentNullException("bll");
            btnLuu.Click += Luu_Click;
        }

        private void Luu_Click(object sender, EventArgs e)
        {
            try
            {
                KhachDuocTao = bll.TaoKhachMoi(new KhachHang
                {
                    TenKhach = txtTen.Text,
                    DiaChi = txtDiaChi.Text,
                    DienThoai = txtDienThoai.Text
                });
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
                NghiepVuException loi = ex as NghiepVuException;
                if (loi != null)
                {
                    if (loi.Truong == "TenKhach") txtTen.Focus();
                    else if (loi.Truong == "DiaChi") txtDiaChi.Focus();
                    else if (loi.Truong == "DienThoai") txtDienThoai.Focus();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

        }
    }
}

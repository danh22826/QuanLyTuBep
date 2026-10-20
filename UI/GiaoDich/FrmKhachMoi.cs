using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public sealed class FrmKhachMoi : Form
    {
        private readonly BanHangBll bll;
        private readonly TextBox txtTen = new TextBox();
        private readonly TextBox txtDiaChi = new TextBox();
        private readonly TextBox txtDienThoai = new TextBox();

        public KhachHang KhachDuocTao { get; private set; }

        public FrmKhachMoi(BanHangBll bll)
        {
            this.bll = bll ?? throw new ArgumentNullException("bll");
            DungGiaoDien();
        }

        private void DungGiaoDien()
        {
            Text = "Thêm khách hàng mới";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(430, 245);
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.White;

            ThemTruong("Tên khách hàng *", txtTen, 20, 25, 390, 29, 100);
            ThemTruong("Địa chỉ", txtDiaChi, 20, 85, 390, 29, 200);
            ThemTruong("Điện thoại", txtDienThoai, 20, 145, 200, 29, 11);

            Button btnLuu = new Button { Text = "Lưu", Location = new Point(220, 195), Size = new Size(90, 32) };
            Button btnHuy = new Button { Text = "Huỷ", Location = new Point(320, 195), Size = new Size(90, 32), DialogResult = DialogResult.Cancel };
            Theme.StyleNut(btnLuu, Theme.HeaderNen);
            btnLuu.Click += Luu_Click;
            Controls.Add(btnLuu);
            Controls.Add(btnHuy);
            AcceptButton = btnLuu;
            CancelButton = btnHuy;
        }

        private void ThemTruong(string nhan, TextBox o, int x, int y, int w, int h, int maxLength)
        {
            Label lbl = new Label { Text = nhan, Location = new Point(x, y), AutoSize = true, ForeColor = Theme.Xam };
            o.Location = new Point(x, y + 22);
            o.Size = new Size(w, h);
            o.MaxLength = maxLength;
            Controls.Add(lbl);
            Controls.Add(o);
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
    }
}

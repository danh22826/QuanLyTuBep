using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public partial class FrmKichThuoc : Form
    {
        private readonly BangView<KichThuoc> dieuKhien;
        private bool dangNap;
        private bool duLieuCuKhongHopLe;
        private static readonly Regex MauKichThuoc = new Regex(
            @"^\s*Dài\s*([0-9]+(?:[.,][0-9]+)?)\s*m\s*[x×]\s*(?:Sâu|Rộng)\s*([0-9]+(?:[.,][0-9]+)?)\s*m\s*$",
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        public FrmKichThuoc()
        {
            PhanQuyen.YeuCau(Quyen.XemDanhMuc);
            InitializeComponent();
            dieuKhien = new BangView<KichThuoc>(this, new KichThuocBll(), CauHinh.KichThuoc());

            nudChieuDai.ValueChanged += GiaTriKichThuoc_Changed;
            nudChieuRong.ValueChanged += GiaTriKichThuoc_Changed;
            chkThietKeTheoYeuCau.CheckedChanged += ThietKeTheoYeuCau_Changed;
            lv.SelectedIndexChanged += LvKichThuoc_SelectedIndexChanged;
            btnLamMoi.Click += delegate { DatMacDinh(); };
            Load += delegate { CapNhatTenVaKhoaNut(); };
            DatMacDinh();
        }

        private void DatMacDinh()
        {
            dangNap = true;
            try
            {
                duLieuCuKhongHopLe = false;
                chkThietKeTheoYeuCau.Checked = false;
                nudChieuDai.Value = 1.2M;
                nudChieuRong.Value = 0.6M;
                nudChieuDai.Enabled = true;
                nudChieuRong.Enabled = true;
                chkThietKeTheoYeuCau.Enabled = true;
                lblKichThuocXemTruoc.Text = "Nhập chiều dài và chiều rộng lớn hơn 0.";
            }
            finally { dangNap = false; }
            CapNhatTenVaKhoaNut();
        }

        private void LvKichThuoc_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lv.SelectedItems.Count == 0)
            {
                DatMacDinh();
                return;
            }

            string tenGoc = txtTenKichThuoc.Text;
            dangNap = true;
            try
            {
                duLieuCuKhongHopLe = false;
                bool dacBiet = string.Equals(tenGoc.Trim(), "Thiết kế theo yêu cầu", StringComparison.CurrentCultureIgnoreCase);
                chkThietKeTheoYeuCau.Checked = dacBiet;
                if (dacBiet)
                {
                    nudChieuDai.Enabled = false;
                    nudChieuRong.Enabled = false;
                    chkThietKeTheoYeuCau.Enabled = true;
                    lblKichThuocXemTruoc.Text = "Kích thước đặc biệt; giữ nguyên tên đang lưu.";
                }
                else
                {
                    decimal dai, rong;
                    if (ThuocKichThuoc(tenGoc, out dai, out rong))
                    {
                        nudChieuDai.Value = dai;
                        nudChieuRong.Value = rong;
                        nudChieuDai.Enabled = true;
                        nudChieuRong.Enabled = true;
                        chkThietKeTheoYeuCau.Enabled = true;
                        lblKichThuocXemTruoc.Text = "Đã đọc được kích thước cũ. Thay đổi số để cập nhật.";
                    }
                    else
                    {
                        duLieuCuKhongHopLe = true;
                        nudChieuDai.Enabled = false;
                        nudChieuRong.Enabled = false;
                        chkThietKeTheoYeuCau.Enabled = false;
                        lblKichThuocXemTruoc.Text = "Định dạng cũ không hợp lệ; giữ nguyên dữ liệu gốc.";
                    }
                }
            }
            finally { dangNap = false; }

            if (duLieuCuKhongHopLe)
                TienIch.Canh("Không phân tích được tên kích thước '" + tenGoc + "'. Dữ liệu gốc được giữ nguyên và thao tác sửa đã bị khóa để tránh mất dữ liệu.");
            CapNhatTenVaKhoaNut();
        }

        private void GiaTriKichThuoc_Changed(object sender, System.EventArgs e)
        {
            if (!dangNap) CapNhatTenVaKhoaNut();
        }

        private void ThietKeTheoYeuCau_Changed(object sender, System.EventArgs e)
        {
            if (dangNap) return;
            bool dacBiet = chkThietKeTheoYeuCau.Checked;
            nudChieuDai.Enabled = !dacBiet && !duLieuCuKhongHopLe;
            nudChieuRong.Enabled = !dacBiet && !duLieuCuKhongHopLe;
            CapNhatTenVaKhoaNut();
        }

        private void CapNhatTenVaKhoaNut()
        {
            if (duLieuCuKhongHopLe)
            {
                btnSua.Enabled = false;
                return;
            }

            bool hopLe = chkThietKeTheoYeuCau.Checked || (nudChieuDai.Value > 0 && nudChieuRong.Value > 0);
            if (hopLe)
            {
                txtTenKichThuoc.Text = chkThietKeTheoYeuCau.Checked
                    ? "Thiết kế theo yêu cầu"
                    : "Dài " + DinhDang(nudChieuDai.Value) + "m x Rộng " + DinhDang(nudChieuRong.Value) + "m";
                lblKichThuocXemTruoc.Text = "Tên kích thước sẽ lưu: " + txtTenKichThuoc.Text;
            }
            else
            {
                txtTenKichThuoc.Clear();
                lblKichThuocXemTruoc.Text = "Nhập chiều dài và chiều rộng lớn hơn 0.";
            }

            bool dangSua = lv.SelectedItems.Count > 0;
            btnThem.Enabled = !dangSua && hopLe;
            btnSua.Enabled = dangSua && hopLe;
        }

        private static string DinhDang(decimal giaTri)
        {
            return giaTri.ToString("0.##", CultureInfo.InvariantCulture);
        }

        private static bool ThuocKichThuoc(string ten, out decimal dai, out decimal rong)
        {
            dai = 0m;
            rong = 0m;
            Match match = MauKichThuoc.Match(ten ?? "");
            if (!match.Success) return false;
            string sDai = match.Groups[1].Value.Replace(',', '.');
            string sRong = match.Groups[2].Value.Replace(',', '.');
            if (!decimal.TryParse(sDai, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out dai) ||
                !decimal.TryParse(sRong, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out rong)) return false;
            if (dai <= 0 || rong <= 0 || dai > 1000 || rong > 1000) return false;

            // NumericUpDown hiển thị tối đa 2 chữ số thập phân; từ chối parse nếu
            // việc nạp sẽ làm tròn giá trị cũ và có thể gây thay đổi ngoài ý muốn.
            return decimal.Round(dai, 2) == dai && decimal.Round(rong, 2) == rong;
        }
    }
}

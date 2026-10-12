using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public static class TienIch
    {
        public static void Nhung(Form f, Control cha)
        {
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            cha.Controls.Add(f);
            f.Show();
        }

        public static void ThongBao(string noiDung)
        {
            MessageBox.Show(noiDung, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Canh(string noiDung)
        {
            MessageBox.Show(noiDung, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static bool Hoi(string noiDung)
        {
            return MessageBox.Show(noiDung, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public static void XuLyLoi(Exception ex)
        {
            if (ex is NghiepVuException)
            {
                Canh(ex.Message);
                return;
            }

            SqlException sql = ex as SqlException;
            if (sql != null)
            {
                string goiY = "";

                if (sql.Number == 53 || sql.Number == -1 || sql.Number == 2 || sql.Number == 4060 || sql.Number == 18456)
                {
                    goiY = "\n\nKiểm tra: SQL Server đã chạy chưa, đã chạy file SQL\\QuanLyBanTuBep.sql chưa, " +
                           "và Data Source trong App.config có đúng không.";
                }
                MessageBox.Show("Lỗi cơ sở dữ liệu: " + sql.Message + goiY, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static readonly Dictionary<string, string> TieuDeCot = new Dictionary<string, string>
        {
            { "MaKichThuoc", "Mã kích thước" }, { "TenKichThuoc", "Kích thước" },
            { "MaChatLieu", "Mã chất liệu" },   { "TenChatLieu", "Chất liệu" },
            { "MaMau", "Mã màu" },              { "TenMau", "Màu sắc" },
            { "MaNuocSX", "Mã nước SX" },       { "TenNuocSX", "Nước sản xuất" },
            { "MaCV", "Mã công việc" },         { "TenCV", "Công việc" },
            { "MucLuong", "Mức lương" },
            { "MaKhach", "Mã khách" },          { "TenKhach", "Tên khách hàng" },
            { "MaNCC", "Mã NCC" },              { "TenNCC", "Nhà cung cấp" },
            { "MaNV", "Mã NV" },                { "TenNV", "Nhân viên" },
            { "GioiTinh", "Giới tính" },        { "NgaySinh", "Ngày sinh" },
            { "DiaChi", "Địa chỉ" },            { "DienThoai", "Điện thoại" },
            { "MaHang", "Mã hàng" },            { "TenHang", "Tên hàng" },
            { "SoLuong", "Số lượng" },          { "GiaNhap", "Giá nhập" },
            { "DonGiaBan", "Đơn giá bán" },     { "DonGia", "Đơn giá" },
            { "ThoiGianBaoHanh", "Bảo hành" },  { "Anh", "Ảnh" },
            { "GhiChu", "Ghi chú" },
            { "SoHDB", "Số HĐ bán" },           { "SoHDN", "Số HĐ nhập" },
            { "NgayBan", "Ngày bán" },          { "NgayNhap", "Ngày nhập" },
            { "TongTien", "Tổng tiền" },        { "ThanhTien", "Thành tiền" },
            { "GiamGia", "Giảm giá (%)" },
            { "TongSoLuongBan", "Tổng SL bán" },{ "TongDoanhThu", "Doanh thu" },
            { "TongTienHoaDon", "Tổng tiền HĐ" },
            { "SoLuotMua", "Số lượt mua" },     { "TongTienMua", "Tổng tiền mua" }
        };

        private class LvThongTin
        {
            public int[] RongTuNhien;
            public bool[] LaChu;
            public bool DangPhanBo;
        }

        private const int CaoDong = 34;
        private const int LeO = 8;

        public static void GanLuoi(ListView lv)
        {
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.OwnerDraw = true;

            ImageList chieuCao = new ImageList();
            chieuCao.ImageSize = new Size(1, CaoDong);
            lv.SmallImageList = chieuCao;

            lv.DrawColumnHeader += VeTieuDeCot;
            lv.DrawItem += delegate(object s, DrawListViewItemEventArgs e) { };
            lv.DrawSubItem += VeO;
            lv.Resize += delegate { PhanBoCot(lv); };
        }

        public static void NapLuoi(ListView lv, object nguon)
        {
            DataView dv = nguon as DataView;
            if (dv == null) dv = ((DataTable)nguon).DefaultView;
            DataTable bang = dv.Table;
            int n = bang.Columns.Count;

            LvThongTin tt = new LvThongTin();
            tt.RongTuNhien = new int[n];
            tt.LaChu = new bool[n];

            lv.BeginUpdate();
            try
            {
                lv.Items.Clear();
                lv.Columns.Clear();

                for (int i = 0; i < n; i++)
                {
                    DataColumn c = bang.Columns[i];
                    string tieuDe;
                    if (!TieuDeCot.TryGetValue(c.ColumnName, out tieuDe)) tieuDe = c.ColumnName;

                    ColumnHeader h = new ColumnHeader();
                    h.Text = tieuDe;
                    h.TextAlign = CanhCot(c.DataType);
                    lv.Columns.Add(h);

                    tt.LaChu[i] = !LaSoHoacNgay(c.DataType);
                    tt.RongTuNhien[i] = TextRenderer.MeasureText(tieuDe, Theme.ChuDam).Width + 2 * LeO + 8;
                }

                List<ListViewItem> ds = new List<ListViewItem>();
                foreach (DataRowView rv in dv)
                {
                    ListViewItem it = new ListViewItem(DinhDangO(rv[0], bang.Columns[0].DataType));
                    for (int i = 1; i < n; i++) it.SubItems.Add(DinhDangO(rv[i], bang.Columns[i].DataType));
                    it.Tag = rv;
                    ds.Add(it);
                }
                lv.Items.AddRange(ds.ToArray());

                int soDong = Math.Min(ds.Count, 100);
                for (int r = 0; r < soDong; r++)
                {
                    for (int i = 0; i < n; i++)
                    {
                        string chu = ds[r].SubItems[i].Text;
                        if (chu.Length == 0) continue;
                        int w = TextRenderer.MeasureText(chu, Theme.ChuMenu).Width + 2 * LeO + 4;
                        if (w > tt.RongTuNhien[i]) tt.RongTuNhien[i] = w;
                    }
                }

                lv.Tag = tt;
                PhanBoCot(lv);
            }
            finally
            {
                lv.EndUpdate();
            }
        }

        private static bool LaSoHoacNgay(Type kieu)
        {
            return kieu == typeof(decimal) || kieu == typeof(double) || kieu == typeof(float)
                   || kieu == typeof(int) || kieu == typeof(long) || kieu == typeof(short)
                   || kieu == typeof(DateTime);
        }

        private static HorizontalAlignment CanhCot(Type kieu)
        {
            if (kieu == typeof(DateTime)) return HorizontalAlignment.Center;
            return LaSoHoacNgay(kieu) ? HorizontalAlignment.Right : HorizontalAlignment.Left;
        }

        private static string DinhDangO(object v, Type kieu)
        {
            if (v == null || v is DBNull) return "";
            if (kieu == typeof(decimal)) return ((decimal)v).ToString("N0");
            if (kieu == typeof(double)) return ((double)v).ToString("0.##");
            if (kieu == typeof(float)) return ((float)v).ToString("0.##");
            if (kieu == typeof(DateTime)) return ((DateTime)v).ToString("dd/MM/yyyy");
            return Convert.ToString(v);
        }

        private static void PhanBoCot(ListView lv)
        {
            LvThongTin tt = lv.Tag as LvThongTin;
            if (tt == null || tt.DangPhanBo || lv.Columns.Count == 0) return;
            int rong = lv.ClientSize.Width - 2;
            if (rong <= 0) return;

            tt.DangPhanBo = true;
            try
            {
                int n = lv.Columns.Count;
                int[] w = (int[])tt.RongTuNhien.Clone();
                int tong = 0, tongChu = 0;
                for (int i = 0; i < n; i++)
                {
                    tong += w[i];
                    if (tt.LaChu[i]) tongChu += w[i];
                }

                if (tong < rong)
                {
                    int thua = rong - tong;
                    if (tongChu > 0)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            if (tt.LaChu[i]) w[i] += (int)((long)thua * w[i] / tongChu);
                        }
                    }
                    else
                    {
                        w[n - 1] += thua;
                    }
                }
                else if (tong > rong && tongChu > 0)
                {
                    int thieu = tong - rong;
                    for (int i = 0; i < n; i++)
                    {
                        if (!tt.LaChu[i]) continue;
                        int co = (int)((long)thieu * w[i] / tongChu);
                        w[i] = Math.Max(Math.Min(w[i], 90), w[i] - co);
                    }

                    int dau = 0;
                    for (int i = 0; i < n; i++) dau += w[i];
                    if (dau > rong)
                    {
                        int du = dau - rong;
                        int tongChu2 = 0;
                        for (int i = 0; i < n; i++) if (tt.LaChu[i]) tongChu2 += w[i];
                        for (int i = 0; i < n && tongChu2 > 0; i++)
                        {
                            if (!tt.LaChu[i]) continue;
                            w[i] = Math.Max(50, w[i] - (int)((long)du * w[i] / tongChu2));
                        }
                    }
                }

                for (int i = 0; i < n; i++) lv.Columns[i].Width = w[i];

                int cuoi = 0;
                for (int i = 0; i < n; i++) cuoi += lv.Columns[i].Width;
                if (cuoi < rong) lv.Columns[n - 1].Width += rong - cuoi;
            }
            finally
            {
                tt.DangPhanBo = false;
            }
        }

        private static TextFormatFlags CoCanh(HorizontalAlignment canh)
        {
            TextFormatFlags f = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis |
                                TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine;
            if (canh == HorizontalAlignment.Right) f |= TextFormatFlags.Right;
            else if (canh == HorizontalAlignment.Center) f |= TextFormatFlags.HorizontalCenter;
            else f |= TextFormatFlags.Left;
            return f;
        }

        private static void VeTieuDeCot(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (SolidBrush nen = new SolidBrush(Theme.HeaderNen))
            {
                e.Graphics.FillRectangle(nen, e.Bounds);
            }
            Rectangle vung = new Rectangle(e.Bounds.X + LeO, e.Bounds.Y, e.Bounds.Width - 2 * LeO, e.Bounds.Height);

            HorizontalAlignment canh = e.ColumnIndex == 0 ? HorizontalAlignment.Left : e.Header.TextAlign;
            TextRenderer.DrawText(e.Graphics, e.Header.Text, Theme.ChuDam, vung, Theme.ChuHeader, CoCanh(canh));
        }

        private static void VeO(object sender, DrawListViewSubItemEventArgs e)
        {
            Color nen = e.Item.Selected ? Theme.DongChon : (e.ItemIndex % 2 == 1 ? Theme.DongXen : Color.White);
            using (SolidBrush b = new SolidBrush(nen))
            {
                e.Graphics.FillRectangle(b, e.Bounds);
            }
            using (Pen vien = new Pen(Theme.VienNhat))
            {
                e.Graphics.DrawLine(vien, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
            }

            Rectangle vung = new Rectangle(e.Bounds.X + LeO, e.Bounds.Y, e.Bounds.Width - 2 * LeO, e.Bounds.Height);
            HorizontalAlignment canh = e.ColumnIndex == 0 ? HorizontalAlignment.Left : e.Header.TextAlign;
            TextRenderer.DrawText(e.Graphics, e.SubItem.Text, Theme.ChuMenu, vung, Theme.ChuChinh, CoCanh(canh));
        }

        public static void ChonGiaTri(ComboBox cbo, object giaTri)
        {
            if (giaTri == null || giaTri is DBNull)
            {
                cbo.SelectedIndex = cbo.Items.Count > 0 && cbo.DataSource != null && ChoPhepRong(cbo) ? 0 : -1;
                return;
            }
            cbo.SelectedValue = giaTri;
        }

        private static bool ChoPhepRong(ComboBox cbo)
        {
            DataTable dt = cbo.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0) return false;
            return dt.Rows[0][cbo.ValueMember] is DBNull;
        }

        public static string LayMa(ComboBox cbo)
        {
            object v = cbo.SelectedValue;
            if (v == null || v is DBNull) return null;
            return Convert.ToString(v);
        }

        public static void XuatCsv(ListView lv, string tenGoiY)
        {
            if (lv.Items.Count == 0)
            {
                Canh("Không có dữ liệu để xuất.");
                return;
            }

            using (SaveFileDialog hop = new SaveFileDialog())
            {
                hop.Filter = "CSV (mở bằng Excel)|*.csv";
                hop.FileName = tenGoiY + ".csv";
                if (hop.ShowDialog() != DialogResult.OK) return;

                try
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("sep=,");

                    List<string> tieuDe = new List<string>();
                    foreach (ColumnHeader cot in lv.Columns) tieuDe.Add(Csv(cot.Text));
                    sb.AppendLine(string.Join(",", tieuDe.ToArray()));

                    foreach (ListViewItem dong in lv.Items)
                    {
                        DataRowView rv = dong.Tag as DataRowView;
                        List<string> o = new List<string>();
                        for (int i = 0; i < lv.Columns.Count; i++)
                        {
                            o.Add(Csv(rv != null ? GiaTriChu(rv[i]) : dong.SubItems[i].Text));
                        }
                        sb.AppendLine(string.Join(",", o.ToArray()));
                    }

                    File.WriteAllText(hop.FileName, sb.ToString(), new UTF8Encoding(true));
                    ThongBao("Đã xuất file:\n" + hop.FileName);
                }
                catch (Exception ex)
                {
                    XuLyLoi(ex);
                }
            }
        }

        private static string GiaTriChu(object v)
        {
            if (v == null || v is DBNull) return "";
            if (v is DateTime) return ((DateTime)v).ToString("dd/MM/yyyy");
            if (v is decimal) return ((decimal)v).ToString("0.##", CultureInfo.InvariantCulture);
            if (v is double) return ((double)v).ToString("0.##", CultureInfo.InvariantCulture);
            return Convert.ToString(v, CultureInfo.InvariantCulture);
        }

        private static string Csv(string s)
        {
            if (s == null) return "";
            if (s.IndexOfAny(new char[] { ',', '"', '\n', '\r' }) >= 0)
            {
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            }
            return s;
        }
    }
}

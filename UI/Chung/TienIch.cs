using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using QuanLyBanTuBep.BLL;
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
            string noiDung = LoiHeThong.ThongDiep(ex);
            if (LoiHeThong.LaLoiNghiepVu(ex))
                Canh(noiDung);
            else
                MessageBox.Show(noiDung, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private class LvThongTin
        {
            public int[] RongTuNhien;
            public bool[] LaChu;
            public bool DangPhanBo;
            public bool ChoCuonNgang;
        }

        private const int CaoDong = 34;
        private const int LeO = 8;

        public static void GanLuoi(ListView lv, bool choCuonNgang = false)
        {
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.OwnerDraw = true;

            ImageList chieuCao = new ImageList();
            chieuCao.ImageSize = new Size(1, CaoDong);
            lv.SmallImageList = chieuCao;
            lv.Tag = new LvThongTin { ChoCuonNgang = choCuonNgang };

            lv.DrawColumnHeader += VeTieuDeCot;
            lv.DrawItem += delegate (object s, DrawListViewItemEventArgs e) { };
            lv.DrawSubItem += VeO;
            lv.Resize += delegate { PhanBoCot(lv); };
        }

        public static void NapLuoi<T>(ListView lv, IEnumerable<T> ds, params string[] cot)
        {
            List<PropertyInfo> thuocTinh = new List<PropertyInfo>();
            if (cot == null || cot.Length == 0)
            {
                foreach (PropertyInfo p in typeof(T).GetProperties())
                    if (p.GetCustomAttributes(typeof(DisplayNameAttribute), true).Length > 0) thuocTinh.Add(p);
            }
            else
            {
                foreach (string ten in cot)
                {
                    PropertyInfo p = typeof(T).GetProperty(ten);
                    if (p == null) throw new InvalidOperationException(typeof(T).Name + " không có thuộc tính " + ten);
                    thuocTinh.Add(p);
                }
            }

            string[] tieuDe = new string[thuocTinh.Count];
            Type[] kieu = new Type[thuocTinh.Count];
            for (int i = 0; i < thuocTinh.Count; i++)
            {
                object[] dn = thuocTinh[i].GetCustomAttributes(typeof(DisplayNameAttribute), true);
                tieuDe[i] = dn.Length > 0 ? ((DisplayNameAttribute)dn[0]).DisplayName : thuocTinh[i].Name;
                kieu[i] = Nullable.GetUnderlyingType(thuocTinh[i].PropertyType) ?? thuocTinh[i].PropertyType;
            }

            List<object[]> dong = new List<object[]>();
            List<object> tags = new List<object>();
            foreach (T item in ds)
            {
                object[] giaTri = new object[thuocTinh.Count];
                for (int i = 0; i < thuocTinh.Count; i++) giaTri[i] = thuocTinh[i].GetValue(item, null);
                dong.Add(giaTri);
                tags.Add(item);
            }
            NapLuoiModel(lv, dong, tags, tieuDe, kieu);
        }

        private static void NapLuoiModel(ListView lv, IList<object[]> dong, IList<object> tags, string[] tieuDe, Type[] kieu)
        {
            LvThongTin cu = lv.Tag as LvThongTin;
            LvThongTin tt = new LvThongTin
            {
                RongTuNhien = new int[tieuDe.Length],
                LaChu = new bool[tieuDe.Length],
                ChoCuonNgang = cu != null && cu.ChoCuonNgang
            };
            lv.BeginUpdate();
            try
            {
                lv.Items.Clear(); lv.Columns.Clear();
                for (int i = 0; i < tieuDe.Length; i++)
                {
                    string title = tieuDe[i];
                    lv.Columns.Add(new ColumnHeader { Text = title, TextAlign = CanhCot(kieu[i]) });
                    tt.LaChu[i] = !LaSoHoacNgay(kieu[i]);
                    tt.RongTuNhien[i] = TextRenderer.MeasureText(title, Theme.ChuDam).Width + 2 * LeO + 8;
                }
                List<ListViewItem> items = new List<ListViewItem>();
                for (int r = 0; r < dong.Count; r++)
                {
                    ListViewItem item = new ListViewItem(DinhDangO(dong[r][0], kieu[0]));
                    for (int c = 1; c < tieuDe.Length; c++) item.SubItems.Add(DinhDangO(dong[r][c], kieu[c]));
                    item.Tag = tags[r]; items.Add(item);
                }
                lv.Items.AddRange(items.ToArray());
                int count = Math.Min(items.Count, 100);
                for (int r = 0; r < count; r++)
                    for (int c = 0; c < tieuDe.Length; c++)
                    {
                        string value = items[r].SubItems[c].Text;
                        if (value.Length > 0) tt.RongTuNhien[c] = Math.Max(tt.RongTuNhien[c], TextRenderer.MeasureText(value, Theme.ChuMenu).Width + 2 * LeO + 4);
                    }
                lv.Tag = tt; PhanBoCot(lv);
            }
            finally { lv.EndUpdate(); }
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
            if (tt == null || tt.RongTuNhien == null || tt.DangPhanBo || lv.Columns.Count == 0) return;
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
                else if (tong > rong && tongChu > 0 && !tt.ChoCuonNgang)
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
            IList<LuaChon> ds = cbo.DataSource as IList<LuaChon>;
            if (ds != null) return ds.Count > 0 && ds[0].Ma == null;
            return false;
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
                    // Dùng đúng dấu ngăn cột của Windows trên máy này (vi-VN thường là ";"),
                    // vì Excel tách cột theo dấu đó khi mở file .csv.
                    string phanCach = CultureInfo.CurrentCulture.TextInfo.ListSeparator;

                    StringBuilder sb = new StringBuilder();

                    List<string> tieuDe = new List<string>();
                    foreach (ColumnHeader cot in lv.Columns) tieuDe.Add(Csv(cot.Text, phanCach));
                    sb.AppendLine(string.Join(phanCach, tieuDe.ToArray()));

                    foreach (ListViewItem dong in lv.Items)
                    {
                        List<string> o = new List<string>();
                        for (int i = 0; i < lv.Columns.Count; i++)
                            o.Add(Csv(dong.SubItems[i].Text, phanCach));
                        sb.AppendLine(string.Join(phanCach, o.ToArray()));
                    }

                    // UTF-8 có BOM để Excel nhận đúng tiếng Việt.
                    // KHÔNG thêm dòng "sep=" ở đầu file: dòng đó làm Excel bỏ qua BOM và đọc sai bảng mã.
                    File.WriteAllText(hop.FileName, sb.ToString(), new UTF8Encoding(true));
                    ThongBao("Đã xuất file:\n" + hop.FileName);
                }
                catch (Exception ex)
                {
                    XuLyLoi(ex);
                }
            }
        }

        private static string Csv(string s, string phanCach)
        {
            if (s == null) return "";
            if (s.IndexOfAny(new char[] { '"', '\n', '\r' }) >= 0 || s.Contains(phanCach))
            {
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            }
            return s;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public class GiaTriThamSo
    {
        private readonly Dictionary<string, object> v = new Dictionary<string, object>();

        internal void Dat(string ten, object giaTri) { v[ten] = giaTri; }
        public int Int(string ten) { return (int)v[ten]; }
        public string Chu(string ten) { return (string)v[ten]; }
        public DateTime? Ngay(string ten) { return (DateTime?)v[ten]; }
        public decimal? Tien(string ten) { return (decimal?)v[ten]; }
    }

    public class ThuTucView<T>
    {
        private readonly Form form;
        private readonly CauHinhThuTuc cfg;
        private readonly BaoCaoBll baoCao;
        private readonly Func<GiaTriThamSo, IList<T>> chay;
        private readonly Dictionary<ThamSo, Control> o = new Dictionary<ThamSo, Control>();
        private readonly ListView lv;
        private readonly Label lblKetQua;

        public ThuTucView(Form form, CauHinhThuTuc cfg, BaoCaoBll baoCao, Func<GiaTriThamSo, IList<T>> chay)
        {
            this.form = form;
            this.cfg = cfg;
            this.baoCao = baoCao;
            this.chay = chay;

            lv = Tim<ListView>("lv");
            TienIch.GanLuoi(lv);
            lblKetQua = Tim<Label>("lblKetQua");
            Button btnXem = Tim<Button>("btnXem");
            Button btnCsv = Tim<Button>("btnCsv");

            foreach (ThamSo t in cfg.DsThamSo)
            {
                string tienTo;
                switch (t.Kieu)
                {
                    case KieuThamSo.Nam: tienTo = "nud"; break;
                    case KieuThamSo.Quy:
                    case KieuThamSo.Thang:
                    case KieuThamSo.ComboDb: tienTo = "cbo"; break;
                    case KieuThamSo.Ngay: tienTo = "dtp"; break;
                    default: tienTo = "txt"; break;
                }
                o[t] = Tim<Control>(tienTo + t.Ten);
            }

            btnXem.Click += delegate { ChayBaoCao(); };
            btnCsv.Click += delegate { TienIch.XuatCsv(lv, cfg.TenFile); };
            form.Load += delegate { KhoiTao(); };
        }

        private T1 Tim<T1>(string ten) where T1 : Control
        {
            Control[] ds = form.Controls.Find(ten, true);
            if (ds.Length == 0 || !(ds[0] is T1)) throw new InvalidOperationException("Form " + form.Name + " thiếu control " + ten);
            return (T1)ds[0];
        }

        private void KhoiTao()
        {
            try
            {
                DateTime moc = baoCao.NgayMacDinh();

                foreach (ThamSo t in cfg.DsThamSo)
                {
                    Control c = o[t];
                    switch (t.Kieu)
                    {
                        case KieuThamSo.Nam:
                            NumericUpDown nam = (NumericUpDown)c;
                            decimal namMoc = moc.Year;
                            if (namMoc < nam.Minimum) namMoc = nam.Minimum;
                            if (namMoc > nam.Maximum) namMoc = nam.Maximum;
                            nam.Value = namMoc;
                            break;
                        case KieuThamSo.Quy:
                            ((ComboBox)c).SelectedIndex = (moc.Month - 1) / 3;
                            break;
                        case KieuThamSo.Thang:
                            ((ComboBox)c).SelectedIndex = moc.Month - 1;
                            break;
                        case KieuThamSo.ComboDb:
                            NapCombo(t, (ComboBox)c);
                            break;
                    }
                }
                ChayBaoCao();
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }

        private static void NapCombo(ThamSo t, ComboBox cbo)
        {
            List<LuaChon> ds = new List<LuaChon>(t.Nguon());
            if (t.ChoPhepTatCa) ds.Insert(0, new LuaChon(null, "(Tất cả)"));
            cbo.DisplayMember = "Ten";
            cbo.ValueMember = "Ma";
            cbo.DataSource = ds;
            cbo.SelectedIndex = ds.Count > 0 ? 0 : -1;
        }

        private void ChayBaoCao()
        {
            try
            {
                GiaTriThamSo gt = new GiaTriThamSo();
                foreach (ThamSo t in cfg.DsThamSo)
                    if (!DocO(t, o[t], gt)) return;

                IList<T> kq = chay(gt);
                TienIch.NapLuoi(lv, kq, cfg.CotLuoi);
                lv.SelectedItems.Clear();
                lblKetQua.Text = "Kết quả: " + kq.Count + " dòng";
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }

        private static bool DocO(ThamSo t, Control c, GiaTriThamSo gt)
        {
            switch (t.Kieu)
            {
                case KieuThamSo.Nam:
                    gt.Dat(t.Ten, (int)((NumericUpDown)c).Value);
                    return true;

                case KieuThamSo.Quy:
                case KieuThamSo.Thang:
                    gt.Dat(t.Ten, Convert.ToInt32(((ComboBox)c).SelectedItem));
                    return true;

                case KieuThamSo.ComboDb:
                    gt.Dat(t.Ten, TienIch.LayMa((ComboBox)c));
                    return true;

                case KieuThamSo.Ngay:
                    DateTimePicker d = (DateTimePicker)c;
                    gt.Dat(t.Ten, d.Checked ? (DateTime?)d.Value.Date : null);
                    return true;

                case KieuThamSo.Tien:
                    string s = ((TextBox)c).Text.Trim();
                    if (s.Length == 0)
                    {
                        gt.Dat(t.Ten, (decimal?)null);
                        return true;
                    }
                    // Cho phép gõ 5.000.000 hoặc 5,000,000 hoặc 5000000
                    string sach = s.Replace(".", "").Replace(",", "").Replace(" ", "");
                    decimal tien;
                    if (!decimal.TryParse(sach, NumberStyles.None, CultureInfo.InvariantCulture, out tien))
                    {
                        TienIch.Canh(t.Nhan + " phải là số (vd: 5000000).");
                        c.Focus();
                        return false;
                    }
                    gt.Dat(t.Ten, (decimal?)tien);
                    return true;

                default:
                    string chu = ((TextBox)c).Text.Trim();
                    gt.Dat(t.Ten, chu.Length == 0 ? null : chu);
                    return true;
            }
        }
    }
}

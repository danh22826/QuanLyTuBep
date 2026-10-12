using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using QuanLyBanTuBep.DAL;

namespace QuanLyBanTuBep.UI
{
    public class ThuTucController
    {
        private readonly Form form;
        private readonly CauHinhThuTuc cfg;
        private readonly Dictionary<ThamSo, Control> o = new Dictionary<ThamSo, Control>();
        private readonly ListView lv;
        private readonly Label lblKetQua;
        private readonly Button btnXem, btnCsv;

        public ThuTucController(Form form, CauHinhThuTuc cfg)
        {
            this.form = form;
            this.cfg = cfg;
            lv = Tim<ListView>("lv");
            TienIch.GanLuoi(lv);
            lblKetQua = Tim<Label>("lblKetQua");
            btnXem = Tim<Button>("btnXem");
            btnCsv = Tim<Button>("btnCsv");

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
                o[t] = Tim<Control>(tienTo + t.Ten.TrimStart('@'));
            }

            btnXem.Click += delegate { ChayThuTuc(); };
            btnCsv.Click += delegate { TienIch.XuatCsv(lv, cfg.TenFile); };
            form.Load += delegate { KhoiTao(); };
        }

        private T Tim<T>(string ten) where T : Control
        {
            Control[] ds = form.Controls.Find(ten, true);
            if (ds.Length == 0 || !(ds[0] is T)) throw new InvalidOperationException("Form " + form.Name + " thiếu control " + ten);
            return (T)ds[0];
        }

        private void KhoiTao()
        {
            try
            {
                DateTime moc = Db.NgayBanGanNhat() ?? DateTime.Today;

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

                ChayThuTuc();
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }

        private void NapCombo(ThamSo t, ComboBox cbo)
        {
            DataTable dt = Db.Query(t.Nguon);
            if (t.ChoPhepTatCa)
            {
                DataRow tatCa = dt.NewRow();
                tatCa["Ma"] = DBNull.Value;
                tatCa["Ten"] = "(Tất cả)";
                dt.Rows.InsertAt(tatCa, 0);
            }
            cbo.DisplayMember = "Ten";
            cbo.ValueMember = "Ma";
            cbo.DataSource = dt;
            cbo.SelectedIndex = dt.Rows.Count > 0 ? 0 : -1;
        }

        private void ChayThuTuc()
        {
            try
            {
                List<SqlParameter> ps = new List<SqlParameter>();
                foreach (ThamSo t in cfg.DsThamSo)
                {
                    SqlParameter p;
                    if (!LayThamSo(t, o[t], out p)) return;
                    ps.Add(p);
                }

                DataTable kq = Db.Proc(cfg.Proc, ps.ToArray());
                TienIch.NapLuoi(lv, kq);
                lv.SelectedItems.Clear();
                lblKetQua.Text = "Kết quả: " + kq.Rows.Count + " dòng";
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }

        private bool LayThamSo(ThamSo t, Control c, out SqlParameter p)
        {
            p = null;
            switch (t.Kieu)
            {
                case KieuThamSo.Nam:
                    p = Db.P(t.Ten, (int)((NumericUpDown)c).Value);
                    return true;

                case KieuThamSo.Quy:
                case KieuThamSo.Thang:
                    p = Db.P(t.Ten, Convert.ToInt32(((ComboBox)c).SelectedItem));
                    return true;

                case KieuThamSo.ComboDb:
                    string ma = TienIch.LayMa((ComboBox)c);
                    if (ma == null && !t.ChoPhepTatCa)
                    {
                        TienIch.Canh("Vui lòng chọn " + t.Nhan.ToLower() + ".");
                        c.Focus();
                        return false;
                    }
                    p = Db.P(t.Ten, ma);
                    return true;

                case KieuThamSo.Ngay:
                    DateTimePicker d = (DateTimePicker)c;
                    p = Db.PNgay(t.Ten, d.Checked ? (DateTime?)d.Value.Date : null);
                    return true;

                case KieuThamSo.Tien:
                    string s = ((TextBox)c).Text.Trim();
                    if (s.Length == 0)
                    {
                        p = Db.P(t.Ten, null);
                        return true;
                    }

                    string sach = s.Replace(".", "").Replace(",", "").Replace(" ", "");
                    decimal tien;
                    if (!decimal.TryParse(sach, NumberStyles.None, CultureInfo.InvariantCulture, out tien))
                    {
                        TienIch.Canh(t.Nhan + " phải là số (vd: 5000000).");
                        c.Focus();
                        return false;
                    }
                    p = Db.P(t.Ten, tien);
                    return true;

                default:
                    string chu = ((TextBox)c).Text.Trim();
                    p = Db.P(t.Ten, chu.Length == 0 ? null : chu);
                    return true;
            }
        }
    }
}

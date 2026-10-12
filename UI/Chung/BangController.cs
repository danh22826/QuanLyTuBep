using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using QuanLyBanTuBep.DAL;

namespace QuanLyBanTuBep.UI
{
    public class BangController
    {
        private readonly Form form;
        private readonly CauHinhBang cfg;
        private readonly Truong truongKhoa;
        private readonly Dictionary<Truong, Control> o = new Dictionary<Truong, Control>();

        private readonly ListView lv;
        private readonly TextBox txtTim;
        private readonly Label lblDem;
        private readonly PictureBox picAnh;
        private readonly TextBox txtAnh;
        private readonly Button btnThem, btnSua, btnXoa, btnLamMoi, btnCsv, btnChonAnh;

        private string anhTen = "";
        private string anhNguon;
        private string[] choChep;

        private bool dangSua;
        private DataTable bang;
        private DataView dv;

        public BangController(Form form, CauHinhBang cfg)
        {
            this.form = form;
            this.cfg = cfg;
            this.truongKhoa = cfg.DsTruong.First(t => t.KhoaChinh);

            lv = Tim<ListView>("lv");
            TienIch.GanLuoi(lv);
            txtTim = Tim<TextBox>("txtTim");
            lblDem = Tim<Label>("lblDem");
            picAnh = TimTuyChon<PictureBox>("picAnh");
            txtAnh = TimTuyChon<TextBox>("txtAnh");
            btnThem = Tim<Button>("btnThem");
            btnSua = Tim<Button>("btnSua");
            btnXoa = Tim<Button>("btnXoa");
            btnLamMoi = Tim<Button>("btnLamMoi");
            btnCsv = Tim<Button>("btnCsv");
            btnChonAnh = TimTuyChon<Button>("btnChonAnh");

            GanO();

            btnThem.Click += delegate { Them(); };
            btnSua.Click += delegate { Sua(); };
            btnXoa.Click += delegate { Xoa(); };
            btnLamMoi.Click += delegate { LamMoi(); };
            btnCsv.Click += delegate { TienIch.XuatCsv(lv, cfg.Bang); };
            txtTim.TextChanged += delegate { ApDungLoc(); };
            lv.SelectedIndexChanged += Lv_Chon;
            if (btnChonAnh != null) btnChonAnh.Click += delegate { ChonAnh(); };

            DatCheDo(false);
            form.Load += delegate { TaiDuLieu(); };
        }

        private T Tim<T>(string ten) where T : Control
        {
            T c = TimTuyChon<T>(ten);
            if (c == null) throw new InvalidOperationException("Form " + form.Name + " thiếu control " + ten);
            return c;
        }

        private T TimTuyChon<T>(string ten) where T : Control
        {
            Control[] ds = form.Controls.Find(ten, true);
            return ds.Length == 0 ? null : ds[0] as T;
        }

        private void GanO()
        {
            foreach (Truong t in cfg.DsTruong)
            {
                string ten;
                switch (t.Kieu)
                {
                    case KieuTruong.So: ten = "nud" + t.Cot; break;
                    case KieuTruong.Ngay: ten = "dtp" + t.Cot; break;
                    case KieuTruong.Combo: ten = "cbo" + t.Cot; break;
                    default: ten = "txt" + t.Cot; break;
                }
                Control c = Tim<Control>(ten);
                o[t] = c;

                if (t.Kieu == KieuTruong.Ngay)
                {
                    DateTimePicker d = (DateTimePicker)c;
                    d.Value = DateTime.Today;
                    d.MaxDate = DateTime.Today;
                    d.Checked = false;
                }
            }
        }

        private void ChonAnh()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Chọn ảnh sản phẩm";
                dlg.Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                Image thu = ThuMucAnh.DocFile(dlg.FileName);
                if (thu == null)
                {
                    TienIch.Canh("File được chọn không phải ảnh hợp lệ.");
                    return;
                }
                anhNguon = dlg.FileName;
                txtAnh.Text = System.IO.Path.GetFileName(dlg.FileName) + " (mới)";
                DatAnh(thu);
            }
        }

        private void TaiDuLieu()
        {
            try
            {
                NapCombo();
                TaiLuoi();
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }

        private void NapCombo()
        {
            foreach (Truong t in cfg.DsTruong)
            {
                if (t.Kieu != KieuTruong.Combo) continue;

                ComboBox cbo = (ComboBox)o[t];
                DataTable dt = Db.Query(t.NguonCombo);
                if (!t.BatBuoc)
                {
                    DataRow trong = dt.NewRow();
                    trong["Ma"] = DBNull.Value;
                    trong["Ten"] = "(không chọn)";
                    dt.Rows.InsertAt(trong, 0);
                }
                cbo.DisplayMember = "Ten";
                cbo.ValueMember = "Ma";
                cbo.DataSource = dt;
                cbo.SelectedIndex = t.BatBuoc ? -1 : 0;
            }
        }

        private void TaiLuoi()
        {
            bang = Db.Query(cfg.SqlLuoi);
            dv = bang.DefaultView;
            ApDungLoc();
            lv.SelectedItems.Clear();
        }

        private void ApDungLoc()
        {
            if (dv == null) return;

            string tuKhoa = txtTim.Text.Trim();
            if (tuKhoa.Length == 0)
            {
                dv.RowFilter = "";
            }
            else
            {
                string mau = ThoatLike(tuKhoa);
                List<string> dieuKien = new List<string>();
                foreach (DataColumn col in bang.Columns)
                {
                    dieuKien.Add("Convert([" + col.ColumnName + "], 'System.String') LIKE '%" + mau + "%'");
                }
                try
                {
                    dv.RowFilter = string.Join(" OR ", dieuKien.ToArray());
                }
                catch (Exception)
                {
                    dv.RowFilter = "";
                }
            }
            TienIch.NapLuoi(lv, dv);
            lblDem.Text = dv.Count + " dòng";
        }

        private static string ThoatLike(string s)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in s)
            {
                if (ch == '\'') sb.Append("''");
                else if (ch == '[' || ch == ']' || ch == '%' || ch == '*') sb.Append('[').Append(ch).Append(']');
                else sb.Append(ch);
            }
            return sb.ToString();
        }

        private void Lv_Chon(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count == 0) return;
            try
            {
                string khoa = lv.SelectedItems[0].Text;
                DataTable dt = Db.Query(
                    "SELECT * FROM " + cfg.Bang + " WHERE " + cfg.CotKhoa + " = @k", Db.P("@k", khoa));
                if (dt.Rows.Count == 0)
                {
                    TienIch.Canh("Bản ghi không còn tồn tại.");
                    TaiLuoi();
                    return;
                }
                DocVaoO(dt.Rows[0]);
                DatCheDo(true);
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }

        private void DocVaoO(DataRow row)
        {
            foreach (Truong t in cfg.DsTruong)
            {
                object v = row[t.Cot];
                Control c = o[t];
                bool rong = (v == null || v is DBNull);

                switch (t.Kieu)
                {
                    case KieuTruong.Chu:
                        ((TextBox)c).Text = rong ? "" : Convert.ToString(v);
                        break;

                    case KieuTruong.So:
                        NumericUpDown n = (NumericUpDown)c;
                        decimal so = rong ? 0m : Convert.ToDecimal(v);
                        if (so < n.Minimum) so = n.Minimum;
                        if (so > n.Maximum) so = n.Maximum;
                        n.Value = so;
                        break;

                    case KieuTruong.Ngay:
                        DateTimePicker d = (DateTimePicker)c;
                        if (rong)
                        {
                            d.Checked = false;
                        }
                        else
                        {
                            DateTime ngay = Convert.ToDateTime(v);
                            if (ngay > d.MaxDate) ngay = d.MaxDate;
                            if (ngay < d.MinDate) ngay = d.MinDate;
                            d.Value = ngay;
                            d.Checked = true;
                        }
                        break;

                    case KieuTruong.Combo:
                        TienIch.ChonGiaTri((ComboBox)c, v);
                        break;

                    case KieuTruong.Anh:
                        anhTen = rong ? "" : Convert.ToString(v);
                        anhNguon = null;
                        txtAnh.Text = anhTen;
                        HienAnhTen(anhTen);
                        break;
                }
            }
        }

        private void DatAnh(Image anh)
        {
            if (picAnh == null) return;
            Image cu = picAnh.Image;
            picAnh.Image = anh;
            if (cu != null) cu.Dispose();
        }

        private void HienAnhTen(string tenFile)
        {
            DatAnh(ThuMucAnh.Doc(tenFile));
        }

        private void HienAnhFile(string duongDan)
        {
            DatAnh(ThuMucAnh.DocFile(duongDan));
        }

        private void ChepAnhCho()
        {
            if (choChep == null) return;
            string[] cp = choChep;
            choChep = null;
            try
            {
                ThuMucAnh.Chep(cp[0], cp[1]);
            }
            catch (Exception ex)
            {
                TienIch.Canh("Đã lưu thông tin nhưng không chép được ảnh vào thư mục Images:\n" + ex.Message);
            }
        }

        private void DatCheDo(bool sua)
        {
            dangSua = sua;
            foreach (Truong t in cfg.DsTruong)
            {
                bool khoa = t.ChiDoc || (sua && (t.KhoaChinh || t.ChiKhiThem));
                o[t].Enabled = !khoa;
                if (t.Kieu == KieuTruong.Anh && btnChonAnh != null) btnChonAnh.Enabled = !khoa;
            }
            btnThem.Enabled = !sua;
            btnSua.Enabled = sua;
            btnXoa.Enabled = sua;
        }

        private void LamMoi()
        {
            foreach (Truong t in cfg.DsTruong)
            {
                Control c = o[t];
                switch (t.Kieu)
                {
                    case KieuTruong.Chu:
                        ((TextBox)c).Clear();
                        break;
                    case KieuTruong.So:
                        ((NumericUpDown)c).Value = 0;
                        break;
                    case KieuTruong.Ngay:
                        ((DateTimePicker)c).Checked = false;
                        break;
                    case KieuTruong.Combo:
                        ComboBox cb = (ComboBox)c;
                        if (cb.Items.Count > 0) cb.SelectedIndex = t.BatBuoc ? -1 : 0;
                        break;
                    case KieuTruong.Anh:
                        anhTen = "";
                        anhNguon = null;
                        txtAnh.Text = "";
                        DatAnh(null);
                        break;
                }
            }
            DatCheDo(false);
            lv.SelectedItems.Clear();
            o[truongKhoa].Focus();
        }

        private bool KiemTra()
        {
            foreach (Truong t in cfg.DsTruong)
            {
                if (t.ChiDoc) continue;
                if (dangSua && (t.KhoaChinh || t.ChiKhiThem)) continue;

                Control c = o[t];
                if (t.Kieu == KieuTruong.Chu)
                {
                    string s = ((TextBox)c).Text.Trim();
                    if (t.BatBuoc && s.Length == 0)
                    {
                        return BaoSai(t.Nhan + " không được để trống.", c);
                    }
                    if (s.Length > 0 && !string.IsNullOrEmpty(t.BieuThuc) && !Regex.IsMatch(s, t.BieuThuc))
                    {
                        return BaoSai(t.BaoSai ?? (t.Nhan + " không hợp lệ."), c);
                    }
                }
                else if (t.Kieu == KieuTruong.Combo)
                {
                    if (t.BatBuoc && TienIch.LayMa((ComboBox)c) == null)
                    {
                        return BaoSai("Vui lòng chọn " + t.Nhan.ToLower() + ".", c);
                    }
                }
            }
            return true;
        }

        private static bool BaoSai(string noiDung, Control c)
        {
            TienIch.Canh(noiDung);
            c.Focus();
            return false;
        }

        private object LayGiaTri(Truong t)
        {
            Control c = o[t];
            switch (t.Kieu)
            {
                case KieuTruong.So:
                    return ((NumericUpDown)c).Value;
                case KieuTruong.Ngay:
                    DateTimePicker d = (DateTimePicker)c;
                    return d.Checked ? (object)d.Value.Date : DBNull.Value;
                case KieuTruong.Combo:
                    string ma = TienIch.LayMa((ComboBox)c);
                    return ma == null ? (object)DBNull.Value : ma;
                case KieuTruong.Anh:
                    if (anhNguon != null)
                    {
                        string tenMoi = ThuMucAnh.TenDich(anhNguon, KhoaDangNhap());
                        choChep = new string[] { anhNguon, tenMoi };
                        return tenMoi;
                    }
                    return anhTen.Length == 0 ? (object)DBNull.Value : anhTen;
                default:
                    string s = ((TextBox)c).Text.Trim();
                    return s.Length == 0 ? (object)DBNull.Value : s;
            }
        }

        private string KhoaDangNhap()
        {
            return ((TextBox)o[truongKhoa]).Text.Trim();
        }

        private void Them()
        {
            if (dangSua)
            {
                TienIch.Canh("Đang chọn một dòng để sửa. Bấm \"Làm mới\" trước khi thêm mục mới.");
                return;
            }
            if (!KiemTra()) return;

            try
            {
                string khoa = KhoaDangNhap();
                int dem = Convert.ToInt32(Db.Scalar(
                    "SELECT COUNT(*) FROM " + cfg.Bang + " WHERE " + cfg.CotKhoa + " = @k", Db.P("@k", khoa)));
                if (dem > 0)
                {
                    BaoSai("Mã \"" + khoa + "\" đã tồn tại.", o[truongKhoa]);
                    return;
                }

                choChep = null;
                List<string> cot = new List<string>();
                List<string> tenThamSo = new List<string>();
                List<SqlParameter> ps = new List<SqlParameter>();
                int i = 0;
                foreach (Truong t in cfg.DsTruong)
                {
                    if (t.ChiDoc) continue;
                    string pn = "@p" + i;
                    cot.Add(t.Cot);
                    tenThamSo.Add(pn);
                    ps.Add(Db.P(pn, LayGiaTri(t)));
                    i++;
                }

                string sql = "INSERT INTO " + cfg.Bang + " (" + string.Join(", ", cot.ToArray()) + ") VALUES (" +
                             string.Join(", ", tenThamSo.ToArray()) + ")";
                Db.NonQuery(sql, ps.ToArray());
                ChepAnhCho();

                TaiLuoi();
                LamMoi();
                TienIch.ThongBao("Thêm thành công!");
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }

        private void Sua()
        {
            if (!dangSua)
            {
                TienIch.Canh("Hãy bấm chọn một dòng trong bảng trước khi sửa.");
                return;
            }
            if (!KiemTra()) return;

            try
            {
                choChep = null;
                List<string> gan = new List<string>();
                List<SqlParameter> ps = new List<SqlParameter>();
                int i = 0;
                foreach (Truong t in cfg.DsTruong)
                {
                    if (t.ChiDoc || t.KhoaChinh || t.ChiKhiThem) continue;
                    string pn = "@p" + i;
                    gan.Add(t.Cot + " = " + pn);
                    ps.Add(Db.P(pn, LayGiaTri(t)));
                    i++;
                }
                ps.Add(Db.P("@khoa", KhoaDangNhap()));

                string sql = "UPDATE " + cfg.Bang + " SET " + string.Join(", ", gan.ToArray()) +
                             " WHERE " + cfg.CotKhoa + " = @khoa";
                int n = Db.NonQuery(sql, ps.ToArray());

                if (n == 0)
                {
                    TienIch.Canh("Không tìm thấy bản ghi (có thể đã bị xoá).");
                    return;
                }
                ChepAnhCho();
                TaiLuoi();
                LamMoi();
                TienIch.ThongBao("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }

        private void Xoa()
        {
            if (!dangSua)
            {
                TienIch.Canh("Hãy bấm chọn một dòng trong bảng trước khi xoá.");
                return;
            }

            string khoa = KhoaDangNhap();
            try
            {
                foreach (PhuThuoc p in cfg.DsPhuThuoc)
                {
                    int n = Convert.ToInt32(Db.Scalar(
                        "SELECT COUNT(*) FROM " + p.Bang + " WHERE " + p.Cot + " = @k", Db.P("@k", khoa)));
                    if (n > 0)
                    {
                        TienIch.Canh("Không thể xoá \"" + khoa + "\" vì đang được dùng trong " + n + " " + p.Ten +
                                     ".\nHãy xử lý các bản ghi đó trước.");
                        return;
                    }
                }

                if (!TienIch.Hoi("Xoá " + cfg.TenDoiTuong + " \"" + khoa + "\"?")) return;

                Db.NonQuery("DELETE FROM " + cfg.Bang + " WHERE " + cfg.CotKhoa + " = @k", Db.P("@k", khoa));
                TaiLuoi();
                LamMoi();
                TienIch.ThongBao("Đã xoá.");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    TienIch.Canh("Không thể xoá vì dữ liệu này đang được nơi khác sử dụng.");
                }
                else
                {
                    TienIch.XuLyLoi(ex);
                }
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }
    }
}

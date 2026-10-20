using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using QuanLyBanTuBep.BLL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.UI
{
    public class BangView<T> where T : class, new()
    {
        private readonly BangBll<T> bll;
        private readonly Form form;
        private readonly CauHinhBang cfg;
        private readonly Dictionary<Truong, Control> o = new Dictionary<Truong, Control>();

        private readonly ListView lv;
        private readonly TextBox txtTim;
        private readonly Label lblDem;
        private readonly PictureBox picAnh;
        private readonly TextBox txtAnh;
        private readonly Button btnThem, btnSua, btnXoa, btnLamMoi, btnCsv, btnChonAnh;

        private string anhTen = "";
        private string anhNguon;
        private bool dangSua;
        private List<T> dsDanhMuc;

        public BangView(Form form, BangBll<T> bll, CauHinhBang cfg)
        {
            this.form = form;
            this.bll = bll;
            this.cfg = cfg;

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
            btnSua.BackColor = btnThem.BackColor;
            if (cfg.DungNgungKinhDoanh) btnXoa.Text = "Ngừng KD";

            GanO();

            btnThem.Click += delegate { Them(); };
            btnSua.Click += delegate { Sua(); };
            btnXoa.Click += delegate { Xoa(); };
            btnLamMoi.Click += delegate { LamMoi(); };
            btnCsv.Click += delegate { TienIch.XuatCsv(lv, cfg.TenFileCsv); };
            txtTim.TextChanged += delegate { ApDungLoc(); };
            lv.SelectedIndexChanged += Lv_Chon;
            if (btnChonAnh != null) btnChonAnh.Click += delegate { ChonAnh(); };

            DatCheDo(false);
            form.Load += delegate { TaiDuLieu(); };
        }

        // ====================== TÌM Ô NHẬP ======================

        private T1 Tim<T1>(string ten) where T1 : Control
        {
            T1 c = TimTuyChon<T1>(ten);
            if (c == null) throw new InvalidOperationException("Form " + form.Name + " thiếu control " + ten);
            return c;
        }

        private T1 TimTuyChon<T1>(string ten) where T1 : Control
        {
            Control[] ds = form.Controls.Find(ten, true);
            return ds.Length == 0 ? null : ds[0] as T1;
        }

        private void GanO()
        {
            foreach (Truong t in cfg.DsTruong)
            {
                string tienTo;
                switch (t.Kieu)
                {
                    case KieuTruong.So: tienTo = "nud"; break;
                    case KieuTruong.Ngay: tienTo = "dtp"; break;
                    case KieuTruong.Combo: tienTo = "cbo"; break;
                    case KieuTruong.CheckBox: tienTo = "chk"; break;
                    default: tienTo = "txt"; break;
                }
                Control c = Tim<Control>(tienTo + t.ThuocTinh);
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

        // ====================== NẠP DỮ LIỆU ======================

        private void TaiDuLieu()
        {
            try
            {
                NapCombo();
                TaiLuoi();
                LamMoi();
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
                List<LuaChon> ds = new List<LuaChon>(bll.LayLuaChon(t.ThuocTinh));
                if (t.ChoPhepRong) ds.Insert(0, new LuaChon(null, "(không chọn)"));
                cbo.DisplayMember = "Ten";
                cbo.ValueMember = "Ma";
                cbo.DataSource = ds;
                cbo.SelectedIndex = t.ChoPhepRong ? 0 : -1;
            }
        }

        private void TaiLuoi()
        {
            dsDanhMuc = bll.LayDanhSach();
            ApDungLoc();
            lv.SelectedItems.Clear();
        }

        private void ApDungLoc()
        {
            if (dsDanhMuc == null) return;
            string tuKhoa = txtTim.Text.Trim();
            PropertyInfo[] cot = cfg.CotLuoi.Select(c => typeof(T).GetProperty(c)).ToArray();

            List<T> ketQua = dsDanhMuc.Where(x => tuKhoa.Length == 0 || cot.Any(p =>
            {
                object v = p.GetValue(x, null);
                return v != null && v.ToString().IndexOf(tuKhoa, StringComparison.CurrentCultureIgnoreCase) >= 0;
            })).ToList();

            TienIch.NapLuoi(lv, ketQua, cfg.CotLuoi);
            lblDem.Text = ketQua.Count + " dòng";
        }

        private void Lv_Chon(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count == 0) return;
            try
            {
                T dong = (T)lv.SelectedItems[0].Tag;
                T model = bll.LayTheoMa(bll.Ma(dong));
                if (model == null)
                {
                    TienIch.Canh("Bản ghi không còn tồn tại.");
                    TaiLuoi();
                    return;
                }
                DocVaoO(model);
                DatCheDo(true);
            }
            catch (Exception ex)
            {
                TienIch.XuLyLoi(ex);
            }
        }

        // ====================== MODEL <-> Ô NHẬP ======================

        private static PropertyInfo Prop(Truong t)
        {
            return typeof(T).GetProperty(t.ThuocTinh);
        }

        private void DocVaoO(T model)
        {
            foreach (Truong t in cfg.DsTruong)
            {
                object v = Prop(t).GetValue(model, null);
                Control c = o[t];
                bool rong = v == null;

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

                    case KieuTruong.CheckBox:
                        ((CheckBox)c).Checked = !rong && Convert.ToBoolean(v);
                        break;

                    case KieuTruong.Anh:
                        anhTen = rong ? "" : Convert.ToString(v);
                        anhNguon = null;
                        if (txtAnh != null) txtAnh.Text = anhTen;
                        DatAnh(ThuMucAnh.Doc(anhTen));
                        break;
                }
            }
        }

        private T DocTuO()
        {
            T model = new T();
            foreach (Truong t in cfg.DsTruong)
            {
                if (t.ChiDoc) continue;
                PropertyInfo p = Prop(t);
                Control c = o[t];
                object giaTri;
                switch (t.Kieu)
                {
                    case KieuTruong.So:
                        giaTri = Convert.ChangeType(((NumericUpDown)c).Value, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType);
                        break;
                    case KieuTruong.Ngay:
                        DateTimePicker d = (DateTimePicker)c;
                        giaTri = d.Checked ? (object)d.Value.Date : null;
                        break;
                    case KieuTruong.Combo:
                        giaTri = TienIch.LayMa((ComboBox)c);
                        break;
                    case KieuTruong.CheckBox:
                        giaTri = ((CheckBox)c).Checked;
                        break;
                    case KieuTruong.Anh:
                        giaTri = anhTen.Length == 0 ? null : anhTen;
                        break;
                    default:
                        giaTri = ((TextBox)c).Text;
                        break;
                }
                p.SetValue(model, giaTri, null);
            }

            PropertyInfo pn = typeof(T).GetProperty("AnhNguon");
            if (pn != null) pn.SetValue(model, anhNguon, null);
            return model;
        }

        // ====================== ẢNH ======================

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
                if (txtAnh != null) txtAnh.Text = System.IO.Path.GetFileName(dlg.FileName) + " (mới)";
                DatAnh(thu);
            }
        }

        private void DatAnh(Image anh)
        {
            if (picAnh == null) return;
            Image cu = picAnh.Image;
            picAnh.Image = anh;
            if (cu != null) cu.Dispose();
        }

        // ====================== TRẠNG THÁI FORM ======================

        private void DatCheDo(bool sua)
        {
            dangSua = sua;
            foreach (Truong t in cfg.DsTruong)
            {
                bool khoa = t.ChiDoc || t.TuDongSinh || (sua && (t.KhoaChinh || t.ChiKhiThem));
                o[t].Enabled = !khoa;
                if (t.Kieu == KieuTruong.Anh && btnChonAnh != null) btnChonAnh.Enabled = !khoa;
            }
            if (cfg.DoiMauChuNutTheoCheDo)
            {
                // Giữ nút có màu chữ theo trạng thái; các thao tác vẫn được kiểm tra trong hàm xử lý.
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLamMoi.Enabled = true;
                Color mauChu = sua ? Color.Black : Color.White;
                btnThem.ForeColor = mauChu;
                btnSua.ForeColor = mauChu;
                btnXoa.ForeColor = mauChu;
                btnLamMoi.ForeColor = mauChu;
                if (cfg.DungNgungKinhDoanh)
                {
                    Truong t = cfg.DsTruong.FirstOrDefault(x => x.ThuocTinh == "NgungKinhDoanh");
                    CheckBox chk = t == null ? null : o[t] as CheckBox;
                    btnXoa.Text = sua && chk != null && chk.Checked ? "Kinh doanh lại" : "Ngừng KD";
                }
            }
            else
            {
                btnThem.Enabled = !sua;
                btnSua.Enabled = sua;
                btnXoa.Enabled = sua;
            }
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
                        if (cb.Items.Count > 0) cb.SelectedIndex = t.ChoPhepRong ? 0 : -1;
                        break;
                    case KieuTruong.CheckBox:
                        ((CheckBox)c).Checked = false;
                        break;
                    case KieuTruong.Anh:
                        anhTen = "";
                        anhNguon = null;
                        if (txtAnh != null) txtAnh.Text = "";
                        DatAnh(null);
                        break;
                }
            }
            TextBox maMoi = o[cfg.TruongKhoa] as TextBox;
            if (maMoi != null) maMoi.Text = bll.SinhMaMoi() ?? "";
            DatCheDo(false);
            lv.SelectedItems.Clear();
            o[cfg.TruongKhoa].Focus();
        }

        private void BaoLoi(Exception ex)
        {
            TienIch.XuLyLoi(ex);
            NghiepVuException nv = ex as NghiepVuException;
            if (nv == null || nv.Truong == null) return;
            Truong t = cfg.DsTruong.FirstOrDefault(x => x.ThuocTinh == nv.Truong);
            if (t != null && o[t].Enabled) o[t].Focus();
        }

        private string KhoaDangNhap()
        {
            return ((TextBox)o[cfg.TruongKhoa]).Text.Trim();
        }

        // ====================== THÊM / SỬA / XOÁ ======================

        private void Them()
        {
            if (dangSua)
            {
                TienIch.Canh("Đang chọn một dòng để sửa. Bấm \"Làm mới\" trước khi thêm mục mới.");
                return;
            }
            try
            {
                string canhBao = bll.Them(DocTuO());
                TaiLuoi();
                LamMoi();
                if (canhBao != null) TienIch.Canh(canhBao);
                TienIch.ThongBao("Thêm thành công!");
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void Sua()
        {
            if (!dangSua)
            {
                TienIch.Canh("Hãy bấm chọn một dòng trong bảng trước khi sửa.");
                return;
            }
            try
            {
                string canhBao = bll.Sua(DocTuO());
                TaiLuoi();
                LamMoi();
                if (canhBao != null) TienIch.Canh(canhBao);
                TienIch.ThongBao("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void Xoa()
        {
            if (!dangSua)
            {
                TienIch.Canh(cfg.DungNgungKinhDoanh
                    ? "Hãy bấm chọn một mặt hàng trước khi đổi trạng thái kinh doanh."
                    : "Hãy bấm chọn một dòng trong bảng trước khi xoá.");
                return;
            }

            string khoa = KhoaDangNhap();
            try
            {
                if (cfg.DungNgungKinhDoanh)
                {
                    HangHoa hang = lv.SelectedItems[0].Tag as HangHoa;
                    HangHoaBll bllHang = bll as HangHoaBll;
                    if (hang == null || bllHang == null)
                        throw new InvalidOperationException("Không thể cập nhật trạng thái mặt hàng.");

                    bool ngung = !hang.NgungKinhDoanh;
                    string xacNhan = ngung
                        ? "Ngừng kinh doanh mặt hàng \"" + khoa + "\"? Mặt hàng vẫn được giữ trong lịch sử hóa đơn."
                        : "Cho mặt hàng \"" + khoa + "\" kinh doanh trở lại?";
                    if (!TienIch.Hoi(xacNhan)) return;

                    bllHang.DoiTrangThaiKinhDoanh(khoa, ngung);
                    TaiLuoi();
                    LamMoi();
                    TienIch.ThongBao(ngung ? "Đã ngừng kinh doanh mặt hàng." : "Mặt hàng đã được kinh doanh trở lại.");
                    return;
                }

                bll.KiemTraXoa(khoa);
                if (!TienIch.Hoi("Xoá " + bll.TenDoiTuong + " \"" + khoa + "\"?")) return;

                bll.Xoa(khoa);
                TaiLuoi();
                LamMoi();
                TienIch.ThongBao("Đã xoá.");
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }
    }
}


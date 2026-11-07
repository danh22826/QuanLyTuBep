using System;
using System.Collections.Generic;
using System.Reflection;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public abstract class BangBll<T> where T : class, new()
    {
        protected abstract BangDal<T> Dal { get; }

        public abstract string TenDoiTuong { get; }

        public abstract string TruongKhoa { get; }

        protected abstract void KiemTra(T x, bool themMoi);

        protected virtual void KiemTraTruocKhiXoa(string ma)
        {
        }

        public virtual IList<LuaChon> LayLuaChon(string truong)
        {
            return new List<LuaChon>();
        }

        public string Ma(T x)
        {
            return Convert.ToString(typeof(T).GetProperty(TruongKhoa).GetValue(x, null));
        }

        public virtual string SinhMaMoi() { return null; }

        public virtual List<T> LayDanhSach()
        {
            PhanQuyen.YeuCau(PhanQuyen.QuyenBang(typeof(T), "Xem"));
            return Dal.LayTatCa();
        }

        public virtual T LayTheoMa(string ma)
        {
            PhanQuyen.YeuCau(PhanQuyen.QuyenBang(typeof(T), "Xem"));
            return Dal.TimTheoMa(ma);
        }

        public virtual string Them(T x)
        {
            PhanQuyen.YeuCau(PhanQuyen.QuyenBang(typeof(T), "Them"));
            ChuanHoa(x);
            KiemTra(x, true);
            if (Dal.TonTai(Ma(x)))
                throw new NghiepVuException("Mã \"" + Ma(x) + "\" đã tồn tại.", TruongKhoa);
            Dal.Them(x);
            return null;
        }

        public virtual string Sua(T x)
        {
            PhanQuyen.YeuCau(PhanQuyen.QuyenBang(typeof(T), "Sua"));
            ChuanHoa(x);
            KiemTra(x, false);
            if (Dal.Sua(x) == 0)
                throw new NghiepVuException("Không tìm thấy bản ghi (có thể đã bị xoá).");
            return null;
        }

        public void KiemTraXoa(string ma)
        {
            PhanQuyen.YeuCau(PhanQuyen.QuyenBang(typeof(T), "Xoa"));
            KiemTraTruocKhiXoa(ma);
        }

        public void Xoa(string ma)
        {
            PhanQuyen.YeuCau(PhanQuyen.QuyenBang(typeof(T), "Xoa"));
            // CSDL dùng ON DELETE SET NULL / CASCADE nên sẽ không tự chặn: phải kiểm tra tham chiếu ở đây.
            KiemTraTruocKhiXoa(ma);
            Dal.Xoa(ma);
        }

        protected static void CamNeuCon(int soThamChieu, string tenBangThamChieu, string ma)
        {
            if (soThamChieu > 0)
                throw new NghiepVuException("Không thể xoá \"" + ma + "\" vì đang được dùng trong " + soThamChieu + " " +
                                            tenBangThamChieu + ".\nHãy xử lý các bản ghi đó trước.");
        }

        private static void ChuanHoa(T x)
        {
            foreach (PropertyInfo p in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (p.PropertyType != typeof(string) || !p.CanRead || !p.CanWrite) continue;
                string s = (string)p.GetValue(x, null);
                if (s == null) continue;
                s = s.Trim();
                p.SetValue(x, s.Length == 0 ? null : s, null);
            }
        }
    }
}



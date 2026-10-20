using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public abstract class BangDal<T> where T : class, new()
    {
        protected abstract string Bang { get; }
        protected abstract string CotKhoa { get; }

        protected abstract string[] CotGhi { get; }

        protected virtual string[] CotChiKhiThem { get { return new string[0]; } }

        protected virtual string TruyVanChon { get { return "SELECT * FROM " + Bang; } }

        protected virtual string KhoaTruyVan { get { return CotKhoa; } }

        public List<T> LayTatCa()
        {
            return DocDuLieu.DanhSach<T>(Db.Query(TruyVanChon + " ORDER BY " + KhoaTruyVan));
        }

        public T TimTheoMa(string ma)
        {
            System.Data.DataTable dt = Db.Query(TruyVanChon + " WHERE " + KhoaTruyVan + " = @k", Db.P("@k", ma));
            return dt.Rows.Count == 0 ? null : DocDuLieu.Dong<T>(dt.Rows[0]);
        }

        public string SinhMaTuDong(string tienTo)
        {
            object o = Db.Scalar(
                "SELECT MAX(TRY_CAST(SUBSTRING(" + CotKhoa + ", @viTri, 18) AS INT)) FROM " + Bang +
                " WHERE " + CotKhoa + " LIKE @mau",
                Db.P("@viTri", tienTo.Length + 1), Db.P("@mau", tienTo + "[0-9]%"));
            int max = (o == null || o is DBNull) ? 0 : Convert.ToInt32(o);
            return tienTo + (max + 1).ToString("00");
        }

        public bool TonTai(string ma)
        {
            return Dem(Bang, CotKhoa, ma) > 0;
        }

        public void Them(T x)
        {
            string cot = string.Join(", ", CotGhi);
            List<string> thamSo = new List<string>();
            List<SqlParameter> ps = new List<SqlParameter>();
            for (int i = 0; i < CotGhi.Length; i++)
            {
                thamSo.Add("@p" + i);
                ps.Add(Db.P("@p" + i, GiaTri(x, CotGhi[i])));
            }
            Db.NonQuery("INSERT INTO " + Bang + " (" + cot + ") VALUES (" + string.Join(", ", thamSo.ToArray()) + ")", ps.ToArray());
        }

        public int Sua(T x)
        {
            List<string> gan = new List<string>();
            List<SqlParameter> ps = new List<SqlParameter>();
            int i = 0;
            foreach (string cot in CotGhi)
            {
                if (cot == CotKhoa || Array.IndexOf(CotChiKhiThem, cot) >= 0) continue;
                gan.Add(cot + " = @p" + i);
                ps.Add(Db.P("@p" + i, GiaTri(x, cot)));
                i++;
            }
            ps.Add(Db.P("@k", GiaTri(x, CotKhoa)));
            return Db.NonQuery("UPDATE " + Bang + " SET " + string.Join(", ", gan.ToArray()) + " WHERE " + CotKhoa + " = @k", ps.ToArray());
        }

        public int Xoa(string ma)
        {
            return Db.NonQuery("DELETE FROM " + Bang + " WHERE " + CotKhoa + " = @k", Db.P("@k", ma));
        }

        protected static int Dem(string bang, string cot, string ma)
        {
            return Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM " + bang + " WHERE " + cot + " = @k", Db.P("@k", ma)));
        }

        private static object GiaTri(T x, string tenThuocTinh)
        {
            PropertyInfo p = typeof(T).GetProperty(tenThuocTinh);
            if (p == null) throw new InvalidOperationException("Model " + typeof(T).Name + " thiếu thuộc tính " + tenThuocTinh);
            return p.GetValue(x, null);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace QuanLyBanTuBep.DAL
{
    internal static class DocDuLieu
    {
        private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> boNho =
            new Dictionary<Type, Dictionary<string, PropertyInfo>>();

        private static Dictionary<string, PropertyInfo> ThuocTinh(Type kieu)
        {
            lock (boNho)
            {
                Dictionary<string, PropertyInfo> ds;
                if (!boNho.TryGetValue(kieu, out ds))
                {
                    ds = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
                    foreach (PropertyInfo p in kieu.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (p.CanWrite && p.GetIndexParameters().Length == 0) ds[p.Name] = p;
                    }
                    boNho[kieu] = ds;
                }
                return ds;
            }
        }

        public static T Dong<T>(DataRow dong) where T : new()
        {
            T model = new T();
            Dictionary<string, PropertyInfo> ds = ThuocTinh(typeof(T));
            foreach (DataColumn cot in dong.Table.Columns)
            {
                PropertyInfo p;
                if (!ds.TryGetValue(cot.ColumnName, out p)) continue;

                object giaTri = dong[cot];
                if (giaTri == DBNull.Value) continue;

                Type dich = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                p.SetValue(model, dich == giaTri.GetType() ? giaTri : Convert.ChangeType(giaTri, dich), null);
            }
            return model;
        }

        public static List<T> DanhSach<T>(DataTable bang) where T : new()
        {
            List<T> ds = new List<T>(bang.Rows.Count);
            foreach (DataRow dong in bang.Rows) ds.Add(Dong<T>(dong));
            return ds;
        }
    }
}

using System;
using System.Drawing;
using System.IO;

namespace QuanLyBanTuBep.UI
{
    public static class ThuMucAnh
    {
        public static string ThuMucGhi()
        {
            string goc = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo d = new DirectoryInfo(goc);
            for (int i = 0; i < 6 && d != null; i++, d = d.Parent)
            {
                if (d.GetFiles("*.csproj").Length > 0) return Path.Combine(d.FullName, "Images");
            }
            return Path.Combine(goc, "Images");
        }

        public static string TenDich(string duongDanNguon, string ma)
        {
            string sach = ma;
            foreach (char ch in Path.GetInvalidFileNameChars()) sach = sach.Replace(ch.ToString(), "_");
            return sach + Path.GetExtension(duongDanNguon).ToLowerInvariant();
        }

        public static void Chep(string duongDanNguon, string tenDich)
        {
            string thuMuc = ThuMucGhi();
            Directory.CreateDirectory(thuMuc);
            string dich = Path.Combine(thuMuc, tenDich);
            if (string.Equals(Path.GetFullPath(duongDanNguon), Path.GetFullPath(dich), StringComparison.OrdinalIgnoreCase)) return;
            File.Copy(duongDanNguon, dich, true);
        }

        public static Image Doc(string tenFile)
        {
            if (string.IsNullOrWhiteSpace(tenFile)) return null;
            string ten = Path.GetFileName(tenFile.Trim());

            string[] thuMuc = { ThuMucGhi(), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images") };
            foreach (string tm in thuMuc)
            {
                string p = Path.Combine(tm, ten);
                if (File.Exists(p)) return DocFile(p);
            }
            return null;
        }

        public static Image DocFile(string duongDan)
        {
            try
            {
                using (FileStream fs = new FileStream(duongDan, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Image goc = Image.FromStream(fs))
                {
                    return new Bitmap(goc);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

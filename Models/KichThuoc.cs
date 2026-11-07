using System.ComponentModel;
using System.Text.RegularExpressions;

namespace QuanLyBanTuBep.Models
{
    public class KichThuoc
    {
        private static readonly Regex MauCu = new Regex(
            @"^(\s*Dài\s*[0-9]+(?:[.,][0-9]+)?\s*m\s*[x×]\s*)(?:Sâu|Rộng)(\s*[0-9]+(?:[.,][0-9]+)?\s*m\s*)$",
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        private string tenKichThuoc;

        [DisplayName("Mã kích thước")]
        public string MaKichThuoc { get; set; }

        [DisplayName("Kích thước")]
        public string TenKichThuoc
        {
            get { return tenKichThuoc; }
            set { tenKichThuoc = TenHienThiChieuRong(value); }
        }

        public static string TenHienThiChieuRong(string ten)
        {
            if (string.IsNullOrEmpty(ten)) return ten;
            Match match = MauCu.Match(ten);
            return match.Success ? match.Groups[1].Value + "Rộng" + match.Groups[2].Value : ten;
        }
    }
}

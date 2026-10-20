using System.Collections.Generic;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public class KhachHangDal : BangDal<KhachHang>
    {
        protected override string Bang { get { return "KhachHang"; } }
        protected override string CotKhoa { get { return "MaKhach"; } }
        protected override string[] CotGhi { get { return new[] { "MaKhach", "TenKhach", "DienThoai", "DiaChi" }; } }

        public string SinhMaTuDong()
        {
            object o = Db.Scalar("SELECT MAX(TRY_CAST(SUBSTRING(MaKhach, 3, 18) AS INT)) FROM KhachHang WHERE MaKhach LIKE 'KH[0-9]%' ");
            int max = (o == null || o is System.DBNull) ? 0 : System.Convert.ToInt32(o);
            return "KH" + (max + 1).ToString("00");
        }

        public int DemHoaDonBan(string ma)
        {
            return Dem("HoaDonBan", "MaKhach", ma);
        }

        public List<KhachHang> LayDanhSachBanHang()
        {
            return DocDuLieu.DanhSach<KhachHang>(Db.Query(
                "SELECT MaKhach, TenKhach, DiaChi, DienThoai FROM KhachHang " +
                "ORDER BY CASE WHEN MaKhach = N'KL00' THEN 0 ELSE 1 END, MaKhach"));
        }
    }
}




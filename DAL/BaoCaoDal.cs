using System.Collections.Generic;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public class BaoCaoDal
    {
        public List<ThongKeSanPham> Top3SanPhamTheoQuy(int nam, int quy)
        {
            return DocDuLieu.DanhSach<ThongKeSanPham>(Db.Proc("sp_BaoCaoTop3SanPhamTheoQuy", Db.P("@Nam", nam), Db.P("@Quy", quy)));
        }

        public List<DongHoaDonNhapTheoNV> HoaDonNhapTheoNhanVien(string maNV)
        {
            return DocDuLieu.DanhSach<DongHoaDonNhapTheoNV>(Db.Proc("sp_BaoCaoHoaDonNhapTheoNV", Db.P("@MaNV", maNV)));
        }

        public List<ThongKeKhachHang> KhachHangTheoThang(int nam, int thang)
        {
            return DocDuLieu.DanhSach<ThongKeKhachHang>(Db.Proc("sp_BaoCaoKhachHangTheoThang", Db.P("@Nam", nam), Db.P("@Thang", thang)));
        }

        public TongQuan TongQuan()
        {
            return new TongQuan
            {
                SoMatHang = System.Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM DMHangHoa")),
                TongTon = System.Convert.ToInt32(Db.Scalar("SELECT ISNULL(SUM(SoLuong), 0) FROM DMHangHoa")),
                SoHoaDonBan = System.Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM HoaDonBan")),
                DoanhThu = System.Convert.ToDecimal(Db.Scalar("SELECT ISNULL(SUM(TongTien),0) FROM HoaDonBan"))
            };
        }
    }
}

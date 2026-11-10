using System.Collections.Generic;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public interface IHangHoaDal : IBangDal<HangHoa>
    {
        string SinhMaTuDong();
        int DemChiTietBan(string ma);
        int DemChiTietNhap(string ma);
        int DatTrangThai(string ma, bool ngung);
        List<HangHoa> TimKiemQuanLy(string maChatLieu, string maNuocSX, string baoHanh, bool? ngungKinhDoanh);
        int DemNgungKinhDoanh();
    }
}

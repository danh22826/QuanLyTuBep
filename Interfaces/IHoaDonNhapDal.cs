using System.Collections.Generic;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public interface IHoaDonNhapDal
    {
        List<HoaDonNhap> LayTatCa();
        HoaDonNhap TimTheoSo(string so);
        KetQuaGhiHoaDon Luu(HoaDonNhap hd);
        string SinhSo();
        bool TonTai(string so);
        KetQuaGhiHoaDon Xoa(string so);
    }
}

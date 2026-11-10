using System;
using System.Collections.Generic;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public interface IHoaDonBanDal
    {
        List<HoaDonBan> LayTatCa();
        List<HoaDonBan> LayGanDay(int soLuong);
        HoaDonBan TimTheoSo(string so);
        DateTime? NgayGanNhat();
        List<HoaDonBan> TimKiem(string maHang, DateTime? ngayBan, decimal? tongTienTu, decimal? tongTienDen);
        List<HoaDonBan> Top5NhoNhatTheoQuy(int nam, int quy);
        KetQuaGhiHoaDon Luu(HoaDonBan hd);
        string SinhSo();
        bool TonTai(string so);
        KetQuaGhiHoaDon Xoa(string so);
    }
}

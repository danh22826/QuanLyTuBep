using System.Collections.Generic;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class TrangChuBll
    {
        public const int NguongSapHet = 3;
        private readonly BaoCaoDal baoCao = new BaoCaoDal();
        private readonly HangHoaDal hang = new HangHoaDal();
        private readonly HoaDonBanDal ban = new HoaDonBanDal();

        public TongQuan TongQuan() { PhanQuyen.YeuCau(Quyen.XemTrangChu); return baoCao.TongQuan(); }
        public List<HangHoa> HangSapHet() { PhanQuyen.YeuCau(Quyen.XemTonKho); return hang.TonDuoi(NguongSapHet); }
        public List<HoaDonBan> HoaDonGanDay() { PhanQuyen.YeuCau(Quyen.XemLichSuBan); return ban.LayGanDay(8); }
    }
}

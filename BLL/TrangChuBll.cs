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

        public TongQuan TongQuan() { return baoCao.TongQuan(); }
        public List<HangHoa> HangSapHet() { return hang.TonDuoi(NguongSapHet); }
        public List<HoaDonBan> HoaDonGanDay() { return ban.LayGanDay(8); }
    }
}

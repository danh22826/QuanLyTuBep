using System;
using System.Collections.Generic;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class TrangChuBll
    {
        public const int NguongSapHet = 3;
        private readonly IBaoCaoDal baoCao;
        private readonly HangHoaDal hang = new HangHoaDal();
        private readonly IHoaDonBanDal ban;

        public TrangChuBll() : this(new BaoCaoDal(), new HoaDonBanDal())
        {
        }

        public TrangChuBll(IBaoCaoDal baoCao, IHoaDonBanDal ban)
        {
            this.baoCao = baoCao ?? throw new ArgumentNullException(nameof(baoCao));
            this.ban = ban ?? throw new ArgumentNullException(nameof(ban));
        }

        public TongQuan TongQuan() { PhanQuyen.YeuCau(Quyen.XemTrangChu); return baoCao.TongQuan(); }
        public List<HangHoa> HangSapHet() { PhanQuyen.YeuCau(Quyen.XemTonKho); return hang.TonDuoi(NguongSapHet); }
        public List<HoaDonBan> HoaDonGanDay() { PhanQuyen.YeuCau(Quyen.XemLichSuBan); return ban.LayGanDay(8); }
    }
}

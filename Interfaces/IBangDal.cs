using System.Collections.Generic;

namespace QuanLyBanTuBep.DAL
{
    public interface IBangDal<T> where T : class, new()
    {
        List<T> LayTatCa();
        T TimTheoMa(string ma);
        bool TonTai(string ma);
        void Them(T x);
        int Sua(T x);
        int Xoa(string ma);
    }
}

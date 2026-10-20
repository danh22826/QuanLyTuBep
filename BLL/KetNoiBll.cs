using QuanLyBanTuBep.DAL;

namespace QuanLyBanTuBep.BLL
{
    public static class KetNoiBll
    {
        public static bool ThuKetNoi(out string loi)
        {
            return Db.ThuKetNoi(out loi);
        }
    }
}

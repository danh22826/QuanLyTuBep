using System;
using System.Data.SqlClient;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public abstract class HoaDonDalBase
    {
        protected abstract string Bang { get; }
        protected abstract string CotKhoa { get; }
        protected abstract string TienTo { get; }

        public bool TonTai(string so)
        {
            return Convert.ToInt32(Db.Scalar(
                "SELECT COUNT(*) FROM " + Bang + " WHERE " + CotKhoa + " = @so", Db.P("@so", so))) > 0;
        }

        public string SinhSo()
        {
            object o = Db.Scalar(
                "SELECT MAX(TRY_CAST(SUBSTRING(" + CotKhoa + ", 4, 20) AS INT)) FROM " + Bang +
                " WHERE " + CotKhoa + " LIKE '" + TienTo + "%'");
            int max = (o == null || o is DBNull) ? 0 : Convert.ToInt32(o);
            return TienTo + (max + 1).ToString("00");
        }

        public KetQuaGhiHoaDon Xoa(string so)
        {
            KetQuaGhiHoaDon kq = new KetQuaGhiHoaDon();
            Db.GiaoDich(delegate (SqlConnection conn, SqlTransaction tran)
            {
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT 1 FROM " + Bang + " WITH (UPDLOCK, HOLDLOCK) WHERE " + CotKhoa + " = @so", conn, tran))
                {
                    cmd.Parameters.Add(Db.P("@so", so));
                    if (cmd.ExecuteScalar() == null) return;
                    kq.HoaDonTonTai = true;
                }

                KiemTraTruocKhiXoa(conn, tran, so, kq);
                if (kq.HangThieuTon.Count > 0) return;

                Db.NonQuery(conn, tran, "DELETE FROM " + Bang + " WHERE " + CotKhoa + " = @so", Db.P("@so", so));
                kq.DaGhi = true;
            });
            return kq;
        }

        protected virtual void KiemTraTruocKhiXoa(SqlConnection conn, SqlTransaction tran, string so, KetQuaGhiHoaDon kq)
        {
        }
    }
}

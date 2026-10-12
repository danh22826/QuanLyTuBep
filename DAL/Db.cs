using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanTuBep.DAL
{
    public static class Db
    {
        private static string ChuoiKetNoi
        {
            get
            {
                ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings["QuanLyBanTuBep"];
                if (cs == null)
                {
                    throw new InvalidOperationException("Thiếu connection string \"QuanLyBanTuBep\" trong App.config.");
                }
                return cs.ConnectionString;
            }
        }

        public static SqlParameter P(string ten, object giaTri)
        {
            return new SqlParameter(ten, giaTri ?? DBNull.Value);
        }

        public static SqlParameter PNgay(string ten, DateTime? giaTri)
        {
            SqlParameter p = new SqlParameter(ten, SqlDbType.Date);
            p.Value = giaTri.HasValue ? (object)giaTri.Value.Date : DBNull.Value;
            return p;
        }

        public static bool ThuKetNoi(out string loi)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ChuoiKetNoi))
                {
                    conn.Open();
                }
                loi = null;
                return true;
            }
            catch (Exception ex)
            {
                loi = ex.Message;
                return false;
            }
        }

        public static DataTable Query(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(ChuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static int NonQuery(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(ChuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static object Scalar(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(ChuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        public static DataTable Proc(string tenProc, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(ChuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand(tenProc, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (ps != null) cmd.Parameters.AddRange(ps);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static void GiaoDich(Action<SqlConnection, SqlTransaction> viec)
        {
            using (SqlConnection conn = new SqlConnection(ChuoiKetNoi))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        viec(conn, tran);
                        tran.Commit();
                    }
                    catch
                    {
                        try { tran.Rollback(); } catch { }
                        throw;
                    }
                }
            }
        }

        public static int NonQuery(SqlConnection conn, SqlTransaction tran, string sql, params SqlParameter[] ps)
        {
            using (SqlCommand cmd = new SqlCommand(sql, conn, tran))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                return cmd.ExecuteNonQuery();
            }
        }

        public static DateTime? NgayBanGanNhat()
        {
            object o = Scalar("SELECT MAX(NgayBan) FROM HoaDonBan");
            if (o == null || o is DBNull) return null;
            return Convert.ToDateTime(o);
        }
    }
}

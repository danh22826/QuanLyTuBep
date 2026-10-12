using System;
using System.Windows.Forms;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.UI;

namespace QuanLyBanTuBep
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string loi;
            if (!Db.ThuKetNoi(out loi))
            {
                MessageBox.Show(
                    "Không kết nối được cơ sở dữ liệu QuanLyBanTuBep.\n\n" +
                    "Chi tiết: " + loi + "\n\n" +
                    "Hãy kiểm tra:\n" +
                    "1. SQL Server đã chạy chưa.\n" +
                    "2. Đã chạy file SQL\\QuanLyBanTuBep.sql trong SSMS chưa.\n" +
                    "3. Data Source trong App.config có đúng tên server không " +
                    "(. hoặc .\\SQLEXPRESS hoặc (localdb)\\MSSQLLocalDB).",
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new FormMain());
        }
    }
}

using System;
using System.Data.SqlClient;

namespace QuanLyBanTuBep.DAL
{
    public static class LoiCsdl
    {
        public static string MoTa(Exception ex)
        {
            SqlException sql = ex as SqlException;
            if (sql == null) return null;

            string thongDiep;
            switch (sql.Number)
            {
                case 2601:
                case 2627:
                    thongDiep = "Dữ liệu bị trùng với một mã đã có trong cơ sở dữ liệu.";
                    break;
                case 547:
                    thongDiep = "Không thể thực hiện vì dữ liệu liên quan đang được sử dụng hoặc không hợp lệ.";
                    break;
                case 515:
                    thongDiep = "Thiếu thông tin bắt buộc. Hãy kiểm tra lại các trường đã nhập.";
                    break;
                case 51001:
                    thongDiep = "Tồn kho không đủ để hoàn tất hóa đơn bán.";
                    break;
                case 1205:
                    thongDiep = "Giao dịch đang xung đột với một thao tác khác. Hãy thử lưu lại.";
                    break;
                case -2:
                    thongDiep = "Kết nối cơ sở dữ liệu quá thời gian chờ. Hãy thử lại.";
                    break;
                case 53:
                case -1:
                case 2:
                case 4060:
                case 18456:
                case 10054:
                case 10060:
                case 11001:
                    thongDiep = "Không kết nối được cơ sở dữ liệu. Kiểm tra SQL Server và Data Source trong App.config.";
                    break;
                default:
                    thongDiep = "Không thể hoàn tất thao tác với cơ sở dữ liệu.";
                    break;
            }
            return thongDiep + "\n\nChi tiết: " + sql.Message;
        }
    }
}

using System;
using System.ComponentModel;

namespace QuanLyBanTuBep.Models
{
    public interface IHoaDon
    {
        [DisplayName("Số hoá đơn")] string So { get; }
        [DisplayName("Ngày")] DateTime Ngay { get; }
        string MaNV { get; }
        string MaDoiTac { get; }
        [DisplayName("Tổng tiền")] decimal TongTien { get; }
    }
}

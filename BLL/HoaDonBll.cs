using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public static class HoaDonBll
    {
        public static string SinhMa(LoaiHoaDon loai)
        {
            return HoaDonDal.SinhMa(loai);
        }

        public static void Luu(HoaDon hd)
        {
            if (hd == null) throw new ArgumentNullException("hd");

            string so = (hd.So ?? "").Trim();
            hd.So = so;

            if (so.Length == 0)
                throw new NghiepVuException("Chưa có số hoá đơn.");
            if (so.Length > 20 || !Regex.IsMatch(so, "^[A-Za-z0-9_]+$"))
                throw new NghiepVuException("Số hoá đơn chỉ gồm chữ không dấu, số, gạch dưới và tối đa 20 ký tự.");
            if (string.IsNullOrEmpty(hd.MaNV))
                throw new NghiepVuException("Vui lòng chọn nhân viên lập hoá đơn.");
            if (string.IsNullOrEmpty(hd.MaDoiTac))
                throw new NghiepVuException(hd.Loai == LoaiHoaDon.Ban ? "Vui lòng chọn khách hàng." : "Vui lòng chọn nhà cung cấp.");
            if (hd.Dong.Count == 0)
                throw new NghiepVuException("Hoá đơn chưa có mặt hàng nào. Hãy thêm ít nhất một dòng.");
            if (HoaDonDal.TonTai(hd.Loai, so))
                throw new NghiepVuException("Số hoá đơn \"" + so + "\" đã tồn tại. Bấm \"Hoá đơn mới\" để lấy số mới.");

            HashSet<string> daCo = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (DongHoaDon d in hd.Dong)
            {
                if (!daCo.Add(d.MaHang))
                    throw new NghiepVuException("Mặt hàng \"" + d.MaHang + "\" xuất hiện nhiều lần trong hoá đơn.");
                if (d.SoLuong <= 0)
                    throw new NghiepVuException("Số lượng của mặt hàng \"" + d.MaHang + "\" phải lớn hơn 0.");
                if (d.GiamGia < 0 || d.GiamGia > 100)
                    throw new NghiepVuException("Giảm giá của mặt hàng \"" + d.MaHang + "\" phải từ 0 đến 100%.");

                if (hd.Loai == LoaiHoaDon.Ban)
                {
                    int? ton = HoaDonDal.TonKho(d.MaHang);
                    if (ton == null)
                        throw new NghiepVuException("Mặt hàng \"" + d.MaHang + "\" không tồn tại.");

                    if (ton.Value < d.SoLuong)
                        throw new NghiepVuException("Mặt hàng \"" + HoaDonDal.TenHang(d.MaHang) + "\" chỉ còn " +
                                                    ton.Value + ", không đủ để bán " + d.SoLuong + ".");
                }
                else
                {
                    if (d.DonGia <= 0)
                        throw new NghiepVuException("Đơn giá nhập của mặt hàng \"" + d.MaHang + "\" phải lớn hơn 0.");
                }
            }

            HoaDonDal.Luu(hd);
        }

        public static void Xoa(LoaiHoaDon loai, string so)
        {
            if (!HoaDonDal.TonTai(loai, so))
                throw new NghiepVuException("Hoá đơn \"" + so + "\" không còn tồn tại.");

            if (loai == LoaiHoaDon.Nhap)
            {
                DataTable thieu = HoaDonDal.HangNhapKhongDuTon(so);
                if (thieu.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Không thể xoá hoá đơn nhập này vì một số mặt hàng đã được bán, tồn kho không đủ để trừ lại:");
                    foreach (DataRow r in thieu.Rows)
                    {
                        sb.AppendLine("- " + r["TenHang"] + ": đã nhập " + r["SoLuongNhap"] + ", tồn hiện tại " + r["TonHienTai"]);
                    }
                    throw new NghiepVuException(sb.ToString());
                }
            }

            HoaDonDal.Xoa(loai, so);
        }
    }
}

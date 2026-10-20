using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class BanHangBll : HoaDonBll
    {
        private readonly HoaDonBanDal dal = new HoaDonBanDal();
        private readonly KhachHangDal khachDal = new KhachHangDal();
        private readonly KhachHangBll khachBll = new KhachHangBll();

        public override LoaiHoaDon Loai { get { return LoaiHoaDon.Ban; } }
        public override bool ChoSuaDonGia { get { return false; } }
        public override decimal LayDonGia(HangHoa hang) { return hang.DonGiaBan; }
        public override string SinhMa() { return dal.SinhSo(); }
        public KhachHang TaoKhachMoi(KhachHang khach) { return khachBll.TaoKhachMoi(khach); }

        public override IList<IHoaDon> DanhSach()
        {
            return dal.LayTatCa().Cast<IHoaDon>().ToList();
        }

        public override IList<LuaChon> DanhSachDoiTac()
        {
            return khachBll.DanhSachChonBanHang();
        }

        protected override bool TonTai(string so) { return dal.TonTai(so); }

        protected override IHoaDon LayDau(string so, List<IChiTietHoaDon> chiTiet)
        {
            HoaDonBan hd = dal.TimTheoSo(so);
            if (hd != null) chiTiet.AddRange(hd.ChiTiet.Cast<IChiTietHoaDon>());
            return hd;
        }

        protected override IChiTietHoaDon TaoDong(string so, HangHoa hang, int soLuong, decimal donGia, double giamGia, decimal thanhTien)
        {
            return new ChiTietHoaDonBan
            {
                SoHDB = so, MaHang = hang.MaHang, TenHang = hang.TenHang,
                TenKichThuoc = hang.TenKichThuoc, TenChatLieu = hang.TenChatLieu,
                TenMau = hang.TenMau, TenNuocSX = hang.TenNuocSX, SoLuong = soLuong,
                DonGia = donGia, GiamGia = giamGia, ThanhTien = thanhTien
            };
        }

        protected override string KiemTraKhiThemDong(HangHoa hang, int soLuong, decimal donGia)
        {
            if (soLuong > hang.SoLuong) return "Tồn kho chỉ còn " + hang.SoLuong + ", không đủ để bán " + soLuong + ".";
            if (hang.SoLuong > 0 && hang.ConLaiLo <= 0)
                return "Mặt hàng chưa có chứng từ nhập tương ứng với tồn kho. Hãy kiểm tra tồn đầu kỳ và chạy giai đoạn UPGRADE C trong SQL\\QuanLyBanTuBep_TongHop.sql.";
            if (soLuong > hang.ConLaiLo) return LoiVuotLo(hang.TenDayDu, hang.ConLaiLo, hang.SoLuong);
            return null;
        }

        private static string LoiVuotLo(string tenHang, int conLaiLo, int tonHienTai)
        {
            return "Mặt hàng \"" + tenHang + "\": giá bán hiện tại chỉ áp dụng cho " + conLaiLo + " cái còn lại của lô đang bán (còn " +
                   tonHienTai + " cái tính cả các lô sau, giá khác). Hãy bán " + conLaiLo +
                   " cái trước, rồi lập hoá đơn khác cho phần còn lại.";
        }

        protected override void KiemTraKhiLuu(IChiTietHoaDon d)
        {
            if (d.DonGia < 0)
                throw new NghiepVuException("Đơn giá bán của mặt hàng \"" + d.MaHang + "\" không hợp lệ.");
        }

        protected override KetQuaGhiHoaDon Ghi(string so, DateTime ngay, string maNV, string maKhach, string tenKhach, string diaChiKhach, string dienThoaiKhach, IList<IChiTietHoaDon> dong)
        {
            maKhach = string.IsNullOrWhiteSpace(maKhach) ? KhachHang.MaKhachLe : maKhach.Trim();
            if (!khachDal.TonTai(maKhach))
                throw new NghiepVuException("Khách hàng " + maKhach + " không tồn tại. Hãy chọn khách hàng có sẵn hoặc tạo khách mới.", "MaDoiTac");
            HoaDonBan hd = new HoaDonBan
            {
                SoHDB = so,
                NgayBan = ngay,
                MaNV = maNV,
                MaKhach = maKhach
            };
            hd.ChiTiet.AddRange(dong.Cast<ChiTietHoaDonBan>());
            return dal.Luu(hd);
        }

        protected override void BaoLoiGhi(KetQuaGhiHoaDon kq, string so)
        {
            if (kq.HangNgungKinhDoanh.Count > 0)
                throw new NghiepVuException("Không thể bán mặt hàng \"" + kq.HangNgungKinhDoanh[0].TenDayDu + "\" vì mặt hàng đã ngừng kinh doanh.");
            HangThieuTon khongCo = kq.HangThieuTon.FirstOrDefault(h => h.KhongTonTai);
            if (khongCo != null)
                throw new NghiepVuException("Mặt hàng \"" + khongCo.MaHang + "\" không tồn tại.");
            if (kq.HangThieuTon.Count > 0)
            {
                HangThieuTon h = kq.HangThieuTon[0];
                throw new NghiepVuException("Mặt hàng \"" + (h.TenDayDu ?? h.TenHang) + "\" chỉ còn " + h.TonHienTai + ", không đủ để bán " + h.SoLuongCan + ".");
            }
            if (kq.HangVuotLo.Count > 0)
            {
                HangVuotLo h = kq.HangVuotLo[0];
                throw new NghiepVuException(LoiVuotLo(h.TenDayDu ?? h.TenHang, h.ConLaiLo, h.TonHienTai), "MaHang");
            }
            if (kq.HangDoiGia.Count > 0)
                throw new NghiepVuException("Đơn giá bán của mặt hàng \"" + kq.HangDoiGia[0].TenDayDu +
                                            "\" đã thay đổi. Hãy tải lại danh sách hàng hoá trước khi lưu hoá đơn.");
            throw new NghiepVuException("Không thể lưu hoá đơn \"" + so + "\".");
        }

        protected override KetQuaGhiHoaDon XoaDb(string so) { return dal.Xoa(so); }

        protected override void BaoLoiXoa(KetQuaGhiHoaDon kq, string so)
        {
            throw new NghiepVuException("Không thể xoá hoá đơn \"" + so + "\".");
        }
    }
}





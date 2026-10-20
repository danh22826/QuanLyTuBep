using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class NhapHangBll : HoaDonBll
    {
        private readonly HoaDonNhapDal dal = new HoaDonNhapDal();
        private readonly NhaCungCapDal nccDal = new NhaCungCapDal();
        private readonly HangHoaDal hangDal = new HangHoaDal();

        public override LoaiHoaDon Loai { get { return LoaiHoaDon.Nhap; } }
        public override bool ChoSuaDonGia { get { return true; } }
        public override decimal LayDonGia(HangHoa hang) { return hang.GiaNhap; }
        public override string SinhMa() { return dal.SinhSo(); }

        public override IList<IHoaDon> DanhSach()
        {
            return dal.LayTatCa().Cast<IHoaDon>().ToList();
        }

        public override IList<LuaChon> DanhSachDoiTac()
        {
            return nccDal.LayTatCa().Select(n => new LuaChon(n.MaNCC, n.TenHienThi)).ToList();
        }

        protected override bool TonTai(string so) { return dal.TonTai(so); }

        protected override IHoaDon LayDau(string so, List<IChiTietHoaDon> chiTiet)
        {
            HoaDonNhap hd = dal.TimTheoSo(so);
            if (hd != null) chiTiet.AddRange(hd.ChiTiet.Cast<IChiTietHoaDon>());
            return hd;
        }

        protected override IChiTietHoaDon TaoDong(string so, HangHoa hang, int soLuong, decimal donGia, double giamGia, decimal thanhTien)
        {
            return new ChiTietHoaDonNhap
            {
                SoHDN = so, MaHang = hang.MaHang, TenHang = hang.TenHang,
                TenKichThuoc = hang.TenKichThuoc, TenChatLieu = hang.TenChatLieu,
                TenMau = hang.TenMau, TenNuocSX = hang.TenNuocSX, SoLuong = soLuong,
                DonGia = donGia, GiamGia = giamGia, ThanhTien = thanhTien
            };
        }

        protected override string KiemTraKhiThemDong(HangHoa hang, int soLuong, decimal donGia)
        {
            if (donGia <= 0) return "Đơn giá nhập phải lớn hơn 0.";
            return null;
        }

        protected override void KiemTraKhiLuu(IChiTietHoaDon d)
        {
            if (d.DonGia <= 0)
                throw new NghiepVuException("Đơn giá nhập của mặt hàng \"" + d.MaHang + "\" phải lớn hơn 0.");
            if (!hangDal.TonTai(d.MaHang))
                throw new NghiepVuException("Mặt hàng \"" + d.MaHang + "\" không tồn tại.");
        }

        protected override KetQuaGhiHoaDon Ghi(string so, DateTime ngay, string maNV, string maNCC, string tenNCC, string diaChiNCC, string dienThoaiNCC, IList<IChiTietHoaDon> dong)
        {
            HoaDonNhap hd = new HoaDonNhap { SoHDN = so, NgayNhap = ngay, MaNV = maNV, MaNCC = maNCC };
            hd.ChiTiet.AddRange(dong.Cast<ChiTietHoaDonNhap>());
            return dal.Luu(hd);
        }

        protected override void BaoLoiGhi(KetQuaGhiHoaDon kq, string so)
        {
            if (kq.HangNgungKinhDoanh.Count > 0)
                throw new NghiepVuException("Không thể nhập mặt hàng \"" + kq.HangNgungKinhDoanh[0].TenDayDu + "\" vì mặt hàng đã ngừng kinh doanh.");
            throw new NghiepVuException("Không thể lưu hoá đơn \"" + so + "\".");
        }

        protected override KetQuaGhiHoaDon XoaDb(string so) { return dal.Xoa(so); }

        protected override void BaoLoiXoa(KetQuaGhiHoaDon kq, string so)
        {
            if (kq.HangThieuTon.Count > 0)
                throw new NghiepVuException(DanhSachThieu(kq.HangThieuTon,
                    "Không thể xoá hoá đơn nhập này vì một số mặt hàng đã được bán, tồn kho không đủ để trừ lại:",
                    "- {0}: cần trừ {1}, tồn hiện tại {2}"));
            throw new NghiepVuException("Không thể xoá hoá đơn \"" + so + "\".");
        }
    }
}


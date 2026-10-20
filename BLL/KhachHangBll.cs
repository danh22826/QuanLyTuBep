using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class KhachHangBll : BangBll<KhachHang>
    {
        private readonly KhachHangDal dal = new KhachHangDal();

        protected override BangDal<KhachHang> Dal { get { return dal; } }
        public override string TenDoiTuong { get { return "khách hàng"; } }
        public override string TruongKhoa { get { return "MaKhach"; } }
        public override string SinhMaMoi() { return dal.SinhMaTuDong("KH"); }

        public override List<KhachHang> LayDanhSach()
        {
            return dal.LayTatCa().Where(k => !string.Equals(k.MaKhach, KhachHang.MaKhachLe, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public IList<LuaChon> DanhSachChonBanHang()
        {
            return dal.LayDanhSachBanHang().Select(k => new LuaChon(k.MaKhach, k.TenHienThi)).ToList();
        }

        public KhachHang TaoKhachMoi(KhachHang x)
        {
            if (x == null) throw new NghiepVuException("Vui lòng nhập thông tin khách hàng.");
            x.MaKhach = SinhMaMoi();
            Them(x);
            return x;
        }

        protected override void KiemTra(KhachHang x, bool themMoi)
        {
            if (string.Equals(x.MaKhach, KhachHang.MaKhachLe, StringComparison.OrdinalIgnoreCase))
                throw new NghiepVuException(themMoi
                    ? "Mã KL00 dành riêng cho khách lẻ hệ thống, không thể thêm khách này."
                    : "Khách lẻ là khách hệ thống, không thể sửa hoặc xoá.", "MaKhach");
            KiemTraDuLieu.Ma(x.MaKhach, "Mã khách hàng", "MaKhach");
            KiemTraDuLieu.Chu(x.TenKhach, "Tên khách hàng", 100, true, "TenKhach");
            KiemTraDuLieu.DienThoai(x.DienThoai, "DienThoai");
            KiemTraDuLieu.Chu(x.DiaChi, "Địa chỉ", 200, false, "DiaChi");
        }

        protected override void KiemTraTruocKhiXoa(string ma)
        {
            if (string.Equals(ma, KhachHang.MaKhachLe, StringComparison.OrdinalIgnoreCase))
                throw new NghiepVuException("Khách lẻ là khách hệ thống, không thể sửa hoặc xoá.");
            CamNeuCon(dal.DemHoaDonBan(ma), "hoá đơn bán", ma);
        }
    }
}

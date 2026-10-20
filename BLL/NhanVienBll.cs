using System;
using System.Collections.Generic;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class NhanVienBll : BangBll<NhanVien>
    {
        private readonly NhanVienDal dal = new NhanVienDal();
        private readonly CongViecDal congViecDal = new CongViecDal();

        protected override BangDal<NhanVien> Dal { get { return dal; } }
        public override string TenDoiTuong { get { return "nhân viên"; } }
        public override string TruongKhoa { get { return "MaNV"; } }
        public override string SinhMaMoi() { return dal.SinhMaTuDong("NV"); }

        protected override void KiemTra(NhanVien x, bool themMoi)
        {
            KiemTraDuLieu.Ma(x.MaNV, "Mã nhân viên", "MaNV");
            KiemTraDuLieu.Chu(x.TenNV, "Tên nhân viên", 100, true, "TenNV");
            if (x.GioiTinh != null && x.GioiTinh != "Nam" && x.GioiTinh != "Nữ")
                throw new NghiepVuException("Giới tính chỉ là Nam hoặc Nữ.", "GioiTinh");
            if (x.NgaySinh.HasValue && x.NgaySinh.Value.Date > DateTime.Today)
                throw new NghiepVuException("Ngày sinh không được ở tương lai.", "NgaySinh");
            KiemTraDuLieu.DienThoai(x.DienThoai, "DienThoai");
            KiemTraDuLieu.Chu(x.DiaChi, "Địa chỉ", 200, false, "DiaChi");
            if (x.MaCV != null && !congViecDal.TonTai(x.MaCV))
                throw new NghiepVuException("Công việc đã chọn không còn tồn tại.", "MaCV");
        }

        protected override void KiemTraTruocKhiXoa(string ma)
        {
            CamNeuCon(dal.DemHoaDonBan(ma), "hoá đơn bán", ma);
            CamNeuCon(dal.DemHoaDonNhap(ma), "hoá đơn nhập", ma);
        }

        public override IList<LuaChon> LayLuaChon(string truong)
        {
            List<LuaChon> ds = new List<LuaChon>();
            if (truong == "GioiTinh")
            {
                ds.Add(new LuaChon("Nam", "Nam"));
                ds.Add(new LuaChon("Nữ", "Nữ"));
            }
            else if (truong == "MaCV")
            {
                foreach (CongViec c in congViecDal.LayTatCa()) ds.Add(new LuaChon(c.MaCV, c.TenCV));
            }
            return ds;
        }
    }
}


using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class NhaCungCapBll : BangBll<NhaCungCap>
    {
        private readonly NhaCungCapDal dal = new NhaCungCapDal();

        protected override BangDal<NhaCungCap> Dal { get { return dal; } }
        public override string TenDoiTuong { get { return "nhà cung cấp"; } }
        public override string TruongKhoa { get { return "MaNCC"; } }
        public override string SinhMaMoi() { return dal.SinhMaTuDong("NCC"); }

        protected override void KiemTra(NhaCungCap x, bool themMoi)
        {
            KiemTraDuLieu.Ma(x.MaNCC, "Mã nhà cung cấp", "MaNCC");
            KiemTraDuLieu.Chu(x.TenNCC, "Tên nhà cung cấp", 150, true, "TenNCC");
            KiemTraDuLieu.DienThoai(x.DienThoai, "DienThoai");
            KiemTraDuLieu.Chu(x.DiaChi, "Địa chỉ", 200, false, "DiaChi");
        }

        protected override void KiemTraTruocKhiXoa(string ma)
        {
            CamNeuCon(dal.DemHoaDonNhap(ma), "hoá đơn nhập", ma);
        }
    }
}


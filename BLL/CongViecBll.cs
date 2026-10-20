using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class CongViecBll : BangBll<CongViec>
    {
        private readonly CongViecDal dal = new CongViecDal();

        protected override BangDal<CongViec> Dal { get { return dal; } }
        public override string TenDoiTuong { get { return "công việc"; } }
        public override string TruongKhoa { get { return "MaCV"; } }
        public override string SinhMaMoi() { return dal.SinhMaTuDong("CV"); }

        protected override void KiemTra(CongViec x, bool themMoi)
        {
            KiemTraDuLieu.Ma(x.MaCV, "Mã công việc", "MaCV");
            KiemTraDuLieu.Chu(x.TenCV, "Tên công việc", 100, true, "TenCV");
            if (x.MucLuong < 0) throw new NghiepVuException("Mức lương không được âm.", "MucLuong");
        }

        protected override void KiemTraTruocKhiXoa(string ma)
        {
            CamNeuCon(dal.DemNhanVien(ma), "nhân viên", ma);
        }
    }
}


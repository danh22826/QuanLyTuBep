using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public abstract class HoaDonBll
    {
        private static readonly Regex MauSo = new Regex("^[A-Za-z0-9_]+$");
        private readonly List<IChiTietHoaDon> dong = new List<IChiTietHoaDon>();
        private readonly NhanVienDal nhanVienDal = new NhanVienDal();
        private readonly HangHoaDal hangHoaDal = new HangHoaDal();

        // ---------- Phần khác nhau giữa bán và nhập ----------

        public abstract LoaiHoaDon Loai { get; }
        public abstract bool ChoSuaDonGia { get; }
        public abstract decimal LayDonGia(HangHoa hang);
        public abstract IList<IHoaDon> DanhSach();
        public abstract IList<LuaChon> DanhSachDoiTac();
        public abstract string SinhMa();
        protected abstract IHoaDon LayDau(string so, List<IChiTietHoaDon> chiTiet);
        protected abstract bool TonTai(string so);
        protected abstract IChiTietHoaDon TaoDong(string so, HangHoa hang, int soLuong, decimal donGia, double giamGia, decimal thanhTien);
        protected abstract string KiemTraKhiThemDong(HangHoa hang, int soLuong, decimal donGia);
        protected abstract void KiemTraKhiLuu(IChiTietHoaDon d);
        protected abstract KetQuaGhiHoaDon Ghi(string so, DateTime ngay, string maNV, string maDoiTac, string tenDoiTac, string diaChiDoiTac, string dienThoaiDoiTac, IList<IChiTietHoaDon> dong);
        protected abstract void BaoLoiGhi(KetQuaGhiHoaDon kq, string so);
        protected abstract KetQuaGhiHoaDon XoaDb(string so);
        protected abstract void BaoLoiXoa(KetQuaGhiHoaDon kq, string so);

        // ---------- Dòng đang soạn / đang xem ----------

        public IList<IChiTietHoaDon> Dong { get { return dong; } }
        public decimal TongTien { get { return dong.Sum(d => d.ThanhTien); } }

        public void BoDong()
        {
            dong.Clear();
        }

        public bool XoaDong(IChiTietHoaDon d)
        {
            return d != null && dong.Remove(d);
        }

        public string ThemDong(string so, HangHoa hang, int soLuong, decimal donGia, double giamGia)
        {
            if (hang == null) return "Vui lòng chọn mặt hàng.";
            if (dong.Any(d => string.Equals(d.MaHang, hang.MaHang, StringComparison.OrdinalIgnoreCase)))
                return "Mặt hàng này đã có trong hoá đơn. Hãy xoá dòng cũ trước nếu muốn đổi số lượng.";
            if (soLuong <= 0) return "Số lượng phải lớn hơn 0.";
            if (giamGia < 0 || giamGia > 100) return "Giảm giá phải từ 0 đến 100%.";

            string loi = KiemTraKhiThemDong(hang, soLuong, donGia);
            if (loi != null) return loi;

            dong.Add(TaoDong(so, hang, soLuong, donGia, giamGia, TinhThanhTien(soLuong, donGia, giamGia)));
            return null;
        }

        public static decimal TinhThanhTien(int soLuong, decimal donGia, double giamGia)
        {
            return Math.Round(soLuong * donGia * (1m - (decimal)giamGia / 100m), 0, MidpointRounding.AwayFromZero);
        }

        // ---------- Dữ liệu nền cho các ô chọn ----------

        public IList<LuaChon> DanhSachNhanVien()
        {
            return nhanVienDal.LayTatCa().Select(n => new LuaChon(n.MaNV, n.TenHienThi)).ToList();
        }

        public IList<HangHoa> DanhSachHangHoa()
        {
            return hangHoaDal.LayDangKinhDoanh();
        }

        // ---------- Xem / lưu / xoá ----------

        public IHoaDon Xem(string so)
        {
            List<IChiTietHoaDon> ct = new List<IChiTietHoaDon>();
            IHoaDon hd = LayDau(so, ct);
            dong.Clear();
            if (hd != null) dong.AddRange(ct);
            return hd;
        }

        public void Luu(string so, DateTime ngay, string maNV, string maDoiTac, string tenDoiTac = null, string diaChiDoiTac = null, string dienThoaiDoiTac = null)
        {
            so = (so ?? "").Trim();
            if (so.Length == 0) throw new NghiepVuException("Chưa có số hoá đơn.", "So");
            if (so.Length > 20 || !MauSo.IsMatch(so))
                throw new NghiepVuException("Số hoá đơn chỉ gồm chữ không dấu, số, gạch dưới và tối đa 20 ký tự.", "So");
            if (string.IsNullOrWhiteSpace(maNV)) throw new NghiepVuException("Vui lòng chọn nhân viên lập hoá đơn.", "MaNV");
            if (string.IsNullOrWhiteSpace(maDoiTac) && Loai == LoaiHoaDon.Nhap)
                throw new NghiepVuException("Vui lòng chọn nhà cung cấp.", "MaDoiTac");
            if (tenDoiTac != null && tenDoiTac.Trim().Length > 100)
                throw new NghiepVuException("Tên khách hàng không được vượt quá 100 ký tự.", "TenKhach");
            if (Loai == LoaiHoaDon.Ban && diaChiDoiTac != null && diaChiDoiTac.Trim().Length > 200)
                throw new NghiepVuException("Địa chỉ khách hàng không được vượt quá 200 ký tự.", "DiaChiKhach");
            if (Loai == LoaiHoaDon.Ban && !string.IsNullOrWhiteSpace(dienThoaiDoiTac))
                KiemTraDuLieu.DienThoai(dienThoaiDoiTac.Trim(), "DienThoaiKhach");
            if (dong.Count == 0)
                throw new NghiepVuException("Hoá đơn chưa có mặt hàng nào. Hãy thêm ít nhất một dòng.");
            if (TonTai(so))
                throw new NghiepVuException("Số hoá đơn \"" + so + "\" đã tồn tại. Bấm \"Hoá đơn mới\" để lấy số mới.", "So");

            HashSet<string> daCo = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (IChiTietHoaDon d in dong)
            {
                if (string.IsNullOrWhiteSpace(d.MaHang)) throw new NghiepVuException("Có dòng hoá đơn chưa chọn mặt hàng.");
                if (!daCo.Add(d.MaHang))
                    throw new NghiepVuException("Mặt hàng \"" + d.MaHang + "\" xuất hiện nhiều lần trong hoá đơn.");
                if (d.SoLuong <= 0)
                    throw new NghiepVuException("Số lượng của mặt hàng \"" + d.MaHang + "\" phải lớn hơn 0.");
                if (d.GiamGia < 0 || d.GiamGia > 100)
                    throw new NghiepVuException("Giảm giá của mặt hàng \"" + d.MaHang + "\" phải từ 0 đến 100%.");
                KiemTraKhiLuu(d);
            }

            // DAL khoá và kiểm tra lại tồn kho trong cùng transaction để chống bán đồng thời vượt tồn.
            KetQuaGhiHoaDon kq = Ghi(so, ngay.Date, maNV, maDoiTac, tenDoiTac, diaChiDoiTac, dienThoaiDoiTac, dong);
            if (!kq.DaGhi) BaoLoiGhi(kq, so);
        }

        public void Xoa(string so)
        {
            if (string.IsNullOrWhiteSpace(so)) throw new NghiepVuException("Chưa có số hoá đơn cần xoá.");
            KetQuaGhiHoaDon kq = XoaDb(so);
            if (!kq.HoaDonTonTai) throw new NghiepVuException("Hoá đơn \"" + so + "\" không còn tồn tại.");
            if (!kq.DaGhi) BaoLoiXoa(kq, so);
        }

        protected static string DanhSachThieu(IEnumerable<HangThieuTon> ds, string dongTieuDe, string dongMau)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(dongTieuDe);
            foreach (HangThieuTon r in ds)
                sb.AppendLine(string.Format(dongMau, r.TenDayDu ?? r.TenHang, r.SoLuongCan, r.TonHienTai));
            return sb.ToString();
        }
    }
}







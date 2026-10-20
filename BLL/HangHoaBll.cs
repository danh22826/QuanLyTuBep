using System;
using System.Collections.Generic;
using QuanLyBanTuBep.DAL;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.BLL
{
    public class HangHoaBll : BangBll<HangHoa>
    {
        private readonly HangHoaDal dal = new HangHoaDal();
        private readonly KichThuocDal kichThuocDal = new KichThuocDal();
        private readonly ChatLieuDal chatLieuDal = new ChatLieuDal();
        private readonly MauSacDal mauSacDal = new MauSacDal();
        private readonly NuocSanXuatDal nuocSanXuatDal = new NuocSanXuatDal();

        protected override BangDal<HangHoa> Dal { get { return dal; } }
        public override string TenDoiTuong { get { return "hàng hoá"; } }
        public override string TruongKhoa { get { return "MaHang"; } }
        public override string SinhMaMoi() { return dal.SinhMaTuDong(); }

        protected override void KiemTra(HangHoa x, bool themMoi)
        {
            KiemTraDuLieu.Ma(x.MaHang, "Mã hàng", "MaHang");
            KiemTraDuLieu.Chu(x.TenHang, "Tên hàng hoá", 150, true, "TenHang");
            KiemTraDuLieu.Chon(x.MaKichThuoc, "Kích thước", "MaKichThuoc");
            KiemTraDuLieu.Chon(x.MaChatLieu, "Chất liệu", "MaChatLieu");
            KiemTraDuLieu.Chon(x.MaMau, "Màu sắc", "MaMau");
            KiemTraDuLieu.Chon(x.MaNuocSX, "Nước sản xuất", "MaNuocSX");


            KiemTraDuLieu.Chu(x.ThoiGianBaoHanh, "Thời gian bảo hành", 50, false, "ThoiGianBaoHanh");
            KiemTraDuLieu.Chu(x.GhiChu, "Ghi chú", 500, false, "GhiChu");
        }

        protected override void KiemTraTruocKhiXoa(string ma)
        {
            CamNeuCon(dal.DemChiTietBan(ma), "hoá đơn bán", ma);
            CamNeuCon(dal.DemChiTietNhap(ma), "hoá đơn nhập", ma);
        }

        public override string Them(HangHoa x)
        {
            ChuanBiThem(x);
            DatTenAnh(x);
            base.Them(x);
            string canhBao = ChepAnh(x);
            string thongBao = "Đã thêm mặt hàng. Hãy lập hoá đơn nhập để có tồn kho và giá bán.";
            return string.IsNullOrWhiteSpace(canhBao) ? thongBao : thongBao + Environment.NewLine + canhBao;
        }

        public override string Sua(HangHoa x)
        {
            HangHoa cu = LayTheoMa(x.MaHang);
            if (cu == null)
                throw new NghiepVuException("Không tìm thấy bản ghi (có thể đã bị xoá).");

            string truongBiDoi = ThuocTinhBiDoi(cu, x);
            if (truongBiDoi != null)
                throw new NghiepVuException(
                    "Không thể đổi kích thước, chất liệu, màu sắc hoặc nước sản xuất của mặt hàng đã tạo. Hãy tạo mặt hàng mới cho biến thể khác.",
                    truongBiDoi);

            x.SoLuong = cu.SoLuong;
            x.GiaNhap = cu.GiaNhap;
            x.NgungKinhDoanh = cu.NgungKinhDoanh;
            DatTenAnh(x);
            base.Sua(x);
            return ChepAnh(x);
        }

        internal static void ChuanBiThem(HangHoa x)
        {
            x.SoLuong = 0;
            x.GiaNhap = 0;
            x.DonGiaBan = 0;
            x.NgungKinhDoanh = false;
        }

        public void DoiTrangThaiKinhDoanh(string ma, bool ngung)
        {
            HangHoa hang = LayTheoMa(ma);
            if (hang == null) throw new NghiepVuException("Không tìm thấy mặt hàng cần cập nhật.");
            if (dal.DatTrangThai(ma, ngung) == 0)
                throw new NghiepVuException("Mặt hàng không còn tồn tại.");
        }

        internal static string ThuocTinhBiDoi(HangHoa cu, HangHoa moi)
        {
            if (cu == null || moi == null) return null;
            if (!string.Equals(cu.MaKichThuoc, moi.MaKichThuoc, StringComparison.Ordinal)) return "MaKichThuoc";
            if (!string.Equals(cu.MaChatLieu, moi.MaChatLieu, StringComparison.Ordinal)) return "MaChatLieu";
            if (!string.Equals(cu.MaMau, moi.MaMau, StringComparison.Ordinal)) return "MaMau";
            if (!string.Equals(cu.MaNuocSX, moi.MaNuocSX, StringComparison.Ordinal)) return "MaNuocSX";
            return null;
        }

        // Nếu người dùng chọn ảnh mới thì tên ảnh lưu trong CSDL = mã hàng + đuôi file; file chỉ được chép sau khi lưu CSDL thành công.
        private static void DatTenAnh(HangHoa x)
        {
            if (!string.IsNullOrEmpty(x.AnhNguon) && !string.IsNullOrEmpty(x.MaHang))
                x.Anh = ThuMucAnh.TenDich(x.AnhNguon, x.MaHang.Trim());
        }

        private static string ChepAnh(HangHoa x)
        {
            if (string.IsNullOrEmpty(x.AnhNguon)) return null;
            try
            {
                ThuMucAnh.Chep(x.AnhNguon, x.Anh);
                return null;
            }
            catch (Exception ex)
            {
                return "Đã lưu thông tin nhưng không chép được ảnh vào thư mục Images:\n" + ex.Message;
            }
        }

        public override IList<LuaChon> LayLuaChon(string truong)
        {
            List<LuaChon> ds = new List<LuaChon>();
            switch (truong)
            {
                case "MaKichThuoc":
                    foreach (KichThuoc k in kichThuocDal.LayTatCa()) ds.Add(new LuaChon(k.MaKichThuoc, k.TenKichThuoc));
                    break;
                case "MaChatLieu":
                    foreach (ChatLieu c in chatLieuDal.LayTatCa()) ds.Add(new LuaChon(c.MaChatLieu, c.TenChatLieu));
                    break;
                case "MaMau":
                    foreach (MauSac m in mauSacDal.LayTatCa()) ds.Add(new LuaChon(m.MaMau, m.TenMau));
                    break;
                case "MaNuocSX":
                    foreach (NuocSanXuat n in nuocSanXuatDal.LayTatCa()) ds.Add(new LuaChon(n.MaNuocSX, n.TenNuocSX));
                    break;
            }
            return ds;
        }
    }
}







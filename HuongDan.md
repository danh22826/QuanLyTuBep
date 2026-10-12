# Quản lý bán tủ bếp (WinForms + SQL Server)

## Chạy project
1. Mở SSMS, chạy file `SQL/QuanLyBanTuBep.sql` (tạo database `QuanLyBanTuBep` + dữ liệu mẫu).
   - Cẩn thận: script này **xoá và tạo lại toàn bộ bảng**, chạy lại sẽ mất dữ liệu bạn đã nhập.
2. Mở `QuanLyBanTuBep.csproj` bằng Visual Studio 2019 / 2022 (workload ".NET desktop development").
3. Mở `App.config`, sửa `Data Source` cho đúng tên server hiện ở ô "Server name" khi đăng nhập SSMS:
   - `.` hoặc `.\SQLEXPRESS` hoặc `(localdb)\MSSQLLocalDB`
4. Bấm F5.

## Cấu trúc
- `DAL/`   truy cập CSDL (`Db.cs`: kết nối, tham số hoá SQL, transaction; `HoaDonDal.cs`)
- `BLL/`   nghiệp vụ hoá đơn (`HoaDonBll.cs`: kiểm tra hợp lệ, chặn bán quá tồn kho)
- `Models/` lớp dữ liệu hoá đơn
- `UI/`    giao diện, chia thư mục con theo chức năng (`ManHinhChinh/`, `QuanLy/`, `DanhMuc/`, `GiaoDich/`, `TraCuu/`, `BaoCao/`; mã dùng chung ở `Chung/`: Theme, TienIch, controller, cấu hình). Namespace vẫn là `QuanLyBanTuBep.UI`. Giao diện nhiều Form: mỗi chức năng là một Form riêng, có file `.Designer.cs` riêng
  (mở file `.cs` của form bằng Visual Studio, bấm View Designer để kéo thả / chỉnh từng ô):
  - `FormMain`: form chính gồm MenuStrip (trên cùng), thanh công cụ nhanh (ToolStrip), tiêu đề teal, vùng nội dung và
    thanh trạng thái (phím tắt + giờ). Chọn mục menu thì form của chức năng đó được nhúng vào vùng nội dung thay cho
    trang đang mở (không bật cửa sổ mới); mục menu và menu cha của trang đang mở được đánh dấu. Trang mới luôn nạp
    dữ liệu mới từ CSDL. Màu menu nằm ở `Theme.MenuRenderer`. Phím tắt: Ctrl+B Bán hàng, Ctrl+N Nhập hàng, Ctrl+H Hàng hoá.
  - Menu Danh mục: `FrmHangHoa`, `FrmNhanVien`, `FrmKhachHang`, `FrmNhaCungCap`, rồi `FrmKichThuoc`, `FrmChatLieu`, `FrmMauSac`, `FrmNuocSanXuat`, `FrmCongViec`
  - Menu Giao dịch: `FrmNhapHang`, `FrmBanHang` (cùng khung `FrmHoaDon`)
  - Menu Tra cứu > Tìm kiếm: `FrmTimSanPham`, `FrmTimHoaDonBan`
  - Menu Tra cứu > Báo cáo: `FrmTop3SanPham`, `FrmHoaDonNhapTheoNV`, `FrmTop5HoaDonBan`, `FrmKhachHangTheoThang`
  - Nghiệp vụ không nằm trong form: `BangController` (Thêm / Sửa / Xoá / Tìm / xem chi tiết + ảnh) và `ThuTucController`
    (gọi stored procedure, xuất CSV) gắn vào control của form theo quy ước tên (`txtMaHang`, `cboMaMau`, `nudGiaNhap`, `dtpNgaySinh`...).
    Thêm / bớt ô trên Designer thì giữ đúng tên theo quy ước này. Cấu hình bảng nằm ở `CauHinhBang.cs`, `CauHinhThuTuc.cs`.
  - `Theme.cs`: bảng màu, font, khoảng cách dùng chung
- `Images/` ảnh sản phẩm (CSDL chỉ lưu tên file ở cột `Anh`). Có sẵn 5 ảnh minh hoạ `tu_bep_01..05.jpg` khớp dữ liệu mẫu.
  Khi thêm / sửa hàng hoá, bấm "Chọn..." để chọn ảnh: ảnh được chép vào `Images/` và đặt tên theo mã hàng (vd `TB06.jpg`).
  Bấm vào một dòng hàng hoá để xem chi tiết và ảnh.
- `SQL/`   script CSDL

## Việc do trigger của CSDL làm (app KHÔNG tự tính lại)
- Thành tiền, tổng tiền hoá đơn
- Tồn kho (cộng khi nhập, trừ khi bán, hoàn lại khi xoá hoá đơn)
- Giá nhập mới nhất, giá bán = 110% giá nhập

## Việc app tự chặn (vì CSDL không chặn)
- Bán quá số lượng tồn kho (CSDL không có CHECK tồn >= 0)
- Xoá danh mục / hàng hoá / nhân viên / khách hàng / nhà cung cấp đang được dùng
  (CSDL dùng ON DELETE SET NULL / CASCADE nên sẽ không báo lỗi, mà làm mất thông tin hoặc xoá lây chi tiết hoá đơn)
- Xoá hoá đơn nhập khi hàng đã bán bớt (sẽ làm tồn kho âm)

## Hiển thị dữ liệu bằng ListView

Mọi danh sách dữ liệu (hàng hoá, hoá đơn, báo cáo, trang chủ...) dùng **ListView** (View.Details), tên control bắt đầu bằng `lv` (`lv`, `lvDs`, `lvDong`, `lvSapHet`, `lvGanDay`).
- Thuộc tính cơ bản đặt trong Designer; `TienIch.GanLuoi(lv)` bật owner-draw (tiêu đề teal, dòng xen kẽ, dòng chọn).
- `TienIch.NapLuoi(lv, DataTable/DataView)` đổ dữ liệu: số căn phải, ngày dd/MM/yyyy, tự chia độ rộng cột.
- Giá trị gốc của dòng nằm ở `item.Tag` (DataRowView); xuất CSV dùng `TienIch.XuatCsv(lv, ...)`.
- Lưu ý: cột đầu tiên của ListView luôn căn trái (giới hạn của control).

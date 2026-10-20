# Quản lý bán tủ bếp — kiến trúc 3 tầng

WinForms .NET Framework 4.7.2, SQL Server. Giữ nguyên các Form và file Designer; namespace vẫn là `QuanLyBanTuBep`, assembly/project là `QuanLyBanHang2`.

## Ba tầng và Model

```
UI (giao diện)  →  BLL (nghiệp vụ)  →  DAL (truy vấn dữ liệu)  →  Db (ADO.NET)  →  SQL Server
                      ↑ Models: mỗi bảng một lớp, dùng chung cho cả ba tầng ↑
```

| Tầng | Thư mục | Trách nhiệm |
|---|---|---|
| UI | `UI/` | Hiện dữ liệu, nhận thao tác, hiện thông báo. Chỉ gọi BLL, không biết bảng/cột/SQL, không chứa quy tắc kiểm tra. |
| BLL | `BLL/` | Quy tắc nghiệp vụ và nội dung thông báo: bắt buộc, độ dài, định dạng, chặn trùng mã, chặn xoá khi còn tham chiếu, tính tiền xem trước, quy tắc hoá đơn. Lỗi ném `NghiepVuException` (kèm tên thuộc tính để UI đưa con trỏ về đúng ô). |
| DAL | `DAL/` | Câu lệnh SQL, ánh xạ dòng → model, transaction và khoá khi ghi hoá đơn. Chỉ trả dữ liệu / kết quả (`KetQuaGhiHoaDon`), không tạo thông báo. |
| Db | `DAL/Db.cs` | Kết nối, parameter, transaction ADO.NET. |
| Models | `Models/` | Mỗi bảng một lớp (`KichThuoc`, `ChatLieu`, `MauSac`, `NuocSanXuat`, `CongViec`, `NhanVien`, `KhachHang`, `NhaCungCap`, `HangHoa`, `HoaDonBan`/`ChiTietHoaDonBan`, `HoaDonNhap`/`ChiTietHoaDonNhap`) cùng vài lớp kết quả báo cáo. `[DisplayName]` là tiêu đề cột lưới. |

### Thêm một bảng danh mục mới
1. Model (một lớp, tên thuộc tính = tên cột).
2. Dùng file SQL duy nhất `SQL/QuanLyBanTuBep_TongHop.sql` trong SSMS. Chỉ chạy phần `FRESH` cho database mới; hoặc lần lượt các phần nâng cấp phù hợp cho database cũ. Không chạy toàn bộ file cùng lúc. SQL Server cần hỗ trợ `CREATE OR ALTER` (SQL Server 2016 SP1 trở lên).
3. `XxxBll : BangBll<Xxx>` — `KiemTra`, `KiemTraTruocKhiXoa`.
4. Form: `new BangView<Xxx>(this, new XxxBll(), CauHinh.Xxx())` và thêm `CauHinh.Xxx()` (tên thuộc tính + nhãn).

### Hoá đơn
`HoaDonBll` (lớp nền) giữ quy tắc chung; `BanHangBll` và `NhapHangBll` chỉ khai báo phần khác nhau. Một `FrmHoaDon` dùng chung cho bán và nhập. Tồn và giá được trigger tính từ chứng từ. Mỗi dòng chi tiết nhập là một lô; FIFO xếp lô theo ngày nhập rồi số hóa đơn. Lô đang bán là lô đầu tiên có số nhập lũy kế lớn hơn tổng số đã bán. Giá nhập hiện trên mặt hàng là giá của lô đó; giá bán tự động bằng 110% giá này. Một dòng bán không được vượt số còn lại của lô; nếu khách mua nhiều hơn, bán phần còn lại trước rồi lập hóa đơn mới cho phần lô tiếp theo. Xóa hoặc sửa hóa đơn làm trigger tính lại tồn và lô hiện tại từ chứng từ còn lại. Tồn đầu kỳ được ghi bằng hóa đơn nhập HDN00. Form Hàng hóa chỉ sửa tên, bảo hành, ảnh và ghi chú; mã hàng, kích thước, chất liệu, màu sắc, nước sản xuất được chọn lúc tạo và khóa khi sửa.

### Khách lẻ và khách hàng trên hoá đơn bán
Khách lẻ là bản ghi dùng chung `KL00` trong bảng `KhachHang`; chương trình chọn khách lẻ mặc định và không tạo khách mới theo mỗi hoá đơn. Khách quen được chọn từ danh sách hoặc tạo bằng nút **Khách mới**. Mã khách mới vẫn theo dạng `KH01`, `KH02`, ... và không tính `KL00`. Bản ghi `KL00` bị ẩn khỏi trang quản lý khách hàng, nhưng vẫn xuất hiện trên combo bán hàng và báo cáo. Khi nâng cấp database cũ, dùng khối **UPGRADE H** trong `SQL/QuanLyBanTuBep_TongHop.sql` để chuẩn hóa khách lẻ và hóa đơn cũ.

## Quy ước tên control (UI gắn theo tên)
- Form danh mục: `txt/nud/dtp/cbo` + tên thuộc tính; `lv`, `txtTim`, `lblDem`, `btnThem/Sua/Xoa/LamMoi/Csv`; hàng hoá thêm `picAnh`, `txtAnh`, `btnChonAnh`.
- Form tìm kiếm / báo cáo: `lv`, `lblKetQua`, `btnXem`, `btnCsv` và các ô tham số (`nudNam`, `cboQuy`, `cboThang`, `cboMaChatLieu`, `dtpNgayBan`, `txtTongTienTu`…).

## Chạy
1. Mở `QuanLyBanHang2.csproj` trong Visual Studio, kiểm tra connection string trong `App.config`.
2. **Database mới:** chạy phần `FRESH`, sau đó chạy khối `DU LIEU MAU BO SUNG` ở cuối cùng tệp để nạp thêm dữ liệu cho mọi bảng.
3. **Database cũ:** sao lưu trước. Chạy các phần nâng cấp phù hợp trong `SQL/QuanLyBanTuBep_TongHop.sql` theo thứ tự. Chạy **UPGRADE H** để chuẩn hóa `KL00`; chỉ chạy **UPGRADE I** nếu database từng cài chức năng Phiếu trả hàng, vì khối này xóa bảng/chi tiết phiếu trả. Khối **DU LIEU MAU BO SUNG** ở cuối file nạp thêm dữ liệu mẫu cho các bảng.
4. Build và chạy. Chạy các khối SQL riêng biệt trên SQL Server theo hướng dẫn đầu tệp; không thực thi toàn bộ tệp một lượt.

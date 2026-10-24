# Hướng dẫn chạy phần mềm Quản lý bán tủ bếp

Tài liệu này dành cho người sử dụng không chuyên về lập trình. Làm theo từng bước; nếu phần mềm đã được cài đặt và cơ sở dữ liệu đã có, bắt đầu từ **Mục 2**.

## 1. Chuẩn bị lần đầu

Phần mềm cần có:

- Máy tính Windows.
- SQL Server LocalDB hoặc SQL Server Express đang hoạt động.
- SQL Server Management Studio (SSMS) để tạo cơ sở dữ liệu lần đầu.
- .NET Framework 4.7.2 trở lên.

### Tạo cơ sở dữ liệu mới

Chỉ làm bước này khi chưa có cơ sở dữ liệu `QuanLyBanTuBep`. Phần `FRESH` sẽ xóa và tạo lại các bảng; không chạy phần này trên cơ sở dữ liệu đang có dữ liệu cần giữ.

1. Mở **SQL Server Management Studio (SSMS)**.
2. Kết nối tới máy chủ `(localdb)\MSSQLLocalDB` bằng **Windows Authentication**.
3. Mở tệp `SQL/QuanLyBanTuBep_TongHop.sql`.
4. Trong tệp, tìm dòng `BAT DAU FRESH` và dòng `KET THUC FRESH`.
5. Chỉ chọn phần nằm giữa hai dòng đó, rồi nhấn **Execute** hoặc **F5**.
6. Sau khi chạy xong, tìm khối `BAT DAU DU LIEU MAU BO SUNG` ở cuối tệp. Chỉ chọn khối đó đến dòng `KET THUC DU LIEU MAU BO SUNG`, rồi nhấn **Execute**.

Khối dữ liệu mẫu tạo thêm danh mục, nhân viên, khách hàng, nhà cung cấp, hàng hóa, hóa đơn nhập và hóa đơn bán. Không chạy cả tệp cùng một lúc.

> Nếu cơ sở dữ liệu đã tồn tại, không chạy `FRESH`. Với cơ sở dữ liệu cũ, các phần nâng cấp có thể thay đổi dữ liệu hoặc bỏ chức năng cũ. Hãy sao lưu trước và nhờ người quản trị chọn đúng các phần `UPGRADE` cần thiết. Riêng `UPGRADE I` gỡ Phiếu trả hàng và xóa dữ liệu phiếu trả cũ; chỉ chạy khi chủ động muốn bỏ chức năng đó.

## 2. Mở phần mềm

1. Mở thư mục `bin/Release/net472`.
2. Nhấp đúp `QuanLyBanHang2.exe`.
3. Giữ tệp `QuanLyBanHang2.exe.config` và thư mục `Images` cùng chỗ với tệp `.exe`.

Phần mềm đang được cấu hình kết nối mặc định tới `(localdb)\MSSQLLocalDB`, cơ sở dữ liệu `QuanLyBanTuBep`, dùng tài khoản Windows của người đăng nhập. Nếu cơ sở dữ liệu nằm trên máy chủ khác, người quản trị cần cập nhật `Data Source` trong tệp `.config` cho đúng máy chủ.

## 3. Dùng các chức năng chính

- **Danh mục:** xem và quản lý hàng hóa, nhân viên, khách hàng, nhà cung cấp, kích thước, chất liệu, màu sắc, nước sản xuất và công việc.
- **Nhập hàng:** chọn nhân viên, nhà cung cấp và mặt hàng; nhập số lượng cùng đơn giá rồi lưu hóa đơn. Tồn kho được cập nhật từ hóa đơn nhập.
- **Bán hàng:** chọn nhân viên, khách hàng hoặc khách lẻ, thêm mặt hàng và số lượng rồi lưu hóa đơn. Không thể bán nhiều hơn số lượng còn trong kho hoặc vượt phần còn lại của lô đang bán.
- **Tra cứu và báo cáo:** tìm sản phẩm/hóa đơn và xem các báo cáo theo quý, nhân viên, tháng hoặc khách hàng.

Giá bán được phần mềm tính từ giá nhập theo quy tắc hiện có. Khi có cảnh báo về lượng còn lại của lô, hãy lập hóa đơn cho phần lượng được phép trước; phần tiếp theo cần lập thành hóa đơn khác.

Ứng dụng hiện không có chức năng lập Phiếu trả hàng.

## 4. Nếu phần mềm báo không kết nối được

Kiểm tra lần lượt:

1. Máy chủ SQL Server/LocalDB đã được cài đặt và đang hoạt động.
2. Cơ sở dữ liệu có tên `QuanLyBanTuBep`.
3. Tên máy chủ trong `QuanLyBanHang2.exe.config` đúng với máy chủ đang dùng.
4. Tài khoản Windows hiện tại có quyền truy cập cơ sở dữ liệu.

Nếu không biết máy chủ nào đang được cài, hãy nhờ người quản trị kiểm tra thay vì tự đổi tên kết nối.

## 5. Sao lưu dữ liệu

Hóa đơn và danh mục được lưu trong SQL Server, không chỉ trong thư mục phần mềm. Trước khi nâng cấp cơ sở dữ liệu hoặc thay đổi máy chủ, hãy nhờ người quản trị sao lưu cơ sở dữ liệu `QuanLyBanTuBep`. Không chạy lại phần `FRESH` để xử lý lỗi kết nối vì phần này tạo lại bảng.

## Tệp SQL chính

- `SQL/QuanLyBanTuBep_TongHop.sql` — tạo cơ sở dữ liệu mới, các phần nâng cấp và dữ liệu mẫu bổ sung.


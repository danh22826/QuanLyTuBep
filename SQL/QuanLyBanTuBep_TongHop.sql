-- FILE SQL TONG HOP - QuanLyBanTuBep
-- Khong dung SQLCMD directive; file gom cac phan SQL thuong de Visual Studio phan tich duoc.
-- Chi chon va chay mot giai doan. Khong Execute toan bo file.
-- Database moi: chon phan FRESH cho database moi.
-- Database cu: chay rieng cac UPGRADE A-I can thiet theo thu tu va khoi DU LIEU MAU BO SUNG o cuoi file.

-- >>> BAT DAU FRESH <<<
-- ==========================================================================================
-- BÀI TẬP LỚN: LẬP TRÌNH TRỰC QUAN - NHÓM 5
-- ĐỀ TÀI: QUẢN LÝ BÁN TỦ BẾP
-- File nguồn: 47_Quan_ly_ban_tu_bep.xlsx
-- Hệ quản trị CSDL: Microsoft SQL Server 2016 SP1 trở lên (cần CREATE OR ALTER)
-- ==========================================================================================

-- 1. TẠO CƠ SỞ DỮ LIỆU
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QuanLyBanTuBep')
BEGIN
    CREATE DATABASE QuanLyBanTuBep;
END
GO

USE QuanLyBanTuBep;
GO

-- 2. XÓA CÁC BẢNG CŨ NẾU ĐÃ TỒN TẠI (Đảm bảo chạy lại script không lỗi)
IF OBJECT_ID(N'ChiTietHoaDonBan', N'U') IS NOT NULL DROP TABLE ChiTietHoaDonBan;
IF OBJECT_ID(N'ChiTietHoaDonNhap', N'U') IS NOT NULL DROP TABLE ChiTietHoaDonNhap;
IF OBJECT_ID(N'HoaDonBan', N'U') IS NOT NULL DROP TABLE HoaDonBan;
IF OBJECT_ID(N'HoaDonNhap', N'U') IS NOT NULL DROP TABLE HoaDonNhap;
IF OBJECT_ID(N'DMHangHoa', N'U') IS NOT NULL DROP TABLE DMHangHoa;
IF OBJECT_ID(N'NhanVien', N'U') IS NOT NULL DROP TABLE NhanVien;
IF OBJECT_ID(N'CongViec', N'U') IS NOT NULL DROP TABLE CongViec;
IF OBJECT_ID(N'KhachHang', N'U') IS NOT NULL DROP TABLE KhachHang;
IF OBJECT_ID(N'NhaCungCap', N'U') IS NOT NULL DROP TABLE NhaCungCap;
IF OBJECT_ID(N'KichThuoc', N'U') IS NOT NULL DROP TABLE KichThuoc;
IF OBJECT_ID(N'ChatLieu', N'U') IS NOT NULL DROP TABLE ChatLieu;
IF OBJECT_ID(N'NuocSanXuat', N'U') IS NOT NULL DROP TABLE NuocSanXuat;
IF OBJECT_ID(N'MauSac', N'U') IS NOT NULL DROP TABLE MauSac;
GO

-- ==========================================================================================
-- 3. TẠO CÁC BẢNG DANH MỤC (BẢNG ĐƠN)
-- ==========================================================================================

-- 3.1. Bảng Kích thước (A8-A10)
CREATE TABLE KichThuoc (
    MaKichThuoc NVARCHAR(20) PRIMARY KEY,
    TenKichThuoc NVARCHAR(50) NOT NULL
);
GO

-- 3.2. Bảng Chất liệu (A14-A16)
CREATE TABLE ChatLieu (
    MaChatLieu NVARCHAR(20) PRIMARY KEY,
    TenChatLieu NVARCHAR(50) NOT NULL
);
GO

-- 3.3. Bảng Nước sản xuất (A19-A21)
CREATE TABLE NuocSanXuat (
    MaNuocSX NVARCHAR(20) PRIMARY KEY,
    TenNuocSX NVARCHAR(50) NOT NULL
);
GO

-- 3.4. Bảng Màu sắc (A23-A25)
CREATE TABLE MauSac (
    MaMau NVARCHAR(20) PRIMARY KEY,
    TenMau NVARCHAR(50) NOT NULL
);
GO

-- 3.5. Bảng Công việc (E28-E31)
CREATE TABLE CongViec (
    MaCV NVARCHAR(20) PRIMARY KEY,
    TenCV NVARCHAR(100) NOT NULL,
    MucLuong DECIMAL(18, 0) DEFAULT 0
);
GO

-- 3.6. Bảng Khách hàng (E22-E26)
CREATE TABLE KhachHang (
    MaKhach NVARCHAR(20) PRIMARY KEY,
    TenKhach NVARCHAR(100) NULL,
    DiaChi NVARCHAR(200) NULL,
    DienThoai NVARCHAR(20) NULL
);
GO

IF NOT EXISTS (SELECT 1 FROM KhachHang WHERE MaKhach = N'KL00')
    INSERT INTO KhachHang (MaKhach, TenKhach, DiaChi, DienThoai)
    VALUES (N'KL00', N'Khách lẻ', NULL, NULL);
GO

-- 3.7. Bảng Nhà cung cấp (G23-G27)
CREATE TABLE NhaCungCap (
    MaNCC NVARCHAR(20) PRIMARY KEY,
    TenNCC NVARCHAR(150) NOT NULL,
    DiaChi NVARCHAR(200) NULL,
    DienThoai NVARCHAR(20) NULL
);
GO

-- ==========================================================================================
-- 4. TẠO CÁC BẢNG CHÍNH (CÓ KHÓA NGOẠI)
-- ==========================================================================================

-- 4.1. Bảng Nhân viên (C22-C29)
CREATE TABLE NhanVien (
    MaNV NVARCHAR(20) PRIMARY KEY,
    TenNV NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10) NULL,
    NgaySinh DATE NULL,
    DienThoai NVARCHAR(20) NULL,
    DiaChi NVARCHAR(200) NULL,
    MaCV NVARCHAR(20) NULL,
    CONSTRAINT FK_NhanVien_CongViec FOREIGN KEY (MaCV) REFERENCES CongViec(MaCV)
        ON UPDATE CASCADE ON DELETE SET NULL
);
GO

-- 4.2. Bảng Danh mục hàng hoá (C8-C20)
CREATE TABLE DMHangHoa (
    MaHang NVARCHAR(20) PRIMARY KEY,
    TenHang NVARCHAR(150) NOT NULL,
    MaKichThuoc NVARCHAR(20) NULL,
    MaChatLieu NVARCHAR(20) NULL,
    MaMau NVARCHAR(20) NULL,
    MaNuocSX NVARCHAR(20) NULL,
    SoLuong INT DEFAULT 0,
    GiaNhap DECIMAL(18, 2) DEFAULT 0,
    DonGiaBan DECIMAL(18, 2) DEFAULT 0,
    ThoiGianBaoHanh NVARCHAR(50) NULL,
    Anh NVARCHAR(255) NULL,
    GhiChu NVARCHAR(500) NULL,
    NgungKinhDoanh BIT NOT NULL CONSTRAINT DF_DMHangHoa_NgungKD DEFAULT (0),
    CONSTRAINT FK_DMHangHoa_KichThuoc FOREIGN KEY (MaKichThuoc) REFERENCES KichThuoc(MaKichThuoc)
        ON UPDATE CASCADE ON DELETE SET NULL,
    CONSTRAINT FK_DMHangHoa_ChatLieu FOREIGN KEY (MaChatLieu) REFERENCES ChatLieu(MaChatLieu)
        ON UPDATE CASCADE ON DELETE SET NULL,
    CONSTRAINT FK_DMHangHoa_MauSac FOREIGN KEY (MaMau) REFERENCES MauSac(MaMau)
        ON UPDATE CASCADE ON DELETE SET NULL,
    CONSTRAINT FK_DMHangHoa_NuocSX FOREIGN KEY (MaNuocSX) REFERENCES NuocSanXuat(MaNuocSX)
        ON UPDATE CASCADE ON DELETE SET NULL
);
GO

-- 4.3. Bảng Hóa đơn bán (E8-E13)
CREATE TABLE HoaDonBan (
    SoHDB NVARCHAR(20) PRIMARY KEY,
    MaNV NVARCHAR(20) NULL,
    NgayBan DATE DEFAULT GETDATE(),
    MaKhach NVARCHAR(20) NULL,
    TongTien DECIMAL(18, 2) DEFAULT 0,
    CONSTRAINT FK_HoaDonBan_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
        ON UPDATE CASCADE ON DELETE SET NULL,
    CONSTRAINT FK_HoaDonBan_KhachHang FOREIGN KEY (MaKhach) REFERENCES KhachHang(MaKhach)
        ON UPDATE CASCADE ON DELETE SET NULL
);
GO

-- 4.4. Bảng Chi tiết hoá đơn bán (G8-G13)
CREATE TABLE ChiTietHoaDonBan (
    SoHDB NVARCHAR(20) NOT NULL,
    MaHang NVARCHAR(20) NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    DonGia DECIMAL(18, 2) NULL,  -- Giá bán được chốt tại thời điểm lập hóa đơn
    GiamGia FLOAT DEFAULT 0,      -- % giảm giá (ví dụ: 0, 5, 10)
    ThanhTien DECIMAL(18, 2) DEFAULT 0,
    PRIMARY KEY (SoHDB, MaHang),
    CONSTRAINT FK_ChiTietHDBan_HoaDon FOREIGN KEY (SoHDB) REFERENCES HoaDonBan(SoHDB)
        ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietHDBan_HangHoa FOREIGN KEY (MaHang) REFERENCES DMHangHoa(MaHang)
        ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO

-- 4.5. Bảng Hoá đơn nhập (E15-E20)
CREATE TABLE HoaDonNhap (
    SoHDN NVARCHAR(20) PRIMARY KEY,
    MaNV NVARCHAR(20) NULL,
    NgayNhap DATE DEFAULT GETDATE(),
    MaNCC NVARCHAR(20) NULL,
    TongTien DECIMAL(18, 2) DEFAULT 0,
    CONSTRAINT FK_HoaDonNhap_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
        ON UPDATE CASCADE ON DELETE SET NULL,
    CONSTRAINT FK_HoaDonNhap_NCC FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
        ON UPDATE CASCADE ON DELETE SET NULL
);
GO

-- 4.6. Bảng Chi tiết hoá đơn nhập (G15-G21)
CREATE TABLE ChiTietHoaDonNhap (
    SoHDN NVARCHAR(20) NOT NULL,
    MaHang NVARCHAR(20) NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    DonGia DECIMAL(18, 2) NOT NULL CHECK (DonGia >= 0),
    GiamGia FLOAT DEFAULT 0,      -- % giảm giá (ví dụ: 0, 5, 10)
    ThanhTien DECIMAL(18, 2) DEFAULT 0,
    PRIMARY KEY (SoHDN, MaHang),
    CONSTRAINT FK_ChiTietHDNhap_HoaDon FOREIGN KEY (SoHDN) REFERENCES HoaDonNhap(SoHDN)
        ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietHDNhap_HangHoa FOREIGN KEY (MaHang) REFERENCES DMHangHoa(MaHang)
        ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO

-- ==========================================================================================
-- 5. CÁC TRIGGER ĐÁP ỨNG NGHIỆP VỤ TỰ ĐỘNG CỦA FILE EXCEL (Dòng 33 - 35)
-- ==========================================================================================

-- Yêu cầu 1, 2, 3:
-- 1. Số lượng trong DM hàng hoá được tự động cập nhật khi nhập hàng và bán hàng
-- 2. Giá nhập trong DM hàng hoá là giá của lô FIFO đang bán
-- 3. Giá bán trong DM hàng hoá = 110% giá nhập của lô FIFO đang bán

-- 5.0. Hàm suy ra giá và số lượng còn lại của lô FIFO đang bán.
CREATE OR ALTER FUNCTION dbo.fn_GiaLoDangBan (@MaHang NVARCHAR(20))
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @DaBan INT = (SELECT ISNULL(SUM(SoLuong), 0) FROM dbo.ChiTietHoaDonBan WHERE MaHang = @MaHang);
    DECLARE @Gia DECIMAL(18, 2);

    SELECT TOP (1) @Gia = lo.DonGia
    FROM (
        SELECT ct.DonGia,
               SUM(ct.SoLuong) OVER (ORDER BY hd.NgayNhap, hd.SoHDN ROWS UNBOUNDED PRECEDING) AS LuyKe
        FROM dbo.ChiTietHoaDonNhap ct
        JOIN dbo.HoaDonNhap hd ON hd.SoHDN = ct.SoHDN
        WHERE ct.MaHang = @MaHang
    ) AS lo
    WHERE lo.LuyKe > @DaBan
    ORDER BY lo.LuyKe;

    IF @Gia IS NULL
        SELECT TOP (1) @Gia = ct.DonGia
        FROM dbo.ChiTietHoaDonNhap ct
        JOIN dbo.HoaDonNhap hd ON hd.SoHDN = ct.SoHDN
        WHERE ct.MaHang = @MaHang
        ORDER BY hd.NgayNhap DESC, hd.SoHDN DESC;

    RETURN @Gia;
END;
GO

CREATE OR ALTER FUNCTION dbo.fn_ConLaiLoDangBan (@MaHang NVARCHAR(20))
RETURNS INT
AS
BEGIN
    DECLARE @DaBan INT = (SELECT ISNULL(SUM(SoLuong), 0) FROM dbo.ChiTietHoaDonBan WHERE MaHang = @MaHang);
    DECLARE @ConLai INT;

    SELECT TOP (1) @ConLai = lo.LuyKe - @DaBan
    FROM (
        SELECT SUM(ct.SoLuong) OVER (ORDER BY hd.NgayNhap, hd.SoHDN ROWS UNBOUNDED PRECEDING) AS LuyKe
        FROM dbo.ChiTietHoaDonNhap ct
        JOIN dbo.HoaDonNhap hd ON hd.SoHDN = ct.SoHDN
        WHERE ct.MaHang = @MaHang
    ) AS lo
    WHERE lo.LuyKe > @DaBan
    ORDER BY lo.LuyKe;

    RETURN ISNULL(@ConLai, 0);
END;
GO

-- 5.1. Trigger nhập: cộng tồn và đặt giá theo lô FIFO đang bán.
CREATE OR ALTER TRIGGER trg_ChiTietHoaDonNhap_UpdateHangHoa
ON ChiTietHoaDonNhap
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        UPDATE ct
        SET ct.ThanhTien = ROUND(
            CAST(i.SoLuong AS DECIMAL(18, 4)) * i.DonGia *
            (1 - CAST(ISNULL(i.GiamGia, 0) AS DECIMAL(9, 4)) / 100), 0)
        FROM ChiTietHoaDonNhap ct
        JOIN inserted i ON ct.SoHDN = i.SoHDN AND ct.MaHang = i.MaHang;
    END;

    ;WITH ThongTinCapNhat AS
    (
        SELECT MaHang, SUM(SoLuongChenhLech) AS TongThayDoiSL
        FROM
        (
            SELECT MaHang, SoLuong AS SoLuongChenhLech FROM inserted
            UNION ALL
            SELECT MaHang, -SoLuong AS SoLuongChenhLech FROM deleted
        ) AS Tmp
        GROUP BY MaHang
    )
    UPDATE h
    SET h.SoLuong = ISNULL(h.SoLuong, 0) + t.TongThayDoiSL
    FROM dbo.DMHangHoa h
    JOIN ThongTinCapNhat t ON t.MaHang = h.MaHang;

    UPDATE h
    SET h.GiaNhap = dbo.fn_GiaLoDangBan(h.MaHang),
        h.DonGiaBan = ROUND(dbo.fn_GiaLoDangBan(h.MaHang) * 1.10, 2)
    FROM dbo.DMHangHoa h
    WHERE h.MaHang IN (SELECT MaHang FROM inserted UNION SELECT MaHang FROM deleted)
      AND dbo.fn_GiaLoDangBan(h.MaHang) IS NOT NULL;

    UPDATE hdn
    SET hdn.TongTien = ISNULL((SELECT SUM(ct.ThanhTien) FROM dbo.ChiTietHoaDonNhap ct WHERE ct.SoHDN = hdn.SoHDN), 0)
    FROM dbo.HoaDonNhap hdn
    WHERE hdn.SoHDN IN (SELECT SoHDN FROM inserted UNION SELECT SoHDN FROM deleted);
END;
GO

-- 5.2. Trigger bán: tính tiền theo giá đã chốt, trừ tồn và chuyển giá khi hết lô.
CREATE OR ALTER TRIGGER trg_ChiTietHoaDonBan_UpdateHangHoa
ON ChiTietHoaDonBan
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        UPDATE ct
        SET ct.ThanhTien = ROUND(
            CAST(i.SoLuong AS DECIMAL(18, 4)) * ISNULL(i.DonGia, ISNULL(h.DonGiaBan, 0)) *
            (1 - CAST(ISNULL(i.GiamGia, 0) AS DECIMAL(9, 4)) / 100), 0)
        FROM dbo.ChiTietHoaDonBan ct
        JOIN inserted i ON i.SoHDB = ct.SoHDB AND i.MaHang = ct.MaHang
        JOIN dbo.DMHangHoa h ON h.MaHang = ct.MaHang;
    END;

    ;WITH ThayDoiSL AS
    (
        SELECT MaHang, SUM(SoLuongChenhLech) AS TongThayDoiSL
        FROM
        (
            SELECT MaHang, -SoLuong AS SoLuongChenhLech FROM inserted
            UNION ALL
            SELECT MaHang, SoLuong AS SoLuongChenhLech FROM deleted
        ) AS Tmp
        GROUP BY MaHang
    )
    UPDATE h
    SET h.SoLuong = ISNULL(h.SoLuong, 0) + t.TongThayDoiSL
    FROM dbo.DMHangHoa h
    JOIN ThayDoiSL t ON t.MaHang = h.MaHang;

    IF EXISTS
    (
        SELECT 1
        FROM dbo.DMHangHoa h
        JOIN (SELECT MaHang FROM inserted UNION SELECT MaHang FROM deleted) AS touched
          ON touched.MaHang = h.MaHang
        WHERE ISNULL(h.SoLuong, 0) < 0
    )
    BEGIN
        ;THROW 51001, N'Số lượng tồn kho không đủ để hoàn tất hóa đơn bán.', 1;
    END;

    UPDATE h
    SET h.GiaNhap = dbo.fn_GiaLoDangBan(h.MaHang),
        h.DonGiaBan = ROUND(dbo.fn_GiaLoDangBan(h.MaHang) * 1.10, 2)
    FROM dbo.DMHangHoa h
    WHERE h.MaHang IN (SELECT MaHang FROM inserted UNION SELECT MaHang FROM deleted)
      AND dbo.fn_GiaLoDangBan(h.MaHang) IS NOT NULL;

    UPDATE hdb
    SET hdb.TongTien = ISNULL((SELECT SUM(ct.ThanhTien) FROM dbo.ChiTietHoaDonBan ct WHERE ct.SoHDB = hdb.SoHDB), 0)
    FROM dbo.HoaDonBan hdb
    WHERE hdb.SoHDB IN (SELECT SoHDB FROM inserted UNION SELECT SoHDB FROM deleted);
END;
GO
-- 5.3. Trigger bổ trợ: giữ Giá bán = 110% Giá nhập khi trigger hoá đơn cập nhật giá lô.
-- Không dùng thao tác sửa danh mục để thay đổi giá; giá chỉ thay đổi qua chứng từ nhập.
CREATE OR ALTER TRIGGER trg_DMHangHoa_AutoUpdateDonGiaBan
ON DMHangHoa
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF UPDATE(GiaNhap)
    BEGIN
        UPDATE h
        SET h.DonGiaBan = ROUND(i.GiaNhap * 1.10, 2)
        FROM DMHangHoa h
        JOIN inserted i ON h.MaHang = i.MaHang
        WHERE i.GiaNhap IS NOT NULL;
    END
END;
GO

-- ==========================================================================================
-- 6. CÁC STORED PROCEDURE & TRUY VẤN THEO YÊU CẦU ĐỀ BÀI (Dòng 36 - 41)
-- ==========================================================================================

-- Yêu cầu 4: Tìm kiếm sản phẩm theo: chất liệu, nước sx, thời gian bảo hành
CREATE OR ALTER PROCEDURE sp_TimKiemSanPham
    @MaChatLieu NVARCHAR(20) = NULL,
    @MaNuocSX NVARCHAR(20) = NULL,
    @ThoiGianBaoHanh NVARCHAR(50) = NULL,
    @NgungKinhDoanh BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        h.MaHang,
        h.TenHang,
        kt.TenKichThuoc,
        cl.TenChatLieu,
        ms.TenMau,
        nsx.TenNuocSX,
        h.SoLuong,
        h.GiaNhap,
        h.DonGiaBan,
        h.ThoiGianBaoHanh,
        h.Anh,
        h.GhiChu,
        h.NgungKinhDoanh
    FROM DMHangHoa h
    LEFT JOIN KichThuoc kt ON h.MaKichThuoc = kt.MaKichThuoc
    LEFT JOIN ChatLieu cl ON h.MaChatLieu = cl.MaChatLieu
    LEFT JOIN MauSac ms ON h.MaMau = ms.MaMau
    LEFT JOIN NuocSanXuat nsx ON h.MaNuocSX = nsx.MaNuocSX
    WHERE (@MaChatLieu IS NULL OR @MaChatLieu = '' OR h.MaChatLieu = @MaChatLieu)
      AND (@MaNuocSX IS NULL OR @MaNuocSX = '' OR h.MaNuocSX = @MaNuocSX)
      AND (@ThoiGianBaoHanh IS NULL OR @ThoiGianBaoHanh = '' OR h.ThoiGianBaoHanh LIKE N'%' + @ThoiGianBaoHanh + '%')
      AND (@NgungKinhDoanh IS NULL OR h.NgungKinhDoanh = @NgungKinhDoanh)
    ORDER BY h.NgungKinhDoanh, h.MaHang;
END;
GO

-- Yêu cầu 5: Tìm kiếm các HĐ bán theo: mã hàng, ngày bán, tổng tiền
CREATE OR ALTER PROCEDURE sp_TimKiemHoaDonBan
    @MaHang NVARCHAR(20) = NULL,
    @NgayBan DATE = NULL,
    @TongTienTu DECIMAL(18, 2) = NULL,
    @TongTienDen DECIMAL(18, 2) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT
        hdb.SoHDB,
        hdb.NgayBan,
        nv.TenNV,
        ISNULL(kh.TenKhach, N'Khách lẻ') AS TenKhach,
        hdb.TongTien
    FROM HoaDonBan hdb
    LEFT JOIN NhanVien nv ON hdb.MaNV = nv.MaNV
    LEFT JOIN KhachHang kh ON hdb.MaKhach = kh.MaKhach
    LEFT JOIN ChiTietHoaDonBan ct ON hdb.SoHDB = ct.SoHDB
    WHERE (@MaHang IS NULL OR @MaHang = '' OR ct.MaHang = @MaHang)
      AND (@NgayBan IS NULL OR hdb.NgayBan = @NgayBan)
      AND (@TongTienTu IS NULL OR hdb.TongTien >= @TongTienTu)
      AND (@TongTienDen IS NULL OR hdb.TongTien <= @TongTienDen);
END;
GO

-- Yêu cầu 6: Báo cáo danh sách 3 sản phẩm bán được nhiều nhất trong một quý chọn trước
CREATE OR ALTER PROCEDURE sp_BaoCaoTop3SanPhamTheoQuy
    @Nam INT,
    @Quy INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 3
        h.MaHang,
        h.TenHang,
        cl.TenChatLieu,
        ms.TenMau,
        kt.TenKichThuoc,
        nsx.TenNuocSX,
        SUM(ct.SoLuong) AS TongSoLuongBan,
        SUM(ct.ThanhTien) AS TongDoanhThu
    FROM ChiTietHoaDonBan ct
    JOIN HoaDonBan hdb ON ct.SoHDB = hdb.SoHDB
    JOIN DMHangHoa h ON ct.MaHang = h.MaHang
    LEFT JOIN ChatLieu cl ON h.MaChatLieu = cl.MaChatLieu
    LEFT JOIN MauSac ms ON h.MaMau = ms.MaMau
    LEFT JOIN KichThuoc kt ON h.MaKichThuoc = kt.MaKichThuoc
    LEFT JOIN NuocSanXuat nsx ON h.MaNuocSX = nsx.MaNuocSX
    WHERE YEAR(hdb.NgayBan) = @Nam 
      AND DATEPART(QUARTER, hdb.NgayBan) = @Quy
    GROUP BY h.MaHang, h.TenHang, cl.TenChatLieu, ms.TenMau, kt.TenKichThuoc, nsx.TenNuocSX
    ORDER BY TongSoLuongBan DESC;
END;
GO

-- Yêu cầu 7: Báo cáo chi tiết danh sách các hoá đơn nhập do một nhân viên nhập được chọn trước
CREATE OR ALTER PROCEDURE sp_BaoCaoHoaDonNhapTheoNV
    @MaNV NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        hdn.SoHDN,
        hdn.NgayNhap,
        nv.MaNV,
        nv.TenNV,
        ncc.TenNCC,
        h.MaHang,
        h.TenHang,
        kt.TenKichThuoc,
        cl.TenChatLieu,
        ms.TenMau,
        nsx.TenNuocSX,
        ct.SoLuong,
        ct.DonGia,
        ct.GiamGia,
        ct.ThanhTien,
        hdn.TongTien AS TongTienHoaDon
    FROM HoaDonNhap hdn
    JOIN NhanVien nv ON hdn.MaNV = nv.MaNV
    LEFT JOIN NhaCungCap ncc ON hdn.MaNCC = ncc.MaNCC
    JOIN ChiTietHoaDonNhap ct ON hdn.SoHDN = ct.SoHDN
    JOIN DMHangHoa h ON ct.MaHang = h.MaHang
    LEFT JOIN KichThuoc kt ON h.MaKichThuoc = kt.MaKichThuoc
    LEFT JOIN ChatLieu cl ON h.MaChatLieu = cl.MaChatLieu
    LEFT JOIN MauSac ms ON h.MaMau = ms.MaMau
    LEFT JOIN NuocSanXuat nsx ON h.MaNuocSX = nsx.MaNuocSX
    WHERE hdn.MaNV = @MaNV
    ORDER BY hdn.NgayNhap DESC, hdn.SoHDN;
END;
GO

-- Yêu cầu 8: Báo cáo chi tiết danh sách 5 hoá đơn có tổng tiền bán hàng nhỏ nhất theo quý chọn trước
CREATE OR ALTER PROCEDURE sp_BaoCaoTop5HDBanNhoNhatTheoQuy
    @Nam INT,
    @Quy INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 5
        hdb.SoHDB,
        hdb.NgayBan,
        nv.TenNV,
        ISNULL(kh.TenKhach, N'Khách lẻ') AS TenKhach,
        hdb.TongTien
    FROM HoaDonBan hdb
    LEFT JOIN NhanVien nv ON hdb.MaNV = nv.MaNV
    LEFT JOIN KhachHang kh ON hdb.MaKhach = kh.MaKhach
    WHERE YEAR(hdb.NgayBan) = @Nam 
      AND DATEPART(QUARTER, hdb.NgayBan) = @Quy
    ORDER BY hdb.TongTien ASC;
END;
GO

-- Yêu cầu 9: Báo cáo danh sách các khách hàng mua hàng theo tháng chọn trước
CREATE OR ALTER PROCEDURE sp_BaoCaoKhachHangTheoThang
    @Nam INT,
    @Thang INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT
        kh.MaKhach,
        kh.TenKhach,
        kh.DiaChi,
        kh.DienThoai,
        COUNT(hdb.SoHDB) AS SoLuotMua,
        SUM(hdb.TongTien) AS TongTienMua
    FROM KhachHang kh
    JOIN HoaDonBan hdb ON kh.MaKhach = hdb.MaKhach
    WHERE YEAR(hdb.NgayBan) = @Nam 
      AND MONTH(hdb.NgayBan) = @Thang
    GROUP BY kh.MaKhach, kh.TenKhach, kh.DiaChi, kh.DienThoai
    ORDER BY TongTienMua DESC;
END;
GO

-- ==========================================================================================
-- 7. CHÈN DỮ LIỆU MẪU (TEST DATA)
-- ==========================================================================================

-- 7.1. Bảng Kích thước
INSERT INTO KichThuoc (MaKichThuoc, TenKichThuoc) VALUES
('KT01', N'Dài 1.2m x Sâu 0.6m'),
('KT02', N'Dài 1.5m x Sâu 0.6m'),
('KT03', N'Dài 2.0m x Sâu 0.6m'),
('KT04', N'Dài 2.4m x Sâu 0.6m'),
('KT05', N'Thiết kế theo yêu cầu');

-- 7.2. Bảng Chất liệu
INSERT INTO ChatLieu (MaChatLieu, TenChatLieu) VALUES
('CL01', N'Gỗ công nghiệp MDF lõi xanh'),
('CL02', N'Gỗ Acrylic bóng gương'),
('CL03', N'Inox 304 cánh kính'),
('CL04', N'Gỗ Sồi Nga tự nhiên'),
('CL05', N'Nhựa cao cấp Picomat');

-- 7.3. Bảng Nước sản xuất
INSERT INTO NuocSanXuat (MaNuocSX, TenNuocSX) VALUES
('NSX01', N'Việt Nam'),
('NSX02', N'Hàn Quốc'),
('NSX03', N'Nhật Bản'),
('NSX04', N'Đức'),
('NSX05', N'Trung Quốc');

-- 7.4. Bảng Màu sắc
INSERT INTO MauSac (MaMau, TenMau) VALUES
('M01', N'Trắng sứ'),
('M02', N'Xám ghi'),
('M03', N'Vân gỗ sồi'),
('M04', N'Đen mờ'),
('M05', N'Xanh rêu pastel');

-- 7.5. Bảng Công việc
INSERT INTO CongViec (MaCV, TenCV, MucLuong) VALUES
('CV01', N'Quản lý cửa hàng', 18000000),
('CV02', N'Nhân viên bán hàng', 8000000),
('CV03', N'Nhân viên kỹ thuật - lắp đặt', 10000000),
('CV04', N'Kế toán kho', 9000000);

-- 7.6. Bảng Nhân viên
INSERT INTO NhanVien (MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV) VALUES
('NV01', N'Nguyễn Văn An', N'Nam', '1992-05-15', '0912345678', N'Cầu Giấy, Hà Nội', 'CV01'),
('NV02', N'Trần Thị Bích', N'Nữ', '1996-08-20', '0923456789', N'Đống Đa, Hà Nội', 'CV02'),
('NV03', N'Lê Hoàng Long', N'Nam', '1995-12-10', '0934567890', N'Hà Đông, Hà Nội', 'CV03'),
('NV04', N'Phạm Thu Hà', N'Nữ', '1998-03-25', '0945678901', N'Thanh Xuân, Hà Nội', 'CV04');

-- 7.7. Bảng Khách hàng
INSERT INTO KhachHang (MaKhach, TenKhach, DiaChi, DienThoai) VALUES
('KH01', N'Đỗ Minh Tuấn', N'Vinhomes Ocean Park, Gia Lâm', '0901112233'),
('KH02', N'Nguyễn Thu Trang', N'EcoPark, Hưng Yên', '0902223344'),
('KH03', N'Vũ Đình Hưng', N'Royal City, Thanh Xuân, Hà Nội', '0903334455'),
('KH04', N'Lê Mai Phương', N'Times City, Hai Bà Trưng, Hà Nội', '0904445566');

-- 7.8. Bảng Nhà cung cấp
INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi, DienThoai) VALUES
('NCC01', N'Công ty Cổ phần Gỗ An Cường', N'Bình Dương', '02743888999'),
('NCC02', N'Công ty TNHH Nhựa Picomat', N'Hà Nội', '0243666777'),
('NCC03', N'Tập đoàn Hafele Việt Nam', N'TP. Hồ Chí Minh', '0283999888');

-- 7.9. Bảng Hàng hoá (Giá bán sẽ tự động là 110% Giá nhập qua trigger)
INSERT INTO DMHangHoa (MaHang, TenHang, MaKichThuoc, MaChatLieu, MaMau, MaNuocSX, SoLuong, GiaNhap, ThoiGianBaoHanh, Anh, GhiChu) VALUES
('TB01', N'Tủ bếp Acrylic chữ I hiện đại', 'KT01', 'CL02', 'M01', 'NSX01', 0, 12000000, N'24 tháng', 'tu_bep_01.jpg', N'Bóng gương cao cấp'),
('TB02', N'Tủ bếp Inox cánh kính cường lực', 'KT02', 'CL03', 'M02', 'NSX04', 0, 22000000, N'36 tháng', 'tu_bep_02.jpg', N'Khung inox 304 không gỉ'),
('TB03', N'Tủ bếp Gỗ Sồi tự nhiên kiểu Bắc Âu', 'KT03', 'CL04', 'M03', 'NSX03', 0, 16000000, N'24 tháng', 'tu_bep_03.jpg', N'Gỗ xử lý chống mối mọt'),
('TB04', N'Tủ bếp MDF lõi xanh phủ Melamine', 'KT04', 'CL01', 'M04', 'NSX01', 0, 9500000, N'12 tháng', 'tu_bep_04.jpg', N'Chống ẩm tốt'),
('TB05', N'Tủ bếp nhựa Picomat chống nước', 'KT01', 'CL05', 'M05', 'NSX02', 0, 14000000, N'24 tháng', 'tu_bep_05.jpg', N'Chịu nước 100%');

-- 7.10. Tồn đầu kỳ được ghi nhận bằng chứng từ nhập để đối chiếu tồn kho.
INSERT INTO HoaDonNhap (SoHDN, MaNV, NgayNhap, MaNCC, TongTien) VALUES
('HDN00', 'NV04', '2025-12-31', 'NCC01', 0);

INSERT INTO ChiTietHoaDonNhap (SoHDN, MaHang, SoLuong, DonGia, GiamGia) VALUES
('HDN00', 'TB01', 5, 12000000, 0),
('HDN00', 'TB02', 3, 22000000, 0),
('HDN00', 'TB03', 4, 16000000, 0),
('HDN00', 'TB04', 8,  9500000, 0),
('HDN00', 'TB05', 6, 14000000, 0);
-- 7.11. Hoá đơn nhập cập nhật tồn và giá theo lô FIFO; giá bán bằng 110% giá nhập của lô đang bán.
INSERT INTO HoaDonNhap (SoHDN, MaNV, NgayNhap, MaNCC, TongTien) VALUES
('HDN01', 'NV04', '2026-01-10', 'NCC01', 0),
('HDN02', 'NV04', '2026-02-15', 'NCC02', 0);

INSERT INTO ChiTietHoaDonNhap (SoHDN, MaHang, SoLuong, DonGia, GiamGia) VALUES
('HDN01', 'TB01', 3, 12000000, 0),
('HDN01', 'TB04', 5, 9500000, 5),
('HDN02', 'TB02', 2, 22000000, 2);

-- 7.12. Bảng Hóa đơn bán & Chi tiết hóa đơn bán (Trigger tự trừ Số lượng tồn, tính Thành tiền và Tổng tiền)
INSERT INTO HoaDonBan (SoHDB, MaNV, NgayBan, MaKhach, TongTien) VALUES
('HDB01', 'NV02', '2026-02-20', 'KH01', 0),
('HDB02', 'NV02', '2026-03-05', 'KH02', 0),
('HDB03', 'NV02', '2026-03-12', 'KH03', 0);

INSERT INTO ChiTietHoaDonBan (SoHDB, MaHang, SoLuong, GiamGia) VALUES
('HDB01', 'TB01', 1, 0),
('HDB01', 'TB04', 1, 5),
('HDB02', 'TB02', 1, 0),
('HDB03', 'TB01', 2, 10);
GO

-- ==========================================================================================
-- KIỂM TRA DỮ LIỆU VÀ CÁC NGHIỆP VỤ ĐÃ CÀI ĐẶT
-- ==========================================================================================
-- 1. Kiểm tra DM hàng hoá sau khi trigger tự cập nhật Giá bán = 110% Giá nhập:
SELECT MaHang, TenHang, SoLuong, GiaNhap, DonGiaBan FROM DMHangHoa;

-- 2. Kiểm tra Hóa đơn bán và Tổng tiền tự động:
SELECT * FROM HoaDonBan;

-- 3. Chạy thử các Store Procedure:
-- YC 4: Tìm kiếm theo chất liệu 'CL02'
EXEC sp_TimKiemSanPham @MaChatLieu = 'CL02';

-- YC 6: Top 3 sản phẩm bán nhiều nhất Quý 1 năm 2026
EXEC sp_BaoCaoTop3SanPhamTheoQuy @Nam = 2026, @Quy = 1;

-- YC 7: Danh sách hóa đơn nhập của nhân viên NV04
EXEC sp_BaoCaoHoaDonNhapTheoNV @MaNV = 'NV04';

-- YC 9: Khách hàng mua hàng trong tháng 3 năm 2026
EXEC sp_BaoCaoKhachHangTheoThang @Nam = 2026, @Thang = 3;
GO







-- >>> KET THUC FRESH <<<

-- >>> BAT DAU UPGRADE A: DOI MA NUOC SAN XUAT <<<
-- Chuyển mã nước sản xuất cũ thành mã có tiền tố NSX.
-- Có thể chạy trên database đang dùng; khóa ngoại MaNuocSX cập nhật dây chuyền.
USE QuanLyBanTuBep;
GO
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @MaDoi TABLE (MaCu NVARCHAR(20) PRIMARY KEY, MaMoi NVARCHAR(20) NOT NULL);
    INSERT INTO @MaDoi (MaCu, MaMoi) VALUES
        (N'VN', N'NSX01'),
        (N'HQ', N'NSX02'),
        (N'NB', N'NSX03'),
        (N'DE', N'NSX04'),
        (N'CN', N'NSX05');

    IF EXISTS (
        SELECT 1
        FROM dbo.NuocSanXuat cu
        JOIN @MaDoi d ON d.MaCu = cu.MaNuocSX
        JOIN dbo.NuocSanXuat moi ON moi.MaNuocSX = d.MaMoi
    )
    BEGIN
        ;THROW 51002, N'Đã có mã NSX01-NSX05 trong danh mục. Hãy kiểm tra dữ liệu trước khi đổi mã cũ.', 1;
    END;

    UPDATE nsx
    SET MaNuocSX = d.MaMoi
    FROM dbo.NuocSanXuat nsx
    JOIN @MaDoi d ON d.MaCu = nsx.MaNuocSX;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    ;THROW;
END CATCH;
GO

-- >>> KET THUC UPGRADE A <<<

-- >>> BAT DAU UPGRADE B: CAP NHAT TRIGGER CO BAN <<<
-- Cập nhật an toàn trigger hóa đơn trên cơ sở dữ liệu đã có.
-- Chạy riêng giai đoạn UPGRADE B trong file tổng hợp; không chạy FRESH vì phần đó xóa và tạo lại bảng.
USE QuanLyBanTuBep;
GO
-- Cho phép tạo mã khách tự động ngay trên hóa đơn mà không bắt buộc nhập tên.
ALTER TABLE dbo.KhachHang ALTER COLUMN TenKhach NVARCHAR(100) NULL;
GO

IF COL_LENGTH(N'dbo.ChiTietHoaDonBan', N'DonGia') IS NULL
    ALTER TABLE dbo.ChiTietHoaDonBan ADD DonGia DECIMAL(18, 2) NULL;
GO

-- Giai đoạn nâng cấp nền: tạm lấy phiếu nhập gần nhất; UPGRADE D sẽ thay bằng FIFO.
CREATE OR ALTER TRIGGER trg_ChiTietHoaDonNhap_UpdateHangHoa
ON ChiTietHoaDonNhap
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        UPDATE ct
        SET ct.ThanhTien = ROUND(
            CAST(i.SoLuong AS DECIMAL(18, 4)) * i.DonGia *
            (1 - CAST(ISNULL(i.GiamGia, 0) AS DECIMAL(9, 4)) / 100), 0)
        FROM ChiTietHoaDonNhap ct
        JOIN inserted i ON ct.SoHDN = i.SoHDN AND ct.MaHang = i.MaHang;
    END;

    ;WITH ThongTinCapNhat AS
    (
        SELECT MaHang, SUM(SoLuongChenhLech) AS TongThayDoiSL
        FROM
        (
            SELECT MaHang, SoLuong AS SoLuongChenhLech FROM inserted
            UNION ALL
            SELECT MaHang, -SoLuong AS SoLuongChenhLech FROM deleted
        ) AS Tmp
        GROUP BY MaHang
    )
    UPDATE h
    SET h.SoLuong = ISNULL(h.SoLuong, 0) + t.TongThayDoiSL
    FROM DMHangHoa h
    JOIN ThongTinCapNhat t ON h.MaHang = t.MaHang;

    ;WITH HangBiAnhHuong AS
    (
        SELECT MaHang FROM inserted
        UNION
        SELECT MaHang FROM deleted
    )
    UPDATE h
    SET h.GiaNhap = COALESCE(g.GiaNhapMoiNhat, 0),
        h.DonGiaBan = ROUND(COALESCE(g.GiaNhapMoiNhat, 0) * 1.10, 2)
    FROM DMHangHoa h
    JOIN HangBiAnhHuong a ON a.MaHang = h.MaHang
    OUTER APPLY
    (
        SELECT TOP (1) ct.DonGia AS GiaNhapMoiNhat
        FROM ChiTietHoaDonNhap ct
        JOIN HoaDonNhap hd ON hd.SoHDN = ct.SoHDN
        WHERE ct.MaHang = h.MaHang
        ORDER BY hd.NgayNhap DESC, hd.SoHDN DESC
    ) g;

    UPDATE hdn
    SET TongTien = ISNULL((SELECT SUM(ThanhTien) FROM ChiTietHoaDonNhap ct WHERE ct.SoHDN = hdn.SoHDN), 0)
    FROM HoaDonNhap hdn
    WHERE hdn.SoHDN IN (SELECT SoHDN FROM inserted UNION SELECT SoHDN FROM deleted);
END;
GO

CREATE OR ALTER TRIGGER trg_ChiTietHoaDonBan_UpdateHangHoa
ON ChiTietHoaDonBan
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        UPDATE ct
        SET ct.ThanhTien = ROUND(
            CAST(i.SoLuong AS DECIMAL(18, 4)) * ISNULL(i.DonGia, ISNULL(h.DonGiaBan, 0)) *
            (1 - CAST(ISNULL(i.GiamGia, 0) AS DECIMAL(9, 4)) / 100), 0)
        FROM ChiTietHoaDonBan ct
        JOIN inserted i ON ct.SoHDB = i.SoHDB AND ct.MaHang = i.MaHang
        JOIN DMHangHoa h ON ct.MaHang = h.MaHang;
    END;

    ;WITH ThayDoiSL AS
    (
        SELECT MaHang, SUM(SoLuongChenhLech) AS TongThayDoiSL
        FROM
        (
            SELECT MaHang, -SoLuong AS SoLuongChenhLech FROM inserted
            UNION ALL
            SELECT MaHang, SoLuong AS SoLuongChenhLech FROM deleted
        ) AS Tmp
        GROUP BY MaHang
    )
    UPDATE h
    SET h.SoLuong = ISNULL(h.SoLuong, 0) + t.TongThayDoiSL
    FROM DMHangHoa h
    JOIN ThayDoiSL t ON h.MaHang = t.MaHang;

    IF EXISTS
    (
        SELECT 1
        FROM DMHangHoa h
        JOIN (SELECT MaHang FROM inserted UNION SELECT MaHang FROM deleted) AS touched
            ON touched.MaHang = h.MaHang
        WHERE ISNULL(h.SoLuong, 0) < 0
    )
    BEGIN
        ;THROW 51001, N'Số lượng tồn kho không đủ để hoàn tất hóa đơn bán.', 1;
    END;

    UPDATE hdb
    SET TongTien = ISNULL((SELECT SUM(ThanhTien) FROM ChiTietHoaDonBan ct WHERE ct.SoHDB = hdb.SoHDB), 0)
    FROM HoaDonBan hdb
    WHERE hdb.SoHDB IN (SELECT SoHDB FROM inserted UNION SELECT SoHDB FROM deleted);
END;
GO

-- Đồng bộ giá tạm theo phiếu nhập gần nhất; UPGRADE D sẽ tính lại theo FIFO.
UPDATE h
SET h.GiaNhap = COALESCE(g.GiaNhapMoiNhat, 0),
    h.DonGiaBan = ROUND(COALESCE(g.GiaNhapMoiNhat, 0) * 1.10, 2)
FROM dbo.DMHangHoa h
OUTER APPLY
(
    SELECT TOP (1) ct.DonGia AS GiaNhapMoiNhat
    FROM dbo.ChiTietHoaDonNhap ct
    JOIN dbo.HoaDonNhap hd ON hd.SoHDN = ct.SoHDN
    WHERE ct.MaHang = h.MaHang
    ORDER BY hd.NgayNhap DESC, hd.SoHDN DESC
) g;
GO



-- Loại bỏ cột dự phòng cũ sau khi trigger đã chuyển sang đọc lịch sử phiếu nhập.
IF COL_LENGTH(N'dbo.DMHangHoa', N'GiaNhapGoc') IS NOT NULL
BEGIN
    DECLARE @TenDefaultGiaNhapGoc sysname, @LenhXoaDefault nvarchar(500);
    SELECT @TenDefaultGiaNhapGoc = dc.name
    FROM sys.default_constraints dc
    JOIN sys.columns c ON c.object_id = dc.parent_object_id AND c.column_id = dc.parent_column_id
    WHERE dc.parent_object_id = OBJECT_ID(N'dbo.DMHangHoa')
      AND c.name = N'GiaNhapGoc';

    IF @TenDefaultGiaNhapGoc IS NOT NULL
    BEGIN
        SET @LenhXoaDefault = N'ALTER TABLE dbo.DMHangHoa DROP CONSTRAINT ' + QUOTENAME(@TenDefaultGiaNhapGoc);
        EXEC sys.sp_executesql @LenhXoaDefault;
    END;

    ALTER TABLE dbo.DMHangHoa DROP COLUMN GiaNhapGoc;
END;
GO
-- >>> KET THUC UPGRADE B <<<

-- >>> BAT DAU UPGRADE C: TAO TON DAU KY HDN00 <<<
-- Chuyển tồn kho hiện có thành chứng từ tồn đầu kỳ HDN00.
-- Chạy sau phần B trên bản sao database trước.
-- Chạy lại sẽ không thay đổi dữ liệu nếu HDN00 đã tồn tại.
USE QuanLyBanTuBep;
GO
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;

    IF EXISTS (SELECT 1 FROM dbo.HoaDonNhap WHERE SoHDN = N'HDN00')
    BEGIN
        ROLLBACK TRANSACTION;
        PRINT N'Đã có hóa đơn HDN00. Không thực hiện thay đổi.';
        RETURN;
    END;

    SELECT
        h.MaHang,
        CAST(ISNULL(h.SoLuong, 0) - (ISNULL(n.TongNhap, 0) - ISNULL(b.TongBan, 0)) AS INT) AS TonDau,
        CAST(ISNULL(h.GiaNhap, 0) AS DECIMAL(18, 2)) AS GiaNhap
    INTO #TonDau
    FROM dbo.DMHangHoa h
    LEFT JOIN (
        SELECT MaHang, SUM(SoLuong) AS TongNhap
        FROM dbo.ChiTietHoaDonNhap
        GROUP BY MaHang
    ) n ON n.MaHang = h.MaHang
    LEFT JOIN (
        SELECT MaHang, SUM(SoLuong) AS TongBan
        FROM dbo.ChiTietHoaDonBan
        GROUP BY MaHang
    ) b ON b.MaHang = h.MaHang;

    IF EXISTS (SELECT 1 FROM #TonDau WHERE TonDau < 0)
    BEGIN
        SELECT MaHang, TonDau AS TonDauAm
        FROM #TonDau
        WHERE TonDau < 0
        ORDER BY MaHang;

        ROLLBACK TRANSACTION;
        RAISERROR(N'Tồn đầu kỳ âm. Dữ liệu chưa được thay đổi; hãy đối soát các mặt hàng được liệt kê.', 16, 1);
    END;

    DELETE FROM #TonDau WHERE TonDau = 0;
    IF NOT EXISTS (SELECT 1 FROM #TonDau)
    BEGIN
        ROLLBACK TRANSACTION;
        PRINT N'Không có tồn đầu kỳ cần chuyển. Không tạo hóa đơn HDN00.';
        RETURN;
    END;

    DECLARE @MaNV NVARCHAR(20), @MaNCC NVARCHAR(20), @NgayGoc DATE, @NgayHDN00 DATE;

    SELECT TOP (1) @MaNV = MaNV, @MaNCC = MaNCC
    FROM dbo.HoaDonNhap
    ORDER BY NgayNhap ASC, SoHDN ASC;

    IF @MaNV IS NULL
        SELECT TOP (1) @MaNV = MaNV FROM dbo.NhanVien ORDER BY MaNV;
    IF @MaNCC IS NULL
        SELECT TOP (1) @MaNCC = MaNCC FROM dbo.NhaCungCap ORDER BY MaNCC;

    IF @MaNV IS NULL OR @MaNCC IS NULL
        RAISERROR(N'Cần có ít nhất một nhân viên và một nhà cung cấp để lập hóa đơn tồn đầu HDN00.', 16, 1);

    SELECT @NgayGoc = MIN(Ngay)
    FROM (
        SELECT NgayNhap AS Ngay FROM dbo.HoaDonNhap
        UNION ALL
        SELECT NgayBan AS Ngay FROM dbo.HoaDonBan
    ) d;

    SET @NgayHDN00 = DATEADD(DAY, -1, COALESCE(@NgayGoc, CONVERT(DATE, GETDATE())));

    INSERT INTO dbo.HoaDonNhap (SoHDN, MaNV, NgayNhap, MaNCC, TongTien)
    VALUES (N'HDN00', @MaNV, @NgayHDN00, @MaNCC, 0);

    INSERT INTO dbo.ChiTietHoaDonNhap (SoHDN, MaHang, SoLuong, DonGia, GiamGia)
    SELECT N'HDN00', MaHang, TonDau, GiaNhap, 0
    FROM #TonDau;

    -- Trigger nhập đã cộng TonDau; trừ lại để giữ nguyên lượng tồn trước chuyển đổi.
    UPDATE h
    SET h.SoLuong = ISNULL(h.SoLuong, 0) - t.TonDau
    FROM dbo.DMHangHoa h
    JOIN #TonDau t ON t.MaHang = h.MaHang;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    ;THROW;
END CATCH;

-- Đối chiếu sau chuyển đổi: truy vấn phải trả về 0 dòng.
SELECT h.MaHang, h.SoLuong AS TonHienTai,
       ISNULL(n.SL, 0) AS TongNhap, ISNULL(b.SL, 0) AS TongBan,
       ISNULL(n.SL, 0) - ISNULL(b.SL, 0) AS TonTheoChungTu
FROM dbo.DMHangHoa h
LEFT JOIN (SELECT MaHang, SUM(SoLuong) AS SL FROM dbo.ChiTietHoaDonNhap GROUP BY MaHang) n ON n.MaHang = h.MaHang
LEFT JOIN (SELECT MaHang, SUM(SoLuong) AS SL FROM dbo.ChiTietHoaDonBan GROUP BY MaHang) b ON b.MaHang = h.MaHang
WHERE h.SoLuong <> ISNULL(n.SL, 0) - ISNULL(b.SL, 0);
GO
-- >>> KET THUC UPGRADE C <<<

-- >>> BAT DAU UPGRADE D: CAI FIFO VA DOI CHIEU <<<
-- Cài FIFO suy ra từ chứng từ nhập/bán trên database đang có dữ liệu.
-- Sao lưu trước; chạy sau UPGRADE B và UPGRADE C trong cùng file tổng hợp.
-- Giá nhập/giá bán hiện tại sẽ được tính lại theo lô FIFO đang bán.
USE QuanLyBanTuBep;
GO

IF COL_LENGTH(N'dbo.ChiTietHoaDonBan', N'DonGia') IS NULL
    ALTER TABLE dbo.ChiTietHoaDonBan ADD DonGia DECIMAL(18, 2) NULL;
GO
-- 5.0. Hàm suy ra giá và số lượng còn lại của lô FIFO đang bán.
CREATE OR ALTER FUNCTION dbo.fn_GiaLoDangBan (@MaHang NVARCHAR(20))
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @DaBan INT = (SELECT ISNULL(SUM(SoLuong), 0) FROM dbo.ChiTietHoaDonBan WHERE MaHang = @MaHang);
    DECLARE @Gia DECIMAL(18, 2);

    SELECT TOP (1) @Gia = lo.DonGia
    FROM (
        SELECT ct.DonGia,
               SUM(ct.SoLuong) OVER (ORDER BY hd.NgayNhap, hd.SoHDN ROWS UNBOUNDED PRECEDING) AS LuyKe
        FROM dbo.ChiTietHoaDonNhap ct
        JOIN dbo.HoaDonNhap hd ON hd.SoHDN = ct.SoHDN
        WHERE ct.MaHang = @MaHang
    ) AS lo
    WHERE lo.LuyKe > @DaBan
    ORDER BY lo.LuyKe;

    IF @Gia IS NULL
        SELECT TOP (1) @Gia = ct.DonGia
        FROM dbo.ChiTietHoaDonNhap ct
        JOIN dbo.HoaDonNhap hd ON hd.SoHDN = ct.SoHDN
        WHERE ct.MaHang = @MaHang
        ORDER BY hd.NgayNhap DESC, hd.SoHDN DESC;

    RETURN @Gia;
END;
GO

CREATE OR ALTER FUNCTION dbo.fn_ConLaiLoDangBan (@MaHang NVARCHAR(20))
RETURNS INT
AS
BEGIN
    DECLARE @DaBan INT = (SELECT ISNULL(SUM(SoLuong), 0) FROM dbo.ChiTietHoaDonBan WHERE MaHang = @MaHang);
    DECLARE @ConLai INT;

    SELECT TOP (1) @ConLai = lo.LuyKe - @DaBan
    FROM (
        SELECT SUM(ct.SoLuong) OVER (ORDER BY hd.NgayNhap, hd.SoHDN ROWS UNBOUNDED PRECEDING) AS LuyKe
        FROM dbo.ChiTietHoaDonNhap ct
        JOIN dbo.HoaDonNhap hd ON hd.SoHDN = ct.SoHDN
        WHERE ct.MaHang = @MaHang
    ) AS lo
    WHERE lo.LuyKe > @DaBan
    ORDER BY lo.LuyKe;

    RETURN ISNULL(@ConLai, 0);
END;
GO

-- 5.1. Trigger nhập: cộng tồn và đặt giá theo lô FIFO đang bán.
CREATE OR ALTER TRIGGER trg_ChiTietHoaDonNhap_UpdateHangHoa
ON ChiTietHoaDonNhap
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        UPDATE ct
        SET ct.ThanhTien = ROUND(
            CAST(i.SoLuong AS DECIMAL(18, 4)) * i.DonGia *
            (1 - CAST(ISNULL(i.GiamGia, 0) AS DECIMAL(9, 4)) / 100), 0)
        FROM ChiTietHoaDonNhap ct
        JOIN inserted i ON ct.SoHDN = i.SoHDN AND ct.MaHang = i.MaHang;
    END;

    ;WITH ThongTinCapNhat AS
    (
        SELECT MaHang, SUM(SoLuongChenhLech) AS TongThayDoiSL
        FROM
        (
            SELECT MaHang, SoLuong AS SoLuongChenhLech FROM inserted
            UNION ALL
            SELECT MaHang, -SoLuong AS SoLuongChenhLech FROM deleted
        ) AS Tmp
        GROUP BY MaHang
    )
    UPDATE h
    SET h.SoLuong = ISNULL(h.SoLuong, 0) + t.TongThayDoiSL
    FROM dbo.DMHangHoa h
    JOIN ThongTinCapNhat t ON t.MaHang = h.MaHang;

    UPDATE h
    SET h.GiaNhap = dbo.fn_GiaLoDangBan(h.MaHang),
        h.DonGiaBan = ROUND(dbo.fn_GiaLoDangBan(h.MaHang) * 1.10, 2)
    FROM dbo.DMHangHoa h
    WHERE h.MaHang IN (SELECT MaHang FROM inserted UNION SELECT MaHang FROM deleted)
      AND dbo.fn_GiaLoDangBan(h.MaHang) IS NOT NULL;

    UPDATE hdn
    SET hdn.TongTien = ISNULL((SELECT SUM(ct.ThanhTien) FROM dbo.ChiTietHoaDonNhap ct WHERE ct.SoHDN = hdn.SoHDN), 0)
    FROM dbo.HoaDonNhap hdn
    WHERE hdn.SoHDN IN (SELECT SoHDN FROM inserted UNION SELECT SoHDN FROM deleted);
END;
GO

-- 5.2. Trigger bán: tính tiền theo giá đã chốt, trừ tồn và chuyển giá khi hết lô.
CREATE OR ALTER TRIGGER trg_ChiTietHoaDonBan_UpdateHangHoa
ON ChiTietHoaDonBan
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        UPDATE ct
        SET ct.ThanhTien = ROUND(
            CAST(i.SoLuong AS DECIMAL(18, 4)) * ISNULL(i.DonGia, ISNULL(h.DonGiaBan, 0)) *
            (1 - CAST(ISNULL(i.GiamGia, 0) AS DECIMAL(9, 4)) / 100), 0)
        FROM dbo.ChiTietHoaDonBan ct
        JOIN inserted i ON i.SoHDB = ct.SoHDB AND i.MaHang = ct.MaHang
        JOIN dbo.DMHangHoa h ON h.MaHang = ct.MaHang;
    END;

    ;WITH ThayDoiSL AS
    (
        SELECT MaHang, SUM(SoLuongChenhLech) AS TongThayDoiSL
        FROM
        (
            SELECT MaHang, -SoLuong AS SoLuongChenhLech FROM inserted
            UNION ALL
            SELECT MaHang, SoLuong AS SoLuongChenhLech FROM deleted
        ) AS Tmp
        GROUP BY MaHang
    )
    UPDATE h
    SET h.SoLuong = ISNULL(h.SoLuong, 0) + t.TongThayDoiSL
    FROM dbo.DMHangHoa h
    JOIN ThayDoiSL t ON t.MaHang = h.MaHang;

    IF EXISTS
    (
        SELECT 1
        FROM dbo.DMHangHoa h
        JOIN (SELECT MaHang FROM inserted UNION SELECT MaHang FROM deleted) AS touched
          ON touched.MaHang = h.MaHang
        WHERE ISNULL(h.SoLuong, 0) < 0
    )
    BEGIN
        ;THROW 51001, N'Số lượng tồn kho không đủ để hoàn tất hóa đơn bán.', 1;
    END;

    UPDATE h
    SET h.GiaNhap = dbo.fn_GiaLoDangBan(h.MaHang),
        h.DonGiaBan = ROUND(dbo.fn_GiaLoDangBan(h.MaHang) * 1.10, 2)
    FROM dbo.DMHangHoa h
    WHERE h.MaHang IN (SELECT MaHang FROM inserted UNION SELECT MaHang FROM deleted)
      AND dbo.fn_GiaLoDangBan(h.MaHang) IS NOT NULL;

    UPDATE hdb
    SET hdb.TongTien = ISNULL((SELECT SUM(ct.ThanhTien) FROM dbo.ChiTietHoaDonBan ct WHERE ct.SoHDB = hdb.SoHDB), 0)
    FROM dbo.HoaDonBan hdb
    WHERE hdb.SoHDB IN (SELECT SoHDB FROM inserted UNION SELECT SoHDB FROM deleted);
END;
GO
-- Đồng bộ giá hiện tại theo lô FIFO đang bán; hàng chưa có phiếu nhập được giữ nguyên.
UPDATE h
SET h.GiaNhap = dbo.fn_GiaLoDangBan(h.MaHang),
    h.DonGiaBan = ROUND(dbo.fn_GiaLoDangBan(h.MaHang) * 1.10, 2)
FROM dbo.DMHangHoa h
WHERE dbo.fn_GiaLoDangBan(h.MaHang) IS NOT NULL;
GO

-- Đối chiếu tồn sau khi cài trigger; truy vấn phải trả về 0 dòng.
SELECT h.MaHang, h.SoLuong AS TonHienTai,
       ISNULL(n.SL, 0) AS TongNhap, ISNULL(b.SL, 0) AS TongBan,
       ISNULL(n.SL, 0) - ISNULL(b.SL, 0) AS TonTheoChungTu
FROM dbo.DMHangHoa h
LEFT JOIN (SELECT MaHang, SUM(SoLuong) AS SL FROM dbo.ChiTietHoaDonNhap GROUP BY MaHang) n ON n.MaHang = h.MaHang
LEFT JOIN (SELECT MaHang, SUM(SoLuong) AS SL FROM dbo.ChiTietHoaDonBan GROUP BY MaHang) b ON b.MaHang = h.MaHang
WHERE h.SoLuong <> ISNULL(n.SL, 0) - ISNULL(b.SL, 0);
GO
-- >>> KET THUC UPGRADE D <<<

-- >>> BAT DAU UPGRADE E: CAP NHAT CAC BAO CAO BIEN THE HANG <<<
-- Chạy riêng giai đoạn này sau UPGRADE D để các báo cáo cũng hiện đủ thuộc tính mặt hàng.
USE QuanLyBanTuBep;
GO

CREATE OR ALTER PROCEDURE dbo.sp_BaoCaoTop3SanPhamTheoQuy
    @Nam INT,
    @Quy INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 3
        h.MaHang,
        h.TenHang,
        cl.TenChatLieu,
        ms.TenMau,
        kt.TenKichThuoc,
        nsx.TenNuocSX,
        SUM(ct.SoLuong) AS TongSoLuongBan,
        SUM(ct.ThanhTien) AS TongDoanhThu
    FROM dbo.ChiTietHoaDonBan ct
    JOIN dbo.HoaDonBan hdb ON ct.SoHDB = hdb.SoHDB
    JOIN dbo.DMHangHoa h ON ct.MaHang = h.MaHang
    LEFT JOIN dbo.ChatLieu cl ON h.MaChatLieu = cl.MaChatLieu
    LEFT JOIN dbo.MauSac ms ON h.MaMau = ms.MaMau
    LEFT JOIN dbo.KichThuoc kt ON h.MaKichThuoc = kt.MaKichThuoc
    LEFT JOIN dbo.NuocSanXuat nsx ON h.MaNuocSX = nsx.MaNuocSX
    WHERE YEAR(hdb.NgayBan) = @Nam
      AND DATEPART(QUARTER, hdb.NgayBan) = @Quy
    GROUP BY h.MaHang, h.TenHang, cl.TenChatLieu, ms.TenMau, kt.TenKichThuoc, nsx.TenNuocSX
    ORDER BY TongSoLuongBan DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_BaoCaoHoaDonNhapTheoNV
    @MaNV NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        hdn.SoHDN,
        hdn.NgayNhap,
        nv.MaNV,
        nv.TenNV,
        ncc.TenNCC,
        h.MaHang,
        h.TenHang,
        kt.TenKichThuoc,
        cl.TenChatLieu,
        ms.TenMau,
        nsx.TenNuocSX,
        ct.SoLuong,
        ct.DonGia,
        ct.GiamGia,
        ct.ThanhTien,
        hdn.TongTien AS TongTienHoaDon
    FROM dbo.HoaDonNhap hdn
    JOIN dbo.NhanVien nv ON hdn.MaNV = nv.MaNV
    LEFT JOIN dbo.NhaCungCap ncc ON hdn.MaNCC = ncc.MaNCC
    JOIN dbo.ChiTietHoaDonNhap ct ON hdn.SoHDN = ct.SoHDN
    JOIN dbo.DMHangHoa h ON ct.MaHang = h.MaHang
    LEFT JOIN dbo.KichThuoc kt ON h.MaKichThuoc = kt.MaKichThuoc
    LEFT JOIN dbo.ChatLieu cl ON h.MaChatLieu = cl.MaChatLieu
    LEFT JOIN dbo.MauSac ms ON h.MaMau = ms.MaMau
    LEFT JOIN dbo.NuocSanXuat nsx ON h.MaNuocSX = nsx.MaNuocSX
    WHERE hdn.MaNV = @MaNV
    ORDER BY hdn.NgayNhap DESC, hdn.SoHDN;
END;
GO
-- >>> KET THUC UPGRADE E <<<

-- >>> BAT DAU UPGRADE F: THEM TRANG THAI NGUNG KINH DOANH <<<
-- Chạy riêng giai đoạn này sau UPGRADE E. Hàng cũ mặc định vẫn đang kinh doanh.
USE QuanLyBanTuBep;
GO

IF COL_LENGTH('dbo.DMHangHoa', 'NgungKinhDoanh') IS NULL
BEGIN
    ALTER TABLE dbo.DMHangHoa
        ADD NgungKinhDoanh BIT NOT NULL
            CONSTRAINT DF_DMHangHoa_NgungKD DEFAULT (0) WITH VALUES;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_TimKiemSanPham
    @MaChatLieu NVARCHAR(20) = NULL,
    @MaNuocSX NVARCHAR(20) = NULL,
    @ThoiGianBaoHanh NVARCHAR(50) = NULL,
    @NgungKinhDoanh BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        h.MaHang,
        h.TenHang,
        kt.TenKichThuoc,
        cl.TenChatLieu,
        ms.TenMau,
        nsx.TenNuocSX,
        h.SoLuong,
        h.GiaNhap,
        h.DonGiaBan,
        h.ThoiGianBaoHanh,
        h.Anh,
        h.GhiChu,
        h.NgungKinhDoanh
    FROM dbo.DMHangHoa h
    LEFT JOIN dbo.KichThuoc kt ON h.MaKichThuoc = kt.MaKichThuoc
    LEFT JOIN dbo.ChatLieu cl ON h.MaChatLieu = cl.MaChatLieu
    LEFT JOIN dbo.MauSac ms ON h.MaMau = ms.MaMau
    LEFT JOIN dbo.NuocSanXuat nsx ON h.MaNuocSX = nsx.MaNuocSX
    WHERE (@MaChatLieu IS NULL OR @MaChatLieu = '' OR h.MaChatLieu = @MaChatLieu)
      AND (@MaNuocSX IS NULL OR @MaNuocSX = '' OR h.MaNuocSX = @MaNuocSX)
      AND (@ThoiGianBaoHanh IS NULL OR @ThoiGianBaoHanh = '' OR h.ThoiGianBaoHanh LIKE N'%' + @ThoiGianBaoHanh + '%')
      AND (@NgungKinhDoanh IS NULL OR h.NgungKinhDoanh = @NgungKinhDoanh)
    ORDER BY h.NgungKinhDoanh, h.MaHang;
END;
GO
-- >>> KET THUC UPGRADE F <<<

-- >>> BAT DAU UPGRADE G: SUA BAO CAO TOP 3 HIEN THI DU THUOC TINH HANG <<<
-- Chạy riêng giai đoạn này nếu báo cáo Top 3 chưa hiện kích thước hoặc nước sản xuất.
USE QuanLyBanTuBep;
GO

CREATE OR ALTER PROCEDURE dbo.sp_BaoCaoTop3SanPhamTheoQuy
    @Nam INT,
    @Quy INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (3)
        h.MaHang AS MaHang,
        h.TenHang AS TenHang,
        kt.TenKichThuoc AS TenKichThuoc,
        cl.TenChatLieu AS TenChatLieu,
        ms.TenMau AS TenMau,
        nsx.TenNuocSX AS TenNuocSX,
        SUM(ct.SoLuong) AS TongSoLuongBan,
        SUM(ct.ThanhTien) AS TongDoanhThu
    FROM dbo.ChiTietHoaDonBan AS ct
    INNER JOIN dbo.HoaDonBan AS hdb ON hdb.SoHDB = ct.SoHDB
    INNER JOIN dbo.DMHangHoa AS h ON h.MaHang = ct.MaHang
    LEFT JOIN dbo.KichThuoc AS kt ON kt.MaKichThuoc = h.MaKichThuoc
    LEFT JOIN dbo.ChatLieu AS cl ON cl.MaChatLieu = h.MaChatLieu
    LEFT JOIN dbo.MauSac AS ms ON ms.MaMau = h.MaMau
    LEFT JOIN dbo.NuocSanXuat AS nsx ON nsx.MaNuocSX = h.MaNuocSX
    WHERE YEAR(hdb.NgayBan) = @Nam
      AND DATEPART(QUARTER, hdb.NgayBan) = @Quy
    GROUP BY
        h.MaHang,
        h.TenHang,
        kt.TenKichThuoc,
        cl.TenChatLieu,
        ms.TenMau,
        nsx.TenNuocSX
    ORDER BY SUM(ct.SoLuong) DESC;
END;
GO
-- >>> KET THUC UPGRADE G <<<

-- Cột DonGia phục vụ tính giá FIFO theo số lượng hàng đã bán.
USE QuanLyBanTuBep;
GO
IF COL_LENGTH(N'dbo.ChiTietHoaDonBan', N'DonGia') IS NULL
    ALTER TABLE dbo.ChiTietHoaDonBan ADD DonGia DECIMAL(18,2) NULL;
GO


-- ==========================================================================================
-- UPGRADE H: CHUAN HOA KHACH LE (gop tu CapNhatKhachLe.sql)
-- UPGRADE I: GO PHIEU TRA HANG (gop tu GoPhieuTraHang.sql; chi chay neu CSDL cu co chuc nang nay)
-- DU LIEU MAU BO SUNG: chay sau FRESH hoac sau cac UPGRADE phu hop.
-- Khong chay ca file mot lan; chon dung cac khoi can ap dung.
-- ==========================================================================================
-- >>> BAT DAU UPGRADE H: CHUAN HOA KHACH LE <<<
-- Khach le KL00 va chuyen hoa don cu khong co thong tin khach.
USE QuanLyBanTuBep;
GO
SET XACT_ABORT ON;

DECLARE @SoHoaDonChuyen INT = 0;
DECLARE @SoKhachXoa INT = 0;

BEGIN TRY
    BEGIN TRANSACTION;

    IF NOT EXISTS (SELECT 1 FROM dbo.KhachHang WHERE MaKhach = N'KL00')
    BEGIN
        INSERT INTO dbo.KhachHang (MaKhach, TenKhach, DiaChi, DienThoai)
        VALUES (N'KL00', N'Khách lẻ', NULL, NULL);
    END;

    UPDATE dbo.KhachHang
       SET TenKhach = N'Khách lẻ', DiaChi = NULL, DienThoai = NULL
     WHERE MaKhach = N'KL00';

    ;WITH KhachTrong AS
    (
        SELECT MaKhach
        FROM dbo.KhachHang
        WHERE MaKhach <> N'KL00'
          AND NULLIF(LTRIM(RTRIM(TenKhach)), N'') IS NULL
          AND NULLIF(LTRIM(RTRIM(DiaChi)), N'') IS NULL
          AND NULLIF(LTRIM(RTRIM(DienThoai)), N'') IS NULL
    )
    UPDATE dbo.HoaDonBan
       SET MaKhach = N'KL00'
     WHERE MaKhach IS NULL
        OR MaKhach IN (SELECT MaKhach FROM KhachTrong);

    SET @SoHoaDonChuyen = @@ROWCOUNT;

    DELETE kh
    FROM dbo.KhachHang AS kh
    WHERE kh.MaKhach <> N'KL00'
      AND NULLIF(LTRIM(RTRIM(kh.TenKhach)), N'') IS NULL
      AND NULLIF(LTRIM(RTRIM(kh.DiaChi)), N'') IS NULL
      AND NULLIF(LTRIM(RTRIM(kh.DienThoai)), N'') IS NULL
      AND NOT EXISTS (SELECT 1 FROM dbo.HoaDonBan AS hd WHERE hd.MaKhach = kh.MaKhach);

    SET @SoKhachXoa = @@ROWCOUNT;
    COMMIT TRANSACTION;

    PRINT N'Số hóa đơn đã chuyển sang KL00: ' + CONVERT(NVARCHAR(20), @SoHoaDonChuyen);
    PRINT N'Số khách trống đã xóa: ' + CONVERT(NVARCHAR(20), @SoKhachXoa);
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

-- >>> KET THUC UPGRADE H <<<
GO

-- >>> BAT DAU UPGRADE I: GO CHUC NANG PHIEU TRA HANG CU <<<

USE [QuanLyBanTuBep];
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @TonTruoc TABLE
(
    MaHang NVARCHAR(20) NOT NULL PRIMARY KEY,
    SoLuong INT NULL
);
DECLARE @TraCanGiam TABLE
(
    MaHang NVARCHAR(20) NOT NULL PRIMARY KEY,
    SoLuongCanGiam INT NOT NULL
);
DECLARE @Loi INT;
DECLARE @ThongBao NVARCHAR(4000);

BEGIN TRY
    BEGIN TRANSACTION;

    RAISERROR(N'Bước 1/7: ghi nhận tồn kho trước khi gỡ.', 10, 1) WITH NOWAIT;
    INSERT INTO @TonTruoc(MaHang, SoLuong)
        SELECT MaHang, ISNULL(SoLuong, 0) FROM dbo.DMHangHoa;
    SELECT MaHang, SoLuong FROM @TonTruoc ORDER BY MaHang;

    RAISERROR(N'Bước 2/7: xóa chi tiết phiếu trả khi trigger còn hoạt động để trừ lại tồn.', 10, 1) WITH NOWAIT;
    IF OBJECT_ID(N'dbo.ChiTietPhieuTra', N'U') IS NOT NULL
    BEGIN
        INSERT INTO @TraCanGiam(MaHang, SoLuongCanGiam)
            SELECT MaHang, SUM(SoLuong)
            FROM dbo.ChiTietPhieuTra
            GROUP BY MaHang;

        IF EXISTS (SELECT 1 FROM dbo.ChiTietPhieuTra)
           AND NOT EXISTS
           (
               SELECT 1
               FROM sys.triggers
               WHERE object_id = OBJECT_ID(N'dbo.trg_ChiTietPhieuTra_CapNhatTon', N'TR')
                 AND parent_id = OBJECT_ID(N'dbo.ChiTietPhieuTra', N'U')
                 AND is_disabled = 0
           )
        BEGIN
            ;THROW 51011, N'Có chi tiết phiếu trả nhưng trigger cộng/trừ tồn không tồn tại hoặc đang bị tắt. Không xóa dữ liệu để tránh sai tồn kho.', 1;
        END;

        DELETE FROM dbo.ChiTietPhieuTra;
    END;

    RAISERROR(N'Bước 3/7: gỡ trigger phiếu trả.', 10, 1) WITH NOWAIT;
    IF OBJECT_ID(N'dbo.trg_ChiTietPhieuTra_CapNhatTon', N'TR') IS NOT NULL
        DROP TRIGGER dbo.trg_ChiTietPhieuTra_CapNhatTon;

    RAISERROR(N'Bước 4/7: khôi phục các đối tượng FIFO và báo cáo về logic không trừ hàng trả.', 10, 1) WITH NOWAIT;
    IF OBJECT_ID(N'dbo.fn_GiaLoDangBan', N'FN') IS NOT NULL
        EXEC sys.sp_executesql N'
CREATE OR ALTER FUNCTION dbo.fn_GiaLoDangBan (@MaHang NVARCHAR(20))
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @DaBan INT = (SELECT ISNULL(SUM(SoLuong), 0) FROM dbo.ChiTietHoaDonBan WHERE MaHang = @MaHang);
    DECLARE @Gia DECIMAL(18, 2);

    SELECT TOP (1) @Gia = lo.DonGia
    FROM (
        SELECT ct.DonGia,
               SUM(ct.SoLuong) OVER (ORDER BY hd.NgayNhap, hd.SoHDN ROWS UNBOUNDED PRECEDING) AS LuyKe
        FROM dbo.ChiTietHoaDonNhap ct
        JOIN dbo.HoaDonNhap hd ON hd.SoHDN = ct.SoHDN
        WHERE ct.MaHang = @MaHang
    ) AS lo
    WHERE lo.LuyKe > @DaBan
    ORDER BY lo.LuyKe;

    IF @Gia IS NULL
        SELECT TOP (1) @Gia = ct.DonGia
        FROM dbo.ChiTietHoaDonNhap ct
        JOIN dbo.HoaDonNhap hd ON hd.SoHDN = ct.SoHDN
        WHERE ct.MaHang = @MaHang
        ORDER BY hd.NgayNhap DESC, hd.SoHDN DESC;

    RETURN @Gia;
END;';

    IF OBJECT_ID(N'dbo.fn_ConLaiLoDangBan', N'FN') IS NOT NULL
        EXEC sys.sp_executesql N'
CREATE OR ALTER FUNCTION dbo.fn_ConLaiLoDangBan (@MaHang NVARCHAR(20))
RETURNS INT
AS
BEGIN
    DECLARE @DaBan INT = (SELECT ISNULL(SUM(SoLuong), 0) FROM dbo.ChiTietHoaDonBan WHERE MaHang = @MaHang);
    DECLARE @ConLai INT;

    SELECT TOP (1) @ConLai = lo.LuyKe - @DaBan
    FROM (
        SELECT SUM(ct.SoLuong) OVER (ORDER BY hd.NgayNhap, hd.SoHDN ROWS UNBOUNDED PRECEDING) AS LuyKe
        FROM dbo.ChiTietHoaDonNhap ct
        JOIN dbo.HoaDonNhap hd ON hd.SoHDN = ct.SoHDN
        WHERE ct.MaHang = @MaHang
    ) AS lo
    WHERE lo.LuyKe > @DaBan
    ORDER BY lo.LuyKe;

    RETURN ISNULL(@ConLai, 0);
END;';

    IF OBJECT_ID(N'dbo.sp_BaoCaoTop3SanPhamTheoQuy', N'P') IS NOT NULL
        EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.sp_BaoCaoTop3SanPhamTheoQuy
    @Nam INT,
    @Quy INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (3)
        h.MaHang AS MaHang,
        h.TenHang AS TenHang,
        kt.TenKichThuoc AS TenKichThuoc,
        cl.TenChatLieu AS TenChatLieu,
        ms.TenMau AS TenMau,
        nsx.TenNuocSX AS TenNuocSX,
        SUM(ct.SoLuong) AS TongSoLuongBan,
        SUM(ct.ThanhTien) AS TongDoanhThu
    FROM dbo.ChiTietHoaDonBan AS ct
    INNER JOIN dbo.HoaDonBan AS hdb ON hdb.SoHDB = ct.SoHDB
    INNER JOIN dbo.DMHangHoa AS h ON h.MaHang = ct.MaHang
    LEFT JOIN dbo.KichThuoc AS kt ON kt.MaKichThuoc = h.MaKichThuoc
    LEFT JOIN dbo.ChatLieu AS cl ON cl.MaChatLieu = h.MaChatLieu
    LEFT JOIN dbo.MauSac AS ms ON ms.MaMau = h.MaMau
    LEFT JOIN dbo.NuocSanXuat AS nsx ON nsx.MaNuocSX = h.MaNuocSX
    WHERE YEAR(hdb.NgayBan) = @Nam
      AND DATEPART(QUARTER, hdb.NgayBan) = @Quy
    GROUP BY
        h.MaHang,
        h.TenHang,
        kt.TenKichThuoc,
        cl.TenChatLieu,
        ms.TenMau,
        nsx.TenNuocSX
    ORDER BY SUM(ct.SoLuong) DESC;
END;';

    RAISERROR(N'Bước 5/7: xóa bảng chi tiết rồi bảng phiếu trả.', 10, 1) WITH NOWAIT;
    IF OBJECT_ID(N'dbo.ChiTietPhieuTra', N'U') IS NOT NULL
        DROP TABLE dbo.ChiTietPhieuTra;
    IF OBJECT_ID(N'dbo.PhieuTraHang', N'U') IS NOT NULL
        DROP TABLE dbo.PhieuTraHang;

    RAISERROR(N'Bước 6/7: ghi nhận tồn kho sau khi gỡ và các mặt hàng có thay đổi.', 10, 1) WITH NOWAIT;
    SELECT MaHang, ISNULL(SoLuong, 0) AS SoLuong
    INTO #TonSau
    FROM dbo.DMHangHoa;

    SELECT MaHang, SoLuong FROM #TonSau ORDER BY MaHang;

    SELECT COALESCE(t.MaHang, s.MaHang) AS MaHang,
           t.SoLuong AS TonTruoc,
           s.SoLuong AS TonSau,
           s.SoLuong - t.SoLuong AS ChenhLech
    FROM @TonTruoc t
    FULL OUTER JOIN #TonSau s ON s.MaHang = t.MaHang
    WHERE ISNULL(t.SoLuong, -2147483648) <> ISNULL(s.SoLuong, -2147483648)
    ORDER BY COALESCE(t.MaHang, s.MaHang);

    DROP TABLE #TonSau;

    RAISERROR(N'Bước 7/7: hoàn tất, xác nhận thay đổi.', 10, 1) WITH NOWAIT;
    COMMIT TRANSACTION;
    PRINT N'Đã gỡ phiếu trả hàng thành công.';
END TRY
BEGIN CATCH
    SET @Loi = ERROR_NUMBER();
    SET @ThongBao = ERROR_MESSAGE();

    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;

    PRINT N'Đã ROLLBACK. Không có thay đổi nào được xác nhận.';
    PRINT N'Lỗi ' + CONVERT(NVARCHAR(20), @Loi) + N': ' + @ThongBao;

    IF @Loi = 51002
    BEGIN
        PRINT N'Mặt hàng thiếu tồn để trừ lại số lượng đã cộng khi trả:';
        SELECT t.MaHang,
               h.TenHang,
               ISNULL(s.SoLuong, 0) AS TonHienTai,
               t.SoLuongCanGiam,
               t.SoLuongCanGiam - ISNULL(s.SoLuong, 0) AS SoLuongThieu
        FROM @TraCanGiam t
        LEFT JOIN @TonTruoc s ON s.MaHang = t.MaHang
        LEFT JOIN dbo.DMHangHoa h ON h.MaHang = t.MaHang
        WHERE t.SoLuongCanGiam > ISNULL(s.SoLuong, 0)
        ORDER BY t.MaHang;
    END;

    THROW;
END CATCH;


GO

-- >>> KET THUC UPGRADE I <<<
GO

-- >>> BAT DAU DU LIEU MAU BO SUNG (chay sau FRESH / cac UPGRADE can thiet) <<<
/*
    Du lieu mau bo sung cho QuanLyBanTuBep.
    Chay sau script chinh da tao schema va trigger. Script khong xoa/sua du lieu co san.
    Thiet ke idempotent theo khoa nghiep vu (ten/so dien thoai cho danh muc, ngay + nhan vien + doi tac cho chung tu).
    Luu y: trigger trong script tong hop la logic dac thu cua du an; can chay tren ban sao DB va doi chieu cac SELECT cuoi.
*/
USE QuanLyBanTuBep;
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    /* Danh muc bo sung: bo qua ma da co; cac ma moi dung tiep so thu tu. */
    DECLARE @Next int;
    SELECT @Next = ISNULL(MAX(TRY_CONVERT(int, SUBSTRING(MaKichThuoc, 3, 18))), 0) + 1 FROM dbo.KichThuoc WHERE MaKichThuoc LIKE N'KT%';
    IF NOT EXISTS (SELECT 1 FROM dbo.KichThuoc WHERE TenKichThuoc=N'Dài 1.8m x Sâu 0.6m')
    BEGIN
      WHILE EXISTS (SELECT 1 FROM dbo.KichThuoc WHERE MaKichThuoc=N'KT'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.KichThuoc(MaKichThuoc,TenKichThuoc) VALUES(N'KT'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Dài 1.8m x Sâu 0.6m');
    END;
    SELECT @Next = ISNULL(MAX(TRY_CONVERT(int, SUBSTRING(MaKichThuoc, 3, 18))), 0) + 1 FROM dbo.KichThuoc WHERE MaKichThuoc LIKE N'KT%';
    IF NOT EXISTS (SELECT 1 FROM dbo.KichThuoc WHERE TenKichThuoc=N'Dài 2.1m x Sâu 0.6m')
    BEGIN
      WHILE EXISTS (SELECT 1 FROM dbo.KichThuoc WHERE MaKichThuoc=N'KT'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.KichThuoc(MaKichThuoc,TenKichThuoc) VALUES(N'KT'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Dài 2.1m x Sâu 0.6m');
    END;

    /* Dam bao cac bang danh muc chat lieu, mau sac, nuoc san xuat cung co du lieu mau. */
    IF NOT EXISTS(SELECT 1 FROM dbo.ChatLieu WHERE TenChatLieu=N'Gỗ công nghiệp MDF lõi xanh')
    BEGIN
      SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaChatLieu,3,18))),0)+1 FROM dbo.ChatLieu WHERE MaChatLieu LIKE N'CL%';
      WHILE EXISTS(SELECT 1 FROM dbo.ChatLieu WHERE MaChatLieu=N'CL'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.ChatLieu(MaChatLieu,TenChatLieu) VALUES(N'CL'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Gỗ công nghiệp MDF lõi xanh');
    END;
    IF NOT EXISTS(SELECT 1 FROM dbo.ChatLieu WHERE TenChatLieu=N'Gỗ Acrylic bóng gương')
    BEGIN
      SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaChatLieu,3,18))),0)+1 FROM dbo.ChatLieu WHERE MaChatLieu LIKE N'CL%';
      WHILE EXISTS(SELECT 1 FROM dbo.ChatLieu WHERE MaChatLieu=N'CL'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.ChatLieu(MaChatLieu,TenChatLieu) VALUES(N'CL'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Gỗ Acrylic bóng gương');
    END;
    IF NOT EXISTS(SELECT 1 FROM dbo.MauSac WHERE TenMau=N'Trắng sứ')
    BEGIN
      SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaMau,2,18))),0)+1 FROM dbo.MauSac WHERE MaMau LIKE N'M%';
      WHILE EXISTS(SELECT 1 FROM dbo.MauSac WHERE MaMau=N'M'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.MauSac(MaMau,TenMau) VALUES(N'M'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Trắng sứ');
    END;
    IF NOT EXISTS(SELECT 1 FROM dbo.MauSac WHERE TenMau=N'Xám ghi')
    BEGIN
      SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaMau,2,18))),0)+1 FROM dbo.MauSac WHERE MaMau LIKE N'M%';
      WHILE EXISTS(SELECT 1 FROM dbo.MauSac WHERE MaMau=N'M'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.MauSac(MaMau,TenMau) VALUES(N'M'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Xám ghi');
    END;
    IF NOT EXISTS(SELECT 1 FROM dbo.NuocSanXuat WHERE TenNuocSX=N'Việt Nam')
    BEGIN
      SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaNuocSX,4,18))),0)+1 FROM dbo.NuocSanXuat WHERE MaNuocSX LIKE N'NSX%';
      WHILE EXISTS(SELECT 1 FROM dbo.NuocSanXuat WHERE MaNuocSX=N'NSX'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.NuocSanXuat(MaNuocSX,TenNuocSX) VALUES(N'NSX'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Việt Nam');
    END;
    IF NOT EXISTS(SELECT 1 FROM dbo.NuocSanXuat WHERE TenNuocSX=N'Hàn Quốc')
    BEGIN
      SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaNuocSX,4,18))),0)+1 FROM dbo.NuocSanXuat WHERE MaNuocSX LIKE N'NSX%';
      WHILE EXISTS(SELECT 1 FROM dbo.NuocSanXuat WHERE MaNuocSX=N'NSX'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.NuocSanXuat(MaNuocSX,TenNuocSX) VALUES(N'NSX'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Hàn Quốc');
    END;

    DECLARE @KT1 nvarchar(20)=(SELECT TOP(1) MaKichThuoc FROM dbo.KichThuoc WHERE TenKichThuoc=N'Dài 1.8m x Sâu 0.6m');
    DECLARE @KT2 nvarchar(20)=(SELECT TOP(1) MaKichThuoc FROM dbo.KichThuoc WHERE TenKichThuoc=N'Dài 2.1m x Sâu 0.6m');
    DECLARE @CL1 nvarchar(20), @CL2 nvarchar(20), @M1 nvarchar(20), @NS1 nvarchar(20);
    SELECT TOP(1) @CL1=MaChatLieu FROM dbo.ChatLieu WHERE TenChatLieu=N'Gỗ công nghiệp MDF lõi xanh';
    SELECT TOP(1) @CL2=MaChatLieu FROM dbo.ChatLieu WHERE TenChatLieu=N'Gỗ Acrylic bóng gương';
    SELECT TOP(1) @M1=MaMau FROM dbo.MauSac WHERE TenMau=N'Trắng sứ';
    SELECT TOP(1) @NS1=MaNuocSX FROM dbo.NuocSanXuat WHERE TenNuocSX=N'Việt Nam';

    IF NOT EXISTS (SELECT 1 FROM dbo.CongViec WHERE TenCV=N'Nhân viên bán hàng bổ sung')
    BEGIN
      SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaCV,3,18))),0)+1 FROM dbo.CongViec WHERE MaCV LIKE N'CV%';
      WHILE EXISTS(SELECT 1 FROM dbo.CongViec WHERE MaCV=N'CV'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.CongViec(MaCV,TenCV,MucLuong) VALUES(N'CV'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Nhân viên bán hàng bổ sung',8500000);
    END;
    DECLARE @CVBan nvarchar(20)=(SELECT TOP(1) MaCV FROM dbo.CongViec WHERE TenCV=N'Nhân viên bán hàng bổ sung');
    IF NOT EXISTS (SELECT 1 FROM dbo.CongViec WHERE TenCV=N'Nhân viên kho bổ sung')
    BEGIN
      SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaCV,3,18))),0)+1 FROM dbo.CongViec WHERE MaCV LIKE N'CV%';
      WHILE EXISTS(SELECT 1 FROM dbo.CongViec WHERE MaCV=N'CV'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.CongViec(MaCV,TenCV,MucLuong) VALUES(N'CV'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Nhân viên kho bổ sung',9500000);
    END;
    DECLARE @CVKho nvarchar(20)=(SELECT TOP(1) MaCV FROM dbo.CongViec WHERE TenCV=N'Nhân viên kho bổ sung');

    IF NOT EXISTS(SELECT 1 FROM dbo.NhanVien WHERE DienThoai=N'0387654001')
    BEGIN
      SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaNV,3,18))),0)+1 FROM dbo.NhanVien WHERE MaNV LIKE N'NV%';
      WHILE EXISTS(SELECT 1 FROM dbo.NhanVien WHERE MaNV=N'NV'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.NhanVien(MaNV,TenNV,GioiTinh,NgaySinh,DienThoai,DiaChi,MaCV)
      VALUES(N'NV'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Nguyễn Hải Yến',N'Nữ','1997-04-12',N'0387654001',N'Long Biên, Hà Nội',@CVBan);
    END;
    IF NOT EXISTS(SELECT 1 FROM dbo.NhanVien WHERE DienThoai=N'0376541002')
    BEGIN
      SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaNV,3,18))),0)+1 FROM dbo.NhanVien WHERE MaNV LIKE N'NV%';
      WHILE EXISTS(SELECT 1 FROM dbo.NhanVien WHERE MaNV=N'NV'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.NhanVien(MaNV,TenNV,GioiTinh,NgaySinh,DienThoai,DiaChi,MaCV)
      VALUES(N'NV'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Phạm Đức Thành',N'Nam','1994-09-23',N'0376541002',N'Hoàng Mai, Hà Nội',@CVBan);
    END;
    IF NOT EXISTS(SELECT 1 FROM dbo.NhanVien WHERE DienThoai=N'0365432003')
    BEGIN
      SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaNV,3,18))),0)+1 FROM dbo.NhanVien WHERE MaNV LIKE N'NV%';
      WHILE EXISTS(SELECT 1 FROM dbo.NhanVien WHERE MaNV=N'NV'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.NhanVien(MaNV,TenNV,GioiTinh,NgaySinh,DienThoai,DiaChi,MaCV)
      VALUES(N'NV'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Đặng Quốc Bảo',N'Nam','1991-11-08',N'0365432003',N'Gia Lâm, Hà Nội',@CVKho);
    END;
    DECLARE @NV1 nvarchar(20)=(SELECT MaNV FROM dbo.NhanVien WHERE DienThoai=N'0387654001');
    DECLARE @NV2 nvarchar(20)=(SELECT MaNV FROM dbo.NhanVien WHERE DienThoai=N'0376541002');
    DECLARE @NVK nvarchar(20)=(SELECT MaNV FROM dbo.NhanVien WHERE DienThoai=N'0365432003');

    /* Khach hang va nha cung cap co so dien thoai gia, rieng; mot so thong tin lien he de NULL. */
    DECLARE @KH TABLE(K int PRIMARY KEY, MaKhach nvarchar(20));
    DECLARE @i int=1, @phone nvarchar(20), @name nvarchar(100), @addr nvarchar(200);
    WHILE @i<=9
    BEGIN
      SET @phone=CASE @i WHEN 1 THEN N'0391112201' WHEN 2 THEN N'0391112202' WHEN 3 THEN N'0391112203' WHEN 4 THEN N'0391112204' WHEN 5 THEN N'0391112205' WHEN 6 THEN N'0391112206' WHEN 7 THEN N'0391112207' WHEN 8 THEN N'0391112208' ELSE N'0391112209' END;
      SET @name=CASE @i WHEN 1 THEN N'Nguyễn Ngọc Mai' WHEN 2 THEN N'Trần Minh Khôi' WHEN 3 THEN N'Phạm Thu Hằng' WHEN 4 THEN N'Lê Quang Vinh' WHEN 5 THEN N'Đỗ Thanh Tâm' WHEN 6 THEN N'Vũ Hoài An' WHEN 7 THEN N'Bùi Đức Nam' WHEN 8 THEN N'Hoàng Lan Chi' ELSE N'Đặng Minh Đức' END;
      SET @addr=CASE WHEN @i=8 THEN NULL ELSE N'Hà Nội, khu vực '+CONVERT(nvarchar(10),@i) END;
      IF NOT EXISTS(SELECT 1 FROM dbo.KhachHang WHERE DienThoai=@phone)
      BEGIN
        SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaKhach,3,18))),0)+1 FROM dbo.KhachHang WHERE MaKhach LIKE N'KH%';
        WHILE EXISTS(SELECT 1 FROM dbo.KhachHang WHERE MaKhach=N'KH'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
        INSERT dbo.KhachHang(MaKhach,TenKhach,DiaChi,DienThoai) VALUES(N'KH'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),@name,@addr,@phone);
      END;
      INSERT @KH(K,MaKhach) SELECT @i,MaKhach FROM dbo.KhachHang WHERE DienThoai=@phone;
      SET @i+=1;
    END;
    IF NOT EXISTS(SELECT 1 FROM dbo.KhachHang WHERE MaKhach=N'KL00') INSERT dbo.KhachHang(MaKhach,TenKhach,DiaChi,DienThoai) VALUES(N'KL00',N'Khách lẻ',NULL,NULL);
    INSERT @KH(K,MaKhach) VALUES(0,N'KL00');

    IF NOT EXISTS(SELECT 1 FROM dbo.NhaCungCap WHERE DienThoai=N'02435550101')
    BEGIN
      SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaNCC,4,18))),0)+1 FROM dbo.NhaCungCap WHERE MaNCC LIKE N'NCC%';
      WHILE EXISTS(SELECT 1 FROM dbo.NhaCungCap WHERE MaNCC=N'NCC'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.NhaCungCap(MaNCC,TenNCC,DiaChi,DienThoai) VALUES(N'NCC'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Công ty Nội thất An Phú',N'Nam Từ Liêm, Hà Nội',N'02435550101');
    END;
    IF NOT EXISTS(SELECT 1 FROM dbo.NhaCungCap WHERE DienThoai=N'02435550102')
    BEGIN
      SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaNCC,4,18))),0)+1 FROM dbo.NhaCungCap WHERE MaNCC LIKE N'NCC%';
      WHILE EXISTS(SELECT 1 FROM dbo.NhaCungCap WHERE MaNCC=N'NCC'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
      INSERT dbo.NhaCungCap(MaNCC,TenNCC,DiaChi,DienThoai) VALUES(N'NCC'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2),N'Vật liệu bếp Minh Khang',N'Đông Anh, Hà Nội',N'02435550102');
    END;
    DECLARE @NCC1 nvarchar(20)=(SELECT MaNCC FROM dbo.NhaCungCap WHERE DienThoai=N'02435550101');
    DECLARE @NCC2 nvarchar(20)=(SELECT MaNCC FROM dbo.NhaCungCap WHERE DienThoai=N'02435550102');

    /* Tao 8 mat hang moi, SoLuong=0; Hang H la hang chua tung nhap. */
    DECLARE @P TABLE(P char(1) PRIMARY KEY, MaHang nvarchar(20), TenHang nvarchar(150), GhiChu nvarchar(500));
    INSERT @P VALUES
      ('A',NULL,N'Tủ bếp chữ L Acrylic',N'DLM-A acrylic trắng sứ'),
      ('B',NULL,N'Tủ bếp chữ L Acrylic',N'DLM-B acrylic xám ghi Hàn Quốc'),
      ('C',NULL,N'Tủ bếp MDF phủ Laminate',N'DLM-C MDF Laminate'),
      ('D',NULL,N'Tủ bếp gỗ óc chó',N'DLM-D gỗ óc chó'),
      ('E',NULL,N'Tủ bếp nhựa chống ẩm',N'DLM-E nhựa chống ẩm'),
      ('F',NULL,N'Tủ bếp Inox 304 cao cấp',N'DLM-F Inox 304'),
      ('G',NULL,N'Tủ bếp MDF phủ Veneer',N'DLM-G MDF Veneer'),
      ('H',NULL,N'Tủ bếp trưng bày thiết kế mới',N'DLM-H chưa nhập');
    UPDATE p SET MaHang=h.MaHang FROM @P p JOIN dbo.DMHangHoa h ON h.GhiChu=p.GhiChu;
    SELECT @Next=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(MaHang,3,18))),0)+1 FROM dbo.DMHangHoa WHERE MaHang LIKE N'TB%';
    DECLARE @PKey char(1), @ma nvarchar(20), @ten nvarchar(150), @gc nvarchar(500);
    DECLARE curP CURSOR LOCAL FAST_FORWARD FOR SELECT P,MaHang,TenHang,GhiChu FROM @P ORDER BY P;
    OPEN curP; FETCH NEXT FROM curP INTO @PKey,@ma,@ten,@gc;
    WHILE @@FETCH_STATUS=0
    BEGIN
      IF @ma IS NULL
      BEGIN
        WHILE EXISTS(SELECT 1 FROM dbo.DMHangHoa WHERE MaHang=N'TB'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2)) SET @Next+=1;
        SET @ma=N'TB'+RIGHT(N'00'+CONVERT(nvarchar(10),@Next),2); SET @Next+=1;
        INSERT dbo.DMHangHoa(MaHang,TenHang,MaKichThuoc,MaChatLieu,MaMau,MaNuocSX,SoLuong,GiaNhap,ThoiGianBaoHanh,Anh,GhiChu)
        VALUES(@ma,@ten,CASE WHEN @PKey IN ('A','B','D','F') THEN @KT2 ELSE @KT1 END,
          CASE WHEN @PKey IN ('A','B') THEN @CL2 ELSE @CL1 END,
          CASE WHEN @PKey='B' THEN (SELECT TOP(1) MaMau FROM dbo.MauSac WHERE TenMau=N'Xám ghi') ELSE @M1 END,
          CASE WHEN @PKey='B' THEN (SELECT TOP(1) MaNuocSX FROM dbo.NuocSanXuat WHERE TenNuocSX=N'Hàn Quốc') ELSE @NS1 END,
          0,0,CASE @PKey WHEN 'D' THEN N'60 tháng' WHEN 'F' THEN N'36 tháng' WHEN 'G' THEN N'12 tháng' ELSE N'24 tháng' END,NULL,@gc);
        UPDATE @P SET MaHang=@ma WHERE P=@PKey;
      END;
      FETCH NEXT FROM curP INTO @PKey,@ma,@ten,@gc;
    END;
    CLOSE curP; DEALLOCATE curP;

    /* 8 hoa don nhap. Ngay nhap duoc sap theo FIFO; hang A co lo nhap lui ngay truoc lo 2026-04-15. */
    DECLARE @HDN TABLE(K int PRIMARY KEY, SoHDN nvarchar(20), Ngay date, MaNV nvarchar(20), MaNCC nvarchar(20));
    INSERT @HDN VALUES
      (1,NULL,'2026-01-20',@NVK,@NCC1),(2,NULL,'2026-02-05',@NVK,@NCC2),
      (3,NULL,'2026-04-01',@NV1,@NCC1),(4,NULL,'2026-04-15',@NVK,@NCC2),
      (5,NULL,'2026-04-10',@NV1,@NCC1),(6,NULL,'2026-06-15',@NVK,@NCC2),
      (7,NULL,'2026-08-01',@NV2,@NCC1),(8,NULL,'2026-05-15',@NVK,@NCC2);
    DECLARE @maxN int; SELECT @maxN=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(SoHDN,4,18))),0) FROM dbo.HoaDonNhap WHERE SoHDN LIKE N'HDN%';
    UPDATE x SET SoHDN=h.SoHDN FROM @HDN x JOIN dbo.HoaDonNhap h ON h.NgayNhap=x.Ngay AND ISNULL(h.MaNV,N'')=ISNULL(x.MaNV,N'') AND ISNULL(h.MaNCC,N'')=ISNULL(x.MaNCC,N'');
    SET @i=1;
    WHILE @i<=8
    BEGIN
      IF NOT EXISTS(SELECT 1 FROM @HDN WHERE K=@i AND SoHDN IS NOT NULL)
      BEGIN
        SET @maxN+=1; SET @ma=N'HDN'+RIGHT(N'00'+CONVERT(nvarchar(10),@maxN),2);
        WHILE EXISTS(SELECT 1 FROM dbo.HoaDonNhap WHERE SoHDN=@ma) BEGIN SET @maxN+=1; SET @ma=N'HDN'+RIGHT(N'00'+CONVERT(nvarchar(10),@maxN),2); END;
        UPDATE @HDN SET SoHDN=@ma WHERE K=@i;
        INSERT dbo.HoaDonNhap(SoHDN,MaNV,NgayNhap,MaNCC,TongTien) SELECT SoHDN,MaNV,Ngay,MaNCC,0 FROM @HDN WHERE K=@i;
      END;
      SET @i+=1;
    END;
    DECLARE @L TABLE(K int, P char(1), Qty int, Price decimal(18,2), Disc float, PRIMARY KEY(K,P));
    INSERT @L VALUES
      (1,'A',5,10000000,0),(1,'C',2,8000000,0),(1,'D',4,25000000,5),
      (2,'B',4,15000000,0),(2,'E',5,13000000,2),
      (3,'A',2,10500000,5),(3,'F',4,28000000,0),
      (4,'A',4,12000000,0),(4,'C',5,9000000,2),(4,'G',3,6000000,0),
      (5,'B',3,16000000,0),(5,'E',2,13500000,5),
      (6,'F',2,29000000,10),(6,'G',2,6500000,0),
      (7,'C',2,9500000,0),(7,'E',1,14000000,10),
      (8,'D',1,26000000,0);
    INSERT dbo.ChiTietHoaDonNhap(SoHDN,MaHang,SoLuong,DonGia,GiamGia)
    SELECT h.SoHDN,p.MaHang,l.Qty,l.Price,l.Disc FROM @L l JOIN @HDN h ON h.K=l.K JOIN @P p ON p.P=l.P
    WHERE NOT EXISTS(SELECT 1 FROM dbo.ChiTietHoaDonNhap z WHERE z.SoHDN=h.SoHDN AND z.MaHang=p.MaHang);

    /* 20 hoa don ban (Jan-Oct), moi hoa don chi co toi da mot dong cho moi mat hang.
       So luong duoc chia nho theo FIFO; khong dong nao vuot phan con lai cua lo. */
    DECLARE @HDB TABLE(K int PRIMARY KEY, SoHDB nvarchar(20), Ngay date, MaNV nvarchar(20), KH int, MaKhach nvarchar(20));
    INSERT @HDB(K,SoHDB,Ngay,MaNV,KH,MaKhach) VALUES
      (1,NULL,'2026-01-25',@NV1,1,NULL),(2,NULL,'2026-02-12',@NV2,0,N'KL00'),
      (3,NULL,'2026-02-22',@NV1,2,NULL),(4,NULL,'2026-03-08',@NV2,0,N'KL00'),
      (5,NULL,'2026-03-25',@NV1,3,NULL),(6,NULL,'2026-04-20',@NV2,4,NULL),
      (7,NULL,'2026-05-05',@NV1,0,N'KL00'),(8,NULL,'2026-05-18',@NV2,1,NULL),
      (9,NULL,'2026-06-20',@NV1,5,NULL),(10,NULL,'2026-06-28',@NV2,0,N'KL00'),
      (11,NULL,'2026-07-10',@NV1,2,NULL),(12,NULL,'2026-07-22',@NV2,2,NULL),
      (13,NULL,'2026-08-05',@NV1,3,NULL),(14,NULL,'2026-08-19',@NV2,3,NULL),
      (15,NULL,'2026-09-02',@NV1,7,NULL),(16,NULL,'2026-09-16',@NV2,0,N'KL00'),
      (17,NULL,'2026-10-02',@NV1,1,NULL),(18,NULL,'2026-10-08',@NV2,4,NULL),
      (19,NULL,'2026-10-12',@NV1,0,N'KL00'),(20,NULL,'2026-10-18',@NV2,8,NULL);
    UPDATE s SET MaKhach=k.MaKhach FROM @HDB s JOIN @KH k ON k.K=s.KH;
    DECLARE @maxB int; SELECT @maxB=ISNULL(MAX(TRY_CONVERT(int,SUBSTRING(SoHDB,4,18))),0) FROM dbo.HoaDonBan WHERE SoHDB LIKE N'HDB%';
    UPDATE x SET SoHDB=h.SoHDB FROM @HDB x JOIN dbo.HoaDonBan h ON h.NgayBan=x.Ngay AND ISNULL(h.MaNV,N'')=ISNULL(x.MaNV,N'') AND ISNULL(h.MaKhach,N'')=ISNULL(x.MaKhach,N'');
    SET @i=1;
    WHILE @i<=20
    BEGIN
      IF NOT EXISTS(SELECT 1 FROM @HDB WHERE K=@i AND SoHDB IS NOT NULL)
      BEGIN
        SET @maxB+=1; SET @ma=N'HDB'+RIGHT(N'00'+CONVERT(nvarchar(10),@maxB),2);
        WHILE EXISTS(SELECT 1 FROM dbo.HoaDonBan WHERE SoHDB=@ma) BEGIN SET @maxB+=1; SET @ma=N'HDB'+RIGHT(N'00'+CONVERT(nvarchar(10),@maxB),2); END;
        UPDATE @HDB SET SoHDB=@ma WHERE K=@i;
        INSERT dbo.HoaDonBan(SoHDB,MaNV,NgayBan,MaKhach,TongTien) SELECT SoHDB,MaNV,Ngay,MaKhach,0 FROM @HDB WHERE K=@i;
      END;
      SET @i+=1;
    END;
    DECLARE @S TABLE(K int, P char(1), Qty int, Disc float, PRIMARY KEY(K,P));
    INSERT @S VALUES
      (1,'A',5,0),(1,'C',1,0),(2,'B',4,5),(3,'C',1,0),(4,'E',1,10),
      (5,'A',2,5),(6,'B',1,0),(7,'C',1,15),(8,'F',1,0),
      (9,'E',1,5),(9,'D',4,0),(9,'F',1,0),(10,'F',1,0),
      (11,'A',1,0),(12,'A',1,5),(13,'A',1,10),(14,'A',1,5),
      (15,'B',2,0),(16,'E',1,10),(17,'C',1,15),(18,'C',3,0),
      (19,'E',2,5),(20,'D',1,0);
    /* Chay tung hoa don theo ngay de trigger chot DonGia theo lo FIFO tai thoi diem phat sinh. */
    SET @i=1;
    WHILE @i<=20
    BEGIN
      INSERT dbo.ChiTietHoaDonBan(SoHDB,MaHang,SoLuong,GiamGia)
      SELECT h.SoHDB,p.MaHang,s.Qty,s.Disc FROM @S s JOIN @HDB h ON h.K=s.K JOIN @P p ON p.P=s.P
      WHERE s.K=@i AND NOT EXISTS(SELECT 1 FROM dbo.ChiTietHoaDonBan z WHERE z.SoHDB=h.SoHDB AND z.MaHang=p.MaHang);
      SET @i+=1;
    END;

    /* Tuy chon: danh dau hang cu het ton neu cot NgungKinhDoanh ton tai. */
    IF COL_LENGTH(N'dbo.DMHangHoa',N'NgungKinhDoanh') IS NOT NULL
      EXEC(N'UPDATE dbo.DMHangHoa SET NgungKinhDoanh=1 WHERE MaHang=N''TB04'' AND ISNULL(SoLuong,0)=0 AND EXISTS(SELECT 1 FROM dbo.ChiTietHoaDonBan WHERE MaHang=N''TB04'');');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
    DECLARE @err nvarchar(4000)=ERROR_MESSAGE();
    RAISERROR(N'Lỗi khi nạp dữ liệu mẫu bổ sung: %s',16,1,@err);
END CATCH;
GO

/* (a) Đối chiếu tồn theo chứng từ; kết quả kỳ vọng 0 dòng. */
SELECT h.MaHang,h.SoLuong AS TonHienTai,ISNULL(n.SL,0) AS TongNhap,ISNULL(b.SL,0) AS TongBan,
       ISNULL(n.SL,0)-ISNULL(b.SL,0) AS TonTheoChungTu
FROM dbo.DMHangHoa h
LEFT JOIN(SELECT MaHang,SUM(SoLuong) SL FROM dbo.ChiTietHoaDonNhap GROUP BY MaHang)n ON n.MaHang=h.MaHang
LEFT JOIN(SELECT MaHang,SUM(SoLuong) SL FROM dbo.ChiTietHoaDonBan GROUP BY MaHang)b ON b.MaHang=h.MaHang
WHERE ISNULL(h.SoLuong,0)<>ISNULL(n.SL,0)-ISNULL(b.SL,0) OR ISNULL(h.SoLuong,0)<0;

/* (b) Giá bán/giá nhập hiện hành; hàng chưa nhập được loại khỏi phép so sánh. */
SELECT MaHang,GiaNhap,DonGiaBan,ROUND(GiaNhap*1.10,2) AS GiaBanTinhLai
FROM dbo.DMHangHoa WHERE GiaNhap IS NOT NULL AND GiaNhap>0 AND ABS(DonGiaBan-ROUND(GiaNhap*1.10,2))>0.01;

/* (c) Tổng tiền chứng từ so với tổng các dòng. */
SELECT N'NHAP' AS Loai, h.SoHDN AS SoHoaDon,h.TongTien,ISNULL(SUM(ct.ThanhTien),0) AS TongDong
FROM dbo.HoaDonNhap h LEFT JOIN dbo.ChiTietHoaDonNhap ct ON ct.SoHDN=h.SoHDN
GROUP BY h.SoHDN,h.TongTien HAVING h.TongTien<>ISNULL(SUM(ct.ThanhTien),0)
UNION ALL
SELECT N'BAN',h.SoHDB,h.TongTien,ISNULL(SUM(ct.ThanhTien),0)
FROM dbo.HoaDonBan h LEFT JOIN dbo.ChiTietHoaDonBan ct ON ct.SoHDB=h.SoHDB
GROUP BY h.SoHDB,h.TongTien HAVING h.TongTien<>ISNULL(SUM(ct.ThanhTien),0);

/* (d) Hàng sắp hết và hàng tồn bằng 0. */
SELECT MaHang,TenHang,SoLuong FROM dbo.DMHangHoa WHERE SoLuong<=3 ORDER BY SoLuong,MaHang;
SELECT MaHang,TenHang,SoLuong FROM dbo.DMHangHoa WHERE SoLuong=0 ORDER BY MaHang;

/* (e) Độ phủ hóa đơn bán theo quý và theo khách. */
SELECT YEAR(NgayBan) AS Nam,DATEPART(QUARTER,NgayBan) AS Quy,COUNT(*) AS SoHoaDon
FROM dbo.HoaDonBan GROUP BY YEAR(NgayBan),DATEPART(QUARTER,NgayBan) ORDER BY Nam,Quy;
SELECT kh.MaKhach,kh.TenKhach,COUNT(h.SoHDB) AS SoHoaDon
FROM dbo.KhachHang kh LEFT JOIN dbo.HoaDonBan h ON h.MaKhach=kh.MaKhach
GROUP BY kh.MaKhach,kh.TenKhach ORDER BY SoHoaDon DESC,kh.MaKhach;

/* (f) Kiem tra nhanh cac bang trong schema deu co du lieu sau khi chay. */
SELECT N'KichThuoc' AS TenBang,COUNT_BIG(*) AS SoDong FROM dbo.KichThuoc
UNION ALL SELECT N'ChatLieu',COUNT_BIG(*) FROM dbo.ChatLieu
UNION ALL SELECT N'NuocSanXuat',COUNT_BIG(*) FROM dbo.NuocSanXuat
UNION ALL SELECT N'MauSac',COUNT_BIG(*) FROM dbo.MauSac
UNION ALL SELECT N'CongViec',COUNT_BIG(*) FROM dbo.CongViec
UNION ALL SELECT N'KhachHang',COUNT_BIG(*) FROM dbo.KhachHang
UNION ALL SELECT N'NhaCungCap',COUNT_BIG(*) FROM dbo.NhaCungCap
UNION ALL SELECT N'NhanVien',COUNT_BIG(*) FROM dbo.NhanVien
UNION ALL SELECT N'DMHangHoa',COUNT_BIG(*) FROM dbo.DMHangHoa
UNION ALL SELECT N'HoaDonNhap',COUNT_BIG(*) FROM dbo.HoaDonNhap
UNION ALL SELECT N'ChiTietHoaDonNhap',COUNT_BIG(*) FROM dbo.ChiTietHoaDonNhap
UNION ALL SELECT N'HoaDonBan',COUNT_BIG(*) FROM dbo.HoaDonBan
UNION ALL SELECT N'ChiTietHoaDonBan',COUNT_BIG(*) FROM dbo.ChiTietHoaDonBan;
GO
-- >>> KET THUC DU LIEU MAU BO SUNG <<<


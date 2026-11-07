/*
  Chay thu cong trong database QuanLyBanTuBep sau khi sao luu.
  Script chi tao bang moi, khong sua/xoa du lieu hien co.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;

IF DB_NAME() <> N'QuanLyBanTuBep'
    THROW 51000, N'Hay chon dung database QuanLyBanTuBep truoc khi chay script.', 1;

IF OBJECT_ID(N'dbo.NhanVien', N'U') IS NULL
    THROW 51001, N'Khong tim thay bang dbo.NhanVien.', 1;

/* Doi chieu schema nguon hien tai: MaNV NVARCHAR(20), NOT NULL, co khoa chinh. */
IF NOT EXISTS (
    SELECT 1 FROM sys.columns c
    JOIN sys.types t ON c.user_type_id=t.user_type_id
    WHERE c.object_id=OBJECT_ID(N'dbo.NhanVien') AND c.name=N'MaNV'
      AND t.name=N'nvarchar' AND c.max_length=40 AND c.is_nullable=0
)
    THROW 51002, N'dbo.NhanVien.MaNV khong khop NVARCHAR(20) NOT NULL; dung lai va kiem tra schema that.', 1;

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes i
    JOIN sys.index_columns ic ON i.object_id=ic.object_id AND i.index_id=ic.index_id
    JOIN sys.columns c ON c.object_id=ic.object_id AND c.column_id=ic.column_id
    WHERE i.object_id=OBJECT_ID(N'dbo.NhanVien') AND i.is_primary_key=1
      AND c.name=N'MaNV' AND ic.key_ordinal=1
)
    THROW 51003, N'MaNV chua duoc xac nhan la khoa chinh cua NhanVien.', 1;

IF OBJECT_ID(N'dbo.TaiKhoan', N'U') IS NOT NULL
    THROW 51004, N'Bang dbo.TaiKhoan da ton tai; script khong ghi de schema hien co.', 1;

BEGIN TRANSACTION;
CREATE TABLE dbo.TaiKhoan
(
    MaTaiKhoan INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_TaiKhoan PRIMARY KEY,
    MaNV NVARCHAR(20) NOT NULL,
    TenDangNhap NVARCHAR(100) NOT NULL,
    MatKhauHash NVARCHAR(500) NOT NULL,
    VaiTro NVARCHAR(10) NOT NULL,
    TrangThai BIT NOT NULL CONSTRAINT DF_TaiKhoan_TrangThai DEFAULT (1),
    NgayTao DATETIME2(0) NOT NULL CONSTRAINT DF_TaiKhoan_NgayTao DEFAULT (SYSUTCDATETIME()),
    LanDangNhapCuoi DATETIME2(0) NULL,
    CONSTRAINT UQ_TaiKhoan_MaNV UNIQUE (MaNV),
    CONSTRAINT UQ_TaiKhoan_TenDangNhap UNIQUE (TenDangNhap),
    CONSTRAINT CK_TaiKhoan_VaiTro CHECK (VaiTro IN (N'ADMIN',N'STAFF')),
    CONSTRAINT FK_TaiKhoan_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV)
);
COMMIT TRANSACTION;
GO

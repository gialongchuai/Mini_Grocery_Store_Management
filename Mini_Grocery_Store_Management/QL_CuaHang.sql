create database QL_CuaHang
use QL_CuaHang

-- Tạo bảng DanhMuc
CREATE TABLE DanhMuc (
    MaDanhMuc INT PRIMARY KEY NOT NULL,
    TenDanhMuc NVARCHAR(50) NOT NULL,
    MoTaDanhMuc NVARCHAR(100) NULL
);
GO

-- Tạo bảng NguoiDung
CREATE TABLE NguoiDung (
    MaNguoiDung INT PRIMARY KEY NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(20) NULL,
    DiaChi NVARCHAR(255) NULL,
    TenDangNhap NVARCHAR(50) NOT NULL,
    MatKhau NVARCHAR(50) NOT NULL,
    VaiTro NVARCHAR(50) NOT NULL,
    CONSTRAINT UQ_TenDangNhap UNIQUE (TenDangNhap)
);
GO

-- Tạo bảng SanPham
CREATE TABLE SanPham (
    MaSanPham INT PRIMARY KEY NOT NULL,
    TenSanPham NVARCHAR(50) NOT NULL,
    MoTaSanPham NVARCHAR(100) NULL,
    GiaSanPham MONEY NOT NULL,
    SoLuongSanPham INT NOT NULL,
    MaDanhMuc INT,
    FOREIGN KEY (MaDanhMuc) REFERENCES DanhMuc(MaDanhMuc)
);
GO

-- Tạo bảng HoaDon
CREATE TABLE HoaDon (
    MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    NgayBan DATETIME NOT NULL,
    TongSoTien MONEY NOT NULL,
    TrangThai NVARCHAR(50) NOT NULL,
    MaNguoiDung INT,
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);
GO

-- Tạo bảng ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    MaChiTiet INT IDENTITY(1,1) PRIMARY KEY,
    MaHoaDon INT,
    MaSanPham INT,
    SoLuong INT NOT NULL,
    GiaBan MONEY NOT NULL,
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham)
);
GO


-- Chèn dữ liệu vào bảng DanhMuc
INSERT INTO DanhMuc (MaDanhMuc, TenDanhMuc, MoTaDanhMuc) VALUES
(1, N'Tiêu dùng', N'Danh mục sản phẩm tiêu dùng hàng ngày'),
(2, N'Đồ ăn', N'Danh mục sản phẩm thực phẩm'),
(3, N'Trang điểm', N'Danh mục sản phẩm mỹ phẩm'),
(4, N'Điện tử', N'Danh mục sản phẩm điện tử'),
(5, N'Quần áo', N'Danh mục sản phẩm thời trang');
GO

-- Chèn dữ liệu vào bảng NguoiDung
INSERT INTO NguoiDung (MaNguoiDung, HoTen, Email, SoDienThoai, DiaChi, TenDangNhap, MatKhau, VaiTro) VALUES
(1, N'Tran Van Teo', N'admin@example.com', N'123456789', N'Ha Noi', N'admin', N'password123', N'Quản trị viên'),
(2, N'Nguyen Van Teo', N'user1@example.com', N'987654321', N'TP.HCM', N'user1', N'userpass', N'Người dùng'),
(3, N'Le Van Teo', N'user2@example.com', N'555555555', N'Hai Phong', N'user2', N'pass123', N'Người dùng');
GO

-- Chèn dữ liệu vào bảng SanPham
INSERT INTO SanPham (MaSanPham, TenSanPham, MoTaSanPham, GiaSanPham, SoLuongSanPham, MaDanhMuc) VALUES
(1, N'Gạo lứt', N'Gạo lứt sạch', 20000, 100, 2),
(2, N'Kem chống nắng', N'Kem chống nắng SPF 30', 35000, 50, 3),
(3, N'Tivi LED 42 inch', N'Tivi LED 42 inch Full HD', 6000000, 10, 4),
(4, N'Áo thun nam', N'Áo thun nam size M', 150000, 30, 5),
(5, N'Chả cá', N'Chả cá tươi ngon', 25000, 40, 2);
GO

-- Chèn dữ liệu vào bảng HoaDon
INSERT INTO HoaDon (NgayBan, TongSoTien, TrangThai, MaNguoiDung) VALUES
('2023-11-09 10:30:00', 80000, N'Đã thanh toán', 2),
('2023-11-09 11:45:00', 155000, N'Đã thanh toán', 3),
('2023-11-08 14:15:00', 3500000, N'Đã thanh toán', 1),
('2023-11-08 15:20:00', 120000, N'Đã thanh toán', 2);
GO

-- Chèn dữ liệu vào bảng ChiTietHoaDon
INSERT INTO ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, GiaBan) VALUES
(1, 2, 2, 35000),
(1, 5, 3, 75000),
(2, 4, 1, 150000),
(3, 3, 2, 1200000),
(4, 1, 5, 100000);
GO

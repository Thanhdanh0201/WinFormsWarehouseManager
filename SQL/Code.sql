CREATE DATABASE QuanLy_KhoHang;
GO
USE QuanLy_KhoHang;
GO

-- =============================================
-- 1. TẠO BẢNG (TABLES)
-- =============================================

-- Bảng User (Admins)
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    BirthDate DATE,
    Email VARCHAR(100) UNIQUE NOT NULL,
    MailboxPassword VARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Bảng Danh mục
CREATE TABLE Categories (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
    HanTonKho_Thang INT -- Số tháng được phép tồn kho
);

-- Bảng Nhà cung cấp
CREATE TABLE Suppliers (
    SupplierID INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName NVARCHAR(150) NOT NULL,
    Email VARCHAR(100),
    Phone VARCHAR(20),
    Address NVARCHAR(255)
);

-- Bảng Người nhận hàng
CREATE TABLE Receivers (
    ReceiverID INT IDENTITY(1,1) PRIMARY KEY,
    ReceiverName NVARCHAR(150) NOT NULL,
    Email VARCHAR(100),
    Phone VARCHAR(20)
);

-- Bảng Sản phẩm
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(150) NOT NULL,
    SoLuong INT DEFAULT 0, -- Tồn kho hiện tại
    DonViTinh NVARCHAR(20),
    HanSuDung DATE,
    NgayNhapKho DATE DEFAULT GETDATE(),
    CategoryID INT,
    SupplierID INT,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID)
);

-- Bảng Hóa đơn nhập
CREATE TABLE ImportReceipts (
    ImportID INT IDENTITY(1,1) PRIMARY KEY,
    ImportDate DATETIME DEFAULT GETDATE(),
    UserID INT,
    SupplierID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID)
);

-- Bảng Chi tiết nhập
CREATE TABLE ImportReceiptDetails (
    ImportDetailID INT IDENTITY(1,1) PRIMARY KEY,
    ImportID INT,
    ProductID INT,
    Quantity INT CHECK (Quantity > 0),
    FOREIGN KEY (ImportID) REFERENCES ImportReceipts(ImportID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Bảng Hóa đơn xuất
CREATE TABLE ExportReceipts (
    ExportID INT IDENTITY(1,1) PRIMARY KEY,
    ExportDate DATETIME DEFAULT GETDATE(),
    UserID INT,
    ReceiverID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (ReceiverID) REFERENCES Receivers(ReceiverID)
);

-- Bảng Chi tiết xuất
CREATE TABLE ExportReceiptDetails (
    ExportDetailID INT IDENTITY(1,1) PRIMARY KEY,
    ExportID INT,
    ProductID INT,
    Quantity INT CHECK (Quantity > 0),
    FOREIGN KEY (ExportID) REFERENCES ExportReceipts(ExportID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Bảng Lịch sử hoạt động
CREATE TABLE ActivityLog (
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    LoaiHanhDong NVARCHAR(50),
    Description NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UserID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Bảng Thông báo
CREATE TABLE Notifications (
    NotiID INT IDENTITY(1,1) PRIMARY KEY,
    LoaiThongBao NVARCHAR(100),
    MoTa NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsRead BIT DEFAULT 0,
    UserID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO
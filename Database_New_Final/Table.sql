-- =============================================
-- 1. TẠO BẢNG
-- =============================================

-- Bảng User
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
    HanTonKho_Thang INT
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

-- Bảng NHẬP KHO (Đã gộp chi tiết vào)
CREATE TABLE ImportReceipts (
    ImportID INT IDENTITY(1,1) PRIMARY KEY,
    ImportDate DATETIME DEFAULT GETDATE(),
    UserID INT REFERENCES Users(UserID),
    SupplierID INT REFERENCES Suppliers(SupplierID),
    
    -- Các cột chi tiết đưa lên đây
    ProductID INT REFERENCES Products(ProductID),
    Quantity INT CHECK (Quantity > 0)
);

-- Bảng XUẤT KHO (Đã gộp chi tiết vào)
CREATE TABLE ExportReceipts (
    ExportID INT IDENTITY(1,1) PRIMARY KEY,
    ExportDate DATETIME DEFAULT GETDATE(),
    UserID INT REFERENCES Users(UserID),
    ReceiverID INT REFERENCES Receivers(ReceiverID),

    -- Các cột chi tiết đưa lên đây
    ProductID INT REFERENCES Products(ProductID),
    Quantity INT CHECK (Quantity > 0)
);

-- Bảng Lịch sử hoạt động
CREATE TABLE ActivityLog (
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    LoaiHanhDong NVARCHAR(50),
    Description NVARCHAR(MAX),
    TableName VARCHAR(50), 
    RecordID INT,          
    CreatedAt DATETIME DEFAULT GETDATE(),
    UserID INT REFERENCES Users(UserID)
);

-- Bảng Thông báo
CREATE TABLE Notifications (
    NotiID INT IDENTITY(1,1) PRIMARY KEY,
    LoaiThongBao NVARCHAR(100),
    MoTa NVARCHAR(MAX),
    RelatedTable VARCHAR(50), 
    RelatedID INT,            
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsRead BIT DEFAULT 0,
    UserID INT REFERENCES Users(UserID)
);
GO

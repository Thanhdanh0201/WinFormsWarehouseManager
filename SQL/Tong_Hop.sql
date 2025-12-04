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

-- =============================================
-- 2. TẠO TRIGGERS & PROCEDURES
-- =============================================

-- Trigger Nhập kho
CREATE TRIGGER trg_AfterImportDetail_Insert
ON ImportReceiptDetails
AFTER INSERT
AS
BEGIN
    DECLARE @ImportID INT, @ProductID INT, @Qty INT, @UserID INT, @CatID INT, @CurrentTotalStock INT;
    
    SELECT @ImportID = i.ImportID, @ProductID = i.ProductID, @Qty = i.Quantity FROM inserted i;
    SELECT @UserID = UserID FROM ImportReceipts WHERE ImportID = @ImportID;
    SELECT @CatID = CategoryID FROM Products WHERE ProductID = @ProductID;

    -- Kiểm tra giới hạn 100 sản phẩm/kho
    SELECT @CurrentTotalStock = SUM(SoLuong) FROM Products WHERE CategoryID = @CatID;

    IF (ISNULL(@CurrentTotalStock, 0) + @Qty) > 100
    BEGIN
        RAISERROR(N'Lỗi: Kho hàng (Danh mục) này đã đầy (Tối đa 100 sản phẩm). Không thể nhập thêm.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Cập nhật số lượng
    UPDATE Products
    SET SoLuong = SoLuong + @Qty, NgayNhapKho = GETDATE()
    WHERE ProductID = @ProductID;

    -- Ghi Log
    INSERT INTO ActivityLog (LoaiHanhDong, Description, UserID)
    VALUES (N'Nhập kho', N'Nhập ' + CAST(@Qty AS NVARCHAR) + N' sản phẩm ID: ' + CAST(@ProductID AS NVARCHAR) + N' vào phiếu ' + CAST(@ImportID AS NVARCHAR), @UserID);
END;
GO

-- Trigger Xuất kho
CREATE TRIGGER trg_AfterExportDetail_Insert
ON ExportReceiptDetails
AFTER INSERT
AS
BEGIN
    DECLARE @ExportID INT, @ProductID INT, @Qty INT, @UserID INT, @CurrentStock INT;
    
    SELECT @ExportID = i.ExportID, @ProductID = i.ProductID, @Qty = i.Quantity FROM inserted i;
    SELECT @UserID = UserID FROM ExportReceipts WHERE ExportID = @ExportID;

    -- Kiểm tra tồn kho
    SELECT @CurrentStock = SoLuong FROM Products WHERE ProductID = @ProductID;
    IF @CurrentStock < @Qty
    BEGIN
        RAISERROR(N'Lỗi: Số lượng tồn kho không đủ để xuất.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Cập nhật số lượng
    UPDATE Products SET SoLuong = SoLuong - @Qty WHERE ProductID = @ProductID;

    -- Ghi Log
    INSERT INTO ActivityLog (LoaiHanhDong, Description, UserID)
    VALUES (N'Xuất kho', N'Xuất ' + CAST(@Qty AS NVARCHAR) + N' sản phẩm ID: ' + CAST(@ProductID AS NVARCHAR) + N' khỏi phiếu ' + CAST(@ExportID AS NVARCHAR), @UserID);
END;
GO

-- Procedure Thông báo
CREATE PROCEDURE sp_CheckAndCreateNotifications
AS
BEGIN
    -- Hết hạn sử dụng
    INSERT INTO Notifications (LoaiThongBao, MoTa, UserID)
    SELECT N'Hết hạn sử dụng', N'Sản phẩm ' + p.ProductName + N' (ID: ' + CAST(p.ProductID AS NVARCHAR) + N') đã hết hạn.', u.UserID
    FROM Products p, Users u WHERE p.HanSuDung < GETDATE() AND p.SoLuong > 0;

    -- Quá hạn tồn kho
    INSERT INTO Notifications (LoaiThongBao, MoTa, UserID)
    SELECT N'Quá hạn tồn kho', N'Sản phẩm ' + p.ProductName + N' đã lưu kho quá hạn.', u.UserID
    FROM Products p JOIN Categories c ON p.CategoryID = c.CategoryID, Users u
    WHERE DATEADD(MONTH, c.HanTonKho_Thang, p.NgayNhapKho) < GETDATE() AND p.SoLuong > 0;
    
    -- Sắp hết hàng
    INSERT INTO Notifications (LoaiThongBao, MoTa, UserID)
    SELECT N'Cảnh báo số lượng', N'Sản phẩm ' + p.ProductName + N' sắp hết (còn ' + CAST(p.SoLuong AS NVARCHAR) + ').', u.UserID
    FROM Products p, Users u WHERE p.SoLuong <= 5 AND p.SoLuong > 0;
END;
GO

-- =============================================
-- 3. CHÈN DỮ LIỆU MẪU (SEEDING DATA)
-- =============================================

-- 6 Danh mục
INSERT INTO Categories (CategoryName, HanTonKho_Thang) VALUES 
(N'Thực phẩm', 6), (N'Linh kiện điện tử', 24), (N'Đồ gia dụng', 24),
(N'Mỹ phẩm', 6), (N'Vật liệu xây dựng', 3), (N'Đồ dùng văn phòng', 6);

-- 24 Nhà cung cấp (Chia đều 6 nhóm)
-- Nhóm 1: Thực phẩm
INSERT INTO Suppliers (SupplierName, Email, Phone, Address) VALUES 
(N'Công Ty CP Acecook Việt Nam', 'contact@acecook.vn', '02838154064', N'KCN Tân Bình, TP.HCM'),
(N'Công Ty CP Hàng Tiêu Dùng Masan', 'cskh@masan.com', '02862563862', N'Quận 1, TP.HCM'),
(N'Công Ty TNHH Nước Giải Khát Suntory PepsiCo', 'sales@pepsico.vn', '02839105421', N'Quận 1, TP.HCM'),
(N'Công Ty CP Sữa Việt Nam (Vinamilk)', 'vinamilk@vinamilk.com.vn', '02854155555', N'Quận 7, TP.HCM');

-- Nhóm 2: Linh kiện điện tử
INSERT INTO Suppliers (SupplierName, Email, Phone, Address) VALUES 
(N'Công Ty CP Thế Giới Số (DigiWorld)', 'support@digiworld.com.vn', '02839290059', N'Quận 3, TP.HCM'),
(N'Công Ty TNHH Phân Phối FPT', 'phanphoi@fpt.com.vn', '02473008888', N'Cầu Giấy, Hà Nội'),
(N'Công Ty TNHH Công Nghệ Viễn Thông Lê Bảo Minh', 'canon@lebaominh.com.vn', '02838386666', N'Quận 1, TP.HCM'),
(N'Công Ty TNHH Máy Tính Phong Vũ', 'sales@phongvu.vn', '18006867', N'Quận 1, TP.HCM');

-- Nhóm 3: Đồ gia dụng
INSERT INTO Suppliers (SupplierName, Email, Phone, Address) VALUES 
(N'Tập Đoàn Sunhouse', 'cskh@sunhouse.com.vn', '18006680', N'Cầu Giấy, Hà Nội'),
(N'Công Ty TNHH Lock&Lock HCM', 'sales@locknlock.vn', '02854135750', N'Quận 7, TP.HCM'),
(N'Tập Đoàn Kangaroo', 'info@kangaroo.vn', '1900555566', N'Đống Đa, Hà Nội'),
(N'Công Ty Cổ Phần Elmich', 'cskh@elmich.vn', '1900636925', N'Thanh Xuân, Hà Nội');

-- Nhóm 4: Mỹ phẩm
INSERT INTO Suppliers (SupplierName, Email, Phone, Address) VALUES 
(N'Công Ty TNHH L''Oreal Việt Nam', 'contact@loreal.vn', '02839369142', N'Quận 1, TP.HCM'),
(N'Công Ty Unilever Việt Nam', 'info@unilever.com.vn', '02838123456', N'Củ Chi, TP.HCM'),
(N'Công Ty Rohto-Mentholatum Việt Nam', 'enquiry@rohto.com.vn', '02838229322', N'Quận 1, TP.HCM'),
(N'Chuỗi Mỹ Phẩm Hasaki Beauty', 'partner@hasaki.vn', '18006324', N'Quận 10, TP.HCM');

-- Nhóm 5: Vật liệu xây dựng
INSERT INTO Suppliers (SupplierName, Email, Phone, Address) VALUES 
(N'Tập Đoàn Hoa Sen', 'bantonthep@hoasengroup.vn', '02837462988', N'Dĩ An, Bình Dương'),
(N'Công Ty Thép Hòa Phát', 'sales@hoaphat.com.vn', '02462848666', N'Hai Bà Trưng, Hà Nội'),
(N'Công Ty Xi Măng Vicem Hà Tiên', 'marketing@hatien1.com.vn', '02838966605', N'Thủ Đức, TP.HCM'),
(N'Công Ty Sơn AkzoNobel (Dulux)', 'duluxvn@akzonobel.com', '1900555561', N'Quận 1, TP.HCM');

-- Nhóm 6: Đồ dùng văn phòng
INSERT INTO Suppliers (SupplierName, Email, Phone, Address) VALUES 
(N'Tập Đoàn Thiên Long', 'info@thienlonggroup.com', '02837505555', N'Tân Tạo, TP.HCM'),
(N'Văn Phòng Phẩm Hồng Hà', 'banhang@vpphongha.com.vn', '02436524250', N'Hoàn Kiếm, Hà Nội'),
(N'Công Ty Giấy Sài Gòn', 'sales@saigonpaper.com', '02862884333', N'KCN Mỹ Xuân A, Vũng Tàu'),
(N'Công Ty Double A Việt Nam', 'service@doublea.com.vn', '02838235555', N'Quận 3, TP.HCM');

-- 7 Người nhận hàng
INSERT INTO Receivers (ReceiverName, Email, Phone) VALUES 
(N'Nguyễn Văn A', 'nguyenvana@gmail.com', '0911111111'), (N'Trần Thị B', 'tranthib@gmail.com', '0922222222'),
(N'Phạm Văn C', 'phamvanc@gmail.com', '0933333333'), (N'Lê Thị D', 'lethid@gmail.com', '0944444444'),
(N'Hoàng Văn E', 'hoangvane@gmail.com', '0955555555'), (N'Đỗ Thị F', 'dothif@gmail.com', '0966666666'),
(N'Vũ Văn G', 'vuvang@gmail.com', '0977777777');

-- 300 SẢN PHẨM MẪU (Sử dụng bảng tạm)
DECLARE @ProductList TABLE (CatID INT, BaseName NVARCHAR(100), Variant NVARCHAR(50), Unit NVARCHAR(20), ShelfLifeMonths INT);

-- Kho 1: Thực phẩm
INSERT INTO @ProductList VALUES 
(1, N'Gạo ST25', N'5kg', N'Túi', 6), (1, N'Gạo Nàng Thơm', N'10kg', N'Túi', 6),
(1, N'Mì Hảo Hảo', N'Thùng', N'Thùng', 6), (1, N'Mì Omachi', N'Ly', N'Ly', 6),
(1, N'Nước mắm Nam Ngư', N'500ml', N'Chai', 12), (1, N'Nước mắm Phú Quốc', N'750ml', N'Chai', 12),
(1, N'Dầu ăn Tường An', N'1L', N'Can', 12), (1, N'Dầu ăn Neptune', N'2L', N'Can', 12),
(1, N'Đường Biên Hòa', N'1kg', N'Gói', 24), (1, N'Đường phèn', N'500g', N'Gói', 24),
(1, N'Muối I-ốt', N'500g', N'Gói', 36), (1, N'Bột ngọt Vedan', N'454g', N'Gói', 36),
(1, N'Hạt nêm Knorr', N'400g', N'Gói', 12), (1, N'Hạt nêm Knorr', N'900g', N'Gói', 12),
(1, N'Sữa đặc Ông Thọ', N'Lon', N'Lon', 12), (1, N'Sữa đặc Ông Thọ', N'Tuýp', N'Tuýp', 12),
(1, N'Sữa tươi Vinamilk', N'1L Có đường', N'Hộp', 6), (1, N'Sữa tươi TH', N'1L', N'Hộp', 6),
(1, N'Sữa chua Vinamilk', N'Lốc 4', N'Lốc', 1), (1, N'Cá mòi 3 Cô Gái', N'155g', N'Lon', 24),
(1, N'Xúc xích Vissan', N'Gói 5', N'Gói', 3), (1, N'Chả lụa Vissan', N'500g', N'Cây', 1),
(1, N'Bánh Chocopie', N'Hộp 12', N'Hộp', 12), (1, N'Bánh Custas', N'Hộp 6', N'Hộp', 12),
(1, N'Bánh quy Cosy', N'Gói lớn', N'Gói', 12), (1, N'Snack Oishi', N'Tôm', N'Gói', 6),
(1, N'Cafe G7', N'Bịch 50', N'Bịch', 24), (1, N'Cafe Phố', N'Hộp 10', N'Hộp', 24),
(1, N'Trà Lipton', N'25 túi', N'Hộp', 24), (1, N'Trà xanh 0 độ', N'500ml', N'Chai', 6),
(1, N'Coca Cola', N'330ml', N'Lon', 12), (1, N'Pepsi', N'330ml', N'Lon', 12),
(1, N'7Up', N'330ml', N'Lon', 12), (1, N'Bia Tiger', N'Thùng', N'Thùng', 12),
(1, N'Bia Heineken', N'Thùng', N'Thùng', 12), (1, N'Tương ớt Chinsu', N'250g', N'Chai', 12),
(1, N'Tương cà Cholimex', N'250g', N'Chai', 12), (1, N'Ngũ cốc', N'20 gói', N'Bịch', 12),
(1, N'Mật ong', N'500ml', N'Chai', 36), (1, N'Nước suối Aquafina', N'500ml', N'Chai', 12),
(1, N'Nước suối Lavie', N'500ml', N'Chai', 12), (1, N'Bơ thực vật', N'Tường An', N'Hộp', 12),
(1, N'Phô mai con bò cười', N'Hộp 8', N'Hộp', 6), (1, N'Trứng gà', N'Vỉ 10', N'Vỉ', 1),
(1, N'Trứng vịt', N'Vỉ 10', N'Vỉ', 1), (1, N'Mì chính Ajinomoto', N'1kg', N'Gói', 36),
(1, N'Bột chiên giòn', N'AjiQuick', N'Gói', 12), (1, N'Sate tôm', N'Hũ', N'Hũ', 24),
(1, N'Tiêu đen', N'Xay', N'Hũ', 24), (1, N'Tỏi phi', N'100g', N'Hũ', 12);

-- Kho 2: Điện tử
INSERT INTO @ProductList VALUES 
(2, N'CPU Intel', N'i3 12100F', N'Cái', 36), (2, N'CPU Intel', N'i5 12400F', N'Cái', 36),
(2, N'CPU AMD', N'Ryzen 5 5600', N'Cái', 36), (2, N'Mainboard Asus', N'H610M', N'Cái', 36),
(2, N'Mainboard Giga', N'B660M', N'Cái', 36), (2, N'RAM Kingston', N'8GB D4', N'Thanh', 36),
(2, N'RAM Corsair', N'16GB D4', N'Thanh', 36), (2, N'SSD Samsung', N'500GB NVMe', N'Cái', 36),
(2, N'SSD Kingston', N'240GB Sata', N'Cái', 36), (2, N'HDD WD', N'1TB Blue', N'Cái', 36),
(2, N'VGA RTX', N'3060 12GB', N'Cái', 36), (2, N'VGA RTX', N'4060 8GB', N'Cái', 36),
(2, N'Nguồn Corsair', N'650W', N'Cái', 36), (2, N'Case Xigmatek', N'Gaming X', N'Cái', 36),
(2, N'Màn hình Dell', N'24 inch', N'Cái', 36), (2, N'Màn hình LG', N'27 inch', N'Cái', 36),
(2, N'Chuột Logitech', N'G102', N'Cái', 24), (2, N'Phím cơ DareU', N'EK87', N'Cái', 24),
(2, N'Tai nghe HyperX', N'Cloud II', N'Cái', 24), (2, N'Loa SoundMax', N'A2100', N'Bộ', 24),
(2, N'Webcam Logitech', N'C920', N'Cái', 24), (2, N'USB Kingston', N'32GB', N'Cái', 24),
(2, N'Thẻ nhớ', N'64GB', N'Cái', 24), (2, N'Cáp HDMI', N'1.5m', N'Sợi', 24),
(2, N'Cáp LAN', N'Cat6 5m', N'Sợi', 24), (2, N'Pin CMOS', N'CR2032', N'Viên', 24),
(2, N'Keo tản nhiệt', N'MX4', N'Tuýp', 12), (2, N'Quạt Case', N'12cm RGB', N'Cái', 24),
(2, N'Tản nhiệt khí', N'CR1000', N'Cái', 36), (2, N'Raspberry Pi', N'Pi 4', N'Cái', 24),
(2, N'Arduino', N'Uno R3', N'Cái', 24), (2, N'ESP8266', N'NodeMCU', N'Cái', 24),
(2, N'Cảm biến', N'DHT11', N'Cái', 12), (2, N'Điện trở', N'Gói', N'Gói', 24),
(2, N'Tụ điện', N'Gói', N'Gói', 24), (2, N'Router Wifi', N'TP-Link', N'Cái', 24),
(2, N'Switch', N'5 Port', N'Cái', 24), (2, N'Ổ cắm Lioa', N'6 lỗ', N'Cái', 24),
(2, N'Pin sạc dự phòng', N'10000mAh', N'Cái', 24), (2, N'Cáp sạc iPhone', N'Lightning', N'Sợi', 12),
(2, N'Cáp sạc Type-C', N'1m', N'Sợi', 12), (2, N'Củ sạc', N'20W', N'Cái', 24),
(2, N'Lót chuột', N'Size L', N'Cái', 12), (2, N'Giá đỡ VGA', N'Led', N'Cái', 24),
(2, N'Dây rút', N'Bịch', N'Bịch', 60), (2, N'Băng keo điện', N'Đen', N'Cuộn', 24),
(2, N'Kìm bấm mạng', N'Tốt', N'Cái', 36), (2, N'Máy hút bụi PC', N'Mini', N'Cái', 24),
(2, N'Bộ vệ sinh màn', N'3 món', N'Bộ', 12), (2, N'Mic thu âm', N'USB', N'Cái', 24);

-- Kho 3: Gia dụng
INSERT INTO @ProductList VALUES 
(3, N'Nồi cơm Sharp', N'1.8L', N'Cái', 24), (3, N'Chảo Sunhouse', N'24cm', N'Cái', 24),
(3, N'Bộ nồi Inox', N'3 cái', N'Bộ', 36), (3, N'Ấm siêu tốc', N'1.8L', N'Cái', 24),
(3, N'Bếp từ', N'Kangaroo', N'Cái', 24), (3, N'Máy xay sinh tố', N'Panasonic', N'Cái', 24),
(3, N'Máy ép', N'Philips', N'Cái', 24), (3, N'Lò vi sóng', N'20L', N'Cái', 24),
(3, N'Nồi chiên', N'5L', N'Cái', 24), (3, N'Bàn ủi', N'Hơi nước', N'Cái', 24),
(3, N'Máy sấy tóc', N'1800W', N'Cái', 24), (3, N'Quạt đứng', N'Senko', N'Cái', 24),
(3, N'Quạt treo', N'Asia', N'Cái', 24), (3, N'Bình giữ nhiệt', N'500ml', N'Cái', 36),
(3, N'Hộp cơm', N'Giữ nhiệt', N'Bộ', 24), (3, N'Bộ dao', N'6 món', N'Bộ', 36),
(3, N'Thớt gỗ', N'30cm', N'Cái', 36), (3, N'Kệ chén', N'Inox', N'Cái', 36),
(3, N'Rổ nhựa', N'Bộ 3', N'Bộ', 24), (3, N'Thau Inox', N'40cm', N'Cái', 36),
(3, N'Chén sứ', N'Chục', N'Chục', 48), (3, N'Dĩa sứ', N'Chục', N'Chục', 48),
(3, N'Muỗng Inox', N'Hộp', N'Hộp', 48), (3, N'Đũa gỗ', N'Bó', N'Bó', 12),
(3, N'Ly thủy tinh', N'Bộ 6', N'Bộ', 48), (3, N'Cây lau nhà', N'360', N'Bộ', 12),
(3, N'Chổi cỏ', N'Mềm', N'Cái', 12), (3, N'Ky rác', N'Nhựa', N'Cái', 24),
(3, N'Thùng rác', N'10L', N'Cái', 24), (3, N'Móc áo', N'Chục', N'Chục', 36),
(3, N'Kẹp áo', N'Vỉ', N'Vỉ', 24), (3, N'Thảm chân', N'Nỉ', N'Cái', 12),
(3, N'Khăn tắm', N'Cotton', N'Cái', 12), (3, N'Gối hơi', N'40x60', N'Cái', 12),
(3, N'Ga giường', N'1m6', N'Cái', 24), (3, N'Mền nỉ', N'Cá nhân', N'Cái', 24),
(3, N'Đèn bàn', N'Led', N'Cái', 24), (3, N'Bóng Led', N'20W', N'Cái', 24),
(3, N'Ổ cắm', N'5 lỗ', N'Cái', 24), (3, N'Vợt muỗi', N'Điện', N'Cái', 12),
(3, N'Đèn pin', N'Sạc', N'Cái', 24), (3, N'Pin AA', N'Vỉ 4', N'Vỉ', 24),
(3, N'Tạp dề', N'Vải', N'Cái', 24), (3, N'Găng tay', N'Cao su', N'Đôi', 12),
(3, N'Miếng rửa chén', N'Xanh', N'Gói', 12), (3, N'Cọ toilet', N'Tròn', N'Cái', 24),
(3, N'Bơm xe', N'Đạp chân', N'Cái', 36), (3, N'Khóa số', N'Vali', N'Cái', 36),
(3, N'Dây phơi', N'Cước', N'Cuộn', 24), (3, N'Ghế nhựa', N'Cao', N'Cái', 60);

-- Kho 4: Mỹ phẩm
INSERT INTO @ProductList VALUES 
(4, N'Son MAC', N'Ruby Woo', N'Thỏi', 12), (4, N'Son BlackRouge', N'A12', N'Thỏi', 12),
(4, N'Vaseline', N'Hũ', N'Hũ', 12), (4, N'Sữa rửa mặt', N'Cetaphil', N'Chai', 12),
(4, N'SRM Simple', N'Gel', N'Tuýp', 12), (4, N'SRM Cerave', N'Dầu', N'Chai', 12),
(4, N'Tẩy trang Bioderma', N'Hồng', N'Chai', 12), (4, N'Tẩy trang LOreal', N'Xanh', N'Chai', 12),
(4, N'Bông tẩy trang', N'Silcot', N'Hộp', 24), (4, N'Toner Klairs', N'180ml', N'Chai', 12),
(4, N'Toner Mamonde', N'250ml', N'Chai', 12), (4, N'Serum Ordinary', N'Niacin', N'Lọ', 12),
(4, N'Serum EL', N'ANR', N'Lọ', 12), (4, N'Kem dưỡng', N'Neutro', N'Hũ', 12),
(4, N'Kem dưỡng', N'Klairs', N'Tuýp', 12), (4, N'KCN Anessa', N'Sữa', N'Tuýp', 12),
(4, N'KCN Skin1004', N'Rau má', N'Tuýp', 12), (4, N'KCN LaRoche', N'Xanh', N'Tuýp', 12),
(4, N'Xịt khoáng', N'Evoluderm', N'Chai', 12), (4, N'Mặt nạ giấy', N'Trà xanh', N'Miếng', 12),
(4, N'Mặt nạ ngủ', N'Môi', N'Hũ', 12), (4, N'Mặt nạ đất sét', N'Kiehls', N'Hũ', 12),
(4, N'Tẩy tế bào chết', N'Cocoon', N'Hũ', 12), (4, N'Sữa tắm Lifebuoy', N'800g', N'Chai', 12),
(4, N'Sữa tắm Enchanteur', N'650g', N'Chai', 12), (4, N'Sữa tắm Dove', N'900g', N'Chai', 12),
(4, N'Dầu gội Sunsilk', N'Đen', N'Chai', 12), (4, N'Dầu gội Clear', N'Bạc hà', N'Chai', 12),
(4, N'Dầu gội H&S', N'Mỹ', N'Chai', 12), (4, N'Dầu xả Pantene', N'3p', N'Tuýp', 12),
(4, N'Ủ tóc Fino', N'Hũ', N'Hũ', 12), (4, N'Dưỡng tóc Moroc', N'25ml', N'Lọ', 24),
(4, N'Lăn Etiaxil', N'Xanh', N'Lọ', 12), (4, N'Lăn Nivea', N'Trắng', N'Lọ', 12),
(4, N'Phấn phủ', N'Innisfree', N'Hộp', 24), (4, N'Kem nền', N'Fit Me', N'Lọ', 12),
(4, N'Che khuyết điểm', N'TheSaem', N'Cây', 12), (4, N'Mascara', N'Maybelline', N'Cây', 6),
(4, N'Kẻ mắt', N'KissMe', N'Cây', 6), (4, N'Chì mày', N'Nâu', N'Cây', 12),
(4, N'Phấn mắt 3CE', N'9 ô', N'Hộp', 24), (4, N'Má hồng', N'Canmake', N'Hộp', 24),
(4, N'Nước hoa Charme', N'50ml', N'Chai', 36), (4, N'DDVS Dạ Hương', N'Xanh', N'Chai', 12),
(4, N'BVS Diana', N'Cánh', N'Gói', 36), (4, N'BVS Kotex', N'Đêm', N'Gói', 36),
(4, N'Khăn ướt', N'Bobby', N'Gói', 12), (4, N'Dầu xanh', N'Con Ó', N'Chai', 36),
(4, N'Dầu nóng', N'Trường Sơn', N'Chai', 36), (4, N'Cao sao vàng', N'Hộp', N'Hộp', 36);

-- Kho 5: Vật liệu xây dựng
INSERT INTO @ProductList VALUES 
(5, N'Xi măng Hà Tiên', N'PCB40', N'Bao', 3), (5, N'Xi măng Nghi Sơn', N'PCB40', N'Bao', 3),
(5, N'Xi măng Holcim', N'Đa dụng', N'Bao', 3), (5, N'Xi măng trắng', N'Indo', N'Bao', 3),
(5, N'Cát xây tô', N'Mịn', N'Khối', 100), (5, N'Cát bê tông', N'Vàng', N'Khối', 100),
(5, N'Cát lấp', N'Đen', N'Khối', 100), (5, N'Đá 1x2', N'Xanh', N'Khối', 100),
(5, N'Đá 4x6', N'Lót', N'Khối', 100), (5, N'Đá mi', N'Bụi', N'Khối', 100),
(5, N'Gạch ống', N'4 lỗ', N'Viên', 100), (5, N'Gạch đinh', N'2 lỗ', N'Viên', 100),
(5, N'Gạch Block', N'Không nung', N'Viên', 100), (5, N'Gạch lát 60', N'Vân mây', N'Thùng', 100),
(5, N'Gạch lát 80', N'Giả đá', N'Thùng', 100), (5, N'Gạch ốp 30x60', N'Hoa', N'Thùng', 100),
(5, N'Keo dán gạch', N'Weber', N'Bao', 12), (5, N'Bột trét', N'Jotun In', N'Bao', 12),
(5, N'Bột trét', N'Dulux Out', N'Bao', 12), (5, N'Sơn lót', N'Kháng kiềm', N'Thùng', 24),
(5, N'Sơn nội thất', N'Maxilite', N'Thùng', 24), (5, N'Sơn ngoại thất', N'Kova', N'Thùng', 24),
(5, N'Sơn dầu', N'Bạch Tuyết', N'Lon', 24), (5, N'Xăng thơm', N'Pha sơn', N'Lít', 12),
(5, N'Thép cuộn', N'Phi 6', N'Kg', 100), (5, N'Thép cuộn', N'Phi 8', N'Kg', 100),
(5, N'Thép cây', N'D10', N'Cây', 100), (5, N'Thép cây', N'D12', N'Cây', 100),
(5, N'Thép cây', N'D16', N'Cây', 100), (5, N'Xà gồ C', N'Hoa Sen', N'Cây', 100),
(5, N'Thép hộp', N'40x40', N'Cây', 100), (5, N'Thép hộp', N'30x60', N'Cây', 100),
(5, N'Tôn lạnh', N'Xanh', N'Mét', 100), (5, N'Tôn PU', N'3 lớp', N'Mét', 100),
(5, N'Ống PVC', N'Phi 27', N'Cây', 100), (5, N'Ống PVC', N'Phi 90', N'Cây', 100),
(5, N'Ống PPR', N'Phi 25', N'Cây', 100), (5, N'Co 90', N'Phi 27', N'Cái', 100),
(5, N'Tê đều', N'Phi 27', N'Cái', 100), (5, N'Van cổng', N'Đồng', N'Cái', 50),
(5, N'Vòi hồ', N'Inox', N'Cái', 50), (5, N'Keo non', N'Quấn ống', N'Cuộn', 24),
(5, N'Lưới B40', N'1m8', N'Cuộn', 50), (5, N'Kẽm buộc', N'1mm', N'Kg', 50),
(5, N'Đinh', N'5 phân', N'Kg', 50), (5, N'Vít tôn', N'5cm', N'Bịch', 50),
(5, N'Bay xây', N'Thép', N'Cái', 50), (5, N'Thước cuộn', N'5m', N'Cái', 24),
(5, N'Xẻng', N'Cán gỗ', N'Cái', 50), (5, N'Xe rùa', N'Thùng tôn', N'Cái', 50);

-- Kho 6: Văn phòng
INSERT INTO @ProductList VALUES 
(6, N'Giấy A4', N'Double A', N'Ram', 24), (6, N'Giấy A4', N'IK Plus', N'Ram', 24),
(6, N'Giấy A3', N'Double A', N'Ram', 24), (6, N'Giấy A5', N'Excel', N'Ram', 24),
(6, N'Bìa còng', N'7cm', N'Cái', 36), (6, N'Bìa còng', N'5cm', N'Cái', 36),
(6, N'Bìa lá', N'A4', N'Xấp', 36), (6, N'Bìa nút', N'A4', N'Cái', 36),
(6, N'Trình ký', N'Nhựa', N'Cái', 36), (6, N'Trình ký', N'Da', N'Cái', 36),
(6, N'Bút bi', N'TL027 Xanh', N'Cây', 24), (6, N'Bút bi', N'TL079 Đen', N'Cây', 24),
(6, N'Bút bi', N'Đỏ', N'Cây', 24), (6, N'Bút Gel', N'Uniball', N'Cây', 24),
(6, N'Bút chì', N'2B', N'Cây', 100), (6, N'Ruột chì', N'0.5', N'Hộp', 100),
(6, N'Dạ quang', N'Vàng', N'Cây', 24), (6, N'Dạ quang', N'Xanh', N'Cây', 24),
(6, N'Lông bảng', N'Xanh', N'Cây', 12), (6, N'Lông dầu', N'Đen', N'Cây', 12),
(6, N'Gôm', N'Pentel', N'Cái', 36), (6, N'Thước', N'30cm', N'Cái', 100),
(6, N'Kéo', N'Văn phòng', N'Cái', 36), (6, N'Dao rọc giấy', N'SDI', N'Cái', 36),
(6, N'Lưỡi dao', N'Hộp', N'Hộp', 36), (6, N'Bấm kim', N'Số 10', N'Cái', 36),
(6, N'Kim bấm', N'Plus', N'Hộp', 100), (6, N'Bấm lỗ', N'Trung', N'Cái', 36),
(6, N'Kẹp bướm', N'19mm', N'Hộp', 36), (6, N'Kẹp bướm', N'25mm', N'Hộp', 36),
(6, N'Kẹp ghim', N'C62', N'Hộp', 36), (6, N'Hồ dán', N'Khô', N'Chai', 12),
(6, N'Keo 502', N'Thuận Phong', N'Chai', 6), (6, N'Keo trong', N'5cm', N'Cuộn', 24),
(6, N'Keo giấy', N'2.4cm', N'Cuộn', 12), (6, N'Keo 2 mặt', N'2.4cm', N'Cuộn', 12),
(6, N'Sổ tay', N'A5', N'Cuốn', 36), (6, N'Sổ da', N'CK7', N'Cuốn', 36),
(6, N'Giấy note', N'Vàng', N'Xấp', 24), (6, N'Giấy note', N'5 màu', N'Vỉ', 24),
(6, N'Khay HS', N'3 tầng', N'Cái', 36), (6, N'Hộp bút', N'Xoay', N'Cái', 36),
(6, N'Máy tính', N'Casio', N'Cái', 60), (6, N'Pin AAA', N'Remote', N'Vỉ', 24),
(6, N'Dấu mộc', N'Sao y', N'Cái', 60), (6, N'Mực dấu', N'Đỏ', N'Lọ', 24),
(6, N'Bảng tên', N'Dây', N'Cái', 24), (6, N'Decal', N'Vàng', N'Xấp', 12),
(6, N'Bìa acco', N'Nhựa', N'Cái', 24), (6, N'Kẹp giấy', N'Màu', N'Hộp', 24);

-- INSERT VÀO BẢNG SẢN PHẨM CHÍNH (ÁP DỤNG LOGIC SUPPLIER NGAY TẠI ĐÂY)
INSERT INTO Products (ProductName, SoLuong, DonViTinh, HanSuDung, NgayNhapKho, CategoryID, SupplierID)
SELECT 
    t.BaseName + ' - ' + t.Variant, 
    1, -- Mặc định tồn 1
    t.Unit,
    DATEADD(MONTH, t.ShelfLifeMonths, GETDATE()), 
    GETDATE(),
    t.CatID,
    -- Logic gán Supplier ID theo nhóm ngành:
    CASE 
        WHEN t.CatID = 1 THEN FLOOR(RAND(CHECKSUM(NEWID())) * (4-1+1) + 1)   -- Cat 1 lấy Supplier 1-4
        WHEN t.CatID = 2 THEN FLOOR(RAND(CHECKSUM(NEWID())) * (8-5+1) + 5)   -- Cat 2 lấy Supplier 5-8
        WHEN t.CatID = 3 THEN FLOOR(RAND(CHECKSUM(NEWID())) * (12-9+1) + 9)  -- Cat 3 lấy Supplier 9-12
        WHEN t.CatID = 4 THEN FLOOR(RAND(CHECKSUM(NEWID())) * (16-13+1) + 13)-- Cat 4 lấy Supplier 13-16
        WHEN t.CatID = 5 THEN FLOOR(RAND(CHECKSUM(NEWID())) * (20-17+1) + 17)-- Cat 5 lấy Supplier 17-20
        WHEN t.CatID = 6 THEN FLOOR(RAND(CHECKSUM(NEWID())) * (24-21+1) + 21)-- Cat 6 lấy Supplier 21-24
    END
FROM @ProductList t;
GO

-- =============================================
-- 4. KIỂM TRA KẾT QUẢ CUỐI CÙNG
-- =============================================
SELECT 'Categories' as TableName, COUNT(*) as Count FROM Categories
UNION ALL
SELECT 'Suppliers', COUNT(*) FROM Suppliers
UNION ALL
SELECT 'Products', COUNT(*) FROM Products
UNION ALL
SELECT 'Receivers', COUNT(*) FROM Receivers;
GO

SELECT TOP 10 ProductID, ProductName, CategoryID, SupplierID FROM Products;
-- =============================================
-- 2. TẠO TRIGGERS TỰ ĐỘNG & LOGIC KHO 
-- Sử dụng "CREATE OR ALTER" để mọi trường hợp chưa có hoặc đã có đều chạy được.
-- =============================================

-- [TRIGGER QUAN TRỌNG] Tự động tạo phiếu nhập khi tạo Sản phẩm mới
CREATE OR ALTER TRIGGER trg_AutoCreateImport_OnNewProduct
ON Products
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Chỉ xử lý nếu có nhập số lượng ban đầu (>0)
    IF EXISTS (SELECT 1 FROM inserted WHERE SoLuong > 0)
    BEGIN
        -- BƯỚC 1: Reset số lượng trong Products về 0 NGAY LẬP TỨC.
        UPDATE Products 
        SET SoLuong = 0 
        FROM Products p
        JOIN inserted i ON p.ProductID = i.ProductID
        WHERE i.SoLuong > 0;

        -- BƯỚC 2: Tạo phiếu nhập kho
        INSERT INTO ImportReceipts (ImportDate, UserID, SupplierID, ProductID, Quantity)
        SELECT 
            GETDATE(),
            1, 
            i.SupplierID,
            i.ProductID,
            i.SoLuong
        FROM inserted i
        WHERE i.SoLuong > 0;
    END
END;
GO

-- [TRIGGER] Xử lý khi Insert vào bảng ImportReceipts (Cộng kho)
CREATE OR ALTER TRIGGER trg_AfterImport_Insert
ON ImportReceipts
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 1. Kiểm tra giới hạn 100 sản phẩm (Logic Set-based)
    IF EXISTS (
        SELECT 1
        FROM Products p
        JOIN Categories c ON p.CategoryID = c.CategoryID
        JOIN (
            SELECT ProductID, SUM(Quantity) as NewQty 
            FROM inserted 
            GROUP BY ProductID
        ) i ON p.ProductID = i.ProductID
        GROUP BY p.CategoryID
        HAVING (SUM(p.SoLuong) + SUM(i.NewQty)) > 10000
    )
    BEGIN
        RAISERROR(N'Lỗi: Danh mục đã đầy (Max 10000).', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- 2. Cộng tồn kho
    UPDATE p
    SET p.SoLuong = p.SoLuong + i.TotalQty, 
        p.NgayNhapKho = GETDATE()
    FROM Products p
    INNER JOIN (
        SELECT ProductID, SUM(Quantity) as TotalQty 
        FROM inserted 
        GROUP BY ProductID
    ) i ON p.ProductID = i.ProductID;

    -- 3. Ghi Log
    INSERT INTO ActivityLog (LoaiHanhDong, Description, TableName, RecordID, UserID)
    SELECT 
        N'Nhập kho', 
        N'Nhập ' + CAST(i.Quantity AS NVARCHAR) + N' SP (ID: ' + CAST(i.ProductID AS NVARCHAR) + N')', 
        'ImportReceipts',
        i.ImportID,
        i.UserID
    FROM inserted i;
END;
GO

-- [TRIGGER] Xử lý khi Insert vào bảng ExportReceipts (Trừ kho)
CREATE OR ALTER TRIGGER trg_AfterExport_Insert
ON ExportReceipts
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Kiểm tra đủ hàng không
    IF EXISTS (
        SELECT 1 
        FROM Products p
        JOIN (
            SELECT ProductID, SUM(Quantity) as TotalQty 
            FROM inserted 
            GROUP BY ProductID
        ) i ON p.ProductID = i.ProductID
        WHERE p.SoLuong < i.TotalQty
    )
    BEGIN
        RAISERROR(N'Lỗi: Không đủ hàng để xuất.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- 2. Trừ tồn kho
    UPDATE p
    SET p.SoLuong = p.SoLuong - i.TotalQty
    FROM Products p
    INNER JOIN (
        SELECT ProductID, SUM(Quantity) as TotalQty 
        FROM inserted 
        GROUP BY ProductID
    ) i ON p.ProductID = i.ProductID;

    -- 3. Ghi Log
    INSERT INTO ActivityLog (LoaiHanhDong, Description, TableName, RecordID, UserID)
    SELECT 
        N'Xuất kho', 
        N'Xuất ' + CAST(i.Quantity AS NVARCHAR) + N' SP (ID: ' + CAST(i.ProductID AS NVARCHAR) + N')', 
        'ExportReceipts',
        i.ExportID,
        i.UserID
    FROM inserted i;
END;
GO

-- Procedure Thông báo
CREATE OR ALTER PROCEDURE sp_CheckAndCreateNotifications
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Hết hạn sử dụng
    INSERT INTO Notifications (LoaiThongBao, MoTa, RelatedTable, RelatedID, UserID)
    SELECT DISTINCT
        N'Hết hạn sử dụng', 
        N'Sản phẩm ' + p.ProductName + N' (ID: ' + CAST(p.ProductID AS NVARCHAR) + N') đã hết hạn.',
        'Products', p.ProductID, u.UserID
    FROM Products p, Users u 
    WHERE p.HanSuDung < GETDATE() AND p.SoLuong > 0
    AND NOT EXISTS (
        SELECT 1 FROM Notifications n 
        WHERE n.UserID = u.UserID AND n.RelatedID = p.ProductID 
        AND n.LoaiThongBao = N'Hết hạn sử dụng' 
        AND CAST(n.CreatedAt AS DATE) = CAST(GETDATE() AS DATE)
    );
    
    -- 2. Quá hạn tồn kho
    INSERT INTO Notifications (LoaiThongBao, MoTa, RelatedTable, RelatedID, UserID)
    SELECT DISTINCT
        N'Quá hạn tồn kho', 
        N'Sản phẩm ' + p.ProductName + N' đã lưu kho quá hạn.',
        'Products', p.ProductID, u.UserID
    FROM Products p 
    JOIN Categories c ON p.CategoryID = c.CategoryID
    CROSS JOIN Users u
    WHERE DATEADD(MONTH, c.HanTonKho_Thang, p.NgayNhapKho) < GETDATE() AND p.SoLuong > 0
    AND NOT EXISTS (
        SELECT 1 FROM Notifications n 
        WHERE n.UserID = u.UserID AND n.RelatedID = p.ProductID 
        AND n.LoaiThongBao = N'Quá hạn tồn kho' 
        AND CAST(n.CreatedAt AS DATE) = CAST(GETDATE() AS DATE)
    );

    -- 3. Sắp hết hàng
    INSERT INTO Notifications (LoaiThongBao, MoTa, RelatedTable, RelatedID, UserID)
    SELECT DISTINCT
        N'Cảnh báo số lượng', 
        N'Sản phẩm ' + p.ProductName + N' sắp hết (còn ' + CAST(p.SoLuong AS NVARCHAR) + ').', 
        'Products', p.ProductID, u.UserID
    FROM Products p, Users u 
    WHERE p.SoLuong <= 5 AND p.SoLuong > 0
    AND NOT EXISTS (
        SELECT 1 FROM Notifications n 
        WHERE n.UserID = u.UserID AND n.RelatedID = p.ProductID 
        AND n.LoaiThongBao = N'Cảnh báo số lượng' 
        AND CAST(n.CreatedAt AS DATE) = CAST(GETDATE() AS DATE)
    );
END;
GO
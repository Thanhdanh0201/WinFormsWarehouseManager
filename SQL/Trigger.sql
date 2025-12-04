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

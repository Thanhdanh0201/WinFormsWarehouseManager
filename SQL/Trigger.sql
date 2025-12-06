-- 2. TẠO TRIGGERS & PROCEDURES 
GO

-- A. TRIGGER KHI NHẬP KHO (ImportReceiptDetails)
CREATE TRIGGER trg_AfterImportDetail_Insert
ON ImportReceiptDetails
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. KIỂM TRA GIỚI HẠN 100 SẢN PHẨM/KHO (Logic tập hợp)
    -- Nếu (Tổng tồn kho hiện tại + Tổng lượng đang nhập của Category đó) > 100 -> Báo lỗi
    IF EXISTS (
        SELECT c.CategoryID
        FROM Categories c
        -- Tính tổng tồn kho hiện tại trong DB
        LEFT JOIN (
            SELECT CategoryID, SUM(SoLuong) AS CurrentStock
            FROM Products
            GROUP BY CategoryID
        ) Stock ON c.CategoryID = Stock.CategoryID
        -- Tính tổng lượng đang chuẩn bị nhập vào (từ bảng ảo inserted)
        JOIN (
            SELECT p.CategoryID, SUM(i.Quantity) AS IncomingQty
            FROM inserted i
            JOIN Products p ON i.ProductID = p.ProductID
            GROUP BY p.CategoryID
        ) Incoming ON c.CategoryID = Incoming.CategoryID
        -- Kiểm tra điều kiện
        WHERE (ISNULL(Stock.CurrentStock, 0) + Incoming.IncomingQty) > 100
    )
    BEGIN
        RAISERROR(N'Lỗi: Kho hàng (Danh mục) này đã đầy hoặc sẽ vượt quá giới hạn 100 sản phẩm sau khi nhập. Giao dịch bị hủy.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- 2. CẬP NHẬT SỐ LƯỢNG & NGÀY NHẬP (Xử lý hàng loạt)
    UPDATE p
    SET p.SoLuong = p.SoLuong + i.Quantity,
        p.NgayNhapKho = GETDATE() -- Cập nhật ngày nhập mới nhất
    FROM Products p
    JOIN inserted i ON p.ProductID = i.ProductID;

    -- 3. GHI LOG HOẠT ĐỘNG
    INSERT INTO ActivityLog (LoaiHanhDong, Description, UserID)
    SELECT 
        N'Nhập kho', 
        N'Nhập ' + CAST(i.Quantity AS NVARCHAR) + N' sản phẩm (ID: ' + CAST(i.ProductID AS NVARCHAR) + N') vào phiếu nhập ' + CAST(i.ImportID AS NVARCHAR),
        ir.UserID
    FROM inserted i
    JOIN ImportReceipts ir ON i.ImportID = ir.ImportID;
END;
GO

-- B. TRIGGER KHI XUẤT KHO (ExportReceiptDetails)
CREATE TRIGGER trg_AfterExportDetail_Insert
ON ExportReceiptDetails
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. KIỂM TRA TỒN KHO CÓ ĐỦ KHÔNG
    -- Nếu có bất kỳ sản phẩm nào trong lô xuất có Số lượng tồn < Số lượng xuất -> Báo lỗi
    IF EXISTS (
        SELECT p.ProductID
        FROM Products p
        JOIN inserted i ON p.ProductID = i.ProductID
        WHERE p.SoLuong < i.Quantity
    )
    BEGIN
        RAISERROR(N'Lỗi: Một số sản phẩm không đủ số lượng tồn kho để xuất. Vui lòng kiểm tra lại.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- 2. TRỪ SỐ LƯỢNG TỒN KHO (Xử lý hàng loạt)
    UPDATE p
    SET p.SoLuong = p.SoLuong - i.Quantity
    FROM Products p
    JOIN inserted i ON p.ProductID = i.ProductID;

    -- 3. GHI LOG HOẠT ĐỘNG
    INSERT INTO ActivityLog (LoaiHanhDong, Description, UserID)
    SELECT 
        N'Xuất kho', 
        N'Xuất ' + CAST(i.Quantity AS NVARCHAR) + N' sản phẩm (ID: ' + CAST(i.ProductID AS NVARCHAR) + N') khỏi phiếu xuất ' + CAST(i.ExportID AS NVARCHAR),
        er.UserID
    FROM inserted i
    JOIN ExportReceipts er ON i.ExportID = er.ExportID;
END;
GO

-- C. PROCEDURE TẠO THÔNG BÁO TỰ ĐỘNG
CREATE PROCEDURE sp_CheckAndCreateNotifications
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Hết hạn sử dụng (Chỉ báo nếu chưa có thông báo cũ chưa đọc)
    INSERT INTO Notifications (LoaiThongBao, MoTa, UserID)
    SELECT 
        N'Hết hạn sử dụng',
        N'Sản phẩm ' + p.ProductName + N' (ID: ' + CAST(p.ProductID AS NVARCHAR) + N') đã hết hạn sử dụng ngày ' + CAST(p.HanSuDung AS NVARCHAR),
        u.UserID
    FROM Products p
    CROSS JOIN Users u -- Gửi cho tất cả Admin
    WHERE p.HanSuDung < GETDATE() 
      AND p.SoLuong > 0
      AND NOT EXISTS ( -- Kiểm tra trùng lặp
          SELECT 1 FROM Notifications n 
          WHERE n.UserID = u.UserID 
            AND n.MoTa LIKE N'%ID: ' + CAST(p.ProductID AS NVARCHAR) + N')%'
            AND n.LoaiThongBao = N'Hết hạn sử dụng'
            AND n.IsRead = 0
      );

    -- 2. Quá hạn tồn kho (Dựa theo quy định Category)
    INSERT INTO Notifications (LoaiThongBao, MoTa, UserID)
    SELECT 
        N'Quá hạn tồn kho',
        N'Sản phẩm ' + p.ProductName + N' thuộc nhóm ' + c.CategoryName + N' đã lưu kho quá hạn quy định.',
        u.UserID
    FROM Products p 
    JOIN Categories c ON p.CategoryID = c.CategoryID
    CROSS JOIN Users u
    WHERE DATEADD(MONTH, c.HanTonKho_Thang, p.NgayNhapKho) < GETDATE() 
      AND p.SoLuong > 0
      AND NOT EXISTS (
          SELECT 1 FROM Notifications n 
          WHERE n.UserID = u.UserID 
            AND n.MoTa LIKE N'%'+ p.ProductName + N'%'
            AND n.LoaiThongBao = N'Quá hạn tồn kho'
            AND n.IsRead = 0
      );
    
    -- 3. Cảnh báo sắp hết hàng (<= 5)
    INSERT INTO Notifications (LoaiThongBao, MoTa, UserID)
    SELECT 
        N'Cảnh báo số lượng',
        N'Sản phẩm ' + p.ProductName + N' sắp hết. Hiện chỉ còn ' + CAST(p.SoLuong AS NVARCHAR),
        u.UserID
    FROM Products p
    CROSS JOIN Users u
    WHERE p.SoLuong <= 5 
      AND p.SoLuong > 0
      AND NOT EXISTS (
          SELECT 1 FROM Notifications n 
          WHERE n.UserID = u.UserID 
            AND n.MoTa LIKE N'%'+ p.ProductName + N'%'
            AND n.LoaiThongBao = N'Cảnh báo số lượng'
            AND n.IsRead = 0
      );
END;
GO
using System;
using System.Data;
using System.Data.SQLite;
using WinFormsWarehouseManager.db;
using WinFormsWarehouseManager.Models;

namespace WinFormsWarehouseManager.Helpers
{
    /// <summary>
    /// Class quản lý tự động tạo và xử lý thông báo (OPTIMIZED với Cache)
    /// </summary>
    public class NotificationManager
    {
        private static DatabaseHelper dbHelper = new DatabaseHelper();

        // CACHE: Lưu thời gian generate lần cuối
        private static DateTime? lastGenerateTime = null;
        private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromMinutes(30);

        /// <summary>
        /// Tạo tất cả các loại thông báo với CACHE 30 phút
        /// </summary>
        public static void GenerateAllNotifications()
        {
            // KIỂM TRA CACHE: Nếu chưa quá 30 phút thì skip
            if (lastGenerateTime.HasValue &&
                DateTime.Now - lastGenerateTime.Value < CACHE_DURATION)
            {
                System.Diagnostics.Debug.WriteLine("Skip generate notifications - Cache still valid");
                return;
            }

            try
            {
                // TỐI ƯU: Chỉ tạo thông báo cho USER HIỆN TẠI
                int userId = UserSession.CurrentUserID;
                if (userId <= 0) return;

                GenerateExpiredProductNotifications(userId);
                GenerateOverstockNotifications(userId);
                GenerateLowStockNotifications(userId);

                // Cập nhật cache time
                lastGenerateTime = DateTime.Now;
                System.Diagnostics.Debug.WriteLine($"Generated notifications for UserID: {userId} at {lastGenerateTime}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Generate all notifications error: {ex.Message}");
            }
        }

        /// <summary>
        /// Force refresh - Bỏ qua cache
        /// </summary>
        public static void ForceRefreshNotifications()
        {
            lastGenerateTime = null;
            GenerateAllNotifications();
        }

        /// <summary>
        /// 1. Tạo thông báo sản phẩm hết hạn sử dụng - CHỈ CHO 1 USER
        /// </summary>
        private static void GenerateExpiredProductNotifications(int userId)
        {
            try
            {
                string query = @"
                    INSERT INTO Notifications (LoaiThongBao, MoTa, RelatedTable, RelatedID, UserID, CreatedAt, IsRead)
                    SELECT 
                        'Hết hạn sử dụng',
                        'Sản phẩm ' || p.ProductName || ' (ID: ' || CAST(p.ProductID AS TEXT) || ') đã hết hạn.',
                        'Products',
                        p.ProductID,
                        @UserID,
                        datetime('now'),
                        0
                    FROM Products p
                    WHERE p.HanSuDung < date('now') 
                      AND p.SoLuong > 0
                      AND NOT EXISTS (
                        SELECT 1 FROM Notifications n
                        WHERE n.UserID = @UserID
                          AND n.RelatedID = p.ProductID
                          AND n.LoaiThongBao = 'Hết hạn sử dụng'
                          AND date(n.CreatedAt) = date('now')
                    )";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@UserID", userId)
                };

                dbHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Generate expired notifications error: {ex.Message}");
            }
        }

        /// <summary>
        /// 2. Tạo thông báo sản phẩm quá hạn tồn kho - CHỈ CHO 1 USER
        /// </summary>
        private static void GenerateOverstockNotifications(int userId)
        {
            try
            {
                string query = @"
                    INSERT INTO Notifications (LoaiThongBao, MoTa, RelatedTable, RelatedID, UserID, CreatedAt, IsRead)
                    SELECT 
                        'Quá hạn tồn kho',
                        'Sản phẩm ' || p.ProductName || ' đã lưu kho quá hạn.',
                        'Products',
                        p.ProductID,
                        @UserID,
                        datetime('now'),
                        0
                    FROM Products p
                    JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE date(p.NgayNhapKho, '+' || c.HanTonKho_Thang || ' months') < date('now')
                      AND p.SoLuong > 0
                      AND NOT EXISTS (
                        SELECT 1 FROM Notifications n
                        WHERE n.UserID = @UserID
                          AND n.RelatedID = p.ProductID
                          AND n.LoaiThongBao = 'Quá hạn tồn kho'
                          AND date(n.CreatedAt) = date('now')
                    )";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@UserID", userId)
                };

                dbHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Generate overstock notifications error: {ex.Message}");
            }
        }

        /// <summary>
        /// 3. Tạo thông báo sản phẩm sắp hết hàng - CHỈ CHO 1 USER
        /// </summary>
        private static void GenerateLowStockNotifications(int userId)
        {
            try
            {
                string query = @"
                    INSERT INTO Notifications (LoaiThongBao, MoTa, RelatedTable, RelatedID, UserID, CreatedAt, IsRead)
                    SELECT 
                        'Cảnh báo số lượng',
                        'Sản phẩm ' || p.ProductName || ' sắp hết (còn ' || CAST(p.SoLuong AS TEXT) || ').',
                        'Products',
                        p.ProductID,
                        @UserID,
                        datetime('now'),
                        0
                    FROM Products p
                    WHERE p.SoLuong <= 5 
                      AND p.SoLuong > 0
                      AND NOT EXISTS (
                        SELECT 1 FROM Notifications n
                        WHERE n.UserID = @UserID
                          AND n.RelatedID = p.ProductID
                          AND n.LoaiThongBao = 'Cảnh báo số lượng'
                          AND date(n.CreatedAt) = date('now')
                    )";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@UserID", userId)
                };

                dbHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Generate low stock notifications error: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy danh sách thông báo của user hiện tại
        /// </summary>
        public static DataTable GetUserNotifications(bool unreadOnly = false)
        {
            try
            {
                int userId = UserSession.CurrentUserID;
                if (userId <= 0) return null;

                string query = @"
                    SELECT 
                        NotiID,
                        LoaiThongBao,
                        MoTa,
                        RelatedTable,
                        RelatedID,
                        CreatedAt,
                        IsRead
                    FROM Notifications
                    WHERE UserID = @UserID";

                if (unreadOnly)
                {
                    query += " AND IsRead = 0";
                }

                query += " ORDER BY CreatedAt DESC";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@UserID", userId)
                };

                return dbHelper.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Get notifications error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Đếm số thông báo chưa đọc
        /// </summary>
        public static int GetUnreadCount()
        {
            try
            {
                int userId = UserSession.CurrentUserID;
                if (userId <= 0) return 0;

                string query = @"
                    SELECT COUNT(*) 
                    FROM Notifications 
                    WHERE UserID = @UserID AND IsRead = 0";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@UserID", userId)
                };

                object result = dbHelper.ExecuteScalar(query, parameters);
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Get unread count error: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Đánh dấu thông báo đã đọc
        /// </summary>
        public static bool MarkAsRead(int notiId)
        {
            try
            {
                string query = @"
                    UPDATE Notifications 
                    SET IsRead = 1 
                    WHERE NotiID = @NotiID AND UserID = @UserID";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@NotiID", notiId),
                    new SQLiteParameter("@UserID", UserSession.CurrentUserID)
                };

                int rowsAffected = dbHelper.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Mark as read error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Đánh dấu tất cả thông báo đã đọc
        /// </summary>
        public static bool MarkAllAsRead()
        {
            try
            {
                string query = @"
                    UPDATE Notifications 
                    SET IsRead = 1 
                    WHERE UserID = @UserID AND IsRead = 0";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@UserID", UserSession.CurrentUserID)
                };

                dbHelper.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Mark all as read error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Xóa thông báo
        /// </summary>
        public static bool DeleteNotification(int notiId)
        {
            try
            {
                string query = @"
                    DELETE FROM Notifications 
                    WHERE NotiID = @NotiID AND UserID = @UserID";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@NotiID", notiId),
                    new SQLiteParameter("@UserID", UserSession.CurrentUserID)
                };

                int rowsAffected = dbHelper.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Delete notification error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Xóa tất cả thông báo đã đọc
        /// </summary>
        public static bool DeleteAllRead()
        {
            try
            {
                string query = @"
                    DELETE FROM Notifications 
                    WHERE UserID = @UserID AND IsRead = 1";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@UserID", UserSession.CurrentUserID)
                };

                dbHelper.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Delete all read error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lấy icon cho loại thông báo
        /// </summary>
        public static string GetNotificationIcon(string loaiThongBao)
        {
            switch (loaiThongBao)
            {
                case "Hết hạn sử dụng":
                    return "⚠️";
                case "Quá hạn tồn kho":
                    return "📦";
                case "Cảnh báo số lượng":
                    return "📉";
                default:
                    return "ℹ️";
            }
        }

        /// <summary>
        /// Lấy màu cho loại thông báo
        /// </summary>
        public static System.Drawing.Color GetNotificationColor(string loaiThongBao)
        {
            switch (loaiThongBao)
            {
                case "Hết hạn sử dụng":
                    return System.Drawing.Color.FromArgb(231, 76, 60); // Đỏ
                case "Quá hạn tồn kho":
                    return System.Drawing.Color.FromArgb(230, 126, 34); // Cam
                case "Cảnh báo số lượng":
                    return System.Drawing.Color.FromArgb(241, 196, 15); // Vàng
                default:
                    return System.Drawing.Color.FromArgb(52, 152, 219); // Xanh dương
            }
        }
    }
}
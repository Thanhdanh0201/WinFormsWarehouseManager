using System;
using System.Data;
using System.Data.SQLite;
using WinFormsWarehouseManager.db;
using WinFormsWarehouseManager.Models;

namespace WinFormsWarehouseManager.Helpers
{
    /// <summary>
    /// Class quản lý tự động tạo và xử lý thông báo
    /// </summary>
    public class NotificationManager
    {
        private static DatabaseHelper dbHelper = new DatabaseHelper();

        /// <summary>
        /// Tạo tất cả các loại thông báo (gọi khi mở app hoặc định kỳ)
        /// </summary>
        public static void GenerateAllNotifications()
        {
            GenerateExpiredProductNotifications();
            GenerateOverstockNotifications();
            GenerateLowStockNotifications();
        }

        /// <summary>
        /// 1. Tạo thông báo sản phẩm hết hạn sử dụng
        /// </summary>
        public static void GenerateExpiredProductNotifications()
        {
            try
            {
                string query = @"
                    INSERT INTO Notifications (LoaiThongBao, MoTa, RelatedTable, RelatedID, UserID, CreatedAt, IsRead)
                    SELECT 
                        LoaiThongBao, MoTa, RelatedTable, RelatedID, UserID,
                        datetime('now'), 0
                    FROM vw_ExpiredProducts v
                    WHERE NOT EXISTS (
                        SELECT 1 FROM Notifications n
                        WHERE n.UserID = v.UserID 
                          AND n.RelatedID = v.RelatedID
                          AND n.LoaiThongBao = 'Hết hạn sử dụng'
                          AND date(n.CreatedAt) = date('now')
                    )";

                dbHelper.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Generate expired notifications error: {ex.Message}");
            }
        }

        /// <summary>
        /// 2. Tạo thông báo sản phẩm quá hạn tồn kho
        /// </summary>
        public static void GenerateOverstockNotifications()
        {
            try
            {
                string query = @"
                    INSERT INTO Notifications (LoaiThongBao, MoTa, RelatedTable, RelatedID, UserID, CreatedAt, IsRead)
                    SELECT 
                        LoaiThongBao, MoTa, RelatedTable, RelatedID, UserID,
                        datetime('now'), 0
                    FROM vw_OverstockProducts v
                    WHERE NOT EXISTS (
                        SELECT 1 FROM Notifications n
                        WHERE n.UserID = v.UserID 
                          AND n.RelatedID = v.RelatedID
                          AND n.LoaiThongBao = 'Quá hạn tồn kho'
                          AND date(n.CreatedAt) = date('now')
                    )";

                dbHelper.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Generate overstock notifications error: {ex.Message}");
            }
        }

        /// <summary>
        /// 3. Tạo thông báo sản phẩm sắp hết hàng
        /// </summary>
        public static void GenerateLowStockNotifications()
        {
            try
            {
                string query = @"
                    INSERT INTO Notifications (LoaiThongBao, MoTa, RelatedTable, RelatedID, UserID, CreatedAt, IsRead)
                    SELECT 
                        LoaiThongBao, MoTa, RelatedTable, RelatedID, UserID,
                        datetime('now'), 0
                    FROM vw_LowStockProducts v
                    WHERE NOT EXISTS (
                        SELECT 1 FROM Notifications n
                        WHERE n.UserID = v.UserID 
                          AND n.RelatedID = v.RelatedID
                          AND n.LoaiThongBao = 'Cảnh báo số lượng'
                          AND date(n.CreatedAt) = date('now')
                    )";

                dbHelper.ExecuteNonQuery(query);
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
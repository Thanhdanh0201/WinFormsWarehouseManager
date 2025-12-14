using System;
using System.Data.SQLite;
using WinFormsWarehouseManager.db;

namespace WinFormsWarehouseManager.Helpers
{
    /// <summary>
    /// Class helper để ghi log hoạt động của user
    /// </summary>
    public static class ActivityLogger
    {
        private static DatabaseHelper dbHelper = new DatabaseHelper();

        /// <summary>
        /// Ghi log hoạt động đơn giản
        /// </summary>
        public static void Log(string action, string description)
        {
            Log(action, description, null, null);
        }

        /// <summary>
        /// Ghi log hoạt động với thông tin bảng và recordID
        /// </summary>
        public static void Log(string action, string description, string tableName, int? recordId)
        {
            try
            {
                int userId = Models.UserSession.CurrentUserID;

                if (userId <= 0)
                {
                    System.Diagnostics.Debug.WriteLine("Cannot log: No user logged in");
                    return;
                }

                string query = @"INSERT INTO ActivityLog 
                               (LoaiHanhDong, Description, TableName, RecordID, UserID) 
                               VALUES (@Action, @Description, @TableName, @RecordID, @UserID)";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Action", action),
                    new SQLiteParameter("@Description", description),
                    new SQLiteParameter("@TableName", (object)tableName ?? DBNull.Value),
                    new SQLiteParameter("@RecordID", (object)recordId ?? DBNull.Value),
                    new SQLiteParameter("@UserID", userId)
                };

                dbHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ActivityLogger error: {ex.Message}");
            }
        }

        /// <summary>
        /// Ghi log thêm mới
        /// </summary>
        public static void LogInsert(string tableName, int recordId, string description = null)
        {
            string desc = description ?? $"Thêm mới bản ghi trong {tableName}";
            Log("INSERT", desc, tableName, recordId);
        }

        /// <summary>
        /// Ghi log cập nhật
        /// </summary>
        public static void LogUpdate(string tableName, int recordId, string description = null)
        {
            string desc = description ?? $"Cập nhật bản ghi trong {tableName}";
            Log("UPDATE", desc, tableName, recordId);
        }

        /// <summary>
        /// Ghi log xóa
        /// </summary>
        public static void LogDelete(string tableName, int recordId, string description = null)
        {
            string desc = description ?? $"Xóa bản ghi trong {tableName}";
            Log("DELETE", desc, tableName, recordId);
        }

        /// <summary>
        /// Ghi log xem/truy cập
        /// </summary>
        public static void LogView(string tableName, int? recordId = null, string description = null)
        {
            string desc = description ?? $"Xem dữ liệu {tableName}";
            Log("VIEW", desc, tableName, recordId);
        }

        /// <summary>
        /// Ghi log export/print
        /// </summary>
        public static void LogExport(string tableName, string exportType = "Export", string description = null)
        {
            string desc = description ?? $"{exportType} dữ liệu {tableName}";
            Log(exportType, desc, tableName, null);
        }
    }
}
using System;

namespace WinFormsWarehouseManager.Models
{
    /// <summary>
    /// Class static quản lý thông tin user đang đăng nhập
    /// Dùng để truy cập UserID cho ActivityLog và Notifications
    /// </summary>
    public static class UserSession
    {
        private static User _currentUser;

        /// <summary>
        /// User hiện tại đang đăng nhập
        /// </summary>
        public static User CurrentUser
        {
            get => _currentUser;
            set => _currentUser = value;
        }

        /// <summary>
        /// UserID của user hiện tại
        /// </summary>
        public static int CurrentUserID
        {
            get => _currentUser?.UserID ?? 0;
        }

        /// <summary>
        /// Tên đầy đủ của user hiện tại
        /// </summary>
        public static string CurrentUserName
        {
            get => _currentUser?.FullName ?? "Unknown";
        }

        /// <summary>
        /// Email của user hiện tại
        /// </summary>
        public static string CurrentUserEmail
        {
            get => _currentUser?.Email ?? "";
        }

        /// <summary>
        /// Kiểm tra có user đang đăng nhập không
        /// </summary>
        public static bool IsLoggedIn
        {
            get => _currentUser != null && _currentUser.IsValid();
        }

        /// <summary>
        /// Đăng xuất - xóa thông tin user
        /// </summary>
        public static void Logout()
        {
            _currentUser = null;
        }

        /// <summary>
        /// Lấy thông tin user để hiển thị
        /// </summary>
        public static string GetDisplayInfo()
        {
            if (!IsLoggedIn)
                return "Chưa đăng nhập";

            return $"{CurrentUserName} - {CurrentUserEmail}";
        }
    }
}
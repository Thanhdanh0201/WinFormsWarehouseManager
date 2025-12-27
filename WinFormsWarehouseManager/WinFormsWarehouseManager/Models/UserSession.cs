using System;

namespace WinFormsWarehouseManager.Models
{
    public static class UserSession
    {
        private static User _currentUser;

        public static User CurrentUser
        {
            get => _currentUser;
            set => _currentUser = value;
        }

        public static int CurrentUserID
        {
            get => _currentUser?.UserID ?? 0;
        }

        public static string CurrentUserName
        {
            get => _currentUser?.FullName ?? "Unknown";
        }

        public static string CurrentUserEmail
        {
            get => _currentUser?.Email ?? "";
        }

        /// <summary>
        /// Mật khẩu mailbox của user hiện tại
        /// </summary>
        public static string CurrentUserMailboxPassword
        {
            get => _currentUser?.MailboxPassword ?? "";
        }

        public static bool IsLoggedIn
        {
            get => _currentUser != null && _currentUser.IsValid();
        }

        public static void Logout()
        {
            _currentUser = null;
        }

        public static string GetDisplayInfo()
        {
            if (!IsLoggedIn)
                return "Chưa đăng nhập";

            return $"{CurrentUserName} - {CurrentUserEmail}";
        }
    }
}
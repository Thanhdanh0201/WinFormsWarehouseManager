using System;

namespace WinFormsWarehouseManager.Models
{
    /// <summary>
    /// Model đại diện cho User trong hệ thống
    /// </summary>
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }

        /// <summary>
        /// Lấy tên hiển thị ngắn gọn (First Name)
        /// </summary>
        public string FirstName
        {
            get
            {
                if (string.IsNullOrEmpty(FullName))
                    return "User";

                string[] parts = FullName.Trim().Split(' ');
                return parts[0];
            }
        }

        /// <summary>
        /// Kiểm tra có phải user hợp lệ không
        /// </summary>
        public bool IsValid()
        {
            return UserID > 0 && !string.IsNullOrEmpty(Email);
        }

        public override string ToString()
        {
            return $"{FullName} ({Email})";
        }
    }
}
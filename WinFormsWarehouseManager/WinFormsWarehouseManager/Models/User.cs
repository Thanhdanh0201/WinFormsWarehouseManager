using System;

namespace WinFormsWarehouseManager.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }  // DateTime? (nullable)
        public string Email { get; set; }
        public string Password { get; set; }
        public string MailboxPassword { get; set; }
        public DateTime CreatedAt { get; set; }   // DateTime

        public bool IsValid()
        {
            return UserID > 0 && !string.IsNullOrEmpty(Email);
        }
    }
}
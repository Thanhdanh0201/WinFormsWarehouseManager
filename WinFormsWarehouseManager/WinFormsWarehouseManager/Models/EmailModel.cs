using System;

namespace WinFormsWarehouseManager.Models
{
    /// <summary>
    /// Model đại diện cho Email
    /// </summary>
    public class EmailModel
    {
        public uint Uid { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
        public string FolderName { get; set; }

        /// <summary>
        /// Lấy tên người gửi ngắn gọn (chỉ tên, bỏ email)
        /// </summary>
        public string FromName
        {
            get
            {
                if (string.IsNullOrEmpty(From))
                    return "Unknown";

                // Loại bỏ phần email nếu có format "Name <email@domain.com>"
                int startIndex = From.IndexOf('<');
                if (startIndex > 0)
                    return From.Substring(0, startIndex).Trim().Trim('"');

                return From;
            }
        }

        /// <summary>
        /// Lấy địa chỉ email từ From
        /// </summary>
        public string FromEmail
        {
            get
            {
                if (string.IsNullOrEmpty(From))
                    return "";

                int startIndex = From.IndexOf('<');
                int endIndex = From.IndexOf('>');

                if (startIndex >= 0 && endIndex > startIndex)
                    return From.Substring(startIndex + 1, endIndex - startIndex - 1);

                return From;
            }
        }

        /// <summary>
        /// Format ngày giờ để hiển thị
        /// </summary>
        public string DateDisplay
        {
            get
            {
                if (Date.Date == DateTime.Today)
                    return Date.ToString("HH:mm");
                else if (Date.Date == DateTime.Today.AddDays(-1))
                    return "Hôm qua " + Date.ToString("HH:mm");
                else if (Date.Year == DateTime.Now.Year)
                    return Date.ToString("dd/MM HH:mm");
                else
                    return Date.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// Lấy preview của body (100 ký tự đầu)
        /// </summary>
        public string BodyPreview
        {
            get
            {
                if (string.IsNullOrEmpty(Body))
                    return "";

                string preview = Body.Replace("\n", " ").Replace("\r", " ").Trim();
                return preview.Length > 100 ? preview.Substring(0, 100) + "..." : preview;
            }
        }

        public override string ToString()
        {
            return $"{FromName} - {Subject}";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using WinFormsWarehouseManager.Models;

namespace WinFormsWarehouseManager.Services
{
    /// <summary>
    /// Service xử lý IMAP operations với Gmail
    /// </summary>
    public class EmailService : IDisposable
    {
        private ImapClient _client;
        private readonly string _email;
        private readonly string _password;
        private const string ImapServer = "imap.gmail.com";
        private const int ImapPort = 993;

        public EmailService(string email, string password)
        {
            _email = email;
            _password = password;
            _client = new ImapClient();
        }

        /// <summary>
        /// Kết nối đến Gmail IMAP server
        /// </summary>
        public bool Connect()
        {
            try
            {
                if (_client.IsConnected)
                    return true;

                _client.Connect(ImapServer, ImapPort, SecureSocketOptions.SslOnConnect);
                _client.Authenticate(_email, _password);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể kết nối IMAP: {ex.Message}");
            }
        }

        /// <summary>
        /// Ngắt kết nối
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (_client.IsConnected)
                {
                    _client.Disconnect(true);
                }
            }
            catch { }
        }

        /// <summary>
        /// Lấy danh sách email từ folder
        /// </summary>
        public List<EmailModel> GetEmails(string folderName, int limit = 50)
        {
            try
            {
                Connect();

                var folder = _client.GetFolder(folderName);
                folder.Open(FolderAccess.ReadOnly);

                var emails = new List<EmailModel>();
                int count = Math.Min(folder.Count, limit);

                // Lấy UIDs trước
                var uids = new List<UniqueId>();
                for (int i = folder.Count - 1; i >= folder.Count - count && i >= 0; i--)
                {
                    uids.Add(new UniqueId((uint)i));
                }

                // Fetch messages với flags
                var items = folder.Fetch(uids, MessageSummaryItems.UniqueId | MessageSummaryItems.Envelope | MessageSummaryItems.Flags | MessageSummaryItems.BodyStructure);

                foreach (var item in items)
                {
                    var message = folder.GetMessage(item.UniqueId);

                    emails.Add(new EmailModel
                    {
                        Uid = item.UniqueId.Id,
                        From = item.Envelope.From.ToString(),
                        To = item.Envelope.To.ToString(),
                        Subject = item.Envelope.Subject ?? "(No Subject)",
                        Body = message.TextBody ?? message.HtmlBody ?? "",
                        Date = item.Envelope.Date?.DateTime ?? DateTime.Now,
                        IsRead = item.Flags.HasValue && item.Flags.Value.HasFlag(MessageFlags.Seen),
                        FolderName = folderName
                    });
                }

                folder.Close();
                return emails;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy email từ {folderName}: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy chi tiết 1 email theo UID
        /// </summary>
        public EmailModel GetEmailByUid(string folderName, uint uid)
        {
            try
            {
                Connect();

                var folder = _client.GetFolder(folderName);
                folder.Open(FolderAccess.ReadWrite);

                var uniqueId = new UniqueId(uid);
                var message = folder.GetMessage(uniqueId);

                // Đánh dấu đã đọc
                folder.AddFlags(uniqueId, MessageFlags.Seen, true);

                var email = new EmailModel
                {
                    Uid = uid,
                    From = message.From.ToString(),
                    To = message.To.ToString(),
                    Subject = message.Subject ?? "(No Subject)",
                    Body = message.TextBody ?? message.HtmlBody ?? "",
                    Date = message.Date.DateTime,
                    IsRead = true,
                    FolderName = folderName
                };

                folder.Close();
                return email;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy email UID {uid}: {ex.Message}");
            }
        }

        /// <summary>
        /// Gửi email mới
        /// </summary>
        public void SendEmail(string to, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("", _email));
                message.To.Add(new MailboxAddress("", to));
                message.Subject = subject;
                message.Body = new TextPart("plain") { Text = body };

                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_email, _password);
                    smtp.Send(message);
                    smtp.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi gửi email: {ex.Message}");
            }
        }

        /// <summary>
        /// Reply email
        /// </summary>
        public void ReplyEmail(EmailModel originalEmail, string replyBody)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("", _email));
                message.To.Add(MailboxAddress.Parse(originalEmail.FromEmail));
                message.Subject = originalEmail.Subject.StartsWith("Re:")
                    ? originalEmail.Subject
                    : "Re: " + originalEmail.Subject;

                string fullBody = replyBody + "\n\n--- Original Message ---\n" + originalEmail.Body;
                message.Body = new TextPart("plain") { Text = fullBody };

                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_email, _password);
                    smtp.Send(message);
                    smtp.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi reply email: {ex.Message}");
            }
        }

        /// <summary>
        /// Chuyển email vào Trash
        /// </summary>
        public void MoveToTrash(string currentFolder, uint uid)
        {
            try
            {
                Connect();

                var sourceFolder = _client.GetFolder(currentFolder);
                sourceFolder.Open(FolderAccess.ReadWrite);

                var trashFolder = _client.GetFolder(SpecialFolder.Trash);

                var uniqueId = new UniqueId(uid);
                sourceFolder.MoveTo(uniqueId, trashFolder);

                sourceFolder.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi xóa email: {ex.Message}");
            }
        }

        /// <summary>
        /// Tìm kiếm email theo từ khóa
        /// </summary>
        public List<EmailModel> SearchEmails(string folderName, string keyword)
        {
            try
            {
                Connect();

                var folder = _client.GetFolder(folderName);
                folder.Open(FolderAccess.ReadOnly);

                var query = SearchQuery.SubjectContains(keyword)
                    .Or(SearchQuery.BodyContains(keyword))
                    .Or(SearchQuery.FromContains(keyword));

                var uids = folder.Search(query);
                var emails = new List<EmailModel>();

                if (uids.Count > 0)
                {
                    var items = folder.Fetch(uids, MessageSummaryItems.UniqueId | MessageSummaryItems.Envelope | MessageSummaryItems.Flags | MessageSummaryItems.BodyStructure);

                    foreach (var item in items)
                    {
                        var message = folder.GetMessage(item.UniqueId);

                        emails.Add(new EmailModel
                        {
                            Uid = item.UniqueId.Id,
                            From = item.Envelope.From.ToString(),
                            To = item.Envelope.To.ToString(),
                            Subject = item.Envelope.Subject ?? "(No Subject)",
                            Body = message.TextBody ?? message.HtmlBody ?? "",
                            Date = item.Envelope.Date?.DateTime ?? DateTime.Now,
                            IsRead = item.Flags.HasValue && item.Flags.Value.HasFlag(MessageFlags.Seen),
                            FolderName = folderName
                        });
                    }
                }

                folder.Close();
                return emails.OrderByDescending(e => e.Date).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi tìm kiếm email: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy số lượng email trong Inbox
        /// </summary>
        public int GetInboxCount()
        {
            try
            {
                Connect();
                var inbox = _client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                int count = inbox.Count;
                inbox.Close();
                return count;
            }
            catch
            {
                return 0;
            }
        }

        public void Dispose()
        {
            Disconnect();
            _client?.Dispose();
        }
    }
}
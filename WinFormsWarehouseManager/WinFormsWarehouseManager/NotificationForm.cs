using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsWarehouseManager.Helpers;

namespace WinFormsWarehouseManager
{
    public partial class NotificationForm : Form
    {
        // CACHE: Lưu DataTable trong memory
        private DataTable cachedNotifications = null;
        private bool isResizing = false;

        // Dictionary để track cards theo NotiID
        private Dictionary<int, Panel> cardDictionary = new Dictionary<int, Panel>();

        public NotificationForm()
        {
            InitializeComponent();

            // Tối ưu resize: Chỉ adjust width, không reload
            this.ResizeBegin += (s, e) => { isResizing = true; };
            this.ResizeEnd += (s, e) =>
            {
                isResizing = false;
                AdjustCardsWidth(); // Chỉ adjust width, không tạo lại
            };
        }

        private void NotificationForm_Load(object sender, EventArgs e)
        {
            // Load danh sách thông báo lần đầu
            LoadNotifications(forceRefresh: false);
        }

        /// <summary>
        /// Load và hiển thị danh sách thông báo với CACHE
        /// </summary>
        private void LoadNotifications(bool forceRefresh = false)
        {
            try
            {
                // CACHE: Chỉ query DB khi cần thiết
                if (forceRefresh || cachedNotifications == null)
                {
                    bool unreadOnly = chkUnreadOnly.Checked;

                    cachedNotifications = NotificationManager.GetUserNotifications(unreadOnly);
                }

                // Clear panel và dictionary
                pnlNotifications.Controls.Clear();
                cardDictionary.Clear();

                if (cachedNotifications == null || cachedNotifications.Rows.Count == 0)
                {
                    lblNoData.Visible = true;
                    lblNoData.BringToFront();
                    UpdateUnreadCount();
                    return;
                }

                lblNoData.Visible = false;

                // Suspend layout để tăng tốc
                pnlNotifications.SuspendLayout();

                // Tạo card cho mỗi thông báo
                foreach (DataRow row in cachedNotifications.Rows)
                {
                    int notiId = Convert.ToInt32(row["NotiID"]);
                    Panel card = CreateNotificationCard(row);
                    pnlNotifications.Controls.Add(card);

                    // Lưu vào dictionary để có thể update sau
                    cardDictionary[notiId] = card;
                }

                pnlNotifications.ResumeLayout();

                // Update số lượng chưa đọc
                UpdateUnreadCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load thông báo: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Chỉ adjust width của cards hiện có, KHÔNG tạo lại
        /// </summary>
        private void AdjustCardsWidth()
        {
            if (isResizing) return;

            int cardWidth = pnlNotifications.ClientSize.Width - 60;
            int minWidth = 1000;
            if (cardWidth < minWidth) cardWidth = minWidth;

            pnlNotifications.SuspendLayout();

            foreach (Panel card in pnlNotifications.Controls.OfType<Panel>())
            {
                card.Width = cardWidth;

                // Update vị trí buttons
                int btnRightMargin = 25;
                int btnDeleteX = cardWidth - 170 - btnRightMargin;
                int btnMarkReadX = btnDeleteX - 170 - 15;

                // Tìm và update vị trí buttons
                foreach (Control ctrl in card.Controls)
                {
                    if (ctrl is IconButton btn)
                    {
                        if (btn.Text == "Xóa")
                            btn.Location = new Point(btnDeleteX, btn.Location.Y);
                        else if (btn.Text == "Đánh dấu" || btn.Text == "Đã đọc")
                            btn.Location = new Point(btnMarkReadX, btn.Location.Y);
                    }
                }

                // Update width của description label
                int rightButtonsSpace = 400;
                int contentWidth = cardWidth - 115 - rightButtonsSpace;
                if (contentWidth < 450) contentWidth = 450;

                foreach (Control ctrl in card.Controls)
                {
                    if (ctrl is Label lbl && lbl.Location.X == 115 && lbl.Location.Y == 52)
                    {
                        lbl.Width = contentWidth;
                        break;
                    }
                }
            }

            pnlNotifications.ResumeLayout();
        }

        /// <summary>
        /// Tạo card hiển thị thông báo
        /// </summary>
        private Panel CreateNotificationCard(DataRow row)
        {
            int notiId = Convert.ToInt32(row["NotiID"]);
            string loaiThongBao = row["LoaiThongBao"].ToString();
            string moTa = row["MoTa"].ToString();
            string createdAt = row["CreatedAt"].ToString();
            bool isRead = Convert.ToInt32(row["IsRead"]) == 1;

            int cardWidth = pnlNotifications.ClientSize.Width - 60;
            int minWidth = 1000;
            if (cardWidth < minWidth) cardWidth = minWidth;

            // Main Panel
            Panel card = new Panel
            {
                Width = cardWidth,
                Height = 180,
                Margin = new Padding(0, 0, 0, 20),
                BackColor = isRead ? Color.FromArgb(236, 240, 241) : Color.White,
                Cursor = Cursors.Hand,
                Tag = notiId // Lưu NotiID vào Tag
            };

            card.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(189, 195, 199), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                }
            };

            // Left color bar
            Panel colorBar = new Panel
            {
                Width = 8,
                Height = 130,
                Location = new Point(0, 0),
                BackColor = NotificationManager.GetNotificationColor(loaiThongBao),
                Tag = "colorBar"
            };
            card.Controls.Add(colorBar);

            // Icon
            Label lblIcon = new Label
            {
                Text = NotificationManager.GetNotificationIcon(loaiThongBao),
                Font = new Font("Segoe UI", 32F),
                Location = new Point(25, 35),
                Size = new Size(70, 70),
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = "icon"
            };
            card.Controls.Add(lblIcon);

            int rightButtonsSpace = 400;
            int contentWidth = cardWidth - 115 - rightButtonsSpace;
            if (contentWidth < 450) contentWidth = 450;

            Label lblType = new Label
            {
                Text = loaiThongBao,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = NotificationManager.GetNotificationColor(loaiThongBao),
                Location = new Point(120, 18),
                Size = new Size(240, 40),
                AutoSize = false,
                AutoEllipsis = true,
                Tag = "type"
            };
            card.Controls.Add(lblType);

            Label lblDesc = new Label
            {
                Text = moTa,
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(115, 52),
                Size = new Size(contentWidth, 40),
                AutoSize = false,
                AutoEllipsis = false,
                Tag = "desc"
            };
            card.Controls.Add(lblDesc);

            Label lblTime = new Label
            {
                Text = FormatDateTime(createdAt),
                Font = new Font("Segoe UI", 6F, FontStyle.Italic),
                ForeColor = Color.FromArgb(149, 165, 166),
                Location = new Point(115, 135),
                Size = new Size(220, 30),
                AutoSize = false,
                Tag = "time"
            };
            card.Controls.Add(lblTime);

            int btnRightMargin = 25;
            int btnDeleteX = cardWidth - 170 - btnRightMargin;
            int btnMarkReadX = btnDeleteX - 170 - 15;

            // Mark as read button
            IconButton btnMarkRead = new IconButton
            {
                Text = isRead ? "Đã đọc" : "Đánh dấu",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = isRead ? Color.FromArgb(149, 165, 166) : Color.FromArgb(46, 204, 113),
                Location = new Point(btnMarkReadX, 40),
                Size = new Size(170, 50),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = !isRead,
                IconChar = isRead ? IconChar.CheckCircle : IconChar.Check,
                IconColor = Color.White,
                IconSize = 35,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(10, 0, 10, 0),
                Tag = "btnMarkRead"
            };
            btnMarkRead.FlatAppearance.BorderSize = 0;
            btnMarkRead.Click += (s, e) =>
            {
                // CHỈ UPDATE UI, KHÔNG RELOAD
                if (NotificationManager.MarkAsRead(notiId))
                {
                    UpdateCardAsRead(card, btnMarkRead);
                    UpdateUnreadCount();

                    // Update cache
                    UpdateCacheRowAsRead(notiId);
                }
            };
            card.Controls.Add(btnMarkRead);

            // Delete button
            IconButton btnDelete = new IconButton
            {
                Text = "Xóa",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(231, 76, 60),
                Location = new Point(btnDeleteX, 40),
                Size = new Size(170, 50),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                IconChar = IconChar.TrashAlt,
                IconColor = Color.White,
                IconSize = 35,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(10, 0, 10, 0),
                Tag = "btnDelete"
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += (s, e) =>
            {
                var result = MessageBox.Show("Xóa thông báo này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (NotificationManager.DeleteNotification(notiId))
                    {
                        // XÓA CARD KHỎI UI, KHÔNG RELOAD
                        RemoveCard(notiId);
                        UpdateUnreadCount();

                        // Update cache
                        RemoveCacheRow(notiId);
                    }
                }
            };
            card.Controls.Add(btnDelete);

            // Click vào card để đánh dấu đã đọc
            card.Click += (s, e) =>
            {
                if (!isRead)
                {
                    if (NotificationManager.MarkAsRead(notiId))
                    {
                        UpdateCardAsRead(card, btnMarkRead);
                        UpdateUnreadCount();
                        UpdateCacheRowAsRead(notiId);
                    }
                }
            };

            return card;
        }

        /// <summary>
        /// CHỈ UPDATE UI CỦA 1 CARD thành đã đọc - KHÔNG RELOAD
        /// </summary>
        private void UpdateCardAsRead(Panel card, IconButton btnMarkRead)
        {
            card.BackColor = Color.FromArgb(236, 240, 241);
            btnMarkRead.Text = "Đã đọc";
            btnMarkRead.BackColor = Color.FromArgb(149, 165, 166);
            btnMarkRead.IconChar = IconChar.CheckCircle;
            btnMarkRead.Enabled = false;
        }

        /// <summary>
        /// XÓA 1 CARD KHỎI UI - KHÔNG RELOAD
        /// </summary>
        private void RemoveCard(int notiId)
        {
            if (cardDictionary.ContainsKey(notiId))
            {
                Panel card = cardDictionary[notiId];
                pnlNotifications.Controls.Remove(card);
                cardDictionary.Remove(notiId);
                card.Dispose();

                // Kiểm tra nếu không còn card nào
                if (pnlNotifications.Controls.Count == 0)
                {
                    lblNoData.Visible = true;
                    lblNoData.BringToFront();
                }
            }
        }

        /// <summary>
        /// Update cache khi mark as read
        /// </summary>
        private void UpdateCacheRowAsRead(int notiId)
        {
            if (cachedNotifications != null)
            {
                foreach (DataRow row in cachedNotifications.Rows)
                {
                    if (Convert.ToInt32(row["NotiID"]) == notiId)
                    {
                        row["IsRead"] = 1;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Xóa row khỏi cache
        /// </summary>
        private void RemoveCacheRow(int notiId)
        {
            if (cachedNotifications != null)
            {
                foreach (DataRow row in cachedNotifications.Rows)
                {
                    if (Convert.ToInt32(row["NotiID"]) == notiId)
                    {
                        row.Delete();
                        cachedNotifications.AcceptChanges();
                        break;
                    }
                }
            }
        }

        private string FormatDateTime(string dateTimeStr)
        {
            try
            {
                DateTime dt = DateTime.Parse(dateTimeStr);
                TimeSpan diff = DateTime.Now - dt;

                if (diff.TotalMinutes < 1)
                    return "Vừa xong";
                if (diff.TotalMinutes < 60)
                    return $"{(int)diff.TotalMinutes} phút trước";
                if (diff.TotalHours < 24)
                    return $"{(int)diff.TotalHours} giờ trước";
                if (diff.TotalDays < 7)
                    return $"{(int)diff.TotalDays} ngày trước";

                return dt.ToString("dd/MM/yyyy HH:mm");
            }
            catch
            {
                return dateTimeStr;
            }
        }

        private void UpdateUnreadCount()
        {
            int count = NotificationManager.GetUnreadCount();
            lblUnreadCount.Text = $"Chưa đọc: {count}";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Force refresh cache trong NotificationManager

            // Reload với force refresh
            NotificationManager.GenerateAllNotifications();
            // Force refresh cache trong NotificationManager
            NotificationManager.ForceRefreshNotifications();

            // Reload với force refresh
            LoadNotifications(forceRefresh: true);

            MessageBox.Show("Đã làm mới thông báo!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMarkAllRead_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Đánh dấu tất cả đã đọc?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                NotificationManager.MarkAllAsRead();

                // Update UI tất cả cards thay vì reload
                foreach (var kvp in cardDictionary)
                {
                    Panel card = kvp.Value;
                    IconButton btnMarkRead = card.Controls.OfType<IconButton>()
                        .FirstOrDefault(b => b.Tag?.ToString() == "btnMarkRead");

                    if (btnMarkRead != null)
                    {
                        UpdateCardAsRead(card, btnMarkRead);
                    }
                }

                // Update cache
                if (cachedNotifications != null)
                {
                    foreach (DataRow row in cachedNotifications.Rows)
                    {
                        row["IsRead"] = 1;
                    }
                }

                UpdateUnreadCount();
            }
        }

        private void btnDeleteAllRead_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Xóa tất cả thông báo đã đọc?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                NotificationManager.DeleteAllRead();

                // Xóa cards đã đọc khỏi UI
                var readCards = cardDictionary.Where(kvp =>
                {
                    Panel card = kvp.Value;
                    return card.BackColor == Color.FromArgb(236, 240, 241);
                }).Select(kvp => kvp.Key).ToList();

                foreach (int notiId in readCards)
                {
                    RemoveCard(notiId);
                }

                // Update cache
                if (cachedNotifications != null)
                {
                    var rowsToDelete = cachedNotifications.AsEnumerable()
                        .Where(r => Convert.ToInt32(r["IsRead"]) == 1)
                        .ToList();

                    foreach (var row in rowsToDelete)
                    {
                        row.Delete();
                    }
                    cachedNotifications.AcceptChanges();
                }

                UpdateUnreadCount();
            }
        }

        private void chkUnreadOnly_CheckedChanged(object sender, EventArgs e)
        {
            // Phải force refresh vì filter thay đổi
            LoadNotifications(forceRefresh: true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NotificationForm_Load_1(object sender, EventArgs e)
        {
        }

        private void lblNoData_Click(object sender, EventArgs e)
        {
        }

        private void lblNoData_Click_1(object sender, EventArgs e)
        {
        }
    }
}
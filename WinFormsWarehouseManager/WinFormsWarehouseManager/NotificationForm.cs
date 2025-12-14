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
        public NotificationForm()
        {
            InitializeComponent();
            this.Resize += NotificationForm_Resize;


        }

        private void NotificationForm_Load(object sender, EventArgs e)
        {
            NotificationManager.GenerateAllNotifications();

            // Load danh sách thông báo
            LoadNotifications();
        }


        /// <summary>
        /// Load và hiển thị danh sách thông báo
        /// </summary>
        private void LoadNotifications()
        {
            try
            {
                // Clear panel
                pnlNotifications.Controls.Clear();

                // Lấy danh sách thông báo
                bool unreadOnly = chkUnreadOnly.Checked;
                DataTable dt = NotificationManager.GetUserNotifications(unreadOnly);

                if (dt == null || dt.Rows.Count == 0)
                {
                    lblNoData.Visible = true;
                    lblNoData.BringToFront();
                    UpdateUnreadCount();
                    return;
                }

                lblNoData.Visible = false;

                // Tạo card cho mỗi thông báo
                foreach (DataRow row in dt.Rows)
                {
                    Panel card = CreateNotificationCard(row);
                    pnlNotifications.Controls.Add(card);
                }

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
        /// Tạo card hiển thị thông báo
        /// </summary>
        private Panel CreateNotificationCard(DataRow row)
        {
            int notiId = Convert.ToInt32(row["NotiID"]);
            string loaiThongBao = row["LoaiThongBao"].ToString();
            string moTa = row["MoTa"].ToString();
            string createdAt = row["CreatedAt"].ToString();
            bool isRead = Convert.ToInt32(row["IsRead"]) == 1;

            // Tính toán width động dựa trên pnlNotifications
            int cardWidth = pnlNotifications.ClientSize.Width - 60; // Trừ padding

            // Điều chỉnh width tối thiểu dựa trên container
            int minWidth = 1000; // Tăng từ 800 lên 1000
            if (cardWidth < minWidth) cardWidth = minWidth;

            // Main Panel
            Panel card = new Panel
            {
                Width = cardWidth,
                Height = 180, 
                Margin = new Padding(0, 0, 0, 20),
                BackColor = isRead ? Color.FromArgb(236, 240, 241) : Color.White,
                Cursor = Cursors.Hand
            };
  
            // Border effect
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
                BackColor = NotificationManager.GetNotificationColor(loaiThongBao)
            };
            card.Controls.Add(colorBar);

            // Icon
            Label lblIcon = new Label
            {
                Text = NotificationManager.GetNotificationIcon(loaiThongBao),
                Font = new Font("Segoe UI", 32F),
                Location = new Point(25, 35),
                Size = new Size(70, 70),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblIcon);

            // Type label - responsive width với multiline
            int rightButtonsSpace = 400; // Khoảng trống cho 2 buttons bên phải
            int contentWidth = cardWidth - 115 - rightButtonsSpace; // 115 = icon space
            if (contentWidth < 450) contentWidth = 400; // Tăng minimum từ 400 lên 450

            Label lblType = new Label
            {
                Text = loaiThongBao,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold), // Tăng từ 13F lên 14F
                ForeColor = NotificationManager.GetNotificationColor(loaiThongBao),
                Location = new Point(120, 18),
                Size = new Size(240, 40),
                AutoSize = false,
                AutoEllipsis = true
            };
            card.Controls.Add(lblType);

            // Description - responsive width với multiline và ellipsis
            Label lblDesc = new Label
            {
                Text = moTa,
                Font = new Font("Segoe UI", 11F), // Tăng từ 11.5F lên 12F
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(115, 52),
                Size = new Size(contentWidth, 40), // Giảm từ 70 xuống 50
                AutoSize = false,
                AutoEllipsis = false
            };
            card.Controls.Add(lblDesc);

            // Time label
            Label lblTime = new Label
            {
                Text = FormatDateTime(createdAt),
                Font = new Font("Segoe UI", 6F, FontStyle.Italic),
                ForeColor = Color.FromArgb(149, 165, 166),
                Location = new Point(115, 135), // Điều chỉnh vị trí
                Size = new Size(220, 30),
                AutoSize = false
            };
            card.Controls.Add(lblTime);

            // Tính toán vị trí buttons từ bên phải
            int btnRightMargin = 25;
            int btnDeleteX = cardWidth - 170 - btnRightMargin; // Giảm width button
            int btnMarkReadX = btnDeleteX - 170 - 15; // Giảm width button

            // Mark as read button với IconButton
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
                Padding = new Padding(10, 0, 10, 0)
            };
            btnMarkRead.FlatAppearance.BorderSize = 0;
            btnMarkRead.Click += (s, e) =>
            {
                if (NotificationManager.MarkAsRead(notiId))
                {
                    LoadNotifications();
                }
            };
            card.Controls.Add(btnMarkRead);

            // Delete button với IconButton
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
                Padding = new Padding(10, 0, 10, 0)
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
                        LoadNotifications();
                    }
                }
            };
            card.Controls.Add(btnDelete);

            // Click vào card để đánh dấu đã đọc
            card.Click += (s, e) =>
            {
                if (!isRead)
                {
                    NotificationManager.MarkAsRead(notiId);
                    LoadNotifications();
                }
            };

            return card;
        }

        /// <summary>
        /// Format datetime thân thiện
        /// </summary>
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

        /// <summary>
        /// Cập nhật số lượng thông báo chưa đọc
        /// </summary>
        private void UpdateUnreadCount()
        {
            int count = NotificationManager.GetUnreadCount();
            lblUnreadCount.Text = $"Chưa đọc: {count}";
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            NotificationManager.GenerateAllNotifications();
            LoadNotifications();
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
                LoadNotifications();
            }
        }

        private void btnDeleteAllRead_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Xóa tất cả thông báo đã đọc?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                NotificationManager.DeleteAllRead();
                LoadNotifications();
            }
        }
        
        private void chkUnreadOnly_CheckedChanged(object sender, EventArgs e)
        {
            LoadNotifications();
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

        /// <summary>
        /// Xử lý khi resize form - reload lại cards với width mới
        /// </summary>
        private void NotificationForm_Resize(object sender, EventArgs e)
        {
            // Chỉ reload nếu có thông báo
            if (pnlNotifications.Controls.Count > 0)
            {
                LoadNotifications();
            }
        }

    }
}

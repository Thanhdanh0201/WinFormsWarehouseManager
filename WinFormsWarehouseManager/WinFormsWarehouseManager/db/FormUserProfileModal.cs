using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using FontAwesome.Sharp;
using WinFormsWarehouseManager.Models;
using WinFormsWarehouseManager.db;

namespace WinFormsWarehouseManager.Forms
{
    public partial class UserProfileModal : Form
    {
        private DatabaseHelper dbHelper;
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public UserProfileModal()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            LoadUserProfile();
            ApplyCardShadows();
        }

        private void ApplyCardShadows()
        {
            // Thêm shadow cho các panel card
            panelInfo.Paint += (s, e) => DrawCardShadow(e.Graphics, panelInfo);
            panelActivity.Paint += (s, e) => DrawCardShadow(e.Graphics, panelActivity);
        }

        private void DrawCardShadow(Graphics g, Panel panel)
        {
            // Vẽ border nhẹ cho card
            using (Pen pen = new Pen(Color.FromArgb(220, 225, 230), 1))
            {
                g.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        private void CreateInfoSection()
        {
            // Title với icon
            IconPictureBox iconInfo = new IconPictureBox
            {
                IconChar = IconChar.UserCircle,
                IconColor = Color.FromArgb(0, 162, 173),
                IconSize = 24,
                Location = new Point(5, 5),
                Size = new Size(24, 24)
            };

            Label lblInfoTitle = new Label
            {
                Text = "Thông tin cá nhân",
                ForeColor = Color.FromArgb(40, 45, 55),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(35, 5),
                AutoSize = true
            };

            panelInfo.Controls.Add(iconInfo);
            panelInfo.Controls.Add(lblInfoTitle);
        }

        private void CreateActivitySection()
        {
            // Title với icon
            IconPictureBox iconActivity = new IconPictureBox
            {
                IconChar = IconChar.ClockRotateLeft,
                IconColor = Color.FromArgb(0, 162, 173),
                IconSize = 24,
                Location = new Point(5, 5),
                Size = new Size(24, 24)
            };

            Label lblActivityTitle = new Label
            {
                Text = "Hoạt động gần đây",
                ForeColor = Color.FromArgb(40, 45, 55),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(35, 5),
                AutoSize = true
            };

            // Flow panel for activities
            flowActivity = new FlowLayoutPanel
            {
                Location = new Point(5, 45),
                Size = new Size(550, 215),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(5)
            };

            panelActivity.Controls.Add(iconActivity);
            panelActivity.Controls.Add(lblActivityTitle);
            panelActivity.Controls.Add(flowActivity);
        }

        private void LoadUserProfile()
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Không có người dùng đang đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Load avatar
            LoadAvatar(UserSession.CurrentUserEmail);

            // Load user info
            LoadUserInfo();

            // Load recent activities
            LoadRecentActivities();
        }

        private void LoadAvatar(string email)
        {
            try
            {
                // Tạo Gravatar URL từ email
                string gravatarUrl = GetGravatarUrl(email, 140);

                using (WebClient client = new WebClient())
                {
                    byte[] imageData = client.DownloadData(gravatarUrl);
                    using (var ms = new System.IO.MemoryStream(imageData))
                    {
                        picAvatar.Image = Image.FromStream(ms);
                    }
                }
            }
            catch
            {
                // Nếu không có avatar Gravatar, tạo avatar mặc định với chữ cái đầu
                CreateDefaultAvatar(UserSession.CurrentUserName);
            }
        }

        private string GetGravatarUrl(string email, int size)
        {
            // Tạo MD5 hash của email (lowercase, trim)
            string emailLower = email.Trim().ToLower();
            string hash = GetMD5Hash(emailLower);

            // URL Gravatar với fallback là 404 (để catch và tạo avatar mặc định)
            return $"https://www.gravatar.com/avatar/{hash}?s={size}&d=404";
        }

        private string GetMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private void CreateDefaultAvatar(string fullName)
        {
            // Lấy chữ cái đầu của tên
            string initial = GetInitials(fullName);

            // Tạo bitmap
            Bitmap bmp = new Bitmap(140, 140);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Background màu teal
                g.Clear(Color.FromArgb(0, 162, 173));

                // Vẽ chữ cái
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                g.DrawString(initial, new Font("Segoe UI", 48, FontStyle.Bold),
                    Brushes.White, new RectangleF(0, 0, 140, 140), sf);
            }

            picAvatar.Image = bmp;
        }

        private string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return "?";

            string[] parts = fullName.Trim().Split(' ');
            if (parts.Length >= 2)
            {
                // Lấy chữ cái đầu của tên và họ
                return (parts[0][0].ToString() + parts[parts.Length - 1][0].ToString()).ToUpper();
            }
            else
            {
                return parts[0][0].ToString().ToUpper();
            }
        }

        private void LoadUserInfo()
        {
            string query = @"SELECT FullName, BirthDate, Email, 
                            datetime(CreatedAt, 'localtime') as CreatedAt 
                            FROM Users WHERE UserID = @UserID";

            var parameters = new System.Data.SQLite.SQLiteParameter[]
            {
                new System.Data.SQLite.SQLiteParameter("@UserID", UserSession.CurrentUserID)
            };

            var dt = dbHelper.ExecuteQuery(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                int yPos = 50;

                // Full Name
                AddInfoRow(panelInfo, IconChar.User, "Họ và tên", row["FullName"].ToString(), yPos);
                yPos += 48;

                // Birth Date
                if (row["BirthDate"] != DBNull.Value)
                {
                    DateTime birthDate = Convert.ToDateTime(row["BirthDate"]);
                    AddInfoRow(panelInfo, IconChar.Calendar, "Ngày sinh", birthDate.ToString("dd/MM/yyyy"), yPos);
                    yPos += 48;
                }

                // Email
                AddInfoRow(panelInfo, IconChar.Envelope, "Email", row["Email"].ToString(), yPos);
                yPos += 48;

                // Member since
                if (row["CreatedAt"] != DBNull.Value)
                {
                    DateTime createdAt = Convert.ToDateTime(row["CreatedAt"]);
                    AddInfoRow(panelInfo, IconChar.Clock, "Tham gia từ", createdAt.ToString("dd/MM/yyyy HH:mm"), yPos);
                }
            }
        }

        private void AddInfoRow(Panel parent, IconChar icon, string label, string value, int yPos)
        {
            IconPictureBox iconBox = new IconPictureBox
            {
                IconChar = icon,
                IconColor = Color.FromArgb(0, 162, 173),
                IconSize = 20,
                Location = new Point(15, yPos + 3),
                Size = new Size(20, 20)
            };

            Label lblLabel = new Label
            {
                Text = label,
                ForeColor = Color.FromArgb(108, 117, 125),
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(36, yPos),
                Size = new Size(133, 26),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblValue = new Label
            {
                Text = value,
                ForeColor = Color.FromArgb(33, 37, 41),
                Font = new Font("Segoe UI", 8F),
                Location = new Point(170, yPos-5),
                AutoSize = true,
                MaximumSize = new Size(450, 0),
                TextAlign = ContentAlignment.MiddleLeft

            };

            parent.Controls.Add(iconBox);
            parent.Controls.Add(lblLabel);
            parent.Controls.Add(lblValue);
        }

        private void LoadRecentActivities()
        {
            string query = @"SELECT LoaiHanhDong, Description, 
                            datetime(CreatedAt, 'localtime') as CreatedAt 
                            FROM ActivityLog 
                            WHERE UserID = @UserID 
                            ORDER BY CreatedAt DESC 
                            LIMIT 20";

            var parameters = new System.Data.SQLite.SQLiteParameter[]
            {
                new System.Data.SQLite.SQLiteParameter("@UserID", UserSession.CurrentUserID)
            };

            var dt = dbHelper.ExecuteQuery(query, parameters);

            flowActivity.Controls.Clear();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    string loaiHanhDong = row["LoaiHanhDong"].ToString();
                    string description = row["Description"].ToString();
                    DateTime createdAt = Convert.ToDateTime(row["CreatedAt"]);

                    // Kết hợp loại hành động và mô tả
                    string actionText = string.IsNullOrEmpty(description)
                        ? loaiHanhDong
                        : $"{loaiHanhDong}: {description}";

                    AddActivityItem(actionText, createdAt);
                }
            }
            else
            {
                Panel emptyPanel = new Panel
                {
                    Width = 530,
                    Height = 80,
                    BackColor = Color.FromArgb(248, 249, 250),
                    Padding = new Padding(20)
                };

                Label lblEmpty = new Label
                {
                    Text = "Chưa có hoạt động nào được ghi nhận",
                    ForeColor = Color.FromArgb(108, 117, 125),
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    AutoSize = true,
                    Location = new Point(20, 30)
                };

                emptyPanel.Controls.Add(lblEmpty);
                flowActivity.Controls.Add(emptyPanel);
            }
        }

        private void AddActivityItem(string action, DateTime timestamp)
        {
            Panel itemPanel = new Panel
            {
                Width = 530,
                Height = 70,
                BackColor = Color.FromArgb(248, 249, 250),
                Margin = new Padding(0, 0, 0, 8),
                Padding = new Padding(15)
            };

            // Vẽ border nhẹ
            itemPanel.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(220, 225, 230), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, itemPanel.Width - 1, itemPanel.Height - 1);
                }
            };

            // Icon based on action type
            IconChar actionIcon = GetIconForAction(action);
            Color iconColor = GetColorForAction(action);

            IconPictureBox icon = new IconPictureBox
            {
                IconChar = actionIcon,
                IconColor = iconColor,
                IconSize = 20,
                Location = new Point(10, 21),
                Size = new Size(20, 20)
            };

            // Action text
            Label lblAction = new Label
            {
                Text = action,
                ForeColor = Color.FromArgb(33, 37, 41),
                Font = new Font("Segoe UI", 8F),
                Location = new Point(45, 10),
                AutoSize = true,
                MaximumSize = new Size(385, 0)
            };

            // Full timestamp với ngày giờ
            string timeText = timestamp.ToString("dd/MM/yyyy HH:mm");
            Label lblTime = new Label
            {
                Text = timeText,
                ForeColor = Color.FromArgb(108, 117, 125),
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(45, 36),
                AutoSize = true
            };

            // Time ago (relative time) - bên phải
            string timeAgo = GetTimeAgo(timestamp);
            Label lblTimeAgo = new Label
            {
                Text = timeAgo,
                ForeColor = Color.FromArgb(0, 162, 173),
                Font = new Font("Segoe UI", 6F, FontStyle.Italic),
                Location = new Point(410, 23),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight
            };

            itemPanel.Controls.Add(icon);
            itemPanel.Controls.Add(lblAction);
            itemPanel.Controls.Add(lblTime);
            itemPanel.Controls.Add(lblTimeAgo);

            flowActivity.Controls.Add(itemPanel);
        }

        private IconChar GetIconForAction(string action)
        {
            if (action.Contains("Đăng nhập") || action.Contains("Login"))
                return IconChar.SignInAlt;
            else if (action.Contains("Đăng xuất") || action.Contains("Logout"))
                return IconChar.SignOutAlt;
            else if (action.Contains("Thêm") || action.Contains("Nhập kho"))
                return IconChar.Plus;
            else if (action.Contains("Xóa"))
                return IconChar.Trash;
            else if (action.Contains("Cập nhật") || action.Contains("Sửa"))
                return IconChar.Edit;
            else if (action.Contains("Xuất kho"))
                return IconChar.FileExport;
            else
                return IconChar.InfoCircle;
        }

        private Color GetColorForAction(string action)
        {
            if (action.Contains("Đăng nhập") || action.Contains("Thêm") || action.Contains("Nhập kho"))
                return Color.FromArgb(40, 167, 69);
            else if (action.Contains("Đăng xuất"))
                return Color.FromArgb(255, 193, 7);
            else if (action.Contains("Xóa"))
                return Color.FromArgb(220, 53, 69);
            else if (action.Contains("Xuất kho"))
                return Color.FromArgb(253, 126, 20);
            else
                return Color.FromArgb(0, 162, 173);
        }

        private string GetTimeAgo(DateTime timestamp)
        {
            TimeSpan diff = DateTime.Now - timestamp;

            if (diff.TotalMinutes < 1)
                return "Vừa xong";
            else if (diff.TotalMinutes < 60)
                return $"{(int)diff.TotalMinutes} phút trước";
            else if (diff.TotalHours < 24)
                return $"{(int)diff.TotalHours} giờ trước";
            else if (diff.TotalDays < 7)
                return $"{(int)diff.TotalDays} ngày trước";
            else
                return timestamp.ToString("dd/MM/yyyy");
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            // Đóng modal khi nhấn ESC
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Vẽ shadow mờ xung quanh form
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.FromArgb(180, 190, 200), 2))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            panelHeader.MouseDown += (s, ev) =>
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            };

            panelHeader.MouseMove += (s, ev) =>
            {
                if (dragging)
                {
                    Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                    this.Location = Point.Add(dragFormPoint, new Size(diff));
                }
            };

            panelHeader.MouseUp += (s, ev) => { dragging = false; };


            // Đảm bảo nút close luôn hiển thị và có thể click
            btnClose.BringToFront();
        }
    }
}
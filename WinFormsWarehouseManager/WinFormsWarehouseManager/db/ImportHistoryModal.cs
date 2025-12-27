using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using WinFormsWarehouseManager.Models;
using WinFormsWarehouseManager.db;

namespace WinFormsWarehouseManager.Forms
{
    public partial class ImportHistoryModal : Form
    {
        private DatabaseHelper dbHelper;
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private Panel panelHeader;
        private IconButton iconbtnExit;
        private Panel panelHistory;
        private FlowLayoutPanel flowHistory;

        public ImportHistoryModal()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            InitializeUI();
        }

        private void InitializeComponent()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.KeyPreview = true;
        }

        private void InitializeUI()
        {
            // Header Panel
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(0, 162, 173)
            };

            // Icon và Title
            IconPictureBox iconHeader = new IconPictureBox
            {
                IconChar = IconChar.ClockRotateLeft,
                IconColor = Color.White,
                IconSize = 32,
                Location = new Point(20, 14),
                Size = new Size(32, 32)
            };

            Label lblTitle = new Label
            {
                Text = "Lịch sử Nhập hàng",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15.5F, FontStyle.Bold),
                Location = new Point(60, 13),
                AutoSize = true
            };

            // Exit button - giống Login form
            iconbtnExit = new IconButton
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                IconChar = IconChar.X,
                IconColor = Color.White,
                IconFont = IconFont.Auto,
                IconSize = 30,
                Size = new Size(45, 45),
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = false
            };
            iconbtnExit.FlatAppearance.BorderSize = 0;
            iconbtnExit.Click += IconbtnExit_Click;
            iconbtnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            panelHeader.Controls.Add(iconHeader);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(iconbtnExit);

            // History Panel
            panelHistory = new Panel
            {
                Location = new Point(20, 80),
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            panelHistory.Paint += (s, e) => DrawCardShadow(e.Graphics, panelHistory);

            // Title section
            IconPictureBox iconHistory = new IconPictureBox
            {
                IconChar = IconChar.None,
                IconColor = Color.FromArgb(0, 162, 173),
                IconSize = 24,
                Location = new Point(15, 15),
                Size = new Size(24, 24)
            };

            Label lblHistoryTitle = new Label
            {
                Text = "Danh sách lịch sử nhập kho",
                ForeColor = Color.FromArgb(40, 45, 55),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(45, 15),
                AutoSize = true
            };

            // Flow panel for history items
            flowHistory = new FlowLayoutPanel
            {
                Location = new Point(10, 55),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(5),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            panelHistory.Controls.Add(iconHistory);
            panelHistory.Controls.Add(lblHistoryTitle);
            panelHistory.Controls.Add(flowHistory);

            this.Controls.Add(panelHeader);
            this.Controls.Add(panelHistory);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Tính toán kích thước 75% của form chính KHI modal được hiển thị
            if (this.Owner != null)
            {
                this.Width = (int)(this.Owner.Width * 0.75);
                this.Height = (int)(this.Owner.Height * 0.75);

                // Center modal trên form chính
                this.Location = new Point(
                    this.Owner.Location.X + (this.Owner.Width - this.Width) / 2,
                    this.Owner.Location.Y + (this.Owner.Height - this.Height) / 2
                );
            }
            else
            {
                this.Size = new Size(900, 600);
                this.CenterToScreen();
            }

            // Cập nhật vị trí exit button
            iconbtnExit.Location = new Point(this.Width - 50, 7);

            // Cập nhật kích thước panelHistory
            panelHistory.Size = new Size(this.Width - 40, this.Height - 100);
            flowHistory.Size = new Size(panelHistory.Width - 20, panelHistory.Height - 65);

            LoadImportHistory();
        }

        private void DrawCardShadow(Graphics g, Panel panel)
        {
            using (Pen pen = new Pen(Color.FromArgb(220, 225, 230), 1))
            {
                g.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        private void LoadImportHistory()
        {
            string query = @"
                SELECT 
                    ir.ImportID,
                    ir.ImportDate,
                    u.FullName as UserName,
                    p.ProductName,
                    ir.Quantity,
                    p.DonViTinh
                FROM ImportReceipts ir
                INNER JOIN Users u ON ir.UserID = u.UserID
                INNER JOIN Products p ON ir.ProductID = p.ProductID
                ORDER BY datetime(ir.ImportDate) DESC
                LIMIT 100";

            var dt = dbHelper.ExecuteQuery(query);

            flowHistory.Controls.Clear();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    string userName = row["UserName"].ToString();
                    string productName = row["ProductName"].ToString();
                    int quantity = Convert.ToInt32(row["Quantity"]);
                    string donViTinh = row["DonViTinh"].ToString();
                    DateTime importDate = Convert.ToDateTime(row["ImportDate"]);

                    AddHistoryItem(userName, productName, quantity, donViTinh, importDate);
                }
            }
            else
            {
                ShowEmptyState();
            }
        }

        private void AddHistoryItem(string userName, string productName, int quantity, string donViTinh, DateTime importDate)
        {
            Panel itemPanel = new Panel
            {
                Width = flowHistory.Width - 30,
                Height = 65,
                BackColor = Color.FromArgb(248, 249, 250),
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(15, 10, 15, 10)
            };

            itemPanel.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(220, 225, 230), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, itemPanel.Width - 1, itemPanel.Height - 1);
                }
            };

            // Icon nhập kho - Bên trái
            IconPictureBox icon = new IconPictureBox
            {
                IconChar = IconChar.BoxesPacking,
                IconColor = Color.FromArgb(40, 167, 69),
                IconSize = 32,
                Location = new Point(15, 16),
                Size = new Size(32, 32)
            };

            // User name - Hàng 1, cột 1
            Label lblUser = new Label
            {
                Text = userName,
                ForeColor = Color.FromArgb(0, 162, 173),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Location = new Point(60, 5),
                AutoSize = true
            };

            // Product name - Hàng 2, cột 1
            Label lblProduct = new Label
            {
                Text = $"Nhập kho: {productName}",
                ForeColor = Color.FromArgb(33, 37, 41),
                Font = new Font("Segoe UI", 6.5F),
                Location = new Point(60, 35),
                AutoSize = true,
                MaximumSize = new Size(350, 0)
            };

            // Quantity - Hàng 1, cột 2 (giữa)
            int centerX = itemPanel.Width / 2 + 50;
            Label lblQuantity = new Label
            {
                Text = $"{quantity:N0} {donViTinh}",
                ForeColor = Color.FromArgb(40, 167, 69),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Location = new Point(centerX, 12),
                AutoSize = true
            };

            // Icon quantity
            IconPictureBox iconQty = new IconPictureBox
            {
                IconChar = IconChar.None,
                IconColor = Color.FromArgb(40, 167, 69),
                IconSize = 16,
                Location = new Point(centerX - 22, 14),
                Size = new Size(16, 16)
            };

            // Import date - Hàng 1, cột 3 (bên phải)
            string dateText = importDate.ToString("dd/MM/yyyy HH:mm");
            Label lblDate = new Label
            {
                Text = dateText,
                ForeColor = Color.FromArgb(108, 117, 125),
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(itemPanel.Width - 195, 10),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight
            };

            // Time ago - Hàng 2, cột 3 (bên phải)
            string timeAgo = GetTimeAgo(importDate);
            Label lblTimeAgo = new Label
            {
                Text = timeAgo,
                ForeColor = Color.FromArgb(0, 162, 173),
                Font = new Font("Segoe UI", 7.5F, FontStyle.Italic),
                Location = new Point(itemPanel.Width - 150, 33),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight
            };

            itemPanel.Controls.Add(icon);
            itemPanel.Controls.Add(lblUser);
            itemPanel.Controls.Add(lblProduct);
            itemPanel.Controls.Add(iconQty);
            itemPanel.Controls.Add(lblQuantity);
            itemPanel.Controls.Add(lblDate);
            itemPanel.Controls.Add(lblTimeAgo);

            flowHistory.Controls.Add(itemPanel);
        }

        private void ShowEmptyState()
        {
            Panel emptyPanel = new Panel
            {
                Width = flowHistory.Width - 30,
                Height = 100,
                BackColor = Color.FromArgb(248, 249, 250)
            };

            IconPictureBox emptyIcon = new IconPictureBox
            {
                IconChar = IconChar.BoxOpen,
                IconColor = Color.FromArgb(108, 117, 125),
                IconSize = 48,
                Location = new Point((emptyPanel.Width - 48) / 2, 15),
                Size = new Size(48, 48)
            };

            Label lblEmpty = new Label
            {
                Text = "Chưa có lịch sử nhập hàng nào",
                ForeColor = Color.FromArgb(108, 117, 125),
                Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter
            };
            lblEmpty.Location = new Point((emptyPanel.Width - lblEmpty.Width) / 2, 70);

            emptyPanel.Controls.Add(emptyIcon);
            emptyPanel.Controls.Add(lblEmpty);
            flowHistory.Controls.Add(emptyPanel);
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
            else if (diff.TotalDays < 30)
                return $"{(int)(diff.TotalDays / 7)} tuần trước";
            else if (diff.TotalDays < 365)
                return $"{(int)(diff.TotalDays / 30)} tháng trước";
            else
                return timestamp.ToString("dd/MM/yyyy");
        }

        private void IconbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(Color.FromArgb(180, 190, 200), 2))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Drag form functionality
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

            iconbtnExit.BringToFront();
        }
    }
}
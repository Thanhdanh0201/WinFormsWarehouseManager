using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using WinFormsWarehouseManager.Models;

namespace WinFormsWarehouseManager.Forms
{
    public partial class FormDeleteConfirmation : Form
    {
        public bool IsConfirmed { get; private set; }
        private List<ProductInfo> productsToDelete;
        private Timer fadeTimer;
        private double opacity = 0;

        public FormDeleteConfirmation(List<ProductInfo> products)
        {
            InitializeComponent();
            productsToDelete = products;
            InitializeUI();
            SetupAnimation();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormDeleteConfirmation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDeleteConfirmation";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xác nhận";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormDeleteConfirmation_Load);
            this.ResumeLayout(false);
        }

        private void InitializeUI()
        {
            this.Paint += FormDeleteConfirmation_Paint;

            // Title Label
            Label lblTitle = new Label
            {
                Text = "XÁC NHẬN XÓA SẢN PHẨM",
                Location = new Point(30, 25),
                Width = 440,
                Height = 35,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(231, 76, 60),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };
            this.Controls.Add(lblTitle);

            // Divider
            Panel divider = new Panel
            {
                Location = new Point(30, 65),
                Width = 440,
                Height = 2,
                BackColor = Color.FromArgb(236, 240, 241)
            };
            this.Controls.Add(divider);

            // Warning icon
            IconPictureBox iconWarning = new IconPictureBox
            {
                Location = new Point(220, 80),
                Size = new Size(60, 60),
                IconChar = IconChar.ExclamationTriangle,
                IconColor = Color.FromArgb(231, 76, 60),
                IconSize = 60
            };
            this.Controls.Add(iconWarning);

            // Warning message
            Label lblWarning = new Label
            {
                Text = $"Bạn có chắc chắn muốn xóa {productsToDelete.Count} sản phẩm?",
                Location = new Point(30, 150),
                Width = 440,
                Height = 30,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };
            this.Controls.Add(lblWarning);

            Label lblSubWarning = new Label
            {
                Text = "Hành động này không thể hoàn tác!",
                Location = new Point(30, 185),
                Width = 440,
                Height = 25,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Italic),
                ForeColor = Color.FromArgb(231, 76, 60),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };
            this.Controls.Add(lblSubWarning);

            // Product list with panel container
            Panel productListPanel = new Panel
            {
                Location = new Point(30, 220),
                Size = new Size(440, 140),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(250, 250, 250),
                AutoScroll = true
            };

            ListBox lstProducts = new ListBox
            {
                Location = new Point(5, 5),
                Size = new Size(425, 130),
                Font = new Font("Segoe UI", 9.5F),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(250, 250, 250),
                SelectionMode = SelectionMode.None
            };

            foreach (var product in productsToDelete)
            {
                // Adjust property names based on your ProductInfo class
                string displayText = $"• {product.ProductName}";

                // Add quantity if available
                if (!string.IsNullOrEmpty(product.SoLuong.ToString()))
                {
                    displayText += $" (SL: {product.SoLuong}";
                    if (!string.IsNullOrEmpty(product.DonViTinh))
                    {
                        displayText += $" {product.DonViTinh}";
                    }
                    displayText += ")";
                }

                lstProducts.Items.Add(displayText);
            }

            productListPanel.Controls.Add(lstProducts);
            this.Controls.Add(productListPanel);

            // Confirm button
            IconButton btnConfirm = new IconButton
            {
                Location = new Point(30, 375),
                Size = new Size(210, 45),
                Text = " Xác nhận",
                IconChar = IconChar.Check,
                IconColor = Color.White,
                IconSize = 20,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.Click += BtnConfirm_Click;
            btnConfirm.MouseEnter += (s, e) => btnConfirm.BackColor = Color.FromArgb(192, 57, 43);
            btnConfirm.MouseLeave += (s, e) => btnConfirm.BackColor = Color.FromArgb(231, 76, 60);
            this.Controls.Add(btnConfirm);

            // Cancel button
            IconButton btnCancel = new IconButton
            {
                Location = new Point(260, 375),
                Size = new Size(210, 45),
                Text = " Hủy",
                IconChar = IconChar.Times,
                IconColor = Color.White,
                IconSize = 20,
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;
            btnCancel.MouseEnter += (s, e) => btnCancel.BackColor = Color.FromArgb(108, 122, 137);
            btnCancel.MouseLeave += (s, e) => btnCancel.BackColor = Color.FromArgb(127, 140, 141);
            this.Controls.Add(btnCancel);

            // ESC to close
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    BtnCancel_Click(null, null);
                }
            };
        }

        private void SetupAnimation()
        {
            fadeTimer = new Timer();
            fadeTimer.Interval = 10;
            fadeTimer.Tick += FadeTimer_Tick;
            fadeTimer.Start();
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            opacity += 0.05;
            if (opacity >= 1)
            {
                opacity = 1;
                fadeTimer.Stop();
            }
            this.Opacity = opacity;
        }

        private void FormDeleteConfirmation_Paint(object sender, PaintEventArgs e)
        {
            // Draw rounded border
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = GetRoundedRectPath(this.ClientRectangle, 15))
            {
                this.Region = new Region(path);

                // Draw shadow
                using (Pen shadowPen = new Pen(Color.FromArgb(50, 0, 0, 0), 3))
                {
                    Rectangle shadowRect = this.ClientRectangle;
                    shadowRect.Inflate(-2, -2);
                    using (GraphicsPath shadowPath = GetRoundedRectPath(shadowRect, 15))
                    {
                        e.Graphics.DrawPath(shadowPen, shadowPath);
                    }
                }
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            // Adjust rectangle to prevent cutting
            rect.Width -= 1;
            rect.Height -= 1;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            IsConfirmed = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            IsConfirmed = false;
            this.Close();
        }

        private void FormDeleteConfirmation_Load(object sender, EventArgs e)
        {
            // Force size to prevent scaling issues
            this.Size = new Size(500, 450);
            this.MinimumSize = new Size(500, 450);
            this.MaximumSize = new Size(500, 450);

            // Center on screen if parent is not available
            if (this.Owner == null)
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
        }
    }
}
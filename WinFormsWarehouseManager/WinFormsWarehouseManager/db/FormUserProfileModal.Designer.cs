using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace WinFormsWarehouseManager.Forms
{
    partial class UserProfileModal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties - Modal rộng và cao hơn
            this.ClientSize = new Size(650, 750);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.ShowInTaskbar = false;
            this.KeyPreview = true;

            // Header Panel - Cao hơn một chút
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(2, 51, 66),

                //BackColor = Color.FromArgb(0, 162, 173),
                Padding = new Padding(20, 0, 20, 0)
            };

            // Title
            lblTitle = new Label
            {
                Text = "Thông tin tài khoản",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 5)
            };

            // Close button
            btnClose = new IconButton
            {
                IconChar = IconChar.Xmark,
                IconColor = Color.White,
                IconSize = 26,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(45, 45),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(595, 8),
                BackColor = Color.Transparent
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 83, 80);
            btnClose.Click += BtnClose_Click;

            panelHeader.Controls.AddRange(new Control[] { lblTitle, btnClose });

            // Content Panel
            panelContent = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(25),
                AutoScroll = true,
                BackColor = Color.FromArgb(245, 247, 250)
            };

            // Avatar - Bo tròn ở giữa, lớn hơn
            picAvatar = new CircularPictureBox
            {
                Size = new Size(140, 140),
                Location = new Point(255, 55),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(0, 162, 173),
                BorderStyle = BorderStyle.None
            };

            // Info Panel - Card style với shadow
            panelInfo = new Panel
            {
                Location = new Point(35, 195),
                Size = new Size(580, 240),
                BackColor = Color.White,
                Padding = new Padding(25)
            };
            CreateInfoSection();

            // Separator line - Thêm margin
            Panel separator = new Panel
            {
                Location = new Point(35, 455),
                Size = new Size(580, 2),
                BackColor = Color.FromArgb(220, 225, 230)
            };

            // Activity Panel - Card style
            panelActivity = new Panel
            {
                Location = new Point(35, 477),
                Size = new Size(580, 280),
                BackColor = Color.White,
                Padding = new Padding(25),
                AutoScroll = false
            };
            CreateActivitySection();

            panelContent.Controls.AddRange(new Control[] { picAvatar, panelInfo, separator, panelActivity });

            this.Controls.AddRange(new Control[] { panelHeader, panelContent });
            this.ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Panel panelContent;
        private Panel panelInfo;
        private Panel panelActivity;
        private IconButton btnClose;
        private Label lblTitle;
        private CircularPictureBox picAvatar;
        private FlowLayoutPanel flowActivity;
    }

    /// <summary>
    /// CircularPictureBox - PictureBox bo tròn cho avatar với border
    /// </summary>
    public class CircularPictureBox : PictureBox
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            // Tạo hình tròn
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, this.Width - 1, this.Height - 1);
            this.Region = new Region(gp);

            base.OnPaint(pe);

            // Vẽ border tròn
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(Color.FromArgb(220, 225, 230), 3))
            {
                pe.Graphics.DrawEllipse(pen, 1, 1, this.Width - 3, this.Height - 3);
            }
        }
    }
}
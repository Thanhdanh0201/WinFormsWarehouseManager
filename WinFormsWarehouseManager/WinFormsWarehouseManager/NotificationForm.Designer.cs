namespace WinFormsWarehouseManager
{
    partial class NotificationForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.pnlLegend = new System.Windows.Forms.Panel();
            this.lblLegendExpired = new System.Windows.Forms.Label();
            this.lblLegendOverstock = new System.Windows.Forms.Label();
            this.lblLegendLowStock = new System.Windows.Forms.Label();
            this.lblUnreadCount = new System.Windows.Forms.Label();
            this.btnRefresh = new FontAwesome.Sharp.IconButton();
            this.btnMarkAllRead = new FontAwesome.Sharp.IconButton();
            this.btnDeleteAllRead = new FontAwesome.Sharp.IconButton();
            this.chkUnreadOnly = new System.Windows.Forms.CheckBox();
            this.pnlNotifications = new System.Windows.Forms.FlowLayoutPanel();
            this.lblNoData = new System.Windows.Forms.Label();
            this.pnlToolbar.SuspendLayout();
            this.pnlLegend.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlToolbar.Controls.Add(this.pnlLegend);
            this.pnlToolbar.Controls.Add(this.lblUnreadCount);
            this.pnlToolbar.Controls.Add(this.btnRefresh);
            this.pnlToolbar.Controls.Add(this.btnMarkAllRead);
            this.pnlToolbar.Controls.Add(this.btnDeleteAllRead);
            this.pnlToolbar.Controls.Add(this.chkUnreadOnly);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1200, 130);
            this.pnlToolbar.TabIndex = 0;
            // 
            // pnlLegend
            // 
            this.pnlLegend.Controls.Add(this.lblLegendExpired);
            this.pnlLegend.Controls.Add(this.lblLegendOverstock);
            this.pnlLegend.Controls.Add(this.lblLegendLowStock);
            this.pnlLegend.Location = new System.Drawing.Point(20, 65);
            this.pnlLegend.Name = "pnlLegend";
            this.pnlLegend.Size = new System.Drawing.Size(750, 35);
            this.pnlLegend.TabIndex = 5;
            // 
            // lblLegendExpired
            // 
            this.lblLegendExpired.AutoSize = true;
            this.lblLegendExpired.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLegendExpired.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblLegendExpired.Location = new System.Drawing.Point(0, 0);
            this.lblLegendExpired.Name = "lblLegendExpired";
            this.lblLegendExpired.Size = new System.Drawing.Size(178, 30);
            this.lblLegendExpired.TabIndex = 0;
            this.lblLegendExpired.Text = "Hết hạn sử dụng";
            // 
            // lblLegendOverstock
            // 
            this.lblLegendOverstock.AutoSize = true;
            this.lblLegendOverstock.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLegendOverstock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.lblLegendOverstock.Location = new System.Drawing.Point(184, 0);
            this.lblLegendOverstock.Name = "lblLegendOverstock";
            this.lblLegendOverstock.Size = new System.Drawing.Size(180, 30);
            this.lblLegendOverstock.TabIndex = 1;
            this.lblLegendOverstock.Text = "Quá hạn tồn kho";
            // 
            // lblLegendLowStock
            // 
            this.lblLegendLowStock.AutoSize = true;
            this.lblLegendLowStock.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLegendLowStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.lblLegendLowStock.Location = new System.Drawing.Point(370, 0);
            this.lblLegendLowStock.Name = "lblLegendLowStock";
            this.lblLegendLowStock.Size = new System.Drawing.Size(143, 30);
            this.lblLegendLowStock.TabIndex = 2;
            this.lblLegendLowStock.Text = "Sắp hết hàng";
            // 
            // lblUnreadCount
            // 
            this.lblUnreadCount.AutoSize = true;
            this.lblUnreadCount.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblUnreadCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblUnreadCount.Location = new System.Drawing.Point(20, 9);
            this.lblUnreadCount.Name = "lblUnreadCount";
            this.lblUnreadCount.Size = new System.Drawing.Size(231, 51);
            this.lblUnreadCount.TabIndex = 0;
            this.lblUnreadCount.Text = "Chưa đọc: 0";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.IconChar = FontAwesome.Sharp.IconChar.Sync;
            this.btnRefresh.IconColor = System.Drawing.Color.White;
            this.btnRefresh.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRefresh.IconSize = 30;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(1030, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnRefresh.Size = new System.Drawing.Size(150, 45);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnMarkAllRead
            // 
            this.btnMarkAllRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMarkAllRead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnMarkAllRead.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMarkAllRead.FlatAppearance.BorderSize = 0;
            this.btnMarkAllRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarkAllRead.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMarkAllRead.ForeColor = System.Drawing.Color.White;
            this.btnMarkAllRead.IconChar = FontAwesome.Sharp.IconChar.CheckDouble;
            this.btnMarkAllRead.IconColor = System.Drawing.Color.White;
            this.btnMarkAllRead.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMarkAllRead.IconSize = 30;
            this.btnMarkAllRead.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMarkAllRead.Location = new System.Drawing.Point(700, 15);
            this.btnMarkAllRead.Name = "btnMarkAllRead";
            this.btnMarkAllRead.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnMarkAllRead.Size = new System.Drawing.Size(160, 45);
            this.btnMarkAllRead.TabIndex = 2;
            this.btnMarkAllRead.Text = "Đọc hết";
            this.btnMarkAllRead.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMarkAllRead.UseVisualStyleBackColor = false;
            this.btnMarkAllRead.Click += new System.EventHandler(this.btnMarkAllRead_Click);
            // 
            // btnDeleteAllRead
            // 
            this.btnDeleteAllRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteAllRead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDeleteAllRead.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteAllRead.FlatAppearance.BorderSize = 0;
            this.btnDeleteAllRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAllRead.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteAllRead.ForeColor = System.Drawing.Color.White;
            this.btnDeleteAllRead.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btnDeleteAllRead.IconColor = System.Drawing.Color.White;
            this.btnDeleteAllRead.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDeleteAllRead.IconSize = 28;
            this.btnDeleteAllRead.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAllRead.Location = new System.Drawing.Point(867, 15);
            this.btnDeleteAllRead.Name = "btnDeleteAllRead";
            this.btnDeleteAllRead.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnDeleteAllRead.Size = new System.Drawing.Size(160, 45);
            this.btnDeleteAllRead.TabIndex = 3;
            this.btnDeleteAllRead.Text = "Xóa hết";
            this.btnDeleteAllRead.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteAllRead.UseVisualStyleBackColor = false;
            this.btnDeleteAllRead.Click += new System.EventHandler(this.btnDeleteAllRead_Click);
            // 
            // chkUnreadOnly
            // 
            this.chkUnreadOnly.AutoSize = true;
            this.chkUnreadOnly.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.chkUnreadOnly.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.chkUnreadOnly.Location = new System.Drawing.Point(304, 15);
            this.chkUnreadOnly.Name = "chkUnreadOnly";
            this.chkUnreadOnly.Size = new System.Drawing.Size(347, 45);
            this.chkUnreadOnly.TabIndex = 1;
            this.chkUnreadOnly.Text = "Chỉ hiển thị chưa đọc";
            this.chkUnreadOnly.UseVisualStyleBackColor = true;
            this.chkUnreadOnly.CheckedChanged += new System.EventHandler(this.chkUnreadOnly_CheckedChanged);
            // 
            // pnlNotifications
            // 
            this.pnlNotifications.AutoScroll = true;
            this.pnlNotifications.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlNotifications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNotifications.Location = new System.Drawing.Point(0, 130);
            this.pnlNotifications.Name = "pnlNotifications";
            this.pnlNotifications.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.pnlNotifications.Size = new System.Drawing.Size(1200, 570);
            this.pnlNotifications.TabIndex = 1;
            // 
            // lblNoData
            // 
            this.lblNoData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoData.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblNoData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.lblNoData.Location = new System.Drawing.Point(0, 130);
            this.lblNoData.Name = "lblNoData";
            this.lblNoData.Size = new System.Drawing.Size(1200, 570);
            this.lblNoData.TabIndex = 2;
            this.lblNoData.Text = "📭 Không có thông báo nào";
            this.lblNoData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoData.Visible = false;
            // 
            // NotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.lblNoData);
            this.Controls.Add(this.pnlNotifications);
            this.Controls.Add(this.pnlToolbar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "NotificationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông báo";
            this.Load += new System.EventHandler(this.NotificationForm_Load);
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlLegend.ResumeLayout(false);
            this.pnlLegend.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Panel pnlLegend;
        private System.Windows.Forms.Label lblLegendExpired;
        private System.Windows.Forms.Label lblLegendOverstock;
        private System.Windows.Forms.Label lblLegendLowStock;
        private System.Windows.Forms.Label lblUnreadCount;
        private FontAwesome.Sharp.IconButton btnRefresh;
        private FontAwesome.Sharp.IconButton btnMarkAllRead;
        private FontAwesome.Sharp.IconButton btnDeleteAllRead;
        private System.Windows.Forms.CheckBox chkUnreadOnly;
        private System.Windows.Forms.FlowLayoutPanel pnlNotifications;
        private System.Windows.Forms.Label lblNoData;
    }
}
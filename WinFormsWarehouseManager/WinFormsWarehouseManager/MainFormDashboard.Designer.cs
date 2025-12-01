namespace WinFormsWarehouseManager
{
    partial class MainFormDashboard
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
            this.panelTop = new Guna.UI2.WinForms.Guna2Panel();
            this.panelLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.btnMenu = new Guna.UI2.WinForms.Guna2Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnIconUser = new System.Windows.Forms.Button();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.panelTop.BorderRadius = 20;
            this.panelTop.Controls.Add(this.button3);
            this.panelTop.Controls.Add(this.button2);
            this.panelTop.Controls.Add(this.button1);
            this.panelTop.Controls.Add(this.btnIconUser);
            this.panelTop.Controls.Add(this.guna2Button1);
            this.panelTop.Controls.Add(this.btnMenu);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1874, 68);
            this.panelTop.TabIndex = 0;
            this.panelTop.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTop_Paint);
            // 
            // panelLeft
            // 
            this.panelLeft.AutoRoundedCorners = true;
            this.panelLeft.BackColor = System.Drawing.Color.White;
            this.panelLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelLeft.BorderRadius = 49;
            this.panelLeft.BorderStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 68);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(100, 1061);
            this.panelLeft.TabIndex = 1;
            // 
            // btnMenu
            // 
            this.btnMenu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMenu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMenu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMenu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMenu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnMenu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnMenu.Location = new System.Drawing.Point(0, 0);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(100, 71);
            this.btnMenu.TabIndex = 1;
            this.btnMenu.Text = "Admin Dashboard";
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::WinFormsWarehouseManager.Properties.Resources.comments;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(1077, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(99, 50);
            this.button3.TabIndex = 5;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::WinFormsWarehouseManager.Properties.Resources.notification;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(1182, -3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 68);
            this.button2.TabIndex = 4;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::WinFormsWarehouseManager.Properties.Resources.email_notification;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(1281, -3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 68);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnIconUser
            // 
            this.btnIconUser.BackgroundImage = global::WinFormsWarehouseManager.Properties.Resources.UserIcon;
            this.btnIconUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIconUser.FlatAppearance.BorderSize = 0;
            this.btnIconUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIconUser.Location = new System.Drawing.Point(1380, 0);
            this.btnIconUser.Name = "btnIconUser";
            this.btnIconUser.Size = new System.Drawing.Size(99, 68);
            this.btnIconUser.TabIndex = 2;
            this.btnIconUser.UseVisualStyleBackColor = true;
            this.btnIconUser.Click += new System.EventHandler(this.btnIconUser_Click);
            // 
            // guna2Button1
            // 
            this.guna2Button1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button1.BackgroundImage = global::WinFormsWarehouseManager.Properties.Resources.UserIcon;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Empty;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(1291, 17);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(124, 76);
            this.guna2Button1.TabIndex = 2;
            // 
            // MainFormDashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(229)))), ((int)(((byte)(233)))));
            this.ClientSize = new System.Drawing.Size(1874, 1129);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelTop);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Name = "MainFormDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainFormDashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelTop;
        private Guna.UI2.WinForms.Guna2Panel panelLeft;
        private Guna.UI2.WinForms.Guna2Button btnMenu;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnIconUser;
        private System.Windows.Forms.Button button3;
    }
}
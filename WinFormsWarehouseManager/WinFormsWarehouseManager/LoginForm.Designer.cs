namespace WinFormsWarehouseManager
{
    partial class LoginForm
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
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.iconbtnMinimize = new FontAwesome.Sharp.IconButton();
            this.iconbtnExit = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUsername = new System.Windows.Forms.Label();
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnLogin = new WinFormsWarehouseManager.RJControls.RJButton();
            this.llblForgotPW = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtUsername = new WinFormsWarehouseManager.CustomControls.CustomTextBox();
            this.txtPassword = new WinFormsWarehouseManager.CustomControls.CustomTextBox();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.panelTitleBar.Controls.Add(this.iconbtnMinimize);
            this.panelTitleBar.Controls.Add(this.iconbtnExit);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(450, 40);
            this.panelTitleBar.TabIndex = 0;
            this.panelTitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseMove);
            // 
            // iconbtnMinimize
            // 
            this.iconbtnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.iconbtnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconbtnMinimize.FlatAppearance.BorderSize = 0;
            this.iconbtnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconbtnMinimize.ForeColor = System.Drawing.Color.White;
            this.iconbtnMinimize.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.iconbtnMinimize.IconColor = System.Drawing.Color.White;
            this.iconbtnMinimize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconbtnMinimize.IconSize = 20;
            this.iconbtnMinimize.Location = new System.Drawing.Point(368, 5);
            this.iconbtnMinimize.Name = "iconbtnMinimize";
            this.iconbtnMinimize.Size = new System.Drawing.Size(35, 30);
            this.iconbtnMinimize.TabIndex = 22;
            this.iconbtnMinimize.UseVisualStyleBackColor = false;
            this.iconbtnMinimize.Click += new System.EventHandler(this.iconbtnMinimize_Click);
            // 
            // iconbtnExit
            // 
            this.iconbtnExit.BackColor = System.Drawing.Color.Transparent;
            this.iconbtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconbtnExit.FlatAppearance.BorderSize = 0;
            this.iconbtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconbtnExit.ForeColor = System.Drawing.Color.White;
            this.iconbtnExit.IconChar = FontAwesome.Sharp.IconChar.X;
            this.iconbtnExit.IconColor = System.Drawing.Color.White;
            this.iconbtnExit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconbtnExit.IconSize = 20;
            this.iconbtnExit.Location = new System.Drawing.Point(409, 5);
            this.iconbtnExit.Name = "iconbtnExit";
            this.iconbtnExit.Size = new System.Drawing.Size(35, 30);
            this.iconbtnExit.TabIndex = 21;
            this.iconbtnExit.UseVisualStyleBackColor = false;
            this.iconbtnExit.Click += new System.EventHandler(this.iconbtnAddSP_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 635);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 15);
            this.panel1.TabIndex = 1;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.Silver;
            this.lblUsername.Location = new System.Drawing.Point(50, 295);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(84, 32);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Email:";
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.AutoSize = true;
            this.chkRememberMe.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.chkRememberMe.ForeColor = System.Drawing.Color.Silver;
            this.chkRememberMe.Location = new System.Drawing.Point(50, 430);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Size = new System.Drawing.Size(322, 35);
            this.chkRememberMe.TabIndex = 4;
            this.chkRememberMe.Text = "Ghi nhớ lần đăng nhập sau";
            this.chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.Silver;
            this.lblPassword.Location = new System.Drawing.Point(50, 360);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(123, 32);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Password";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.btnLogin.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.btnLogin.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnLogin.BorderRadius = 15;
            this.btnLogin.BorderSize = 0;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(50, 475);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(350, 50);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.TextColor = System.Drawing.Color.White;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // llblForgotPW
            // 
            this.llblForgotPW.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.llblForgotPW.AutoSize = true;
            this.llblForgotPW.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.llblForgotPW.ForeColor = System.Drawing.Color.LightGray;
            this.llblForgotPW.LinkColor = System.Drawing.Color.Silver;
            this.llblForgotPW.Location = new System.Drawing.Point(141, 547);
            this.llblForgotPW.Name = "llblForgotPW";
            this.llblForgotPW.Size = new System.Drawing.Size(171, 30);
            this.llblForgotPW.TabIndex = 8;
            this.llblForgotPW.TabStop = true;
            this.llblForgotPW.Text = "Quên mật khẩu?";
            this.llblForgotPW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.llblForgotPW.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblForgotPW_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::WinFormsWarehouseManager.Properties.Resources.logo2;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(125, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 200);
            this.panel2.TabIndex = 9;
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.txtUsername.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.txtUsername.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(173)))));
            this.txtUsername.BorderRadius = 8;
            this.txtUsername.BorderSize = 2;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtUsername.ForeColor = System.Drawing.Color.White;
            this.txtUsername.IconChar = FontAwesome.Sharp.IconChar.User;
            this.txtUsername.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.txtUsername.Location = new System.Drawing.Point(50, 318);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsername.Multiline = false;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtUsername.PasswordChar = false;
            this.txtUsername.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtUsername.PlaceholderText = "Nhập email của bạn..";
            this.txtUsername.ShowTogglePassword = false;
            this.txtUsername.Size = new System.Drawing.Size(350, 51);
            this.txtUsername.TabIndex = 3;
            this.txtUsername.Texts = "";
            this.txtUsername.UnderlinedStyle = false;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.txtPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.txtPassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(173)))));
            this.txtPassword.BorderRadius = 8;
            this.txtPassword.BorderSize = 2;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.IconChar = FontAwesome.Sharp.IconChar.Lock;
            this.txtPassword.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.txtPassword.Location = new System.Drawing.Point(50, 383);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Multiline = false;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtPassword.PasswordChar = true;
            this.txtPassword.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtPassword.PlaceholderText = "Nhập mật khẩu của bạn..";
            this.txtPassword.ShowTogglePassword = true;
            this.txtPassword.Size = new System.Drawing.Size(350, 51);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.Texts = "";
            this.txtPassword.UnderlinedStyle = false;
            // 
            // LoginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(450, 650);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.llblForgotPW);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.chkRememberMe);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoginForm";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LoginForm_MouseMove);
            this.panelTitleBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton iconbtnExit;
        private FontAwesome.Sharp.IconButton iconbtnMinimize;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.Label lblPassword;
        private RJControls.RJButton btnLogin;
        private System.Windows.Forms.LinkLabel llblForgotPW;
        private System.Windows.Forms.Panel panel2;
        private CustomControls.CustomTextBox txtUsername;
        private CustomControls.CustomTextBox txtPassword;
    }
}
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
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnLogin = new WinFormsWarehouseManager.RJControls.RJButton();
            this.llblForgotPW = new System.Windows.Forms.LinkLabel();
            this.iconSplitButton1 = new FontAwesome.Sharp.IconSplitButton();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.panelTitleBar.Controls.Add(this.iconbtnMinimize);
            this.panelTitleBar.Controls.Add(this.iconbtnExit);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(700, 50);
            this.panelTitleBar.TabIndex = 0;
            this.panelTitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseMove);
            // 
            // iconbtnMinimize
            // 
            this.iconbtnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.iconbtnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconbtnMinimize.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(20)))));
            this.iconbtnMinimize.FlatAppearance.BorderSize = 0;
            this.iconbtnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconbtnMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconbtnMinimize.ForeColor = System.Drawing.Color.White;
            this.iconbtnMinimize.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.iconbtnMinimize.IconColor = System.Drawing.Color.White;
            this.iconbtnMinimize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconbtnMinimize.IconSize = 30;
            this.iconbtnMinimize.Location = new System.Drawing.Point(601, 2);
            this.iconbtnMinimize.Name = "iconbtnMinimize";
            this.iconbtnMinimize.Size = new System.Drawing.Size(45, 45);
            this.iconbtnMinimize.TabIndex = 22;
            this.iconbtnMinimize.Tag = "";
            this.iconbtnMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconbtnMinimize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconbtnMinimize.UseVisualStyleBackColor = false;
            this.iconbtnMinimize.Click += new System.EventHandler(this.iconbtnMinimize_Click);
            // 
            // iconbtnExit
            // 
            this.iconbtnExit.BackColor = System.Drawing.Color.Transparent;
            this.iconbtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconbtnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(20)))));
            this.iconbtnExit.FlatAppearance.BorderSize = 0;
            this.iconbtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconbtnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconbtnExit.ForeColor = System.Drawing.Color.White;
            this.iconbtnExit.IconChar = FontAwesome.Sharp.IconChar.X;
            this.iconbtnExit.IconColor = System.Drawing.Color.White;
            this.iconbtnExit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconbtnExit.IconSize = 30;
            this.iconbtnExit.Location = new System.Drawing.Point(652, 2);
            this.iconbtnExit.Name = "iconbtnExit";
            this.iconbtnExit.Size = new System.Drawing.Size(45, 45);
            this.iconbtnExit.TabIndex = 21;
            this.iconbtnExit.Tag = "";
            this.iconbtnExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconbtnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconbtnExit.UseVisualStyleBackColor = false;
            this.iconbtnExit.Click += new System.EventHandler(this.iconbtnAddSP_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 880);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 20);
            this.panel1.TabIndex = 1;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.Silver;
            this.lblUsername.Location = new System.Drawing.Point(99, 303);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(136, 37);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Location = new System.Drawing.Point(99, 343);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(500, 24);
            this.txtUsername.TabIndex = 3;
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.AutoSize = true;
            this.chkRememberMe.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRememberMe.ForeColor = System.Drawing.Color.Silver;
            this.chkRememberMe.Location = new System.Drawing.Point(99, 480);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Size = new System.Drawing.Size(221, 41);
            this.chkRememberMe.TabIndex = 4;
            this.chkRememberMe.Text = "Remember me";
            this.chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Location = new System.Drawing.Point(99, 421);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(500, 24);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.Silver;
            this.lblPassword.Location = new System.Drawing.Point(99, 381);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(128, 37);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Password";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(97)))), ((int)(((byte)(238)))));
            this.btnLogin.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(97)))), ((int)(((byte)(238)))));
            this.btnLogin.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnLogin.BorderRadius = 20;
            this.btnLogin.BorderSize = 0;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(99, 561);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(500, 65);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "Login";
            this.btnLogin.TextColor = System.Drawing.Color.White;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // llblForgotPW
            // 
            this.llblForgotPW.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.llblForgotPW.AutoSize = true;
            this.llblForgotPW.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llblForgotPW.ForeColor = System.Drawing.Color.LightGray;
            this.llblForgotPW.LinkColor = System.Drawing.Color.Silver;
            this.llblForgotPW.Location = new System.Drawing.Point(279, 681);
            this.llblForgotPW.Name = "llblForgotPW";
            this.llblForgotPW.Size = new System.Drawing.Size(133, 21);
            this.llblForgotPW.TabIndex = 8;
            this.llblForgotPW.TabStop = true;
            this.llblForgotPW.Text = "Forgot Password?";
            this.llblForgotPW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iconSplitButton1
            // 
            this.iconSplitButton1.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconSplitButton1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconSplitButton1.IconColor = System.Drawing.Color.Black;
            this.iconSplitButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconSplitButton1.IconSize = 48;
            this.iconSplitButton1.Name = "iconSplitButton1";
            this.iconSplitButton1.Rotation = 0D;
            this.iconSplitButton1.Size = new System.Drawing.Size(23, 23);
            this.iconSplitButton1.Text = "iconSplitButton1";
            // 
            // LoginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(59)))), ((int)(((byte)(104)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(700, 900);
            this.Controls.Add(this.llblForgotPW);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.chkRememberMe);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoginForm";
            this.Opacity = 0.85D;
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
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private RJControls.RJButton btnLogin;
        private System.Windows.Forms.LinkLabel llblForgotPW;
        private FontAwesome.Sharp.IconSplitButton iconSplitButton1;
    }
}
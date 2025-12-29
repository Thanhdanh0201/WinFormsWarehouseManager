using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsWarehouseManager.db;
using WinFormsWarehouseManager.Services;
using WinFormsWarehouseManager.Forms;
using WinFormsWarehouseManager.Helpers;

namespace WinFormsWarehouseManager
{
    public partial class MainForm : Form
    {
        //Fields
        private Size formSize;
        private IconButton currBtn;
        private Panel leftBorderBtn;
        private Form currChildForm;
        private int borderSize = 2;

        private System.Windows.Forms.Label badgeNhapKho;
        private System.Windows.Forms.Label badgeXuatKho;
        private System.Windows.Forms.Timer timerUpdateBadges;

        // Fields cho logout animation
        private Panel logoutPanel;
        private Label lblSaving;
        private ProgressBar progressBarLogout;
        private System.Windows.Forms.Timer logoutTimer;
        private int logoutProgress = 0;

        public MainForm()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            this.Padding = new Padding(borderSize);
            this.BackColor = Color.White;
            NotificationManager.GenerateAllNotifications();

            InitializeBadges();
            InitializeBadgeTimer();
            InitializeLogoutPanel();
        }

        private void InitializeLogoutPanel()
        {
            // Tạo panel overlay cho logout
            logoutPanel = new Panel
            {
                BackColor = Color.FromArgb(220, 2, 51, 66), // Semi-transparent với opacity cao hơn
                Dock = DockStyle.Fill,
                Visible = false
            };

            // Label với icon hourglass "⏳ Loading emails..."
            lblSaving = new Label
            {
                Text = "⏳ Saving ...",
                Font = new Font("Segoe UI", 18F, FontStyle.Regular), // Không bold, size lớn hơn
                ForeColor = Color.FromArgb(2, 51, 66), // Màu chữ theo theme
                AutoSize = true,
                BackColor = Color.Transparent
            };

            // Progress Bar dài hơn và style Marquee
            progressBarLogout = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30,
                Width = 500, // Dài hơn
                Height = 8, // Mỏng hơn, thanh thoát
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                ForeColor = Color.FromArgb(2, 51, 66)
            };

            // Timer cho animation
            logoutTimer = new System.Windows.Forms.Timer
            {
                Interval = 2000 // 2 giây để hoàn thành
            };
            logoutTimer.Tick += LogoutTimer_Tick;

            logoutPanel.Controls.Add(lblSaving);
            logoutPanel.Controls.Add(progressBarLogout);

            // Thêm vào panelDesktop
            panelDesktop.Controls.Add(logoutPanel);
            logoutPanel.BringToFront();

            // Layout khi panel resize
            logoutPanel.Resize += (s, e) =>
            {
                CenterLogoutControls();
            };
        }

        private void CenterLogoutControls()
        {
            if (logoutPanel == null) return;

            // Center label
            lblSaving.Location = new Point(
                (logoutPanel.Width - lblSaving.Width) / 2,
                (logoutPanel.Height - lblSaving.Height - progressBarLogout.Height - 30) / 2
            );

            // Center progress bar
            progressBarLogout.Location = new Point(
                (logoutPanel.Width - progressBarLogout.Width) / 2,
                lblSaving.Bottom + 25
            );
        }

        private void LogoutTimer_Tick(object sender, EventArgs e)
        {
            // Sau 2 giây thì hoàn thành
            logoutTimer.Stop();
            CompleteLogout();
        }

        private void StartLogout()
        {
            // Hiển thị logout panel
            logoutPanel.Visible = true;
            logoutPanel.BringToFront();
            CenterLogoutControls();

            // Bắt đầu animation marquee
            progressBarLogout.Style = ProgressBarStyle.Marquee;
            logoutTimer.Start();
        }

        private void CompleteLogout()
        {
            // Ẩn logout panel
            logoutPanel.Visible = false;

            // Đóng form hiện tại và mở login form
            this.Hide();

            LoginForm loginForm = new LoginForm();
            loginForm.FormClosed += (s, args) =>
            {
                // Nếu đăng nhập thành công, hiển thị lại MainForm
                // Nếu không, thoát ứng dụng
                if (loginForm.DialogResult == DialogResult.OK)
                {
                    this.Show();
                    // Reset lại form
                    ResetMainForm();
                }
                else
                {
                    Application.Exit();
                }
            };

            loginForm.ShowDialog();
        }

        private void ResetMainForm()
        {
            // Reset các biến và UI về trạng thái ban đầu
            if (currChildForm != null)
            {
                currChildForm.Close();
                currChildForm = null;
            }

            DisableButton();
            leftBorderBtn.Visible = false;
            iconChildForm.Visible = false;
            lblChildForm.Visible = false;

            // Click vào dashboard
            iconButton1.PerformClick();
        }

        private void InitializeBadges()
        {
            // Badge cho Nhập Kho (iconButton3)
            badgeNhapKho = new Label
            {
                AutoSize = false,
                Size = new Size(24, 24),
                BackColor = Color.FromArgb(4, 119, 154),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false,
                Location = new Point(iconButton3.Right - 30, iconButton3.Top + 5)
            };

            badgeNhapKho.Paint += (s, e) => {
                Label lbl = s as Label;
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, lbl.Width - 1, lbl.Height - 1);
                lbl.Region = new Region(path);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Color.FromArgb(2, 51, 66), 2))
                {
                    e.Graphics.DrawEllipse(pen, 0, 0, lbl.Width - 1, lbl.Height - 1);
                }
            };

            panelMenu.Controls.Add(badgeNhapKho);
            badgeNhapKho.BringToFront();

            // Badge cho Xuất Kho (iconButton4)
            badgeXuatKho = new Label
            {
                AutoSize = false,
                Size = new Size(24, 24),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false,
                Location = new Point(iconButton4.Right - 30, iconButton4.Top + 5)
            };

            badgeXuatKho.Paint += (s, e) => {
                Label lbl = s as Label;
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, lbl.Width - 1, lbl.Height - 1);
                lbl.Region = new Region(path);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Color.FromArgb(2, 51, 66), 2))
                {
                    e.Graphics.DrawEllipse(pen, 0, 0, lbl.Width - 1, lbl.Height - 1);
                }
            };

            panelMenu.Controls.Add(badgeXuatKho);
            badgeXuatKho.BringToFront();
        }

        private void InitializeBadgeTimer()
        {
            timerUpdateBadges = new System.Windows.Forms.Timer();
            timerUpdateBadges.Interval = 500;
            timerUpdateBadges.Tick += TimerUpdateBadges_Tick;
            timerUpdateBadges.Start();
        }

        private void TimerUpdateBadges_Tick(object sender, EventArgs e)
        {
            UpdateBadges();
        }

        private void UpdateBadges()
        {
            int nhapKhoCount = GetTempImportCount();
            if (nhapKhoCount > 0)
            {
                badgeNhapKho.Text = nhapKhoCount > 99 ? "99+" : nhapKhoCount.ToString();
                badgeNhapKho.Visible = true;
            }
            else
            {
                badgeNhapKho.Visible = false;
            }

            int xuatKhoCount = GetTempExportCount();
            if (xuatKhoCount > 0)
            {
                badgeXuatKho.Text = xuatKhoCount > 99 ? "99+" : xuatKhoCount.ToString();
                badgeXuatKho.Visible = true;
            }
            else
            {
                badgeXuatKho.Visible = false;
            }
        }

        private int GetTempImportCount()
        {
            try
            {
                var tempData = WinFormsWarehouseManager.Utils.TempImportManager.Load();
                return tempData?.Items?.Count ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private int GetTempExportCount()
        {
            try
            {
                var tempData = WinFormsWarehouseManager.Utils.TempExportManager.Load();
                return tempData?.Items?.Count ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        public void RefreshBadges()
        {
            UpdateBadges();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020;
            const int SC_RESTORE = 0xF120;
            const int WM_NCHITTEST = 0x0084;
            const int resizeAreaSize = 10;

            const int HTCLIENT = 1;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)
                {
                    if ((int)m.Result == HTCLIENT)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= resizeAreaSize)
                        {
                            if (clientPoint.X <= resizeAreaSize)
                                m.Result = (IntPtr)HTTOPLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))
                                m.Result = (IntPtr)HTTOP;
                            else
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize))
                        {
                            if (clientPoint.X <= resizeAreaSize)
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))
                                m.Result = (IntPtr)HTBOTTOM;
                            else
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }

            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }

            if (m.Msg == WM_SYSCOMMAND)
            {
                int wParam = (m.WParam.ToInt32() & 0xFFF0);
                if (wParam == SC_MINIMIZE)
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)
                    this.Size = formSize;
            }
            base.WndProc(ref m);
        }

        private void DisableButton()
        {
            if (currBtn != null)
            {
                currBtn.BackColor = Color.FromArgb(2, 51, 66);
                currBtn.ForeColor = Color.GhostWhite;
                currBtn.TextAlign = ContentAlignment.MiddleLeft;
                currBtn.IconColor = Color.GhostWhite;
                currBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();

                currBtn = (IconButton)senderBtn;
                currBtn.BackColor = Color.FromArgb(37, 36, 81);
                currBtn.ForeColor = color;
                currBtn.TextAlign = ContentAlignment.MiddleCenter;
                currBtn.IconColor = color;
                currBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currBtn.ImageAlign = ContentAlignment.MiddleRight;

                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                iconChildForm.Visible = true;
                lblChildForm.Visible = true;
                iconChildForm.IconChar = currBtn.IconChar;
                lblChildForm.Text = currBtn.Text;
                lblChildForm.ForeColor = iconChildForm.IconColor;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (currChildForm != null)
            {
                currChildForm.Close();
            }
            currChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(4, 119, 154);
            public static Color color4 = Color.FromArgb(220, 53, 69);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
            public static Color color7 = Color.Aqua;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new FormDashboard());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            OpenChildForm(new FormProductList());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            OpenChildForm(new FormNhapKho());
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new FormXuatKho());
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new NotificationForm());
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            // Nút đăng xuất - Bắt đầu animation logout
            DialogResult result = MessageBox.Show(
                "Bạn có muốn đăng xuất?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                StartLogout();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
            iconButton1.PerformClick();
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color7);
            iconChildForm.Visible = false;
            lblChildForm.Visible = false;
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color7);
            iconChildForm.Visible = false;
            lblChildForm.Visible = false;
            OpenChildForm(new MailBoxForm());
        }

        private void btnIconUser_Click_1(object sender, EventArgs e)
        {
            UserProfileModal modal = new UserProfileModal();
            modal.ShowDialog(this);
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(0, 8, 8, 8);
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != borderSize)
                    {
                        this.Padding = new Padding(borderSize);
                    }
                    break;
            }
        }

        private void iconbtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconbtnZoom_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void iconbtnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Placeholder event handlers
        private void lblNameProject_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void panelTop_Paint(object sender, PaintEventArgs e) { }
        private void btnIconUser_Click(object sender, EventArgs e) { }
        private void lblNameProject_Click_1(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void iconbtnMenu_Click(object sender, EventArgs e) { }
        private void panelDesktop_Paint(object sender, PaintEventArgs e) { }
    }
}
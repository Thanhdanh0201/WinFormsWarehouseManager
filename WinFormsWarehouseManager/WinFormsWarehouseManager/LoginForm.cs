using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsWarehouseManager.db;
using WinFormsWarehouseManager.Models;
using WinFormsWarehouseManager.Forms;
using WinFormsWarehouseManager.Services;

namespace WinFormsWarehouseManager
{
    public partial class LoginForm : Form
    {
        private DatabaseHelper dbHelper;
        private const string REMEMBER_FILE = "remember.dat";

        // Splash screen controls - hiển thị trên panel login
        private Label lblWelcome;
        private ProgressBar progressBarSplash;
        private Label lblSplashStatus;
        private Timer timerProgress;
        private Timer timerTextReveal;
        private int progressValue = 0;
        private MainForm mainForm;
        private string fullWelcomeText = "";
        private int currentCharIndex = 0;

        public LoginForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            LoadRememberMe();
            InitializeSplashControls();
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// Khởi tạo các controls cho Splash Screen - hiển thị trên panel login
        /// </summary>
        private void InitializeSplashControls()
        {
            // Label Welcome - hiển thị ở vị trí email/password
            lblWelcome = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(230, 230, 230),
                AutoSize = false,
                Size = new Size(350, 120), // Chiếm vùng email + password
                TextAlign = ContentAlignment.MiddleCenter,
                Location = txtUsername.Location, // Vị trí của textbox email
                Visible = false,
                BackColor = Color.Transparent
            };

            // ProgressBar - style giống button đăng nhập
            progressBarSplash = new ProgressBar
            {
                Size = btnLogin.Size, // Size giống button login
                Location = btnLogin.Location, // Vị trí giống button login
                Style = ProgressBarStyle.Continuous,
                Maximum = 100,
                Value = 0,
                Visible = false
            };

            // Tùy chỉnh style của progress bar để đẹp như button
            progressBarSplash.Height = btnLogin.Height;

            // Label Status - hiển thị dưới progress bar
            lblSplashStatus = new Label
            {
                Text = "Đang tải dữ liệu...",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(180, 180, 180),
                AutoSize = false,
                Size = new Size(350, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(btnLogin.Location.X, btnLogin.Location.Y + btnLogin.Height + 10),
                Visible = false,
                BackColor = Color.Transparent
            };

            // Add controls vào form (cùng level với các controls login)
            this.Controls.Add(lblWelcome);
            this.Controls.Add(progressBarSplash);
            this.Controls.Add(lblSplashStatus);

            // Đưa splash controls lên trên cùng
            lblWelcome.BringToFront();
            progressBarSplash.BringToFront();
            lblSplashStatus.BringToFront();

            // Timer để update progress
            timerProgress = new Timer
            {
                Interval = 30
            };
            timerProgress.Tick += TimerProgress_Tick;

            // Timer để hiện text từ từ
            timerTextReveal = new Timer
            {
                Interval = 50
            };
            timerTextReveal.Tick += TimerTextReveal_Tick;
        }

        /// <summary>
        /// Timer tick để hiện text từng ký tự
        /// </summary>
        private void TimerTextReveal_Tick(object sender, EventArgs e)
        {
            if (currentCharIndex < fullWelcomeText.Length)
            {
                currentCharIndex++;
                lblWelcome.Text = fullWelcomeText.Substring(0, currentCharIndex);
            }
            else
            {
                timerTextReveal.Stop();
            }
        }

        /// <summary>
        /// Timer tick để update progress bar
        /// </summary>
        private void TimerProgress_Tick(object sender, EventArgs e)
        {
            if (progressValue < 100)
            {
                progressValue += 2; // Tăng 2% mỗi lần
                progressBarSplash.Value = progressValue;

                // Update status text theo progress
                if (progressValue < 30)
                    lblSplashStatus.Text = "Đang khởi tạo...";
                else if (progressValue < 60)
                    lblSplashStatus.Text = "Đang tải giao diện...";
                else if (progressValue < 90)
                    lblSplashStatus.Text = "Đang tải dữ liệu...";
                else
                    lblSplashStatus.Text = "Hoàn tất!";
            }
            else
            {
                // Hoàn thành
                timerProgress.Stop();

                // Delay 200ms để người dùng thấy 100%
                Task.Delay(200).ContinueWith(t =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        // Ẩn LoginForm
                        this.Hide();

                        // Hiện MainForm (đã được load sẵn)
                        if (mainForm != null)
                        {
                            mainForm.Show();
                            mainForm.FormClosed += (s, args) => this.Close();
                        }
                    });
                });
            }
        }

        /// <summary>
        /// Hiển thị splash screen và load MainForm
        /// </summary>
        private async void ShowSplashAndLoadMainForm(User user)
        {
            // Ẩn các controls login
            HideLoginControls();

            // Hiện splash controls
            lblWelcome.Visible = true;
            progressBarSplash.Visible = true;
            lblSplashStatus.Visible = true;

            // Chuẩn bị text để hiện dần
            fullWelcomeText = $"Xin chào {user.FullName},bạn đã đăng nhập vào Warehouse Manager";
            currentCharIndex = 0;
            lblWelcome.Text = "";

            // Bắt đầu hiện text từ từ
            timerTextReveal.Start();

            // Đợi 1.8s cho text hiện xong
            await Task.Delay(1800);

            // Reset progress
            progressValue = 0;
            progressBarSplash.Value = 0;

            // Bắt đầu progress bar
            timerProgress.Start();

            // Load MainForm trong background và ẩn đi
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    // Tạo MainForm (tốn thời gian)
                    mainForm = new MainForm();

                    // Load form nhưng ẩn đi (không hiện lên)
                    mainForm.Opacity = 0; // Ẩn hoàn toàn
                    mainForm.Show(); // Load form
                    mainForm.Hide(); // Ẩn ngay
                    mainForm.Opacity = 1; // Khôi phục opacity
                });

                // Giả lập thời gian để progress bar chạy mượt
                System.Threading.Thread.Sleep(500);
            });

            // Sau khi load xong, chờ progress bar chạy hết
            // Timer sẽ tự động mở MainForm khi đạt 100%
        }

        /// <summary>
        /// Ẩn các controls login
        /// </summary>
        private void HideLoginControls()
        {
            txtUsername.Visible = false;
            txtPassword.Visible = false;
            chkRememberMe.Visible = false;
            btnLogin.Visible = false;
            llblForgotPW.Visible = false;

            // Ẩn các label "Email:" và "Password:" nếu có
            foreach (Control control in this.Controls)
            {
                if (control is Label && control != lblWelcome && control != lblSplashStatus)
                {
                    control.Visible = false;
                }
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (!dbHelper.TestConnection())
            {
                MessageBox.Show("Không thể kết nối database. Vui lòng kiểm tra lại!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void iconbtnAddSP_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconbtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panelTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void LoginForm_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Texts.Trim();
            string password = txtPassword.Texts;

            // Validate input
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập Email!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập Password!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Xác thực đăng nhập
            User user = AuthenticateUser(email, password);

            if (user != null)
            {
                // Lưu thông tin Remember Me nếu được chọn
                if (chkRememberMe.Checked)
                {
                    SaveRememberMe(email, password);
                }
                else
                {
                    ClearRememberMe();
                }

                // Lưu thông tin user hiện tại vào Session
                UserSession.CurrentUser = user;

                // Ghi log đăng nhập
                LogActivity(user.UserID, "Đăng nhập", "User đăng nhập vào hệ thống");

                // Hiện splash và load MainForm
                ShowSplashAndLoadMainForm(user);
            }
            else
            {
                MessageBox.Show("Email hoặc Password không chính xác!",
                    "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Texts = "";
                txtPassword.Focus();
            }
        }

        /// <summary>
        /// Xử lý khi click vào link Forgot Password
        /// </summary>
        private void llblForgotPW_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Mở modal form để nhập email
            ForgotPasswordModal modal = new ForgotPasswordModal();

            if (modal.ShowDialog() == DialogResult.OK)
            {
                string email = modal.EmailEntered;

                // Kiểm tra email có tồn tại trong database không
                User user = GetUserByEmail(email);

                if (user != null)
                {
                    // Gửi password về email
                    SendPasswordToEmail(user);
                }
                else
                {
                    MessageBox.Show("Email không tồn tại trong hệ thống!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Lấy thông tin user theo email
        /// </summary>
        private User GetUserByEmail(string email)
        {
            try
            {
                string query = @"SELECT UserID, FullName, BirthDate, Email, Password, MailboxPassword, CreatedAt 
                               FROM Users 
                               WHERE Email = @Email";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Email", email)
                };

                var dt = dbHelper.ExecuteQuery(query, parameters);

                if (dt != null && dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    return new User
                    {
                        UserID = Convert.ToInt32(row["UserID"]),
                        FullName = row["FullName"].ToString(),
                        BirthDate = row["BirthDate"] != DBNull.Value
                            ? Convert.ToDateTime(row["BirthDate"])
                            : (DateTime?)null,
                        Email = row["Email"].ToString(),
                        Password = row["Password"].ToString(),
                        MailboxPassword = row["MailboxPassword"] != DBNull.Value
                            ? row["MailboxPassword"].ToString()
                            : "",
                        CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn database: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Gửi password về email của user
        /// </summary>
        private void SendPasswordToEmail(User user)
        {
            try
            {
                // Kiểm tra user có MailboxPassword không
                if (string.IsNullOrEmpty(user.MailboxPassword))
                {
                    MessageBox.Show("Tài khoản này chưa cấu hình MailboxPassword để gửi email!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Hiển thị loading
                this.Cursor = Cursors.WaitCursor;
                this.Enabled = false;

                // Sử dụng EmailService để gửi mail
                using (EmailService emailService = new EmailService(user.Email, user.MailboxPassword))
                {
                    string subject = "Khôi phục mật khẩu - Warehouse Manager";
                    string body = $@"Xin chào {user.FullName},

Bạn đã yêu cầu khôi phục mật khẩu cho tài khoản: {user.Email}

Mật khẩu của bạn là: {user.Password}

Vui lòng đăng nhập và đổi mật khẩu mới để bảo mật tài khoản.

Trân trọng,
Warehouse Manager System";

                    emailService.SendEmail(user.Email, subject, body);

                    // Ghi log
                    LogActivity(user.UserID, "Quên mật khẩu", $"User yêu cầu gửi lại mật khẩu về email {user.Email}");

                    MessageBox.Show($"Mật khẩu đã được gửi về email: {user.Email}\nVui lòng kiểm tra hộp thư!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi gửi email: {ex.Message}\n\nVui lòng kiểm tra:\n" +
                    "1. MailboxPassword (App Password) đã được cấu hình đúng\n" +
                    "2. Kết nối Internet\n" +
                    "3. Cài đặt bảo mật Gmail",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.Enabled = true;
            }
        }

        /// <summary>
        /// Xác thực thông tin đăng nhập
        /// </summary>
        private User AuthenticateUser(string email, string password)
        {
            try
            {
                string query = @"SELECT UserID, FullName, BirthDate, Email, MailboxPassword, CreatedAt 
                       FROM Users 
                       WHERE Email = @Email AND Password = @Password";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Email", email),
                    new SQLiteParameter("@Password", password)
                };

                var dt = dbHelper.ExecuteQuery(query, parameters);

                if (dt != null && dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    return new User
                    {
                        UserID = Convert.ToInt32(row["UserID"]),
                        FullName = row["FullName"].ToString(),
                        BirthDate = row["BirthDate"] != DBNull.Value
                            ? Convert.ToDateTime(row["BirthDate"])
                            : (DateTime?)null,
                        Email = row["Email"].ToString(),
                        MailboxPassword = row["MailboxPassword"] != DBNull.Value
                            ? row["MailboxPassword"].ToString()
                            : "",
                        CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xác thực: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Ghi log hoạt động
        /// </summary>
        private void LogActivity(int userId, string action, string description)
        {
            try
            {
                string query = @"INSERT INTO ActivityLog (LoaiHanhDong, Description, UserID) 
                               VALUES (@Action, @Description, @UserID)";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Action", action),
                    new SQLiteParameter("@Description", description),
                    new SQLiteParameter("@UserID", userId)
                };

                dbHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Log error: {ex.Message}");
            }
        }

        /// <summary>
        /// Lưu thông tin Remember Me vào file
        /// </summary>
        private void SaveRememberMe(string email, string password)
        {
            try
            {
                string filePath = System.IO.Path.Combine(Application.StartupPath, REMEMBER_FILE);
                string encodedData = Convert.ToBase64String(
                    System.Text.Encoding.UTF8.GetBytes($"{email}|{password}")
                );
                System.IO.File.WriteAllText(filePath, encodedData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Save remember error: {ex.Message}");
            }
        }

        /// <summary>
        /// Load thông tin Remember Me từ file
        /// </summary>
        private void LoadRememberMe()
        {
            try
            {
                string filePath = System.IO.Path.Combine(Application.StartupPath, REMEMBER_FILE);

                if (System.IO.File.Exists(filePath))
                {
                    string encodedData = System.IO.File.ReadAllText(filePath);
                    string decodedData = System.Text.Encoding.UTF8.GetString(
                        Convert.FromBase64String(encodedData)
                    );

                    string[] parts = decodedData.Split('|');
                    if (parts.Length == 2)
                    {
                        txtUsername.Texts = parts[0];
                        txtPassword.Texts = parts[1];
                        chkRememberMe.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Load remember error: {ex.Message}");
            }
        }

        /// <summary>
        /// Xóa file Remember Me
        /// </summary>
        private void ClearRememberMe()
        {
            try
            {
                string filePath = System.IO.Path.Combine(Application.StartupPath, REMEMBER_FILE);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Clear remember error: {ex.Message}");
            }
        }
    }
}
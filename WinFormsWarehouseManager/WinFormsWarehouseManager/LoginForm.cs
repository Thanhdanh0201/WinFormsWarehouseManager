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

        public LoginForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            LoadRememberMe();
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

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

                // Mở MainForm
                MessageBox.Show($"Đăng nhập thành công!\nXin chào {user.FullName}",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MainForm mainForm = new MainForm();
                this.Hide();
                mainForm.ShowDialog();
                this.Close();
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
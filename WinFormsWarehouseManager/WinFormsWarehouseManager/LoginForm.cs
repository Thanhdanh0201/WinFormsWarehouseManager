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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

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

            string email = txtUsername.Text.Trim();
            string password = txtPassword.Text;

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
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        /// <summary>
        /// Xác thực thông tin đăng nhập
        /// </summary>
        private User AuthenticateUser(string email, string password)
        {
            try
            {
                string query = @"SELECT UserID, FullName, BirthDate, Email, CreatedAt 
                               FROM Users 
                               WHERE Email = @Email AND MailboxPassword = @Password";

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
                        BirthDate = row["BirthDate"].ToString(),
                        Email = row["Email"].ToString(),
                        CreatedAt = row["CreatedAt"].ToString()
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
                // Log lỗi nhưng không hiển thị cho user
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
                        txtUsername.Text = parts[0];
                        txtPassword.Text = parts[1];
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
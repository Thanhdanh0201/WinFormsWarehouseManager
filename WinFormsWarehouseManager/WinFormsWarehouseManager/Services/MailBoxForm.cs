using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsWarehouseManager.Models;
using WinFormsWarehouseManager.Services;
using WinFormsWarehouseManager.db;

namespace WinFormsWarehouseManager.Forms
{
    public partial class MailBoxForm : Form
    {
        private EmailService _emailService;
        private DatabaseHelper _dbHelper;
        private List<EmailModel> _currentEmails;
        private EmailModel _selectedEmail;
        private string _currentFolder = "INBOX";
        private bool _isComposing = false;
        private Color _accentColor = Color.FromArgb(2, 51, 66);
        private Color _cardColor = Color.FromArgb(200, 210, 215); // Trộn accent với trắng
        private Color _bgColor = Color.FromArgb(240, 240, 240); // Xám nhạt

        public MailBoxForm()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _currentEmails = new List<EmailModel>();
            InitializeMailbox();
        }

        private void InitializeMailbox()
        {
            try
            {
                /*
                // Lấy thông tin user từ UserSession
                if (!UserSession.IsLoggedIn)
                {
                    MessageBox.Show("Vui lòng đăng nhập trước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                // Lấy password từ database
                string query = "SELECT MailboxPassword FROM Users WHERE Email = @Email";
                var dt = _dbHelper.ExecuteQuery(query, new System.Data.SQLite.SQLiteParameter[]
                {
                    new System.Data.SQLite.SQLiteParameter("@Email", UserSession.CurrentUserEmail)
                });

                if (dt == null || dt.Rows.Count == 0 || string.IsNullOrEmpty(dt.Rows[0]["MailboxPassword"].ToString()))
                {
                    MessageBox.Show("Không tìm thấy mật khẩu email. Vui lòng cập nhật trong hệ thống!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                string password = dt.Rows[0]["MailboxPassword"].ToString();
                _emailService = new EmailService(UserSession.CurrentUserEmail, password);*/
                // Test kết nối

                _emailService.Connect();
                LoadEmails();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo mailbox:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadEmails()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _currentEmails = _emailService.GetEmails(_currentFolder, 50);
                DisplayEmailCards();
                UpdateInboxCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải email:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void DisplayEmailCards()
        {
            flowEmailList.Controls.Clear();
            int displayCount = Math.Min(_currentEmails.Count, 7);

            for (int i = 0; i < displayCount; i++)
            {
                var email = _currentEmails[i];
                var card = CreateEmailCard(email);
                flowEmailList.Controls.Add(card);
            }
        }

        private Panel CreateEmailCard(EmailModel email)
        {
            Panel card = new Panel
            {
                Width = flowEmailList.Width - 40,
                Height = 80,
                BackColor = _cardColor,
                Cursor = Cursors.Hand,
                Tag = email,
                Margin = new Padding(0, 0, 0, 10)
            };

            // TableLayoutPanel for card content
            TableLayoutPanel cardLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(10)
            };
            cardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            cardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            cardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            cardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));

            // From label
            Label lblFrom = new Label
            {
                Text = email.FromName,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = _accentColor,
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Date label
            Label lblDate = new Label
            {
                Text = email.DateDisplay,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleRight
            };

            // Subject label
            Label lblSubject = new Label
            {
                Text = email.Subject,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9.5f, email.IsRead ? FontStyle.Regular : FontStyle.Bold),
                ForeColor = Color.Black,
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleLeft
            };

            cardLayout.Controls.Add(lblFrom, 0, 0);
            cardLayout.Controls.Add(lblDate, 1, 0);
            cardLayout.Controls.Add(lblSubject, 0, 1);
            cardLayout.SetColumnSpan(lblSubject, 2);

            card.Controls.Add(cardLayout);

            // Events
            card.Click += (s, e) => OnEmailCardClick(email);
            cardLayout.Click += (s, e) => OnEmailCardClick(email);
            foreach (Control ctrl in cardLayout.Controls)
            {
                ctrl.Click += (s, e) => OnEmailCardClick(email);
            }

            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(180, 200, 210);
            card.MouseLeave += (s, e) => card.BackColor = _cardColor;

            return card;
        }

        private void OnEmailCardClick(EmailModel email)
        {
            try
            {
                _selectedEmail = _emailService.GetEmailByUid(email.FolderName, email.Uid);
                _isComposing = false;
                ShowDetailPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải email:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowDetailPanel()
        {
            _composePanel.Visible = false;
            _detailPanel.Visible = true;
            _detailPanel.Controls.Clear();

            int yPos = 20;

            // From
            AddDetailLabel("From:", _selectedEmail.From, ref yPos);
            // To
            AddDetailLabel("To:", _selectedEmail.To, ref yPos);
            // Date
            AddDetailLabel("Date:", _selectedEmail.Date.ToString("dd/MM/yyyy HH:mm:ss"), ref yPos);
            // Subject
            AddDetailLabel("Subject:", _selectedEmail.Subject, ref yPos);

            yPos += 10;

            // Body
            TextBox txtBody = new TextBox
            {
                Location = new Point(20, yPos),
                Width = _detailPanel.Width - 40,
                Height = _detailPanel.Height - yPos - 70,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Text = _selectedEmail.Body,
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            _detailPanel.Controls.Add(txtBody);

            yPos = _detailPanel.Height - 50;

            // Buttons
            Button btnReply = CreateButton("Reply", 20, yPos, 100);
            btnReply.Click += BtnReply_Click;

            Button btnDelete = CreateButton("Delete", 130, yPos, 100);
            btnDelete.Click += BtnDelete_Click;

            _detailPanel.Controls.AddRange(new Control[] { btnReply, btnDelete });
        }

        private void AddDetailLabel(string label, string value, ref int yPos)
        {
            Label lbl = new Label
            {
                Text = label,
                Location = new Point(20, yPos),
                Width = 80,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = _accentColor
            };

            Label lblValue = new Label
            {
                Text = value,
                Location = new Point(110, yPos),
                Width = _detailPanel.Width - 130,
                Font = new Font("Segoe UI", 10),
                AutoEllipsis = true
            };

            _detailPanel.Controls.AddRange(new Control[] { lbl, lblValue });
            yPos += 30;
        }

        private void ShowComposePanel(bool isReply = false)
        {
            _detailPanel.Visible = false;
            _composePanel.Visible = true;
            _composePanel.Controls.Clear();
            _isComposing = true;

            int yPos = 20;

            // To
            Label lblTo = new Label { Text = "To:", Location = new Point(20, yPos), Width = 80, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtTo = new TextBox { Name = "txtTo", Location = new Point(110, yPos), Width = _composePanel.Width - 130 };
            yPos += 35;

            // Subject
            Label lblSubject = new Label { Text = "Subject:", Location = new Point(20, yPos), Width = 80, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtSubject = new TextBox { Name = "txtSubject", Location = new Point(110, yPos), Width = _composePanel.Width - 130 };
            yPos += 35;

            // Body
            Label lblBody = new Label { Text = "Message:", Location = new Point(20, yPos), Width = 80, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            yPos += 25;
            TextBox txtBody = new TextBox
            {
                Name = "txtBody",
                Location = new Point(20, yPos),
                Width = _composePanel.Width - 40,
                Height = _composePanel.Height - yPos - 70,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 10)
            };

            if (isReply && _selectedEmail != null)
            {
                txtTo.Text = _selectedEmail.FromEmail;
                txtSubject.Text = _selectedEmail.Subject.StartsWith("Re:") ? _selectedEmail.Subject : "Re: " + _selectedEmail.Subject;
                txtBody.Text = "\n\n--- Original Message ---\n" + _selectedEmail.Body;
                txtTo.ReadOnly = true;
                txtSubject.ReadOnly = true;
            }

            yPos = _composePanel.Height - 50;

            Button btnSend = CreateButton("Send", 20, yPos, 100);
            btnSend.Click += (s, e) => BtnSend_Click(txtTo.Text, txtSubject.Text, txtBody.Text, isReply);

            Button btnCancel = CreateButton("Cancel", 130, yPos, 100);
            btnCancel.Click += (s, e) =>
            {
                _isComposing = false;
                _composePanel.Visible = false;
                _detailPanel.Visible = false;
            };

            _composePanel.Controls.AddRange(new Control[] { lblTo, txtTo, lblSubject, txtSubject, lblBody, txtBody, btnSend, btnCancel });
        }

        private Button CreateButton(string text, int x, int y, int width)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Width = width,
                Height = 35,
                BackColor = _accentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private void BtnReply_Click(object sender, EventArgs e)
        {
            ShowComposePanel(true);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa email này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _emailService.MoveToTrash(_selectedEmail.FolderName, _selectedEmail.Uid);
                    MessageBox.Show("Đã chuyển email vào Trash!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmails();
                    _detailPanel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa email:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSend_Click(string to, string subject, string body, bool isReply)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(to) || string.IsNullOrWhiteSpace(subject))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Cursor = Cursors.WaitCursor;

                if (isReply)
                    _emailService.ReplyEmail(_selectedEmail, body);
                else
                    _emailService.SendEmail(to, subject, body);

                MessageBox.Show("Đã gửi email thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isComposing = false;
                _composePanel.Visible = false;
                LoadEmails();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi gửi email:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BtnCompose_Click(object sender, EventArgs e)
        {
            ShowComposePanel(false);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = _txtSearchBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    LoadEmails();
                    return;
                }

                Cursor = Cursors.WaitCursor;
                _currentEmails = _emailService.SearchEmails(_currentFolder, keyword);
                DisplayEmailCards();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BtnFolder_Click(string folderName)
        {
            _currentFolder = folderName;
            LoadEmails();
            _detailPanel.Visible = false;
            _composePanel.Visible = false;
        }

        private void UpdateInboxCount()
        {
            try
            {
                int count = _emailService.GetInboxCount();
                var btnInbox = this.Controls.Find("btnInbox", true).FirstOrDefault() as Button;
                if (btnInbox != null)
                {
                    btnInbox.Text = $"Inbox ({count})";
                }
            }
            catch { }
        }

        private void TxtSearchBox_Enter(object sender, EventArgs e)
        {
            if (_txtSearchBox.Text == "Search emails...")
            {
                _txtSearchBox.Text = "";
                _txtSearchBox.ForeColor = Color.Black;
            }
        }

        private void TxtSearchBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtSearchBox.Text))
            {
                _txtSearchBox.Text = "Search emails...";
                _txtSearchBox.ForeColor = Color.Gray;
            }
        }

        // Custom Title Bar Controls
        private Point _dragPoint;
        private bool _isDragging = false;

        private void PanelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragPoint = new Point(e.X, e.Y);
            }
        }

        private void PanelTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - _dragPoint.X, p.Y - _dragPoint.Y);
            }
        }

        private void PanelTitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _emailService?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsWarehouseManager.db;
using WinFormsWarehouseManager.Models;
using WinFormsWarehouseManager.Services;

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
        private Color _cardColor = Color.FromArgb(200, 210, 215);
        private Color _bgColor = Color.FromArgb(240, 240, 240);

        public MailBoxForm()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _currentEmails = new List<EmailModel>();
        }

        private void MailBoxForm_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeMailbox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in MailBoxForm_Load:\n{ex.Message}\n\nStack:\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeMailbox()
        {
            try
            {
                _emailService = new EmailService("wmdanhnguyenthanh24520264@gmail.com", "kuzcfeqdiwmcwuia");
                _emailService.Connect();
                LoadEmails();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in InitializeMailbox:\n{ex.Message}\n\nStack:\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void LoadEmails()
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Cursor = Cursors.WaitCursor;
                });

                _currentEmails = _emailService.GetEmails(_currentFolder, 50);

                this.Invoke((MethodInvoker)delegate
                {
                    DisplayEmailCards();
                    UpdateInboxCount();
                });
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show($"ERROR in LoadEmails:\n{ex.Message}\n\nStack:\n{ex.StackTrace}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
            finally
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Cursor = Cursors.Default;
                });
            }
        }

        private void DisplayEmailCards()
        {
            try
            {
                flowEmailList.SuspendLayout();
                flowEmailList.Controls.Clear();

                if (_currentEmails == null || _currentEmails.Count == 0)
                {
                    Label lblEmpty = new Label
                    {
                        Text = "No emails found",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 11),
                        ForeColor = Color.Gray,
                        Padding = new Padding(20)
                    };
                    flowEmailList.Controls.Add(lblEmpty);
                    flowEmailList.ResumeLayout();
                    return;
                }

                int displayCount = Math.Min(_currentEmails.Count, 50);

                for (int i = 0; i < displayCount; i++)
                {
                    try
                    {
                        var email = _currentEmails[i];
                        if (email != null)
                        {
                            var card = CreateEmailCard(email);
                            flowEmailList.Controls.Add(card);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"ERROR creating card {i}:\n{ex.Message}", "Error");
                    }
                }

                flowEmailList.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in DisplayEmailCards:\n{ex.Message}\n\nStack:\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateEmailCard(EmailModel email)
        {
            try
            {
                int cardWidth = flowEmailList.ClientSize.Width - 50;
                if (cardWidth < 100) cardWidth = 500;

                Panel card = new Panel
                {
                    Width = cardWidth,
                    Height = 80,
                    BackColor = _cardColor,
                    Cursor = Cursors.Hand,
                    Tag = email,
                    Margin = new Padding(0, 0, 0, 10)
                };

                TableLayoutPanel cardLayout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 2,
                    Padding = new Padding(10),
                    BackColor = Color.Transparent
                };

                cardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
                cardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                cardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                cardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

                Label lblFrom = new Label
                {
                    Text = email.FromName ?? "Unknown",
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = _accentColor,
                    AutoEllipsis = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    BackColor = Color.Transparent
                };

                Label lblDate = new Label
                {
                    Text = email.DateDisplay ?? DateTime.Now.ToString("dd/MM/yyyy"),
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.Gray,
                    TextAlign = ContentAlignment.MiddleRight,
                    BackColor = Color.Transparent
                };

                Label lblSubject = new Label
                {
                    Text = email.Subject ?? "(No Subject)",
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 9.5f, email.IsRead ? FontStyle.Regular : FontStyle.Bold),
                    ForeColor = Color.Black,
                    AutoEllipsis = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    BackColor = Color.Transparent
                };

                cardLayout.Controls.Add(lblFrom, 0, 0);
                cardLayout.Controls.Add(lblDate, 1, 0);
                cardLayout.Controls.Add(lblSubject, 0, 1);
                cardLayout.SetColumnSpan(lblSubject, 2);

                card.Controls.Add(cardLayout);

                card.Click += (s, ev) => OnEmailCardClick(email);
                cardLayout.Click += (s, ev) => OnEmailCardClick(email);
                lblFrom.Click += (s, ev) => OnEmailCardClick(email);
                lblDate.Click += (s, ev) => OnEmailCardClick(email);
                lblSubject.Click += (s, ev) => OnEmailCardClick(email);

                card.MouseEnter += (s, ev) => card.BackColor = Color.FromArgb(180, 200, 210);
                card.MouseLeave += (s, ev) => card.BackColor = _cardColor;

                return card;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in CreateEmailCard:\n{ex.Message}\n\nStack:\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Panel { Width = 500, Height = 80, BackColor = Color.Red };
            }
        }

        private void OnEmailCardClick(EmailModel email)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _selectedEmail = _emailService.GetEmailByUid(email.FolderName, email.Uid);
                _isComposing = false;
                ShowDetailPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in OnEmailCardClick:\n{ex.Message}\n\nStack:\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ShowDetailPanel()
        {
            try
            {
                _composePanel.Visible = false;
                _detailPanel.Visible = true;
                _detailPanel.Controls.Clear();

                int yPos = 20;
                int leftMargin = 30;

                AddDetailLabel("From:", _selectedEmail.From ?? "", ref yPos, leftMargin);
                AddDetailLabel("To:", _selectedEmail.To ?? "", ref yPos, leftMargin);
                AddDetailLabel("Date:", _selectedEmail.Date.ToString("dd/MM/yyyy HH:mm:ss"), ref yPos, leftMargin);
                AddDetailLabel("Subject:", _selectedEmail.Subject ?? "", ref yPos, leftMargin);

                yPos += 20;

                TextBox txtBody = new TextBox
                {
                    Location = new Point(leftMargin, yPos),
                    Width = _detailPanel.Width - (leftMargin * 2) - 20,
                    Height = _detailPanel.Height - yPos - 100,
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    ReadOnly = true,
                    Text = _selectedEmail.Body ?? "",
                    Font = new Font("Segoe UI", 10),
                    BorderStyle = BorderStyle.FixedSingle,
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
                };
                _detailPanel.Controls.Add(txtBody);

                Button btnReply = new Button
                {
                    Text = "Reply",
                    Location = new Point(leftMargin, _detailPanel.Height - 60),
                    Width = 100,
                    Height = 40,
                    BackColor = _accentColor,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                };
                btnReply.Click += BtnReply_Click;

                Button btnDelete = new Button
                {
                    Text = "Delete",
                    Location = new Point(leftMargin + 110, _detailPanel.Height - 60),
                    Width = 100,
                    Height = 40,
                    BackColor = Color.FromArgb(220, 53, 69),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                };
                btnDelete.Click += BtnDelete_Click;

                _detailPanel.Controls.Add(btnReply);
                _detailPanel.Controls.Add(btnDelete);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in ShowDetailPanel:\n{ex.Message}\n\nStack:\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddDetailLabel(string label, string value, ref int yPos, int leftMargin)
        {
            Label lbl = new Label
            {
                Text = label,
                Location = new Point(leftMargin, yPos),
                Width = 80,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = _accentColor,
                AutoSize = false
            };

            Label lblValue = new Label
            {
                Text = value,
                Location = new Point(leftMargin + 90, yPos),
                Width = _detailPanel.Width - (leftMargin + 90) - 40,
                Font = new Font("Segoe UI", 10),
                AutoEllipsis = true,
                AutoSize = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            _detailPanel.Controls.Add(lbl);
            _detailPanel.Controls.Add(lblValue);
            yPos += 35;
        }

        private void ShowComposePanel(bool isReply = false)
        {
            try
            {
                _detailPanel.Visible = false;
                _composePanel.Visible = true;
                _composePanel.Controls.Clear();
                _isComposing = true;

                int yPos = 20;
                int leftMargin = 30;

                Label lblTo = new Label
                {
                    Text = "To:",
                    Location = new Point(leftMargin, yPos),
                    Width = 80,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };

                TextBox txtTo = new TextBox
                {
                    Name = "txtTo",
                    Location = new Point(leftMargin + 90, yPos),
                    Width = _composePanel.Width - (leftMargin + 90) - 40,
                    Font = new Font("Segoe UI", 10),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                yPos += 40;

                Label lblSubject = new Label
                {
                    Text = "Subject:",
                    Location = new Point(leftMargin, yPos),
                    Width = 80,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };

                TextBox txtSubject = new TextBox
                {
                    Name = "txtSubject",
                    Location = new Point(leftMargin + 90, yPos),
                    Width = _composePanel.Width - (leftMargin + 90) - 40,
                    Font = new Font("Segoe UI", 10),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                yPos += 40;

                Label lblBody = new Label
                {
                    Text = "Message:",
                    Location = new Point(leftMargin, yPos),
                    Width = 100,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                yPos += 30;

                TextBox txtBody = new TextBox
                {
                    Name = "txtBody",
                    Location = new Point(leftMargin, yPos),
                    Width = _composePanel.Width - (leftMargin * 2) - 20,
                    Height = _composePanel.Height - yPos - 100,
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    Font = new Font("Segoe UI", 10),
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
                };

                if (isReply && _selectedEmail != null)
                {
                    txtTo.Text = _selectedEmail.FromEmail ?? "";
                    txtSubject.Text = _selectedEmail.Subject?.StartsWith("Re:") == true ?
                        _selectedEmail.Subject : "Re: " + _selectedEmail.Subject;
                    txtBody.Text = "\n\n--- Original Message ---\n" + (_selectedEmail.Body ?? "");
                    txtTo.ReadOnly = true;
                    txtSubject.ReadOnly = true;
                }

                Button btnSend = new Button
                {
                    Text = "Send",
                    Location = new Point(leftMargin, _composePanel.Height - 60),
                    Width = 100,
                    Height = 40,
                    BackColor = _accentColor,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                };
                btnSend.Click += (s, e) => BtnSend_Click(txtTo.Text, txtSubject.Text, txtBody.Text, isReply);

                Button btnCancel = new Button
                {
                    Text = "Cancel",
                    Location = new Point(leftMargin + 110, _composePanel.Height - 60),
                    Width = 100,
                    Height = 40,
                    BackColor = Color.Gray,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                };
                btnCancel.Click += (s, e) =>
                {
                    _isComposing = false;
                    _composePanel.Visible = false;
                    _detailPanel.Visible = false;
                };

                _composePanel.Controls.AddRange(new Control[] {
                    lblTo, txtTo, lblSubject, txtSubject, lblBody, txtBody, btnSend, btnCancel
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in ShowComposePanel:\n{ex.Message}\n\nStack:\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    MessageBox.Show("Đã chuyển email vào Trash!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmails();
                    _detailPanel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in BtnDelete_Click:\n{ex.Message}\n\nStack:\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSend_Click(string to, string subject, string body, bool isReply)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(to) || string.IsNullOrWhiteSpace(subject))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Cursor = Cursors.WaitCursor;

                if (isReply)
                    _emailService.ReplyEmail(_selectedEmail, body);
                else
                    _emailService.SendEmail(to, subject, body);

                MessageBox.Show("Đã gửi email thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isComposing = false;
                _composePanel.Visible = false;
                LoadEmails();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in BtnSend_Click:\n{ex.Message}\n\nStack:\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (string.IsNullOrWhiteSpace(keyword) || keyword == "Search emails...")
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
                MessageBox.Show($"ERROR in BtnSearch_Click:\n{ex.Message}\n\nStack:\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void BtnInbox_Click(object sender, EventArgs e)
        {
            BtnFolder_Click("INBOX");
        }

        private void BtnSent_Click(object sender, EventArgs e)
        {
            BtnFolder_Click("[Gmail]/Sent Mail");
        }

        private void BtnTrash_Click(object sender, EventArgs e)
        {
            BtnFolder_Click("[Gmail]/Trash");
        }

        private void UpdateInboxCount()
        {
            try
            {
                int count = _emailService.GetInboxCount();
                btnInbox.Text = $"Inbox ({count})";
            }
            catch (Exception ex)
            {
                // Silent fail
            }
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

        private void flowEmailList_Paint(object sender, PaintEventArgs e)
        {
            // Empty event handler
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                _emailService?.Dispose();
            }
            catch { }
            base.OnFormClosing(e);
        }
    }
}
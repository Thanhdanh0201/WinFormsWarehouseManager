using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsWarehouseManager.CustomControls;
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

        // UI Colors
        private Color _accentColor = Color.FromArgb(2, 51, 66);
        private Color _cardColor = Color.FromArgb(200, 210, 215);
        private Color _bgColor = Color.FromArgb(240, 240, 240);

        // Cache system
        private static Dictionary<string, List<EmailModel>> _emailCache;
        private static Dictionary<uint, EmailModel> _emailDetailCache;
        private static DateTime _lastCacheUpdate;
        private const int CACHE_DURATION_MINUTES = 5;

        // Thread synchronization
        private static SemaphoreSlim _emailServiceLock = new SemaphoreSlim(1, 1);
        private CancellationTokenSource _cancellationTokenSource;

        // UI Components
        private CustomTextBox _txtSearchBox;
        private Panel _detailPanel;
        private Panel _composePanel;
        private Label _lblLoadingIndicator;

        // Folder buttons for highlighting
        private IconButton _btnInbox;
        private IconButton _btnSent;
        private IconButton _btnTrash;
        private IconButton _btnCompose;

        public MailBoxForm()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _currentEmails = new List<EmailModel>();

            // Initialize cache
            if (_emailCache == null)
            {
                _emailCache = new Dictionary<string, List<EmailModel>>();
                _emailDetailCache = new Dictionary<uint, EmailModel>();
                _lastCacheUpdate = DateTime.MinValue;
            }

            // Optimize rendering
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Custom Search TextBox
            _txtSearchBox = new CustomTextBox
            {
                BackColor = SystemColors.Window,
                BorderColor = Color.FromArgb(2, 51, 66),
                BorderFocusColor = Color.FromArgb(0, 35, 44),
                BorderRadius = 8,
                BorderSize = 2,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(64, 64, 64),
                Multiline = false,
                Name = "txtSearchBox",
                Padding = new Padding(15, 10, 15, 10),
                PasswordChar = false,
                PlaceholderColor = Color.DarkGray,
                PlaceholderText = "Search emails...",
                Texts = "",
                UnderlinedStyle = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // Detail Panel
            _detailPanel = new Panel
            {
                Name = "detailPanel",
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Visible = false,
                AutoScroll = true
            };

            // Compose Panel
            _composePanel = new Panel
            {
                Name = "composePanel",
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Visible = false,
                AutoScroll = true
            };

            // Loading Indicator
            _lblLoadingIndicator = new Label
            {
                Text = "⏳ Loading emails...",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = _accentColor,
                AutoSize = true,
                Visible = false
            };
        }

        private void MailBoxForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Setup UI Layout
                SetupMainLayout();

                // Add custom components to panels
                panelSearch.Controls.Add(_txtSearchBox);
                _txtSearchBox.Location = new Point(20, 25);
                _txtSearchBox.Size = new Size(panelSearch.Width - 220, 66);

                panelRight.Controls.Add(_detailPanel);
                panelRight.Controls.Add(_composePanel);

                flowEmailList.Controls.Add(_lblLoadingIndicator);
                _lblLoadingIndicator.Location = new Point(20, 20);

                // Setup initial layout - Hide right panel
                HideDetailPanel();

                SetupIconButtons();

                // Highlight Inbox button by default
                HighlightFolderButton(_btnInbox);

                InitializeMailbox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupMainLayout()
        {
            // Main form settings
            this.BackColor = _bgColor;

            // Panel styling
            panelLeft.BackColor = Color.White;
            panelCenter.BackColor = _bgColor;
            panelRight.BackColor = Color.White;
            panelSearch.BackColor = Color.White;

            // FlowLayoutPanel styling
            flowEmailList.BackColor = _bgColor;
            flowEmailList.AutoScroll = true;
            flowEmailList.Padding = new Padding(20, 10, 20, 10);
        }

        private void SetupIconButtons()
        {
            panelLeftContent.Controls.Clear();

            // Compose Button
            _btnCompose = new IconButton
            {
                IconChar = IconChar.PenToSquare,
                IconColor = _accentColor,
                IconSize = 24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Text = "   Compose",
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                ForeColor = _accentColor,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Padding = new Padding(25, 0, 0, 0),
                Margin = new Padding(0, 15, 0, 0),
                Tag = "btnCompose"
            };
            _btnCompose.FlatAppearance.BorderSize = 0;
            _btnCompose.Click += BtnCompose_Click;

            // Inbox Button
            _btnInbox = new IconButton
            {
                IconChar = IconChar.Inbox,
                IconColor = _accentColor,
                IconSize = 24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Text = "   Inbox",
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                ForeColor = _accentColor,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Padding = new Padding(25, 0, 0, 0),
                Margin = new Padding(0, 15, 0, 0),
                Tag = "btnInbox"
            };
            _btnInbox.FlatAppearance.BorderSize = 0;
            _btnInbox.Click += BtnInbox_Click;

            // Sent Button
            _btnSent = new IconButton
            {
                IconChar = IconChar.PaperPlane,
                IconColor = _accentColor,
                IconSize = 24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Text = "   Sent",
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                ForeColor = _accentColor,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Padding = new Padding(25, 0, 0, 0),
                Margin = new Padding(0, 5, 0, 0),
                Tag = "btnSent"
            };
            _btnSent.FlatAppearance.BorderSize = 0;
            _btnSent.Click += BtnSent_Click;

            // Trash Button
            _btnTrash = new IconButton
            {
                IconChar = IconChar.TrashAlt,
                IconColor = _accentColor,
                IconSize = 24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Text = "   Trash",
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                ForeColor = _accentColor,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Padding = new Padding(25, 0, 0, 0),
                Margin = new Padding(0, 5, 0, 0),
                Tag = "btnTrash"
            };
            _btnTrash.FlatAppearance.BorderSize = 0;
            _btnTrash.Click += BtnTrash_Click;

            // Search Button
            var oldSearch = panelSearch.Controls.OfType<Button>().FirstOrDefault(b => b.Name == "btnSearch");
            if (oldSearch != null) panelSearch.Controls.Remove(oldSearch);

            var iconSearch = new IconButton
            {
                Name = "btnSearch",
                IconChar = IconChar.MagnifyingGlass,
                IconColor = Color.White,
                IconSize = 26,
                Text = "",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Width = 160,
                Height = 66,
                BackColor = _accentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            iconSearch.FlatAppearance.BorderSize = 0;
            iconSearch.Click += BtnSearch_Click;
            iconSearch.MouseEnter += (s, e) => iconSearch.BackColor = Color.FromArgb(0, 70, 90);
            iconSearch.MouseLeave += (s, e) => iconSearch.BackColor = _accentColor;

            // Add buttons in reverse order (Dock.Top stacks bottom-to-top)
            panelLeftContent.Controls.Add(_btnTrash);
            panelLeftContent.Controls.Add(_btnSent);
            panelLeftContent.Controls.Add(_btnInbox);
            panelLeftContent.Controls.Add(_btnCompose);

            panelSearch.Controls.Add(iconSearch);
            iconSearch.Location = new Point(panelSearch.Width - iconSearch.Width - 20, 25);
            iconSearch.BringToFront();
        }

        private void HighlightFolderButton(IconButton activeButton)
        {
            // Reset all folder buttons
            var allButtons = new[] { _btnInbox, _btnSent, _btnTrash };
            foreach (var btn in allButtons)
            {
                if (btn != null)
                {
                    btn.BackColor = Color.White;
                    btn.MouseEnter -= FolderButton_MouseEnter;
                    btn.MouseLeave -= FolderButton_MouseLeave;
                    btn.MouseEnter += FolderButton_MouseEnter;
                    btn.MouseLeave += FolderButton_MouseLeave;
                }
            }

            // Highlight active button
            if (activeButton != null)
            {
                activeButton.BackColor = Color.FromArgb(240, 245, 248);
                activeButton.MouseEnter -= FolderButton_MouseEnter;
                activeButton.MouseLeave -= FolderButton_MouseLeave;
            }
        }

        private void FolderButton_MouseEnter(object sender, EventArgs e)
        {
            if (sender is IconButton btn)
            {
                btn.BackColor = Color.FromArgb(220, 230, 240);
            }
        }

        private void FolderButton_MouseLeave(object sender, EventArgs e)
        {
            if (sender is IconButton btn)
            {
                btn.BackColor = Color.White;
            }
        }

        private void InitializeMailbox()
        {
            try
            {
                if (!UserSession.IsLoggedIn)
                {
                    MessageBox.Show("Vui lòng đăng nhập trước khi sử dụng chức năng email.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string email = UserSession.CurrentUserEmail;
                string mailboxPassword = UserSession.CurrentUserMailboxPassword;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(mailboxPassword))
                {
                    MessageBox.Show("Bạn chưa cấu hình thông tin mailbox.\nVui lòng cập nhật trong phần cài đặt tài khoản.",
                        "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _emailService = new EmailService(email, mailboxPassword);
                _emailService.Connect();

                _ = LoadEmailsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo mailbox:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadEmailsAsync()
        {
            try
            {
                if (IsCacheValid(_currentFolder))
                {
                    _currentEmails = _emailCache[_currentFolder];
                    DisplayEmailCards();
                    UpdateInboxCount();
                    return;
                }

                ShowLoadingIndicator(true);

                await _emailServiceLock.WaitAsync();
                try
                {
                    await Task.Run(() =>
                    {
                        _currentEmails = _emailService.GetEmails(_currentFolder, 50);
                        _emailCache[_currentFolder] = _currentEmails;
                        _lastCacheUpdate = DateTime.Now;
                    });
                }
                finally
                {
                    _emailServiceLock.Release();
                }

                DisplayEmailCards();
                UpdateInboxCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải emails:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoadingIndicator(false);
            }
        }

        private void ShowLoadingIndicator(bool show)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowLoadingIndicator(show)));
                return;
            }

            _lblLoadingIndicator.Visible = show;

            if (show)
            {
                Cursor = Cursors.WaitCursor;
                flowEmailList.Enabled = false;
            }
            else
            {
                Cursor = Cursors.Default;
                flowEmailList.Enabled = true;
            }
        }

        private bool IsCacheValid(string folder)
        {
            return _emailCache.ContainsKey(folder) &&
                   (DateTime.Now - _lastCacheUpdate).TotalMinutes < CACHE_DURATION_MINUTES;
        }

        private void DisplayEmailCards()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(DisplayEmailCards));
                return;
            }

            try
            {
                flowEmailList.SuspendLayout();
                flowEmailList.Controls.Clear();
                flowEmailList.Controls.Add(_lblLoadingIndicator);

                if (_currentEmails == null || _currentEmails.Count == 0)
                {
                    Panel emptyPanel = new Panel
                    {
                        Width = flowEmailList.ClientSize.Width - 40,
                        Height = 150,
                        BackColor = Color.White,
                        Margin = new Padding(0, 20, 0, 0)
                    };

                    Label lblEmpty = new Label
                    {
                        Text = "📭\n\nNo emails found",
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 14, FontStyle.Regular),
                        ForeColor = Color.Gray,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    emptyPanel.Controls.Add(lblEmpty);
                    flowEmailList.Controls.Add(emptyPanel);
                    flowEmailList.ResumeLayout();
                    return;
                }

                foreach (var email in _currentEmails.Take(50))
                {
                    if (email != null)
                    {
                        var card = CreateEmailCard(email);
                        flowEmailList.Controls.Add(card);
                    }
                }

                flowEmailList.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị emails:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateEmailCard(EmailModel email)
        {
            try
            {
                int cardWidth = flowEmailList.ClientSize.Width - 50;
                if (cardWidth < 100) cardWidth = 600;

                Panel card = new Panel
                {
                    Width = cardWidth,
                    Height = 100,
                    BackColor = email.IsRead ? Color.White : Color.FromArgb(240, 248, 255),
                    Cursor = Cursors.Hand,
                    Tag = email,
                    Margin = new Padding(0, 0, 0, 10),
                    BorderStyle = BorderStyle.FixedSingle
                };

                if (!email.IsRead)
                {
                    Panel indicator = new Panel
                    {
                        Width = 5,
                        Dock = DockStyle.Left,
                        BackColor = _accentColor
                    };
                    card.Controls.Add(indicator);
                }

                TableLayoutPanel cardLayout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 2,
                    Padding = new Padding(email.IsRead ? 18 : 23, 15, 18, 15),
                    BackColor = Color.Transparent
                };

                cardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
                cardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                cardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
                cardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

                Label lblFrom = new Label
                {
                    Text = email.FromName ?? "Unknown",
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                    ForeColor = _accentColor,
                    AutoEllipsis = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    BackColor = Color.Transparent
                };

                Label lblDate = new Label
                {
                    Text = email.DateDisplay ?? DateTime.Now.ToString("dd/MM/yyyy"),
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.Gray,
                    TextAlign = ContentAlignment.MiddleRight,
                    BackColor = Color.Transparent
                };

                Label lblSubject = new Label
                {
                    Text = email.Subject ?? "(No Subject)",
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 10F, email.IsRead ? FontStyle.Regular : FontStyle.Bold),
                    ForeColor = email.IsRead ? Color.FromArgb(80, 80, 80) : Color.Black,
                    AutoEllipsis = true,
                    TextAlign = ContentAlignment.TopLeft,
                    BackColor = Color.Transparent
                };

                cardLayout.Controls.Add(lblFrom, 0, 0);
                cardLayout.Controls.Add(lblDate, 1, 0);
                cardLayout.Controls.Add(lblSubject, 0, 1);
                cardLayout.SetColumnSpan(lblSubject, 2);

                card.Controls.Add(cardLayout);
                cardLayout.BringToFront();

                card.Click += (s, ev) => OnEmailCardClick(email);
                cardLayout.Click += (s, ev) => OnEmailCardClick(email);
                lblFrom.Click += (s, ev) => OnEmailCardClick(email);
                lblDate.Click += (s, ev) => OnEmailCardClick(email);
                lblSubject.Click += (s, ev) => OnEmailCardClick(email);

                Color hoverColor = email.IsRead ? Color.FromArgb(245, 248, 250) : Color.FromArgb(230, 240, 255);
                Color normalColor = email.IsRead ? Color.White : Color.FromArgb(240, 248, 255);

                card.MouseEnter += (s, ev) => card.BackColor = hoverColor;
                card.MouseLeave += (s, ev) => card.BackColor = normalColor;

                return card;
            }
            catch (Exception ex)
            {
                return new Panel
                {
                    Width = 600,
                    Height = 100,
                    BackColor = Color.LightCoral,
                    Margin = new Padding(0, 0, 0, 10)
                };
            }
        }

        private async void OnEmailCardClick(EmailModel email)
        {
            try
            {
                if (!_emailDetailCache.ContainsKey(email.Uid))
                {
                    Cursor = Cursors.WaitCursor;

                    await _emailServiceLock.WaitAsync();
                    try
                    {
                        await Task.Run(() =>
                        {
                            _selectedEmail = _emailService.GetEmailByUid(email.FolderName, email.Uid);
                            _emailDetailCache[email.Uid] = _selectedEmail;
                        });
                    }
                    finally
                    {
                        _emailServiceLock.Release();
                    }

                    Cursor = Cursors.Default;
                }
                else
                {
                    _selectedEmail = _emailDetailCache[email.Uid];
                }

                _isComposing = false;
                ShowDetailPanel();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Lỗi khi mở email:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowDetailPanel()
        {
            try
            {
                panelRight.Visible = true;

                // Adjust column widths safely
                tableLayoutMain.SuspendLayout();

                if (tableLayoutMain.ColumnStyles.Count >= 3)
                {
                    tableLayoutMain.ColumnStyles[0].SizeType = SizeType.Absolute;
                    tableLayoutMain.ColumnStyles[0].Width = 300F;

                    tableLayoutMain.ColumnStyles[1].SizeType = SizeType.Percent;
                    tableLayoutMain.ColumnStyles[1].Width = 40F;

                    tableLayoutMain.ColumnStyles[2].SizeType = SizeType.Percent;
                    tableLayoutMain.ColumnStyles[2].Width = 60F;
                }

                tableLayoutMain.ResumeLayout();

                _composePanel.Visible = false;
                _detailPanel.Visible = true;
                _detailPanel.Controls.Clear();

                var btnBack = new IconButton
                {
                    IconChar = IconChar.ArrowLeft,
                    IconColor = _accentColor,
                    IconSize = 22,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    Text = "   Back",
                    TextAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Location = new Point(25, 15),
                    Width = 120,
                    Height = 40,
                    BackColor = Color.White,
                    ForeColor = _accentColor,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                btnBack.FlatAppearance.BorderColor = _accentColor;
                btnBack.FlatAppearance.BorderSize = 2;
                btnBack.Click += (s, e) => HideDetailPanel();

                int yPos = 75;
                int leftMargin = 25;

                Panel headerPanel = new Panel
                {
                    Location = new Point(leftMargin, yPos),
                    Width = _detailPanel.Width - (leftMargin * 2),
                    Height = 180,
                    BackColor = Color.FromArgb(248, 250, 252),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                int headerY = 15;
                AddHeaderField(headerPanel, "From:", _selectedEmail.From ?? "", ref headerY);
                AddHeaderField(headerPanel, "To:", _selectedEmail.To ?? "", ref headerY);
                AddHeaderField(headerPanel, "Date:", _selectedEmail.Date.ToString("dd/MM/yyyy HH:mm"), ref headerY);
                AddHeaderField(headerPanel, "Subject:", _selectedEmail.Subject ?? "(No Subject)", ref headerY);

                yPos += 200;

                TextBox txtBody = new TextBox
                {
                    Location = new Point(leftMargin, yPos),
                    Width = _detailPanel.Width - (leftMargin * 2) - 20,
                    Height = _detailPanel.Height - yPos - 80,
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    ReadOnly = true,
                    Text = _selectedEmail.Body ?? "",
                    Font = new Font("Segoe UI", 10.5F),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White,
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
                };

                var btnReply = new IconButton
                {
                    IconChar = IconChar.Reply,
                    IconColor = Color.White,
                    IconSize = 20,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    Text = "   Reply",
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Location = new Point(leftMargin, _detailPanel.Height - 60),
                    Width = 120,
                    Height = 45,
                    BackColor = _accentColor,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                };
                btnReply.FlatAppearance.BorderSize = 0;
                btnReply.Click += BtnReply_Click;

                var btnDelete = new IconButton
                {
                    IconChar = IconChar.TrashAlt,
                    IconColor = Color.White,
                    IconSize = 20,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    Text = "   Delete",
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Location = new Point(leftMargin + 135, _detailPanel.Height - 60),
                    Width = 120,
                    Height = 45,
                    BackColor = Color.FromArgb(220, 53, 69),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                };
                btnDelete.FlatAppearance.BorderSize = 0;
                btnDelete.Click += BtnDelete_Click;

                _detailPanel.Controls.Add(btnBack);
                _detailPanel.Controls.Add(headerPanel);
                _detailPanel.Controls.Add(txtBody);
                _detailPanel.Controls.Add(btnReply);
                _detailPanel.Controls.Add(btnDelete);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị detail:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddHeaderField(Panel parent, string label, string value, ref int yPos)
        {
            Label lblField = new Label
            {
                Text = label,
                Location = new Point(15, yPos),
                Width = 80,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                AutoSize = false
            };

            Label lblValue = new Label
            {
                Text = value,
                Location = new Point(100, yPos),
                Width = parent.Width - 115,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoEllipsis = true,
                AutoSize = false
            };

            parent.Controls.Add(lblField);
            parent.Controls.Add(lblValue);
            yPos += 35;
        }

        private void HideDetailPanel()
        {
            panelRight.Visible = false;
            _detailPanel.Visible = false;
            _composePanel.Visible = false;

            tableLayoutMain.SuspendLayout();

            if (tableLayoutMain.ColumnStyles.Count >= 3)
            {
                tableLayoutMain.ColumnStyles[0].SizeType = SizeType.Absolute;
                tableLayoutMain.ColumnStyles[0].Width = 300F;

                tableLayoutMain.ColumnStyles[1].SizeType = SizeType.Percent;
                tableLayoutMain.ColumnStyles[1].Width = 100F;

                tableLayoutMain.ColumnStyles[2].SizeType = SizeType.Absolute;
                tableLayoutMain.ColumnStyles[2].Width = 0F;
            }

            tableLayoutMain.ResumeLayout();
        }

        private void ShowComposePanel(bool isReply = false)
        {
            try
            {
                panelRight.Visible = true;

                tableLayoutMain.SuspendLayout();

                if (tableLayoutMain.ColumnStyles.Count >= 3)
                {
                    tableLayoutMain.ColumnStyles[0].SizeType = SizeType.Absolute;
                    tableLayoutMain.ColumnStyles[0].Width = 300F;

                    tableLayoutMain.ColumnStyles[1].SizeType = SizeType.Percent;
                    tableLayoutMain.ColumnStyles[1].Width = 40F;

                    tableLayoutMain.ColumnStyles[2].SizeType = SizeType.Percent;
                    tableLayoutMain.ColumnStyles[2].Width = 60F;
                }

                tableLayoutMain.ResumeLayout();

                _detailPanel.Visible = false;
                _composePanel.Visible = true;
                _composePanel.Controls.Clear();
                _isComposing = true;

                var btnBack = new IconButton
                {
                    IconChar = IconChar.ArrowLeft,
                    IconColor = _accentColor,
                    IconSize = 22,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    Text = "   Back",
                    TextAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Location = new Point(25, 15),
                    Width = 120,
                    Height = 40,
                    BackColor = Color.White,
                    ForeColor = _accentColor,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                btnBack.FlatAppearance.BorderColor = _accentColor;
                btnBack.FlatAppearance.BorderSize = 2;
                btnBack.Click += (s, e) => HideDetailPanel();

                int yPos = 75;
                int leftMargin = 25;

                Label lblTo = new Label
                {
                    Text = "To:",
                    Location = new Point(leftMargin, yPos),
                    Width = 100,
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = _accentColor
                };

                CustomTextBox txtTo = new CustomTextBox
                {
                    Name = "txtTo",
                    Location = new Point(leftMargin + 110, yPos - 5),
                    Width = _composePanel.Width - (leftMargin + 110) - 40,
                    Height = 45,  // THÊM
                    BackColor = SystemColors.Window,
                    BorderColor = Color.FromArgb(2, 51, 66),
                    BorderFocusColor = Color.FromArgb(0, 35, 44),
                    BorderRadius = 8,  // ĐỔI từ 5
                    BorderSize = 2,
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.FromArgb(64, 64, 64),  // THÊM
                    Multiline = false,  // THÊM
                    Padding = new Padding(15, 10, 15, 10),  // THÊM
                    PasswordChar = false,  // THÊM
                    PlaceholderColor = Color.DarkGray,  // THÊM
                    PlaceholderText = "Recipient email...",  // THÊM
                    Texts = "",  // THÊM
                    UnderlinedStyle = false,  // THÊM
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                yPos += 60;

                Label lblSubject = new Label
                {
                    Text = "Subject:",
                    Location = new Point(leftMargin, yPos),
                    Width = 100,
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = _accentColor
                };

                CustomTextBox txtSubject = new CustomTextBox
                {
                    Name = "txtSubject",
                    Location = new Point(leftMargin + 110, yPos - 5),
                    Width = _composePanel.Width - (leftMargin + 110) - 40,
                    Height = 45,  // THÊM
                    BackColor = SystemColors.Window,
                    BorderColor = Color.FromArgb(2, 51, 66),
                    BorderFocusColor = Color.FromArgb(0, 35, 44),
                    BorderRadius = 8,  // ĐỔI từ 5
                    BorderSize = 2,
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.FromArgb(64, 64, 64),  // THÊM
                    Multiline = false,  // THÊM
                    Padding = new Padding(15, 10, 15, 10),  // THÊM
                    PasswordChar = false,  // THÊM
                    PlaceholderColor = Color.DarkGray,  // THÊM
                    PlaceholderText = "Email subject...",  // THÊM
                    Texts = "",  // THÊM
                    UnderlinedStyle = false,  // THÊM
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                yPos += 60;

                Label lblBody = new Label
                {
                    Text = "Message:",
                    Location = new Point(leftMargin, yPos),
                    Width = 120,
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = _accentColor
                };
                yPos += 40;

                TextBox txtBody = new TextBox
                {
                    Name = "txtBody",
                    Location = new Point(leftMargin, yPos),
                    Width = _composePanel.Width - (leftMargin * 2) - 20,
                    Height = _composePanel.Height - yPos - 90,
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    Font = new Font("Segoe UI", 10.5F),
                    BorderStyle = BorderStyle.FixedSingle,
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
                };

                if (isReply && _selectedEmail != null)
                {
                    txtTo.Texts = _selectedEmail.FromEmail ?? "";
                    txtSubject.Texts = _selectedEmail.Subject?.StartsWith("Re:") == true ?
                        _selectedEmail.Subject : "Re: " + _selectedEmail.Subject;
                    txtBody.Text = "\n\n--- Original Message ---\n" + (_selectedEmail.Body ?? "");
                    txtTo.Enabled = false;
                    txtSubject.Enabled = false;
                }

                var btnSend = new IconButton
                {
                    IconChar = IconChar.PaperPlane,
                    IconColor = Color.White,
                    IconSize = 20,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    Text = "   Send",
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Location = new Point(leftMargin, _composePanel.Height - 60),
                    Width = 130,
                    Height = 45,
                    BackColor = _accentColor,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                };
                btnSend.FlatAppearance.BorderSize = 0;
                btnSend.Click += (s, e) => BtnSend_Click(txtTo.Texts, txtSubject.Texts, txtBody.Text, isReply);

                _composePanel.Controls.Add(btnBack);
                _composePanel.Controls.Add(lblTo);
                _composePanel.Controls.Add(txtTo);
                _composePanel.Controls.Add(lblSubject);
                _composePanel.Controls.Add(txtSubject);
                _composePanel.Controls.Add(lblBody);
                _composePanel.Controls.Add(txtBody);
                _composePanel.Controls.Add(btnSend);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị compose:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReply_Click(object sender, EventArgs e)
        {
            ShowComposePanel(true);
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa email này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;

                    await _emailServiceLock.WaitAsync();
                    try
                    {
                        await Task.Run(() =>
                        {
                            _emailService.MoveToTrash(_selectedEmail.FolderName, _selectedEmail.Uid);
                        });
                    }
                    finally
                    {
                        _emailServiceLock.Release();
                    }

                    _emailCache.Clear();
                    _emailDetailCache.Remove(_selectedEmail.Uid);

                    HideDetailPanel();

                    MessageBox.Show("Đã chuyển email vào Trash!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await LoadEmailsAsync();

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Lỗi khi xóa email:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnSend_Click(string to, string subject, string body, bool isReply)
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

                await _emailServiceLock.WaitAsync();
                try
                {
                    await Task.Run(() =>
                    {
                        if (isReply)
                            _emailService.ReplyEmail(_selectedEmail, body);
                        else
                            _emailService.SendEmail(to, subject, body);
                    });
                }
                finally
                {
                    _emailServiceLock.Release();
                }

                HideDetailPanel();

                MessageBox.Show("Đã gửi email thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _emailCache.Clear();

                // Chỉ reload nếu đang ở INBOX, không reload Sent folder
                if (_currentFolder == "INBOX")
                {
                    await LoadEmailsAsync();
                }
                else
                {
                    // Chuyển về INBOX và highlight button
                    _currentFolder = "INBOX";
                    HighlightFolderButton(_btnInbox);
                    await LoadEmailsAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi email:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = _txtSearchBox.Texts.Trim();
                if (string.IsNullOrWhiteSpace(keyword) || keyword == "Search emails...")
                {
                    await LoadEmailsAsync();
                    return;
                }

                ShowLoadingIndicator(true);

                await _emailServiceLock.WaitAsync();
                try
                {
                    await Task.Run(() =>
                    {
                        _currentEmails = _emailService.SearchEmails(_currentFolder, keyword);
                    });
                }
                finally
                {
                    _emailServiceLock.Release();
                }

                DisplayEmailCards();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoadingIndicator(false);
            }
        }

        private void TxtSearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                BtnSearch_Click(sender, e);
            }
        }

        private async void BtnFolder_Click(string folderName, IconButton clickedButton)
        {
            _currentFolder = folderName;
            HideDetailPanel();
            HighlightFolderButton(clickedButton);
            await LoadEmailsAsync();
        }

        private void BtnInbox_Click(object sender, EventArgs e)
        {
            BtnFolder_Click("INBOX", _btnInbox);
        }

        private void BtnSent_Click(object sender, EventArgs e)
        {
            BtnFolder_Click("[Gmail]/Sent Mail", _btnSent);
        }

        private void BtnTrash_Click(object sender, EventArgs e)
        {
            BtnFolder_Click("[Gmail]/Trash", _btnTrash);
        }

        private void UpdateInboxCount()
        {
            try
            {
                int count = _emailService.GetInboxCount();
                if (_btnInbox != null)
                {
                    _btnInbox.Text = $"   Inbox ({count})";
                }
            }
            catch
            {
                // Silent fail
            }
        }

        public async void RefreshEmails()
        {
            _emailCache.Clear();
            _emailDetailCache.Clear();
            await LoadEmailsAsync();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource?.Dispose();
                _emailService?.Dispose();
            }
            catch { }
            base.OnFormClosing(e);
        }
    }
}
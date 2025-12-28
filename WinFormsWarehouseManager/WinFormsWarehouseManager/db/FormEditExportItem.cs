using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using WinFormsWarehouseManager.db;
using WinFormsWarehouseManager.Models;

namespace WinFormsWarehouseManager.Forms
{
    public partial class FormEditExportItem : Form
    {
        public bool IsUpdated { get; private set; }
        public TempExportItem UpdatedItem { get; private set; }

        private TempExportItem originalItem;
        private DatabaseHelper dbHelper;
        private Timer fadeTimer;
        private double opacity = 0;
        private const int FORM_WIDTH = 520;
        private const int FORM_HEIGHT = 580;

        // Controls
        private ComboBox cbbReceiver;
        private TextBox txtName;
        private NumericUpDown numQty;
        private Label lblProductInfo;
        private Label lblTonKhoHienTai;

        public FormEditExportItem(TempExportItem item, int receiverID, DatabaseHelper helper)
        {
            InitializeComponent();
            originalItem = item;
            dbHelper = helper;

            this.Width = FORM_WIDTH;
            this.Height = FORM_HEIGHT;
            this.MinimumSize = new Size(FORM_WIDTH, FORM_HEIGHT);
            this.MaximumSize = new Size(FORM_WIDTH, FORM_HEIGHT);

            InitializeUI();
            LoadReceivers();
            LoadData();
            SetupAnimation();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(FORM_WIDTH, FORM_HEIGHT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEditExportItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chỉnh sửa sản phẩm xuất";
            this.BackColor = Color.White;
            this.Opacity = 0;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.TopMost = true;

            this.ResumeLayout(false);
        }

        private void InitializeUI()
        {
            this.Paint += FormEditExportItem_Paint;

            // Title
            Label lblTitle = new Label
            {
                Text = "CHỈNH SỬA THÔNG TIN XUẤT KHO",
                Location = new Point(30, 25),
                Size = new Size(460, 35),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 53, 69),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };
            this.Controls.Add(lblTitle);

            // Divider
            Panel divider = new Panel
            {
                Location = new Point(30, 65),
                Size = new Size(460, 2),
                BackColor = Color.FromArgb(236, 240, 241)
            };
            this.Controls.Add(divider);

            // Info label
            lblProductInfo = new Label
            {
                Location = new Point(30, 75),
                Size = new Size(460, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.FromArgb(127, 140, 141),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false
            };
            this.Controls.Add(lblProductInfo);

            int yPos = 110;
            int labelWidth = 130;
            int inputWidth = 320;
            int spacing = 70;

            // Receiver ComboBox - Custom style
            AddLabel("Người nhận:", 30, yPos, labelWidth);

            // Panel wrapper for border
            Panel cbbPanel = new Panel
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 32),
                BackColor = Color.FromArgb(220, 53, 69),
                Padding = new Padding(2)
            };

            cbbReceiver = new ComboBox
            {
                Location = new Point(2, 2),
                Size = new Size(inputWidth - 4, 28),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(64, 64, 64)
            };

            cbbPanel.Controls.Add(cbbReceiver);
            this.Controls.Add(cbbPanel);
            yPos += spacing;

            // Product Name
            AddLabel("Tên sản phẩm:", 30, yPos, labelWidth);
            txtName = new TextBox
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 30),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true,
                BackColor = Color.FromArgb(236, 240, 241)
            };
            this.Controls.Add(txtName);
            yPos += spacing;

            // Tồn kho hiện tại
            lblTonKhoHienTai = new Label
            {
                Location = new Point(30, yPos),
                Size = new Size(460, 25),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 167, 69),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false
            };
            this.Controls.Add(lblTonKhoHienTai);
            yPos += 35;

            // Quantity
            AddLabel("Số lượng xuất:", 30, yPos, labelWidth);
            numQty = new NumericUpDown
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 30),
                Font = new Font("Segoe UI", 10),
                Maximum = 999999,
                Minimum = 1,
                BorderStyle = BorderStyle.FixedSingle
            };
            numQty.ValueChanged += NumQty_ValueChanged;
            this.Controls.Add(numQty);
            yPos += 100;

            // Save button
            IconButton btnSave = new IconButton
            {
                Location = new Point(30, yPos),
                Size = new Size(225, 45),
                Text = "  Lưu thay đổi",
                IconChar = IconChar.Save,
                IconColor = Color.White,
                IconSize = 20,
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            btnSave.MouseEnter += (s, e) => btnSave.BackColor = Color.FromArgb(34, 139, 34);
            btnSave.MouseLeave += (s, e) => btnSave.BackColor = Color.FromArgb(40, 167, 69);
            this.Controls.Add(btnSave);

            // Cancel button
            IconButton btnCancel = new IconButton
            {
                Location = new Point(265, yPos),
                Size = new Size(220, 45),
                Text = "  Hủy",
                IconChar = IconChar.Times,
                IconColor = Color.White,
                IconSize = 20,
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;
            btnCancel.MouseEnter += (s, e) => btnCancel.BackColor = Color.FromArgb(108, 122, 137);
            btnCancel.MouseLeave += (s, e) => btnCancel.BackColor = Color.FromArgb(127, 140, 141);
            this.Controls.Add(btnCancel);

            // ESC to close
            this.KeyPreview = true;
            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Escape)
                {
                    BtnCancel_Click(null, null);
                }
            };
        }

        private void AddLabel(string text, int x, int y, int width)
        {
            Label lbl = new Label
            {
                Text = text,
                Location = new Point(x, y + 5),
                Size = new Size(width, 25),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(52, 73, 94),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false
            };
            this.Controls.Add(lbl);
        }

        private void LoadReceivers()
        {
            try
            {
                string query = "SELECT ReceiverID, ReceiverName FROM Receivers ORDER BY ReceiverName";
                DataTable dt = dbHelper.ExecuteQuery(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    cbbReceiver.DataSource = dt;
                    cbbReceiver.DisplayMember = "ReceiverName";
                    cbbReceiver.ValueMember = "ReceiverID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách người nhận: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            txtName.Text = originalItem.ProductName;
            numQty.Value = originalItem.Quantity;
            numQty.Maximum = originalItem.SoLuongTonKho;

            lblProductInfo.Text = $"ProductID: {originalItem.ProductID} | Danh mục: {originalItem.CategoryName}";
            lblProductInfo.ForeColor = Color.FromArgb(52, 152, 219);

            // Set selected receiver
            if (cbbReceiver.DataSource != null && originalItem.ReceiverID > 0)
            {
                DataTable dt = cbbReceiver.DataSource as DataTable;
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["ReceiverID"]) == originalItem.ReceiverID)
                        {
                            cbbReceiver.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }

            UpdateTonKhoLabel();
        }

        private void NumQty_ValueChanged(object sender, EventArgs e)
        {
            UpdateTonKhoLabel();
        }

        private void UpdateTonKhoLabel()
        {
            int remaining = originalItem.SoLuongTonKho - (int)numQty.Value;
            lblTonKhoHienTai.Text = $"Tồn kho hiện tại: {originalItem.SoLuongTonKho} {originalItem.DonViTinh} → Còn lại sau xuất: {remaining} {originalItem.DonViTinh}";

            if (remaining < 0)
            {
                lblTonKhoHienTai.ForeColor = Color.FromArgb(220, 53, 69);
            }
            else if (remaining < 5)
            {
                lblTonKhoHienTai.ForeColor = Color.FromArgb(255, 193, 7);
            }
            else
            {
                lblTonKhoHienTai.ForeColor = Color.FromArgb(40, 167, 69);
            }
        }

        private void SetupAnimation()
        {
            fadeTimer = new Timer();
            fadeTimer.Interval = 10;
            fadeTimer.Tick += FadeTimer_Tick;
            fadeTimer.Start();
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            opacity += 0.05;
            if (opacity >= 1)
            {
                opacity = 1;
                fadeTimer.Stop();
            }
            this.Opacity = opacity;
        }

        private void FormEditExportItem_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = GetRoundedRectPath(this.ClientRectangle, 15))
            {
                this.Region = new Region(path);

                using (Pen shadowPen = new Pen(Color.FromArgb(50, 0, 0, 0), 3))
                {
                    Rectangle shadowRect = this.ClientRectangle;
                    shadowRect.Inflate(-2, -2);
                    using (GraphicsPath shadowPath = GetRoundedRectPath(shadowRect, 15))
                    {
                        e.Graphics.DrawPath(shadowPen, shadowPath);
                    }
                }
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            rect.Width -= 1;
            rect.Height -= 1;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        private bool ValidateInput()
        {
            if (cbbReceiver.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn người nhận!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbReceiver.Focus();
                return false;
            }

            if (numQty.Value <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numQty.Focus();
                return false;
            }

            if (numQty.Value > originalItem.SoLuongTonKho)
            {
                MessageBox.Show($"Số lượng xuất ({numQty.Value}) vượt quá tồn kho ({originalItem.SoLuongTonKho})!",
                    "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numQty.Focus();
                return false;
            }

            return true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                DataRowView drv = (DataRowView)cbbReceiver.SelectedItem;
                int selectedReceiverID = Convert.ToInt32(drv["ReceiverID"]);
                string selectedReceiverName = drv["ReceiverName"].ToString();

                UpdatedItem = new TempExportItem(
                    originalItem.ProductID,
                    originalItem.ProductName,
                    originalItem.CategoryID,
                    originalItem.CategoryName,
                    (int)numQty.Value,
                    originalItem.DonViTinh,
                    originalItem.SoLuongTonKho,
                    selectedReceiverID,
                    selectedReceiverName
                );

                IsUpdated = true;
                MessageBox.Show("Đã cập nhật thông tin sản phẩm xuất!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            IsUpdated = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
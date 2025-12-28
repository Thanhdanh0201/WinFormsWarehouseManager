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
    public partial class FormEditImportItem : Form
    {
        public bool IsUpdated { get; private set; }
        public TempImportItem UpdatedItem { get; private set; }

        private TempImportItem originalItem;
        private int supplierID;
        private DatabaseHelper dbHelper;
        private Timer fadeTimer;
        private double opacity = 0;
        private const int FORM_WIDTH = 520;
        private const int FORM_HEIGHT = 580;

        // Controls
        private TextBox txtName;
        private NumericUpDown numQty;
        private TextBox txtUnit;
        private DateTimePicker dtpExpiry;
        private ComboBox cboCategory;
        private Label lblProductInfo;

        public FormEditImportItem(TempImportItem item, int supplierID, DatabaseHelper helper)
        {
            InitializeComponent();
            originalItem = item;
            this.supplierID = supplierID;
            dbHelper = helper;

            this.Width = FORM_WIDTH;
            this.Height = FORM_HEIGHT;
            this.MinimumSize = new Size(FORM_WIDTH, FORM_HEIGHT);
            this.MaximumSize = new Size(FORM_WIDTH, FORM_HEIGHT);

            InitializeUI();
            LoadData();
            SetupAnimation();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(FORM_WIDTH, FORM_HEIGHT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEditImportItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chỉnh sửa sản phẩm";
            this.BackColor = Color.White;
            this.Opacity = 0;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.TopMost = true;

            this.ResumeLayout(false);
        }

        private void InitializeUI()
        {
            this.Paint += FormEditImportItem_Paint;

            // Title
            Label lblTitle = new Label
            {
                Text = "CHỈNH SỬA THÔNG TIN SẢN PHẨM",
                Location = new Point(30, 25),
                Size = new Size(460, 35),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
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

            // Info label (hiện ProductID nếu có)
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
            int spacing = 60;

            // Product Name
            AddLabel("Tên sản phẩm:", 30, yPos, labelWidth);
            txtName = new TextBox
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 30),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true, // Không cho sửa tên
                BackColor = Color.FromArgb(236, 240, 241)
            };
            this.Controls.Add(txtName);
            yPos += spacing;

            // Category
            AddLabel("Danh mục:", 30, yPos, labelWidth);
            cboCategory = new ComboBox
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 30),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            this.Controls.Add(cboCategory);
            yPos += spacing;

            // Quantity
            AddLabel("Số lượng:", 30, yPos, labelWidth);
            numQty = new NumericUpDown
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 30),
                Font = new Font("Segoe UI", 10),
                Maximum = 999999,
                Minimum = 1,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(numQty);
            yPos += spacing;

            // Unit
            AddLabel("Đơn vị tính:", 30, yPos, labelWidth);
            txtUnit = new TextBox
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 30),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txtUnit);
            yPos += spacing;

            // Expiry Date
            AddLabel("Hạn sử dụng:", 30, yPos, labelWidth);
            dtpExpiry = new DateTimePicker
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 30),
                Font = new Font("Segoe UI", 10),
                Format = DateTimePickerFormat.Short
            };
            this.Controls.Add(dtpExpiry);
            yPos += 80;

            // Save button
            IconButton btnSave = new IconButton
            {
                Location = new Point(30, yPos),
                Size = new Size(225, 45),
                Text = "  Lưu thay đổi",
                IconChar = IconChar.Save,
                IconColor = Color.White,
                IconSize = 20,
                BackColor = Color.FromArgb(39, 174, 96),
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
            btnSave.MouseEnter += (s, e) => btnSave.BackColor = Color.FromArgb(34, 153, 84);
            btnSave.MouseLeave += (s, e) => btnSave.BackColor = Color.FromArgb(39, 174, 96);
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

        private void LoadData()
        {
            // Load product data
            txtName.Text = originalItem.ProductName;
            numQty.Value = originalItem.Quantity;
            txtUnit.Text = originalItem.DonViTinh;

            if (DateTime.TryParse(originalItem.HanSuDung, out DateTime expiry))
                dtpExpiry.Value = expiry;

            // Hiển thị info
            if (originalItem.IsNewProduct)
            {
                lblProductInfo.Text = "⚠ Sản phẩm mới (chưa có trong kho)";
                lblProductInfo.ForeColor = Color.FromArgb(230, 126, 34);
            }
            else
            {
                lblProductInfo.Text = $"📦 ProductID: {originalItem.ProductID} | Sản phẩm đã tồn tại trong kho";
                lblProductInfo.ForeColor = Color.FromArgb(52, 152, 219);
            }

            // Load categories
            LoadCategoriesComboBox();
        }

        private void LoadCategoriesComboBox()
        {
            string query = "SELECT CategoryID, CategoryName FROM Categories ORDER BY CategoryName";
            DataTable dt = dbHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                cboCategory.DataSource = dt;
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryID";

                // Select current category
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["CategoryID"]) == originalItem.CategoryID)
                    {
                        cboCategory.SelectedIndex = i;
                        break;
                    }
                }
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

        private void FormEditImportItem_Paint(object sender, PaintEventArgs e)
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
            if (numQty.Value <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numQty.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUnit.Text))
            {
                MessageBox.Show("Đơn vị tính không được để trống!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnit.Focus();
                return false;
            }

            if (cboCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn danh mục!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCategory.Focus();
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
                DataRowView drv = (DataRowView)cboCategory.SelectedItem;
                int categoryID = Convert.ToInt32(drv["CategoryID"]);
                string categoryName = drv["CategoryName"].ToString();

                // Tạo item mới với thông tin đã chỉnh sửa
                UpdatedItem = new TempImportItem(
                    originalItem.ProductID,
                    originalItem.ProductName, // Không cho sửa tên
                    categoryID,
                    categoryName,
                    (int)numQty.Value,
                    txtUnit.Text.Trim(),
                    dtpExpiry.Value.ToString("yyyy-MM-dd"),
                    originalItem.IsNewProduct,
                    originalItem.SupplierID,      // THÊM - giữ nguyên supplier cũ
                    originalItem.SupplierName     // THÊM - giữ nguyên supplier cũ
                );

                IsUpdated = true;
                MessageBox.Show("Đã cập nhật thông tin sản phẩm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            this.Close();
        }
    }
}
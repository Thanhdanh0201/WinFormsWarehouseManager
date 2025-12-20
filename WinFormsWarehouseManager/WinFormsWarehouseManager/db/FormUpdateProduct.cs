using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using WinFormsWarehouseManager.db;
using WinFormsWarehouseManager.Models;

namespace WinFormsWarehouseManager.Forms
{
    public partial class FormUpdateProduct : Form
    {
        public bool IsUpdated { get; private set; }
        private ProductInfo product;
        private DatabaseHelper dbHelper;
        private Timer fadeTimer;
        private double opacity = 0;
        private const int FORM_WIDTH = 520;
        private const int FORM_HEIGHT = 620;

        // Controls
        private TextBox txtName;
        private NumericUpDown numQty;
        private TextBox txtUnit;
        private DateTimePicker dtpExpiry;
        private TextBox txtImport;
        private ComboBox cboCategory;
        private ComboBox cboSupplier;

        public FormUpdateProduct(ProductInfo productInfo, DatabaseHelper helper)
        {
            InitializeComponent();
            product = productInfo;
            dbHelper = helper;

            // Force exact size BEFORE initializing UI
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
            this.Name = "FormUpdateProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cập nhật sản phẩm";
            this.BackColor = Color.White;
            this.Opacity = 0;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.TopMost = true;

            this.ResumeLayout(false);
        }

        private void InitializeUI()
        {
            this.Paint += FormUpdateProduct_Paint;

            // Title
            Label lblTitle = new Label
            {
                Text = "CẬP NHẬT THÔNG TIN SẢN PHẨM",
                Location = new Point(30, 25),
                Size = new Size(460, 35),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
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

            int yPos = 85;
            int labelWidth = 130;
            int inputWidth = 320;
            int spacing = 60;

            // Product Name
            AddLabel("Tên sản phẩm:", 30, yPos, labelWidth);
            txtName = new TextBox
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 25),
                Font = new Font("Segoe UI", 13),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txtName);
            yPos += spacing;

            // Quantity
            AddLabel("Số lượng:", 30, yPos, labelWidth);
            numQty = new NumericUpDown
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 25),
                Font = new Font("Segoe UI", 13),
                Maximum = 999999,
                Minimum = 0,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(numQty);
            yPos += spacing;

            // Unit
            AddLabel("Đơn vị tính:", 30, yPos, labelWidth);
            txtUnit = new TextBox
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 25),
                Font = new Font("Segoe UI", 13),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txtUnit);
            yPos += spacing;

            // Expiry Date
            AddLabel("Hạn sử dụng:", 30, yPos, labelWidth);
            dtpExpiry = new DateTimePicker
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 25),
                Font = new Font("Segoe UI", 13),
                Format = DateTimePickerFormat.Short
            };
            this.Controls.Add(dtpExpiry);
            yPos += spacing;

            // Import Date (readonly)
            AddLabel("Ngày nhập kho:", 30, yPos, labelWidth);
            txtImport = new TextBox
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 25),
                Font = new Font("Segoe UI", 13),
                ReadOnly = true,
                BackColor = Color.FromArgb(236, 240, 241),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txtImport);
            yPos += spacing;

            // Category
            AddLabel("Danh mục:", 30, yPos, labelWidth);
            cboCategory = new ComboBox
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 25),
                Font = new Font("Segoe UI", 13),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            this.Controls.Add(cboCategory);
            yPos += spacing;

            // Supplier
            AddLabel("Nhà cung cấp:", 30, yPos, labelWidth);
            cboSupplier = new ComboBox
            {
                Location = new Point(165, yPos),
                Size = new Size(inputWidth, 25),
                Font = new Font("Segoe UI", 13),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            this.Controls.Add(cboSupplier);
            yPos += 75;

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
                Location = new Point(x, y + 3), // Align with textbox
                Size = new Size(width, 25),
                Font = new Font("Segoe UI", 7, FontStyle.Regular),
                ForeColor = Color.FromArgb(52, 73, 94),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false
            };
            this.Controls.Add(lbl);
        }

        private void LoadData()
        {
            // Load product data
            txtName.Text = product.ProductName;
            numQty.Value = product.SoLuong;
            txtUnit.Text = product.DonViTinh;
            txtImport.Text = product.NgayNhapKho;

            if (DateTime.TryParse(product.HanSuDung, out DateTime expiry))
                dtpExpiry.Value = expiry;

            // Load categories
            LoadCategoriesComboBox();

            // Load suppliers
            LoadSuppliersComboBox();
        }

        private void LoadCategoriesComboBox()
        {
            DataTable dt = dbHelper.ExecuteQuery("SELECT CategoryName FROM Categories ORDER BY CategoryName");
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cboCategory.Items.Add(row["CategoryName"].ToString());
                }

                if (!string.IsNullOrEmpty(product.CategoryName))
                {
                    cboCategory.SelectedItem = product.CategoryName;
                }
            }
        }

        private void LoadSuppliersComboBox()
        {
            DataTable dt = dbHelper.ExecuteQuery("SELECT SupplierName FROM Suppliers ORDER BY SupplierName");
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cboSupplier.Items.Add(row["SupplierName"].ToString());
                }

                if (!string.IsNullOrEmpty(product.SupplierName))
                {
                    cboSupplier.SelectedItem = product.SupplierName;
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

        private void FormUpdateProduct_Paint(object sender, PaintEventArgs e)
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
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Tên sản phẩm không được để trống!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

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

            if (cboSupplier.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSupplier.Focus();
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
                // Get CategoryID
                string queryCat = "SELECT CategoryID FROM Categories WHERE CategoryName = @CategoryName";
                object catResult = dbHelper.ExecuteScalar(queryCat, new System.Data.SQLite.SQLiteParameter[] {
                    new System.Data.SQLite.SQLiteParameter("@CategoryName", cboCategory.SelectedItem.ToString())
                });
                int categoryID = catResult != null ? Convert.ToInt32(catResult) : 0;

                // Get SupplierID
                string querySup = "SELECT SupplierID FROM Suppliers WHERE SupplierName = @SupplierName";
                object supResult = dbHelper.ExecuteScalar(querySup, new System.Data.SQLite.SQLiteParameter[] {
                    new System.Data.SQLite.SQLiteParameter("@SupplierName", cboSupplier.SelectedItem.ToString())
                });
                int supplierID = supResult != null ? Convert.ToInt32(supResult) : 0;

                // Update product
                string queryUpdate = @"
                    UPDATE Products 
                    SET ProductName = @ProductName, 
                        SoLuong = @SoLuong, 
                        DonViTinh = @DonViTinh,
                        HanSuDung = @HanSuDung,
                        CategoryID = @CategoryID,
                        SupplierID = @SupplierID
                    WHERE ProductID = @ProductID";

                int result = dbHelper.ExecuteNonQuery(queryUpdate, new System.Data.SQLite.SQLiteParameter[] {
                    new System.Data.SQLite.SQLiteParameter("@ProductName", txtName.Text.Trim()),
                    new System.Data.SQLite.SQLiteParameter("@SoLuong", (int)numQty.Value),
                    new System.Data.SQLite.SQLiteParameter("@DonViTinh", txtUnit.Text.Trim()),
                    new System.Data.SQLite.SQLiteParameter("@HanSuDung", dtpExpiry.Value.ToString("yyyy-MM-dd")),
                    new System.Data.SQLite.SQLiteParameter("@CategoryID", categoryID),
                    new System.Data.SQLite.SQLiteParameter("@SupplierID", supplierID),
                    new System.Data.SQLite.SQLiteParameter("@ProductID", product.ProductID)
                });

                if (result > 0)
                {
                    IsUpdated = true;
                    MessageBox.Show("Đã cập nhật sản phẩm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
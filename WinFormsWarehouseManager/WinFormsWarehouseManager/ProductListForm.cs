using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using WinFormsWarehouseManager.db;
using WinFormsWarehouseManager.Models;

namespace WinFormsWarehouseManager.Forms
{
    public partial class FormProductList : Form
    {
        private DatabaseHelper dbHelper;
        private List<ProductInfo> allProducts;
        private List<ProductInfo> filteredProducts;
        private List<Panel> productCards;
        private Dictionary<int, bool> selectedProducts; // ProductID -> IsSelected

        private const int CARDS_PER_PAGE = 10;
        private int currentLoadedCount = 0;

        // Category colors - Query from database dynamically
        private Dictionary<string, Color> categoryColors;

        public FormProductList()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            allProducts = new List<ProductInfo>();
            filteredProducts = new List<ProductInfo>();
            productCards = new List<Panel>();
            selectedProducts = new Dictionary<int, bool>();

            InitializeCategoryColors();
            InitializeUI();
            LoadCategoriesToComboBox();
            LoadProducts();
        }

        private void InitializeCategoryColors()
        {
            categoryColors = new Dictionary<string, Color>()
            {
                { "Thực phẩm", Color.FromArgb(46, 204, 113) },          // Green
                { "Linh kiện điện tử", Color.FromArgb(230, 126, 34) },  // Orange
                { "Đồ gia dụng", Color.FromArgb(41, 128, 185) },        // Blue
                { "Mỹ phẩm", Color.FromArgb(231, 76, 60) },             // Red
                { "Vật liệu xây dựng", Color.FromArgb(149, 165, 166) }, // Gray
                { "Đồ dùng văn phòng", Color.FromArgb(142, 68, 173) }   // Purple
            };
        }

        private void InitializeUI()
        {
            this.BackColor = Color.FromArgb(2, 51, 66);

            // Setup FlowLayoutPanel scroll event for lazy loading
            flowLayoutPanelProducts.Scroll += FlowLayoutPanelProducts_Scroll;

            // Initialize ComboBox Sort
            cboSort.Items.Clear();
            cboSort.Items.Add("Mới nhất");
            cboSort.Items.Add("Cũ nhất");
            cboSort.SelectedIndex = 0;
        }

        private void LoadCategoriesToComboBox()
        {
            cboCategory.Items.Clear();
            cboCategory.Items.Add("Tất cả");

            string query = "SELECT DISTINCT CategoryName FROM Categories ORDER BY CategoryName";
            DataTable dt = dbHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cboCategory.Items.Add(row["CategoryName"].ToString());
                }
            }

            cboCategory.SelectedIndex = 0;
        }

        private void LoadProducts()
        {
            string query = @"
                SELECT 
                    p.ProductID, p.ProductName, p.SoLuong, p.DonViTinh, 
                    p.HanSuDung, p.NgayNhapKho,
                    p.CategoryID, c.CategoryName,
                    p.SupplierID, s.SupplierName
                FROM Products p
                LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID
                ORDER BY p.NgayNhapKho DESC";

            DataTable dt = dbHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                allProducts.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    var product = new ProductInfo
                    {
                        ProductID = Convert.ToInt32(row["ProductID"]),
                        ProductName = row["ProductName"].ToString(),
                        SoLuong = Convert.ToInt32(row["SoLuong"]),
                        DonViTinh = row["DonViTinh"].ToString(),
                        HanSuDung = row["HanSuDung"].ToString(),
                        NgayNhapKho = row["NgayNhapKho"].ToString(),
                        CategoryID = row["CategoryID"] != DBNull.Value ? Convert.ToInt32(row["CategoryID"]) : 0,
                        CategoryName = row["CategoryName"]?.ToString() ?? "Khác",
                        SupplierID = row["SupplierID"] != DBNull.Value ? Convert.ToInt32(row["SupplierID"]) : 0,
                        SupplierName = row["SupplierName"]?.ToString() ?? "N/A"
                    };
                    allProducts.Add(product);
                }

                filteredProducts = new List<ProductInfo>(allProducts);
                RefreshProductCards();
            }
        }

        private void RefreshProductCards()
        {
            flowLayoutPanelProducts.Controls.Clear();
            productCards.Clear();
            currentLoadedCount = 0;

            LoadMoreCards();
        }

        private void LoadMoreCards()
        {
            int toLoad = Math.Min(CARDS_PER_PAGE, filteredProducts.Count - currentLoadedCount);

            for (int i = 0; i < toLoad; i++)
            {
                var product = filteredProducts[currentLoadedCount + i];
                Panel card = CreateProductCard(product);
                productCards.Add(card);
                flowLayoutPanelProducts.Controls.Add(card);
            }

            currentLoadedCount += toLoad;
        }

        private Panel CreateProductCard(ProductInfo product)
        {
            // Get category color
            Color categoryColor = categoryColors.ContainsKey(product.CategoryName)
                ? categoryColors[product.CategoryName]
                : Color.FromArgb(127, 140, 141); // Default gray

            // Main card panel
            Panel card = new Panel
            {
                Width = 320,
                Height = 200,
                BackColor = Color.White,
                Margin = new Padding(10),
                Tag = product
            };

            // Category color strip (left side)
            Panel colorStrip = new Panel
            {
                Width = 8,
                Height = 200,
                BackColor = categoryColor,
                Location = new Point(0, 0)
            };
            card.Controls.Add(colorStrip);

            // Checkbox
            CheckBox chkSelect = new CheckBox
            {
                Location = new Point(20, 10),
                Width = 20,
                Height = 20,
                Tag = product.ProductID,
                Checked = selectedProducts.ContainsKey(product.ProductID)
            };
            chkSelect.CheckedChanged += ChkSelect_CheckedChanged;
            card.Controls.Add(chkSelect);

            // Product name
            Label lblName = new Label
            {
                Text = product.ProductName,
                Location = new Point(50, 10),
                Width = 250,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(2, 51, 66)
            };
            card.Controls.Add(lblName);

            // Quantity
            Label lblQuantity = new Label
            {
                Text = $"Số lượng: {product.SoLuong} {product.DonViTinh}",
                Location = new Point(50, 40),
                Width = 250,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(52, 73, 94)
            };
            card.Controls.Add(lblQuantity);

            // Expiry date
            Label lblExpiry = new Label
            {
                Text = $"HSD: {product.HanSuDung}",
                Location = new Point(50, 65),
                Width = 250,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(52, 73, 94)
            };
            card.Controls.Add(lblExpiry);

            // Import date
            Label lblImportDate = new Label
            {
                Text = $"Ngày nhập: {product.NgayNhapKho}",
                Location = new Point(50, 90),
                Width = 250,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(52, 73, 94)
            };
            card.Controls.Add(lblImportDate);

            // Category badge
            Panel categoryBadge = new Panel
            {
                Location = new Point(50, 120),
                Width = 130,
                Height = 25,
                BackColor = categoryColor
            };
            Label lblCategory = new Label
            {
                Text = product.CategoryName,
                Location = new Point(5, 4),
                Width = 120,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            categoryBadge.Controls.Add(lblCategory);
            card.Controls.Add(categoryBadge);

            // Supplier
            Label lblSupplier = new Label
            {
                Text = $"NCC: {product.SupplierName}",
                Location = new Point(50, 155),
                Width = 250,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(127, 140, 141)
            };
            card.Controls.Add(lblSupplier);

            return card;
        }

        private void ChkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            int productID = (int)chk.Tag;

            if (chk.Checked)
            {
                if (!selectedProducts.ContainsKey(productID))
                    selectedProducts.Add(productID, true);
            }
            else
            {
                if (selectedProducts.ContainsKey(productID))
                    selectedProducts.Remove(productID);
            }

            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            int selectedCount = selectedProducts.Count;

            // Hide "Nhập kho thêm" button if selection is not exactly 1
            btnNhapKho.Visible = (selectedCount == 1);

            // Enable/disable buttons based on selection
            btnXuatKho.Enabled = (selectedCount > 0);
            btnDelete.Enabled = (selectedCount > 0);
            btnCapNhat.Enabled = (selectedCount == 1);
        }

        private void FlowLayoutPanelProducts_Scroll(object sender, ScrollEventArgs e)
        {
            // Lazy loading when scrolling near bottom
            if (e.NewValue >= flowLayoutPanelProducts.VerticalScroll.Maximum - 100)
            {
                if (currentLoadedCount < filteredProducts.Count)
                {
                    LoadMoreCards();
                }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ApplyFilters();
                e.Handled = true;
            }
        }

        private void CboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void CboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            filteredProducts = new List<ProductInfo>(allProducts);

            // Filter by search text
            string searchText = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredProducts = filteredProducts
                    .Where(p => p.ProductName.ToLower().Contains(searchText))
                    .ToList();
            }

            // Filter by category
            if (cboCategory.SelectedIndex > 0) // Index 0 is "Tất cả"
            {
                string selectedCategory = cboCategory.SelectedItem.ToString();
                filteredProducts = filteredProducts
                    .Where(p => p.CategoryName == selectedCategory)
                    .ToList();
            }

            // Sort
            if (cboSort.SelectedIndex == 0) // Mới nhất
            {
                filteredProducts = filteredProducts
                    .OrderByDescending(p => p.NgayNhapKho)
                    .ToList();
            }
            else if (cboSort.SelectedIndex == 1) // Cũ nhất
            {
                filteredProducts = filteredProducts
                    .OrderBy(p => p.NgayNhapKho)
                    .ToList();
            }

            RefreshProductCards();
        }

        private void ChkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkSelectAll.Checked;

            selectedProducts.Clear();

            foreach (Panel card in productCards)
            {
                CheckBox chk = card.Controls.OfType<CheckBox>().FirstOrDefault();
                if (chk != null)
                {
                    chk.CheckedChanged -= ChkSelect_CheckedChanged;
                    chk.Checked = isChecked;
                    chk.CheckedChanged += ChkSelect_CheckedChanged;

                    if (isChecked)
                    {
                        int productID = (int)chk.Tag;
                        selectedProducts[productID] = true;
                    }
                }
            }

            UpdateButtonStates();
        }

        private void BtnNhapKho_Click(object sender, EventArgs e)
        {
            if (selectedProducts.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn 1 sản phẩm để nhập kho!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int productID = selectedProducts.Keys.First();
            ProductInfo product = allProducts.FirstOrDefault(p => p.ProductID == productID);

            if (product != null)
            {
                /*
                // Open FormNhapKho and pass product
                FormNhapKho formNhapKho = new FormNhapKho(product);
                formNhapKho.ShowDialog();
                LoadProducts(); // Refresh after import
                */
            }
        }

        private void BtnXuatKho_Click(object sender, EventArgs e)
        {
            if (selectedProducts.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xuất kho!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get selected products
            List<ProductInfo> selectedProductsList = allProducts
                .Where(p => selectedProducts.ContainsKey(p.ProductID))
                .ToList();
            /*
            // Open FormXuatKho and pass List<ProductInfo>
            FormXuatKho formXuatKho = new FormXuatKho(selectedProductsList);
            formXuatKho.ShowDialog();
            LoadProducts(); // Refresh after export
            */
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProducts.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ShowDeleteConfirmationOverlay();
        }

        private void ShowDeleteConfirmationOverlay()
        {
            // Create overlay panel
            Panel overlayPanel = new Panel
            {
                Size = this.ClientSize,
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(180, 0, 0, 0), // Semi-transparent black
                Name = "overlayPanel"
            };

            // Create confirmation dialog
            Panel dialogPanel = new Panel
            {
                Size = new Size(500, 400),
                BackColor = Color.White,
                Location = new Point((this.Width - 500) / 2, (this.Height - 400) / 2)
            };

            // Title
            Label lblTitle = new Label
            {
                Text = "XÁC NHẬN XÓA SẢN PHẨM",
                Location = new Point(20, 20),
                Width = 460,
                Height = 30,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(2, 51, 66),
                TextAlign = ContentAlignment.MiddleCenter
            };
            dialogPanel.Controls.Add(lblTitle);

            // Product list
            ListBox lstProducts = new ListBox
            {
                Location = new Point(20, 60),
                Size = new Size(460, 250),
                Font = new Font("Segoe UI", 10)
            };

            foreach (int productID in selectedProducts.Keys)
            {
                ProductInfo product = allProducts.FirstOrDefault(p => p.ProductID == productID);
                if (product != null)
                {
                    lstProducts.Items.Add($"- {product.ProductName} (SL: {product.SoLuong} {product.DonViTinh})");
                }
            }
            dialogPanel.Controls.Add(lstProducts);

            // Confirm button
            IconButton btnConfirm = new IconButton
            {
                Location = new Point(20, 330),
                Size = new Size(220, 45),
                Text = "  Xác nhận xóa",
                IconChar = IconChar.Check,
                IconColor = Color.White,
                IconSize = 24,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.Click += (s, e) => {
                ConfirmDelete();
                this.Controls.Remove(overlayPanel);
            };
            dialogPanel.Controls.Add(btnConfirm);

            // Cancel button
            IconButton btnCancel = new IconButton
            {
                Location = new Point(260, 330),
                Size = new Size(220, 45),
                Text = "  Hủy",
                IconChar = IconChar.Times,
                IconColor = Color.White,
                IconSize = 24,
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Controls.Remove(overlayPanel);
            dialogPanel.Controls.Add(btnCancel);

            overlayPanel.Controls.Add(dialogPanel);
            this.Controls.Add(overlayPanel);
            overlayPanel.BringToFront();
        }

        private void ConfirmDelete()
        {
            foreach (int productID in selectedProducts.Keys)
            {
                string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                dbHelper.ExecuteNonQuery(query, new System.Data.SQLite.SQLiteParameter[] {
                    new System.Data.SQLite.SQLiteParameter("@ProductID", productID)
                });
            }

            selectedProducts.Clear();
            chkSelectAll.Checked = false;
            MessageBox.Show("Đã xóa sản phẩm thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadProducts();
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            if (selectedProducts.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn 1 sản phẩm để cập nhật!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int productID = selectedProducts.Keys.First();
            ProductInfo product = allProducts.FirstOrDefault(p => p.ProductID == productID);

            if (product != null)
            {
                ShowUpdateOverlay(product);
            }
        }

        private void ShowUpdateOverlay(ProductInfo product)
        {
            // Create overlay panel
            Panel overlayPanel = new Panel
            {
                Size = this.ClientSize,
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(180, 0, 0, 0),
                Name = "overlayPanelUpdate"
            };

            // Create update dialog
            Panel dialogPanel = new Panel
            {
                Size = new Size(550, 600),
                BackColor = Color.White,
                Location = new Point((this.Width - 550) / 2, (this.Height - 600) / 2),
                AutoScroll = true
            };

            // Title
            Label lblTitle = new Label
            {
                Text = "CẬP NHẬT THÔNG TIN SẢN PHẨM",
                Location = new Point(20, 20),
                Width = 510,
                Height = 30,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(2, 51, 66),
                TextAlign = ContentAlignment.MiddleCenter
            };
            dialogPanel.Controls.Add(lblTitle);

            int yPos = 70;
            int labelWidth = 120;
            int inputWidth = 360;
            int spacing = 50;

            // Product Name
            Label lblName = new Label { Text = "Tên sản phẩm:", Location = new Point(30, yPos), Width = labelWidth, Font = new Font("Segoe UI", 10) };
            TextBox txtName = new TextBox { Location = new Point(160, yPos), Width = inputWidth, Font = new Font("Segoe UI", 10), Text = product.ProductName };
            dialogPanel.Controls.Add(lblName);
            dialogPanel.Controls.Add(txtName);
            yPos += spacing;

            // Quantity
            Label lblQty = new Label { Text = "Số lượng:", Location = new Point(30, yPos), Width = labelWidth, Font = new Font("Segoe UI", 10) };
            NumericUpDown numQty = new NumericUpDown { Location = new Point(160, yPos), Width = inputWidth, Font = new Font("Segoe UI", 10), Value = product.SoLuong, Maximum = 999999 };
            dialogPanel.Controls.Add(lblQty);
            dialogPanel.Controls.Add(numQty);
            yPos += spacing;

            // Unit
            Label lblUnit = new Label { Text = "Đơn vị tính:", Location = new Point(30, yPos), Width = labelWidth, Font = new Font("Segoe UI", 10) };
            TextBox txtUnit = new TextBox { Location = new Point(160, yPos), Width = inputWidth, Font = new Font("Segoe UI", 10), Text = product.DonViTinh };
            dialogPanel.Controls.Add(lblUnit);
            dialogPanel.Controls.Add(txtUnit);
            yPos += spacing;

            // Expiry Date
            Label lblExpiry = new Label { Text = "Hạn sử dụng:", Location = new Point(30, yPos), Width = labelWidth, Font = new Font("Segoe UI", 10) };
            DateTimePicker dtpExpiry = new DateTimePicker { Location = new Point(160, yPos), Width = inputWidth, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            if (DateTime.TryParse(product.HanSuDung, out DateTime expiry))
                dtpExpiry.Value = expiry;
            dialogPanel.Controls.Add(lblExpiry);
            dialogPanel.Controls.Add(dtpExpiry);
            yPos += spacing;

            // Import Date (readonly)
            Label lblImport = new Label { Text = "Ngày nhập kho:", Location = new Point(30, yPos), Width = labelWidth, Font = new Font("Segoe UI", 10) };
            TextBox txtImport = new TextBox { Location = new Point(160, yPos), Width = inputWidth, Font = new Font("Segoe UI", 10), Text = product.NgayNhapKho, ReadOnly = true, BackColor = Color.LightGray };
            dialogPanel.Controls.Add(lblImport);
            dialogPanel.Controls.Add(txtImport);
            yPos += spacing;

            // Category
            Label lblCategory = new Label { Text = "Danh mục:", Location = new Point(30, yPos), Width = labelWidth, Font = new Font("Segoe UI", 10) };
            ComboBox cboCategory = new ComboBox { Location = new Point(160, yPos), Width = inputWidth, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            LoadCategoriesComboBox(cboCategory, product.CategoryName);
            dialogPanel.Controls.Add(lblCategory);
            dialogPanel.Controls.Add(cboCategory);
            yPos += spacing;

            // Supplier
            Label lblSupplier = new Label { Text = "Nhà cung cấp:", Location = new Point(30, yPos), Width = labelWidth, Font = new Font("Segoe UI", 10) };
            ComboBox cboSupplier = new ComboBox { Location = new Point(160, yPos), Width = inputWidth, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            LoadSuppliersComboBox(cboSupplier, product.SupplierName);
            dialogPanel.Controls.Add(lblSupplier);
            dialogPanel.Controls.Add(cboSupplier);
            yPos += 70;

            // Save button
            IconButton btnSave = new IconButton
            {
                Location = new Point(30, yPos),
                Size = new Size(240, 45),
                Text = "  Lưu thay đổi",
                IconChar = IconChar.Save,
                IconColor = Color.White,
                IconSize = 24,
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += (s, e) => {
                SaveProductUpdate(product.ProductID, txtName.Text, (int)numQty.Value, txtUnit.Text,
                    dtpExpiry.Value.ToString("yyyy-MM-dd"), cboCategory.SelectedItem.ToString(),
                    cboSupplier.SelectedItem.ToString());
                this.Controls.Remove(overlayPanel);
            };
            dialogPanel.Controls.Add(btnSave);

            // Cancel button
            IconButton btnCancel = new IconButton
            {
                Location = new Point(290, yPos),
                Size = new Size(240, 45),
                Text = "  Hủy",
                IconChar = IconChar.Times,
                IconColor = Color.White,
                IconSize = 24,
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Controls.Remove(overlayPanel);
            dialogPanel.Controls.Add(btnCancel);

            overlayPanel.Controls.Add(dialogPanel);
            this.Controls.Add(overlayPanel);
            overlayPanel.BringToFront();
        }

        private void LoadCategoriesComboBox(ComboBox cbo, string selectedCategory)
        {
            DataTable dt = dbHelper.ExecuteQuery("SELECT CategoryName FROM Categories ORDER BY CategoryName");
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cbo.Items.Add(row["CategoryName"].ToString());
                }

                if (!string.IsNullOrEmpty(selectedCategory))
                {
                    cbo.SelectedItem = selectedCategory;
                }
            }
        }

        private void LoadSuppliersComboBox(ComboBox cbo, string selectedSupplier)
        {
            DataTable dt = dbHelper.ExecuteQuery("SELECT SupplierName FROM Suppliers ORDER BY SupplierName");
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cbo.Items.Add(row["SupplierName"].ToString());
                }

                if (!string.IsNullOrEmpty(selectedSupplier))
                {
                    cbo.SelectedItem = selectedSupplier;
                }
            }
        }

        private void SaveProductUpdate(int productID, string name, int quantity, string unit,
            string expiryDate, string categoryName, string supplierName)
        {
            // Get CategoryID from CategoryName
            string queryCat = "SELECT CategoryID FROM Categories WHERE CategoryName = @CategoryName";
            object catResult = dbHelper.ExecuteScalar(queryCat, new System.Data.SQLite.SQLiteParameter[] {
                new System.Data.SQLite.SQLiteParameter("@CategoryName", categoryName)
            });
            int categoryID = catResult != null ? Convert.ToInt32(catResult) : 0;

            // Get SupplierID from SupplierName
            string querySup = "SELECT SupplierID FROM Suppliers WHERE SupplierName = @SupplierName";
            object supResult = dbHelper.ExecuteScalar(querySup, new System.Data.SQLite.SQLiteParameter[] {
                new System.Data.SQLite.SQLiteParameter("@SupplierName", supplierName)
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
                new System.Data.SQLite.SQLiteParameter("@ProductName", name),
                new System.Data.SQLite.SQLiteParameter("@SoLuong", quantity),
                new System.Data.SQLite.SQLiteParameter("@DonViTinh", unit),
                new System.Data.SQLite.SQLiteParameter("@HanSuDung", expiryDate),
                new System.Data.SQLite.SQLiteParameter("@CategoryID", categoryID),
                new System.Data.SQLite.SQLiteParameter("@SupplierID", supplierID),
                new System.Data.SQLite.SQLiteParameter("@ProductID", productID)
            });

            if (result > 0)
            {
                MessageBox.Show("Đã cập nhật sản phẩm thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
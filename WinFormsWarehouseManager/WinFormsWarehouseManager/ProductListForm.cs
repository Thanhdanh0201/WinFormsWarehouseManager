using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private Dictionary<int, bool> selectedProducts;

        private const int CARDS_PER_PAGE = 75;
        private int currentLoadedCount = 0;

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
                { "Thực phẩm", Color.FromArgb(46, 204, 113) },
                { "Linh kiện điện tử", Color.FromArgb(230, 126, 34) },
                { "Đồ gia dụng", Color.FromArgb(41, 128, 185) },
                { "Mỹ phẩm", Color.FromArgb(231, 76, 60) },
                { "Vật liệu xây dựng", Color.FromArgb(149, 165, 166) },
                { "Đồ dùng văn phòng", Color.FromArgb(142, 68, 173) }
            };
        }

        private void InitializeUI()
        {
            this.BackColor = Color.FromArgb(236, 240, 241);

            panelFilter.BackColor = Color.White;
            panelButtons.BackColor = Color.White;
            flowLayoutPanelProducts.BackColor = Color.FromArgb(236, 240, 241);

            // Setup scroll event
            flowLayoutPanelProducts.Scroll += FlowLayoutPanelProducts_Scroll;

            // Style search textbox
            txtSearch.ForeColor = Color.Gray;
            txtSearch.Enter += (s, e) => {
                if (txtSearch.Text == "Nhập tên sản phẩm...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };
            txtSearch.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Nhập tên sản phẩm...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };

            // Initialize sort combo
            cboSort.Items.Clear();
            cboSort.Items.Add("Mới nhất");
            cboSort.Items.Add("Cũ nhất");
            cboSort.SelectedIndex = 0;

            // Style buttons
            StyleButton(btnSearch, Color.FromArgb(41, 128, 185));
            StyleButton(btnNhapKho, Color.FromArgb(39, 174, 96));
            StyleButton(btnXuatKho, Color.FromArgb(41, 128, 185));
            StyleButton(btnDelete, Color.FromArgb(231, 76, 60));
            StyleButton(btnCapNhat, Color.FromArgb(230, 126, 34));
        }

        private void StyleButton(IconButton btn, Color bgColor)
        {
            btn.BackColor = bgColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.IconColor = Color.White;

            // Add hover effect
            btn.MouseEnter += (s, e) => {
                btn.BackColor = ControlPaint.Light(bgColor, 0.1f);
            };
            btn.MouseLeave += (s, e) => {
                btn.BackColor = bgColor;
            };
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
            Color categoryColor = categoryColors.ContainsKey(product.CategoryName)
                ? categoryColors[product.CategoryName]
                : Color.FromArgb(127, 140, 141);

            // Main card panel with rounded corners - INCREASED SIZE
            Panel card = new RoundedPanel
            {
                Width = 280,
                Height = 180,
                BackColor = Color.White,
                Margin = new Padding(8),
                Tag = product,
                CornerRadius = 10,
                Cursor = Cursors.Hand
            };

            // Add subtle shadow effect
            card.Paint += (s, e) => {
                using (var path = GetRoundedRectPath(card.ClientRectangle, 10))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var pen = new Pen(Color.FromArgb(30, 0, 0, 0), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };

            // Hover effects - brighten card
            card.MouseEnter += (s, e) => {
                card.BackColor = Color.FromArgb(248, 249, 250);
            };
            card.MouseLeave += (s, e) => {
                card.BackColor = Color.White;
            };

            // Category color strip (left side, rounded)
            Panel colorStrip = new Panel
            {
                Width = 5,
                Height = 180,
                BackColor = categoryColor,
                Location = new Point(0, 0)
            };
            card.Controls.Add(colorStrip);

            // Checkbox with custom style
            CheckBox chkSelect = new CheckBox
            {
                Location = new Point(15, 12),
                Width = 20,
                Height = 20,
                Tag = product.ProductID,
                Checked = selectedProducts.ContainsKey(product.ProductID),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            chkSelect.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199);
            chkSelect.FlatAppearance.CheckedBackColor = Color.FromArgb(41, 128, 185);
            chkSelect.CheckedChanged += ChkSelect_CheckedChanged;
            card.Controls.Add(chkSelect);

            // Click anywhere on card to toggle checkbox
            card.Click += (s, e) => {
                chkSelect.Checked = !chkSelect.Checked;
            };

            // Product name (bold, larger) - MULTILINE with auto font resize
            Label lblName = new Label
            {
                Text = product.ProductName,
                Location = new Point(45, 10),
                Width = 225,
                Height = 35,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = false,
                Cursor = Cursors.Hand
            };

            // Auto-resize font if text is too long
            using (Graphics g = lblName.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(product.ProductName, lblName.Font);
                if (textSize.Width > lblName.Width)
                {
                    // Try smaller font sizes
                    if (textSize.Width > lblName.Width * 1.5)
                        lblName.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
                    else if (textSize.Width > lblName.Width * 1.2)
                        lblName.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                    else
                        lblName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }

            lblName.Click += (s, e) => chkSelect.Checked = !chkSelect.Checked;
            card.Controls.Add(lblName);

            // Divider line
            Panel divider = new Panel
            {
                Location = new Point(15, 48),
                Width = 255,
                Height = 1,
                BackColor = Color.FromArgb(236, 240, 241)
            };
            card.Controls.Add(divider);

            // Info section - COMPACT
            int yPos = 56;

            // Quantity
            Label lblQuantity = new Label
            {
                Text = $"SL: {product.SoLuong} {product.DonViTinh}",
                Location = new Point(15, yPos),
                Width = 255,
                Height = 20,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoEllipsis = true,
                Cursor = Cursors.Hand
            };
            lblQuantity.Click += (s, e) => chkSelect.Checked = !chkSelect.Checked;
            card.Controls.Add(lblQuantity);
            yPos += 22;

            // Expiry date
            Label lblExpiry = new Label
            {
                Text = $"HSD: {product.HanSuDung}",
                Location = new Point(15, yPos),
                Width = 255,
                Height = 20,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoEllipsis = true,
                Cursor = Cursors.Hand
            };
            lblExpiry.Click += (s, e) => chkSelect.Checked = !chkSelect.Checked;
            card.Controls.Add(lblExpiry);
            yPos += 22;

            // Import date
            Label lblImportDate = new Label
            {
                Text = $"Ngày nhập: {product.NgayNhapKho}",
                Location = new Point(15, yPos),
                Width = 255,
                Height = 20,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoEllipsis = true,
                Cursor = Cursors.Hand
            };
            lblImportDate.Click += (s, e) => chkSelect.Checked = !chkSelect.Checked;
            card.Controls.Add(lblImportDate);

            // Category badge (rounded, modern) - SMALLER
            Panel categoryBadge = new RoundedPanel
            {
                Location = new Point(15, 130),
                Width = 110,
                Height = 24,
                BackColor = categoryColor,
                CornerRadius = 12,
                Cursor = Cursors.Hand
            };
            categoryBadge.Click += (s, e) => chkSelect.Checked = !chkSelect.Checked;

            Label lblCategory = new Label
            {
                Text = product.CategoryName,
                Location = new Point(0, 0),
                Width = 110,
                Height = 24,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true,
                Cursor = Cursors.Hand
            };
            lblCategory.Click += (s, e) => chkSelect.Checked = !chkSelect.Checked;
            categoryBadge.Controls.Add(lblCategory);
            card.Controls.Add(categoryBadge);

            // Supplier (bottom) - SMALLER
            Label lblSupplier = new Label
            {
                Text = $"NCC: {product.SupplierName}",
                Location = new Point(15, 158),
                Width = 255,
                Height = 16,
                Font = new Font("Segoe UI", 7F, FontStyle.Italic),
                ForeColor = Color.FromArgb(149, 165, 166),
                AutoEllipsis = true,
                Cursor = Cursors.Hand
            };
            lblSupplier.Click += (s, e) => chkSelect.Checked = !chkSelect.Checked;
            card.Controls.Add(lblSupplier);

            // Make color strip also clickable
            colorStrip.Cursor = Cursors.Hand;
            colorStrip.Click += (s, e) => chkSelect.Checked = !chkSelect.Checked;

            return card;
        }

        // Helper method for rounded rectangles
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
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

            btnNhapKho.Visible = (selectedCount == 1);
            btnXuatKho.Enabled = (selectedCount > 0);
            btnDelete.Enabled = (selectedCount > 0);
            btnCapNhat.Enabled = (selectedCount == 1);
        }

        private void FlowLayoutPanelProducts_Scroll(object sender, ScrollEventArgs e)
        {
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

            string searchText = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText) && searchText != "nhập tên sản phẩm...")
            {
                filteredProducts = filteredProducts
                    .Where(p => p.ProductName.ToLower().Contains(searchText))
                    .ToList();
            }

            if (cboCategory.SelectedIndex > 0)
            {
                string selectedCategory = cboCategory.SelectedItem.ToString();
                filteredProducts = filteredProducts
                    .Where(p => p.CategoryName == selectedCategory)
                    .ToList();
            }

            if (cboSort.SelectedIndex == 0)
            {
                filteredProducts = filteredProducts
                    .OrderByDescending(p => p.NgayNhapKho)
                    .ToList();
            }
            else if (cboSort.SelectedIndex == 1)
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
                // Open FormNhapKho
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

            List<ProductInfo> selectedProductsList = allProducts
                .Where(p => selectedProducts.ContainsKey(p.ProductID))
                .ToList();

            // Open FormXuatKho
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProducts.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get selected products list
            List<ProductInfo> selectedProductsList = allProducts
                .Where(p => selectedProducts.ContainsKey(p.ProductID))
                .ToList();

            // Show modal confirmation
            using (FormDeleteConfirmation deleteForm = new FormDeleteConfirmation(selectedProductsList))
            {
                if (deleteForm.ShowDialog(this) == DialogResult.OK || deleteForm.IsConfirmed)
                {
                    // Delete confirmed
                    foreach (int productID in selectedProducts.Keys.ToList())
                    {
                        string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                        dbHelper.ExecuteNonQuery(query, new System.Data.SQLite.SQLiteParameter[] {
                    new System.Data.SQLite.SQLiteParameter("@ProductID", productID)
                });
                    }

                    // Clear selection and reload
                    selectedProducts.Clear();
                    chkSelectAll.Checked = false;

                    MessageBox.Show("Đã xóa sản phẩm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadProducts();
                }
            }
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
                // Show modal update form
                using (FormUpdateProduct updateForm = new FormUpdateProduct(product, dbHelper))
                {
                    if (updateForm.ShowDialog(this) == DialogResult.OK || updateForm.IsUpdated)
                    {
                        // Clear selection and reload
                        selectedProducts.Clear();
                        chkSelectAll.Checked = false;
                        LoadProducts();
                    }
                }
            }
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
            string queryCat = "SELECT CategoryID FROM Categories WHERE CategoryName = @CategoryName";
            object catResult = dbHelper.ExecuteScalar(queryCat, new System.Data.SQLite.SQLiteParameter[] {
                new System.Data.SQLite.SQLiteParameter("@CategoryName", categoryName)
            });
            int categoryID = catResult != null ? Convert.ToInt32(catResult) : 0;

            string querySup = "SELECT SupplierID FROM Suppliers WHERE SupplierName = @SupplierName";
            object supResult = dbHelper.ExecuteScalar(querySup, new System.Data.SQLite.SQLiteParameter[] {
                new System.Data.SQLite.SQLiteParameter("@SupplierName", supplierName)
            });
            int supplierID = supResult != null ? Convert.ToInt32(supResult) : 0;

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

        private void flowLayoutPanelProducts_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    // Helper class for rounded panels
    public class RoundedPanel : Panel
    {
        public int CornerRadius { get; set; } = 10;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = GetRoundedRectPath(this.ClientRectangle, CornerRadius))
            {
                this.Region = new Region(path);
                using (Pen pen = new Pen(this.BackColor, 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter - 1, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter - 1, rect.Bottom - diameter - 1, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter - 1, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
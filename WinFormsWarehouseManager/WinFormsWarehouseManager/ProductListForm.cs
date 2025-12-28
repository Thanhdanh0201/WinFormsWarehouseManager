using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using WinFormsWarehouseManager.db;
using WinFormsWarehouseManager.Models;
using WinFormsWarehouseManager.Utils;

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
        private void TxtSearch_Paint(object sender, PaintEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null) return;

            // Draw custom border
            using (Pen pen = new Pen(Color.FromArgb(2, 51, 66), 2))
            {
                Rectangle rect = new Rectangle(0, 0, txt.Width - 1, txt.Height - 1);
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        private void InitializeCategoryColors()
        {
            categoryColors = new Dictionary<string, Color>()
            {
                { "Thực phẩm", Color.FromArgb(46, 204, 113) },
                { "Linh kiện điện tử", Color.FromArgb(231, 76, 60) },
                { "Đồ gia dụng", Color.FromArgb(52, 152, 219) },
                { "Mỹ phẩm", Color.FromArgb(149, 165, 166) },
                { "Vật liệu xây dựng", Color.FromArgb(26, 188, 156) },
                { "Đồ dùng văn phòng", Color.FromArgb(241, 196, 15) }
            };
        }
        private void WrapComboBoxWithBorder(ComboBox cbo, Panel parentPanel)
        {
            // Tạo panel wrapper cho border
            Panel borderPanel = new Panel
            {
                Location = new Point(cbo.Location.X - 2, cbo.Location.Y - 2),
                Size = new Size(cbo.Width + 4, cbo.Height + 4),
                BackColor = Color.FromArgb(2, 51, 66),
                Padding = new Padding(2)
            };

            // Di chuyển combobox vào panel
            parentPanel.Controls.Remove(cbo);
            cbo.Location = new Point(2, 2);
            borderPanel.Controls.Add(cbo);
            parentPanel.Controls.Add(borderPanel);
            borderPanel.BringToFront();
        }

        private void InitializeUI()
        {
            this.BackColor = Color.FromArgb(236, 240, 241);

            panelFilter.BackColor = Color.White;
            panelButtons.BackColor = Color.White;
            flowLayoutPanelProducts.BackColor = Color.FromArgb(236, 240, 241);

            // Setup scroll event
            flowLayoutPanelProducts.Scroll += FlowLayoutPanelProducts_Scroll;

            // Style search textbox - CẬP NHẬT cho RJTextBox
            txtSearch.PlaceholderText = "Nhập tên sản phẩm...";
            txtSearch.PlaceholderColor = Color.Gray;
            txtSearch._TextChanged += (s, e) => {
                // Optional: handle text changed
            };

            // Initialize sort combo - CẬP NHẬT
            cboSort.Items.Clear();
            cboSort.Items.Add("Mới nhất");
            cboSort.Items.Add("Cũ nhất");
            cboSort.SelectedIndex = 0;

            // Style buttons - CẬP NHẬT màu cho btnSearch, btnNhapKho, btnXuatKho
            Color primaryColor = Color.FromArgb(2, 51, 66);
            //StyleButton(btnSearch, primaryColor);
            //StyleButton(btnNhapKho, primaryColor);
            //StyleButton(btnXuatKho, primaryColor);
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

            // Main card panel with rounded corners
            Panel card = new RoundedPanel
            {
                Width = 280,
                Height = 220,
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

            // Hover effects
            card.MouseEnter += (s, e) => {
                card.BackColor = Color.FromArgb(248, 249, 250);
            };
            card.MouseLeave += (s, e) => {
                card.BackColor = Color.White;
            };

            // Category color strip
            Panel colorStrip = new Panel
            {
                Width = 5,
                Height = 190,
                BackColor = categoryColor,
                Location = new Point(0, 0)
            };
            card.Controls.Add(colorStrip);

            // ===== CUSTOM CHECKBOX WITH COLOR =====
            CheckBox chkSelect = new CheckBox
            {
                Location = new Point(15, 12),
                Width = 20,
                Height = 20,
                Tag = product.ProductID,
                Checked = selectedProducts.ContainsKey(product.ProductID),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Appearance = Appearance.Button,
                BackColor = Color.FromArgb(189, 195, 199), // Màu mờ khi chưa chọn
                ForeColor = Color.White
            };

            // Style checkbox
            chkSelect.FlatAppearance.BorderSize = 0;
            chkSelect.FlatAppearance.CheckedBackColor = Color.FromArgb(2, 51, 66);

            // Custom paint để vẽ dấu tick
            chkSelect.Paint += (s, e) => {
                CheckBox cb = s as CheckBox;
                if (cb.Checked)
                {
                    // Vẽ dấu tick trắng
                    using (Pen pen = new Pen(Color.White, 2))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        // Vẽ dấu tick
                        e.Graphics.DrawLine(pen, 5, 10, 8, 14);
                        e.Graphics.DrawLine(pen, 8, 14, 15, 6);
                    }
                }
            };

            chkSelect.CheckedChanged += (s, e) => {
                CheckBox cb = s as CheckBox;
                // Đổi màu khi check/uncheck
                cb.BackColor = cb.Checked
                    ? Color.FromArgb(2, 51, 66)
                    : Color.FromArgb(189, 195, 199);
                cb.Invalidate(); // Vẽ lại để hiện dấu tick

                ChkSelect_CheckedChanged(s, e);
            };

            card.Controls.Add(chkSelect);
            // ===== END CUSTOM CHECKBOX =====

            // Click anywhere on card to toggle checkbox
            card.Click += (s, e) => {
                chkSelect.Checked = !chkSelect.Checked;
            };

            // Product name
            Label lblName = new Label
            {
                Text = product.ProductName,
                Location = new Point(37, 10),
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

            // Info section
            int yPos = 56;

            // Quantity
            Label lblQuantity = new Label
            {
                Text = $"SL: {product.SoLuong} {product.DonViTinh}",
                Location = new Point(15, yPos - 10),
                Width = 255,
                Height = 23,
                Font = new Font("Segoe UI", 7.5F),
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
                Location = new Point(15, yPos - 7),
                Width = 255,
                Height = 23,
                Font = new Font("Segoe UI", 7.5F),
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
                Location = new Point(15, yPos - 5),
                Width = 255,
                Height = 23,
                Font = new Font("Segoe UI", 7F),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoEllipsis = true,
                Cursor = Cursors.Hand
            };
            lblImportDate.Click += (s, e) => chkSelect.Checked = !chkSelect.Checked;
            card.Controls.Add(lblImportDate);

            // Category badge
            Panel categoryBadge = new RoundedPanel
            {
                Location = new Point(15, 130 - 5),
                Width = 130,
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
                Font = new Font("Segoe UI", 6.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true,
                Cursor = Cursors.Hand
            };
            lblCategory.Click += (s, e) => chkSelect.Checked = !chkSelect.Checked;
            categoryBadge.Controls.Add(lblCategory);
            card.Controls.Add(categoryBadge);

            // Supplier
            Label lblSupplier = new Label
            {
                Text = $"NCC: {product.SupplierName}",
                Location = new Point(15, 154 - 3),
                Width = 255,
                Height = 23,
                Font = new Font("Segoe UI", 6F, FontStyle.Italic),
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

            btnNhapKho.Visible = (selectedCount > 0);  
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

            // Lấy text từ RJTextBox - CẬP NHẬT
            string searchText = txtSearch.Texts.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText) && searchText != "nhập tên sản phẩm...")
            {
                filteredProducts = filteredProducts
                    .Where(p => p.ProductName.ToLower().Contains(searchText))
                    .ToList();
            }

            // RJComboBox vẫn dùng SelectedIndex bình thường
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
            if (selectedProducts.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 sản phẩm để nhập kho!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Lấy danh sách sản phẩm đã chọn
                List<ProductInfo> productsToImport = allProducts
                    .Where(p => selectedProducts.ContainsKey(p.ProductID))
                    .ToList();

                // Lọc ra các sản phẩm KHÔNG có nhà cung cấp
                var productsWithoutSupplier = productsToImport
                    .Where(p => p.SupplierID <= 0)
                    .ToList();

                // Lọc ra các sản phẩm CÓ nhà cung cấp
                var validProducts = productsToImport
                    .Where(p => p.SupplierID > 0)
                    .ToList();

                // Nếu không có sản phẩm hợp lệ nào
                if (validProducts.Count == 0)
                {
                    MessageBox.Show("Các sản phẩm đã chọn chưa có nhà cung cấp!\nVui lòng cập nhật nhà cung cấp trước khi nhập kho.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Nếu có sản phẩm bị loại bỏ, thông báo cho user
                if (productsWithoutSupplier.Count > 0)
                {
                    string skippedList = string.Join("\n", productsWithoutSupplier.Select(p => $"- {p.ProductName}"));
                    MessageBox.Show(
                        $"Đã bỏ qua {productsWithoutSupplier.Count} sản phẩm chưa có nhà cung cấp:\n\n{skippedList}\n\n" +
                        $"Sẽ tiếp tục nhập {validProducts.Count} sản phẩm còn lại.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Lấy nhà cung cấp đầu tiên làm default (hoặc có thể để null)
                int defaultSupplierID = validProducts.First().SupplierID;
                string defaultSupplierName = validProducts.First().SupplierName;

                // Tạo TempImportData với supplier mặc định
                TempImportData tempData = new TempImportData(defaultSupplierID, defaultSupplierName);

                // Thêm TẤT CẢ sản phẩm hợp lệ vào temp
                foreach (var product in validProducts)
                {
                    TempImportItem item = new TempImportItem(
                        product.ProductID,
                        product.ProductName,
                        product.CategoryID,
                        product.CategoryName,
                        1, // Số lượng mặc định = 1
                        product.DonViTinh,
                        product.HanSuDung,
                        false, // Không phải sản phẩm mới
                        product.SupplierID,
                        product.SupplierName
                    );

                    tempData.Items.Add(item);
                }

                // Lưu vào file temp
                TempImportManager.Save(tempData);

                // Mở FormNhapKho
                FormNhapKho formNhapKho = new FormNhapKho();
                formNhapKho.ShowDialog(this);

                // Sau khi đóng form, reload lại products
                selectedProducts.Clear();
                chkSelectAll.Checked = false;
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            try
            {
                // 1. Lấy người nhận gần nhất
                int defaultReceiverID = GetLastReceiverID();
                string defaultReceiverName = GetReceiverName(defaultReceiverID);

                if (defaultReceiverID <= 0)
                {
                    MessageBox.Show("Không tìm thấy lịch sử người nhận!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Tạo TempExportData
                TempExportData tempData = new TempExportData(defaultReceiverID, defaultReceiverName);

                // 3. Thêm các sản phẩm đã chọn vào temp
                foreach (int productID in selectedProducts.Keys)
                {
                    ProductInfo product = allProducts.FirstOrDefault(p => p.ProductID == productID);
                    if (product != null && product.SoLuong > 0)
                    {
                        TempExportItem item = new TempExportItem(
                            product.ProductID,
                            product.ProductName,
                            product.CategoryID,
                            product.CategoryName,
                            1, // Số lượng mặc định = 1
                            product.DonViTinh,
                            product.SoLuong,
                            defaultReceiverID,
                            defaultReceiverName
                        );

                        tempData.Items.Add(item);
                    }
                }

                // 4. Lưu vào file temp
                TempExportManager.Save(tempData);

                // 5. Mở FormXuatKho
                FormXuatKho formXuatKho = new FormXuatKho();
                formXuatKho.ShowDialog(this);

                // 6. Sau khi đóng form, reload lại products
                selectedProducts.Clear();
                chkSelectAll.Checked = false;
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private int GetLastReceiverID()
        {
            try
            {
                string query = @"SELECT ReceiverID 
                        FROM ExportReceipts 
                        ORDER BY ExportDate DESC 
                        LIMIT 1";

                object result = dbHelper.ExecuteScalar(query);

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting last receiver: {ex.Message}");
            }

            return 0;
        }

        private string GetReceiverName(int receiverID)
        {
            if (receiverID <= 0)
                return "N/A";

            try
            {
                string query = "SELECT ReceiverName FROM Receivers WHERE ReceiverID = @ReceiverID";
                System.Data.SQLite.SQLiteParameter[] parameters = {
            new System.Data.SQLite.SQLiteParameter("@ReceiverID", receiverID)
        };

                object result = dbHelper.ExecuteScalar(query, parameters);

                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting receiver name: {ex.Message}");
            }

            return "N/A";
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
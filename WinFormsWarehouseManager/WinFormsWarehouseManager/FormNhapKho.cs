using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using WinFormsWarehouseManager.Forms;
using WinFormsWarehouseManager.db;
using WinFormsWarehouseManager.Models;
using WinFormsWarehouseManager.Utils;

namespace WinFormsWarehouseManager
{
    public partial class FormNhapKho : Form
    {
        private DatabaseHelper dbHelper;
        private bool isAddingNewSupplier = false;
        private bool isAddingNewProduct = false;
        private TempImportData tempData;

        // Lưu controls gốc để toggle
        private RJControls.RJTextBox txtTenSP_New;
        private RJControls.RJTextBox txtDonViTinh_New;

        // NEW: Controls được tạo lại hoàn toàn
        private FlowLayoutPanel flowItemsList;
        private Panel panelSupplierInfo;

        public FormNhapKho()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            InitializeForm();
        }

        private void InitializeForm()
        {
            dtpHanSuDung.Visible = true;
            dtpHanSuDung.BringToFront();
            dtpHanSuDung.BackColor = Color.Red; // Tạm thời để dễ nhìn

            LoadNhaCungCap();
            LoadDanhMuc();
            RebuildRightPanel(); // NEW: Rebuild UI
            ResetForm();
        }

        #region Load Data from Database

        private void LoadNhaCungCap()
        {
            string query = "SELECT SupplierID, SupplierName FROM Suppliers ORDER BY SupplierName";
            DataTable dt = dbHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                cbbNhaCungCap.DataSource = dt;
                cbbNhaCungCap.DisplayMember = "SupplierName";
                cbbNhaCungCap.ValueMember = "SupplierID";
                cbbNhaCungCap.SelectedIndex = -1;
            }
        }

        private void LoadDanhMuc()
        {
            string query = "SELECT CategoryID, CategoryName, HanTonKho_Thang FROM Categories ORDER BY CategoryName";
            DataTable dt = dbHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                cbbDanhMuc.DataSource = dt;
                cbbDanhMuc.DisplayMember = "CategoryName";
                cbbDanhMuc.ValueMember = "CategoryID";
                cbbDanhMuc.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// Lấy tên nhà cung cấp từ database theo SupplierID
        /// </summary>
        private string GetSupplierName(int supplierID)
        {
            if (supplierID <= 0)
                return "Chưa chọn NCC";

            try
            {
                string query = "SELECT SupplierName FROM Suppliers WHERE SupplierID = @SupplierID";
                SQLiteParameter[] parameters = {
            new SQLiteParameter("@SupplierID", supplierID)
        };

                DataTable dt = dbHelper.ExecuteQuery(query, parameters);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["SupplierName"].ToString();
                }
            }
            catch (Exception ex)
            {
                // Log error nếu cần
                System.Diagnostics.Debug.WriteLine($"Error getting supplier name: {ex.Message}");
            }

            return "N/A";
        }
        private void LoadSanPhamTheoDanhMuc(int categoryID)
        {
            int? supplierID = GetComboBoxValue(cbbNhaCungCap, "SupplierID");

            if (!supplierID.HasValue)
            {
                cbbTenSP.DataSource = null;
                return;
            }

            string query = @"SELECT ProductID, ProductName, DonViTinh, HanSuDung 
                           FROM Products 
                           WHERE CategoryID = @CategoryID AND SupplierID = @SupplierID
                           ORDER BY ProductName";

            SQLiteParameter[] parameters = {
                new SQLiteParameter("@CategoryID", categoryID),
                new SQLiteParameter("@SupplierID", supplierID.Value)
            };

            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            if (dt != null)
            {
                cbbTenSP.DataSource = dt;
                cbbTenSP.DisplayMember = "ProductName";
                cbbTenSP.ValueMember = "ProductID";
                cbbTenSP.SelectedIndex = -1;
            }
        }

        #endregion

        #region Helper Methods for RJComboBox

        private int? GetComboBoxValue(RJControls.RJComboBox comboBox, string valueMember)
        {
            if (comboBox.SelectedItem == null || comboBox.SelectedIndex == -1)
                return null;

            if (comboBox.SelectedItem is DataRowView drv)
            {
                return Convert.ToInt32(drv[valueMember]);
            }

            return null;
        }

        private void SetComboBoxValue(RJControls.RJComboBox comboBox, string valueMember, int value)
        {
            if (comboBox.DataSource == null)
                return;

            DataTable dt = comboBox.DataSource as DataTable;
            if (dt == null)
                return;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i][valueMember]) == value)
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
        }

        #endregion

        #region ComboBox Events

        private void cbbNhaCungCap_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbNhaCungCap.SelectedItem == null || isAddingNewSupplier)
                return;

            int? supplierID = GetComboBoxValue(cbbNhaCungCap, "SupplierID");
            if (supplierID.HasValue)
            {
                LoadSupplierInfo(supplierID.Value);

                if (cbbDanhMuc.SelectedIndex != -1 && !isAddingNewProduct)
                {
                    int? categoryID = GetComboBoxValue(cbbDanhMuc, "CategoryID");
                    if (categoryID.HasValue)
                    {
                        LoadSanPhamTheoDanhMuc(categoryID.Value);
                    }
                }
            }
        }

        private void LoadSupplierInfo(int supplierID)
        {
            string query = @"SELECT SupplierName, Email, Phone, Address 
                           FROM Suppliers 
                           WHERE SupplierID = @SupplierID";

            SQLiteParameter[] parameters = {
                new SQLiteParameter("@SupplierID", supplierID)
            };

            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtTenNCC.Texts = row["SupplierName"].ToString();
                txtEmail.Texts = row["Email"].ToString();
                txtSDT.Texts = row["Phone"].ToString();
                txtDiaChi.Texts = row["Address"].ToString();
            }
        }

        private void cbbDanhMuc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbDanhMuc.SelectedItem == null)
                return;

            int? categoryID = GetComboBoxValue(cbbDanhMuc, "CategoryID");
            if (!categoryID.HasValue)
                return;

            DataRowView drv = (DataRowView)cbbDanhMuc.SelectedItem;
            int hanTonKho = Convert.ToInt32(drv["HanTonKho_Thang"]);
            lblHanTonKho.Text = $"Hạn tồn kho mặc định: {hanTonKho} tháng";

            if (!isAddingNewProduct)
            {
                LoadSanPhamTheoDanhMuc(categoryID.Value);
            }
        }

        private void cbbTenSP_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTenSP.SelectedItem == null || isAddingNewProduct)
                return;

            DataRowView drv = (DataRowView)cbbTenSP.SelectedItem;

            string donViTinh = drv["DonViTinh"].ToString();
            cbbDonViTinh.DataSource = new string[] { donViTinh };
            cbbDonViTinh.SelectedIndex = 0;

            if (drv["HanSuDung"] != DBNull.Value && !string.IsNullOrEmpty(drv["HanSuDung"].ToString()))
            {
                try
                {
                    dtpHanSuDung.Value = DateTime.Parse(drv["HanSuDung"].ToString());
                }
                catch
                {
                    dtpHanSuDung.Value = DateTime.Now;
                }
            }
        }

        #endregion
        #region Toggle New Supplier Mode

        private RJControls.RJButton btnLuuNCC; // Khai báo button Lưu


        private FontAwesome.Sharp.IconButton btnLuuNCC1; // Khai báo button Lưu (dùng IconButton)

        private void btnNhaCungCapMoi_Click(object sender, EventArgs e)
        {
            if (!isAddingNewSupplier)
            {
                isAddingNewSupplier = true;
                btnNhaCungCapMoi.Text = "Hủy";

                cbbNhaCungCap.Visible = false;
                txtTenNCC.Enabled = true;
                txtEmail.Enabled = true;
                txtSDT.Enabled = true;
                txtDiaChi.Enabled = true;

                txtTenNCC.Texts = "";
                txtEmail.Texts = "";
                txtSDT.Texts = "";
                txtDiaChi.Texts = "";

                // Hiện button Lưu
                ShowSaveSupplierButton();
            }
            else
            {
                CancelNewSupplier();
            }
        }

        private void ShowSaveSupplierButton()
        {
            if (btnLuuNCC1 == null)
            {
                btnLuuNCC1 = new FontAwesome.Sharp.IconButton
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    BackColor = Color.FromArgb(4, 119, 154),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                    ForeColor = Color.White,
                    IconChar = IconChar.Save,
                    IconColor = Color.White,
                    IconFont = IconFont.Auto,
                    IconSize = 28,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    Location = new Point(560, 12), // Kế bên button Hủy (btnNhaCungCapMoi ở 827)
                    Size = new Size(240, 50),
                    Text = "Lưu",
                    TextAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    UseVisualStyleBackColor = false,
                    Cursor = Cursors.Hand,
                    Padding = new Padding(7, 0, 0, 0)
                };
                btnLuuNCC1.FlatAppearance.BorderSize = 0;
                btnLuuNCC1.Click += BtnLuuNCC_Click;

                // Hover effect
                btnLuuNCC1.MouseEnter += (s, e) => btnLuuNCC1.BackColor = Color.FromArgb(4, 119, 154);
                btnLuuNCC1.MouseLeave += (s, e) => btnLuuNCC1.BackColor = Color.FromArgb(4, 119, 154);

                panelNCCTop.Controls.Add(btnLuuNCC1);
            }
            btnLuuNCC1.Visible = true;
            btnLuuNCC1.BringToFront();
        }

        private void BtnLuuNCC1_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtTenNCC.Texts))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNCC.Focus();
                return;
            }

            // Lưu vào database
            int supplierID = SaveNewSupplier();

            if (supplierID > 0)
            {
                // Thông báo thành công
                MessageBox.Show($"✅ Đã lưu nhà cung cấp: {txtTenNCC.Texts.Trim()}", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload danh sách nhà cung cấp
                LoadNhaCungCap();

                // Tự động chọn nhà cung cấp vừa tạo
                SetComboBoxValue(cbbNhaCungCap, "SupplierID", supplierID);

                // Load thông tin supplier vào các textbox (readonly)
                LoadSupplierInfo(supplierID);

                // Trở về chế độ bình thường
                CancelNewSupplier();
            }
        }
        private void BtnLuuNCC_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtTenNCC.Texts))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNCC.Focus();
                return;
            }

            // Lưu vào database
            int supplierID = SaveNewSupplier();

            if (supplierID > 0)
            {
                // Thông báo thành công
                MessageBox.Show($"✅ Đã lưu nhà cung cấp: {txtTenNCC.Texts.Trim()}", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload danh sách nhà cung cấp
                LoadNhaCungCap();

                // Tự động chọn nhà cung cấp vừa tạo
                SetComboBoxValue(cbbNhaCungCap, "SupplierID", supplierID);

                // Load thông tin supplier vào các textbox (readonly)
                LoadSupplierInfo(supplierID);

                // Trở về chế độ bình thường
                CancelNewSupplier();
            }
        }

        private void CancelNewSupplier()
        {
            isAddingNewSupplier = false;
            btnNhaCungCapMoi.Text = "Nhà cung cấp mới";

            cbbNhaCungCap.Visible = true;
            txtTenNCC.Enabled = false;
            txtEmail.Enabled = false;
            txtSDT.Enabled = false;
            txtDiaChi.Enabled = false;

            // Ẩn button Lưu
            if (btnLuuNCC1 != null)
            {
                btnLuuNCC1.Visible = false;
            }

            // Không xóa textbox nữa vì đã load info từ supplier vừa chọn
        }

        #endregion

        #region Save New Supplier

        private int SaveNewSupplier()
        {
            string tenNCC = txtTenNCC.Texts.Trim();
            string email = txtEmail.Texts.Trim();
            string sdt = txtSDT.Texts.Trim();
            string diaChi = txtDiaChi.Texts.Trim();

            string query = @"INSERT INTO Suppliers (SupplierName, Email, Phone, Address)
                   VALUES (@SupplierName, @Email, @Phone, @Address);
                   SELECT last_insert_rowid();";

            SQLiteParameter[] parameters = {
        new SQLiteParameter("@SupplierName", tenNCC),
        new SQLiteParameter("@Email", email),
        new SQLiteParameter("@Phone", sdt),
        new SQLiteParameter("@Address", diaChi)
    };

            object result = dbHelper.ExecuteScalar(query, parameters);

            if (result != null)
            {
                return Convert.ToInt32(result);
            }

            MessageBox.Show("❌ Lỗi khi thêm nhà cung cấp mới!", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -1;
        }

        #endregion

        #region Toggle New Product Mode

        private void btnSanPhamMoi_Click(object sender, EventArgs e)
        {
            if (!isAddingNewProduct)
            {
                if (!isAddingNewSupplier && cbbNhaCungCap.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp trước!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbbDanhMuc.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn danh mục trước!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                isAddingNewProduct = true;
                btnSanPhamMoi.Text = "Hủy";
                SwitchToNewProductMode();
            }
            else
            {
                CancelNewProduct();
            }
        }

        private void SwitchToNewProductMode()
        {
            cbbTenSP.Visible = false;

            if (txtTenSP_New == null)
            {
                txtTenSP_New = new RJControls.RJTextBox();
                txtTenSP_New.Location = cbbTenSP.Location;
                txtTenSP_New.Size = cbbTenSP.Size;
                txtTenSP_New.BorderColor = System.Drawing.Color.FromArgb(2, 51, 66);
                txtTenSP_New.BorderFocusColor = System.Drawing.Color.FromArgb(255, 179, 71);
                txtTenSP_New.BorderRadius = 5;
                txtTenSP_New.BorderSize = 2;
                txtTenSP_New.PlaceholderText = "Nhập tên sản phẩm";
                panelSPBody.Controls.Add(txtTenSP_New);
            }
            txtTenSP_New.Visible = true;
            txtTenSP_New.Texts = "";

            cbbDonViTinh.Visible = false;

            if (txtDonViTinh_New == null)
            {
                txtDonViTinh_New = new RJControls.RJTextBox();
                txtDonViTinh_New.Location = cbbDonViTinh.Location;
                txtDonViTinh_New.Size = cbbDonViTinh.Size;
                txtDonViTinh_New.BorderColor = System.Drawing.Color.FromArgb(2, 51, 66);
                txtDonViTinh_New.BorderFocusColor = System.Drawing.Color.FromArgb(255, 179, 71);
                txtDonViTinh_New.BorderRadius = 5;
                txtDonViTinh_New.BorderSize = 2;
                txtDonViTinh_New.PlaceholderText = "Nhập đơn vị tính (kg, hộp, chai...)";
                panelSPBody.Controls.Add(txtDonViTinh_New);
            }
            txtDonViTinh_New.Visible = true;
            txtDonViTinh_New.Texts = "";

            dtpHanSuDung.Value = DateTime.Now;
        }

        private void CancelNewProduct()
        {
            isAddingNewProduct = false;
            btnSanPhamMoi.Text = "Sản phẩm mới";

            if (txtTenSP_New != null) txtTenSP_New.Visible = false;
            if (txtDonViTinh_New != null) txtDonViTinh_New.Visible = false;

            cbbTenSP.Visible = true;
            cbbDonViTinh.Visible = true;

            if (cbbDanhMuc.SelectedIndex != -1)
            {
                int? categoryID = GetComboBoxValue(cbbDanhMuc, "CategoryID");
                if (categoryID.HasValue)
                {
                    LoadSanPhamTheoDanhMuc(categoryID.Value);
                }
            }
        }

        #endregion

        #region Add to Temp List

        private void btnThemVaoDanhSach_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            int supplierID = GetOrCreateSupplierID();
            if (supplierID == -1)
                return;

            TempImportItem newItem = CreateTempImportItem(supplierID);
            if (newItem == null)
                return;

            AddItemToList(newItem);
            SaveTempDataToFile();
            ResetProductPanel();
        }

        private bool ValidateInput()
        {
            if (!isAddingNewSupplier && cbbNhaCungCap.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (isAddingNewSupplier)
            {
                if (string.IsNullOrWhiteSpace(txtTenNCC.Texts))
                {
                    MessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (cbbDanhMuc.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn danh mục!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (isAddingNewProduct)
            {
                if (string.IsNullOrWhiteSpace(txtTenSP_New.Texts))
                {
                    MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtDonViTinh_New.Texts))
                {
                    MessageBox.Show("Vui lòng nhập đơn vị tính!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                if (cbbTenSP.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (!int.TryParse(txtSoLuong.Texts, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private int GetOrCreateSupplierID()
        {
            if (isAddingNewSupplier)
            {
                int supplierID = SaveNewSupplier();
                if (supplierID == -1)
                    return -1;

                if (tempData == null)
                {
                    tempData = new TempImportData(supplierID, txtTenNCC.Texts.Trim());
                }
                else
                {
                    tempData.SupplierID = supplierID;
                    tempData.SupplierName = txtTenNCC.Texts.Trim();
                }

                return supplierID;
            }
            else
            {
                int? supplierID = GetComboBoxValue(cbbNhaCungCap, "SupplierID");
                if (!supplierID.HasValue)
                    return -1;

                DataRowView drv = (DataRowView)cbbNhaCungCap.SelectedItem;
                string supplierName = drv["SupplierName"].ToString();

                if (tempData == null)
                {
                    tempData = new TempImportData(supplierID.Value, supplierName);
                }
                else
                {
                    // KEY FIX: Luôn update khi chọn supplier mới
                    tempData.SupplierID = supplierID.Value;
                    tempData.SupplierName = supplierName;
                }

                return supplierID.Value;
            }
        }

        private TempImportItem CreateTempImportItem(int supplierID)
        {
            int? categoryID = GetComboBoxValue(cbbDanhMuc, "CategoryID");
            if (!categoryID.HasValue)
                return null;

            DataRowView drvCategory = (DataRowView)cbbDanhMuc.SelectedItem;
            string categoryName = drvCategory["CategoryName"].ToString();

            int quantity = int.Parse(txtSoLuong.Texts);
            string hanSuDung = dtpHanSuDung.Value.ToString("yyyy-MM-dd");

            // Lấy tên supplier
            string supplierName = GetSupplierName(supplierID);

            if (isAddingNewProduct)
            {
                return new TempImportItem(
                    null,
                    txtTenSP_New.Texts.Trim(),
                    categoryID.Value,
                    categoryName,
                    quantity,
                    txtDonViTinh_New.Texts.Trim(),
                    hanSuDung,
                    true,
                    supplierID,      // THÊM
                    supplierName     // THÊM
                );
            }
            else
            {
                int? productID = GetComboBoxValue(cbbTenSP, "ProductID");
                if (!productID.HasValue)
                    return null;

                return new TempImportItem(
                    productID.Value,
                    cbbTenSP.Text,
                    categoryID.Value,
                    categoryName,
                    quantity,
                    cbbDonViTinh.Text,
                    hanSuDung,
                    false,
                    supplierID,      // THÊM
                    supplierName     // THÊM
                );
            }
        }

        private void AddItemToList(TempImportItem item)
        {
            AddItemCard(item);
            tempData.Items.Add(item);
            UpdateSupplierInfoDisplay();
            UpdateTongSoLuong();
        }

        private void SaveTempDataToFile()
        {
            if (tempData != null && tempData.Items.Count > 0)
            {
                TempImportManager.Save(tempData);
            }
        }

        #endregion

        #region Button Actions

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (flowItemsList.Controls.Count == 0)
            {
                MessageBox.Show("Danh sách nhập trống! Vui lòng thêm sản phẩm.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Xác nhận nhập {flowItemsList.Controls.Count} sản phẩm vào kho?",
                "Xác nhận nhập kho",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            bool success = SaveToDatabase();

            if (success)
            {
                MessageBox.Show("Nhập kho thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                TempImportManager.Delete();
                ResetForm();
            }
        }

        private bool SaveToDatabase()
        {
            try
            {
                int userID = UserSession.CurrentUserID;
                if (userID == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                foreach (TempImportItem item in tempData.Items)
                {
                    if (item == null)
                        continue;

                    if (item.IsNewProduct)
                    {
                        int productID = InsertNewProduct(item, item.SupplierID); // DÙNG item.SupplierID
                        if (productID == -1)
                            return false;
                    }
                    else
                    {
                        bool insertResult = InsertImportReceipt(
                            userID,
                            item.SupplierID,  // DÙNG item.SupplierID
                            item.ProductID.Value,
                            item.Quantity
                        );

                        if (!insertResult)
                            return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private int InsertNewProduct(TempImportItem item, int supplierID)
        {
            string query = @"INSERT INTO Products (ProductName, SoLuong, DonViTinh, HanSuDung, CategoryID, SupplierID)
                           VALUES (@ProductName, @SoLuong, @DonViTinh, @HanSuDung, @CategoryID, @SupplierID);
                           SELECT last_insert_rowid();";

            SQLiteParameter[] parameters = {
                new SQLiteParameter("@ProductName", item.ProductName),
                new SQLiteParameter("@SoLuong", item.Quantity),
                new SQLiteParameter("@DonViTinh", item.DonViTinh),
                new SQLiteParameter("@HanSuDung", item.HanSuDung),
                new SQLiteParameter("@CategoryID", item.CategoryID),
                new SQLiteParameter("@SupplierID", supplierID)
            };

            object result = dbHelper.ExecuteScalar(query, parameters);

            if (result != null)
            {
                return Convert.ToInt32(result);
            }

            MessageBox.Show($"Lỗi khi thêm sản phẩm: {item.ProductName}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -1;
        }

        private bool InsertImportReceipt(int userID, int supplierID, int productID, int quantity)
        {
            string query = @"INSERT INTO ImportReceipts (ImportDate, UserID, SupplierID, ProductID, Quantity)
                           VALUES (datetime('now'), @UserID, @SupplierID, @ProductID, @Quantity)";

            SQLiteParameter[] parameters = {
                new SQLiteParameter("@UserID", userID),
                new SQLiteParameter("@SupplierID", supplierID),
                new SQLiteParameter("@ProductID", productID),
                new SQLiteParameter("@Quantity", quantity)
            };

            int result = dbHelper.ExecuteNonQuery(query, parameters);

            if (result <= 0)
            {
                MessageBox.Show("Lỗi khi lưu phiếu nhập!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn hủy? Tất cả dữ liệu sẽ bị xóa.",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                TempImportManager.Delete();
                ResetForm();
            }
        }

        #endregion

        #region Helper Methods

        private void ResetForm()
        {
            CancelNewSupplier();
            cbbNhaCungCap.SelectedIndex = -1;

            CancelNewProduct();
            cbbDanhMuc.SelectedIndex = -1;
            cbbTenSP.DataSource = null;
            cbbDonViTinh.DataSource = null;
            txtSoLuong.Texts = "1";
            dtpHanSuDung.Value = DateTime.Now;
            lblHanTonKho.Text = "Hạn tồn kho mặc định: 0";

            flowItemsList.Controls.Clear();
            panelSupplierInfo.Visible = false;
            UpdateTongSoLuong();

            tempData = null;
        }

        private void ResetProductPanel()
        {
            if (isAddingNewProduct)
            {
                CancelNewProduct();
            }

            cbbTenSP.SelectedIndex = -1;
            cbbDonViTinh.DataSource = null;
            txtSoLuong.Texts = "1";
            dtpHanSuDung.Value = DateTime.Now;
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

        private void FormNhapKho_Load(object sender, EventArgs e)
        {
            LoadTempDataIfExists();
        }

        private void LoadTempDataIfExists()
        {
            if (TempImportManager.Exists())
            {
                tempData = TempImportManager.Load();

                if (tempData != null)
                {
                    // Tự động set supplier
                    SetComboBoxValue(cbbNhaCungCap, "SupplierID", tempData.SupplierID);

                    // Tự động load tất cả items
                    foreach (var item in tempData.Items)
                    {
                        AddItemCard(item);
                    }

                    UpdateSupplierInfoDisplay();
                    UpdateTongSoLuong();
                }
            }
        }

        private void dtpHanSuDung_ValueChanged(object sender, EventArgs e)
        {
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ImportHistoryModal modal = new ImportHistoryModal();
            modal.ShowDialog(this);
        }

        #region REBUILD RIGHT PANEL - CLEAN & RESPONSIVE

        private void RebuildRightPanel()
        {
            // XÓA SẠCH tất cả controls cũ trong panelDanhSach
            panelDanhSach.Controls.Clear();

            // 1. PANEL TOP - Giữ lại (chứa lblDanhSachTitle)
            panelDanhSachTop.Dock = DockStyle.Top;
            panelDanhSach.Controls.Add(panelDanhSachTop);

            // 2. PANEL BOTTOM - Giữ lại (chứa buttons + lblTongSoLuong)
            panelDanhSachBottom.Dock = DockStyle.Bottom;
            panelDanhSach.Controls.Add(panelDanhSachBottom);

            // 3. PANEL SUPPLIER INFO - Tạo mới
            panelSupplierInfo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = Color.FromArgb(240, 248, 255),
                Visible = false,
                Padding = new Padding(15, 10, 15, 10)
            };
            panelDanhSach.Controls.Add(panelSupplierInfo);

            // 4. FLOW PANEL - Tạo mới (chứa cards)
            flowItemsList = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(15, 10, 15, 10)
            };

            // KEY: Event handlers cho responsive
            flowItemsList.Resize += FlowItemsList_Resize;
            flowItemsList.ClientSizeChanged += FlowItemsList_Resize;

            panelDanhSach.Controls.Add(flowItemsList);

            // Đảm bảo thứ tự hiển thị đúng
            panelDanhSachTop.BringToFront();
            panelSupplierInfo.BringToFront();
            flowItemsList.BringToFront();
            panelDanhSachBottom.BringToFront();
        }

        private void FlowItemsList_Resize(object sender, EventArgs e)
        {
            if (flowItemsList == null) return;

            // Tính width available (ClientSize tự động trừ scrollbar)
            int availableWidth = flowItemsList.ClientSize.Width - 30; // 30 = left + right padding

            foreach (Control ctrl in flowItemsList.Controls)
            {
                if (ctrl is Panel cardPanel)
                {
                    // Resize card
                    cardPanel.Width = availableWidth;

                    // Reposition buttons để luôn ở bên phải
                    foreach (Control child in cardPanel.Controls)
                    {
                        if (child is IconButton btn)
                        {
                            if (btn.IconChar == IconChar.Edit)
                            {
                                btn.Location = new Point(cardPanel.Width - 110, btn.Location.Y);
                            }
                            else if (btn.IconChar == IconChar.TrashAlt)
                            {
                                btn.Location = new Point(cardPanel.Width - 55, btn.Location.Y);
                            }
                        }
                    }
                }
            }
        }

        private void UpdateSupplierInfoDisplay()
        {
            panelSupplierInfo.Controls.Clear();

            if (tempData == null || tempData.SupplierID == 0)
            {
                panelSupplierInfo.Visible = false;
                return;
            }

            panelSupplierInfo.Visible = false;

            // Icon
            IconPictureBox iconSupplier = new IconPictureBox
            {
                IconChar = IconChar.TruckField,
                IconColor = Color.FromArgb(0, 162, 173),
                IconSize = 28,
                Location = new Point(15, 18),
                Size = new Size(28, 28)
            };

            // Label
            Label lblSupplier = new Label
            {
                Text = $"Nhà cung cấp: {tempData.SupplierName}",
                ForeColor = Color.FromArgb(0, 162, 173),
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                Location = new Point(52, 20),
                AutoSize = true
            };

            panelSupplierInfo.Controls.Add(iconSupplier);
            panelSupplierInfo.Controls.Add(lblSupplier);

            // Border bottom
            panelSupplierInfo.Paint -= PanelSupplierInfo_Paint;
            panelSupplierInfo.Paint += PanelSupplierInfo_Paint;
        }

        private void PanelSupplierInfo_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null) return;

            using (Pen pen = new Pen(Color.FromArgb(220, 225, 230), 2))
            {
                e.Graphics.DrawLine(pen, 0, panel.Height - 1, panel.Width, panel.Height - 1);
            }
        }

        private void UpdateTongSoLuong()
        {
            int tongSP = flowItemsList.Controls.Count;
            lblTongSoLuong.Text = $"Tổng số lượng: {tongSP} SP";
        }

        private void AddItemCard(TempImportItem item)
        {
            // Tính width động
            int cardWidth = flowItemsList.ClientSize.Width - 30;

            Panel cardPanel = new Panel
            {
                Width = cardWidth,
                Height = 150,
                BackColor = Color.FromArgb(248, 249, 250),
                Margin = new Padding(0, 0, 0, 12),
                Padding = new Padding(15),
                Tag = item
            };

            // Border
            cardPanel.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(220, 225, 230), 1))
                {
                    Rectangle rect = new Rectangle(0, 0, cardPanel.Width - 1, cardPanel.Height - 1);
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };

            // Icon sản phẩm chính (bên trái)
            IconPictureBox iconProduct = new IconPictureBox
            {
                IconChar = item.IsNewProduct ? IconChar.PlusCircle : IconChar.BoxOpen,
                IconColor = item.IsNewProduct ? Color.FromArgb(40, 167, 69) : Color.FromArgb(52, 152, 219),
                IconSize = 40,
                Location = new Point(15, 15),
                Size = new Size(40, 40)
            };

            // Tên sản phẩm
            Label lblProductName = new Label
            {
                Text = item.ProductName,
                ForeColor = Color.FromArgb(33, 37, 41),
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                Location = new Point(70, 8),
                AutoSize = false,
                Size = new Size(320, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Tag "MỚI"
            if (item.IsNewProduct)
            {
                Label lblNew = new Label
                {
                    Text = "MỚI",
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(40, 167, 69),
                    Font = new Font("Segoe UI", 7F, FontStyle.Bold),
                    Location = new Point(400, 10),
                    AutoSize = true,
                    Padding = new Padding(6, 3, 6, 3)
                };
                cardPanel.Controls.Add(lblNew);
            }

            // Dòng 1: Danh mục
            IconPictureBox iconCategory = new IconPictureBox
            {
                IconChar = IconChar.FolderOpen,
                IconColor = Color.FromArgb(108, 117, 125),
                IconSize = 16,
                Location = new Point(70, 38),
                Size = new Size(16, 16)
            };

            Label lblCategory = new Label
            {
                Text = item.CategoryName,
                ForeColor = Color.FromArgb(108, 117, 125),
                Font = new Font("Segoe UI", 9F),
                Location = new Point(92, 37),
                AutoSize = true
            };

            // Dòng 2: Nhà cung cấp - LẤY TỪ ITEM
            IconPictureBox iconSupplier = new IconPictureBox
            {
                IconChar = IconChar.TruckField,
                IconColor = Color.FromArgb(0, 162, 173),
                IconSize = 16,
                Location = new Point(70, 63),
                Size = new Size(16, 16)
            };

            Label lblSupplier = new Label
            {
                Text = item.SupplierName ?? "N/A",  // LẤY TỪ ITEM, KHÔNG PHẢI tempData
                ForeColor = Color.FromArgb(0, 162, 173),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(92, 62),
                AutoSize = true
            };

            // Dòng 3: Số lượng
            IconPictureBox iconQuantity = new IconPictureBox
            {
                IconChar = IconChar.Box,
                IconColor = Color.FromArgb(40, 167, 69),
                IconSize = 16,
                Location = new Point(70, 88),
                Size = new Size(16, 16)
            };

            Label lblQuantity = new Label
            {
                Text = $"{item.Quantity:N0} {item.DonViTinh}",
                ForeColor = Color.FromArgb(40, 167, 69),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Location = new Point(92, 87),
                AutoSize = true
            };

            // Dòng 4: Hạn sử dụng
            IconPictureBox iconExpiry = new IconPictureBox
            {
                IconChar = IconChar.CalendarCheck,
                IconColor = Color.FromArgb(220, 53, 69),
                IconSize = 16,
                Location = new Point(70, 113),
                Size = new Size(16, 16)
            };

            DateTime expiryDate;
            if (DateTime.TryParse(item.HanSuDung, out expiryDate))
            {
                Label lblExpiry = new Label
                {
                    Text = $"HSD: {expiryDate:dd/MM/yyyy}",
                    ForeColor = Color.FromArgb(220, 53, 69),
                    Font = new Font("Segoe UI", 9F),
                    Location = new Point(92, 112),
                    AutoSize = true
                };
                cardPanel.Controls.Add(lblExpiry);
            }

            // Button Sửa
            IconButton btnEdit = new IconButton
            {
                Text = "",
                IconChar = IconChar.Edit,
                IconColor = Color.White,
                IconSize = 22,
                Size = new Size(45, 45),
                Location = new Point(cardWidth - 110, 50),
                BackColor = Color.FromArgb(52, 152, 219),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += (s, e) => OpenEditModal(item, flowItemsList.Controls.IndexOf(cardPanel));
            btnEdit.MouseEnter += (s, e) => btnEdit.BackColor = Color.FromArgb(41, 128, 185);
            btnEdit.MouseLeave += (s, e) => btnEdit.BackColor = Color.FromArgb(52, 152, 219);

            // Button Xóa
            IconButton btnDelete = new IconButton
            {
                Text = "",
                IconChar = IconChar.TrashAlt,
                IconColor = Color.White,
                IconSize = 22,
                Size = new Size(45, 45),
                Location = new Point(cardWidth - 55, 50),
                BackColor = Color.FromArgb(220, 53, 69),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += (s, e) =>
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa sản phẩm này khỏi danh sách?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    flowItemsList.Controls.Remove(cardPanel);

                    if (tempData != null && tempData.Items != null)
                    {
                        tempData.Items.Remove(item);
                        SaveTempDataToFile();
                    }

                    UpdateTongSoLuong();
                }
            };
            btnDelete.MouseEnter += (s, e) => btnDelete.BackColor = Color.FromArgb(192, 57, 43);
            btnDelete.MouseLeave += (s, e) => btnDelete.BackColor = Color.FromArgb(220, 53, 69);

            // Add all controls to card
            cardPanel.Controls.Add(iconProduct);
            cardPanel.Controls.Add(lblProductName);
            cardPanel.Controls.Add(iconCategory);
            cardPanel.Controls.Add(lblCategory);
            cardPanel.Controls.Add(iconSupplier);
            cardPanel.Controls.Add(lblSupplier);
            cardPanel.Controls.Add(iconQuantity);
            cardPanel.Controls.Add(lblQuantity);
            cardPanel.Controls.Add(iconExpiry);
            cardPanel.Controls.Add(btnEdit);
            cardPanel.Controls.Add(btnDelete);

            flowItemsList.Controls.Add(cardPanel);
        }
        private void OpenEditModal(TempImportItem item, int rowIndex)
        {
            using (FormEditImportItem modal = new FormEditImportItem(item, tempData.SupplierID, dbHelper))
            {
                if (modal.ShowDialog() == DialogResult.OK || modal.IsUpdated)
                {
                    TempImportItem updatedItem = modal.UpdatedItem;

                    // Cập nhật trong tempData
                    int itemIndex = tempData.Items.FindIndex(i =>
                        i.ProductID == item.ProductID &&
                        i.ProductName == item.ProductName);

                    if (itemIndex >= 0)
                    {
                        tempData.Items[itemIndex] = updatedItem;
                    }

                    // Reload lại TẤT CẢ cards với supplier hiện tại
                    flowItemsList.Controls.Clear();
                    foreach (var tempItem in tempData.Items)
                    {
                        AddItemCard(tempItem);
                    }

                    SaveTempDataToFile();
                    UpdateTongSoLuong();
                }
            }
        }

        #endregion
    }
}
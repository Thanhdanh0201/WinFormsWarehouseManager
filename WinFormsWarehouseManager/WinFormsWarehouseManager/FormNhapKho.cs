using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using WinFormsWarehouseManager.db;
using WinFormsWarehouseManager.Models;

namespace WinFormsWarehouseManager
{
    public partial class FormNhapKho : Form
    {
        private DatabaseHelper dbHelper;
        private bool isAddingNewSupplier = false;
        private bool isAddingNewProduct = false;

        // Lưu controls gốc để toggle
        private RJControls.RJComboBox cbbTenSP_Original;
        private RJControls.RJComboBox cbbDonViTinh_Original;
        private RJControls.RJTextBox txtTenSP_New;
        private RJControls.RJTextBox txtDonViTinh_New;

        public FormNhapKho()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            InitializeForm();
        }

        private void InitializeForm()
        {
            LoadNhaCungCap();
            LoadDanhMuc();
            StyleDataGridView();
            SetupDataGridView();
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

        private void LoadSanPhamTheoDanhMuc(int categoryID)
        {
            string query = @"SELECT ProductID, ProductName, DonViTinh, HanSuDung 
                           FROM Products 
                           WHERE CategoryID = @CategoryID 
                           ORDER BY ProductName";

            SQLiteParameter[] parameters = {
                new SQLiteParameter("@CategoryID", categoryID)
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

        // Helper method để lấy giá trị từ RJComboBox
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

        // Helper method để set giá trị cho RJComboBox
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

            // Hiển thị hạn tồn kho
            DataRowView drv = (DataRowView)cbbDanhMuc.SelectedItem;
            int hanTonKho = Convert.ToInt32(drv["HanTonKho_Thang"]);
            lblHanTonKho.Text = $"Hạn tồn kho mặc định: {hanTonKho} tháng";

            // Load sản phẩm theo danh mục (chỉ khi không ở chế độ thêm mới)
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

            // Fill đơn vị tính
            string donViTinh = drv["DonViTinh"].ToString();
            cbbDonViTinh.DataSource = new string[] { donViTinh };
            cbbDonViTinh.SelectedIndex = 0;

            // Fill hạn sử dụng
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

        private void btnNhaCungCapMoi_Click(object sender, EventArgs e)
        {
            if (!isAddingNewSupplier)
            {
                // Chuyển sang chế độ thêm mới
                isAddingNewSupplier = true;
                btnNhaCungCapMoi.Text = "Hủy";

                // Ẩn ComboBox, enable TextBoxes
                cbbNhaCungCap.Visible = false;
                txtTenNCC.Enabled = true;
                txtEmail.Enabled = true;
                txtSDT.Enabled = true;
                txtDiaChi.Enabled = true;

                // Clear textboxes
                txtTenNCC.Texts = "";
                txtEmail.Texts = "";
                txtSDT.Texts = "";
                txtDiaChi.Texts = "";
            }
            else
            {
                // Hủy - quay lại chế độ chọn
                CancelNewSupplier();
            }
        }

        private void CancelNewSupplier()
        {
            isAddingNewSupplier = false;
            btnNhaCungCapMoi.Text = "Nhà cung cấp mới";

            // Hiện ComboBox, disable TextBoxes
            cbbNhaCungCap.Visible = true;
            txtTenNCC.Enabled = false;
            txtEmail.Enabled = false;
            txtSDT.Enabled = false;
            txtDiaChi.Enabled = false;

            // Clear textboxes
            txtTenNCC.Texts = "";
            txtEmail.Texts = "";
            txtSDT.Texts = "";
            txtDiaChi.Texts = "";
        }

        #endregion

        #region Toggle New Product Mode

        private void btnSanPhamMoi_Click(object sender, EventArgs e)
        {
            if (!isAddingNewProduct)
            {
                // Kiểm tra đã chọn danh mục chưa
                if (cbbDanhMuc.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn danh mục trước!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chuyển sang chế độ thêm mới
                isAddingNewProduct = true;
                btnSanPhamMoi.Text = "Hủy";

                // Thay ComboBox bằng TextBox
                SwitchToNewProductMode();
            }
            else
            {
                // Hủy - quay lại chế độ chọn
                CancelNewProduct();
            }
        }

        private void SwitchToNewProductMode()
        {
            // Ẩn ComboBox Tên SP
            cbbTenSP.Visible = false;

            // Tạo và hiển thị TextBox Tên SP
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

            // Ẩn ComboBox Đơn vị tính
            cbbDonViTinh.Visible = false;

            // Tạo và hiển thị TextBox Đơn vị tính
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

            // Reset hạn sử dụng
            dtpHanSuDung.Value = DateTime.Now;
        }

        private void CancelNewProduct()
        {
            isAddingNewProduct = false;
            btnSanPhamMoi.Text = "Sản phẩm mới";

            // Ẩn TextBoxes
            if (txtTenSP_New != null) txtTenSP_New.Visible = false;
            if (txtDonViTinh_New != null) txtDonViTinh_New.Visible = false;

            // Hiện ComboBoxes
            cbbTenSP.Visible = true;
            cbbDonViTinh.Visible = true;

            // Reload sản phẩm theo danh mục
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

        #region Add to List

        private void btnThemVaoDanhSach_Click(object sender, EventArgs e)
        {
            // Validation
            if (!ValidateInput())
                return;

            int productID;
            string tenSP, danhMuc, donViTinh;

            if (isAddingNewProduct)
            {
                // Lưu sản phẩm mới vào DB và lấy ProductID
                productID = SaveNewProduct();
                if (productID == -1)
                    return;

                tenSP = txtTenSP_New.Texts.Trim();
                donViTinh = txtDonViTinh_New.Texts.Trim();
            }
            else
            {
                // Lấy thông tin sản phẩm từ ComboBox
                int? productIDNullable = GetComboBoxValue(cbbTenSP, "ProductID");
                if (!productIDNullable.HasValue)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                productID = productIDNullable.Value;
                tenSP = cbbTenSP.Text;
                donViTinh = cbbDonViTinh.Text;
            }

            danhMuc = cbbDanhMuc.Text;
            int soLuong = int.Parse(txtSoLuong.Texts);
            string hanSuDung = dtpHanSuDung.Value.ToString("yyyy-MM-dd");

            // Thêm vào DataGridView
            dgvDanhSachNhap.Rows.Add(tenSP, danhMuc, soLuong, donViTinh, hanSuDung, "Xóa");

            // Lưu ProductID vào Tag của row
            dgvDanhSachNhap.Rows[dgvDanhSachNhap.Rows.Count - 1].Tag = productID;

            // Cập nhật tổng số lượng
            UpdateTongSoLuong();

            // Reset form sản phẩm
            ResetProductPanel();
        }

        private bool ValidateInput()
        {
            // Kiểm tra nhà cung cấp
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

            // Kiểm tra danh mục
            if (cbbDanhMuc.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn danh mục!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra sản phẩm
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

            // Kiểm tra số lượng
            if (!int.TryParse(txtSoLuong.Texts, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private int SaveNewProduct()
        {
            string tenSP = txtTenSP_New.Texts.Trim();
            string donViTinh = txtDonViTinh_New.Texts.Trim();
            int? categoryIDNullable = GetComboBoxValue(cbbDanhMuc, "CategoryID");
            if (!categoryIDNullable.HasValue)
                return -1;

            int categoryID = categoryIDNullable.Value;
            string hanSuDung = dtpHanSuDung.Value.ToString("yyyy-MM-dd");

            // Lấy SupplierID
            int supplierID;
            if (isAddingNewSupplier)
            {
                supplierID = SaveNewSupplier();
                if (supplierID == -1)
                    return -1;
            }
            else
            {
                int? supplierIDNullable = GetComboBoxValue(cbbNhaCungCap, "SupplierID");
                if (!supplierIDNullable.HasValue)
                    return -1;

                supplierID = supplierIDNullable.Value;
            }

            // Insert sản phẩm mới (SoLuong = 0, sẽ được cộng qua trigger khi nhập kho)
            string query = @"INSERT INTO Products (ProductName, SoLuong, DonViTinh, HanSuDung, CategoryID, SupplierID)
                           VALUES (@ProductName, 0, @DonViTinh, @HanSuDung, @CategoryID, @SupplierID);
                           SELECT last_insert_rowid();";

            SQLiteParameter[] parameters = {
                new SQLiteParameter("@ProductName", tenSP),
                new SQLiteParameter("@DonViTinh", donViTinh),
                new SQLiteParameter("@HanSuDung", hanSuDung),
                new SQLiteParameter("@CategoryID", categoryID),
                new SQLiteParameter("@SupplierID", supplierID)
            };

            object result = dbHelper.ExecuteScalar(query, parameters);

            if (result != null)
            {
                int productID = Convert.ToInt32(result);

                // Reset chế độ thêm mới
                CancelNewProduct();

                return productID;
            }

            MessageBox.Show("Lỗi khi thêm sản phẩm mới!", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -1;
        }

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
                int supplierID = Convert.ToInt32(result);

                // Reload ComboBox và select NCC vừa tạo
                LoadNhaCungCap();
                SetComboBoxValue(cbbNhaCungCap, "SupplierID", supplierID);

                // Reset chế độ thêm mới
                CancelNewSupplier();

                return supplierID;
            }

            MessageBox.Show("Lỗi khi thêm nhà cung cấp mới!", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -1;
        }

        #endregion

        #region DataGridView

        private void SetupDataGridView()
        {
            dgvDanhSachNhap.Rows.Clear();
            UpdateTongSoLuong();
        }

        private void dgvDanhSachNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xử lý click button Xóa
            if (e.ColumnIndex == dgvDanhSachNhap.Columns["ColXoa"].Index && e.RowIndex >= 0)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa sản phẩm này khỏi danh sách?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    dgvDanhSachNhap.Rows.RemoveAt(e.RowIndex);
                    UpdateTongSoLuong();
                }
            }
        }

        private void UpdateTongSoLuong()
        {
            int tongSP = dgvDanhSachNhap.Rows.Count;
            lblTongSoLuong.Text = $"Tổng số lượng: {tongSP} SP";
        }

        #endregion

        #region Button Actions

        private void btnDongY_Click(object sender, EventArgs e)
        {
            // Validation
            if (dgvDanhSachNhap.Rows.Count == 0)
            {
                MessageBox.Show("Danh sách nhập trống! Vui lòng thêm sản phẩm.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận
            DialogResult result = MessageBox.Show(
                $"Xác nhận nhập {dgvDanhSachNhap.Rows.Count} sản phẩm vào kho?",
                "Xác nhận nhập kho",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            // Lưu từng dòng vào ImportReceipts
            bool success = SaveImportReceipts();

            if (success)
            {
                MessageBox.Show("Nhập kho thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }
        }

        private bool SaveImportReceipts()
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

                // Lấy SupplierID
                int supplierID;
                if (isAddingNewSupplier)
                {
                    // Nếu đang ở chế độ thêm NCC mới nhưng chưa lưu
                    MessageBox.Show("Vui lòng hoàn tất thêm nhà cung cấp trước!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    int? supplierIDNullable = GetComboBoxValue(cbbNhaCungCap, "SupplierID");
                    if (!supplierIDNullable.HasValue)
                    {
                        MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    supplierID = supplierIDNullable.Value;
                }

                string query = @"INSERT INTO ImportReceipts (ImportDate, UserID, SupplierID, ProductID, Quantity)
                               VALUES (datetime('now'), @UserID, @SupplierID, @ProductID, @Quantity)";

                foreach (DataGridViewRow row in dgvDanhSachNhap.Rows)
                {
                    int productID = Convert.ToInt32(row.Tag);
                    int quantity = Convert.ToInt32(row.Cells["ColSoLuong"].Value);

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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn hủy? Tất cả dữ liệu sẽ bị xóa.",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ResetForm();
            }
        }

        #endregion

        #region Helper Methods

        private void ResetForm()
        {
            // Reset Nhà cung cấp
            CancelNewSupplier();
            cbbNhaCungCap.SelectedIndex = -1;

            // Reset Sản phẩm
            CancelNewProduct();
            cbbDanhMuc.SelectedIndex = -1;
            cbbTenSP.DataSource = null;
            cbbDonViTinh.DataSource = null;
            txtSoLuong.Texts = "1";
            dtpHanSuDung.Value = DateTime.Now;
            lblHanTonKho.Text = "Hạn tồn kho mặc định: 0";

            // Reset DataGridView
            dgvDanhSachNhap.Rows.Clear();
            UpdateTongSoLuong();
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

        private void StyleDataGridView()
        {
            dgvDanhSachNhap.BorderStyle = BorderStyle.None;
            dgvDanhSachNhap.BackgroundColor = Color.White;
            dgvDanhSachNhap.GridColor = Color.FromArgb(240, 244, 247);
            dgvDanhSachNhap.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDanhSachNhap.AllowUserToResizeRows = false;
            dgvDanhSachNhap.RowHeadersVisible = false;
            dgvDanhSachNhap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSachNhap.MultiSelect = false;

            dgvDanhSachNhap.EnableHeadersVisualStyles = false;
            dgvDanhSachNhap.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(2, 51, 66);
            dgvDanhSachNhap.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDanhSachNhap.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvDanhSachNhap.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDanhSachNhap.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            dgvDanhSachNhap.ColumnHeadersHeight = 40;
            dgvDanhSachNhap.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvDanhSachNhap.DefaultCellStyle.BackColor = Color.White;
            dgvDanhSachNhap.DefaultCellStyle.ForeColor = Color.FromArgb(2, 51, 66);
            dgvDanhSachNhap.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvDanhSachNhap.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgvDanhSachNhap.DefaultCellStyle.SelectionForeColor = Color.FromArgb(2, 51, 66);
            dgvDanhSachNhap.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvDanhSachNhap.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Cho phép wrap text
            dgvDanhSachNhap.RowTemplate.Height = 45; // Tăng chiều cao để hiển thị đủ text

            dgvDanhSachNhap.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvDanhSachNhap.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(2, 51, 66);
            dgvDanhSachNhap.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgvDanhSachNhap.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(2, 51, 66);

            dgvDanhSachNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Điều chỉnh FillWeight cho các cột để hiển thị tên sản phẩm rõ hơn
            if (dgvDanhSachNhap.Columns.Count > 0)
            {
                dgvDanhSachNhap.Columns["ColTenSP"].FillWeight = 35; // 35% cho tên sản phẩm
                dgvDanhSachNhap.Columns["ColDanhMuc"].FillWeight = 25; // 25% cho danh mục
                dgvDanhSachNhap.Columns["ColSoLuong"].FillWeight = 10; // 10% cho số lượng
                dgvDanhSachNhap.Columns["ColDonViTinh"].FillWeight = 10; // 10% cho đơn vị tính
                dgvDanhSachNhap.Columns["ColHanSuDung"].FillWeight = 15; // 15% cho hạn SD
                dgvDanhSachNhap.Columns["ColXoa"].FillWeight = 5; // 5% cho nút xóa
            }
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

        private void FormNhapKho_Load(object sender, EventArgs e)
        {

        }
    }
}
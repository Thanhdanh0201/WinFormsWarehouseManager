using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using WinFormsWarehouseManager.db;
using WinFormsWarehouseManager.Models;

namespace WinFormsWarehouseManager
{
    public partial class FormXuatKho : Form
    {
        private DatabaseHelper dbHelper;
        private bool isAddingNewReceiver = false;
        private int currentStockQuantity = 0; // Số lượng tồn kho hiện tại

        public FormXuatKho()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            InitializeForm();
        }

        private void InitializeForm()
        {
            LoadNguoiNhan();
            LoadDanhMuc();
            StyleDataGridView();
            SetupDataGridView();
            ResetForm();
        }

        #region Load Data from Database

        private void LoadNguoiNhan()
        {
            string query = "SELECT ReceiverID, ReceiverName FROM Receivers ORDER BY ReceiverName";
            DataTable dt = dbHelper.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                cbbNguoiNhan.DataSource = dt;
                cbbNguoiNhan.DisplayMember = "ReceiverName";
                cbbNguoiNhan.ValueMember = "ReceiverID";
                cbbNguoiNhan.SelectedIndex = -1;
            }
        }

        private void LoadDanhMuc()
        {
            string query = "SELECT CategoryID, CategoryName FROM Categories ORDER BY CategoryName";
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
            string query = @"SELECT ProductID, ProductName, SoLuong 
                           FROM Products 
                           WHERE CategoryID = @CategoryID AND SoLuong > 0
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

        private void cbbNguoiNhan_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbNguoiNhan.SelectedItem == null || isAddingNewReceiver)
                return;

            int? receiverID = GetComboBoxValue(cbbNguoiNhan, "ReceiverID");
            if (receiverID.HasValue)
            {
                LoadReceiverInfo(receiverID.Value);
            }
        }

        private void LoadReceiverInfo(int receiverID)
        {
            string query = @"SELECT ReceiverName, Email, Phone, Address 
                           FROM Receivers 
                           WHERE ReceiverID = @ReceiverID";

            SQLiteParameter[] parameters = {
                new SQLiteParameter("@ReceiverID", receiverID)
            };

            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtTenNguoiNhan.Texts = row["ReceiverName"].ToString();
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

            LoadSanPhamTheoDanhMuc(categoryID.Value);

            // Reset thông tin sản phẩm
            cbbTenSP.SelectedIndex = -1;
            txtSoLuong.Texts = "";
            lblTonKho.Text = "Tồn kho hiện tại: 0";
            currentStockQuantity = 0;
        }

        private void cbbTenSP_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTenSP.SelectedItem == null)
                return;

            DataRowView drv = (DataRowView)cbbTenSP.SelectedItem;
            currentStockQuantity = Convert.ToInt32(drv["SoLuong"]);

            lblTonKho.Text = $"Tồn kho hiện tại: {currentStockQuantity}";
            lblTonKho.ForeColor = currentStockQuantity > 5
                ? Color.FromArgb(2, 51, 66)
                : Color.FromArgb(220, 53, 69);
        }

        #endregion

        #region Toggle New Receiver Mode

        private void btnNguoiNhanMoi_Click(object sender, EventArgs e)
        {
            if (!isAddingNewReceiver)
            {
                // Chuyển sang chế độ thêm mới
                isAddingNewReceiver = true;
                btnNguoiNhanMoi.Text = "Hủy";
                btnNguoiNhanMoi.IconChar = FontAwesome.Sharp.IconChar.Xmark;

                // Ẩn ComboBox, enable TextBoxes
                cbbNguoiNhan.Visible = false;
                txtTenNguoiNhan.Enabled = true;
                txtEmail.Enabled = true;
                txtSDT.Enabled = true;
                txtDiaChi.Enabled = true;

                // Clear textboxes
                txtTenNguoiNhan.Texts = "";
                txtEmail.Texts = "";
                txtSDT.Texts = "";
                txtDiaChi.Texts = "";
            }
            else
            {
                // Hủy - quay lại chế độ chọn
                CancelNewReceiver();
            }
        }

        private void CancelNewReceiver()
        {
            isAddingNewReceiver = false;
            btnNguoiNhanMoi.Text = "Người nhận mới";
            btnNguoiNhanMoi.IconChar = FontAwesome.Sharp.IconChar.Edit;

            // Hiện ComboBox, disable TextBoxes
            cbbNguoiNhan.Visible = true;
            txtTenNguoiNhan.Enabled = false;
            txtEmail.Enabled = false;
            txtSDT.Enabled = false;
            txtDiaChi.Enabled = false;

            // Clear textboxes
            txtTenNguoiNhan.Texts = "";
            txtEmail.Texts = "";
            txtSDT.Texts = "";
            txtDiaChi.Texts = "";
        }

        #endregion

        #region Add to List

        private void btnThemVaoDanhSach_Click(object sender, EventArgs e)
        {
            // Validation
            if (!ValidateInput())
                return;

            int? productIDNullable = GetComboBoxValue(cbbTenSP, "ProductID");
            if (!productIDNullable.HasValue)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productID = productIDNullable.Value;
            string tenSP = cbbTenSP.Text;
            string danhMuc = cbbDanhMuc.Text;
            int soLuong = int.Parse(txtSoLuong.Texts);

            // Kiểm tra số lượng xuất có vượt quá tồn kho không
            if (soLuong > currentStockQuantity)
            {
                MessageBox.Show($"Số lượng xuất vượt quá tồn kho!\nTồn kho hiện tại: {currentStockQuantity}",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra sản phẩm đã có trong danh sách chưa
            foreach (DataGridViewRow row in dgvDanhSachXuat.Rows)
            {
                if (Convert.ToInt32(row.Tag) == productID)
                {
                    MessageBox.Show("Sản phẩm đã có trong danh sách!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Thêm vào DataGridView
            dgvDanhSachXuat.Rows.Add(tenSP, danhMuc, soLuong, "Xóa");

            // Lưu ProductID vào Tag của row
            dgvDanhSachXuat.Rows[dgvDanhSachXuat.Rows.Count - 1].Tag = productID;

            // Cập nhật tổng số lượng
            UpdateTongSoLuong();

            // Reset form sản phẩm
            ResetProductPanel();
        }

        private bool ValidateInput()
        {
            // Kiểm tra người nhận
            if (!isAddingNewReceiver && cbbNguoiNhan.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn người nhận!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (isAddingNewReceiver)
            {
                if (string.IsNullOrWhiteSpace(txtTenNguoiNhan.Texts))
                {
                    MessageBox.Show("Vui lòng nhập tên người nhận!", "Thông báo",
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
            if (cbbTenSP.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
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

        private int SaveNewReceiver()
        {
            string tenNguoiNhan = txtTenNguoiNhan.Texts.Trim();
            string email = txtEmail.Texts.Trim();
            string sdt = txtSDT.Texts.Trim();
            string diaChi = txtDiaChi.Texts.Trim();

            string query = @"INSERT INTO Receivers (ReceiverName, Email, Phone, Address)
                           VALUES (@ReceiverName, @Email, @Phone, @Address);
                           SELECT last_insert_rowid();";

            SQLiteParameter[] parameters = {
                new SQLiteParameter("@ReceiverName", tenNguoiNhan),
                new SQLiteParameter("@Email", email),
                new SQLiteParameter("@Phone", sdt),
                new SQLiteParameter("@Address", diaChi)
            };

            object result = dbHelper.ExecuteScalar(query, parameters);

            if (result != null)
            {
                int receiverID = Convert.ToInt32(result);

                // Reload ComboBox và select người nhận vừa tạo
                LoadNguoiNhan();
                SetComboBoxValue(cbbNguoiNhan, "ReceiverID", receiverID);

                // Reset chế độ thêm mới
                CancelNewReceiver();

                return receiverID;
            }

            MessageBox.Show("Lỗi khi thêm người nhận mới!", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -1;
        }

        #endregion

        #region DataGridView

        private void SetupDataGridView()
        {
            dgvDanhSachXuat.Rows.Clear();
            UpdateTongSoLuong();
        }

        private void dgvDanhSachXuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Load thông tin khi click vào row
            if (e.RowIndex >= 0 && e.ColumnIndex != dgvDanhSachXuat.Columns["ColXoa"].Index)
            {
                DataGridViewRow row = dgvDanhSachXuat.Rows[e.RowIndex];

                // Load thông tin sản phẩm lên form
                string tenSP = row.Cells["ColTenSP"].Value.ToString();
                string danhMuc = row.Cells["ColDanhMuc"].Value.ToString();
                int soLuong = Convert.ToInt32(row.Cells["ColSoLuong"].Value);
                int productID = Convert.ToInt32(row.Tag);

                // Set danh mục
                for (int i = 0; i < cbbDanhMuc.Items.Count; i++)
                {
                    DataRowView drv = (DataRowView)cbbDanhMuc.Items[i];
                    if (drv["CategoryName"].ToString() == danhMuc)
                    {
                        cbbDanhMuc.SelectedIndex = i;
                        break;
                    }
                }

                // Set sản phẩm
                SetComboBoxValue(cbbTenSP, "ProductID", productID);

                // Set số lượng
                txtSoLuong.Texts = soLuong.ToString();
            }
        }

        private void dgvDanhSachXuat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xử lý click button Xóa
            if (e.ColumnIndex == dgvDanhSachXuat.Columns["ColXoa"].Index && e.RowIndex >= 0)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa sản phẩm này khỏi danh sách?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    dgvDanhSachXuat.Rows.RemoveAt(e.RowIndex);
                    UpdateTongSoLuong();
                }
            }
        }

        private void UpdateTongSoLuong()
        {
            int tongSP = dgvDanhSachXuat.Rows.Count;
            int tongSoLuong = 0;

            foreach (DataGridViewRow row in dgvDanhSachXuat.Rows)
            {
                tongSoLuong += Convert.ToInt32(row.Cells["ColSoLuong"].Value);
            }

            lblTongSoLuong.Text = $"Tổng số lượng: {tongSoLuong} ({tongSP} SP)";
        }

        #endregion

        #region Button Actions

        private void btnDongY_Click(object sender, EventArgs e)
        {
            // Validation
            if (dgvDanhSachXuat.Rows.Count == 0)
            {
                MessageBox.Show("Danh sách xuất trống! Vui lòng thêm sản phẩm.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận
            DialogResult result = MessageBox.Show(
                $"Xác nhận xuất {dgvDanhSachXuat.Rows.Count} sản phẩm khỏi kho?",
                "Xác nhận xuất kho",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            // Lưu từng dòng vào ExportReceipts
            bool success = SaveExportReceipts();

            if (success)
            {
                MessageBox.Show("Xuất kho thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }
        }

        private bool SaveExportReceipts()
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

                // Lấy ReceiverID
                int receiverID;
                if (isAddingNewReceiver)
                {
                    receiverID = SaveNewReceiver();
                    if (receiverID == -1)
                        return false;
                }
                else
                {
                    int? receiverIDNullable = GetComboBoxValue(cbbNguoiNhan, "ReceiverID");
                    if (!receiverIDNullable.HasValue)
                    {
                        MessageBox.Show("Vui lòng chọn người nhận!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    receiverID = receiverIDNullable.Value;
                }

                string query = @"INSERT INTO ExportReceipts (ExportDate, UserID, ReceiverID, ProductID, Quantity)
                               VALUES (datetime('now'), @UserID, @ReceiverID, @ProductID, @Quantity)";

                foreach (DataGridViewRow row in dgvDanhSachXuat.Rows)
                {
                    int productID = Convert.ToInt32(row.Tag);
                    int quantity = Convert.ToInt32(row.Cells["ColSoLuong"].Value);

                    SQLiteParameter[] parameters = {
                        new SQLiteParameter("@UserID", userID),
                        new SQLiteParameter("@ReceiverID", receiverID),
                        new SQLiteParameter("@ProductID", productID),
                        new SQLiteParameter("@Quantity", quantity)
                    };

                    int result = dbHelper.ExecuteNonQuery(query, parameters);

                    if (result <= 0)
                    {
                        MessageBox.Show("Lỗi khi lưu phiếu xuất!", "Lỗi",
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
            // Reset Người nhận
            CancelNewReceiver();
            cbbNguoiNhan.SelectedIndex = -1;

            // Reset Sản phẩm
            cbbDanhMuc.SelectedIndex = -1;
            cbbTenSP.DataSource = null;
            txtSoLuong.Texts = "";
            lblTonKho.Text = "Tồn kho hiện tại: 0";
            currentStockQuantity = 0;

            // Reset DataGridView
            dgvDanhSachXuat.Rows.Clear();
            UpdateTongSoLuong();
        }

        private void ResetProductPanel()
        {
            cbbTenSP.SelectedIndex = -1;
            txtSoLuong.Texts = "";
            lblTonKho.Text = "Tồn kho hiện tại: 0";
            currentStockQuantity = 0;
        }

        private void StyleDataGridView()
        {
            dgvDanhSachXuat.BorderStyle = BorderStyle.None;
            dgvDanhSachXuat.BackgroundColor = Color.White;
            dgvDanhSachXuat.GridColor = Color.FromArgb(240, 244, 247);
            dgvDanhSachXuat.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDanhSachXuat.AllowUserToResizeRows = false;
            dgvDanhSachXuat.RowHeadersVisible = false;
            dgvDanhSachXuat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSachXuat.MultiSelect = false;

            dgvDanhSachXuat.EnableHeadersVisualStyles = false;
            dgvDanhSachXuat.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(2, 51, 66);
            dgvDanhSachXuat.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDanhSachXuat.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvDanhSachXuat.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDanhSachXuat.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            dgvDanhSachXuat.ColumnHeadersHeight = 40;
            dgvDanhSachXuat.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvDanhSachXuat.DefaultCellStyle.BackColor = Color.White;
            dgvDanhSachXuat.DefaultCellStyle.ForeColor = Color.FromArgb(2, 51, 66);
            dgvDanhSachXuat.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvDanhSachXuat.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgvDanhSachXuat.DefaultCellStyle.SelectionForeColor = Color.FromArgb(2, 51, 66);
            dgvDanhSachXuat.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvDanhSachXuat.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDanhSachXuat.RowTemplate.Height = 50;

            dgvDanhSachXuat.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvDanhSachXuat.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(2, 51, 66);
            dgvDanhSachXuat.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgvDanhSachXuat.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(2, 51, 66);

            dgvDanhSachXuat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

        private void FormXuatKho_Load(object sender, EventArgs e)
        {

        }
    }
}
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
    public partial class FormXuatKho : Form
    {
        private DatabaseHelper dbHelper;
        private bool isAddingNewReceiver = false;
        private TempExportData tempData;

        // NEW: Controls được tạo lại hoàn toàn
        private FlowLayoutPanel flowItemsList;
        private Panel panelReceiverInfo;

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
            RebuildRightPanel(); // NEW: Rebuild UI
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

        /// <summary>
        /// Lấy tên người nhận từ database theo ReceiverID
        /// </summary>
        private string GetReceiverName(int receiverID)
        {
            if (receiverID <= 0)
                return "Chưa chọn người nhận";

            try
            {
                string query = "SELECT ReceiverName FROM Receivers WHERE ReceiverID = @ReceiverID";
                SQLiteParameter[] parameters = {
                    new SQLiteParameter("@ReceiverID", receiverID)
                };

                DataTable dt = dbHelper.ExecuteQuery(query, parameters);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["ReceiverName"].ToString();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting receiver name: {ex.Message}");
            }

            return "N/A";
        }

        private void LoadSanPhamTheoDanhMuc(int categoryID)
        {
            // Chỉ load sản phẩm còn hàng (SoLuong > 0)
            string query = @"SELECT ProductID, ProductName, DonViTinh, SoLuong 
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
                txtTenNN.Texts = row["ReceiverName"].ToString();
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

            // Reset tồn kho label
            lblTonKhoHienTai.Text = "Tồn kho hiện tại: 0";
            txtSoLuong.Texts = "";
        }

        private void cbbTenSP_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTenSP.SelectedItem == null)
                return;

            DataRowView drv = (DataRowView)cbbTenSP.SelectedItem;
            int soLuongTon = Convert.ToInt32(drv["SoLuong"]);
            string donViTinh = drv["DonViTinh"].ToString();

            lblTonKhoHienTai.Text = $"Tồn kho hiện tại: {soLuongTon} {donViTinh}";
            lblTonKhoHienTai.ForeColor = Color.FromArgb(40, 167, 69);

            txtSoLuong.Texts = "";
        }

        #endregion

        #region Toggle New Receiver Mode

        private FontAwesome.Sharp.IconButton btnLuuNN;

        private void btnNguoiNhanMoi_Click(object sender, EventArgs e)
        {
            if (!isAddingNewReceiver)
            {
                isAddingNewReceiver = true;
                btnNguoiNhanMoi.Text = "Hủy";

                cbbNguoiNhan.Visible = false;
                txtTenNN.Enabled = true;
                txtEmail.Enabled = true;
                txtSDT.Enabled = true;
                txtDiaChi.Enabled = true;

                txtTenNN.Texts = "";
                txtEmail.Texts = "";
                txtSDT.Texts = "";
                txtDiaChi.Texts = "";

                ShowSaveReceiverButton();
            }
            else
            {
                CancelNewReceiver();
            }
        }

        private void ShowSaveReceiverButton()
        {
            if (btnLuuNN == null)
            {
                btnLuuNN = new FontAwesome.Sharp.IconButton
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    BackColor = Color.FromArgb(220, 53, 69),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                    ForeColor = Color.White,
                    IconChar = IconChar.Save,
                    IconColor = Color.White,
                    IconFont = IconFont.Auto,
                    IconSize = 28,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    Location = new Point(590, 12),
                    Size = new Size(240, 50),
                    Text = "Lưu",
                    TextAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    UseVisualStyleBackColor = false,
                    Cursor = Cursors.Hand,
                    Padding = new Padding(7, 0, 0, 0)
                };
                btnLuuNN.FlatAppearance.BorderSize = 0;
                btnLuuNN.Click += BtnLuuNN_Click;

                btnLuuNN.MouseEnter += (s, e) => btnLuuNN.BackColor = Color.FromArgb(192, 57, 43);
                btnLuuNN.MouseLeave += (s, e) => btnLuuNN.BackColor = Color.FromArgb(220, 53, 69);

                panelNNTop.Controls.Add(btnLuuNN);
            }
            btnLuuNN.Visible = true;
            btnLuuNN.BringToFront();
        }

        private void BtnLuuNN_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenNN.Texts))
            {
                MessageBox.Show("Vui lòng nhập tên người nhận!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNN.Focus();
                return;
            }

            int receiverID = SaveNewReceiver();

            if (receiverID > 0)
            {
                MessageBox.Show($"Đã lưu người nhận: {txtTenNN.Texts.Trim()}", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadNguoiNhan();
                SetComboBoxValue(cbbNguoiNhan, "ReceiverID", receiverID);
                LoadReceiverInfo(receiverID);
                CancelNewReceiver();
            }
        }

        private void CancelNewReceiver()
        {
            isAddingNewReceiver = false;
            btnNguoiNhanMoi.Text = "Người nhận mới";

            cbbNguoiNhan.Visible = true;
            txtTenNN.Enabled = false;
            txtEmail.Enabled = false;
            txtSDT.Enabled = false;
            txtDiaChi.Enabled = false;

            if (btnLuuNN != null)
            {
                btnLuuNN.Visible = false;
            }
        }

        #endregion

        #region Save New Receiver

        private int SaveNewReceiver()
        {
            string tenNN = txtTenNN.Texts.Trim();
            string email = txtEmail.Texts.Trim();
            string sdt = txtSDT.Texts.Trim();
            string diaChi = txtDiaChi.Texts.Trim();

            string query = @"INSERT INTO Receivers (ReceiverName, Email, Phone, Address)
                           VALUES (@ReceiverName, @Email, @Phone, @Address);
                           SELECT last_insert_rowid();";

            SQLiteParameter[] parameters = {
                new SQLiteParameter("@ReceiverName", tenNN),
                new SQLiteParameter("@Email", email),
                new SQLiteParameter("@Phone", sdt),
                new SQLiteParameter("@Address", diaChi)
            };

            object result = dbHelper.ExecuteScalar(query, parameters);

            if (result != null)
            {
                return Convert.ToInt32(result);
            }

            MessageBox.Show("❌ Lỗi khi thêm người nhận mới!", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -1;
        }

        #endregion

        #region Add to Temp List

        private void btnThemVaoDanhSach_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            int receiverID = GetOrCreateReceiverID();
            if (receiverID == -1)
                return;

            TempExportItem newItem = CreateTempExportItem(receiverID);
            if (newItem == null)
                return;

            AddItemToList(newItem);
            SaveTempDataToFile();
            ResetProductPanel();
        }

        private bool ValidateInput()
        {
            if (!isAddingNewReceiver && cbbNguoiNhan.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn người nhận!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (isAddingNewReceiver)
            {
                if (string.IsNullOrWhiteSpace(txtTenNN.Texts))
                {
                    MessageBox.Show("Vui lòng nhập tên người nhận!", "Thông báo",
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

            if (cbbTenSP.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtSoLuong.Texts, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra tồn kho
            DataRowView drv = (DataRowView)cbbTenSP.SelectedItem;
            int soLuongTon = Convert.ToInt32(drv["SoLuong"]);

            if (soLuong > soLuongTon)
            {
                MessageBox.Show($"Số lượng xuất ({soLuong}) vượt quá tồn kho ({soLuongTon})!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private int GetOrCreateReceiverID()
        {
            if (isAddingNewReceiver)
            {
                int receiverID = SaveNewReceiver();
                if (receiverID == -1)
                    return -1;

                if (tempData == null)
                {
                    tempData = new TempExportData(receiverID, txtTenNN.Texts.Trim());
                }
                else
                {
                    tempData.ReceiverID = receiverID;
                    tempData.ReceiverName = txtTenNN.Texts.Trim();
                }

                return receiverID;
            }
            else
            {
                int? receiverID = GetComboBoxValue(cbbNguoiNhan, "ReceiverID");
                if (!receiverID.HasValue)
                    return -1;

                DataRowView drv = (DataRowView)cbbNguoiNhan.SelectedItem;
                string receiverName = drv["ReceiverName"].ToString();

                if (tempData == null)
                {
                    tempData = new TempExportData(receiverID.Value, receiverName);
                }
                else
                {
                    tempData.ReceiverID = receiverID.Value;
                    tempData.ReceiverName = receiverName;
                }

                return receiverID.Value;
            }
        }

        private TempExportItem CreateTempExportItem(int receiverID)
        {
            int? categoryID = GetComboBoxValue(cbbDanhMuc, "CategoryID");
            if (!categoryID.HasValue)
                return null;

            DataRowView drvCategory = (DataRowView)cbbDanhMuc.SelectedItem;
            string categoryName = drvCategory["CategoryName"].ToString();

            int? productID = GetComboBoxValue(cbbTenSP, "ProductID");
            if (!productID.HasValue)
                return null;

            DataRowView drvProduct = (DataRowView)cbbTenSP.SelectedItem;
            string productName = drvProduct["ProductName"].ToString();
            string donViTinh = drvProduct["DonViTinh"].ToString();
            int soLuongTon = Convert.ToInt32(drvProduct["SoLuong"]);

            int quantity = int.Parse(txtSoLuong.Texts);
            string receiverName = GetReceiverName(receiverID);

            return new TempExportItem(
                productID.Value,
                productName,
                categoryID.Value,
                categoryName,
                quantity,
                donViTinh,
                soLuongTon,
                receiverID,
                receiverName
            );
        }

        private void AddItemToList(TempExportItem item)
        {
            AddItemCard(item);
            tempData.Items.Add(item);
            UpdateTongSoLuong();
        }

        private void SaveTempDataToFile()
        {
            if (tempData != null && tempData.Items.Count > 0)
            {
                TempExportManager.Save(tempData);
            }
        }

        #endregion

        #region Button Actions

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (flowItemsList.Controls.Count == 0)
            {
                MessageBox.Show("Danh sách xuất trống! Vui lòng thêm sản phẩm.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Xác nhận xuất {flowItemsList.Controls.Count} sản phẩm ra khỏi kho?",
                "Xác nhận xuất kho",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            bool success = SaveToDatabase();

            if (success)
            {
                MessageBox.Show("Xuất kho thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                TempExportManager.Delete();
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

                foreach (TempExportItem item in tempData.Items)
                {
                    if (item == null)
                        continue;

                    bool insertResult = InsertExportReceipt(
                        userID,
                        item.ReceiverID,
                        item.ProductID,
                        item.Quantity
                    );

                    if (!insertResult)
                        return false;
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

        private bool InsertExportReceipt(int userID, int receiverID, int productID, int quantity)
        {
            string query = @"INSERT INTO ExportReceipts (ExportDate, UserID, ReceiverID, ProductID, Quantity)
                           VALUES (datetime('now'), @UserID, @ReceiverID, @ProductID, @Quantity)";

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
                TempExportManager.Delete();
                ResetForm();
            }
        }

        #endregion

        #region Helper Methods

        private void ResetForm()
        {
            CancelNewReceiver();
            cbbNguoiNhan.SelectedIndex = -1;

            cbbDanhMuc.SelectedIndex = -1;
            cbbTenSP.DataSource = null;
            txtSoLuong.Texts = "";
            lblTonKhoHienTai.Text = "Tồn kho hiện tại: 0";

            flowItemsList.Controls.Clear();
            UpdateTongSoLuong();

            tempData = null;
        }

        private void ResetProductPanel()
        {
            cbbTenSP.SelectedIndex = -1;
            txtSoLuong.Texts = "";
            lblTonKhoHienTai.Text = "Tồn kho hiện tại: 0";
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (cbbTenSP.SelectedIndex == -1)
                return;

            DataRowView drv = (DataRowView)cbbTenSP.SelectedItem;
            int soLuongTon = Convert.ToInt32(drv["SoLuong"]);
            string donViTinh = drv["DonViTinh"].ToString();

            if (int.TryParse(txtSoLuong.Texts, out int soLuongXuat) && soLuongXuat > 0)
            {
                int conLai = soLuongTon - soLuongXuat;
                lblTonKhoHienTai.Text = $"Tồn kho: {soLuongTon} {donViTinh} → Còn lại: {conLai} {donViTinh}";

                if (conLai < 0)
                {
                    lblTonKhoHienTai.ForeColor = Color.FromArgb(220, 53, 69);
                }
                else if (conLai < 5)
                {
                    lblTonKhoHienTai.ForeColor = Color.FromArgb(255, 193, 7);
                }
                else
                {
                    lblTonKhoHienTai.ForeColor = Color.FromArgb(40, 167, 69);
                }
            }
            else
            {
                lblTonKhoHienTai.Text = $"Tồn kho hiện tại: {soLuongTon} {donViTinh}";
                lblTonKhoHienTai.ForeColor = Color.FromArgb(40, 167, 69);
            }
        }

        #endregion

        private void FormXuatKho_Load(object sender, EventArgs e)
        {
            LoadTempDataIfExists();
        }

        private void LoadTempDataIfExists()
        {
            if (TempExportManager.Exists())
            {
                tempData = TempExportManager.Load();

                if (tempData != null)
                {
                    // Tự động load luôn, không cần xác nhận
                    foreach (var item in tempData.Items)
                    {
                        AddItemCard(item);
                    }
                    UpdateTongSoLuong();
                }
            }
        }
        

        private void btnLichSuXuatHang_Click(object sender, EventArgs e)
        {
            ExportHistoryModal modal = new ExportHistoryModal();
            modal.ShowDialog(this);
        }

        #region REBUILD RIGHT PANEL - CLEAN & RESPONSIVE

        private void RebuildRightPanel()
        {
            panelDanhSach.Controls.Clear();

            // 1. PANEL TOP
            panelDanhSachTop.Dock = DockStyle.Top;
            panelDanhSach.Controls.Add(panelDanhSachTop);

            // 2. PANEL BOTTOM
            panelDanhSachBottom.Dock = DockStyle.Bottom;
            panelDanhSach.Controls.Add(panelDanhSachBottom);

            // 3. PANEL RECEIVER INFO


            // 4. FLOW PANEL
            flowItemsList = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(15, 10, 15, 10)
            };

            flowItemsList.Resize += FlowItemsList_Resize;
            flowItemsList.ClientSizeChanged += FlowItemsList_Resize;

            panelDanhSach.Controls.Add(flowItemsList);

            panelDanhSachTop.BringToFront();
            flowItemsList.BringToFront();
            panelDanhSachBottom.BringToFront();
        }

        private void FlowItemsList_Resize(object sender, EventArgs e)
        {
            if (flowItemsList == null) return;

            int availableWidth = flowItemsList.ClientSize.Width - 30;

            foreach (Control ctrl in flowItemsList.Controls)
            {
                if (ctrl is Panel cardPanel)
                {
                    cardPanel.Width = availableWidth;

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

        


        private void UpdateTongSoLuong()
        {
            int tongSP = flowItemsList.Controls.Count;
            lblTongSoLuong.Text = $"Tổng số lượng: {tongSP} SP";
        }

        private void AddItemCard(TempExportItem item)
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
                IconChar = IconChar.BoxOpen,
                IconColor = Color.FromArgb(220, 53, 69),
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

            // Dòng 2: Người nhận
            IconPictureBox iconReceiver = new IconPictureBox
            {
                IconChar = IconChar.UserCheck,
                IconColor = Color.FromArgb(255, 193, 7),
                IconSize = 16,
                Location = new Point(70, 63),
                Size = new Size(16, 16)
            };

            Label lblReceiver = new Label
            {
                Text = item.ReceiverName ?? "N/A",
                ForeColor = Color.FromArgb(255, 193, 7),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(92, 62),
                AutoSize = true
            };

            // Dòng 3: Số lượng xuất
            IconPictureBox iconQuantity = new IconPictureBox
            {
                IconChar = IconChar.ArrowAltCircleRight,
                IconColor = Color.FromArgb(220, 53, 69),
                IconSize = 16,
                Location = new Point(70, 88),
                Size = new Size(16, 16)
            };

            Label lblQuantity = new Label
            {
                Text = $"Xuất: {item.Quantity:N0} {item.DonViTinh}",
                ForeColor = Color.FromArgb(220, 53, 69),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Location = new Point(92, 87),
                AutoSize = true
            };

            // Dòng 4: Tồn kho
            IconPictureBox iconStock = new IconPictureBox
            {
                IconChar = IconChar.Warehouse,
                IconColor = Color.FromArgb(40, 167, 69),
                IconSize = 16,
                Location = new Point(70, 113),
                Size = new Size(16, 16)
            };

            int conLai = item.SoLuongTonKho - item.Quantity;
            Color stockColor = conLai < 5 ? Color.FromArgb(220, 53, 69) : Color.FromArgb(40, 167, 69);

            Label lblStock = new Label
            {
                Text = $"Tồn kho: {item.SoLuongTonKho} → Còn: {conLai}",
                ForeColor = stockColor,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(92, 112),
                AutoSize = true
            };

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
            cardPanel.Controls.Add(iconReceiver);
            cardPanel.Controls.Add(lblReceiver);
            cardPanel.Controls.Add(iconQuantity);
            cardPanel.Controls.Add(lblQuantity);
            cardPanel.Controls.Add(iconStock);
            cardPanel.Controls.Add(lblStock);
            cardPanel.Controls.Add(btnEdit);
            cardPanel.Controls.Add(btnDelete);

            flowItemsList.Controls.Add(cardPanel);
        }

        private void OpenEditModal(TempExportItem item, int rowIndex)
        {
            using (FormEditExportItem modal = new FormEditExportItem(item, tempData.ReceiverID, dbHelper))
            {
                if (modal.ShowDialog() == DialogResult.OK || modal.IsUpdated)
                {
                    TempExportItem updatedItem = modal.UpdatedItem;

                    // Cập nhật trong tempData
                    int itemIndex = tempData.Items.FindIndex(i =>
                        i.ProductID == item.ProductID &&
                        i.ProductName == item.ProductName);

                    if (itemIndex >= 0)
                    {
                        tempData.Items[itemIndex] = updatedItem;
                    }

                    // Reload lại TẤT CẢ cards với receiver hiện tại
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
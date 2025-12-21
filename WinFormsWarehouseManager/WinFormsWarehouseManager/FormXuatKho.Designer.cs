using System.Windows.Forms;

namespace WinFormsWarehouseManager
{
    partial class FormXuatKho
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelDanhSach = new System.Windows.Forms.Panel();
            this.dgvDanhSachXuat = new System.Windows.Forms.DataGridView();
            this.ColTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDanhMuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColXoa = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panelDanhSachBottom = new System.Windows.Forms.Panel();
            this.btnHuy = new WinFormsWarehouseManager.RJControls.RJButton();
            this.btnDongY = new WinFormsWarehouseManager.RJControls.RJButton();
            this.lblTongSoLuong = new System.Windows.Forms.Label();
            this.panelDanhSachTop = new System.Windows.Forms.Panel();
            this.lblDanhSachTitle = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelSanPham = new System.Windows.Forms.Panel();
            this.panelSPBody = new System.Windows.Forms.Panel();
            this.lblTonKho = new System.Windows.Forms.Label();
            this.btnThemVaoDanhSach = new WinFormsWarehouseManager.RJControls.RJButton();
            this.txtSoLuong = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.cbbTenSP = new WinFormsWarehouseManager.RJControls.RJComboBox();
            this.cbbDanhMuc = new WinFormsWarehouseManager.RJControls.RJComboBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.lblDanhMuc = new System.Windows.Forms.Label();
            this.panelSPTop = new System.Windows.Forms.Panel();
            this.lblSanPhamTitle = new System.Windows.Forms.Label();
            this.panelNguoiNhan = new System.Windows.Forms.Panel();
            this.panelNguoiNhanBody = new System.Windows.Forms.Panel();
            this.txtDiaChi = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.txtSDT = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.txtEmail = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.txtTenNguoiNhan = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.cbbNguoiNhan = new WinFormsWarehouseManager.RJControls.RJComboBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblTenNguoiNhan = new System.Windows.Forms.Label();
            this.lblNguoiNhan = new System.Windows.Forms.Label();
            this.panelNguoiNhanTop = new System.Windows.Forms.Panel();
            this.btnNguoiNhanMoi = new FontAwesome.Sharp.IconButton();
            this.lblNguoiNhanTitle = new System.Windows.Forms.Label();
            this.panelContainer.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachXuat)).BeginInit();
            this.panelDanhSachBottom.SuspendLayout();
            this.panelDanhSachTop.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelSanPham.SuspendLayout();
            this.panelSPBody.SuspendLayout();
            this.panelSPTop.SuspendLayout();
            this.panelNguoiNhan.SuspendLayout();
            this.panelNguoiNhanBody.SuspendLayout();
            this.panelNguoiNhanTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelContainer.Controls.Add(this.panelRight);
            this.panelContainer.Controls.Add(this.panelLeft);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Padding = new System.Windows.Forms.Padding(20, 19, 20, 19);
            this.panelContainer.Size = new System.Drawing.Size(1867, 1000);
            this.panelContainer.TabIndex = 0;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.panelDanhSach);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(1140, 19);
            this.panelRight.Margin = new System.Windows.Forms.Padding(4);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.panelRight.Size = new System.Drawing.Size(707, 962);
            this.panelRight.TabIndex = 1;
            // 
            // panelDanhSach
            // 
            this.panelDanhSach.BackColor = System.Drawing.Color.White;
            this.panelDanhSach.Controls.Add(this.dgvDanhSachXuat);
            this.panelDanhSach.Controls.Add(this.panelDanhSachBottom);
            this.panelDanhSach.Controls.Add(this.panelDanhSachTop);
            this.panelDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDanhSach.Location = new System.Drawing.Point(13, 0);
            this.panelDanhSach.Margin = new System.Windows.Forms.Padding(4);
            this.panelDanhSach.Name = "panelDanhSach";
            this.panelDanhSach.Size = new System.Drawing.Size(694, 962);
            this.panelDanhSach.TabIndex = 0;
            // 
            // dgvDanhSachXuat
            // 
            this.dgvDanhSachXuat.AllowUserToAddRows = false;
            this.dgvDanhSachXuat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSachXuat.BackgroundColor = System.Drawing.Color.White;
            this.dgvDanhSachXuat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDanhSachXuat.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDanhSachXuat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDanhSachXuat.ColumnHeadersHeight = 40;
            this.dgvDanhSachXuat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDanhSachXuat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTenSP,
            this.ColDanhMuc,
            this.ColSoLuong,
            this.ColXoa});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDanhSachXuat.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDanhSachXuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSachXuat.EnableHeadersVisualStyles = false;
            this.dgvDanhSachXuat.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDanhSachXuat.Location = new System.Drawing.Point(0, 75);
            this.dgvDanhSachXuat.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDanhSachXuat.Name = "dgvDanhSachXuat";
            this.dgvDanhSachXuat.ReadOnly = true;
            this.dgvDanhSachXuat.RowHeadersVisible = false;
            this.dgvDanhSachXuat.RowHeadersWidth = 82;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDanhSachXuat.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDanhSachXuat.RowTemplate.Height = 50;
            this.dgvDanhSachXuat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachXuat.Size = new System.Drawing.Size(694, 737);
            this.dgvDanhSachXuat.TabIndex = 2;
            this.dgvDanhSachXuat.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachXuat_CellClick);
            this.dgvDanhSachXuat.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachXuat_CellContentClick);
            // 
            // ColTenSP
            // 
            this.ColTenSP.HeaderText = "Tên sản phẩm";
            this.ColTenSP.MinimumWidth = 10;
            this.ColTenSP.Name = "ColTenSP";
            this.ColTenSP.ReadOnly = true;
            this.ColTenSP.Width = 280;
            // 
            // ColDanhMuc
            // 
            this.ColDanhMuc.HeaderText = "Danh mục";
            this.ColDanhMuc.MinimumWidth = 10;
            this.ColDanhMuc.Name = "ColDanhMuc";
            this.ColDanhMuc.ReadOnly = true;
            this.ColDanhMuc.Width = 200;
            // 
            // ColSoLuong
            // 
            this.ColSoLuong.HeaderText = "Số lượng";
            this.ColSoLuong.MinimumWidth = 10;
            this.ColSoLuong.Name = "ColSoLuong";
            this.ColSoLuong.ReadOnly = true;
            this.ColSoLuong.Width = 100;
            // 
            // ColXoa
            // 
            this.ColXoa.HeaderText = "";
            this.ColXoa.MinimumWidth = 10;
            this.ColXoa.Name = "ColXoa";
            this.ColXoa.ReadOnly = true;
            this.ColXoa.Text = "Xóa";
            this.ColXoa.UseColumnTextForButtonValue = true;
            this.ColXoa.Width = 80;
            // 
            // panelDanhSachBottom
            // 
            this.panelDanhSachBottom.Controls.Add(this.btnHuy);
            this.panelDanhSachBottom.Controls.Add(this.btnDongY);
            this.panelDanhSachBottom.Controls.Add(this.lblTongSoLuong);
            this.panelDanhSachBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelDanhSachBottom.Location = new System.Drawing.Point(0, 812);
            this.panelDanhSachBottom.Margin = new System.Windows.Forms.Padding(4);
            this.panelDanhSachBottom.Name = "panelDanhSachBottom";
            this.panelDanhSachBottom.Padding = new System.Windows.Forms.Padding(27, 19, 27, 25);
            this.panelDanhSachBottom.Size = new System.Drawing.Size(694, 150);
            this.panelDanhSachBottom.TabIndex = 1;
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.btnHuy.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.btnHuy.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnHuy.BorderRadius = 10;
            this.btnHuy.BorderSize = 0;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(505, 72);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(4);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(160, 52);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.TextColor = System.Drawing.Color.White;
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDongY
            // 
            this.btnDongY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDongY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnDongY.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnDongY.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnDongY.BorderRadius = 10;
            this.btnDongY.BorderSize = 0;
            this.btnDongY.FlatAppearance.BorderSize = 0;
            this.btnDongY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDongY.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDongY.ForeColor = System.Drawing.Color.White;
            this.btnDongY.Location = new System.Drawing.Point(324, 72);
            this.btnDongY.Margin = new System.Windows.Forms.Padding(4);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(160, 52);
            this.btnDongY.TabIndex = 1;
            this.btnDongY.Text = "Đồng ý";
            this.btnDongY.TextColor = System.Drawing.Color.White;
            this.btnDongY.UseVisualStyleBackColor = false;
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // lblTongSoLuong
            // 
            this.lblTongSoLuong.AutoSize = true;
            this.lblTongSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongSoLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTongSoLuong.Location = new System.Drawing.Point(27, 25);
            this.lblTongSoLuong.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTongSoLuong.Name = "lblTongSoLuong";
            this.lblTongSoLuong.Size = new System.Drawing.Size(271, 37);
            this.lblTongSoLuong.TabIndex = 0;
            this.lblTongSoLuong.Text = "Tổng số lượng: 0 SP";
            // 
            // panelDanhSachTop
            // 
            this.panelDanhSachTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.panelDanhSachTop.Controls.Add(this.lblDanhSachTitle);
            this.panelDanhSachTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDanhSachTop.Location = new System.Drawing.Point(0, 0);
            this.panelDanhSachTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelDanhSachTop.Name = "panelDanhSachTop";
            this.panelDanhSachTop.Size = new System.Drawing.Size(694, 75);
            this.panelDanhSachTop.TabIndex = 0;
            // 
            // lblDanhSachTitle
            // 
            this.lblDanhSachTitle.AutoSize = true;
            this.lblDanhSachTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDanhSachTitle.ForeColor = System.Drawing.Color.White;
            this.lblDanhSachTitle.Location = new System.Drawing.Point(27, 19);
            this.lblDanhSachTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDanhSachTitle.Name = "lblDanhSachTitle";
            this.lblDanhSachTitle.Size = new System.Drawing.Size(399, 45);
            this.lblDanhSachTitle.TabIndex = 0;
            this.lblDanhSachTitle.Text = "Danh sách sản phẩm xuất";
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.panelSanPham);
            this.panelLeft.Controls.Add(this.panelNguoiNhan);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(20, 19);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(4);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(1120, 962);
            this.panelLeft.TabIndex = 0;
            // 
            // panelSanPham
            // 
            this.panelSanPham.BackColor = System.Drawing.Color.White;
            this.panelSanPham.Controls.Add(this.panelSPBody);
            this.panelSanPham.Controls.Add(this.panelSPTop);
            this.panelSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSanPham.Location = new System.Drawing.Point(0, 463);
            this.panelSanPham.Margin = new System.Windows.Forms.Padding(4, 13, 4, 4);
            this.panelSanPham.Name = "panelSanPham";
            this.panelSanPham.Size = new System.Drawing.Size(1120, 499);
            this.panelSanPham.TabIndex = 1;
            // 
            // panelSPBody
            // 
            this.panelSPBody.Controls.Add(this.lblTonKho);
            this.panelSPBody.Controls.Add(this.btnThemVaoDanhSach);
            this.panelSPBody.Controls.Add(this.txtSoLuong);
            this.panelSPBody.Controls.Add(this.cbbTenSP);
            this.panelSPBody.Controls.Add(this.cbbDanhMuc);
            this.panelSPBody.Controls.Add(this.lblSoLuong);
            this.panelSPBody.Controls.Add(this.lblTenSP);
            this.panelSPBody.Controls.Add(this.lblDanhMuc);
            this.panelSPBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSPBody.Location = new System.Drawing.Point(0, 75);
            this.panelSPBody.Margin = new System.Windows.Forms.Padding(4);
            this.panelSPBody.Name = "panelSPBody";
            this.panelSPBody.Padding = new System.Windows.Forms.Padding(40, 25, 40, 25);
            this.panelSPBody.Size = new System.Drawing.Size(1120, 424);
            this.panelSPBody.TabIndex = 1;
            // 
            // lblTonKho
            // 
            this.lblTonKho.AutoSize = true;
            this.lblTonKho.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTonKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTonKho.Location = new System.Drawing.Point(587, 202);
            this.lblTonKho.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTonKho.Name = "lblTonKho";
            this.lblTonKho.Size = new System.Drawing.Size(204, 32);
            this.lblTonKho.TabIndex = 8;
            this.lblTonKho.Text = "Tồn kho hiện tại: 0";
            // 
            // btnThemVaoDanhSach
            // 
            this.btnThemVaoDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemVaoDanhSach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnThemVaoDanhSach.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnThemVaoDanhSach.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnThemVaoDanhSach.BorderRadius = 10;
            this.btnThemVaoDanhSach.BorderSize = 0;
            this.btnThemVaoDanhSach.FlatAppearance.BorderSize = 0;
            this.btnThemVaoDanhSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemVaoDanhSach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemVaoDanhSach.ForeColor = System.Drawing.Color.White;
            this.btnThemVaoDanhSach.Location = new System.Drawing.Point(787, 340);
            this.btnThemVaoDanhSach.Margin = new System.Windows.Forms.Padding(4);
            this.btnThemVaoDanhSach.Name = "btnThemVaoDanhSach";
            this.btnThemVaoDanhSach.Size = new System.Drawing.Size(293, 56);
            this.btnThemVaoDanhSach.TabIndex = 7;
            this.btnThemVaoDanhSach.Text = "Thêm vào danh sách";
            this.btnThemVaoDanhSach.TextColor = System.Drawing.Color.White;
            this.btnThemVaoDanhSach.UseVisualStyleBackColor = false;
            this.btnThemVaoDanhSach.Click += new System.EventHandler(this.btnThemVaoDanhSach_Click);
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.BackColor = System.Drawing.SystemColors.Window;
            this.txtSoLuong.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.txtSoLuong.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(179)))), ((int)(((byte)(71)))));
            this.txtSoLuong.BorderRadius = 5;
            this.txtSoLuong.BorderSize = 2;
            this.txtSoLuong.ForeColor = System.Drawing.Color.DimGray;
            this.txtSoLuong.Location = new System.Drawing.Point(593, 238);
            this.txtSoLuong.Margin = new System.Windows.Forms.Padding(4);
            this.txtSoLuong.Multiline = false;
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Padding = new System.Windows.Forms.Padding(13, 9, 13, 9);
            this.txtSoLuong.PasswordChar = false;
            this.txtSoLuong.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtSoLuong.PlaceholderText = "Nhập số lượng xuất";
            this.txtSoLuong.Size = new System.Drawing.Size(487, 49);
            this.txtSoLuong.TabIndex = 6;
            this.txtSoLuong.Texts = "";
            this.txtSoLuong.UnderlinedStyle = false;
            this.txtSoLuong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoLuong_KeyPress);
            // 
            // cbbTenSP
            // 
            this.cbbTenSP.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbbTenSP.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cbbTenSP.BorderSize = 2;
            this.cbbTenSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTenSP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbbTenSP.ForeColor = System.Drawing.Color.DimGray;
            this.cbbTenSP.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cbbTenSP.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.cbbTenSP.ListTextColor = System.Drawing.Color.DimGray;
            this.cbbTenSP.Location = new System.Drawing.Point(593, 138);
            this.cbbTenSP.Margin = new System.Windows.Forms.Padding(4);
            this.cbbTenSP.MinimumSize = new System.Drawing.Size(267, 38);
            this.cbbTenSP.Name = "cbbTenSP";
            this.cbbTenSP.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbTenSP.Size = new System.Drawing.Size(487, 48);
            this.cbbTenSP.TabIndex = 5;
            this.cbbTenSP.Texts = "";
            this.cbbTenSP.OnSelectedIndexChanged += new System.EventHandler(this.cbbTenSP_OnSelectedIndexChanged);
            // 
            // cbbDanhMuc
            // 
            this.cbbDanhMuc.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbbDanhMuc.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cbbDanhMuc.BorderSize = 2;
            this.cbbDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbbDanhMuc.ForeColor = System.Drawing.Color.DimGray;
            this.cbbDanhMuc.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cbbDanhMuc.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.cbbDanhMuc.ListTextColor = System.Drawing.Color.DimGray;
            this.cbbDanhMuc.Location = new System.Drawing.Point(47, 138);
            this.cbbDanhMuc.Margin = new System.Windows.Forms.Padding(4);
            this.cbbDanhMuc.MinimumSize = new System.Drawing.Size(267, 38);
            this.cbbDanhMuc.Name = "cbbDanhMuc";
            this.cbbDanhMuc.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbDanhMuc.Size = new System.Drawing.Size(487, 48);
            this.cbbDanhMuc.TabIndex = 4;
            this.cbbDanhMuc.Texts = "";
            this.cbbDanhMuc.OnSelectedIndexChanged += new System.EventHandler(this.cbbDanhMuc_OnSelectedIndexChanged);
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblSoLuong.Location = new System.Drawing.Point(587, 201);
            this.lblSoLuong.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(151, 32);
            this.lblSoLuong.TabIndex = 2;
            this.lblSoLuong.Text = "Số lượng xuất";
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenSP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTenSP.Location = new System.Drawing.Point(587, 101);
            this.lblTenSP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(164, 32);
            this.lblTenSP.TabIndex = 1;
            this.lblTenSP.Text = "Tên sản phẩm";
            // 
            // lblDanhMuc
            // 
            this.lblDanhMuc.AutoSize = true;
            this.lblDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDanhMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblDanhMuc.Location = new System.Drawing.Point(40, 101);
            this.lblDanhMuc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDanhMuc.Name = "lblDanhMuc";
            this.lblDanhMuc.Size = new System.Drawing.Size(125, 32);
            this.lblDanhMuc.TabIndex = 0;
            this.lblDanhMuc.Text = "Danh mục";
            // 
            // panelSPTop
            // 
            this.panelSPTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.panelSPTop.Controls.Add(this.lblSanPhamTitle);
            this.panelSPTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSPTop.Location = new System.Drawing.Point(0, 0);
            this.panelSPTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelSPTop.Name = "panelSPTop";
            this.panelSPTop.Size = new System.Drawing.Size(1120, 75);
            this.panelSPTop.TabIndex = 0;
            // 
            // lblSanPhamTitle
            // 
            this.lblSanPhamTitle.AutoSize = true;
            this.lblSanPhamTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSanPhamTitle.ForeColor = System.Drawing.Color.White;
            this.lblSanPhamTitle.Location = new System.Drawing.Point(27, 19);
            this.lblSanPhamTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSanPhamTitle.Name = "lblSanPhamTitle";
            this.lblSanPhamTitle.Size = new System.Drawing.Size(319, 45);
            this.lblSanPhamTitle.TabIndex = 0;
            this.lblSanPhamTitle.Text = "Thông tin sản phẩm";
            // 
            // panelNguoiNhan
            // 
            this.panelNguoiNhan.BackColor = System.Drawing.Color.White;
            this.panelNguoiNhan.Controls.Add(this.panelNguoiNhanBody);
            this.panelNguoiNhan.Controls.Add(this.panelNguoiNhanTop);
            this.panelNguoiNhan.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNguoiNhan.Location = new System.Drawing.Point(0, 0);
            this.panelNguoiNhan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 13);
            this.panelNguoiNhan.Name = "panelNguoiNhan";
            this.panelNguoiNhan.Size = new System.Drawing.Size(1120, 450);
            this.panelNguoiNhan.TabIndex = 0;
            // 
            // panelNguoiNhanBody
            // 
            this.panelNguoiNhanBody.Controls.Add(this.txtDiaChi);
            this.panelNguoiNhanBody.Controls.Add(this.txtSDT);
            this.panelNguoiNhanBody.Controls.Add(this.txtEmail);
            this.panelNguoiNhanBody.Controls.Add(this.txtTenNguoiNhan);
            this.panelNguoiNhanBody.Controls.Add(this.cbbNguoiNhan);
            this.panelNguoiNhanBody.Controls.Add(this.lblDiaChi);
            this.panelNguoiNhanBody.Controls.Add(this.lblSDT);
            this.panelNguoiNhanBody.Controls.Add(this.lblEmail);
            this.panelNguoiNhanBody.Controls.Add(this.lblTenNguoiNhan);
            this.panelNguoiNhanBody.Controls.Add(this.lblNguoiNhan);
            this.panelNguoiNhanBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNguoiNhanBody.Location = new System.Drawing.Point(0, 75);
            this.panelNguoiNhanBody.Margin = new System.Windows.Forms.Padding(4);
            this.panelNguoiNhanBody.Name = "panelNguoiNhanBody";
            this.panelNguoiNhanBody.Padding = new System.Windows.Forms.Padding(40, 25, 40, 25);
            this.panelNguoiNhanBody.Size = new System.Drawing.Size(1120, 375);
            this.panelNguoiNhanBody.TabIndex = 1;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.BackColor = System.Drawing.SystemColors.Window;
            this.txtDiaChi.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.txtDiaChi.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(179)))), ((int)(((byte)(71)))));
            this.txtDiaChi.BorderRadius = 5;
            this.txtDiaChi.BorderSize = 2;
            this.txtDiaChi.Enabled = false;
            this.txtDiaChi.ForeColor = System.Drawing.Color.DimGray;
            this.txtDiaChi.Location = new System.Drawing.Point(593, 275);
            this.txtDiaChi.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiaChi.Multiline = false;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Padding = new System.Windows.Forms.Padding(13, 9, 13, 9);
            this.txtDiaChi.PasswordChar = false;
            this.txtDiaChi.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtDiaChi.PlaceholderText = "Địa chỉ";
            this.txtDiaChi.Size = new System.Drawing.Size(487, 49);
            this.txtDiaChi.TabIndex = 9;
            this.txtDiaChi.Texts = "";
            this.txtDiaChi.UnderlinedStyle = false;
            // 
            // txtSDT
            // 
            this.txtSDT.BackColor = System.Drawing.SystemColors.Window;
            this.txtSDT.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.txtSDT.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(179)))), ((int)(((byte)(71)))));
            this.txtSDT.BorderRadius = 5;
            this.txtSDT.BorderSize = 2;
            this.txtSDT.Enabled = false;
            this.txtSDT.ForeColor = System.Drawing.Color.DimGray;
            this.txtSDT.Location = new System.Drawing.Point(47, 275);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(4);
            this.txtSDT.Multiline = false;
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Padding = new System.Windows.Forms.Padding(13, 9, 13, 9);
            this.txtSDT.PasswordChar = false;
            this.txtSDT.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtSDT.PlaceholderText = "Số điện thoại";
            this.txtSDT.Size = new System.Drawing.Size(487, 49);
            this.txtSDT.TabIndex = 8;
            this.txtSDT.Texts = "";
            this.txtSDT.UnderlinedStyle = false;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.SystemColors.Window;
            this.txtEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.txtEmail.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(179)))), ((int)(((byte)(71)))));
            this.txtEmail.BorderRadius = 5;
            this.txtEmail.BorderSize = 2;
            this.txtEmail.Enabled = false;
            this.txtEmail.ForeColor = System.Drawing.Color.DimGray;
            this.txtEmail.Location = new System.Drawing.Point(593, 175);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Multiline = false;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Padding = new System.Windows.Forms.Padding(13, 9, 13, 9);
            this.txtEmail.PasswordChar = false;
            this.txtEmail.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtEmail.PlaceholderText = "Email";
            this.txtEmail.Size = new System.Drawing.Size(487, 49);
            this.txtEmail.TabIndex = 7;
            this.txtEmail.Texts = "";
            this.txtEmail.UnderlinedStyle = false;
            // 
            // txtTenNguoiNhan
            // 
            this.txtTenNguoiNhan.BackColor = System.Drawing.SystemColors.Window;
            this.txtTenNguoiNhan.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.txtTenNguoiNhan.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(179)))), ((int)(((byte)(71)))));
            this.txtTenNguoiNhan.BorderRadius = 5;
            this.txtTenNguoiNhan.BorderSize = 2;
            this.txtTenNguoiNhan.Enabled = false;
            this.txtTenNguoiNhan.ForeColor = System.Drawing.Color.DimGray;
            this.txtTenNguoiNhan.Location = new System.Drawing.Point(47, 175);
            this.txtTenNguoiNhan.Margin = new System.Windows.Forms.Padding(4);
            this.txtTenNguoiNhan.Multiline = false;
            this.txtTenNguoiNhan.Name = "txtTenNguoiNhan";
            this.txtTenNguoiNhan.Padding = new System.Windows.Forms.Padding(13, 9, 13, 9);
            this.txtTenNguoiNhan.PasswordChar = false;
            this.txtTenNguoiNhan.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtTenNguoiNhan.PlaceholderText = "Tên người nhận";
            this.txtTenNguoiNhan.Size = new System.Drawing.Size(487, 49);
            this.txtTenNguoiNhan.TabIndex = 6;
            this.txtTenNguoiNhan.Texts = "";
            this.txtTenNguoiNhan.UnderlinedStyle = false;
            // 
            // cbbNguoiNhan
            // 
            this.cbbNguoiNhan.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbbNguoiNhan.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cbbNguoiNhan.BorderSize = 2;
            this.cbbNguoiNhan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbNguoiNhan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbbNguoiNhan.ForeColor = System.Drawing.Color.DimGray;
            this.cbbNguoiNhan.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cbbNguoiNhan.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.cbbNguoiNhan.ListTextColor = System.Drawing.Color.DimGray;
            this.cbbNguoiNhan.Location = new System.Drawing.Point(47, 75);
            this.cbbNguoiNhan.Margin = new System.Windows.Forms.Padding(4);
            this.cbbNguoiNhan.MinimumSize = new System.Drawing.Size(267, 38);
            this.cbbNguoiNhan.Name = "cbbNguoiNhan";
            this.cbbNguoiNhan.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbNguoiNhan.Size = new System.Drawing.Size(487, 48);
            this.cbbNguoiNhan.TabIndex = 5;
            this.cbbNguoiNhan.Texts = "";
            this.cbbNguoiNhan.OnSelectedIndexChanged += new System.EventHandler(this.cbbNguoiNhan_OnSelectedIndexChanged);
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDiaChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblDiaChi.Location = new System.Drawing.Point(587, 239);
            this.lblDiaChi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(88, 32);
            this.lblDiaChi.TabIndex = 4;
            this.lblDiaChi.Text = "Địa chỉ";
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSDT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblSDT.Location = new System.Drawing.Point(40, 239);
            this.lblSDT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(157, 32);
            this.lblSDT.TabIndex = 3;
            this.lblSDT.Text = "Số điện thoại";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblEmail.Location = new System.Drawing.Point(587, 139);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(72, 32);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email";
            // 
            // lblTenNguoiNhan
            // 
            this.lblTenNguoiNhan.AutoSize = true;
            this.lblTenNguoiNhan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenNguoiNhan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTenNguoiNhan.Location = new System.Drawing.Point(40, 139);
            this.lblTenNguoiNhan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenNguoiNhan.Name = "lblTenNguoiNhan";
            this.lblTenNguoiNhan.Size = new System.Drawing.Size(179, 32);
            this.lblTenNguoiNhan.TabIndex = 1;
            this.lblTenNguoiNhan.Text = "Tên người nhận";
            // 
            // lblNguoiNhan
            // 
            this.lblNguoiNhan.AutoSize = true;
            this.lblNguoiNhan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNguoiNhan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNguoiNhan.Location = new System.Drawing.Point(40, 39);
            this.lblNguoiNhan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNguoiNhan.Name = "lblNguoiNhan";
            this.lblNguoiNhan.Size = new System.Drawing.Size(138, 32);
            this.lblNguoiNhan.TabIndex = 0;
            this.lblNguoiNhan.Text = "Người nhận";
            // 
            // panelNguoiNhanTop
            // 
            this.panelNguoiNhanTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.panelNguoiNhanTop.Controls.Add(this.btnNguoiNhanMoi);
            this.panelNguoiNhanTop.Controls.Add(this.lblNguoiNhanTitle);
            this.panelNguoiNhanTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNguoiNhanTop.Location = new System.Drawing.Point(0, 0);
            this.panelNguoiNhanTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelNguoiNhanTop.Name = "panelNguoiNhanTop";
            this.panelNguoiNhanTop.Size = new System.Drawing.Size(1120, 75);
            this.panelNguoiNhanTop.TabIndex = 0;
            // 
            // btnNguoiNhanMoi
            // 
            this.btnNguoiNhanMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNguoiNhanMoi.BackColor = System.Drawing.Color.Transparent;
            this.btnNguoiNhanMoi.FlatAppearance.BorderSize = 0;
            this.btnNguoiNhanMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNguoiNhanMoi.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnNguoiNhanMoi.ForeColor = System.Drawing.Color.White;
            this.btnNguoiNhanMoi.IconChar = FontAwesome.Sharp.IconChar.Edit;
            this.btnNguoiNhanMoi.IconColor = System.Drawing.Color.White;
            this.btnNguoiNhanMoi.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNguoiNhanMoi.IconSize = 28;
            this.btnNguoiNhanMoi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNguoiNhanMoi.Location = new System.Drawing.Point(867, 12);
            this.btnNguoiNhanMoi.Margin = new System.Windows.Forms.Padding(4);
            this.btnNguoiNhanMoi.Name = "btnNguoiNhanMoi";
            this.btnNguoiNhanMoi.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnNguoiNhanMoi.Size = new System.Drawing.Size(227, 50);
            this.btnNguoiNhanMoi.TabIndex = 1;
            this.btnNguoiNhanMoi.Text = "Người nhận mới";
            this.btnNguoiNhanMoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNguoiNhanMoi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNguoiNhanMoi.UseVisualStyleBackColor = false;
            this.btnNguoiNhanMoi.Click += new System.EventHandler(this.btnNguoiNhanMoi_Click);
            // 
            // lblNguoiNhanTitle
            // 
            this.lblNguoiNhanTitle.AutoSize = true;
            this.lblNguoiNhanTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNguoiNhanTitle.ForeColor = System.Drawing.Color.White;
            this.lblNguoiNhanTitle.Location = new System.Drawing.Point(27, 19);
            this.lblNguoiNhanTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNguoiNhanTitle.Name = "lblNguoiNhanTitle";
            this.lblNguoiNhanTitle.Size = new System.Drawing.Size(348, 45);
            this.lblNguoiNhanTitle.TabIndex = 0;
            this.lblNguoiNhanTitle.Text = "Thông tin người nhận";
            // 
            // FormXuatKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1867, 1000);
            this.Controls.Add(this.panelContainer);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormXuatKho";
            this.Text = "Xuất Kho";
            this.Load += new System.EventHandler(this.FormXuatKho_Load);
            this.panelContainer.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachXuat)).EndInit();
            this.panelDanhSachBottom.ResumeLayout(false);
            this.panelDanhSachBottom.PerformLayout();
            this.panelDanhSachTop.ResumeLayout(false);
            this.panelDanhSachTop.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelSanPham.ResumeLayout(false);
            this.panelSPBody.ResumeLayout(false);
            this.panelSPBody.PerformLayout();
            this.panelSPTop.ResumeLayout(false);
            this.panelSPTop.PerformLayout();
            this.panelNguoiNhan.ResumeLayout(false);
            this.panelNguoiNhanBody.ResumeLayout(false);
            this.panelNguoiNhanBody.PerformLayout();
            this.panelNguoiNhanTop.ResumeLayout(false);
            this.panelNguoiNhanTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panelRight;
        private Panel panelDanhSach;
        private System.Windows.Forms.DataGridView dgvDanhSachXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDanhMuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSoLuong;
        private System.Windows.Forms.DataGridViewButtonColumn ColXoa;
        private System.Windows.Forms.Panel panelDanhSachBottom;
        private RJControls.RJButton btnHuy;
        private RJControls.RJButton btnDongY;
        private System.Windows.Forms.Label lblTongSoLuong;
        private System.Windows.Forms.Panel panelDanhSachTop;
        private System.Windows.Forms.Label lblDanhSachTitle;
        private System.Windows.Forms.Panel panelLeft;
        private Panel panelSanPham;
        private System.Windows.Forms.Panel panelSPBody;
        private System.Windows.Forms.Label lblTonKho;
        private RJControls.RJButton btnThemVaoDanhSach;
        private RJControls.RJTextBox txtSoLuong;
        private RJControls.RJComboBox cbbTenSP;
        private RJControls.RJComboBox cbbDanhMuc;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.Label lblDanhMuc;
        private System.Windows.Forms.Panel panelSPTop;
        private System.Windows.Forms.Label lblSanPhamTitle;
        private Panel panelNguoiNhan;
        private System.Windows.Forms.Panel panelNguoiNhanBody;
        private RJControls.RJTextBox txtDiaChi;
        private RJControls.RJTextBox txtSDT;
        private RJControls.RJTextBox txtEmail;
        private RJControls.RJTextBox txtTenNguoiNhan;
        private RJControls.RJComboBox cbbNguoiNhan;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblTenNguoiNhan;
        private System.Windows.Forms.Label lblNguoiNhan;
        private System.Windows.Forms.Panel panelNguoiNhanTop;
        private FontAwesome.Sharp.IconButton btnNguoiNhanMoi;
        private System.Windows.Forms.Label lblNguoiNhanTitle;
    }
}
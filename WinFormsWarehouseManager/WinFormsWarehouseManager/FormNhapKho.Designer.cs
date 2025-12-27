using System.Windows.Forms;

namespace WinFormsWarehouseManager
{
    partial class FormNhapKho
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
            this.dgvDanhSachNhap = new System.Windows.Forms.DataGridView();
            this.ColTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDanhMuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColHanSuDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.lblHanTonKho = new System.Windows.Forms.Label();
            this.btnThemVaoDanhSach = new WinFormsWarehouseManager.RJControls.RJButton();
            this.dtpHanSuDung = new WinFormsWarehouseManager.RJControls.RJDateTimePicker();
            this.txtSoLuong = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.cbbDonViTinh = new WinFormsWarehouseManager.RJControls.RJComboBox();
            this.cbbTenSP = new WinFormsWarehouseManager.RJControls.RJComboBox();
            this.cbbDanhMuc = new WinFormsWarehouseManager.RJControls.RJComboBox();
            this.lblHanSuDung = new System.Windows.Forms.Label();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblDonViTinh = new System.Windows.Forms.Label();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.lblDanhMuc = new System.Windows.Forms.Label();
            this.panelSPTop = new System.Windows.Forms.Panel();
            this.btnSanPhamMoi = new FontAwesome.Sharp.IconButton();
            this.lblSanPhamTitle = new System.Windows.Forms.Label();
            this.panelNhaCungCap = new System.Windows.Forms.Panel();
            this.panelNCCBody = new System.Windows.Forms.Panel();
            this.txtDiaChi = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.txtSDT = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.txtEmail = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.txtTenNCC = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.cbbNhaCungCap = new WinFormsWarehouseManager.RJControls.RJComboBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblTenNCC = new System.Windows.Forms.Label();
            this.lblNhaCungCap = new System.Windows.Forms.Label();
            this.panelNCCTop = new System.Windows.Forms.Panel();
            this.btnNhaCungCapMoi = new FontAwesome.Sharp.IconButton();
            this.lblNCCTitle = new System.Windows.Forms.Label();
            this.panelContainer.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachNhap)).BeginInit();
            this.panelDanhSachBottom.SuspendLayout();
            this.panelDanhSachTop.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelSanPham.SuspendLayout();
            this.panelSPBody.SuspendLayout();
            this.panelSPTop.SuspendLayout();
            this.panelNhaCungCap.SuspendLayout();
            this.panelNCCBody.SuspendLayout();
            this.panelNCCTop.SuspendLayout();
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
            this.panelDanhSach.Controls.Add(this.dgvDanhSachNhap);
            this.panelDanhSach.Controls.Add(this.panelDanhSachBottom);
            this.panelDanhSach.Controls.Add(this.panelDanhSachTop);
            this.panelDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDanhSach.Location = new System.Drawing.Point(13, 0);
            this.panelDanhSach.Margin = new System.Windows.Forms.Padding(4);
            this.panelDanhSach.Name = "panelDanhSach";
            this.panelDanhSach.Size = new System.Drawing.Size(694, 962);
            this.panelDanhSach.TabIndex = 0;
            // 
            // dgvDanhSachNhap
            // 
            this.dgvDanhSachNhap.AllowUserToAddRows = false;
            this.dgvDanhSachNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSachNhap.BackgroundColor = System.Drawing.Color.White;
            this.dgvDanhSachNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDanhSachNhap.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDanhSachNhap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDanhSachNhap.ColumnHeadersHeight = 40;
            this.dgvDanhSachNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDanhSachNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTenSP,
            this.ColDanhMuc,
            this.ColSoLuong,
            this.ColDonViTinh,
            this.ColHanSuDung,
            this.ColXoa});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDanhSachNhap.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDanhSachNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSachNhap.EnableHeadersVisualStyles = false;
            this.dgvDanhSachNhap.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDanhSachNhap.Location = new System.Drawing.Point(0, 75);
            this.dgvDanhSachNhap.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDanhSachNhap.Name = "dgvDanhSachNhap";
            this.dgvDanhSachNhap.ReadOnly = true;
            this.dgvDanhSachNhap.RowHeadersVisible = false;
            this.dgvDanhSachNhap.RowHeadersWidth = 82;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDanhSachNhap.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDanhSachNhap.RowTemplate.Height = 50;
            this.dgvDanhSachNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachNhap.Size = new System.Drawing.Size(694, 737);
            this.dgvDanhSachNhap.TabIndex = 2;
            this.dgvDanhSachNhap.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachNhap_CellContentClick);
            // 
            // ColTenSP
            // 
            this.ColTenSP.HeaderText = "Tên SP";
            this.ColTenSP.MinimumWidth = 15;
            this.ColTenSP.Name = "ColTenSP";
            this.ColTenSP.ReadOnly = true;
            // 
            // ColDanhMuc
            // 
            this.ColDanhMuc.HeaderText = "Danh mục";
            this.ColDanhMuc.MinimumWidth = 10;
            this.ColDanhMuc.Name = "ColDanhMuc";
            this.ColDanhMuc.ReadOnly = true;
            // 
            // ColSoLuong
            // 
            this.ColSoLuong.HeaderText = "SL";
            this.ColSoLuong.MinimumWidth = 10;
            this.ColSoLuong.Name = "ColSoLuong";
            this.ColSoLuong.ReadOnly = true;
            // 
            // ColDonViTinh
            // 
            this.ColDonViTinh.HeaderText = "ĐVT";
            this.ColDonViTinh.MinimumWidth = 10;
            this.ColDonViTinh.Name = "ColDonViTinh";
            this.ColDonViTinh.ReadOnly = true;
            // 
            // ColHanSuDung
            // 
            this.ColHanSuDung.HeaderText = "Hạn SD";
            this.ColHanSuDung.MinimumWidth = 10;
            this.ColHanSuDung.Name = "ColHanSuDung";
            this.ColHanSuDung.ReadOnly = true;
            // 
            // ColXoa
            // 
            this.ColXoa.HeaderText = "";
            this.ColXoa.MinimumWidth = 10;
            this.ColXoa.Name = "ColXoa";
            this.ColXoa.ReadOnly = true;
            this.ColXoa.Text = "Xóa";
            this.ColXoa.UseColumnTextForButtonValue = true;
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
            this.btnDongY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.btnDongY.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
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
            this.panelDanhSachTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
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
            this.lblDanhSachTitle.Size = new System.Drawing.Size(410, 45);
            this.lblDanhSachTitle.TabIndex = 0;
            this.lblDanhSachTitle.Text = "Danh sách sản phẩm nhập";
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.panelSanPham);
            this.panelLeft.Controls.Add(this.panelNhaCungCap);
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
            this.panelSanPham.Location = new System.Drawing.Point(0, 450);
            this.panelSanPham.Margin = new System.Windows.Forms.Padding(4, 13, 4, 4);
            this.panelSanPham.Name = "panelSanPham";
            this.panelSanPham.Size = new System.Drawing.Size(1120, 512);
            this.panelSanPham.TabIndex = 1;
            // 
            // panelSPBody
            // 
            this.panelSPBody.Controls.Add(this.iconButton1);
            this.panelSPBody.Controls.Add(this.lblHanTonKho);
            this.panelSPBody.Controls.Add(this.btnThemVaoDanhSach);
            this.panelSPBody.Controls.Add(this.dtpHanSuDung);
            this.panelSPBody.Controls.Add(this.txtSoLuong);
            this.panelSPBody.Controls.Add(this.cbbDonViTinh);
            this.panelSPBody.Controls.Add(this.cbbTenSP);
            this.panelSPBody.Controls.Add(this.cbbDanhMuc);
            this.panelSPBody.Controls.Add(this.lblHanSuDung);
            this.panelSPBody.Controls.Add(this.lblSoLuong);
            this.panelSPBody.Controls.Add(this.lblDonViTinh);
            this.panelSPBody.Controls.Add(this.lblTenSP);
            this.panelSPBody.Controls.Add(this.lblDanhMuc);
            this.panelSPBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSPBody.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.panelSPBody.Location = new System.Drawing.Point(0, 75);
            this.panelSPBody.Margin = new System.Windows.Forms.Padding(4);
            this.panelSPBody.Name = "panelSPBody";
            this.panelSPBody.Padding = new System.Windows.Forms.Padding(40, 25, 40, 25);
            this.panelSPBody.Size = new System.Drawing.Size(1120, 437);
            this.panelSPBody.TabIndex = 1;
            // 
            // iconButton1
            // 
            this.iconButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.iconButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.iconButton1.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton1.ForeColor = System.Drawing.Color.White;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.History;
            this.iconButton1.IconColor = System.Drawing.Color.White;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.IconSize = 45;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.Location = new System.Drawing.Point(47, 353);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Size = new System.Drawing.Size(260, 56);
            this.iconButton1.TabIndex = 12;
            this.iconButton1.Text = "Lịch sử Nhập hàng";
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // lblHanTonKho
            // 
            this.lblHanTonKho.AutoSize = true;
            this.lblHanTonKho.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblHanTonKho.ForeColor = System.Drawing.Color.Gray;
            this.lblHanTonKho.Location = new System.Drawing.Point(587, 294);
            this.lblHanTonKho.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHanTonKho.Name = "lblHanTonKho";
            this.lblHanTonKho.Size = new System.Drawing.Size(249, 30);
            this.lblHanTonKho.TabIndex = 11;
            this.lblHanTonKho.Text = "Hạn tồn kho mặc định: 0";
            // 
            // btnThemVaoDanhSach
            // 
            this.btnThemVaoDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemVaoDanhSach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.btnThemVaoDanhSach.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.btnThemVaoDanhSach.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnThemVaoDanhSach.BorderRadius = 10;
            this.btnThemVaoDanhSach.BorderSize = 0;
            this.btnThemVaoDanhSach.FlatAppearance.BorderSize = 0;
            this.btnThemVaoDanhSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemVaoDanhSach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemVaoDanhSach.ForeColor = System.Drawing.Color.White;
            this.btnThemVaoDanhSach.Location = new System.Drawing.Point(787, 353);
            this.btnThemVaoDanhSach.Margin = new System.Windows.Forms.Padding(4);
            this.btnThemVaoDanhSach.Name = "btnThemVaoDanhSach";
            this.btnThemVaoDanhSach.Size = new System.Drawing.Size(293, 56);
            this.btnThemVaoDanhSach.TabIndex = 10;
            this.btnThemVaoDanhSach.Text = "Thêm vào danh sách";
            this.btnThemVaoDanhSach.TextColor = System.Drawing.Color.White;
            this.btnThemVaoDanhSach.UseVisualStyleBackColor = false;
            this.btnThemVaoDanhSach.Click += new System.EventHandler(this.btnThemVaoDanhSach_Click);
            // 
            // dtpHanSuDung
            // 
            this.dtpHanSuDung.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.dtpHanSuDung.BorderSize = 2;
            this.dtpHanSuDung.CalendarFont = new System.Drawing.Font("Segoe UI", 25F);
            this.dtpHanSuDung.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHanSuDung.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHanSuDung.Location = new System.Drawing.Point(593, 238);
            this.dtpHanSuDung.Margin = new System.Windows.Forms.Padding(4);
            this.dtpHanSuDung.MinimumSize = new System.Drawing.Size(485, 45);
            this.dtpHanSuDung.Name = "dtpHanSuDung";
            this.dtpHanSuDung.Size = new System.Drawing.Size(485, 45);
            this.dtpHanSuDung.SkinColor = System.Drawing.Color.White;
            this.dtpHanSuDung.TabIndex = 9;
            this.dtpHanSuDung.TextColor = System.Drawing.Color.DimGray;
            this.dtpHanSuDung.ValueChanged += new System.EventHandler(this.dtpHanSuDung_ValueChanged);
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.BackColor = System.Drawing.SystemColors.Window;
            this.txtSoLuong.BorderColor = System.Drawing.Color.Transparent;
            this.txtSoLuong.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.txtSoLuong.BorderRadius = 5;
            this.txtSoLuong.BorderSize = 2;
            this.txtSoLuong.ForeColor = System.Drawing.Color.DimGray;
            this.txtSoLuong.Location = new System.Drawing.Point(47, 238);
            this.txtSoLuong.Margin = new System.Windows.Forms.Padding(4);
            this.txtSoLuong.Multiline = false;
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Padding = new System.Windows.Forms.Padding(13, 9, 13, 9);
            this.txtSoLuong.PasswordChar = false;
            this.txtSoLuong.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtSoLuong.PlaceholderText = "Nhập số lượng";
            this.txtSoLuong.Size = new System.Drawing.Size(487, 61);
            this.txtSoLuong.TabIndex = 8;
            this.txtSoLuong.Texts = "";
            this.txtSoLuong.UnderlinedStyle = false;
            this.txtSoLuong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoLuong_KeyPress);
            // 
            // cbbDonViTinh
            // 
            this.cbbDonViTinh.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbbDonViTinh.BorderColor = System.Drawing.Color.Transparent;
            this.cbbDonViTinh.BorderSize = 2;
            this.cbbDonViTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDonViTinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbbDonViTinh.ForeColor = System.Drawing.Color.DimGray;
            this.cbbDonViTinh.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cbbDonViTinh.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.cbbDonViTinh.ListTextColor = System.Drawing.Color.DimGray;
            this.cbbDonViTinh.Location = new System.Drawing.Point(593, 138);
            this.cbbDonViTinh.Margin = new System.Windows.Forms.Padding(4);
            this.cbbDonViTinh.MinimumSize = new System.Drawing.Size(267, 38);
            this.cbbDonViTinh.Name = "cbbDonViTinh";
            this.cbbDonViTinh.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbDonViTinh.Size = new System.Drawing.Size(487, 48);
            this.cbbDonViTinh.TabIndex = 7;
            this.cbbDonViTinh.Texts = "";
            // 
            // cbbTenSP
            // 
            this.cbbTenSP.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbbTenSP.BorderColor = System.Drawing.Color.Transparent;
            this.cbbTenSP.BorderSize = 2;
            this.cbbTenSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTenSP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbbTenSP.ForeColor = System.Drawing.Color.DimGray;
            this.cbbTenSP.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cbbTenSP.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.cbbTenSP.ListTextColor = System.Drawing.Color.DimGray;
            this.cbbTenSP.Location = new System.Drawing.Point(47, 138);
            this.cbbTenSP.Margin = new System.Windows.Forms.Padding(4);
            this.cbbTenSP.MinimumSize = new System.Drawing.Size(267, 38);
            this.cbbTenSP.Name = "cbbTenSP";
            this.cbbTenSP.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbTenSP.Size = new System.Drawing.Size(487, 48);
            this.cbbTenSP.TabIndex = 6;
            this.cbbTenSP.Texts = "";
            this.cbbTenSP.OnSelectedIndexChanged += new System.EventHandler(this.cbbTenSP_OnSelectedIndexChanged);
            // 
            // cbbDanhMuc
            // 
            this.cbbDanhMuc.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbbDanhMuc.BorderColor = System.Drawing.Color.Transparent;
            this.cbbDanhMuc.BorderSize = 2;
            this.cbbDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbbDanhMuc.ForeColor = System.Drawing.Color.DimGray;
            this.cbbDanhMuc.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cbbDanhMuc.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.cbbDanhMuc.ListTextColor = System.Drawing.Color.DimGray;
            this.cbbDanhMuc.Location = new System.Drawing.Point(47, 38);
            this.cbbDanhMuc.Margin = new System.Windows.Forms.Padding(4);
            this.cbbDanhMuc.MinimumSize = new System.Drawing.Size(267, 38);
            this.cbbDanhMuc.Name = "cbbDanhMuc";
            this.cbbDanhMuc.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbDanhMuc.Size = new System.Drawing.Size(487, 48);
            this.cbbDanhMuc.TabIndex = 5;
            this.cbbDanhMuc.Texts = "";
            this.cbbDanhMuc.OnSelectedIndexChanged += new System.EventHandler(this.cbbDanhMuc_OnSelectedIndexChanged);
            // 
            // lblHanSuDung
            // 
            this.lblHanSuDung.AutoSize = true;
            this.lblHanSuDung.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.lblHanSuDung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblHanSuDung.Location = new System.Drawing.Point(587, 201);
            this.lblHanSuDung.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHanSuDung.Name = "lblHanSuDung";
            this.lblHanSuDung.Size = new System.Drawing.Size(168, 37);
            this.lblHanSuDung.TabIndex = 4;
            this.lblHanSuDung.Text = "Hạn sử dụng";
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.lblSoLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblSoLuong.Location = new System.Drawing.Point(40, 201);
            this.lblSoLuong.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(124, 37);
            this.lblSoLuong.TabIndex = 3;
            this.lblSoLuong.Text = "Số lượng";
            // 
            // lblDonViTinh
            // 
            this.lblDonViTinh.AutoSize = true;
            this.lblDonViTinh.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.lblDonViTinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblDonViTinh.Location = new System.Drawing.Point(587, 101);
            this.lblDonViTinh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDonViTinh.Name = "lblDonViTinh";
            this.lblDonViTinh.Size = new System.Drawing.Size(147, 37);
            this.lblDonViTinh.TabIndex = 2;
            this.lblDonViTinh.Text = "Đơn vị tính";
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.lblTenSP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTenSP.Location = new System.Drawing.Point(40, 101);
            this.lblTenSP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(179, 37);
            this.lblTenSP.TabIndex = 1;
            this.lblTenSP.Text = "Tên sản phẩm";
            // 
            // lblDanhMuc
            // 
            this.lblDanhMuc.AutoSize = true;
            this.lblDanhMuc.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.lblDanhMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblDanhMuc.Location = new System.Drawing.Point(40, 2);
            this.lblDanhMuc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDanhMuc.Name = "lblDanhMuc";
            this.lblDanhMuc.Size = new System.Drawing.Size(137, 37);
            this.lblDanhMuc.TabIndex = 0;
            this.lblDanhMuc.Text = "Danh mục";
            // 
            // panelSPTop
            // 
            this.panelSPTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.panelSPTop.Controls.Add(this.btnSanPhamMoi);
            this.panelSPTop.Controls.Add(this.lblSanPhamTitle);
            this.panelSPTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSPTop.Location = new System.Drawing.Point(0, 0);
            this.panelSPTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelSPTop.Name = "panelSPTop";
            this.panelSPTop.Size = new System.Drawing.Size(1120, 75);
            this.panelSPTop.TabIndex = 0;
            // 
            // btnSanPhamMoi
            // 
            this.btnSanPhamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSanPhamMoi.BackColor = System.Drawing.Color.Transparent;
            this.btnSanPhamMoi.FlatAppearance.BorderSize = 0;
            this.btnSanPhamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSanPhamMoi.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnSanPhamMoi.ForeColor = System.Drawing.Color.White;
            this.btnSanPhamMoi.IconChar = FontAwesome.Sharp.IconChar.Edit;
            this.btnSanPhamMoi.IconColor = System.Drawing.Color.White;
            this.btnSanPhamMoi.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSanPhamMoi.IconSize = 28;
            this.btnSanPhamMoi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSanPhamMoi.Location = new System.Drawing.Point(853, 12);
            this.btnSanPhamMoi.Margin = new System.Windows.Forms.Padding(4);
            this.btnSanPhamMoi.Name = "btnSanPhamMoi";
            this.btnSanPhamMoi.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnSanPhamMoi.Size = new System.Drawing.Size(240, 50);
            this.btnSanPhamMoi.TabIndex = 1;
            this.btnSanPhamMoi.Text = "Sản phẩm mới";
            this.btnSanPhamMoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSanPhamMoi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSanPhamMoi.UseVisualStyleBackColor = false;
            this.btnSanPhamMoi.Click += new System.EventHandler(this.btnSanPhamMoi_Click);
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
            // panelNhaCungCap
            // 
            this.panelNhaCungCap.BackColor = System.Drawing.Color.White;
            this.panelNhaCungCap.Controls.Add(this.panelNCCBody);
            this.panelNhaCungCap.Controls.Add(this.panelNCCTop);
            this.panelNhaCungCap.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNhaCungCap.Location = new System.Drawing.Point(0, 0);
            this.panelNhaCungCap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 13);
            this.panelNhaCungCap.Name = "panelNhaCungCap";
            this.panelNhaCungCap.Size = new System.Drawing.Size(1120, 450);
            this.panelNhaCungCap.TabIndex = 0;
            // 
            // panelNCCBody
            // 
            this.panelNCCBody.Controls.Add(this.txtDiaChi);
            this.panelNCCBody.Controls.Add(this.txtSDT);
            this.panelNCCBody.Controls.Add(this.txtEmail);
            this.panelNCCBody.Controls.Add(this.txtTenNCC);
            this.panelNCCBody.Controls.Add(this.cbbNhaCungCap);
            this.panelNCCBody.Controls.Add(this.lblDiaChi);
            this.panelNCCBody.Controls.Add(this.lblSDT);
            this.panelNCCBody.Controls.Add(this.lblEmail);
            this.panelNCCBody.Controls.Add(this.lblTenNCC);
            this.panelNCCBody.Controls.Add(this.lblNhaCungCap);
            this.panelNCCBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNCCBody.Location = new System.Drawing.Point(0, 75);
            this.panelNCCBody.Margin = new System.Windows.Forms.Padding(4);
            this.panelNCCBody.Name = "panelNCCBody";
            this.panelNCCBody.Padding = new System.Windows.Forms.Padding(40, 25, 40, 25);
            this.panelNCCBody.Size = new System.Drawing.Size(1120, 375);
            this.panelNCCBody.TabIndex = 1;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.BackColor = System.Drawing.SystemColors.Window;
            this.txtDiaChi.BorderColor = System.Drawing.Color.Transparent;
            this.txtDiaChi.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
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
            this.txtSDT.BorderColor = System.Drawing.Color.Transparent;
            this.txtSDT.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
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
            this.txtEmail.BorderColor = System.Drawing.Color.Transparent;
            this.txtEmail.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
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
            // txtTenNCC
            // 
            this.txtTenNCC.BackColor = System.Drawing.SystemColors.Window;
            this.txtTenNCC.BorderColor = System.Drawing.Color.Transparent;
            this.txtTenNCC.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.txtTenNCC.BorderRadius = 5;
            this.txtTenNCC.BorderSize = 2;
            this.txtTenNCC.Enabled = false;
            this.txtTenNCC.ForeColor = System.Drawing.Color.DimGray;
            this.txtTenNCC.Location = new System.Drawing.Point(47, 175);
            this.txtTenNCC.Margin = new System.Windows.Forms.Padding(4);
            this.txtTenNCC.Multiline = false;
            this.txtTenNCC.Name = "txtTenNCC";
            this.txtTenNCC.Padding = new System.Windows.Forms.Padding(13, 9, 13, 9);
            this.txtTenNCC.PasswordChar = false;
            this.txtTenNCC.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtTenNCC.PlaceholderText = "Tên nhà cung cấp";
            this.txtTenNCC.Size = new System.Drawing.Size(487, 49);
            this.txtTenNCC.TabIndex = 6;
            this.txtTenNCC.Texts = "";
            this.txtTenNCC.UnderlinedStyle = false;
            // 
            // cbbNhaCungCap
            // 
            this.cbbNhaCungCap.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbbNhaCungCap.BorderColor = System.Drawing.Color.Transparent;
            this.cbbNhaCungCap.BorderSize = 2;
            this.cbbNhaCungCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbNhaCungCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.cbbNhaCungCap.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cbbNhaCungCap.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.cbbNhaCungCap.ListTextColor = System.Drawing.Color.DimGray;
            this.cbbNhaCungCap.Location = new System.Drawing.Point(47, 75);
            this.cbbNhaCungCap.Margin = new System.Windows.Forms.Padding(4);
            this.cbbNhaCungCap.MinimumSize = new System.Drawing.Size(267, 38);
            this.cbbNhaCungCap.Name = "cbbNhaCungCap";
            this.cbbNhaCungCap.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbNhaCungCap.Size = new System.Drawing.Size(487, 48);
            this.cbbNhaCungCap.TabIndex = 5;
            this.cbbNhaCungCap.Texts = "";
            this.cbbNhaCungCap.OnSelectedIndexChanged += new System.EventHandler(this.cbbNhaCungCap_OnSelectedIndexChanged);
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.lblDiaChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblDiaChi.Location = new System.Drawing.Point(587, 239);
            this.lblDiaChi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(98, 37);
            this.lblDiaChi.TabIndex = 4;
            this.lblDiaChi.Text = "Địa chỉ";
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.lblSDT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblSDT.Location = new System.Drawing.Point(40, 239);
            this.lblSDT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(174, 37);
            this.lblSDT.TabIndex = 3;
            this.lblSDT.Text = "Số điện thoại";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblEmail.Location = new System.Drawing.Point(587, 139);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(82, 37);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email";
            // 
            // lblTenNCC
            // 
            this.lblTenNCC.AutoSize = true;
            this.lblTenNCC.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.lblTenNCC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTenNCC.Location = new System.Drawing.Point(40, 139);
            this.lblTenNCC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenNCC.Name = "lblTenNCC";
            this.lblTenNCC.Size = new System.Drawing.Size(222, 37);
            this.lblTenNCC.TabIndex = 1;
            this.lblTenNCC.Text = "Tên nhà cung cấp";
            // 
            // lblNhaCungCap
            // 
            this.lblNhaCungCap.AutoSize = true;
            this.lblNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.lblNhaCungCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNhaCungCap.Location = new System.Drawing.Point(40, 39);
            this.lblNhaCungCap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new System.Drawing.Size(180, 37);
            this.lblNhaCungCap.TabIndex = 0;
            this.lblNhaCungCap.Text = "Nhà cung cấp";
            // 
            // panelNCCTop
            // 
            this.panelNCCTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.panelNCCTop.Controls.Add(this.btnNhaCungCapMoi);
            this.panelNCCTop.Controls.Add(this.lblNCCTitle);
            this.panelNCCTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNCCTop.Location = new System.Drawing.Point(0, 0);
            this.panelNCCTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelNCCTop.Name = "panelNCCTop";
            this.panelNCCTop.Size = new System.Drawing.Size(1120, 75);
            this.panelNCCTop.TabIndex = 0;
            // 
            // btnNhaCungCapMoi
            // 
            this.btnNhaCungCapMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNhaCungCapMoi.BackColor = System.Drawing.Color.Transparent;
            this.btnNhaCungCapMoi.FlatAppearance.BorderSize = 0;
            this.btnNhaCungCapMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhaCungCapMoi.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnNhaCungCapMoi.ForeColor = System.Drawing.Color.White;
            this.btnNhaCungCapMoi.IconChar = FontAwesome.Sharp.IconChar.Edit;
            this.btnNhaCungCapMoi.IconColor = System.Drawing.Color.White;
            this.btnNhaCungCapMoi.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNhaCungCapMoi.IconSize = 28;
            this.btnNhaCungCapMoi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNhaCungCapMoi.Location = new System.Drawing.Point(827, 12);
            this.btnNhaCungCapMoi.Margin = new System.Windows.Forms.Padding(4);
            this.btnNhaCungCapMoi.Name = "btnNhaCungCapMoi";
            this.btnNhaCungCapMoi.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnNhaCungCapMoi.Size = new System.Drawing.Size(267, 50);
            this.btnNhaCungCapMoi.TabIndex = 1;
            this.btnNhaCungCapMoi.Text = "Nhà cung cấp mới";
            this.btnNhaCungCapMoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNhaCungCapMoi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhaCungCapMoi.UseVisualStyleBackColor = false;
            this.btnNhaCungCapMoi.Click += new System.EventHandler(this.btnNhaCungCapMoi_Click);
            // 
            // lblNCCTitle
            // 
            this.lblNCCTitle.AutoSize = true;
            this.lblNCCTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNCCTitle.ForeColor = System.Drawing.Color.White;
            this.lblNCCTitle.Location = new System.Drawing.Point(27, 19);
            this.lblNCCTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNCCTitle.Name = "lblNCCTitle";
            this.lblNCCTitle.Size = new System.Drawing.Size(373, 45);
            this.lblNCCTitle.TabIndex = 0;
            this.lblNCCTitle.Text = "Thông tin nhà cung cấp";
            // 
            // FormNhapKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1867, 1000);
            this.Controls.Add(this.panelContainer);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormNhapKho";
            this.Text = "Nhập Kho";
            this.Load += new System.EventHandler(this.FormNhapKho_Load);
            this.panelContainer.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachNhap)).EndInit();
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
            this.panelNhaCungCap.ResumeLayout(false);
            this.panelNCCBody.ResumeLayout(false);
            this.panelNCCBody.PerformLayout();
            this.panelNCCTop.ResumeLayout(false);
            this.panelNCCTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panelRight;
        private Panel panelDanhSach;
        private System.Windows.Forms.DataGridView dgvDanhSachNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDanhMuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColHanSuDung;
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
        private System.Windows.Forms.Label lblHanTonKho;
        private RJControls.RJButton btnThemVaoDanhSach;
        private RJControls.RJDateTimePicker dtpHanSuDung;
        private RJControls.RJTextBox txtSoLuong;
        private RJControls.RJComboBox cbbDonViTinh;
        private RJControls.RJComboBox cbbTenSP;
        private RJControls.RJComboBox cbbDanhMuc;
        private System.Windows.Forms.Label lblHanSuDung;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Label lblDonViTinh;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.Label lblDanhMuc;
        private System.Windows.Forms.Panel panelSPTop;
        private FontAwesome.Sharp.IconButton btnSanPhamMoi;
        private System.Windows.Forms.Label lblSanPhamTitle;
        private Panel panelNhaCungCap;
        private System.Windows.Forms.Panel panelNCCBody;
        private RJControls.RJTextBox txtDiaChi;
        private RJControls.RJTextBox txtSDT;
        private RJControls.RJTextBox txtEmail;
        private RJControls.RJTextBox txtTenNCC;
        private RJControls.RJComboBox cbbNhaCungCap;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblTenNCC;
        private System.Windows.Forms.Label lblNhaCungCap;
        private System.Windows.Forms.Panel panelNCCTop;
        private FontAwesome.Sharp.IconButton btnNhaCungCapMoi;
        private System.Windows.Forms.Label lblNCCTitle;
        private FontAwesome.Sharp.IconButton iconButton1;
    }
    
}
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
            this.panelDanhSachBottom = new System.Windows.Forms.Panel();
            this.btnHuy = new WinFormsWarehouseManager.RJControls.RJButton();
            this.btnDongY = new WinFormsWarehouseManager.RJControls.RJButton();
            this.lblTongSoLuong = new System.Windows.Forms.Label();
            this.panelDanhSachTop = new System.Windows.Forms.Panel();
            this.lblDanhSachTitle = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelSanPham = new System.Windows.Forms.Panel();
            this.panelSPBody = new System.Windows.Forms.Panel();
            this.btnLichSuXuatHang = new FontAwesome.Sharp.IconButton();
            this.lblTonKhoHienTai = new System.Windows.Forms.Label();
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
            this.panelNNBody = new System.Windows.Forms.Panel();
            this.txtDiaChi = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.txtSDT = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.txtEmail = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.txtTenNN = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.cbbNguoiNhan = new WinFormsWarehouseManager.RJControls.RJComboBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblTenNN = new System.Windows.Forms.Label();
            this.lblNguoiNhan = new System.Windows.Forms.Label();
            this.panelNNTop = new System.Windows.Forms.Panel();
            this.btnNguoiNhanMoi = new FontAwesome.Sharp.IconButton();
            this.lblNNTitle = new System.Windows.Forms.Label();
            this.panelContainer.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelDanhSach.SuspendLayout();
            this.panelDanhSachBottom.SuspendLayout();
            this.panelDanhSachTop.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelSanPham.SuspendLayout();
            this.panelSPBody.SuspendLayout();
            this.panelSPTop.SuspendLayout();
            this.panelNguoiNhan.SuspendLayout();
            this.panelNNBody.SuspendLayout();
            this.panelNNTop.SuspendLayout();
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
            this.panelDanhSach.Controls.Add(this.panelDanhSachBottom);
            this.panelDanhSach.Controls.Add(this.panelDanhSachTop);
            this.panelDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDanhSach.Location = new System.Drawing.Point(13, 0);
            this.panelDanhSach.Margin = new System.Windows.Forms.Padding(4);
            this.panelDanhSach.Name = "panelDanhSach";
            this.panelDanhSach.Size = new System.Drawing.Size(694, 962);
            this.panelDanhSach.TabIndex = 0;
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
            this.btnDongY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDongY.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
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
            this.panelDanhSachTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
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
            this.lblDanhSachTitle.Size = new System.Drawing.Size(406, 45);
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
            this.panelSanPham.Location = new System.Drawing.Point(0, 450);
            this.panelSanPham.Margin = new System.Windows.Forms.Padding(4, 13, 4, 4);
            this.panelSanPham.Name = "panelSanPham";
            this.panelSanPham.Size = new System.Drawing.Size(1120, 512);
            this.panelSanPham.TabIndex = 1;
            // 
            // panelSPBody
            // 
            this.panelSPBody.Controls.Add(this.btnLichSuXuatHang);
            this.panelSPBody.Controls.Add(this.lblTonKhoHienTai);
            this.panelSPBody.Controls.Add(this.btnThemVaoDanhSach);
            this.panelSPBody.Controls.Add(this.txtSoLuong);
            this.panelSPBody.Controls.Add(this.cbbTenSP);
            this.panelSPBody.Controls.Add(this.cbbDanhMuc);
            this.panelSPBody.Controls.Add(this.lblSoLuong);
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
            // btnLichSuXuatHang
            // 
            this.btnLichSuXuatHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLichSuXuatHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnLichSuXuatHang.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLichSuXuatHang.ForeColor = System.Drawing.Color.White;
            this.btnLichSuXuatHang.IconChar = FontAwesome.Sharp.IconChar.History;
            this.btnLichSuXuatHang.IconColor = System.Drawing.Color.White;
            this.btnLichSuXuatHang.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLichSuXuatHang.IconSize = 45;
            this.btnLichSuXuatHang.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLichSuXuatHang.Location = new System.Drawing.Point(47, 353);
            this.btnLichSuXuatHang.Name = "btnLichSuXuatHang";
            this.btnLichSuXuatHang.Size = new System.Drawing.Size(260, 56);
            this.btnLichSuXuatHang.TabIndex = 12;
            this.btnLichSuXuatHang.Text = "Lịch sử Xuất hàng";
            this.btnLichSuXuatHang.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLichSuXuatHang.UseVisualStyleBackColor = false;
            this.btnLichSuXuatHang.Click += new System.EventHandler(this.btnLichSuXuatHang_Click);
            // 
            // lblTonKhoHienTai
            // 
            this.lblTonKhoHienTai.AutoSize = true;
            this.lblTonKhoHienTai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTonKhoHienTai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblTonKhoHienTai.Location = new System.Drawing.Point(40, 294);
            this.lblTonKhoHienTai.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTonKhoHienTai.Name = "lblTonKhoHienTai";
            this.lblTonKhoHienTai.Size = new System.Drawing.Size(222, 32);
            this.lblTonKhoHienTai.TabIndex = 11;
            this.lblTonKhoHienTai.Text = "Tồn kho hiện tại: 0";
            // 
            // btnThemVaoDanhSach
            // 
            this.btnThemVaoDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemVaoDanhSach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnThemVaoDanhSach.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
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
            // txtSoLuong
            // 
            this.txtSoLuong.BackColor = System.Drawing.SystemColors.Window;
            this.txtSoLuong.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.txtSoLuong.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.txtSoLuong.BorderRadius = 5;
            this.txtSoLuong.BorderSize = 2;
            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.txtSoLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSoLuong.Location = new System.Drawing.Point(593, 138);
            this.txtSoLuong.Margin = new System.Windows.Forms.Padding(4);
            this.txtSoLuong.Multiline = false;
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Padding = new System.Windows.Forms.Padding(13, 9, 13, 9);
            this.txtSoLuong.PasswordChar = false;
            this.txtSoLuong.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtSoLuong.PlaceholderText = "Nhập số lượng xuất";
            this.txtSoLuong.Size = new System.Drawing.Size(487, 59);
            this.txtSoLuong.TabIndex = 8;
            this.txtSoLuong.Texts = "";
            this.txtSoLuong.UnderlinedStyle = false;
            this.txtSoLuong._TextChanged += new System.EventHandler(this.txtSoLuong_TextChanged);
            this.txtSoLuong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoLuong_KeyPress);
            // 
            // cbbTenSP
            // 
            this.cbbTenSP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.cbbTenSP.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.cbbTenSP.BorderRadius = 8;
            this.cbbTenSP.BorderSize = 2;
            this.cbbTenSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTenSP.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.cbbTenSP.ForeColor = System.Drawing.Color.White;
            this.cbbTenSP.IconColor = System.Drawing.Color.White;
            this.cbbTenSP.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.cbbTenSP.ListTextColor = System.Drawing.Color.White;
            this.cbbTenSP.Location = new System.Drawing.Point(593, 38);
            this.cbbTenSP.Margin = new System.Windows.Forms.Padding(4);
            this.cbbTenSP.MinimumSize = new System.Drawing.Size(267, 38);
            this.cbbTenSP.Name = "cbbTenSP";
            this.cbbTenSP.Padding = new System.Windows.Forms.Padding(2);
            this.cbbTenSP.Size = new System.Drawing.Size(487, 50);
            this.cbbTenSP.TabIndex = 6;
            this.cbbTenSP.Texts = "";
            this.cbbTenSP.OnSelectedIndexChanged += new System.EventHandler(this.cbbTenSP_OnSelectedIndexChanged);
            // 
            // cbbDanhMuc
            // 
            this.cbbDanhMuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.cbbDanhMuc.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.cbbDanhMuc.BorderRadius = 8;
            this.cbbDanhMuc.BorderSize = 2;
            this.cbbDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.cbbDanhMuc.ForeColor = System.Drawing.Color.White;
            this.cbbDanhMuc.IconColor = System.Drawing.Color.White;
            this.cbbDanhMuc.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.cbbDanhMuc.ListTextColor = System.Drawing.Color.White;
            this.cbbDanhMuc.Location = new System.Drawing.Point(47, 38);
            this.cbbDanhMuc.Margin = new System.Windows.Forms.Padding(4);
            this.cbbDanhMuc.MinimumSize = new System.Drawing.Size(267, 38);
            this.cbbDanhMuc.Name = "cbbDanhMuc";
            this.cbbDanhMuc.Padding = new System.Windows.Forms.Padding(2);
            this.cbbDanhMuc.Size = new System.Drawing.Size(487, 50);
            this.cbbDanhMuc.TabIndex = 5;
            this.cbbDanhMuc.Texts = "";
            this.cbbDanhMuc.OnSelectedIndexChanged += new System.EventHandler(this.cbbDanhMuc_OnSelectedIndexChanged);
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.lblSoLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblSoLuong.Location = new System.Drawing.Point(587, 101);
            this.lblSoLuong.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(175, 37);
            this.lblSoLuong.TabIndex = 3;
            this.lblSoLuong.Text = "Số lượng xuất";
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.lblTenSP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTenSP.Location = new System.Drawing.Point(587, 2);
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
            this.panelSPTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
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
            this.panelNguoiNhan.Controls.Add(this.panelNNBody);
            this.panelNguoiNhan.Controls.Add(this.panelNNTop);
            this.panelNguoiNhan.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNguoiNhan.Location = new System.Drawing.Point(0, 0);
            this.panelNguoiNhan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 13);
            this.panelNguoiNhan.Name = "panelNguoiNhan";
            this.panelNguoiNhan.Size = new System.Drawing.Size(1120, 450);
            this.panelNguoiNhan.TabIndex = 0;
            // 
            // panelNNBody
            // 
            this.panelNNBody.Controls.Add(this.txtDiaChi);
            this.panelNNBody.Controls.Add(this.txtSDT);
            this.panelNNBody.Controls.Add(this.txtEmail);
            this.panelNNBody.Controls.Add(this.txtTenNN);
            this.panelNNBody.Controls.Add(this.cbbNguoiNhan);
            this.panelNNBody.Controls.Add(this.lblDiaChi);
            this.panelNNBody.Controls.Add(this.lblSDT);
            this.panelNNBody.Controls.Add(this.lblEmail);
            this.panelNNBody.Controls.Add(this.lblTenNN);
            this.panelNNBody.Controls.Add(this.lblNguoiNhan);
            this.panelNNBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNNBody.Location = new System.Drawing.Point(0, 75);
            this.panelNNBody.Margin = new System.Windows.Forms.Padding(4);
            this.panelNNBody.Name = "panelNNBody";
            this.panelNNBody.Padding = new System.Windows.Forms.Padding(40, 25, 40, 25);
            this.panelNNBody.Size = new System.Drawing.Size(1120, 375);
            this.panelNNBody.TabIndex = 1;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.BackColor = System.Drawing.SystemColors.Window;
            this.txtDiaChi.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.txtDiaChi.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.txtDiaChi.BorderRadius = 5;
            this.txtDiaChi.BorderSize = 2;
            this.txtDiaChi.Enabled = false;
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiaChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDiaChi.Location = new System.Drawing.Point(593, 275);
            this.txtDiaChi.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiaChi.Multiline = false;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Padding = new System.Windows.Forms.Padding(13, 9, 13, 9);
            this.txtDiaChi.PasswordChar = false;
            this.txtDiaChi.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtDiaChi.PlaceholderText = "Địa chỉ";
            this.txtDiaChi.Size = new System.Drawing.Size(487, 59);
            this.txtDiaChi.TabIndex = 9;
            this.txtDiaChi.Texts = "";
            this.txtDiaChi.UnderlinedStyle = false;
            // 
            // txtSDT
            // 
            this.txtSDT.BackColor = System.Drawing.SystemColors.Window;
            this.txtSDT.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.txtSDT.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.txtSDT.BorderRadius = 5;
            this.txtSDT.BorderSize = 2;
            this.txtSDT.Enabled = false;
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSDT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSDT.Location = new System.Drawing.Point(47, 275);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(4);
            this.txtSDT.Multiline = false;
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Padding = new System.Windows.Forms.Padding(13, 9, 13, 9);
            this.txtSDT.PasswordChar = false;
            this.txtSDT.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtSDT.PlaceholderText = "Số điện thoại";
            this.txtSDT.Size = new System.Drawing.Size(487, 59);
            this.txtSDT.TabIndex = 8;
            this.txtSDT.Texts = "";
            this.txtSDT.UnderlinedStyle = false;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.SystemColors.Window;
            this.txtEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.txtEmail.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.txtEmail.BorderRadius = 5;
            this.txtEmail.BorderSize = 2;
            this.txtEmail.Enabled = false;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEmail.Location = new System.Drawing.Point(593, 175);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Multiline = false;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Padding = new System.Windows.Forms.Padding(13, 9, 13, 9);
            this.txtEmail.PasswordChar = false;
            this.txtEmail.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtEmail.PlaceholderText = "Email";
            this.txtEmail.Size = new System.Drawing.Size(487, 59);
            this.txtEmail.TabIndex = 7;
            this.txtEmail.Texts = "";
            this.txtEmail.UnderlinedStyle = false;
            // 
            // txtTenNN
            // 
            this.txtTenNN.BackColor = System.Drawing.SystemColors.Window;
            this.txtTenNN.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.txtTenNN.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.txtTenNN.BorderRadius = 5;
            this.txtTenNN.BorderSize = 2;
            this.txtTenNN.Enabled = false;
            this.txtTenNN.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenNN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTenNN.Location = new System.Drawing.Point(47, 175);
            this.txtTenNN.Margin = new System.Windows.Forms.Padding(4);
            this.txtTenNN.Multiline = false;
            this.txtTenNN.Name = "txtTenNN";
            this.txtTenNN.Padding = new System.Windows.Forms.Padding(13, 9, 13, 9);
            this.txtTenNN.PasswordChar = false;
            this.txtTenNN.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtTenNN.PlaceholderText = "Tên người nhận";
            this.txtTenNN.Size = new System.Drawing.Size(487, 59);
            this.txtTenNN.TabIndex = 6;
            this.txtTenNN.Texts = "";
            this.txtTenNN.UnderlinedStyle = false;
            // 
            // cbbNguoiNhan
            // 
            this.cbbNguoiNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.cbbNguoiNhan.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.cbbNguoiNhan.BorderRadius = 8;
            this.cbbNguoiNhan.BorderSize = 2;
            this.cbbNguoiNhan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbNguoiNhan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cbbNguoiNhan.ForeColor = System.Drawing.Color.White;
            this.cbbNguoiNhan.IconColor = System.Drawing.Color.White;
            this.cbbNguoiNhan.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.cbbNguoiNhan.ListTextColor = System.Drawing.Color.White;
            this.cbbNguoiNhan.Location = new System.Drawing.Point(47, 75);
            this.cbbNguoiNhan.Margin = new System.Windows.Forms.Padding(4);
            this.cbbNguoiNhan.MinimumSize = new System.Drawing.Size(267, 38);
            this.cbbNguoiNhan.Name = "cbbNguoiNhan";
            this.cbbNguoiNhan.Padding = new System.Windows.Forms.Padding(2);
            this.cbbNguoiNhan.Size = new System.Drawing.Size(487, 50);
            this.cbbNguoiNhan.TabIndex = 5;
            this.cbbNguoiNhan.Texts = "";
            this.cbbNguoiNhan.OnSelectedIndexChanged += new System.EventHandler(this.cbbNguoiNhan_OnSelectedIndexChanged);
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
            // lblTenNN
            // 
            this.lblTenNN.AutoSize = true;
            this.lblTenNN.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.lblTenNN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTenNN.Location = new System.Drawing.Point(40, 139);
            this.lblTenNN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenNN.Name = "lblTenNN";
            this.lblTenNN.Size = new System.Drawing.Size(192, 37);
            this.lblTenNN.TabIndex = 1;
            this.lblTenNN.Text = "Tên người nhận";
            // 
            // lblNguoiNhan
            // 
            this.lblNguoiNhan.AutoSize = true;
            this.lblNguoiNhan.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.lblNguoiNhan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNguoiNhan.Location = new System.Drawing.Point(40, 39);
            this.lblNguoiNhan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNguoiNhan.Name = "lblNguoiNhan";
            this.lblNguoiNhan.Size = new System.Drawing.Size(155, 37);
            this.lblNguoiNhan.TabIndex = 0;
            this.lblNguoiNhan.Text = "Người nhận";
            // 
            // panelNNTop
            // 
            this.panelNNTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.panelNNTop.Controls.Add(this.btnNguoiNhanMoi);
            this.panelNNTop.Controls.Add(this.lblNNTitle);
            this.panelNNTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNNTop.Location = new System.Drawing.Point(0, 0);
            this.panelNNTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelNNTop.Name = "panelNNTop";
            this.panelNNTop.Size = new System.Drawing.Size(1120, 75);
            this.panelNNTop.TabIndex = 0;
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
            this.btnNguoiNhanMoi.Location = new System.Drawing.Point(857, 12);
            this.btnNguoiNhanMoi.Margin = new System.Windows.Forms.Padding(4);
            this.btnNguoiNhanMoi.Name = "btnNguoiNhanMoi";
            this.btnNguoiNhanMoi.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnNguoiNhanMoi.Size = new System.Drawing.Size(237, 50);
            this.btnNguoiNhanMoi.TabIndex = 1;
            this.btnNguoiNhanMoi.Text = "Người nhận mới";
            this.btnNguoiNhanMoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNguoiNhanMoi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNguoiNhanMoi.UseVisualStyleBackColor = false;
            this.btnNguoiNhanMoi.Click += new System.EventHandler(this.btnNguoiNhanMoi_Click);
            // 
            // lblNNTitle
            // 
            this.lblNNTitle.AutoSize = true;
            this.lblNNTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNNTitle.ForeColor = System.Drawing.Color.White;
            this.lblNNTitle.Location = new System.Drawing.Point(27, 19);
            this.lblNNTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNNTitle.Name = "lblNNTitle";
            this.lblNNTitle.Size = new System.Drawing.Size(348, 45);
            this.lblNNTitle.TabIndex = 0;
            this.lblNNTitle.Text = "Thông tin người nhận";
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
            this.panelNNBody.ResumeLayout(false);
            this.panelNNBody.PerformLayout();
            this.panelNNTop.ResumeLayout(false);
            this.panelNNTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panelRight;
        private Panel panelDanhSach;
        private System.Windows.Forms.Panel panelDanhSachBottom;
        private RJControls.RJButton btnHuy;
        private RJControls.RJButton btnDongY;
        private System.Windows.Forms.Label lblTongSoLuong;
        private System.Windows.Forms.Panel panelDanhSachTop;
        private System.Windows.Forms.Label lblDanhSachTitle;
        private System.Windows.Forms.Panel panelLeft;
        private Panel panelSanPham;
        private System.Windows.Forms.Panel panelSPBody;
        private System.Windows.Forms.Label lblTonKhoHienTai;
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
        private System.Windows.Forms.Panel panelNNBody;
        private RJControls.RJTextBox txtDiaChi;
        private RJControls.RJTextBox txtSDT;
        private RJControls.RJTextBox txtEmail;
        private RJControls.RJTextBox txtTenNN;
        private RJControls.RJComboBox cbbNguoiNhan;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblTenNN;
        private System.Windows.Forms.Label lblNguoiNhan;
        private System.Windows.Forms.Panel panelNNTop;
        private FontAwesome.Sharp.IconButton btnNguoiNhanMoi;
        private System.Windows.Forms.Label lblNNTitle;
        private FontAwesome.Sharp.IconButton btnLichSuXuatHang;
    }
}
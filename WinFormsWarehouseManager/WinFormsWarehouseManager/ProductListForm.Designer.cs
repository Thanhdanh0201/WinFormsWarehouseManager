namespace WinFormsWarehouseManager.Forms
{
    partial class FormProductList
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
            this.panelFilter = new System.Windows.Forms.Panel();
            this.btnSearch = new FontAwesome.Sharp.IconButton();
            this.txtSearch = new WinFormsWarehouseManager.RJControls.RJTextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cboSort = new WinFormsWarehouseManager.RJControls.RJComboBox();
            this.lblSort = new System.Windows.Forms.Label();
            this.cboCategory = new WinFormsWarehouseManager.RJControls.RJComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnNhapKho = new FontAwesome.Sharp.IconButton();
            this.btnXuatKho = new FontAwesome.Sharp.IconButton();
            this.btnDelete = new FontAwesome.Sharp.IconButton();
            this.btnCapNhat = new FontAwesome.Sharp.IconButton();
            this.flowLayoutPanelProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.panelFilter.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.White;
            this.panelFilter.Controls.Add(this.btnSearch);
            this.panelFilter.Controls.Add(this.txtSearch);
            this.panelFilter.Controls.Add(this.lblSearch);
            this.panelFilter.Controls.Add(this.cboSort);
            this.panelFilter.Controls.Add(this.lblSort);
            this.panelFilter.Controls.Add(this.cboCategory);
            this.panelFilter.Controls.Add(this.lblCategory);
            this.panelFilter.Controls.Add(this.chkSelectAll);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 0);
            this.panelFilter.Margin = new System.Windows.Forms.Padding(5);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Padding = new System.Windows.Forms.Padding(34, 25, 34, 25);
            this.panelFilter.Size = new System.Drawing.Size(2057, 150);
            this.panelFilter.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSearch.FlatAppearance.BorderSize = 3;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSearch.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnSearch.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSearch.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSearch.IconSize = 22;
            this.btnSearch.Location = new System.Drawing.Point(669, 47);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(189, 66);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = " Tìm";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.SystemColors.Window;
            this.txtSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.txtSearch.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(35)))), ((int)(((byte)(44)))));
            this.txtSearch.BorderRadius = 5;
            this.txtSearch.BorderSize = 2;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSearch.Location = new System.Drawing.Point(223, 47);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(5);
            this.txtSearch.Multiline = false;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Padding = new System.Windows.Forms.Padding(13, 9, 13, 9);
            this.txtSearch.PasswordChar = false;
            this.txtSearch.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtSearch.PlaceholderText = "Nhập tên sản phẩm...";
            this.txtSearch.Size = new System.Drawing.Size(426, 59);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.Texts = "";
            this.txtSearch.UnderlinedStyle = false;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSearch_KeyPress);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblSearch.Location = new System.Drawing.Point(51, 60);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(148, 38);
            this.lblSearch.TabIndex = 5;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // cboSort
            // 
            this.cboSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cboSort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cboSort.BorderRadius = 8;
            this.cboSort.BorderSize = 2;
            this.cboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSort.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cboSort.ForeColor = System.Drawing.Color.White;
            this.cboSort.IconColor = System.Drawing.Color.White;
            this.cboSort.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cboSort.ListTextColor = System.Drawing.Color.White;
            this.cboSort.Location = new System.Drawing.Point(1509, 47);
            this.cboSort.Margin = new System.Windows.Forms.Padding(5);
            this.cboSort.MinimumSize = new System.Drawing.Size(267, 38);
            this.cboSort.Name = "cboSort";
            this.cboSort.Padding = new System.Windows.Forms.Padding(2);
            this.cboSort.Size = new System.Drawing.Size(267, 66);
            this.cboSort.TabIndex = 4;
            this.cboSort.Texts = "";
            this.cboSort.OnSelectedIndexChanged += new System.EventHandler(this.CboSort_SelectedIndexChanged);
            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblSort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblSort.Location = new System.Drawing.Point(1363, 60);
            this.lblSort.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(128, 38);
            this.lblSort.TabIndex = 3;
            this.lblSort.Text = "Sắp xếp:";
            // 
            // cboCategory
            // 
            this.cboCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cboCategory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cboCategory.BorderRadius = 8;
            this.cboCategory.BorderSize = 2;
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cboCategory.ForeColor = System.Drawing.Color.White;
            this.cboCategory.IconColor = System.Drawing.Color.White;
            this.cboCategory.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.cboCategory.ListTextColor = System.Drawing.Color.White;
            this.cboCategory.Location = new System.Drawing.Point(1071, 47);
            this.cboCategory.Margin = new System.Windows.Forms.Padding(5);
            this.cboCategory.MinimumSize = new System.Drawing.Size(267, 38);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Padding = new System.Windows.Forms.Padding(2);
            this.cboCategory.Size = new System.Drawing.Size(267, 66);
            this.cboCategory.TabIndex = 2;
            this.cboCategory.Texts = "";
            this.cboCategory.OnSelectedIndexChanged += new System.EventHandler(this.CboCategory_SelectedIndexChanged);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblCategory.Location = new System.Drawing.Point(891, 60);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(159, 38);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "Danh mục:";
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.chkSelectAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.chkSelectAll.Location = new System.Drawing.Point(1817, 58);
            this.chkSelectAll.Margin = new System.Windows.Forms.Padding(5);
            this.chkSelectAll.MinimumSize = new System.Drawing.Size(200, 66);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(200, 66);
            this.chkSelectAll.TabIndex = 0;
            this.chkSelectAll.Text = "Chọn tất cả";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.ChkSelectAll_CheckedChanged);
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.White;
            this.panelButtons.Controls.Add(this.btnNhapKho);
            this.panelButtons.Controls.Add(this.btnXuatKho);
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Controls.Add(this.btnCapNhat);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 1067);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(5);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(34, 25, 34, 25);
            this.panelButtons.Size = new System.Drawing.Size(2057, 133);
            this.panelButtons.TabIndex = 2;
            // 
            // btnNhapKho
            // 
            this.btnNhapKho.BackColor = System.Drawing.Color.Transparent;
            this.btnNhapKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNhapKho.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(154)))));
            this.btnNhapKho.FlatAppearance.BorderSize = 3;
            this.btnNhapKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhapKho.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnNhapKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnNhapKho.IconChar = FontAwesome.Sharp.IconChar.ArrowDown;
            this.btnNhapKho.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnNhapKho.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNhapKho.IconSize = 26;
            this.btnNhapKho.Location = new System.Drawing.Point(51, 28);
            this.btnNhapKho.Margin = new System.Windows.Forms.Padding(5);
            this.btnNhapKho.Name = "btnNhapKho";
            this.btnNhapKho.Size = new System.Drawing.Size(326, 80);
            this.btnNhapKho.TabIndex = 0;
            this.btnNhapKho.Text = "  Nhập kho thêm";
            this.btnNhapKho.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhapKho.UseVisualStyleBackColor = false;
            this.btnNhapKho.Visible = false;
            this.btnNhapKho.Click += new System.EventHandler(this.BtnNhapKho_Click);
            // 
            // btnXuatKho
            // 
            this.btnXuatKho.BackColor = System.Drawing.Color.Transparent;
            this.btnXuatKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatKho.Enabled = false;
            this.btnXuatKho.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXuatKho.FlatAppearance.BorderSize = 3;
            this.btnXuatKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatKho.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXuatKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnXuatKho.IconChar = FontAwesome.Sharp.IconChar.ArrowUp;
            this.btnXuatKho.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnXuatKho.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnXuatKho.IconSize = 26;
            this.btnXuatKho.Location = new System.Drawing.Point(411, 28);
            this.btnXuatKho.Margin = new System.Windows.Forms.Padding(5);
            this.btnXuatKho.Name = "btnXuatKho";
            this.btnXuatKho.Size = new System.Drawing.Size(274, 80);
            this.btnXuatKho.TabIndex = 1;
            this.btnXuatKho.Text = "  Xuất kho";
            this.btnXuatKho.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnXuatKho.UseVisualStyleBackColor = false;
            this.btnXuatKho.Click += new System.EventHandler(this.BtnXuatKho_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btnDelete.IconColor = System.Drawing.Color.White;
            this.btnDelete.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDelete.IconSize = 26;
            this.btnDelete.Location = new System.Drawing.Point(720, 28);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(223, 80);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "  Xóa";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnCapNhat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCapNhat.Enabled = false;
            this.btnCapNhat.FlatAppearance.BorderSize = 0;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.IconChar = FontAwesome.Sharp.IconChar.Edit;
            this.btnCapNhat.IconColor = System.Drawing.Color.White;
            this.btnCapNhat.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCapNhat.IconSize = 26;
            this.btnCapNhat.Location = new System.Drawing.Point(977, 28);
            this.btnCapNhat.Margin = new System.Windows.Forms.Padding(5);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(223, 80);
            this.btnCapNhat.TabIndex = 3;
            this.btnCapNhat.Text = "  Cập nhật";
            this.btnCapNhat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.BtnCapNhat_Click);
            // 
            // flowLayoutPanelProducts
            // 
            this.flowLayoutPanelProducts.AutoScroll = true;
            this.flowLayoutPanelProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.flowLayoutPanelProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelProducts.Location = new System.Drawing.Point(0, 150);
            this.flowLayoutPanelProducts.Margin = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanelProducts.Name = "flowLayoutPanelProducts";
            this.flowLayoutPanelProducts.Padding = new System.Windows.Forms.Padding(26, 25, 26, 25);
            this.flowLayoutPanelProducts.Size = new System.Drawing.Size(2057, 917);
            this.flowLayoutPanelProducts.TabIndex = 3;
            this.flowLayoutPanelProducts.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanelProducts_Paint);
            // 
            // FormProductList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2057, 1200);
            this.Controls.Add(this.flowLayoutPanelProducts);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormProductList";
            this.Text = "Danh sách sản phẩm";
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private WinFormsWarehouseManager.RJControls.RJComboBox cboCategory;
        private System.Windows.Forms.Label lblCategory;
        private WinFormsWarehouseManager.RJControls.RJComboBox cboSort;
        private System.Windows.Forms.Label lblSort;
        private WinFormsWarehouseManager.RJControls.RJTextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private FontAwesome.Sharp.IconButton btnSearch;
        private System.Windows.Forms.Panel panelButtons;
        private FontAwesome.Sharp.IconButton btnNhapKho;
        private FontAwesome.Sharp.IconButton btnXuatKho;
        private FontAwesome.Sharp.IconButton btnDelete;
        private FontAwesome.Sharp.IconButton btnCapNhat;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProducts;
    }
}
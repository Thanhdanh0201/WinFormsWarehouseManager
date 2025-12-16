namespace WinFormsWarehouseManager
{
    partial class FormDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.tableLayoutPanelTop = new System.Windows.Forms.TableLayoutPanel();
            this.panelNumExport = new System.Windows.Forms.Panel();
            this.iconPictureBoxExport = new FontAwesome.Sharp.IconPictureBox();
            this.lblNumExport = new System.Windows.Forms.Label();
            this.lblTitleExport = new System.Windows.Forms.Label();
            this.panelNumImport = new System.Windows.Forms.Panel();
            this.iconPictureBoxImport = new FontAwesome.Sharp.IconPictureBox();
            this.lblNumImport = new System.Windows.Forms.Label();
            this.lblTitleImport = new System.Windows.Forms.Label();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.dtpFromDate = new WinFormsWarehouseManager.RJControls.CustomDateTimePicker();
            this.dtpToDate = new WinFormsWarehouseManager.RJControls.CustomDateTimePicker();
            this.btnApply = new FontAwesome.Sharp.IconButton();
            this.btnToday = new System.Windows.Forms.Button();
            this.btnLast7Days = new System.Windows.Forms.Button();
            this.btnLast30Days = new System.Windows.Forms.Button();
            this.btnThisMonth = new System.Windows.Forms.Button();
            this.panelNumProducts = new System.Windows.Forms.Panel();
            this.iconPictureBoxProducts = new FontAwesome.Sharp.IconPictureBox();
            this.lblNumProducts = new System.Windows.Forms.Label();
            this.lblTitleProducts = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelDataGrid = new System.Windows.Forms.Panel();
            this.dgvDS = new System.Windows.Forms.DataGridView();
            this.lblTitleExpired = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.iconPictureBoxReceivers = new FontAwesome.Sharp.IconPictureBox();
            this.iconPictureBoxSuppliers = new FontAwesome.Sharp.IconPictureBox();
            this.lblNumRecievers = new System.Windows.Forms.Label();
            this.lblTitleReceiver = new System.Windows.Forms.Label();
            this.lblNumSuppliers = new System.Windows.Forms.Label();
            this.lblTitleSuppliers = new System.Windows.Forms.Label();
            this.lblTitleStats = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.tableLayoutPanelTop.SuspendLayout();
            this.panelNumExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBoxExport)).BeginInit();
            this.panelNumImport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBoxImport)).BeginInit();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.panelNumProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBoxProducts)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDS)).BeginInit();
            this.panelStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBoxReceivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBoxSuppliers)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.panelTop, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.Padding = new System.Windows.Forms.Padding(22, 23, 22, 23);
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1911, 1295);
            this.tableLayoutPanelMain.TabIndex = 0;
            this.tableLayoutPanelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanelMain_Paint);
            // 
            // panelTop
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.panelTop, 2);
            this.panelTop.Controls.Add(this.tableLayoutPanelTop);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTop.Location = new System.Drawing.Point(26, 28);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1859, 427);
            this.panelTop.TabIndex = 0;
            // 
            // tableLayoutPanelTop
            // 
            this.tableLayoutPanelTop.ColumnCount = 3;
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanelTop.Controls.Add(this.panelNumExport, 2, 1);
            this.tableLayoutPanelTop.Controls.Add(this.panelNumImport, 1, 1);
            this.tableLayoutPanelTop.Controls.Add(this.flowLayoutPanelButtons, 0, 0);
            this.tableLayoutPanelTop.Controls.Add(this.panelNumProducts, 0, 1);
            this.tableLayoutPanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelTop.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanelTop.Name = "tableLayoutPanelTop";
            this.tableLayoutPanelTop.RowCount = 2;
            this.tableLayoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTop.Size = new System.Drawing.Size(1859, 427);
            this.tableLayoutPanelTop.TabIndex = 0;
            // 
            // panelNumExport
            // 
            this.panelNumExport.BackColor = System.Drawing.Color.White;
            this.panelNumExport.Controls.Add(this.iconPictureBoxExport);
            this.panelNumExport.Controls.Add(this.lblNumExport);
            this.panelNumExport.Controls.Add(this.lblTitleExport);
            this.panelNumExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNumExport.Location = new System.Drawing.Point(1253, 106);
            this.panelNumExport.Margin = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.panelNumExport.Name = "panelNumExport";
            this.panelNumExport.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.panelNumExport.Size = new System.Drawing.Size(591, 309);
            this.panelNumExport.TabIndex = 3;
            this.panelNumExport.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelCard_Paint);
            // 
            // iconPictureBoxExport
            // 
            this.iconPictureBoxExport.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBoxExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.iconPictureBoxExport.IconChar = FontAwesome.Sharp.IconChar.BoxesPacking;
            this.iconPictureBoxExport.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.iconPictureBoxExport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBoxExport.IconSize = 72;
            this.iconPictureBoxExport.Location = new System.Drawing.Point(30, 31);
            this.iconPictureBoxExport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.iconPictureBoxExport.Name = "iconPictureBoxExport";
            this.iconPictureBoxExport.Size = new System.Drawing.Size(72, 75);
            this.iconPictureBoxExport.TabIndex = 2;
            this.iconPictureBoxExport.TabStop = false;
            // 
            // lblNumExport
            // 
            this.lblNumExport.AutoSize = true;
            this.lblNumExport.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumExport.Location = new System.Drawing.Point(30, 156);
            this.lblNumExport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumExport.Name = "lblNumExport";
            this.lblNumExport.Size = new System.Drawing.Size(74, 86);
            this.lblNumExport.TabIndex = 1;
            this.lblNumExport.Text = "0";
            // 
            // lblTitleExport
            // 
            this.lblTitleExport.AutoSize = true;
            this.lblTitleExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblTitleExport.Location = new System.Drawing.Point(30, 117);
            this.lblTitleExport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleExport.Name = "lblTitleExport";
            this.lblTitleExport.Size = new System.Drawing.Size(365, 32);
            this.lblTitleExport.TabIndex = 0;
            this.lblTitleExport.Text = "Tổng số lượng hóa đơn xuất kho";
            // 
            // panelNumImport
            // 
            this.panelNumImport.BackColor = System.Drawing.Color.White;
            this.panelNumImport.Controls.Add(this.iconPictureBoxImport);
            this.panelNumImport.Controls.Add(this.lblNumImport);
            this.panelNumImport.Controls.Add(this.lblTitleImport);
            this.panelNumImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNumImport.Location = new System.Drawing.Point(634, 106);
            this.panelNumImport.Margin = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.panelNumImport.Name = "panelNumImport";
            this.panelNumImport.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.panelNumImport.Size = new System.Drawing.Size(589, 309);
            this.panelNumImport.TabIndex = 2;
            this.panelNumImport.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelCard_Paint);
            // 
            // iconPictureBoxImport
            // 
            this.iconPictureBoxImport.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBoxImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.iconPictureBoxImport.IconChar = FontAwesome.Sharp.IconChar.TruckRampBox;
            this.iconPictureBoxImport.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.iconPictureBoxImport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBoxImport.IconSize = 72;
            this.iconPictureBoxImport.Location = new System.Drawing.Point(30, 31);
            this.iconPictureBoxImport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.iconPictureBoxImport.Name = "iconPictureBoxImport";
            this.iconPictureBoxImport.Size = new System.Drawing.Size(72, 75);
            this.iconPictureBoxImport.TabIndex = 2;
            this.iconPictureBoxImport.TabStop = false;
            // 
            // lblNumImport
            // 
            this.lblNumImport.AutoSize = true;
            this.lblNumImport.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumImport.Location = new System.Drawing.Point(30, 156);
            this.lblNumImport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumImport.Name = "lblNumImport";
            this.lblNumImport.Size = new System.Drawing.Size(74, 86);
            this.lblNumImport.TabIndex = 1;
            this.lblNumImport.Text = "0";
            // 
            // lblTitleImport
            // 
            this.lblTitleImport.AutoSize = true;
            this.lblTitleImport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblTitleImport.Location = new System.Drawing.Point(30, 117);
            this.lblTitleImport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleImport.Name = "lblTitleImport";
            this.lblTitleImport.Size = new System.Drawing.Size(374, 32);
            this.lblTitleImport.TabIndex = 0;
            this.lblTitleImport.Text = "Tổng số lượng hóa đơn nhập kho";
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.AutoSize = true;
            this.flowLayoutPanelButtons.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelTop.SetColumnSpan(this.flowLayoutPanelButtons, 3);
            this.flowLayoutPanelButtons.Controls.Add(this.dtpFromDate);
            this.flowLayoutPanelButtons.Controls.Add(this.dtpToDate);
            this.flowLayoutPanelButtons.Controls.Add(this.btnApply);
            this.flowLayoutPanelButtons.Controls.Add(this.btnToday);
            this.flowLayoutPanelButtons.Controls.Add(this.btnLast7Days);
            this.flowLayoutPanelButtons.Controls.Add(this.btnLast30Days);
            this.flowLayoutPanelButtons.Controls.Add(this.btnThisMonth);
            this.flowLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(4, 5);
            this.flowLayoutPanelButtons.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(1851, 84);
            this.flowLayoutPanelButtons.TabIndex = 0;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.dtpFromDate.BorderColorFocus = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.dtpFromDate.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.dtpFromDate.BorderRadius = 7;
            this.dtpFromDate.BorderSize = 3;
            this.dtpFromDate.CalendarFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.dtpFromDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.dtpFromDate.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpFromDate.CalendarTrailingForeColor = System.Drawing.Color.Firebrick;
            this.dtpFromDate.CustomFormat = "MMM dd, yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.IconSize = 50;
            this.dtpFromDate.Location = new System.Drawing.Point(4, 5);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpFromDate.MinimumSize = new System.Drawing.Size(193, 55);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(223, 55);
            this.dtpFromDate.SkinColor = System.Drawing.Color.White;
            this.dtpFromDate.TabIndex = 0;
            this.dtpFromDate.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            // 
            // dtpToDate
            // 
            this.dtpToDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.dtpToDate.BorderColorFocus = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.dtpToDate.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.dtpToDate.BorderRadius = 7;
            this.dtpToDate.BorderSize = 3;
            this.dtpToDate.CalendarFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.dtpToDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.dtpToDate.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpToDate.CalendarTrailingForeColor = System.Drawing.Color.Firebrick;
            this.dtpToDate.CustomFormat = "MMM dd, yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.IconSize = 50;
            this.dtpToDate.Location = new System.Drawing.Point(235, 5);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpToDate.MinimumSize = new System.Drawing.Size(193, 55);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(223, 55);
            this.dtpToDate.SkinColor = System.Drawing.Color.White;
            this.dtpToDate.TabIndex = 1;
            this.dtpToDate.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.White;
            this.btnApply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnApply.FlatAppearance.BorderSize = 0;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnApply.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnApply.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnApply.IconSize = 40;
            this.btnApply.Location = new System.Drawing.Point(466, 5);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(60, 62);
            this.btnApply.TabIndex = 2;
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnToday
            // 
            this.btnToday.BackColor = System.Drawing.Color.White;
            this.btnToday.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToday.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnToday.Location = new System.Drawing.Point(534, 5);
            this.btnToday.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnToday.MinimumSize = new System.Drawing.Size(150, 55);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(150, 62);
            this.btnToday.TabIndex = 4;
            this.btnToday.Text = "Hôm nay";
            this.btnToday.UseVisualStyleBackColor = false;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            this.btnToday.MouseEnter += new System.EventHandler(this.FilterButton_MouseEnter);
            this.btnToday.MouseLeave += new System.EventHandler(this.FilterButton_MouseLeave);
            // 
            // btnLast7Days
            // 
            this.btnLast7Days.BackColor = System.Drawing.Color.White;
            this.btnLast7Days.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.btnLast7Days.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast7Days.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast7Days.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLast7Days.Location = new System.Drawing.Point(692, 5);
            this.btnLast7Days.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLast7Days.MinimumSize = new System.Drawing.Size(150, 55);
            this.btnLast7Days.Name = "btnLast7Days";
            this.btnLast7Days.Size = new System.Drawing.Size(165, 62);
            this.btnLast7Days.TabIndex = 5;
            this.btnLast7Days.Text = "7 ngày trước";
            this.btnLast7Days.UseVisualStyleBackColor = false;
            this.btnLast7Days.Click += new System.EventHandler(this.btnLast7Days_Click);
            this.btnLast7Days.MouseEnter += new System.EventHandler(this.FilterButton_MouseEnter);
            this.btnLast7Days.MouseLeave += new System.EventHandler(this.FilterButton_MouseLeave);
            // 
            // btnLast30Days
            // 
            this.btnLast30Days.BackColor = System.Drawing.Color.White;
            this.btnLast30Days.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.btnLast30Days.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast30Days.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast30Days.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLast30Days.Location = new System.Drawing.Point(865, 5);
            this.btnLast30Days.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLast30Days.MinimumSize = new System.Drawing.Size(150, 55);
            this.btnLast30Days.Name = "btnLast30Days";
            this.btnLast30Days.Size = new System.Drawing.Size(180, 62);
            this.btnLast30Days.TabIndex = 6;
            this.btnLast30Days.Text = "30 ngày trước";
            this.btnLast30Days.UseVisualStyleBackColor = false;
            this.btnLast30Days.Click += new System.EventHandler(this.btnLast30Days_Click);
            this.btnLast30Days.MouseEnter += new System.EventHandler(this.FilterButton_MouseEnter);
            this.btnLast30Days.MouseLeave += new System.EventHandler(this.FilterButton_MouseLeave);
            // 
            // btnThisMonth
            // 
            this.btnThisMonth.BackColor = System.Drawing.Color.White;
            this.btnThisMonth.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.btnThisMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThisMonth.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThisMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnThisMonth.Location = new System.Drawing.Point(1053, 5);
            this.btnThisMonth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnThisMonth.MinimumSize = new System.Drawing.Size(150, 55);
            this.btnThisMonth.Name = "btnThisMonth";
            this.btnThisMonth.Size = new System.Drawing.Size(180, 62);
            this.btnThisMonth.TabIndex = 7;
            this.btnThisMonth.Text = "Tháng này";
            this.btnThisMonth.UseVisualStyleBackColor = false;
            this.btnThisMonth.Click += new System.EventHandler(this.btnThisMonth_Click);
            this.btnThisMonth.MouseEnter += new System.EventHandler(this.FilterButton_MouseEnter);
            this.btnThisMonth.MouseLeave += new System.EventHandler(this.FilterButton_MouseLeave);
            // 
            // panelNumProducts
            // 
            this.panelNumProducts.BackColor = System.Drawing.Color.White;
            this.panelNumProducts.Controls.Add(this.iconPictureBoxProducts);
            this.panelNumProducts.Controls.Add(this.lblNumProducts);
            this.panelNumProducts.Controls.Add(this.lblTitleProducts);
            this.panelNumProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNumProducts.Location = new System.Drawing.Point(15, 106);
            this.panelNumProducts.Margin = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.panelNumProducts.Name = "panelNumProducts";
            this.panelNumProducts.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.panelNumProducts.Size = new System.Drawing.Size(589, 309);
            this.panelNumProducts.TabIndex = 1;
            this.panelNumProducts.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelCard_Paint);
            // 
            // iconPictureBoxProducts
            // 
            this.iconPictureBoxProducts.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBoxProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.iconPictureBoxProducts.IconChar = FontAwesome.Sharp.IconChar.BoxOpen;
            this.iconPictureBoxProducts.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.iconPictureBoxProducts.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBoxProducts.IconSize = 72;
            this.iconPictureBoxProducts.Location = new System.Drawing.Point(30, 31);
            this.iconPictureBoxProducts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.iconPictureBoxProducts.Name = "iconPictureBoxProducts";
            this.iconPictureBoxProducts.Size = new System.Drawing.Size(72, 75);
            this.iconPictureBoxProducts.TabIndex = 2;
            this.iconPictureBoxProducts.TabStop = false;
            // 
            // lblNumProducts
            // 
            this.lblNumProducts.AutoSize = true;
            this.lblNumProducts.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumProducts.Location = new System.Drawing.Point(30, 156);
            this.lblNumProducts.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumProducts.Name = "lblNumProducts";
            this.lblNumProducts.Size = new System.Drawing.Size(74, 86);
            this.lblNumProducts.TabIndex = 1;
            this.lblNumProducts.Text = "0";
            // 
            // lblTitleProducts
            // 
            this.lblTitleProducts.AutoSize = true;
            this.lblTitleProducts.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblTitleProducts.Location = new System.Drawing.Point(30, 117);
            this.lblTitleProducts.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleProducts.Name = "lblTitleProducts";
            this.lblTitleProducts.Size = new System.Drawing.Size(281, 32);
            this.lblTitleProducts.TabIndex = 0;
            this.lblTitleProducts.Text = "Tổng số lượng sản phẩm";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanelMain.SetColumnSpan(this.tableLayoutPanel1, 2);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.panelDataGrid, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelStats, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(26, 964);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1859, 303);
            this.tableLayoutPanel1.TabIndex = 3;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // panelDataGrid
            // 
            this.panelDataGrid.BackColor = System.Drawing.Color.White;
            this.panelDataGrid.Controls.Add(this.dgvDS);
            this.panelDataGrid.Controls.Add(this.lblTitleExpired);
            this.panelDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDataGrid.Location = new System.Drawing.Point(572, 12);
            this.panelDataGrid.Margin = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.panelDataGrid.Name = "panelDataGrid";
            this.panelDataGrid.Padding = new System.Windows.Forms.Padding(30, 23, 30, 23);
            this.panelDataGrid.Size = new System.Drawing.Size(1272, 279);
            this.panelDataGrid.TabIndex = 33;
            this.panelDataGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelCard_Paint);
            // 
            // dgvDS
            // 
            this.dgvDS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDS.BackgroundColor = System.Drawing.Color.White;
            this.dgvDS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDS.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.dgvDS.Location = new System.Drawing.Point(30, 76);
            this.dgvDS.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvDS.Name = "dgvDS";
            this.dgvDS.RowHeadersWidth = 82;
            this.dgvDS.RowTemplate.Height = 33;
            this.dgvDS.Size = new System.Drawing.Size(1212, 180);
            this.dgvDS.TabIndex = 32;
            // 
            // lblTitleExpired
            // 
            this.lblTitleExpired.AutoSize = true;
            this.lblTitleExpired.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleExpired.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleExpired.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTitleExpired.Location = new System.Drawing.Point(30, 23);
            this.lblTitleExpired.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleExpired.Name = "lblTitleExpired";
            this.lblTitleExpired.Padding = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.lblTitleExpired.Size = new System.Drawing.Size(557, 53);
            this.lblTitleExpired.TabIndex = 33;
            this.lblTitleExpired.Text = "Danh sách sản phẩm hết hạn / gần hết hạn";
            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.White;
            this.panelStats.Controls.Add(this.iconPictureBoxReceivers);
            this.panelStats.Controls.Add(this.iconPictureBoxSuppliers);
            this.panelStats.Controls.Add(this.lblNumRecievers);
            this.panelStats.Controls.Add(this.lblTitleReceiver);
            this.panelStats.Controls.Add(this.lblNumSuppliers);
            this.panelStats.Controls.Add(this.lblTitleSuppliers);
            this.panelStats.Controls.Add(this.lblTitleStats);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStats.Location = new System.Drawing.Point(15, 12);
            this.panelStats.Margin = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.panelStats.Name = "panelStats";
            this.panelStats.Padding = new System.Windows.Forms.Padding(30, 23, 30, 23);
            this.panelStats.Size = new System.Drawing.Size(527, 279);
            this.panelStats.TabIndex = 29;
            this.panelStats.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelCard_Paint);
            // 
            // iconPictureBoxReceivers
            // 
            this.iconPictureBoxReceivers.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBoxReceivers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.iconPictureBoxReceivers.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.iconPictureBoxReceivers.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.iconPictureBoxReceivers.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBoxReceivers.IconSize = 36;
            this.iconPictureBoxReceivers.Location = new System.Drawing.Point(30, 181);
            this.iconPictureBoxReceivers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.iconPictureBoxReceivers.Name = "iconPictureBoxReceivers";
            this.iconPictureBoxReceivers.Size = new System.Drawing.Size(36, 38);
            this.iconPictureBoxReceivers.TabIndex = 7;
            this.iconPictureBoxReceivers.TabStop = false;
            // 
            // iconPictureBoxSuppliers
            // 
            this.iconPictureBoxSuppliers.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBoxSuppliers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.iconPictureBoxSuppliers.IconChar = FontAwesome.Sharp.IconChar.TruckField;
            this.iconPictureBoxSuppliers.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.iconPictureBoxSuppliers.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBoxSuppliers.IconSize = 36;
            this.iconPictureBoxSuppliers.Location = new System.Drawing.Point(30, 92);
            this.iconPictureBoxSuppliers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.iconPictureBoxSuppliers.Name = "iconPictureBoxSuppliers";
            this.iconPictureBoxSuppliers.Size = new System.Drawing.Size(36, 38);
            this.iconPictureBoxSuppliers.TabIndex = 6;
            this.iconPictureBoxSuppliers.TabStop = false;
            // 
            // lblNumRecievers
            // 
            this.lblNumRecievers.AutoSize = true;
            this.lblNumRecievers.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumRecievers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumRecievers.Location = new System.Drawing.Point(75, 211);
            this.lblNumRecievers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumRecievers.Name = "lblNumRecievers";
            this.lblNumRecievers.Size = new System.Drawing.Size(44, 51);
            this.lblNumRecievers.TabIndex = 5;
            this.lblNumRecievers.Text = "0";
            // 
            // lblTitleReceiver
            // 
            this.lblTitleReceiver.AutoSize = true;
            this.lblTitleReceiver.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleReceiver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblTitleReceiver.Location = new System.Drawing.Point(74, 181);
            this.lblTitleReceiver.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleReceiver.Name = "lblTitleReceiver";
            this.lblTitleReceiver.Size = new System.Drawing.Size(216, 30);
            this.lblTitleReceiver.TabIndex = 4;
            this.lblTitleReceiver.Text = "Số lượng khách hàng";
            // 
            // lblNumSuppliers
            // 
            this.lblNumSuppliers.AutoSize = true;
            this.lblNumSuppliers.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumSuppliers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumSuppliers.Location = new System.Drawing.Point(75, 122);
            this.lblNumSuppliers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumSuppliers.Name = "lblNumSuppliers";
            this.lblNumSuppliers.Size = new System.Drawing.Size(44, 51);
            this.lblNumSuppliers.TabIndex = 3;
            this.lblNumSuppliers.Text = "0";
            // 
            // lblTitleSuppliers
            // 
            this.lblTitleSuppliers.AutoSize = true;
            this.lblTitleSuppliers.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleSuppliers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblTitleSuppliers.Location = new System.Drawing.Point(75, 92);
            this.lblTitleSuppliers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleSuppliers.Name = "lblTitleSuppliers";
            this.lblTitleSuppliers.Size = new System.Drawing.Size(234, 30);
            this.lblTitleSuppliers.TabIndex = 2;
            this.lblTitleSuppliers.Text = "Số lượng nhà cung cấp";
            // 
            // lblTitleStats
            // 
            this.lblTitleStats.AutoSize = true;
            this.lblTitleStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleStats.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTitleStats.Location = new System.Drawing.Point(30, 23);
            this.lblTitleStats.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleStats.Name = "lblTitleStats";
            this.lblTitleStats.Padding = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.lblTitleStats.Size = new System.Drawing.Size(248, 53);
            this.lblTitleStats.TabIndex = 0;
            this.lblTitleStats.Text = "Các thông số khác";
            // 
            // FormDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1911, 1295);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormDashboard";
            this.Text = "Dashboard - Warehouse Manager";
            this.Load += new System.EventHandler(this.FormDashboard_Load);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.tableLayoutPanelTop.ResumeLayout(false);
            this.tableLayoutPanelTop.PerformLayout();
            this.panelNumExport.ResumeLayout(false);
            this.panelNumExport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBoxExport)).EndInit();
            this.panelNumImport.ResumeLayout(false);
            this.panelNumImport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBoxImport)).EndInit();
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.panelNumProducts.ResumeLayout(false);
            this.panelNumProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBoxProducts)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelDataGrid.ResumeLayout(false);
            this.panelDataGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDS)).EndInit();
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBoxReceivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBoxSuppliers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTop;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private FontAwesome.Sharp.IconButton btnApply;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnLast7Days;
        private System.Windows.Forms.Button btnLast30Days;
        private System.Windows.Forms.Button btnThisMonth;
        private System.Windows.Forms.Panel panelNumExport;
        private FontAwesome.Sharp.IconPictureBox iconPictureBoxExport;
        private System.Windows.Forms.Label lblNumExport;
        private System.Windows.Forms.Label lblTitleExport;
        private System.Windows.Forms.Panel panelNumImport;
        private FontAwesome.Sharp.IconPictureBox iconPictureBoxImport;
        private System.Windows.Forms.Label lblNumImport;
        private System.Windows.Forms.Label lblTitleImport;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartReceipts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelDataGrid;
        private System.Windows.Forms.DataGridView dgvDS;
        private System.Windows.Forms.Label lblTitleExpired;
        private System.Windows.Forms.Panel panelStats;
        private FontAwesome.Sharp.IconPictureBox iconPictureBoxReceivers;
        private FontAwesome.Sharp.IconPictureBox iconPictureBoxSuppliers;
        private System.Windows.Forms.Label lblNumRecievers;
        private System.Windows.Forms.Label lblTitleReceiver;
        private System.Windows.Forms.Label lblNumSuppliers;
        private System.Windows.Forms.Label lblTitleSuppliers;
        private System.Windows.Forms.Label lblTitleStats;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTopCategories;
        private System.Windows.Forms.Panel panelNumProducts;
        private FontAwesome.Sharp.IconPictureBox iconPictureBoxProducts;
        private System.Windows.Forms.Label lblNumProducts;
        private System.Windows.Forms.Label lblTitleProducts;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private WinFormsWarehouseManager.RJControls.CustomDateTimePicker dtpFromDate;
        private WinFormsWarehouseManager.RJControls.CustomDateTimePicker dtpToDate;
    }
}
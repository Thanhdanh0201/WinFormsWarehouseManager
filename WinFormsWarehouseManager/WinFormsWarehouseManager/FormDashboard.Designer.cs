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
            this.lblNumExport = new System.Windows.Forms.Label();
            this.lblTitleExport = new System.Windows.Forms.Label();
            this.panelNumImport = new System.Windows.Forms.Panel();
            this.lblNumImport = new System.Windows.Forms.Label();
            this.lblTitleImport = new System.Windows.Forms.Label();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCustom = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.btnLast7Days = new System.Windows.Forms.Button();
            this.btnLast30Days = new System.Windows.Forms.Button();
            this.btnThisMonth = new System.Windows.Forms.Button();
            this.panelNumProducts = new System.Windows.Forms.Panel();
            this.lblNumProducts = new System.Windows.Forms.Label();
            this.lblTitleProducts = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvDS = new System.Windows.Forms.DataGridView();
            this.panelStats = new System.Windows.Forms.Panel();
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
            this.panelNumImport.SuspendLayout();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.panelNumProducts.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDS)).BeginInit();
            this.panelStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.04348F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.95652F));
            this.tableLayoutPanelMain.Controls.Add(this.panelTop, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1274, 829);
            this.tableLayoutPanelMain.TabIndex = 0;
            this.tableLayoutPanelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanelMain_Paint);
            // 
            // panelTop
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.panelTop, 2);
            this.panelTop.Controls.Add(this.tableLayoutPanelTop);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTop.Location = new System.Drawing.Point(3, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1268, 284);
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
            this.tableLayoutPanelTop.Name = "tableLayoutPanelTop";
            this.tableLayoutPanelTop.RowCount = 2;
            this.tableLayoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTop.Size = new System.Drawing.Size(1268, 284);
            this.tableLayoutPanelTop.TabIndex = 0;
            // 
            // panelNumExport
            // 
            this.panelNumExport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNumExport.Controls.Add(this.lblNumExport);
            this.panelNumExport.Controls.Add(this.lblTitleExport);
            this.panelNumExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNumExport.Location = new System.Drawing.Point(847, 55);
            this.panelNumExport.Name = "panelNumExport";
            this.panelNumExport.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panelNumExport.Size = new System.Drawing.Size(418, 235);
            this.panelNumExport.TabIndex = 3;
            // 
            // lblNumExport
            // 
            this.lblNumExport.AutoSize = true;
            this.lblNumExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumExport.Location = new System.Drawing.Point(10, 25);
            this.lblNumExport.Name = "lblNumExport";
            this.lblNumExport.Size = new System.Drawing.Size(24, 25);
            this.lblNumExport.TabIndex = 1;
            this.lblNumExport.Text = "0";
            // 
            // lblTitleExport
            // 
            this.lblTitleExport.AutoSize = true;
            this.lblTitleExport.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTitleExport.Location = new System.Drawing.Point(10, 0);
            this.lblTitleExport.Name = "lblTitleExport";
            this.lblTitleExport.Size = new System.Drawing.Size(321, 25);
            this.lblTitleExport.TabIndex = 0;
            this.lblTitleExport.Text = "Tổng số lượng hóa đơn xuất kho";
            // 
            // panelNumImport
            // 
            this.panelNumImport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNumImport.Controls.Add(this.lblNumImport);
            this.panelNumImport.Controls.Add(this.lblTitleImport);
            this.panelNumImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNumImport.Location = new System.Drawing.Point(425, 55);
            this.panelNumImport.Name = "panelNumImport";
            this.panelNumImport.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panelNumImport.Size = new System.Drawing.Size(416, 235);
            this.panelNumImport.TabIndex = 2;
            // 
            // lblNumImport
            // 
            this.lblNumImport.AutoSize = true;
            this.lblNumImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumImport.Location = new System.Drawing.Point(10, 25);
            this.lblNumImport.Name = "lblNumImport";
            this.lblNumImport.Size = new System.Drawing.Size(24, 25);
            this.lblNumImport.TabIndex = 1;
            this.lblNumImport.Text = "0";
            // 
            // lblTitleImport
            // 
            this.lblTitleImport.AutoSize = true;
            this.lblTitleImport.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTitleImport.Location = new System.Drawing.Point(10, 0);
            this.lblTitleImport.Name = "lblTitleImport";
            this.lblTitleImport.Size = new System.Drawing.Size(328, 25);
            this.lblTitleImport.TabIndex = 0;
            this.lblTitleImport.Text = "Tổng số lượng hóa đơn nhập kho";
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.AutoSize = true;
            this.flowLayoutPanelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.tableLayoutPanelTop.SetColumnSpan(this.flowLayoutPanelButtons, 3);
            this.flowLayoutPanelButtons.Controls.Add(this.dtpFromDate);
            this.flowLayoutPanelButtons.Controls.Add(this.dtpToDate);
            this.flowLayoutPanelButtons.Controls.Add(this.btnApply);
            this.flowLayoutPanelButtons.Controls.Add(this.btnCustom);
            this.flowLayoutPanelButtons.Controls.Add(this.btnToday);
            this.flowLayoutPanelButtons.Controls.Add(this.btnLast7Days);
            this.flowLayoutPanelButtons.Controls.Add(this.btnLast30Days);
            this.flowLayoutPanelButtons.Controls.Add(this.btnThisMonth);
            this.flowLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(1262, 46);
            this.flowLayoutPanelButtons.TabIndex = 0;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.dtpFromDate.CustomFormat = "MMM,dd,yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(3, 3);
            this.dtpFromDate.MinimumSize = new System.Drawing.Size(130, 30);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(200, 31);
            this.dtpFromDate.TabIndex = 0;
            // 
            // dtpToDate
            // 
            this.dtpToDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.dtpToDate.CustomFormat = "MMM,dd,yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(209, 3);
            this.dtpToDate.MinimumSize = new System.Drawing.Size(130, 30);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(200, 31);
            this.dtpToDate.TabIndex = 1;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(415, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(40, 40);
            this.btnApply.TabIndex = 2;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCustom
            // 
            this.btnCustom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnCustom.Location = new System.Drawing.Point(461, 3);
            this.btnCustom.MinimumSize = new System.Drawing.Size(130, 30);
            this.btnCustom.Name = "btnCustom";
            this.btnCustom.Size = new System.Drawing.Size(130, 40);
            this.btnCustom.TabIndex = 3;
            this.btnCustom.Text = "Custom";
            this.btnCustom.UseVisualStyleBackColor = true;
            this.btnCustom.Click += new System.EventHandler(this.btnCustom_Click);
            // 
            // btnToday
            // 
            this.btnToday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnToday.Location = new System.Drawing.Point(597, 3);
            this.btnToday.MinimumSize = new System.Drawing.Size(130, 30);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(130, 40);
            this.btnToday.TabIndex = 4;
            this.btnToday.Text = "Today";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btnLast7Days
            // 
            this.btnLast7Days.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLast7Days.Location = new System.Drawing.Point(733, 3);
            this.btnLast7Days.MinimumSize = new System.Drawing.Size(130, 30);
            this.btnLast7Days.Name = "btnLast7Days";
            this.btnLast7Days.Size = new System.Drawing.Size(130, 40);
            this.btnLast7Days.TabIndex = 5;
            this.btnLast7Days.Text = "Last 7 Days";
            this.btnLast7Days.UseVisualStyleBackColor = true;
            this.btnLast7Days.Click += new System.EventHandler(this.btnLast7Days_Click);
            // 
            // btnLast30Days
            // 
            this.btnLast30Days.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLast30Days.Location = new System.Drawing.Point(869, 3);
            this.btnLast30Days.MinimumSize = new System.Drawing.Size(130, 30);
            this.btnLast30Days.Name = "btnLast30Days";
            this.btnLast30Days.Size = new System.Drawing.Size(130, 40);
            this.btnLast30Days.TabIndex = 6;
            this.btnLast30Days.Text = "Last 30 Days";
            this.btnLast30Days.UseVisualStyleBackColor = true;
            this.btnLast30Days.Click += new System.EventHandler(this.btnLast30Days_Click);
            // 
            // btnThisMonth
            // 
            this.btnThisMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnThisMonth.Location = new System.Drawing.Point(1005, 3);
            this.btnThisMonth.MinimumSize = new System.Drawing.Size(130, 30);
            this.btnThisMonth.Name = "btnThisMonth";
            this.btnThisMonth.Size = new System.Drawing.Size(130, 40);
            this.btnThisMonth.TabIndex = 7;
            this.btnThisMonth.Text = "This Month";
            this.btnThisMonth.UseVisualStyleBackColor = true;
            this.btnThisMonth.Click += new System.EventHandler(this.btnThisMonth_Click);
            // 
            // panelNumProducts
            // 
            this.panelNumProducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNumProducts.Controls.Add(this.lblNumProducts);
            this.panelNumProducts.Controls.Add(this.lblTitleProducts);
            this.panelNumProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNumProducts.Location = new System.Drawing.Point(3, 55);
            this.panelNumProducts.Name = "panelNumProducts";
            this.panelNumProducts.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panelNumProducts.Size = new System.Drawing.Size(416, 235);
            this.panelNumProducts.TabIndex = 1;
            // 
            // lblNumProducts
            // 
            this.lblNumProducts.AutoSize = true;
            this.lblNumProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumProducts.Location = new System.Drawing.Point(10, 25);
            this.lblNumProducts.Name = "lblNumProducts";
            this.lblNumProducts.Size = new System.Drawing.Size(24, 25);
            this.lblNumProducts.TabIndex = 1;
            this.lblNumProducts.Text = "0";
            // 
            // lblTitleProducts
            // 
            this.lblTitleProducts.AutoSize = true;
            this.lblTitleProducts.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTitleProducts.Location = new System.Drawing.Point(10, 0);
            this.lblTitleProducts.Name = "lblTitleProducts";
            this.lblTitleProducts.Size = new System.Drawing.Size(249, 25);
            this.lblTitleProducts.TabIndex = 0;
            this.lblTitleProducts.Text = "Tổng số lượng sản phẩm";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanelMain.SetColumnSpan(this.tableLayoutPanel1, 2);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.dgvDS, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelStats, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 624);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1268, 202);
            this.tableLayoutPanel1.TabIndex = 3;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // dgvDS
            // 
            this.dgvDS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDS.Location = new System.Drawing.Point(383, 3);
            this.dgvDS.Name = "dgvDS";
            this.dgvDS.RowHeadersWidth = 82;
            this.dgvDS.RowTemplate.Height = 33;
            this.dgvDS.Size = new System.Drawing.Size(882, 196);
            this.dgvDS.TabIndex = 32;
            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.White;
            this.panelStats.Controls.Add(this.lblNumRecievers);
            this.panelStats.Controls.Add(this.lblTitleReceiver);
            this.panelStats.Controls.Add(this.lblNumSuppliers);
            this.panelStats.Controls.Add(this.lblTitleSuppliers);
            this.panelStats.Controls.Add(this.lblTitleStats);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStats.Location = new System.Drawing.Point(3, 3);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(374, 196);
            this.panelStats.TabIndex = 29;
            // 
            // lblNumRecievers
            // 
            this.lblNumRecievers.AutoSize = true;
            this.lblNumRecievers.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNumRecievers.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumRecievers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumRecievers.Location = new System.Drawing.Point(0, 104);
            this.lblNumRecievers.Name = "lblNumRecievers";
            this.lblNumRecievers.Size = new System.Drawing.Size(97, 37);
            this.lblNumRecievers.TabIndex = 5;
            this.lblNumRecievers.Text = "10000";
            // 
            // lblTitleReceiver
            // 
            this.lblTitleReceiver.AutoSize = true;
            this.lblTitleReceiver.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleReceiver.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleReceiver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTitleReceiver.Location = new System.Drawing.Point(0, 83);
            this.lblTitleReceiver.Name = "lblTitleReceiver";
            this.lblTitleReceiver.Size = new System.Drawing.Size(157, 21);
            this.lblTitleReceiver.TabIndex = 4;
            this.lblTitleReceiver.Text = "Số lượng khách hàng";
            // 
            // lblNumSuppliers
            // 
            this.lblNumSuppliers.AutoSize = true;
            this.lblNumSuppliers.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNumSuppliers.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumSuppliers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumSuppliers.Location = new System.Drawing.Point(0, 46);
            this.lblNumSuppliers.Name = "lblNumSuppliers";
            this.lblNumSuppliers.Size = new System.Drawing.Size(97, 37);
            this.lblNumSuppliers.TabIndex = 3;
            this.lblNumSuppliers.Text = "10000";
            // 
            // lblTitleSuppliers
            // 
            this.lblTitleSuppliers.AutoSize = true;
            this.lblTitleSuppliers.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleSuppliers.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleSuppliers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTitleSuppliers.Location = new System.Drawing.Point(0, 25);
            this.lblTitleSuppliers.Name = "lblTitleSuppliers";
            this.lblTitleSuppliers.Size = new System.Drawing.Size(169, 21);
            this.lblTitleSuppliers.TabIndex = 2;
            this.lblTitleSuppliers.Text = "Số lượng nhà cung cấp";
            // 
            // lblTitleStats
            // 
            this.lblTitleStats.AutoSize = true;
            this.lblTitleStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleStats.Font = new System.Drawing.Font("Segoe UI", 7.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblTitleStats.Location = new System.Drawing.Point(0, 0);
            this.lblTitleStats.Name = "lblTitleStats";
            this.lblTitleStats.Size = new System.Drawing.Size(173, 25);
            this.lblTitleStats.TabIndex = 0;
            this.lblTitleStats.Text = "Các thông số khác";
            // 
            // FormDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 829);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "FormDashboard";
            this.Text = "FormDashboardNew";
            this.Load += new System.EventHandler(this.FormDashboard_Load);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.tableLayoutPanelTop.ResumeLayout(false);
            this.tableLayoutPanelTop.PerformLayout();
            this.panelNumExport.ResumeLayout(false);
            this.panelNumExport.PerformLayout();
            this.panelNumImport.ResumeLayout(false);
            this.panelNumImport.PerformLayout();
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.panelNumProducts.ResumeLayout(false);
            this.panelNumProducts.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDS)).EndInit();
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTop;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCustom;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnLast7Days;
        private System.Windows.Forms.Button btnLast30Days;
        private System.Windows.Forms.Button btnThisMonth;
        private System.Windows.Forms.Panel panelNumExport;
        private System.Windows.Forms.Label lblNumExport;
        private System.Windows.Forms.Label lblTitleExport;
        private System.Windows.Forms.Panel panelNumImport;
        private System.Windows.Forms.Label lblNumImport;
        private System.Windows.Forms.Label lblTitleImport;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartReceipts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblNumRecievers;
        private System.Windows.Forms.Label lblTitleReceiver;
        private System.Windows.Forms.Label lblNumSuppliers;
        private System.Windows.Forms.Label lblTitleSuppliers;
        private System.Windows.Forms.Label lblTitleStats;

        private System.Windows.Forms.DataGridView dgvDS;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTopCategories;
        private System.Windows.Forms.Panel panelNumProducts;
        private System.Windows.Forms.Label lblNumProducts;
        private System.Windows.Forms.Label lblTitleProducts;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
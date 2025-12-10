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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnOK = new System.Windows.Forms.Button();
            this.dtpfromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtptoDate = new System.Windows.Forms.DateTimePicker();
            this.labelSanPham = new System.Windows.Forms.Label();
            this.btnLast7Days = new System.Windows.Forms.Button();
            this.btnThisMonth = new System.Windows.Forms.Button();
            this.btnLast30Days = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.btnCustom = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblNumProducts = new System.Windows.Forms.Label();
            this.lblNumImportReciepts = new System.Windows.Forms.Label();
            this.lblNumExportReciepts = new System.Windows.Forms.Label();
            this.chartReceipts = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTopCategories = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblNumSuppliers = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblNumRecievers = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.dgvDS = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartReceipts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopCategories)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDS)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnOK.Location = new System.Drawing.Point(443, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(30, 30);
            this.btnOK.TabIndex = 18;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dtpfromDate
            // 
            this.dtpfromDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromDate.CustomFormat = "MMM dd,yyyy";
            this.dtpfromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpfromDate.Location = new System.Drawing.Point(12, 10);
            this.dtpfromDate.Name = "dtpfromDate";
            this.dtpfromDate.Size = new System.Drawing.Size(200, 31);
            this.dtpfromDate.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.label1.Location = new System.Drawing.Point(24, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(311, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tổng số lượng hóa đơn nhập kho";
            // 
            // dtptoDate
            // 
            this.dtptoDate.CustomFormat = "MMM dd,yyyy";
            this.dtptoDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtptoDate.Location = new System.Drawing.Point(237, 9);
            this.dtptoDate.Name = "dtptoDate";
            this.dtptoDate.Size = new System.Drawing.Size(200, 31);
            this.dtptoDate.TabIndex = 12;
            // 
            // labelSanPham
            // 
            this.labelSanPham.AutoSize = true;
            this.labelSanPham.Font = new System.Drawing.Font("Segoe UI", 7.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSanPham.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.labelSanPham.Location = new System.Drawing.Point(17, 4);
            this.labelSanPham.Name = "labelSanPham";
            this.labelSanPham.Size = new System.Drawing.Size(233, 25);
            this.labelSanPham.TabIndex = 0;
            this.labelSanPham.Text = "Tổng số lượng sản phẩm";
            // 
            // btnLast7Days
            // 
            this.btnLast7Days.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast7Days.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLast7Days.Location = new System.Drawing.Point(731, 9);
            this.btnLast7Days.Name = "btnLast7Days";
            this.btnLast7Days.Size = new System.Drawing.Size(120, 31);
            this.btnLast7Days.TabIndex = 15;
            this.btnLast7Days.Text = "Last 7 days";
            this.btnLast7Days.UseVisualStyleBackColor = true;
            // 
            // btnThisMonth
            // 
            this.btnThisMonth.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThisMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnThisMonth.Location = new System.Drawing.Point(983, 10);
            this.btnThisMonth.Name = "btnThisMonth";
            this.btnThisMonth.Size = new System.Drawing.Size(120, 31);
            this.btnThisMonth.TabIndex = 13;
            this.btnThisMonth.Text = "This month";
            this.btnThisMonth.UseVisualStyleBackColor = true;
            // 
            // btnLast30Days
            // 
            this.btnLast30Days.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast30Days.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnLast30Days.Location = new System.Drawing.Point(857, 9);
            this.btnLast30Days.Name = "btnLast30Days";
            this.btnLast30Days.Size = new System.Drawing.Size(120, 31);
            this.btnLast30Days.TabIndex = 14;
            this.btnLast30Days.Text = "Last 30 days";
            this.btnLast30Days.UseVisualStyleBackColor = true;
            // 
            // btnToday
            // 
            this.btnToday.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnToday.Location = new System.Drawing.Point(605, 9);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(120, 31);
            this.btnToday.TabIndex = 16;
            this.btnToday.Text = "Today";
            this.btnToday.UseVisualStyleBackColor = true;
            // 
            // btnCustom
            // 
            this.btnCustom.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnCustom.Location = new System.Drawing.Point(479, 9);
            this.btnCustom.Name = "btnCustom";
            this.btnCustom.Size = new System.Drawing.Size(120, 31);
            this.btnCustom.TabIndex = 17;
            this.btnCustom.Text = "Custom";
            this.btnCustom.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.label2.Location = new System.Drawing.Point(20, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(306, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tổng số lượng hóa đơn xuất kho";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblNumExportReciepts);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(706, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 77);
            this.panel1.TabIndex = 22;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lblNumImportReciepts);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(284, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(416, 76);
            this.panel2.TabIndex = 23;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.lblNumProducts);
            this.panel3.Controls.Add(this.labelSanPham);
            this.panel3.Location = new System.Drawing.Point(12, 46);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(266, 77);
            this.panel3.TabIndex = 25;
            // 
            // lblNumProducts
            // 
            this.lblNumProducts.AutoSize = true;
            this.lblNumProducts.Font = new System.Drawing.Font("Segoe UI", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumProducts.Location = new System.Drawing.Point(14, 29);
            this.lblNumProducts.Name = "lblNumProducts";
            this.lblNumProducts.Size = new System.Drawing.Size(102, 40);
            this.lblNumProducts.TabIndex = 1;
            this.lblNumProducts.Text = "10000";
            // 
            // lblNumImportReciepts
            // 
            this.lblNumImportReciepts.AutoSize = true;
            this.lblNumImportReciepts.Font = new System.Drawing.Font("Segoe UI", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumImportReciepts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumImportReciepts.Location = new System.Drawing.Point(21, 29);
            this.lblNumImportReciepts.Name = "lblNumImportReciepts";
            this.lblNumImportReciepts.Size = new System.Drawing.Size(102, 40);
            this.lblNumImportReciepts.TabIndex = 2;
            this.lblNumImportReciepts.Text = "10000";
            // 
            // lblNumExportReciepts
            // 
            this.lblNumExportReciepts.AutoSize = true;
            this.lblNumExportReciepts.Font = new System.Drawing.Font("Segoe UI", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumExportReciepts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumExportReciepts.Location = new System.Drawing.Point(17, 29);
            this.lblNumExportReciepts.Name = "lblNumExportReciepts";
            this.lblNumExportReciepts.Size = new System.Drawing.Size(102, 40);
            this.lblNumExportReciepts.TabIndex = 3;
            this.lblNumExportReciepts.Text = "10000";
            // 
            // chartReceipts
            // 
            this.chartReceipts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea3.Name = "ChartArea1";
            this.chartReceipts.ChartAreas.Add(chartArea3);
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend3.Name = "Legend1";
            this.chartReceipts.Legends.Add(legend3);
            this.chartReceipts.Location = new System.Drawing.Point(12, 129);
            this.chartReceipts.Name = "chartReceipts";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartReceipts.Series.Add(series3);
            this.chartReceipts.Size = new System.Drawing.Size(754, 320);
            this.chartReceipts.TabIndex = 26;
            // 
            // chartTopCategories
            // 
            this.chartTopCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea4.Name = "ChartArea1";
            this.chartTopCategories.ChartAreas.Add(chartArea4);
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend4.Name = "Legend1";
            legend4.Title = "Thông số danh mục sản phẩm";
            legend4.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold);
            this.chartTopCategories.Legends.Add(legend4);
            this.chartTopCategories.Location = new System.Drawing.Point(772, 129);
            this.chartTopCategories.Name = "chartTopCategories";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series4.Font = new System.Drawing.Font("Microsoft JhengHei", 10F);
            series4.IsValueShownAsLabel = true;
            series4.LabelForeColor = System.Drawing.Color.White;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartTopCategories.Series.Add(series4);
            this.chartTopCategories.Size = new System.Drawing.Size(344, 519);
            this.chartTopCategories.TabIndex = 27;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.lblNumRecievers);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.lblNumSuppliers);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(12, 455);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(250, 193);
            this.panel4.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.label4.Location = new System.Drawing.Point(17, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Các thông số khác";
            // 
            // lblNumSuppliers
            // 
            this.lblNumSuppliers.AutoSize = true;
            this.lblNumSuppliers.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumSuppliers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumSuppliers.Location = new System.Drawing.Point(14, 50);
            this.lblNumSuppliers.Name = "lblNumSuppliers";
            this.lblNumSuppliers.Size = new System.Drawing.Size(97, 37);
            this.lblNumSuppliers.TabIndex = 3;
            this.lblNumSuppliers.Text = "10000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.label5.Location = new System.Drawing.Point(17, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 21);
            this.label5.TabIndex = 2;
            this.label5.Text = "Số lượng nhà cung cấp";
            // 
            // lblNumRecievers
            // 
            this.lblNumRecievers.AutoSize = true;
            this.lblNumRecievers.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumRecievers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.lblNumRecievers.Location = new System.Drawing.Point(15, 108);
            this.lblNumRecievers.Name = "lblNumRecievers";
            this.lblNumRecievers.Size = new System.Drawing.Size(97, 37);
            this.lblNumRecievers.TabIndex = 5;
            this.lblNumRecievers.Text = "10000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.label7.Location = new System.Drawing.Point(18, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 21);
            this.label7.TabIndex = 4;
            this.label7.Text = "Số lượng khách hàng";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.dgvDS);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Location = new System.Drawing.Point(268, 455);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(498, 193);
            this.panel5.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 7.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.label12.Location = new System.Drawing.Point(12, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(420, 25);
            this.label12.TabIndex = 0;
            this.label12.Text = "Danh sách sản phẩm hết hạn hoặc gần hết hạn";
            // 
            // dgvDS
            // 
            this.dgvDS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDS.Location = new System.Drawing.Point(16, 32);
            this.dgvDS.Name = "dgvDS";
            this.dgvDS.RowHeadersWidth = 82;
            this.dgvDS.RowTemplate.Height = 33;
            this.dgvDS.Size = new System.Drawing.Size(478, 150);
            this.dgvDS.TabIndex = 1;
            // 
            // FormDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 660);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.chartTopCategories);
            this.Controls.Add(this.chartReceipts);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dtpfromDate);
            this.Controls.Add(this.dtptoDate);
            this.Controls.Add(this.btnLast7Days);
            this.Controls.Add(this.btnThisMonth);
            this.Controls.Add(this.btnLast30Days);
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.btnCustom);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.Name = "FormDashboard";
            this.Text = "FormDashboard";
            this.Load += new System.EventHandler(this.FormDashboard_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartReceipts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopCategories)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DateTimePicker dtpfromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtptoDate;
        private System.Windows.Forms.Label labelSanPham;
        private System.Windows.Forms.Button btnLast7Days;
        private System.Windows.Forms.Button btnThisMonth;
        private System.Windows.Forms.Button btnLast30Days;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnCustom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblNumProducts;
        private System.Windows.Forms.Label lblNumExportReciepts;
        private System.Windows.Forms.Label lblNumImportReciepts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartReceipts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTopCategories;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblNumSuppliers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNumRecievers;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgvDS;
    }
}
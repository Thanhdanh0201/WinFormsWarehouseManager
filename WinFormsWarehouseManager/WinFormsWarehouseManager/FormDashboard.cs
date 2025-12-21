using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WinFormsWarehouseManager.db;
using WinFormsWarehouseManager.Models;

namespace WinFormsWarehouseManager
{
    public partial class FormDashboard : Form
    {
        private DashboardSQLite Model;

        public FormDashboard()
        {
            InitializeComponent();
            AddCharts();

            Model = new DashboardSQLite();

            //Default - Last 7 days
            dtpFromDate.Value = DateTime.Today.AddDays(-7);
            dtpToDate.Value = DateTime.Now;
            btnLast7Days.Select();

            // Apply modern styling
            StyleChartsAndDataGrid();
            UpdateButtonSelection(btnLast7Days);

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                bool loaded = Model.LoadData(dtpFromDate.Value, dtpToDate.Value);

                if (!loaded)
                {
                    return;
                }

                LoadNumbers();
                LoadTopCategories();
                LoadCharts();
                LoadExpiredProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi trong LoadData:\n{ex.Message}",
                               "Lỗi",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }

        private void LoadNumbers()
        {
            lblNumProducts.Text = Model.NumProducts.ToString();
            lblNumSuppliers.Text = Model.NumSuppliers.ToString();
            lblNumRecievers.Text = Model.NumRecievers.ToString();
            lblNumExport.Text = Model.NumExportReceipts.ToString();
            lblNumImport.Text = Model.NumImportReceipts.ToString();
        }

        private void LoadTopCategories()
        {
            chartTopCategories.DataSource = Model.TopCategoriesList;

            Series series = chartTopCategories.Series[0];
            series.XValueMember = "Key";
            series.YValueMembers = "Value";

            chartTopCategories.DataBind();

            // Apply custom colors after data binding
            Color[] customColors = new Color[]
            {
                Color.FromArgb(52, 152, 219),   // Blue - Đồ gia dụng
                Color.FromArgb(241, 196, 15),   // Yellow - Đồ dùng văn phòng
                Color.FromArgb(231, 76, 60),    // Red - Linh kiện điện tử
                Color.FromArgb(26, 188, 156),   // Teal - Vật liệu xây dựng
                Color.FromArgb(149, 165, 166),  // Gray - Mỹ phẩm
                Color.FromArgb(46, 204, 113),   // Green - Thực phẩm

                Color.FromArgb(155, 89, 182),   // Purple
                Color.FromArgb(230, 126, 34)    // Orange
            };

            String[] titles = new String[]
            {
                "Đồ gia dụng",
                "Đồ dùng văn phòng",
                "Linh kiện điện tử",
                "Vật liệu xây dựng",
                "Mỹ phẩm",
                "Thực phẩm"
            };


            for (int i = 0; i < series.Points.Count && i < customColors.Length; i++)
            {
                series.Points[i].Color = customColors[i];
                series.Points[i].LegendText = titles[i];
                series.Points[i].BorderWidth = 0;
            }
        }

        private void LoadCharts()
        {
            chartReceipts.Series.Clear();

            // === SERIES IMPORT ===
            Series sImport = new Series("Phiếu Nhập")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                Color = Color.FromArgb(52, 152, 219), // Blue
                BorderWidth = 0
            };

            foreach (var item in Model.ImportChartList)
                sImport.Points.AddXY(item.Date, item.TotalCount);

            // === SERIES EXPORT ===
            Series sExport = new Series("Phiếu Xuất")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                Color = Color.FromArgb(241, 196, 15), // Yellow/Orange
                BorderWidth = 0
            };

            foreach (var item in Model.ExportChartList)
                sExport.Points.AddXY(item.Date, item.TotalCount);

            chartReceipts.Series.Add(sImport);
            chartReceipts.Series.Add(sExport);

            chartReceipts.ChartAreas[0].AxisX.Interval = 1;
            chartReceipts.Legends[0].Enabled = true;
        }

        private void LoadExpiredProducts()
        {
            dgvDS.DataSource = null;
            dgvDS.DataSource = Model.UpcomingExpiredList;

            // Đặt tiêu đề cột cho dễ đọc
            if (dgvDS.Columns.Count > 0)
            {
                dgvDS.Columns["ProductName"].HeaderText = "Tên sản phẩm";
                dgvDS.Columns["HanSuDung"].HeaderText = "Hạn sử dụng";
                dgvDS.Columns["TinhTrang"].HeaderText = "Tình trạng";

                // Format cột ngày
                dgvDS.Columns["HanSuDung"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            UpdateButtonSelection(null);
            LoadData();
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Today;
            dtpToDate.Value = DateTime.Now;
            UpdateButtonSelection(btnToday);
            LoadData();
        }

        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Today.AddDays(-7);
            dtpToDate.Value = DateTime.Now;
            UpdateButtonSelection(btnLast7Days);
            LoadData();
        }

        private void btnLast30Days_Click(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Now;
            UpdateButtonSelection(btnLast30Days);
            LoadData();
        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            dtpFromDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpToDate.Value = DateTime.Now;
            UpdateButtonSelection(btnThisMonth);
            LoadData();
        }

        private void AddCharts()
        {
            // Tạo chartReceipts
            chartReceipts = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartReceipts.Dock = DockStyle.Fill;
            var chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chartArea1.Name = "ChartArea1";
            chartReceipts.ChartAreas.Add(chartArea1);
            var legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            legend1.Name = "Legend1";
            chartReceipts.Legends.Add(legend1);
            tableLayoutPanelMain.Controls.Add(chartReceipts, 1, 1);

            // Tạo chartTopCategories
            chartTopCategories = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartTopCategories.Dock = DockStyle.Fill;
            var chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chartArea2.Name = "ChartArea1";
            chartTopCategories.ChartAreas.Add(chartArea2);
            var legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            legend2.Name = "Legend1";
            chartTopCategories.Legends.Add(legend2);
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series1.Name = "Series1";
            chartTopCategories.Series.Add(series1);

            tableLayoutPanelMain.Controls.Add(chartTopCategories, 0, 1);
        }

        private void tableLayoutPanelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {

        }

        // ============================================================
        // STYLING METHODS
        // ============================================================

        private void StyleChartsAndDataGrid()
        {
            StyleChartReceipts();
            StyleChartTopCategories();
            StyleDataGridView();
        }

        private void StyleChartReceipts()
        {
            chartReceipts.BackColor = Color.White;
            chartReceipts.BorderSkin.SkinStyle = BorderSkinStyle.None;

            if (chartReceipts.ChartAreas.Count > 0)
            {
                ChartArea chartArea = chartReceipts.ChartAreas[0];
                chartArea.BackColor = Color.White;

                // Grid lines
                chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(240, 244, 247);
                chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(240, 244, 247);
                chartArea.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Solid;
                chartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Solid;

                // Axis
                chartArea.AxisX.LineColor = Color.FromArgb(222, 226, 230);
                chartArea.AxisY.LineColor = Color.FromArgb(222, 226, 230);
                chartArea.AxisX.LabelStyle.ForeColor = Color.FromArgb(108, 117, 125);
                chartArea.AxisY.LabelStyle.ForeColor = Color.FromArgb(108, 117, 125);
                chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 9F);
                chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 9F);
                chartArea.AxisX.ArrowStyle = AxisArrowStyle.None;
                chartArea.AxisY.ArrowStyle = AxisArrowStyle.None;

                chartArea.InnerPlotPosition = new ElementPosition(8, 8, 88, 82);
            }

            if (chartReceipts.Legends.Count > 0)
            {
                Legend legend = chartReceipts.Legends[0];
                legend.BackColor = Color.Transparent;
                legend.ForeColor = Color.FromArgb(2, 51, 66);
                legend.Font = new Font("Segoe UI", 9F);
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Far;
                legend.BorderColor = Color.Transparent;
            }

            if (chartReceipts.Titles.Count == 0)
            {
                chartReceipts.Titles.Add(new Title
                {
                    Text = "Thống kê phiếu nhập/xuất",
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(2, 51, 66),
                    Alignment = ContentAlignment.TopLeft,
                    Docking = Docking.Top
                });
            }
        }

        private void StyleChartTopCategories()
        {
            chartTopCategories.BackColor = Color.White;
            chartTopCategories.BorderSkin.SkinStyle = BorderSkinStyle.None;

            if (chartTopCategories.ChartAreas.Count > 0)
            {
                ChartArea chartArea = chartTopCategories.ChartAreas[0];
                chartArea.BackColor = Color.White;
                chartArea.AxisX.MajorGrid.Enabled = false;
                chartArea.AxisY.MajorGrid.Enabled = false;
                chartArea.AxisX.Enabled = AxisEnabled.False;
                chartArea.AxisY.Enabled = AxisEnabled.False;
                chartArea.InnerPlotPosition = new ElementPosition(5, 10, 90, 80);
            }

            if (chartTopCategories.Series.Count > 0)
            {
                Series series = chartTopCategories.Series[0];
                series.ChartType = SeriesChartType.Doughnut;
                series["DoughnutRadius"] = "35";
                series["DoughnutLabelStyle"] = "Outside";
                series.IsValueShownAsLabel = true;
                series.LabelForeColor = Color.FromArgb(2, 51, 66);
                series.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                series.Label = "#VAL";
                series["PieLabelStyle"] = "Outside";
                series.Palette = ChartColorPalette.None;
            }

            if (chartTopCategories.Legends.Count > 0)
            {
                Legend legend = chartTopCategories.Legends[0];
                legend.BackColor = Color.Transparent;
                legend.ForeColor = Color.FromArgb(2, 51, 66);
                legend.Font = new Font("Segoe UI", 9F);
                legend.Docking = Docking.Right;
                legend.Alignment = StringAlignment.Center;
                legend.BorderColor = Color.Transparent;
                legend.LegendStyle = LegendStyle.Column;
            }

            if (chartTopCategories.Titles.Count == 0)
            {
                chartTopCategories.Titles.Add(new Title
                {
                    Text = "Danh mục sản phẩm",
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(2, 51, 66),
                    Alignment = ContentAlignment.TopLeft,
                    Docking = Docking.Top
                });
            }
        }

        private void StyleDataGridView()
        {
            dgvDS.BorderStyle = BorderStyle.None;
            dgvDS.BackgroundColor = Color.White;
            dgvDS.GridColor = Color.FromArgb(240, 244, 247);
            dgvDS.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDS.AllowUserToResizeRows = false;
            dgvDS.RowHeadersVisible = false;
            dgvDS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDS.MultiSelect = false;

            dgvDS.EnableHeadersVisualStyles = false;
            dgvDS.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(2, 51, 66);
            dgvDS.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDS.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvDS.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDS.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            dgvDS.ColumnHeadersHeight = 40;
            dgvDS.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvDS.DefaultCellStyle.BackColor = Color.White;
            dgvDS.DefaultCellStyle.ForeColor = Color.FromArgb(2, 51, 66);
            dgvDS.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvDS.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgvDS.DefaultCellStyle.SelectionForeColor = Color.FromArgb(2, 51, 66);
            dgvDS.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvDS.RowTemplate.Height = 35;

            dgvDS.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvDS.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(2, 51, 66);
            dgvDS.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dgvDS.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(2, 51, 66);

            dgvDS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // ============================================================
        // PAINT METHODS FOR ROUNDED CORNERS
        // ============================================================

        private void PanelCard_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null) return;

            int cornerRadius = 12;
            GraphicsPath path = GetRoundedRectanglePath(panel.ClientRectangle, cornerRadius);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawShadow(e.Graphics, panel.ClientRectangle, cornerRadius);

            using (SolidBrush brush = new SolidBrush(panel.BackColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            using (Pen pen = new Pen(Color.FromArgb(222, 226, 230), 1))
            {
                e.Graphics.DrawPath(pen, path);
            }

            panel.Region = new Region(path);
        }

        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            rect.Inflate(-1, -1);
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void DrawShadow(Graphics g, Rectangle rect, int cornerRadius)
        {
            int shadowOffset = 2;
            Rectangle shadowRect = rect;
            shadowRect.Offset(shadowOffset, shadowOffset);
            shadowRect.Inflate(-1, -1);

            GraphicsPath shadowPath = GetRoundedRectanglePath(shadowRect, cornerRadius);

            using (PathGradientBrush brush = new PathGradientBrush(shadowPath))
            {
                brush.CenterColor = Color.FromArgb(20, 0, 0, 0);
                brush.SurroundColors = new Color[] { Color.FromArgb(0, 0, 0, 0) };
                brush.FocusScales = new PointF(0.8f, 0.8f);
                g.FillPath(brush, shadowPath);
            }
        }

        // ============================================================
        // BUTTON HOVER & SELECTION
        // ============================================================

        private void FilterButton_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;
            btn.BackColor = Color.FromArgb(2, 51, 66);
            btn.ForeColor = Color.White;
        }

        private void FilterButton_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            bool isSelected = false;

            if (btn == btnToday && dtpFromDate.Value.Date == DateTime.Today &&
                dtpToDate.Value.Date == DateTime.Today)
                isSelected = true;
            else if (btn == btnLast7Days && dtpFromDate.Value.Date == DateTime.Today.AddDays(-7) &&
                     dtpToDate.Value.Date >= DateTime.Today)
                isSelected = true;
            else if (btn == btnLast30Days && dtpFromDate.Value.Date == DateTime.Today.AddDays(-30) &&
                     dtpToDate.Value.Date >= DateTime.Today)
                isSelected = true;
            else if (btn == btnThisMonth && dtpFromDate.Value.Date.Day == 1 &&
                     dtpFromDate.Value.Date.Month == DateTime.Today.Month)
                isSelected = true;

            if (!isSelected)
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.FromArgb(2, 51, 66);
            }
        }

        private void UpdateButtonSelection(Button selectedButton)
        {
            Button[] buttons = {  btnToday, btnLast7Days, btnLast30Days, btnThisMonth };

            foreach (Button btn in buttons)
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.FromArgb(2, 51, 66);
            }

            if (selectedButton != null)
            {
                selectedButton.BackColor = Color.FromArgb(2, 51, 66);
                selectedButton.ForeColor = Color.White;
            }
        }
    }
}

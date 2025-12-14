using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            chartTopCategories.Series[0].XValueMember = "Key";
            chartTopCategories.Series[0].YValueMembers = "Value";
            chartTopCategories.DataBind();
        }

        private void LoadCharts()
        {
            chartReceipts.Series.Clear();

            // === SERIES IMPORT ===
            Series sImport = new Series("Phiếu Nhập")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String
            };

            foreach (var item in Model.ImportChartList)
                sImport.Points.AddXY(item.Date, item.TotalCount);

            // === SERIES EXPORT ===
            Series sExport = new Series("Phiếu Xuất")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String
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
            LoadData();
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {

        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Today;
            dtpToDate.Value = DateTime.Now;
            LoadData();
        }

        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Today.AddDays(-7);
            dtpToDate.Value = DateTime.Now;
            LoadData();
        }

        private void btnLast30Days_Click(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Now;
            LoadData();
        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            dtpFromDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpToDate.Value = DateTime.Now;
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
            /*
            chartTopCategories.Titles = "Thong ke danh muc san pham";
            chartTopCategories.
            */
            tableLayoutPanelMain.Controls.Add(chartTopCategories, 0, 1);
           
        }

        private void tableLayoutPanelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void artanPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}

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
using WinFormsWarehouseManager.Models;

namespace WinFormsWarehouseManager
{
    public partial class FormDashboard : Form
    {
        //Fields
        private Dashboard Model; 
        public FormDashboard()
        {
            InitializeComponent();
            Model = new Dashboard();

            //Default - Last 7 days
            dtpfromDate.Value = DateTime.Today.AddDays(-7);
            dtptoDate.Value = DateTime.Now;
            btnLast7Days.Select();

            LoadData();

        }


        private void LoadData()
        {
            /*
            var refreshData = Model.LoadData(dtpfromDate.Value, dtptoDate.Value);

            if (refreshData == true)
            {
                lblNumProducts.Text = Model.NumProducts.ToString();
                lblNumSuppliers.Text = Model.NumSuppliers.ToString();
                lblNumRecievers.Text = Model.NumRecievers.ToString();
                lblNumExportReciepts.Text = Model.NumExportReceipts.ToString();
                lblNumImportReciepts.Text = Model.NumImportReceipts.ToString();
                
                chartTopCategories.DataSource = Model.TopCategoriesList;
                chartTopCategories.Series[0].XValueMember = "Key";
                chartTopCategories.Series[0].YValueMembers = "Value";
                chartTopCategories.DataBind();

                //Thể hiện cả import và export receipts trên cùng 1 biểu đồ
                chartReceipts.Series.Clear();

                // ===== Series Nhập =====
                Series sImport = new Series("Phiếu Nhập");
                sImport.ChartType = SeriesChartType.Column;
                sImport.XValueType = ChartValueType.String;

                foreach (var item in Model.ImportChartList)
                {
                    sImport.Points.AddXY(item.Date, item.TotalCount);
                }

                // ===== Series Xuất =====
                Series sExport = new Series("Phiếu Xuất");
                sExport.ChartType = SeriesChartType.Column;
                sExport.XValueType = ChartValueType.String;

                foreach (var item in Model.ExportChartList)
                {
                    sExport.Points.AddXY(item.Date, item.TotalCount);
                }

                // Add series vào chart
                chartReceipts.Series.Add(sImport);
                chartReceipts.Series.Add(sExport);

                // Format hiển thị
                chartReceipts.ChartAreas[0].AxisX.Interval = 1;
                chartReceipts.Legends[0].Enabled = true;


                //Datagridview
                dgvDS.DataSource = Model.UpcomingExpiredList;

                
                Console.WriteLine("Dashboard data loaded successfully.");
            }
            else
            {
               MessageBox.Show("Loi");
               Console.WriteLine("Failed to load dashboard data.");
            }
            */
            try
            {
                bool loaded = Model.LoadData(dtpfromDate.Value, dtptoDate.Value);

                if (!loaded)
                    return;

                LoadNumbers();
                LoadTopCategories();
                LoadCharts();
                LoadExpiredProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải Dashboard: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // ============================================================
        // BIND DATA LÊN UI
        // ============================================================

        private void LoadNumbers()
        {
            lblNumProducts.Text = Model.NumProducts.ToString();
            lblNumSuppliers.Text = Model.NumSuppliers.ToString();
            lblNumRecievers.Text = Model.NumRecievers.ToString();
            lblNumExportReciepts.Text = Model.NumExportReceipts.ToString();
            lblNumImportReciepts.Text = Model.NumImportReceipts.ToString();
        }

        private void LoadTopCategories()
        {
            chartTopCategories.Series[0].XValueMember = "Key";
            chartTopCategories.Series[0].YValueMembers = "Value";
            chartTopCategories.DataSource = Model.TopCategoriesList;
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
            dgvDS.DataSource = Model.UpcomingExpiredList;
        }



        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panelNCC1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}

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
        private DashboardSQLite Model; 
        public FormDashboard()
        {
            InitializeComponent();
            Model = new DashboardSQLite();

            //Default - Last 7 days
            dtpfromDate.Value = DateTime.Today.AddDays(-7);
            dtptoDate.Value = DateTime.Now;
            btnLast7Days.Select();

            LoadData();

        }


        private void LoadData()
        {
            try
            {
                bool loaded = Model.LoadData(dtpfromDate.Value, dtptoDate.Value);

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
            lblNumExportReciepts.Text = Model.NumExportReceipts.ToString();
            lblNumImportReciepts.Text = Model.NumImportReceipts.ToString();
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



        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panelNCC1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpfromDate.Value = DateTime.Today;
            dtptoDate.Value = DateTime.Now;
            LoadData();
        }

        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            dtpfromDate.Value = DateTime.Today.AddDays(-7);
            dtptoDate.Value = DateTime.Now;
            LoadData();
        }

        private void btnLast30Days_Click(object sender, EventArgs e)
        {
            dtpfromDate.Value = DateTime.Today.AddDays(-30);
            dtptoDate.Value = DateTime.Now;
            LoadData();
        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            dtpfromDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtptoDate.Value = DateTime.Now;
            LoadData();
        }
    }
}

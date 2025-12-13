using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using WinFormsWarehouseManager.db;

namespace WinFormsWarehouseManager.Models
{
    public struct ChartByDate
    {
        public string Date { get; set; }
        public int TotalCount { get; set; }
    }

    public class ExpiredProduct
    {
        public string ProductName { get; set; }
        public DateTime HanSuDung { get; set; }
        public string TinhTrang { get; set; }
    }

    class DashboardSQLite : DbConnection
    {
        private DateTime startDate;
        private DateTime endDate;
        private int numberDays;

        public int NumRecievers { get; private set; }
        public int NumSuppliers { get; private set; }
        public int NumProducts { get; private set; }
        public int NumCategories { get; private set; }
        public int NumImportReceipts { get; private set; }
        public int NumExportReceipts { get; private set; }
        public int NumActivityLogs { get; private set; }
        public int NumNotifications { get; private set; }

        public List<KeyValuePair<string, int>> TopCategoriesList { get; private set; }
        public List<ExpiredProduct> UpcomingExpiredList { get; private set; }

        public List<ChartByDate> ImportChartList { get; private set; }
        public List<ChartByDate> ExportChartList { get; private set; }

        public DashboardSQLite()
        {
            TopCategoriesList = new List<KeyValuePair<string, int>>();
            UpcomingExpiredList = new List<ExpiredProduct>();
            ImportChartList = new List<ChartByDate>();
            ExportChartList = new List<ChartByDate>();
        }

        // ============================== LOAD DATA ==============================
        public bool LoadData(DateTime fromDate, DateTime toDate)
        {
            toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day, toDate.Hour, toDate.Minute, 59);

            if (fromDate != startDate || toDate != endDate)
            {
                startDate = fromDate;
                endDate = toDate;
                numberDays = (int)(endDate - startDate).TotalDays + 1;

                GetNumberData();
                GetImportAnalysis();
                GetExportAnalysis();
                GetProductAnalysis();

                return true;
            }

            return false;
        }

        // ============================== SUPPORT FUNCTION ==============================
        private DataTable LoadDataTable(string query, params SQLiteParameter[] parameters)
        {
            var dt = new DataTable();

            using (var connection = GetConnection())
            using (var adapter = new SQLiteDataAdapter(query, connection))
            {
                if (parameters != null)
                    adapter.SelectCommand.Parameters.AddRange(parameters);

                adapter.Fill(dt);
            }
            return dt;
        }

        private int GetCount(string query, params SQLiteParameter[] parameters)
        {
            DataTable dt = LoadDataTable(query, parameters);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
        }

        // ============================== 1) NUMBER DATA ==============================
        private void GetNumberData()
        {
            NumRecievers = GetCount("SELECT COUNT(*) FROM Receivers");
            NumSuppliers = GetCount("SELECT COUNT(*) FROM Suppliers");
            NumProducts = GetCount("SELECT COUNT(*) FROM Products");
            NumCategories = GetCount("SELECT COUNT(*) FROM Categories");

            NumImportReceipts = GetCount(
                @"SELECT COUNT(*) FROM ImportReceipts 
                  WHERE ImportDate BETWEEN @from AND @to",
                new SQLiteParameter("@from", startDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new SQLiteParameter("@to", endDate.ToString("yyyy-MM-dd HH:mm:ss"))
            );

            NumExportReceipts = GetCount(
                @"SELECT COUNT(*) FROM ExportReceipts 
                  WHERE ExportDate BETWEEN @from AND @to",
                new SQLiteParameter("@from", startDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new SQLiteParameter("@to", endDate.ToString("yyyy-MM-dd HH:mm:ss"))
            );

            NumActivityLogs = GetCount(
                @"SELECT COUNT(*) FROM ActivityLog 
                  WHERE CreatedAt BETWEEN @from AND @to",
                new SQLiteParameter("@from", startDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new SQLiteParameter("@to", endDate.ToString("yyyy-MM-dd HH:mm:ss"))
            );

            NumNotifications = GetCount(
                @"SELECT COUNT(*) FROM Notifications WHERE IsRead = 0"
            );
        }

        // ============================== 2) IMPORT ANALYSIS ==============================
        private void GetImportAnalysis()
        {
            ImportChartList.Clear();

            DataTable dt = LoadDataTable(
                @"SELECT ImportDate, COUNT(*) AS Total 
                  FROM ImportReceipts 
                  WHERE ImportDate BETWEEN @from AND @to
                  GROUP BY date(ImportDate)
                  ORDER BY ImportDate",
                new SQLiteParameter("@from", startDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new SQLiteParameter("@to", endDate.ToString("yyyy-MM-dd HH:mm:ss"))
            );

            List<KeyValuePair<DateTime, int>> raw = new List<KeyValuePair<DateTime, int>>();

            foreach (DataRow row in dt.Rows)
            {
                raw.Add(new KeyValuePair<DateTime, int>(
                    DateTime.Parse(row["ImportDate"].ToString()),
                    Convert.ToInt32(row["Total"])
                ));
            }

            BuildChartData(raw, ImportChartList);
        }

        // ============================== 3) EXPORT ANALYSIS ==============================
        private void GetExportAnalysis()
        {
            ExportChartList.Clear();

            DataTable dt = LoadDataTable(
                @"SELECT ExportDate, COUNT(*) AS Total 
                  FROM ExportReceipts 
                  WHERE ExportDate BETWEEN @from AND @to
                  GROUP BY date(ExportDate)
                  ORDER BY ExportDate",
                new SQLiteParameter("@from", startDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new SQLiteParameter("@to", endDate.ToString("yyyy-MM-dd HH:mm:ss"))
            );

            List<KeyValuePair<DateTime, int>> raw = new List<KeyValuePair<DateTime, int>>();

            foreach (DataRow row in dt.Rows)
            {
                raw.Add(new KeyValuePair<DateTime, int>(
                    DateTime.Parse(row["ExportDate"].ToString()),
                    Convert.ToInt32(row["Total"])
                ));
            }

            BuildChartData(raw, ExportChartList);
        }

        // ============================== BUILD CHART ==============================
        private void BuildChartData(List<KeyValuePair<DateTime, int>> raw, List<ChartByDate> chartList)
        {
            if (numberDays <= 30)
            {
                foreach (var item in raw)
                {
                    chartList.Add(new ChartByDate
                    {
                        Date = item.Key.ToString("dd MMM"),
                        TotalCount = item.Value
                    });
                }
            }
            else if (numberDays <= 92)
            {
                var grouped = raw
                    .GroupBy(x => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                        x.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                    .Select(g => new ChartByDate
                    {
                        Date = $"Week {g.Key}",
                        TotalCount = g.Sum(x => x.Value)
                    });

                chartList.AddRange(grouped);
            }
            else
            {
                var grouped = raw
                    .GroupBy(x => new { x.Key.Year, x.Key.Month })
                    .Select(g => new ChartByDate
                    {
                        Date = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM yyyy"),
                        TotalCount = g.Sum(x => x.Value)
                    });

                chartList.AddRange(grouped);
            }
        }

        // ============================== 4) PRODUCT ANALYSIS ==============================
        private void GetProductAnalysis()
        {
            TopCategoriesList.Clear();
            UpcomingExpiredList.Clear();

            // TOP CATEGORY
            DataTable top = LoadDataTable(
                @"SELECT c.CategoryName, COUNT(p.ProductID) AS ProductCount
                  FROM Categories c
                  INNER JOIN Products p ON c.CategoryID = p.CategoryID
                  WHERE p.NgayNhapKho BETWEEN @from AND @to
                  GROUP BY c.CategoryName
                  HAVING COUNT(p.ProductID) > 0
                  ORDER BY ProductCount DESC
                  LIMIT 5",
                new SQLiteParameter("@from", startDate.ToString("yyyy-MM-dd")),
                new SQLiteParameter("@to", endDate.ToString("yyyy-MM-dd"))
            );

            foreach (DataRow r in top.Rows)
            {
                TopCategoriesList.Add(new KeyValuePair<string, int>(
                    r["CategoryName"].ToString(),
                    Convert.ToInt32(r["ProductCount"])
                ));
            }

            // UPCOMING EXPIRED
            // SQLite: datetime('now', '+30 days') thay cho DATEADD
            // SQLite: julianday() để tính số ngày chênh lệch thay cho DATEDIFF
            DataTable exp = LoadDataTable(
                @"SELECT ProductName, HanSuDung,
                  CASE 
                    WHEN HanSuDung < date('now') 
                    THEN 'Hết hạn'
                    ELSE 'Sắp hết hạn (' || 
                         CAST(CAST(julianday(HanSuDung) - julianday('now') AS INTEGER) AS TEXT) || 
                         ' ngày)'
                  END AS TinhTrang
                  FROM Products
                  WHERE HanSuDung IS NOT NULL
                    AND HanSuDung <= date('now', '+30 days')
                    AND NgayNhapKho BETWEEN @from AND @to
                  ORDER BY 
                    CASE 
                        WHEN HanSuDung < date('now') THEN 0
                        ELSE 1
                    END,
                    HanSuDung ASC",
                new SQLiteParameter("@from", startDate.ToString("yyyy-MM-dd")),
                new SQLiteParameter("@to", endDate.ToString("yyyy-MM-dd"))
            );

            foreach (DataRow r in exp.Rows)
            {
                string productName = r["ProductName"].ToString();
                DateTime hanSuDung = DateTime.Parse(r["HanSuDung"].ToString());
                string tinhTrang = r["TinhTrang"].ToString();

                UpcomingExpiredList.Add(new ExpiredProduct
                {
                    ProductName = productName,
                    HanSuDung = hanSuDung,
                    TinhTrang = tinhTrang
                });
            }
        }
    }
}
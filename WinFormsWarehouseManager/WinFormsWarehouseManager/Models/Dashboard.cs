using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsWarehouseManager.db;

namespace WinFormsWarehouseManager.Models
{
    public struct ChartByDate
    {
        public string Date { get; set; }
        public int TotalCount { get; set; }
    }

    class Dashboard : DbConnection
    {
        // Fields and Properties
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
        public List<KeyValuePair<string, DateTime>> UpcomingExpiredList { get; private set; }

        public List<KeyValuePair<DateTime, int>> ImportAnalysis { get; private set; }
        public int TotalImport { get; private set; }
        public List<ChartByDate> ImportChartList { get; private set; }

        public List<KeyValuePair<DateTime, int>> ExportAnalysis { get; private set; }
        public int TotalExport { get; private set; }
        public List<ChartByDate> ExportChartList { get; private set; }

        public Dashboard()
        {
            ImportChartList = new List<ChartByDate>();
            ExportChartList = new List<ChartByDate>();
        }

        private void GetImportAnalisys()
        {
            ImportAnalysis = new List<KeyValuePair<DateTime, int>>();
            TotalImport = 0;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT ImportDate, COUNT(*) 
                                FROM ImportReceipts 
                                WHERE ImportDate BETWEEN @fromDate AND @toDate 
                                GROUP BY ImportDate";
                    command.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = endDate;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var date = (DateTime)reader[0];
                        var count = (int)reader[1];
                        ImportAnalysis.Add(new KeyValuePair<DateTime, int>(date, count));
                        TotalImport += count;
                    }
                    reader.Close();
                }
            }

            // Group for chart
            ImportChartList.Clear();
            if (numberDays <= 30)
            {
                foreach (var item in ImportAnalysis)
                {
                    ImportChartList.Add(new ChartByDate()
                    {
                        Date = item.Key.ToString("dd MMM"),
                        TotalCount = item.Value
                    });
                }
            }
            else if (numberDays <= 92)
            {
                var grouped = ImportAnalysis
                    .GroupBy(x => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                        x.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                    .Select(g => new { Week = g.Key, Total = g.Sum(x => x.Value) });

                foreach (var item in grouped)
                {
                    ImportChartList.Add(new ChartByDate()
                    {
                        Date = $"Week {item.Week}",
                        TotalCount = item.Total
                    });
                }
            }
            else
            {
                var grouped = ImportAnalysis
                    .GroupBy(x => new { x.Key.Year, x.Key.Month })
                    .Select(g => new {
                        Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                        Total = g.Sum(x => x.Value)
                    });

                foreach (var item in grouped)
                {
                    ImportChartList.Add(new ChartByDate()
                    {
                        Date = item.Month.ToString("MMM yyyy"),
                        TotalCount = item.Total
                    });
                }
            }
        }

        private void GetExportAnalisys()
        {
            ExportAnalysis = new List<KeyValuePair<DateTime, int>>();
            TotalExport = 0;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT ExportDate, COUNT(*) 
                                FROM ExportReceipts 
                                WHERE ExportDate BETWEEN @fromDate AND @toDate 
                                GROUP BY ExportDate";
                    command.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = endDate;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var date = (DateTime)reader[0];
                        var count = (int)reader[1];
                        ExportAnalysis.Add(new KeyValuePair<DateTime, int>(date, count));
                        TotalExport += count;
                    }
                    reader.Close();
                }
            }

            // Group for chart
            ExportChartList.Clear();
            if (numberDays <= 30)
            {
                foreach (var item in ExportAnalysis)
                {
                    ExportChartList.Add(new ChartByDate()
                    {
                        Date = item.Key.ToString("dd MMM"),
                        TotalCount = item.Value
                    });
                }
            }
            else if (numberDays <= 92)
            {
                var grouped = ExportAnalysis
                    .GroupBy(x => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                        x.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                    .Select(g => new { Week = g.Key, Total = g.Sum(x => x.Value) });

                foreach (var item in grouped)
                {
                    ExportChartList.Add(new ChartByDate()
                    {
                        Date = $"Week {item.Week}",
                        TotalCount = item.Total
                    });
                }
            }
            else
            {
                var grouped = ExportAnalysis
                    .GroupBy(x => new { x.Key.Year, x.Key.Month })
                    .Select(g => new {
                        Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                        Total = g.Sum(x => x.Value)
                    });

                foreach (var item in grouped)
                {
                    ExportChartList.Add(new ChartByDate()
                    {
                        Date = item.Month.ToString("MMM yyyy"),
                        TotalCount = item.Total
                    });
                }
            }
        }
        private void GetProductAnalisys(DateTime fromDate, DateTime toDate)
        {
            TopCategoriesList = new List<KeyValuePair<string, int>>();

            UpcomingExpiredList = new List<KeyValuePair<string, DateTime>>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    // 1. Top danh mục sản phẩm trong khoảng thời gian nhập kho
                    command.CommandText = @"
                SELECT c.CategoryName, COUNT(p.ProductID) AS ProductCount
                FROM Categories c
                LEFT JOIN Products p ON c.CategoryID = p.CategoryID
                WHERE p.NgayNhapKho BETWEEN @fromDate AND @toDate
                GROUP BY c.CategoryName
                ORDER BY ProductCount DESC";
                    command.Parameters.Clear();
                    command.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = fromDate;
                    command.Parameters.Add("@toDate", SqlDbType.DateTime).Value = toDate;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        TopCategoriesList.Add(new KeyValuePair<string, int>(
                            reader.GetString(0), reader.GetInt32(1)));
                    }
                    reader.Close();

                    

                    // 3. Sản phẩm sắp hết hạn hoặc đã hết hạn (HanSuDung <= toDate)
                    command.CommandText = @"
                SELECT ProductName, HanSuDung
                FROM Products
                WHERE HanSuDung IS NOT NULL
                  AND HanSuDung <= @toDate
                  AND NgayNhapKho BETWEEN @fromDate AND @toDate
                ORDER BY HanSuDung ASC";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        UpcomingExpiredList.Add(new KeyValuePair<string, DateTime>(
                            reader.GetString(0), reader.GetDateTime(1)));
                    }
                    reader.Close();
                }
            }
        }

    }

}




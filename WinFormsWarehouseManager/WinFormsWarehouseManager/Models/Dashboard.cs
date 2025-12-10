/*using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            TopCategoriesList = new List<KeyValuePair<string, int>>();
            UpcomingExpiredList = new List<KeyValuePair<string, DateTime>>();
        }

        //Phương thức chính để load data
        public bool LoadData(DateTime fromDate, DateTime toDate) 
        {
            
            toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day, toDate.Hour, toDate.Minute, 59); 
            if (fromDate != this.startDate || toDate != this.endDate) 
            { 
                startDate = fromDate; 
                endDate = toDate; 
                numberDays = (int)(endDate - startDate).TotalDays + 1; 
                GetNumberData(); 
                GetProductAnalysis(fromDate, toDate); 
                GetImportAnalysis(); 
                GetExportAnalysis(); 
                Console.WriteLine("Data refreshed for new query: {0} - {1}", fromDate, toDate); 
                return true ; 
            } 
            else 
            { 
                Console.WriteLine("Data not refreshed, same query: {0} - {1}", fromDate, toDate);
                return false ; 
            } 
            
        }


        //Lấy các số liệu thống kê tổng quan
        private void GetNumberData()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    // Số lượng người nhận
                    command.CommandText = "SELECT COUNT(*) FROM Receivers";
                    NumRecievers = (int)command.ExecuteScalar();

                    // Số lượng nhà cung cấp
                    command.CommandText = "SELECT COUNT(*) FROM Suppliers";
                    NumSuppliers = (int)command.ExecuteScalar();

                    // Số lượng sản phẩm
                    command.CommandText = "SELECT COUNT(*) FROM Products";
                    NumProducts = (int)command.ExecuteScalar();

                    // Số lượng danh mục
                    command.CommandText = "SELECT COUNT(*) FROM Categories";
                    NumCategories = (int)command.ExecuteScalar();

                    // Số phiếu nhập trong khoảng thời gian
                    command.CommandText = @"SELECT COUNT(*) FROM ImportReceipts 
                                           WHERE ImportDate BETWEEN @fromDate AND @toDate";
                    command.Parameters.Clear();
                    command.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", SqlDbType.DateTime).Value = endDate;
                    NumImportReceipts = (int)command.ExecuteScalar();

                    // Số phiếu xuất trong khoảng thời gian
                    command.CommandText = @"SELECT COUNT(*) FROM ExportReceipts 
                                           WHERE ExportDate BETWEEN @fromDate AND @toDate";
                    NumExportReceipts = (int)command.ExecuteScalar();

                    // Số lượng activity logs
                    command.CommandText = @"SELECT COUNT(*) FROM ActivityLogs 
                                           WHERE CreatedAt BETWEEN @fromDate AND @toDate";
                    NumActivityLogs = (int)command.ExecuteScalar();

                    // Số lượng thông báo chưa đọc
                    command.CommandText = "SELECT COUNT(*) FROM Notifications WHERE IsRead = 0";
                    command.Parameters.Clear();
                    NumNotifications = (int)command.ExecuteScalar();
                }
            }
        }

        private void GetImportAnalysis()
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
                                GROUP BY ImportDate
                                ORDER BY ImportDate";
                    command.Parameters.Clear(); // ✅ THÊM
                    command.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", SqlDbType.DateTime).Value = endDate;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var date = reader.GetDateTime(0);
                        var count = reader.GetInt32(1);
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
                    .Select(g => new { Week = g.Key, Total = g.Sum(x => x.Value) })
                    .OrderBy(x => x.Week);

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
                    })
                    .OrderBy(x => x.Month);

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


        private void GetExportAnalysis()
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
                                GROUP BY ExportDate
                                ORDER BY ExportDate";
                    command.Parameters.Clear();
                    command.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", SqlDbType.DateTime).Value = endDate;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var date = reader.GetDateTime(0);
                        var count = reader.GetInt32(1);
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
                    .Select(g => new { Week = g.Key, Total = g.Sum(x => x.Value) })
                    .OrderBy(x => x.Week);

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
                    })
                    .OrderBy(x => x.Month);

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

        private void GetProductAnalysis(DateTime fromDate, DateTime toDate)
        {
            TopCategoriesList.Clear();
            UpcomingExpiredList.Clear();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    // 1. Top 5 danh mục sản phẩm trong khoảng thời gian nhập kho
                    command.CommandText = @"
                        SELECT TOP 5 c.CategoryName, COUNT(p.ProductID) AS ProductCount
                        FROM Categories c
                        INNER JOIN Products p ON c.CategoryID = p.CategoryID
                        WHERE p.NgayNhapKho BETWEEN @fromDate AND @toDate
                        GROUP BY c.CategoryName
                        HAVING COUNT(p.ProductID) > 0
                        ORDER BY ProductCount DESC";
                    command.Parameters.Clear();
                    command.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = fromDate;
                    command.Parameters.Add("@toDate", SqlDbType.DateTime).Value = toDate;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string categoryName = reader.IsDBNull(0) ? "N/A" : reader.GetString(0);
                        int productCount = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                        TopCategoriesList.Add(new KeyValuePair<string, int>(categoryName, productCount));
                    }
                    reader.Close();

                    // 2. Sản phẩm sắp hết hạn hoặc đã hết hạn (trong 30 ngày)
                    command.CommandText = @"
                        SELECT ProductName,HanSuDung,
                        CASE 
                        WHEN HanSuDung < GETDATE() 
                        THEN N'Hết hạn'
                        ELSE 
                        N'Sắp hết hạn (' 
                        + CAST(DATEDIFF(DAY, GETDATE(), HanSuDung) AS NVARCHAR(10)) 
                        + N' ngày)'
                        END AS TinhTrang
                        FROM Products
                        WHERE 
                            HanSuDung IS NOT NULL
                            AND HanSuDung <= DATEADD(DAY, 30, GETDATE())   -- chỉ lấy sp đã hết hạn hoặc sắp hết hạn <= 30 ngày
                            AND NgayNhapKho BETWEEN @fromDate AND @toDate
                        ORDER BY 
                            CASE 
                                WHEN HanSuDung < GETDATE() THEN 0  -- hết hạn xếp trước
                                ELSE 1                             -- sắp hết hạn xếp sau
                            END,
                            HanSuDung ASC;
                        ";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string productName = reader.IsDBNull(0) ? "N/A" : reader.GetString(0);
                        DateTime expiredDate = reader.GetDateTime(1);
                        UpcomingExpiredList.Add(new KeyValuePair<string, DateTime>(productName, expiredDate));
                    }
                    reader.Close();
                }
            }

        }
    }
}

*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

    class Dashboard : DbConnection
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
        public List<KeyValuePair<string, DateTime>> UpcomingExpiredList { get; private set; }

        public List<ChartByDate> ImportChartList { get; private set; }
        public List<ChartByDate> ExportChartList { get; private set; }

        public Dashboard()
        {
            TopCategoriesList = new List<KeyValuePair<string, int>>();
            UpcomingExpiredList = new List<KeyValuePair<string, DateTime>>();
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
        private DataTable LoadDataTable(string query, params SqlParameter[] parameters)
        {
            var dt = new DataTable();

            using (var connection = GetConnection())
            using (var adapter = new SqlDataAdapter(query, connection))
            {
                if (parameters != null)
                    adapter.SelectCommand.Parameters.AddRange(parameters);

                adapter.Fill(dt);
            }
            return dt;
        }

        private int GetCount(string query, params SqlParameter[] parameters)
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
                new SqlParameter("@from", startDate),
                new SqlParameter("@to", endDate)
            );

            NumExportReceipts = GetCount(
                @"SELECT COUNT(*) FROM ExportReceipts 
                  WHERE ExportDate BETWEEN @from AND @to",
                new SqlParameter("@from", startDate),
                new SqlParameter("@to", endDate)
            );

            NumActivityLogs = GetCount(
                @"SELECT COUNT(*) FROM ActivityLogs 
                  WHERE CreatedAt BETWEEN @from AND @to",
                new SqlParameter("@from", startDate),
                new SqlParameter("@to", endDate)
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
                  GROUP BY ImportDate
                  ORDER BY ImportDate",
                new SqlParameter("@from", startDate),
                new SqlParameter("@to", endDate)
            );

            List<KeyValuePair<DateTime, int>> raw = new List<KeyValuePair<DateTime, int>>();

            foreach (DataRow row in dt.Rows)
            {
                raw.Add(new KeyValuePair<DateTime, int>(
                    Convert.ToDateTime(row["ImportDate"]),
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
                  GROUP BY ExportDate
                  ORDER BY ExportDate",
                new SqlParameter("@from", startDate),
                new SqlParameter("@to", endDate)
            );

            List<KeyValuePair<DateTime, int>> raw = new List<KeyValuePair<DateTime, int>>();

            foreach (DataRow row in dt.Rows)
            {
                raw.Add(new KeyValuePair<DateTime, int>(
                    Convert.ToDateTime(row["ExportDate"]),
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
                @"SELECT TOP 5 c.CategoryName, COUNT(p.ProductID) AS ProductCount
                  FROM Categories c
                  INNER JOIN Products p ON c.CategoryID = p.CategoryID
                  WHERE p.NgayNhapKho BETWEEN @from AND @to
                  GROUP BY c.CategoryName
                  HAVING COUNT(p.ProductID) > 0
                  ORDER BY ProductCount DESC",
                new SqlParameter("@from", startDate),
                new SqlParameter("@to", endDate)
            );

            foreach (DataRow r in top.Rows)
            {
                TopCategoriesList.Add(new KeyValuePair<string, int>(
                    r["CategoryName"].ToString(),
                    Convert.ToInt32(r["ProductCount"])
                ));
            }

            // UPCOMING EXPIRED
            DataTable exp = LoadDataTable(
                @"SELECT ProductName, HanSuDung
                  FROM Products
                  WHERE HanSuDung IS NOT NULL
                    AND HanSuDung <= DATEADD(DAY,30,GETDATE())
                    AND NgayNhapKho BETWEEN @from AND @to
                  ORDER BY HanSuDung ASC",
                new SqlParameter("@from", startDate),
                new SqlParameter("@to", endDate)
            );

            foreach (DataRow r in exp.Rows)
            {
                UpcomingExpiredList.Add(new KeyValuePair<string, DateTime>(
                    r["ProductName"].ToString(),
                    Convert.ToDateTime(r["HanSuDung"])
                ));
            }
        }
    }
}

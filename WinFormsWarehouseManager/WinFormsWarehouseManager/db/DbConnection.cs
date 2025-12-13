using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsWarehouseManager.db
{
    public abstract class DbConnection
    {
        private readonly string connectionString;

        public DbConnection()
        {
            // Đường dẫn đến file SQLite.db trong thư mục bin\Debug
            string dbPath = System.IO.Path.Combine(Application.StartupPath, "SQLite.db");
            connectionString = $"Data Source={dbPath};Version=3;";
        }

        protected SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace WinFormsWarehouseManager.db
{
    public abstract class DbConnection
    {
        private readonly string connectionString;
        public DbConnection()
        {
            connectionString = "Server=danhnguyen6f42\\mssqlserver03;Database=QuanLyKhoHang;Integrated Security=True;";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}

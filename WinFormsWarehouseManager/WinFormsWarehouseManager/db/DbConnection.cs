using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

namespace WinFormsWarehouseManager.db
{
        public abstract class DbConnection
        {
            private readonly string connectionString;
            public DbConnection()
            {
            connectionString = @"Server=localhost;Database=QuanLyKhoHang;Integrated Security=True;Encrypt=False";
        }
        protected SqlConnection GetConnection()
            {
                return new SqlConnection(connectionString);
            }
    }

}

using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WinFormsWarehouseManager.db
{
    public class DatabaseHelper : DbConnection
    {
        /// <summary>
        /// Thực thi câu lệnh SELECT và trả về DataTable
        /// Dùng cho DataGridView và Chart
        /// </summary>
        /// <param name="query">Câu lệnh SQL SELECT</param>
        /// <param name="parameters">Tham số (tùy chọn)</param>
        /// <returns>DataTable chứa kết quả hoặc null nếu có lỗi</returns>
        public DataTable ExecuteQuery(string query, SQLiteParameter[] parameters = null)
        {
            try
            {
                using (SQLiteConnection conn = GetConnection())
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        DataTable dt = new DataTable();
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn dữ liệu:\n{ex.Message}",
                    "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Thực thi câu lệnh INSERT, UPDATE, DELETE
        /// </summary>
        /// <param name="query">Câu lệnh SQL</param>
        /// <param name="parameters">Tham số (tùy chọn)</param>
        /// <returns>Số dòng bị ảnh hưởng, hoặc -1 nếu có lỗi</returns>
        public int ExecuteNonQuery(string query, SQLiteParameter[] parameters = null)
        {
            try
            {
                using (SQLiteConnection conn = GetConnection())
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thực thi câu lệnh:\n{ex.Message}",
                    "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        /// <summary>
        /// Thực thi câu lệnh trả về 1 giá trị duy nhất
        /// Dùng cho COUNT, SUM, MAX, MIN, AVG...
        /// </summary>
        /// <param name="query">Câu lệnh SQL</param>
        /// <param name="parameters">Tham số (tùy chọn)</param>
        /// <returns>Giá trị kết quả hoặc null nếu có lỗi</returns>
        public object ExecuteScalar(string query, SQLiteParameter[] parameters = null)
        {
            try
            {
                using (SQLiteConnection conn = GetConnection())
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        object result = cmd.ExecuteScalar();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lấy giá trị:\n{ex.Message}",
                    "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Kiểm tra kết nối database có thành công không
        /// </summary>
        /// <returns>true nếu kết nối thành công, false nếu thất bại</returns>
        public bool TestConnection()
        {
            try
            {
                using (SQLiteConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối database:\n{ex.Message}",
                    "Lỗi Kết Nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
using System;
using System.IO;
using Newtonsoft.Json;
using WinFormsWarehouseManager.Models;

namespace WinFormsWarehouseManager.Utils
{
    /// <summary>
    /// Quản lý file JSON lưu danh sách nhập tạm
    /// </summary>
    public static class TempImportManager
    {
        private static readonly string AppDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "WarehouseManager"
        );

        private static readonly string TempFilePath = Path.Combine(AppDataFolder, "temp_import_list.json");

        /// <summary>
        /// Đảm bảo thư mục tồn tại
        /// </summary>
        private static void EnsureDirectoryExists()
        {
            if (!Directory.Exists(AppDataFolder))
            {
                Directory.CreateDirectory(AppDataFolder);
            }
        }

        /// <summary>
        /// Lưu dữ liệu vào file JSON
        /// </summary>
        public static bool Save(TempImportData data)
        {
            try
            {
                EnsureDirectoryExists();

                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(TempFilePath, json);

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    $"Lỗi khi lưu file tạm:\n{ex.Message}",
                    "Lỗi File",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error
                );
                return false;
            }
        }

        /// <summary>
        /// Đọc dữ liệu từ file JSON
        /// </summary>
        public static TempImportData Load()
        {
            try
            {
                if (!File.Exists(TempFilePath))
                {
                    return null;
                }

                string json = File.ReadAllText(TempFilePath);
                TempImportData data = JsonConvert.DeserializeObject<TempImportData>(json);

                return data;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    $"Lỗi khi đọc file tạm:\n{ex.Message}",
                    "Lỗi File",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error
                );
                return null;
            }
        }

        /// <summary>
        /// Xóa file tạm
        /// </summary>
        public static bool Delete()
        {
            try
            {
                if (File.Exists(TempFilePath))
                {
                    File.Delete(TempFilePath);
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    $"Lỗi khi xóa file tạm:\n{ex.Message}",
                    "Lỗi File",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error
                );
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra file tạm có tồn tại không
        /// </summary>
        public static bool Exists()
        {
            return File.Exists(TempFilePath);
        }

        /// <summary>
        /// Lấy đường dẫn file tạm (cho debug)
        /// </summary>
        public static string GetFilePath()
        {
            return TempFilePath;
        }
    }
}
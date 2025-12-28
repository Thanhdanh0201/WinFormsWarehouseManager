using System;
using System.IO;
using Newtonsoft.Json;
using WinFormsWarehouseManager.Models;

namespace WinFormsWarehouseManager.Utils
{
    /// <summary>
    /// Quản lý lưu/đọc dữ liệu xuất kho tạm vào file JSON
    /// </summary>
    public static class TempExportManager
    {
        private static readonly string TempFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "WarehouseManager",
            "temp_export.json"
        );

        static TempExportManager()
        {
            string directory = Path.GetDirectoryName(TempFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// Lưu dữ liệu xuất tạm vào file
        /// </summary>
        public static void Save(TempExportData data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(TempFilePath, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lưu temp export: {ex.Message}");
            }
        }

        /// <summary>
        /// Đọc dữ liệu xuất tạm từ file
        /// </summary>
        public static TempExportData Load()
        {
            try
            {
                if (!File.Exists(TempFilePath))
                    return null;

                string json = File.ReadAllText(TempFilePath);
                return JsonConvert.DeserializeObject<TempExportData>(json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi đọc temp export: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Xóa file dữ liệu tạm
        /// </summary>
        public static void Delete()
        {
            try
            {
                if (File.Exists(TempFilePath))
                {
                    File.Delete(TempFilePath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi xóa temp export: {ex.Message}");
            }
        }

        /// <summary>
        /// Kiểm tra file tạm có tồn tại không
        /// </summary>
        public static bool Exists()
        {
            return File.Exists(TempFilePath) && new FileInfo(TempFilePath).Length > 0;
        }
    }
}
using System;
using System.Collections.Generic;

namespace WinFormsWarehouseManager.Models
{
    /// <summary>
    /// Container cho toàn bộ dữ liệu nhập tạm (bao gồm Supplier và Items)
    /// </summary>
    public class TempImportData
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string ImportDate { get; set; } // Format: yyyy-MM-dd HH:mm:ss
        public List<TempImportItem> Items { get; set; }

        public TempImportData()
        {
            ImportDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Items = new List<TempImportItem>();
        }

        public TempImportData(int supplierID, string supplierName)
        {
            SupplierID = supplierID;
            SupplierName = supplierName;
            ImportDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Items = new List<TempImportItem>();
        }
    }
}
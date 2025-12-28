using System;
using System.Collections.Generic;

namespace WinFormsWarehouseManager.Models
{
    /// <summary>
    /// Container cho toàn bộ dữ liệu xuất tạm (bao gồm Receiver và Items)
    /// </summary>
    public class TempExportData
    {
        public int ReceiverID { get; set; }
        public string ReceiverName { get; set; }
        public string ExportDate { get; set; } // Format: yyyy-MM-dd HH:mm:ss
        public List<TempExportItem> Items { get; set; }

        public TempExportData()
        {
            ExportDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Items = new List<TempExportItem>();
        }

        public TempExportData(int receiverID, string receiverName)
        {
            ReceiverID = receiverID;
            ReceiverName = receiverName;
            ExportDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Items = new List<TempExportItem>();
        }
    }
}

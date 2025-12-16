using System;

namespace WinFormsWarehouseManager.Models
{
    public class ProductInfo
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SoLuong { get; set; }
        public string DonViTinh { get; set; }
        public string HanSuDung { get; set; }
        public string NgayNhapKho { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
    }
}
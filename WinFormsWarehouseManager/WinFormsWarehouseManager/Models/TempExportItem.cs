using System;

namespace WinFormsWarehouseManager.Models
{
    /// <summary>
    /// Model cho mỗi sản phẩm trong danh sách xuất kho tạm
    /// </summary>
    public class TempExportItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuongTonKho { get; set; } // Số lượng hiện có trong kho
        public int ReceiverID { get; set; }
        public string ReceiverName { get; set; }

        public TempExportItem()
        {
        }

        public TempExportItem(int productID, string productName, int categoryID, string categoryName,
            int quantity, string donViTinh, int soLuongTonKho, int receiverID, string receiverName)
        {
            ProductID = productID;
            ProductName = productName;
            CategoryID = categoryID;
            CategoryName = categoryName;
            Quantity = quantity;
            DonViTinh = donViTinh;
            SoLuongTonKho = soLuongTonKho;
            ReceiverID = receiverID;
            ReceiverName = receiverName;
        }

        public bool IsValid(out string errorMessage)
        {
            if (ProductID <= 0)
            {
                errorMessage = "Sản phẩm không hợp lệ!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(ProductName))
            {
                errorMessage = "Tên sản phẩm không được để trống!";
                return false;
            }

            if (CategoryID <= 0)
            {
                errorMessage = "Danh mục không hợp lệ!";
                return false;
            }

            if (Quantity <= 0)
            {
                errorMessage = "Số lượng phải lớn hơn 0!";
                return false;
            }

            if (Quantity > SoLuongTonKho)
            {
                errorMessage = $"Số lượng xuất ({Quantity}) vượt quá tồn kho ({SoLuongTonKho})!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(DonViTinh))
            {
                errorMessage = "Đơn vị tính không được để trống!";
                return false;
            }

            if (ReceiverID <= 0)
            {
                errorMessage = "Người nhận không hợp lệ!";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
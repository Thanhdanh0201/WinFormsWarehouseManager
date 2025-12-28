public class TempImportItem
{
    public int? ProductID { get; set; }
    public string ProductName { get; set; }
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
    public int Quantity { get; set; }
    public string DonViTinh { get; set; }
    public string HanSuDung { get; set; }
    public bool IsNewProduct { get; set; }

    // THÊM 2 FIELDS NÀY
    public int SupplierID { get; set; }
    public string SupplierName { get; set; }

    public TempImportItem()
    {
    }

    // Sửa constructor
    public TempImportItem(int? productID, string productName, int categoryID, string categoryName,
        int quantity, string donViTinh, string hanSuDung, bool isNewProduct,
        int supplierID, string supplierName)
    {
        ProductID = productID;
        ProductName = productName;
        CategoryID = categoryID;
        CategoryName = categoryName;
        Quantity = quantity;
        DonViTinh = donViTinh;
        HanSuDung = hanSuDung;
        IsNewProduct = isNewProduct;
        SupplierID = supplierID;
        SupplierName = supplierName;
    }

    public bool IsValid(out string errorMessage)
    {
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

        if (string.IsNullOrWhiteSpace(DonViTinh))
        {
            errorMessage = "Đơn vị tính không được để trống!";
            return false;
        }

        if (string.IsNullOrWhiteSpace(HanSuDung))
        {
            errorMessage = "Hạn sử dụng không hợp lệ!";
            return false;
        }

        errorMessage = string.Empty;
        return true;
    }
}
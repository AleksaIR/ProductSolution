namespace ProductGUI.Models
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? SupplierID { get; set; }
        public string SupplierName { get; set; }
    }
}
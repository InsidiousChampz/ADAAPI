namespace TEMPLETEAPI.Models.Product
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int StockCount { get; set; }

        //ProductGroupId is a FK from ProductGroup ===
        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }
        //==============================================
    }
}
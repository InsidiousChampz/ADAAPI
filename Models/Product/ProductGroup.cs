using System.Collections.Generic;

namespace STANDARDAPI.Models.Product
{
    public class ProductGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace STANDARDAPI.Models.Product
{
    public class ProductGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public List<Product> Products { get; set; }
    }
}
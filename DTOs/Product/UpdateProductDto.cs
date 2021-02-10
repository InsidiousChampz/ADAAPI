using System;

namespace STANDARDAPI.DTOs.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int StockCount { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public int ProductGroupId { get; set; }
    }
}
using System;
using STANDARDAPI.Models.Product;

namespace STANDARDAPI.DTOs.Product
{
    public class GetProductDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int StockCount { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public int ProductGroupId { get; set; }
    }
}
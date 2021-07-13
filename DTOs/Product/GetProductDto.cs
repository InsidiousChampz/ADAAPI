using System;
using System.Collections.Generic;
using CustomerProFileAPI.Models.Product;

namespace CustomerProFileAPI.DTOs.Product
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int StockCount { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public int ProductGroupId { get; set; }
        public GetProductGroupDto ProductGroup { get; set; }

    }
}
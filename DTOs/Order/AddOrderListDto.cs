using System;

namespace CustomerProFileAPI.DTOs.Order
{
    public class AddOrderListDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }
    }
}
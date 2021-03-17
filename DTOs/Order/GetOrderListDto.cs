using System;

namespace STANDARDAPI.DTOs.Order
{
    public class GetOrderListDto
    {
        public string Ordernumber { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public int OrderId { get; set; }
        public GetOrderDto Order { get; set; }
    }
}
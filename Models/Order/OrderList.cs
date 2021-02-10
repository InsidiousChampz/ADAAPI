using System;

namespace STANDARDAPI.Models.Order
{
    public class OrderList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }

        //ProductGroupId is a FK from ProductGroup ===
        public int OrderId { get; set; }
        public Order Order { get; set; }
        //==============================================
    }
}
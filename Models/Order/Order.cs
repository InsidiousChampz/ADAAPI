using System;
using System.Collections.Generic;

namespace SmsUpdateCustomer_Api.Models.Order
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateOrder { get; set; }
        public int ItemCount { get; set; }
        public int Total { get; set; }
        public int Discount { get; set; }
        public int Net { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }

        //OrderListID is a FK from OrderList ===
        public string OrderNumber { get; set; }
        public List<OrderList> OrderLists { get; set; }
        //==============================================
    }
}
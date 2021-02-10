using System;
using System.Collections.Generic;
using STANDARDAPI.Models.Order;

namespace STANDARDAPI.DTOs.Order
{
    public class GetOrderDto
    {
        public DateTime DateOrder { get; set; }
        public int ItemCount { get; set; }
        public int Total { get; set; }
        public int Discount { get; set; }
        public int Net { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public List<GetOrderListDto> OrderLists { get; set; }

    }
}
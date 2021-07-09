using System;
using System.Collections.Generic;
using INFOEDITORAPI.Models.Order;

namespace INFOEDITORAPI.DTOs.Order
{
    public class AddOrderDto
    {
        public int ItemCount { get; set; }
        public int Total { get; set; }
        public int Discount { get; set; }
        public int Net { get; set; }


    }
}
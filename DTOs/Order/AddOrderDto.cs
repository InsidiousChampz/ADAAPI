using System;
using System.Collections.Generic;
using STANDARDAPI.Models.Order;

namespace STANDARDAPI.DTOs.Order
{
    public class AddOrderDto
    {
        public int ItemCount { get; set; }
        public int Total { get; set; }
        public int Discount { get; set; }
        public int Net { get; set; }


    }
}
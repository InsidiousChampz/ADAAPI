using System;
using System.ComponentModel.DataAnnotations;
using SmsUpdateCustomer_Api.Validations;
namespace SmsUpdateCustomer_Api.DTOs.Order
{
    public class GetOrderFilterDto : PaginationDto
    {
        public DateTime? DateOrder { get; set; }
        public int ItemCount { get; set; }
        public int Total { get; set; }
        public int Discount { get; set; }
        public int Net { get; set; }
        public string CreateBy { get; set; }
        public string OrderNumber { get; set; }
        public bool? Status { get; set; }
        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; } = true;
    }
}
using System;
using System.Collections.Generic;

namespace SmsUpdateCustomer_Api.DTOs.Product
{
    public class GetProductGroupFilterDto : PaginationDto
    {
        //Filter
        public string Name { get; set; }
        public string CreateBy { get; set; }
        public bool? Status { get; set; }
        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; } = true;

    }
}
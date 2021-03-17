using System;

namespace STANDARDAPI.DTOs.Order
{
    public class GetOrderDateFilterDTO : PaginationDto
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; } = true;

    }
}
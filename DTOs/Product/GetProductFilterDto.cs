using System;
using System.Collections.Generic;

namespace STANDARDAPI.DTOs.Product
{
    public class GetProductFilterDto : PaginationDto
    {
        //Filter
        public string Name { get; set; }
        public int? Price { get; set; }
        public int? StockCount { get; set; }
        public int? ProductGroupId { get; set; }

        //Ordering
        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; } = true;
    }
}
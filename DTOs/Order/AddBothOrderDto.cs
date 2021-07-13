using System.Collections.Generic;

namespace CustomerProFileAPI.DTOs.Order
{
    /// <summary>
    /// Add both Order 
    /// </summary>
    public class AddBothOrderDto
    {
        public AddOrderDto Header { get; set; }
        public List<AddOrderListDto> Detail { get; set; }
    }
}
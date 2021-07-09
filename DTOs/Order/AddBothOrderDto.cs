using System.Collections.Generic;

namespace INFOEDITORAPI.DTOs.Order
{
    public class AddBothOrderDto
    {
        public AddOrderDto Header { get; set; }
        public List<AddOrderListDto> Detail { get; set; }
    }
}
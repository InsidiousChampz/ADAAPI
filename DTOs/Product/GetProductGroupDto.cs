using System.Collections.Generic;

namespace TEMPLETEAPI.DTOs.Product
{
    public class GetProductGroupDto
    {
        public string Name { get; set; }
        public List<GetProductDto> Products { get; set; }

    }
}
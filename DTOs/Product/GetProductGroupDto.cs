using System;
using System.Collections.Generic;

namespace CustomerProFileAPI.DTOs.Product
{
    public class GetProductGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public List<GetProductDto> Products { get; set; }

    }
}
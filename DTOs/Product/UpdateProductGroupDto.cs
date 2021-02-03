using System;

namespace STANDARDAPI.DTOs.Product
{
    public class UpdateProductGroupDto
    {
        public string Name { get; set; }
        // public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
    }
}
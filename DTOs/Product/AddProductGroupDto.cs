using System;
using System.ComponentModel.DataAnnotations;
using STANDARDAPI.Validations;

namespace STANDARDAPI.DTOs.Product
{
    public class AddProductGroupDto
    {
        [FirstLetterUpperCaseAttribute]
        [Required]
        public string Name { get; set; }
        //public string CreateBy { get; set; }
        //public DateTime CreateDate { get; set; }
        //public bool Status { get; set; }
    }
}
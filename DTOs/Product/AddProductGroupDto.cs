using System;
using System.ComponentModel.DataAnnotations;
using CustomerProFileAPI.Validations;

namespace CustomerProFileAPI.DTOs.Product
{
    public class AddProductGroupDto
    {
        [FirstLetterUpperCaseAttribute]
        [Required]
        public string Name { get; set; }
    }
}
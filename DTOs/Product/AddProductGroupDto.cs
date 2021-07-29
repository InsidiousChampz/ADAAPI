using System;
using System.ComponentModel.DataAnnotations;
using SmsUpdateCustomer_Api.Validations;

namespace SmsUpdateCustomer_Api.DTOs.Product
{
    public class AddProductGroupDto
    {
        [FirstLetterUpperCaseAttribute]
        [Required]
        public string Name { get; set; }
    }
}
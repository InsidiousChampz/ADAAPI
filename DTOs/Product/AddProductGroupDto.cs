using System;
using System.ComponentModel.DataAnnotations;
using INFOEDITORAPI.Validations;

namespace INFOEDITORAPI.DTOs.Product
{
    public class AddProductGroupDto
    {
        [FirstLetterUpperCaseAttribute]
        [Required]
        public string Name { get; set; }
    }
}
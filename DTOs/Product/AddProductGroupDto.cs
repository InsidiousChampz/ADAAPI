using System.ComponentModel.DataAnnotations;
using STANDARDAPI.Validations;

namespace STANDARDAPI.DTOs.Product
{
    public class AddProductGroupDto
    {
        [FirstLetterUpperCaseAttribute]
        [Required]
        public string Name { get; set; }
    }
}
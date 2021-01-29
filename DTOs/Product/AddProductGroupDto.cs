using System.ComponentModel.DataAnnotations;
using NetCoreAPI_Template_v2.Validations;

namespace TEMPLETEAPI.DTOs.Product
{
    public class AddProductGroupDto
    {
        [FirstLetterUpperCaseAttribute]
        [Required]
        public string Name { get; set; }
    }
}
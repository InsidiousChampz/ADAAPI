using System.ComponentModel.DataAnnotations;
using CustomerProFileAPI.Validations;

namespace CustomerProFileAPI.DTOs.Product
{
    public class AddProductDto
    {
        [FirstLetterUpperCaseAttribute]
        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int StockCount { get; set; }

        [Required]
        public int ProductGroupId { get; set; }

    }
}
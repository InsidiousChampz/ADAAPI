using System.ComponentModel.DataAnnotations;
using STANDARDAPI.Validations;

namespace STANDARDAPI.DTOs.Product
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

        //public string CreateBy { get; set; }
        //public DateTime CreateDate { get; set; }
        //public bool Status { get; set; }

        [Required]
        public int ProductGroupId { get; set; }

    }
}
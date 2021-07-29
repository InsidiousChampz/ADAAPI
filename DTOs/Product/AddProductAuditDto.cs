using System.ComponentModel.DataAnnotations;
using SmsUpdateCustomer_Api.Validations;

namespace SmsUpdateCustomer_Api.DTOs.Product
{
    public class AddProductAuditDto
    {
        [FirstLetterUpperCaseAttribute]
        [Required]
        public string Name { get; set; }
        [Required]
        public int StockCount { get; set; }
        [Required]
        public int AuditAmount { get; set; }
        [Required]
        public int AuditTotalAmount { get; set; }
        [Required]
        public int ProductGroupId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ProductAuditTypeId { get; set; }
        public string Remark { get; set; }
    }


}
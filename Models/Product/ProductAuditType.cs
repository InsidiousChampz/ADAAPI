using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmsUpdateCustomer_Api.Models.Product
{
    public class ProductAuditType
    {
        public int Id { get; set; }
        public string NameType { get; set; }
        public List<ProductAudit> ProductAudits { get; set; }
    }
}
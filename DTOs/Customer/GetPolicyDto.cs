using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.DTOs.Customer
{
    public class GetPolicyDto
    {
        [StringLength(255)]
        public string ApplicationCode { get; set; }

        [StringLength(255)]
        public string ProductType { get; set; }

        [StringLength(255)]
        public string Product { get; set; }

        [Column(TypeName = "decimal(16, 2)")]
        public decimal? Premium { get; set; }

        public int? CustPersonId { get; set; }

        public Guid? Customer_guid { get; set; }

        [StringLength(255)]
        public string CustName { get; set; }

        public int? PersonId { get; set; }

        public Guid? Payer_guid { get; set; }

        [StringLength(255)]
        public string PayerName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Admin
{
    public class AddAdminTransactionDto
    {
        [StringLength(255)]
        public string UserId { get; set; }

        [StringLength(1000)]
        public string BeforeChange { get; set; }

        [StringLength(1000)]
        public string AfterChange { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; }
    }
}

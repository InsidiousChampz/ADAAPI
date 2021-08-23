using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Admin
{
    public class GetLoginCustomerDto : PaginationDto
    {
        public int PersonId { get; set; }       
        [StringLength(40)]
        public string FirstName { get; set; }
        
        [StringLength(40)]
        public string LastName { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(13)]
        public string IdentityCard { get; set; }

        [StringLength(40)]
        public string PrimaryPhone { get; set; }

        [StringLength(13)]
        public string LoginIdentityCard { get; set; }

        [StringLength(100)]
        public string LoginLastName { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; }
        
        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; } = true;
    }
}

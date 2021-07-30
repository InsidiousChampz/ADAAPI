using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Admin
{
    public class GetEditCustomerDto : PaginationDto
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
        public bool IsAgentConfirm { get; set; }
        public bool IsCustomerReply { get; set; } = false;
        public bool IsSMSSended { get; set; } = false;
        [Column(TypeName = "datetime")]
        public DateTime ReplyDate { get; set; } = new DateTime(1900 - 01 - 01);
        [Column(TypeName = "datetime")]
        public DateTime SentDate { get; set; } = new DateTime(1900 - 01 - 01);
        [StringLength(255)]
        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; } = true;
    }
}

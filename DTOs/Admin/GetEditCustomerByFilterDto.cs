using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Admin
{
    public class GetEditCustomerByFilterDto: PaginationDto
    {

        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(13)]
        public string IdentityCard { get; set; }
        
        [StringLength(40)]
        public string PrimaryPhone { get; set; }

        public bool? IsConfirm { get; set; }

        public string OrderingField { get; set; }

        public bool AscendingOrder { get; set; } = true;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Admin
{
    public class GetPolicyCustomerFilterDto : PaginationDto
    {
        public int PersonId { get; set; }
        public string OrderingField { get; set; }

        public bool AscendingOrder { get; set; } = true;

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Admin
{
    public class GetMergeByFilterDto : PaginationDto
    {
        [StringLength(20)]
        public string Caption { get; set; }

        public int PersonId { get; set; }

        public string OrderingField { get; set; }

        public bool AscendingOrder { get; set; } = true;
    }
}

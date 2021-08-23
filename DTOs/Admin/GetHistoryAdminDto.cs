using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Admin
{
    public class GetHistoryAdminDto
    {
        public string UserId { get; set; }

        public int PersonId { get; set; }

        [StringLength(255)]
        public string CustomerName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastUpdated { get; set; }

        [StringLength(1000)]
        public string BeforeChange { get; set; }

        [StringLength(1000)]
        public string AfterChange { get; set; }
    }
}

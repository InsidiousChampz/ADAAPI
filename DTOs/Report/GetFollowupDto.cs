using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Report
{
    public class GetFollowupDto
    {
        public DateTime? FollowupDateStart { get; set; }

        public DateTime? FollowupDateEnd { get; set; }
    }
}

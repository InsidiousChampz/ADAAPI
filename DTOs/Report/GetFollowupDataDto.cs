using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Report
{
    public class GetFollowupDataDto
    {
        public DateTime InformDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public int? PersonId { get; set; }
        public string PayerName { get; set; }
        public string OrganizeName { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Branch { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Report
{
    public class GetFollowupEditDataDto
    {
        public int PersonId { get; set; }
        public string PayerName { get; set; }
        public string OrganizeName { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Area { get; set; }
        public int BranchId { get; set; }
        public string Branch { get; set; }
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public string AppId { get; set; }
        public string PrimaryPhone { get; set; }
        public bool CustomerConfirm { get; set; }

    }
}

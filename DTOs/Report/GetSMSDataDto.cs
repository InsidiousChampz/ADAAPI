using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Report
{
    public class GetSMSDataDto
    {
        public int PersonId  { get; set; }
        public string PrimaryPhone { get; set; }
        //public string FullName { get; set; }

        //[Column(TypeName = "date")]
        //public DateTime Birthdate { get; set; }

        public DateTime? DateSMSSended { get; set; }

        [StringLength(255)]
        public string SMSResult { get; set; }

        [StringLength(255)]
        public string SMSCause { get; set; }

        public bool IsCustomerReply { get; set; } = false;

        public DateTime? ReplyDate { get; set; }

        public int NumberofSended { get; set; } = 0;






    }
}

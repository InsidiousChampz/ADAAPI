using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Admin
{
    public class GetPolicyCustomerDataDto 
    {
        public string ProductType { get; set; }
        public string ApplicationCode { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartCoverDate { get; set; }
        public string AppStatus { get; set; }
        public string CustomerType { get; set; }
    }
}

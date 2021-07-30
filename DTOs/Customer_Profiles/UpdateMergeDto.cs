using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Customer_Profiles
{
    public class UpdateMergeDto
    {
        public int personId { get; set; }
        public string ListMergFrom { get; set; }
        public string ListMergeTo { get; set; }


    }
}

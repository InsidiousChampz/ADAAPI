using SmsUpdateCustomer_Api.DTOs.Customer_Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Admin
{
    public class ConfirmAdminDto
    {
        public AddProfileDto PersonData { get; set; }
        public UpdateMergeDto MergeData { get; set; }
    }
}

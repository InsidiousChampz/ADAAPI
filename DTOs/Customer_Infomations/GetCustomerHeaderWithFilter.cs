using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Customer_Infomations
{
    public class GetCustomerHeaderWithFilter
    {
        [StringLength(13)]
        public string LoginIdentityCard { get; set; }

        [StringLength(100)]
        public string LoginLastName { get; set; }

    }
}

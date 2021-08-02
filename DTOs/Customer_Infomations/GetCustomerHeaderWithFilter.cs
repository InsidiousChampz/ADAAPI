using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Customer_Infomations
{
    public class GetCustomerHeaderWithFilter
    {
        [Required(ErrorMessage = "IdentityCard is not correct."), StringLength(13)]
        public string LoginIdentityCard { get; set; }

        [Required(ErrorMessage = "LastName is invalid."), StringLength(100)]
        public string LoginLastName { get; set; }

    }
}

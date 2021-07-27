using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Customer_Profiles
{
    public class AddHotlineDto
    {
        public int? PersonId { get; set; }
        
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(40)]
        public string PrimaryPhone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}

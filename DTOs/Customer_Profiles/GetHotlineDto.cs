using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Customer_Profiles
{
    public class GetHotlineDto
    {
        public int PersonId { get; set; }

        [Required(ErrorMessage = "FirstName is invalid."), StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is invalid."), StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "PrimaryPhone is not invalid."), StringLength(40)]
        public string PrimaryPhone { get; set; }

        [StringLength(255), RegularExpression("^(.+)@(.+)$", ErrorMessage = "Email Format is not Correct.")]
        public string Email { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Customer_Profiles
{
    public class GetProfileDto : PaginationDto
    {
        public int? PersonId { get; set; }
        public Guid Customer_guid { get; set; }
        public int? TitleId { get; set; }

        [Required(ErrorMessage = "FirstName is invalid."), StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is invalid."), StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Birthdate is not correct.")]
        [Column(TypeName = "date")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "IdentityCard is not correct."), StringLength(13)]
        public string IdentityCard { get; set; }

        [Required(ErrorMessage = "PrimaryPhone is not invalid."), StringLength(40)]
        public string PrimaryPhone { get; set; }

        [StringLength(40)]
        public string SecondaryPhone { get; set; }

        [StringLength(255), RegularExpression("^(.+)@(.+)$", ErrorMessage = "Email Format is not Correct.")]
        public string Email { get; set; }

        [StringLength(255)]
        public string LineID { get; set; }

        public bool IsUpdated { get; set; }

        public DateTime LastUpdated { get; set; }

        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; } = true;

    }
}

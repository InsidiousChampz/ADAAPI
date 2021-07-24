using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.DTOs.Customer_Profiles
{
    public class GetProfileDto
    {
        public int? PersonId { get; set; }
        public Guid Customer_guid { get; set; }
        public int? TitleId { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime Birthdate { get; set; }

        [StringLength(13)]
        public string IdentityCard { get; set; }

        [StringLength(40)]
        public string PrimaryPhone { get; set; }

        [StringLength(40)]
        public string SecondaryPhone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string LineID { get; set; }

     
    }
}

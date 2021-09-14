using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Admin
{
    public class DataCenterDTO
    {
        [StringLength(1000)]
        public string PersonIdList { get; set; }

        [StringLength(1000)]
        public string ToPersonId { get; set; }

        public int? TitleId { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string IdentityCard { get; set; }

        [Column(TypeName = "date")]
        public DateTime Birthdate { get; set; }

        [StringLength(40)]
        public string PrimaryPhone { get; set; }

        [StringLength(40)]
        public string SecondaryPhone { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string LineID { get; set; }

        [StringLength(20)]
        public string DocumentCode { get; set; }

        public int UpdatedByUserId { get; set; }
    }
}

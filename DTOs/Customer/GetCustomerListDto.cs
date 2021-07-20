using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.DTOs.Customer
{
    public class GetCustomerListDto
    {
        public int? PersonId { get; set; }
        public Guid Customer_guid { get; set; }
        public int? TitleId { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [Column(TypeName = "datetime")]
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

        public int? WorkAddressId { get; set; }

        [StringLength(255)]
        public string WorkAddressName { get; set; }

        [StringLength(255)]
        public string WorkAddress1 { get; set; }

        [StringLength(255)]
        public string WorkAddress2 { get; set; }

        [StringLength(20)]
        public string WorkAddressSubDistrictCode { get; set; }

        [StringLength(255)]
        public string WorkAddressSubDistrict { get; set; }

        [StringLength(255)]
        public string WorkAddressDistrict { get; set; }

        [StringLength(255)]
        public string WorkAddressProvince { get; set; }

        [StringLength(5)]
        public string WorkAddressZipCode { get; set; }

        public List<GetPolicyDto> Policies { get; set; }

    }
}

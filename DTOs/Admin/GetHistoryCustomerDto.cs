using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Customer_Profiles
{
    public class GetHistoryCustomerDto
    {
        public int PersonId { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        public DateTime LastUpdate { get; set; }
        [StringLength(255)]
        public string FieldData { get; set; }
        [StringLength(255)]
        public string BeforeChanged { get; set; }
        [StringLength(255)]
        public string AfterChanged { get; set; }
    }
}

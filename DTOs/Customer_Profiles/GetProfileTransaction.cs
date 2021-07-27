using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Customer_Profiles
{
    public class GetProfileTransaction
    {
        public int PersonId { get; set; }

        [StringLength(255)]
        public string FieldData { get; set; }
        [StringLength(255)]
        public string BeforeChange { get; set; }
        [StringLength(255)]
        public string AfterChange { get; set; }

        public int EditorId { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}

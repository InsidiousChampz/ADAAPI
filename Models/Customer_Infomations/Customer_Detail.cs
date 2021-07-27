using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Models.Customer_Infomations
{
    [Table("DetailCustomer", Schema = "ifo")]
    public class Customer_Detail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailCustomerID { get; set; }
        public int PayerID { get; set; }
        public int PersonId { get; set; }

        [StringLength(20)]
        public string PersonType { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; }

        
    }
}

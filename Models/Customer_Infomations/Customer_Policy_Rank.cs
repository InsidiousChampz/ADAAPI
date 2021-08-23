using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Models.Customer_Infomations
{
    [Table("RankPolicyCustomer", Schema = "ifo")]
    public class Customer_Policy_Rank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(100)]
        public string ProductTypeDetail { get; set; }
        public int? ProductTypeRank { get; set; }
    }
}

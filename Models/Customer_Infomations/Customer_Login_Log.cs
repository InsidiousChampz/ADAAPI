using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Models.Customer_Infomations
{
    [Table("CustomerLoginLog", Schema = "dbo")]
    public class Customer_Login_Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(13)]
        public string IdentityCard { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LoginDate { get; set; }

    }
}

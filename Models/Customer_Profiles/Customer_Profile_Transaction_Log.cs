using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Models.Customer_Profiles
{
    [Table("CustomerTransactionLogs", Schema = "scp")]
    public class Customer_Profile_Transaction_Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PersonId { get; set; }

        public int EditorId { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(1000)]
        public string BeforeChange { get; set; }

        [StringLength(1000)]
        public string AfterChange { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }

        
    }
}

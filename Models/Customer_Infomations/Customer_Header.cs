using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Models.Customer_Infomations
{
    [Table("HeaderCustomer", Schema = "ifo")]
    public class Customer_Header
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HeaderCustomerID { get; set; }
        public int PayerPersonId { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(13)]
        public string IdentityCard { get; set; }

        [Column(TypeName = "date")]
        public DateTime Birthdate { get; set; }

        [StringLength(13)]
        public string LoginIdentityCard { get; set; }

        [StringLength(100)]
        public string LoginLastName { get; set; }

        [StringLength(255)]
        public string LoginRefCode { get; set; }

        [StringLength(255)]
        public string SMSFormat { get; set; }

        [StringLength(40)]
        public string PrimaryPhone { get; set; }
        public bool IsAgentConfirm { get; set; } = false;
        public bool IsCustomerReply { get; set; } = false;
        public bool IsSMSSended { get; set; } = false;

        [Column(TypeName = "datetime")]
        public DateTime ConfirmDate { get; set; } = new DateTime(1900 - 01 - 01);

        [Column(TypeName = "datetime")]
        public DateTime ReplyDate { get; set; } = new DateTime(1900 - 01 - 01);

        [Column(TypeName = "datetime")]
        public DateTime SentDate { get; set; } = new DateTime(1900 - 01 - 01);

        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; } = new DateTime(1900 - 01 - 01);





    }
}

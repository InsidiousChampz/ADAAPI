using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Models.Customer_Infomations
{
    [Table("FollowupCustomer", Schema = "ifo")]
    public class Customer_FollowUp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PersonId { get; set; }
        [StringLength(100)]
        public string PayerFirstName { get; set; }
        [StringLength(100)]
        public string PayerLastName { get; set; }
        [StringLength(13)]
        public string PrimaryPhone { get; set; }
        [StringLength(255)]
        public string OrganizeName { get; set; }
        [StringLength(255)]
        public string District { get; set; }
        [StringLength(255)]
        public string Province { get; set; }
        [StringLength(255)]
        public string Area { get; set; }
        public int BranchId { get; set; }
        [StringLength(255)]
        public string Branch { get; set; }
        public int AgentId { get; set; }
        [StringLength(100)]
        public string AgentName { get; set; }
        [StringLength(50)]
        public string AppID { get; set; }
        public bool Result { get; set; }
        public bool AdminConfirm { get; set; } = false;
        public bool CustomerConfirm { get; set; } = false;
        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; }

    }
}

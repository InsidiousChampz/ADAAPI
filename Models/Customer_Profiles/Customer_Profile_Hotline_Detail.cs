using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Models.Customer_Profiles
{
    [Table("CustomerHotlineDetail", Schema = "scp")]
    public class Customer_Profile_Hotline_Detail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int HotlineId { get; set; }
        [StringLength(255)]
        public string HotlineDescriptions { get; set; }
    }
}

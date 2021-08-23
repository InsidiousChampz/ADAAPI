using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Models.Customer_Profiles
{

        [Table("CustomerHotline", Schema = "scp")]  
        public class Customer_Profile_Hotline
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public int? PersonId { get; set; }

            [Required(ErrorMessage = "FirstName is invalid."), StringLength(100)]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "LastName is invalid."), StringLength(100)]
            public string LastName { get; set; }

           [Required(ErrorMessage = "PrimaryPhone is not invalid."), StringLength(40)]
            public string PrimaryPhone { get; set; }

            [StringLength(255), RegularExpression("^(.+)@(.+)$", ErrorMessage = "Email Format is not Correct.")]
            public string Email { get; set; }

            [StringLength(255)]
            public string Remark { get; set; }

            public int TypeHotLine { get; set; } = 0;

            [Column(TypeName = "datetime")]
            public DateTime InformDate { get; set; }

            [Column(TypeName = "datetime")]
            public DateTime LastUpdated { get; set; }
        }
}

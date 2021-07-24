using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.Models.Customer_Snapshots
{
    [Table("PayerSnapshot", Schema ="ss")]
    public class Payer_Snapshot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public Guid Customer_guid { get; set; }
        public int? TitleId { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime Birthdate { get; set; }

        [StringLength(13)]
        public string IdentityCard { get; set; }

        [StringLength(40)]
        public string PrimaryPhone { get; set; }

        [StringLength(40)]
        public string SecondaryPhone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string LineID { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; }

        //Link for Table Policy_SnapShot
        public virtual List<Policy_Snapshot> Policies { get; set; }
        


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.Models.Customer_Snapshots
{
    [Table("SnapPolicy", Schema = "ss")]
    public class Policy_Snapshot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255)]
        public string ApplicationCode { get; set; }

        [StringLength(255)]
        public string ProductType { get; set; }

        [StringLength(255)]
        public string Product { get; set; }

        [Column(TypeName = "decimal(16, 2)")]
        public decimal? Premium { get; set; }

        public int? CustPersonId { get; set; }

        public Guid? Customer_guid { get; set; }

        [StringLength(255)]
        public string CustName { get; set; }

        public int? PersonId { get; set; } //PayerPersonId

        public Guid? Payer_guid { get; set; }

        [StringLength(255)]
        public string PayerName { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; }

        public int? Customer_SnapshotId { get; set; }
        public virtual Customer_Snapshot Customer_Snapshot { get; set; }

    }
}

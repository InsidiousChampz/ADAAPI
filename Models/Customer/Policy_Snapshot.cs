using SmsUpdateCustomer_Api.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Models.Customer_Snapshots
{
    [Table("PolicySnapshot", Schema = "ss")]
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
        public string ProductTypeDetail { get; set; }

        [StringLength(255)]
        public string Product { get; set; }

        [Column(TypeName = "decimal(16, 2)")]
        public decimal? Premium { get; set; }

        public int? CustPersonId { get; set; }

        public Guid? Customer_guid { get; set; }

        [StringLength(255)]
        public string CustName { get; set; }
        
        public int? PayerPersonId { get; set; } 

        public Guid? Payer_guid { get; set; }

        [StringLength(255)]
        public string PayerName { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; }

        //.. new from miw.....
        [StringLength(255)]
        public string CustBuildingName { get; set; }
        [StringLength(255)]
        public string CustSubDistrict { get; set; }
        [StringLength(255)]
        public string CustDistrict { get; set; }
        [StringLength(255)]
        public string CustProvince { get; set; }
        [StringLength(255)]
        public string PayerBuildingName { get; set; }
        [StringLength(255)]
        public string PayerSubDistrict { get; set; }
        [StringLength(255)]
        public string PayerDistrict { get; set; }
        [StringLength(255)]
        public string PayerProvince { get; set; }
        [StringLength(255)]
        public string PayerBranch { get; set; }
        [StringLength(255)]
        public int? PayerBranchId { get; set; }
        [StringLength(255)]
        public string PayerStudyArea { get; set; }
        [StringLength(255)]
        public int? PayerStudyAreaId { get; set; }
        [StringLength(255)]
        public string SaleName { get; set; }
        [StringLength(255)]
        public string SaleCode { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartCoverDate { get; set; }
        [StringLength(255)]
        public string AppStatus { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CancelDate { get; set; }

        [StringLength(50)]
        public string CustPrimaryPhone { get; set; }

        [StringLength(50)]
        public string PayerPrimaryPhone { get; set; }

        [StringLength(250)]
        public string PaymentType { get; set; }

        //....................
        public int? Payer_SnapshotId { get; set; }
        //Ref back to Customer
        public virtual Customer_Snapshot CustomerDetail { get; set; }
        
    }
}

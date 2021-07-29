using System;
using System.ComponentModel.DataAnnotations;

namespace SmsUpdateCustomer_Api.Models.Product
{
    public class ProductAudit
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int StockCount { get; set; }         //Real quantity in Stock
        public int AuditAmount { get; set; }        //Quantity you need add or remove
        public int AuditTotalAmount { get; set; }   //Toal quantity after calculate, So this field will be update into StockCount after Submit also
        public string Remark { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }

        //ProductAuditTypeId is a FK from ProductAuditType ===
        public int? ProductAuditTypeId { get; set; }
        public ProductAuditType ProductAuditType { get; set; }
        //====================================================

        //ProductId is a FK from Product =====================
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        //====================================================

        //ProductId is a FK from ProductGroup =====================
        public int? ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }
        //====================================================
    }
}
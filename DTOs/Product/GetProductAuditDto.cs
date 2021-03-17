using System;

namespace STANDARDAPI.DTOs.Product
{
    public class GetProductAuditDto
    {
        public string Name { get; set; }
        public int StockCount { get; set; }
        public int AuditAmount { get; set; }
        public int AuditTotalAmount { get; set; }
        public string Remark { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int ProductId { get; set; }
        public int ProductGroupId { get; set; }
        public int ProductAuditTypeId { get; set; }


    }
}
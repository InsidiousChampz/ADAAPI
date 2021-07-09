using System.ComponentModel.DataAnnotations;
using INFOEDITORAPI.Validations;

namespace INFOEDITORAPI.DTOs.Product
{
    public class GetProductAuditFilterDto : PaginationDto
    {
        public string Name { get; set; }
        public string Remark { get; set; }
        public string CreateBy { get; set; }
        public int? ProductGroupId { get; set; }
        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; } = true;
    }
}
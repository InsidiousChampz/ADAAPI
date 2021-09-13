using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Admin
{
    public class GetResultSubDocumentDataDto
    {
        public string documentId { get; set; }
        public string documentFileId { get; set; }
        public int runningNo { get; set; }
        public string documentTypeId { get; set; }
        public string documentTypeName { get; set; }
        public string pathThumbnailImg { get; set; }
        public string pathFullDoc { get; set; }
    }
}

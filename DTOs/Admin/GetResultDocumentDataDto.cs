using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Admin
{
    public class GetResultDocumentDataDto
    { 
        public List<GetResultSubDocumentDataDto> Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

    }
}

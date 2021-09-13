using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Admin
{
    public class GetResultDataCenterDto
    {
        public bool IsResult { get; set; }
        public string Result { get; set; }
        public string Msg { get; set; }
    }
}

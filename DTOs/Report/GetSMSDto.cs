using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Report
{
    public class GetSMSDto
    {
        public DateTime? SendedDateStart { get; set; }

        public DateTime? SendedDateEnd { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ADAAPI.DTOs.Admin
{
    public class GetTableDTO
    {
        [StringLength(5)]
        public string empId { get; set; }
        [StringLength(50)]
        public string empFirstname { get; set; }
        [StringLength(50)]
        public string empLastname { get; set; }
        [StringLength(10)]
        public string empPhoneNumber { get; set; }
    }
}

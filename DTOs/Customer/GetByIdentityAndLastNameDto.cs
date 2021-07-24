using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.DTOs.Customer
{
    public class GetByIdentityAndLastNameDto
    {
        [StringLength(13)]
        public string IdentityCard { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        
    }
}

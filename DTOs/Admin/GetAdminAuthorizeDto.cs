using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.DTOs.Admin
{
    public class GetAdminAuthorizeDto
    {
        public int Id { get; set; }

        public int AdminId { get; set; }

        public Guid Admin_guid { get; set; }

        public string AdminFirstName { get; set; }

        public string AdminLastName { get; set; }

        public string AdminUserIdFromAuth { get; set; }

        public bool Active { get; set; } = true;

        [Column(TypeName = "datetime")]
        public DateTime? LastUpdated { get; set; }

    }
}

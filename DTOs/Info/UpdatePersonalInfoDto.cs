﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace INFOEDITORAPI.DTOs.Info
{
    public interface UpdatePersonalInfoDto
    {

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Firstname is Missing"), StringLength(500, MinimumLength = 0)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Firstname is Missing"), StringLength(500, MinimumLength = 0)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Genre must be specified")]
        public string Genre { get; set; }


        [Required(ErrorMessage = "Email is Missing"), RegularExpression("^(.+)@(.+)$")]
        public string Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime BirthDate { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
    }
}
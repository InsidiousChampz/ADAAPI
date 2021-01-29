using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NetCoreAPI_Template_v2.Validations;
using TEMPLETEAPI.Models;

namespace TEMPLETEAPI.DTOs
{
    public class AddSkillDto
    {
        [FirstLetterUpperCaseAttribute]
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 100)]
        public int Damage { get; set; }

    }
}
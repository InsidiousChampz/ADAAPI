using System.ComponentModel.DataAnnotations;
using NetCoreAPI_Template_v2.Validations;

namespace TEMPLETEAPI.DTOs
{
    public class AddWeaponDto
    {
        [FirstLetterUpperCaseAttribute]
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 100)]
        public int Damage { get; set; }
        public int CharacterId { get; set; }
    }
}
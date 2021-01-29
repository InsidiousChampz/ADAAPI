using System.ComponentModel.DataAnnotations;
using NetCoreAPI_Template_v2.Validations;

namespace TEMPLETEAPI.DTOs
{
    public class AddCharacterDto
    {
        [FirstLetterUpperCaseAttribute]
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 9999)]
        public int HitPoints { get; set; }
        [Required]
        [Range(1, 255)]
        public int Strength { get; set; }
        [Required]
        [Range(1, 255)]
        public int Defense { get; set; }
        [Required]
        [Range(1, 255)]
        public int Intelligence { get; set; }
    }
}
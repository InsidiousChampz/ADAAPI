using System.ComponentModel.DataAnnotations.Schema;

namespace TEMPLETEAPI.Models
{
    [Table("CharacterSkill")]
    public class CharacterSkill
    {
        public Character Character { get; set; }
        public Skill Skill { get; set; }
        public int CharacterId { get; set; }
        public int SkillId { get; set; }
    }
}
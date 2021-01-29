using System.Collections.Generic;

namespace TEMPLETEAPI.DTOs
{
    public class UpdateCharacterDto
    {
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }

    }
}
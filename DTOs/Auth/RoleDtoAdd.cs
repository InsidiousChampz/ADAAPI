using INFOEDITORAPI.Validations;

namespace INFOEDITORAPI.DTOs
{
    public class RoleDtoAdd
    {
        [FirstLetterUpperCase]
        public string RoleName { get; set; }
    }
}
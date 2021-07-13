using CustomerProFileAPI.Validations;

namespace CustomerProFileAPI.DTOs
{
    public class RoleDtoAdd
    {
        [FirstLetterUpperCase]
        public string RoleName { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace tts.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
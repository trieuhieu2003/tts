using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace tts.Categories.Dto
{
    [AutoMap(typeof(Category))]

    public class CreateCategoriesDto
    {
        public int Id { get; set; }

        [Required]

        public string NameCategory { get; set; }

    }
}

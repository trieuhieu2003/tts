using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace tts.Categories.Dto
{
    [AutoMap(typeof(Category))]
    public class CategoriesDto : AuditedEntityDto
    {
        [Required]
        public string NameCategory { get; set; }
    }
}

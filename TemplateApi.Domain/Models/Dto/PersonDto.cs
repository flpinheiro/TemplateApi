using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Domain.Models.Dto
{
    public class PersonDto
    {
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? SurName { get; set; }
        [Required]
        public DateOnly BirthDay { get; set; } = new DateOnly();
    }
}

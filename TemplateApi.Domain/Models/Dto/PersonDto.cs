using System.ComponentModel.DataAnnotations;
using TemplateApi.CrossCutting.Attributes;

namespace TemplateApi.Domain.Models.Dto
{
    public class PersonDto : AddPersonDto
    {
        public string? Id { get; set; }
    }

    public class AddPersonDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? SurName { get; set; }
        [Required, CPF]
        public string? CPF { get; set; }
        [Required]
        public DateOnly BirthDay { get; set; } = new DateOnly();
    }
}

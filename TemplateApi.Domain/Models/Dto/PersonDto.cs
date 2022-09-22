using System.ComponentModel.DataAnnotations;
using TemplateApi.CrossCutting.Attributes;
using TemplateApi.CrossCutting.Validators;
using TemplateApi.Domain.Models.Constants;

namespace TemplateApi.Domain.Models.Dto
{
    public class PersonDto : AddPersonDto
    {
        public string? Id { get; set; }
    }

    public class AddPersonDto
    {
        [Required, MaxLength(PersonContants.NameSize)]
        public string? Name { get; set; }
        [Required, MaxLength(PersonContants.SurnameSize)]
        public string? SurName { get; set; }
        [Required, CPF, MaxLength(CPFValidator.Size + 3), MinLength(CPFValidator.Size)]
        public string? CPF { get; set; }
        [Required]
        public DateOnly BirthDay { get; set; } = new DateOnly();
    }
    public class UpdatePersonDto
    {
        [MaxLength(PersonContants.NameSize)]
        public string? Name { get; set; }
        [MaxLength(PersonContants.SurnameSize)]
        public string? SurName { get; set; }
        public DateOnly? BirthDay { get; set; }
    }
}

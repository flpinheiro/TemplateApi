using FluentValidation;
using System.ComponentModel.DataAnnotations;
using TemplateApi.CrossCutting.Attributes;
using TemplateApi.CrossCutting.Validators;

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

    public class PersonDtoValidation : AbstractValidator<AddPersonDto>
    {
        public PersonDtoValidation()
        {
            RuleFor(a => a.Name).NotNull().NotEmpty();
            RuleFor(a => a.SurName).NotNull().NotEmpty();
            RuleFor(a => a.CPF).NotNull().NotEmpty().Must(cpf => !string.IsNullOrEmpty(cpf) && cpf.IsValidCpf());
            RuleFor(a => a.BirthDay).NotNull().NotEmpty();
        }
    }
}

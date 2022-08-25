using FluentValidation;
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

    public class PersonQueryDto
    {
        public string? Name { get; set; }
        public string? Cpf { get; set; }
    }

    public class PersonDtoValidation : AbstractValidator<AddPersonDto>
    {
        public PersonDtoValidation()
        {
            RuleFor(a => a.Name).NotNull().NotEmpty().MaximumLength(PersonContants.NameSize);
            RuleFor(a => a.SurName).NotNull().NotEmpty().MaximumLength(PersonContants.SurnameSize);
            RuleFor(a => a.CPF).NotNull().NotEmpty().Must(cpf => !string.IsNullOrEmpty(cpf) && cpf.IsValidCpf()).MinimumLength(CPFValidator.Size).MaximumLength(CPFValidator.Size + 3);
            RuleFor(a => a.BirthDay).NotNull().NotEmpty();
        }
    }

    public class PersonQueryDtoValidation : AbstractValidator<PersonQueryDto>
    {
        public PersonQueryDtoValidation()
        {
            RuleFor(a => a.Cpf).Must(cpf=> !string.IsNullOrEmpty(cpf) && cpf.IsValidCpf()).When(a => !string.IsNullOrEmpty(a.Cpf)).MinimumLength(CPFValidator.Size).MaximumLength(CPFValidator.Size + 3);
            RuleFor(a => a.Name).MaximumLength(PersonContants.NameSize);
        }
    }
}

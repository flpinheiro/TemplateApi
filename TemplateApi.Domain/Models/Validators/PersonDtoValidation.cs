using FluentValidation;
using TemplateApi.CrossCutting.Validators;
using TemplateApi.Domain.Models.Constants;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Domain.Models.Validators
{
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
}

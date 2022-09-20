using FluentValidation;
using TemplateApi.CrossCutting.Validators;
using TemplateApi.Domain.Models.Constants;
using TemplateApi.Domain.Models.Queries;

namespace TemplateApi.Domain.Models.Validators
{
    public class PersonQueryValidation : AbstractValidator<PersonQuery>
    {
        public PersonQueryValidation()
        {
            RuleFor(a => a.Cpf).Must(cpf => !string.IsNullOrEmpty(cpf) && cpf.IsValidCpf()).When(a => !string.IsNullOrEmpty(a.Cpf)).MinimumLength(CPFValidator.Size).MaximumLength(CPFValidator.Size + 3);
            RuleFor(a => a.Name).MaximumLength(PersonContants.NameSize);
        }
    }
}

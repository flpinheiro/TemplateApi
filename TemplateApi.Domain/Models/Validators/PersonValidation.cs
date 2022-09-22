using FluentValidation;
using TemplateApi.CrossCutting.Utils;
using TemplateApi.CrossCutting.Validators;
using TemplateApi.Domain.Models.Constants;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.Domain.Models.Queries;

namespace TemplateApi.Domain.Models.Validators
{
    public class PersonValidation
    {
        public IValidator<AddPersonDto> AddPersonValidator { get; } = new AddPersonDtoValidation();
        public IValidator<UpdatePersonDto> UpdatePersonValidator { get; } = new UpdatePerrsonDtoValidation();
        public IValidator<PersonQuery> PersonQueryvalidator { get; } = new PersonQueryValidation();
    }
    internal class AddPersonDtoValidation : AbstractValidator<AddPersonDto>
    {
        public AddPersonDtoValidation()
        {
            RuleFor(a => a.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(PersonContants.NameSize);
            RuleFor(a => a.SurName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(PersonContants.SurnameSize);
            RuleFor(a => a.CPF)
                .NotNull()
                .NotEmpty()
                .Must(cpf => !string.IsNullOrEmpty(cpf) && cpf.IsValidCpf())
                .MinimumLength(CPFValidator.Size)
                .MaximumLength(CPFValidator.Size + 3);
            RuleFor(a => a.BirthDay)
                .NotNull()
                .NotEmpty();
        }
    }

    internal class UpdatePerrsonDtoValidation : AbstractValidator<UpdatePersonDto>
    {
        public UpdatePerrsonDtoValidation()
        {
            RuleFor(a => a.Name)
                .MaximumLength(PersonContants.NameSize);
            RuleFor(a => a.SurName)
                .MaximumLength(PersonContants.SurnameSize);
            //RuleFor(a => a.BirthDay).NotNull().NotEmpty();
        }
    }

    internal class PersonQueryValidation : AbstractValidator<PersonQuery>
    {
        public PersonQueryValidation()
        {
            RuleFor(a => a.Cpf)
                .Must(cpf => !string.IsNullOrEmpty(cpf) && cpf.IsValidCpf())
                .When(a => !string.IsNullOrEmpty(a.Cpf) && a.Cpf.OnlyNumber().Length == CPFValidator.Size)
                .MinimumLength(3)
                .MaximumLength(CPFValidator.Size + 3);
            RuleFor(a => a.Name)
                .MaximumLength(PersonContants.NameSize);
        }
    }
}

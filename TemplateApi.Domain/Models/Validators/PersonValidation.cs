﻿using FluentValidation;
using TemplateApi.CrossCutting.Utils;
using TemplateApi.CrossCutting.Validators;
using TemplateApi.Domain.Interfaces.Validator;
using TemplateApi.Domain.Models.Constants;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.Domain.Models.Queries;

namespace TemplateApi.Domain.Models.Validators
{
    public class PersonValidation : IPersonValidation
    {
        public IValidator<AddPersonDto> AddPersonValidator { get; } = new AddPersonDtoValidation();
        public IValidator<UpdatePersonDto> UpdatePersonValidator { get; } = new UpdatePerrsonDtoValidation();
        public IValidator<PersonQuery> PersonQueryvalidator { get; } = new PersonQueryValidation();

        private class AddPersonDtoValidation : AbstractValidator<AddPersonDto>
        {
            public AddPersonDtoValidation()
            {
                RuleFor(a => a.Name)
                    .NotNull()
                    .NotEmpty()
                    .MaximumLength(PersonContants.NameSize);
                RuleFor(a => a.Surname)
                    .NotNull()
                    .NotEmpty()
                    .MaximumLength(PersonContants.SurnameSize);
                RuleFor(a => a.CPF)
                    .NotNull()
                    .NotEmpty()
                    .Must(cpf => !string.IsNullOrEmpty(cpf) && cpf.IsValidCpf())
                    .MinimumLength(CPFValidator.Size)
                    .MaximumLength(CPFValidator.Size + 3);
                RuleFor(a => a.Birthday)
                    .NotNull()
                    .NotEmpty();
            }
        }

        private class UpdatePerrsonDtoValidation : AbstractValidator<UpdatePersonDto>
        {
            public UpdatePerrsonDtoValidation()
            {
                RuleFor(a => a.Name)
                    .MaximumLength(PersonContants.NameSize);
                RuleFor(a => a.Surname)
                    .MaximumLength(PersonContants.SurnameSize);
                //RuleFor(a => a.BirthDay).NotNull().NotEmpty();
            }
        }

        private class PersonQueryValidation : AbstractValidator<PersonQuery>
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

}

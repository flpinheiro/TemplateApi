using System.ComponentModel.DataAnnotations;
using TemplateApi.CrossCutting.Validators;

namespace TemplateApi.CrossCutting.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CPFAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("CPF can't be null");
            if (IsValid(value)) return ValidationResult.Success;
            return new ValidationResult("That is no a Valid Brazilian CPF");
        }

        public override bool IsValid(object? value)
        {
            var cpf = value as string;
            return !string.IsNullOrEmpty(cpf) && CPFValidator.IsValidCpf(cpf);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CPFCNPJAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("CPF can't be null");
            if (IsValid(value)) return ValidationResult.Success;
            return new ValidationResult("That is no a Valid Brazilian CPF");
        }

        public override bool IsValid(object? value)
        {
            var cpf = value as string;
            return !string.IsNullOrEmpty(cpf) && (CPFValidator.IsValidCpf(cpf) || CNPJValidator.IsValidCNPJ(cpf));
        }
    }
}

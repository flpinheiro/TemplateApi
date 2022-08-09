using System.ComponentModel.DataAnnotations;
using TemplateApi.CrossCutting.Validators;

namespace TemplateApi.CrossCutting.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CNPJAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (IsValid(value)) return ValidationResult.Success;
            return new ValidationResult("That is no a Valid Brazilian CNPJ");
        }

        public override bool IsValid(object? value)
        {
            var cnpj = value as string;
            return !string.IsNullOrEmpty(cnpj) && CNPJValidator.IsValidCNPJ(cnpj);
        }
    }
}

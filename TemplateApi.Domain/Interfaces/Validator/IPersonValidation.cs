using FluentValidation;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.Domain.Models.Queries;

namespace TemplateApi.Domain.Interfaces.Validator
{
    public interface IPersonValidation
    {
        IValidator<AddPersonDto> AddPersonValidator { get; }
        IValidator<PersonQuery> PersonQueryvalidator { get; }
        IValidator<UpdatePersonDto> UpdatePersonValidator { get; }
    }
}
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Domain.Interfaces.Services
{
    public interface IPersonService
    {
        PersonDtoValidation ValidationRules { get; }
        IValidator<AddPersonDto> Validator { get; }

        Task<PersonDto> GetPersonById(string id);
        Task<IEnumerable<PersonDto>> GetPersonByName(string name);
        Task<IEnumerable<PersonDto>> GetAllPerson();

        Task<PersonDto> AddPerson(AddPersonDto person);
        Task UpdatePerson(string id, PersonDto person);
        Task<PersonDto> DeletePerson(string id);
        FileStreamResult ExportToExcel(IEnumerable<PersonDto> people);
        Task<IEnumerable<PersonDto>> GetPersonByName(string name, Pagination pagination);
        Task<IEnumerable<PersonDto>> GetAllPerson(Pagination pagination);
    }
}

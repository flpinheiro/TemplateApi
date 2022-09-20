using Microsoft.AspNetCore.Mvc;
using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.Domain.Models.Queries;

namespace TemplateApi.Domain.Interfaces.Services
{
    public interface IPersonService
    {
        Task<PersonDto> GetPersonById(string id);
        Task<PersonDto> AddPerson(AddPersonDto person);
        Task UpdatePerson(string id, PersonDto person);
        Task<PersonDto> DeletePerson(string id);
        FileStreamResult ExportToExcel(IEnumerable<PersonDto> people);
        Task<IEnumerable<PersonDto>> GetPeoplePaginatedAsync(PersonQuery queryDto, Pagination pagination);
        Task<IEnumerable<PersonDto>> GetPeopleAsync(PersonQuery queryDto);
        PaginationResponse CountPeople(PersonQuery queryDto, Pagination pagination);
        Task<bool> AnyAsync(string id);
        Task<AddPersonDto> GetRandomPerson();
    }
}

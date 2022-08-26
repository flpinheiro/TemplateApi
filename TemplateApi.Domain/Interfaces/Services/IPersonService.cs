using Microsoft.AspNetCore.Mvc;
using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Domain.Interfaces.Services
{
    public interface IPersonService
    {
        Task<PersonDto> GetPersonById(string id);
        Task<PersonDto> AddPerson(AddPersonDto person);
        Task UpdatePerson(string id, PersonDto person);
        Task<PersonDto> DeletePerson(string id);
        FileStreamResult ExportToExcel(IEnumerable<PersonDto> people);
        Task<IEnumerable<PersonDto>> GetPeoplePaginatedAsync(PersonQueryDto queryDto, Pagination pagination);
        Task<IEnumerable<PersonDto>> GetPeopleAsync(PersonQueryDto queryDto);
        PaginationResponse CountPeople(PersonQueryDto queryDto, Pagination pagination);
    }
}

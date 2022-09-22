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
        Task UpdatePerson(string id, UpdatePersonDto person);
        Task<PersonDto> DeletePerson(string id);
        FileStreamResult ExportToExcel(IEnumerable<PersonDto> people);
        Task<IEnumerable<PersonDto>> GetPeoplePaginatedAsync(PersonQuery queryDto, Pagination pagination);
        Task<IEnumerable<PersonDto>> GetPeopleAsync(PersonQuery queryDto);
        PaginationResponse CountPeople(PersonQuery queryDto, Pagination pagination);
        Task<bool> AnyAsync(string id);

#if DEBUG
        /// <summary>
        /// DEBUG ONLY METHOD return a random persom created to be used during the debug process
        /// </summary>
        /// <returns></returns>
        AddPersonDto GetRandomPerson();
        Task<string> GetRandomPersonId();
#endif
    }
}

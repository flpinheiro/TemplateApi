using Microsoft.AspNetCore.Mvc;
using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.Domain.Models.Queries;

namespace TemplateApi.Domain.Interfaces.Services
{
    public interface IPersonService
    {
        Task<PersonDto?> Get(string id);
        Task<string> Add(AddPersonDto person);
        Task Update(string id, UpdatePersonDto person);
        Task Delete(string id);
        FileStreamResult ExportToExcel(IEnumerable<PersonDto> people);
        Task<IEnumerable<PersonDto>> Get(PersonQuery queryDto, Pagination pagination);
        Task<IEnumerable<PersonDto>> Get(PersonQuery queryDto);
        PaginationResponse Count(PersonQuery queryDto, Pagination pagination);
        Task<bool> Any(string id);
        Task<PagedList<Person>> GetPaginated(PersonQuery personQuery, PagedListQuery pageListQuery);
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

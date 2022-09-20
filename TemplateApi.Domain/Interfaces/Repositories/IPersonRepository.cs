using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Queries;

namespace TemplateApi.Domain.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        Task<Person?> GetByIdAsync(string id);
        Task<bool> AnyAsync(string id);
        string Add(Person model);
        void Update(Person model);
        void Delete(Person model);
        Task<IEnumerable<Person>> GetPeoplePaginatedAsync(PersonQuery queryDto, Pagination pagination);
        Task<IEnumerable<Person>> GetPeopleAsync(PersonQuery queryDto);
        PaginationResponse CountPeople(PersonQuery queryDto, Pagination pagination);
    }
}

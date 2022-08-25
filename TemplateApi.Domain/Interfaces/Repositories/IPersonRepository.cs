using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Domain.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        Task<Person?> GetByIdAsync(string id);
        //Task<IEnumerable<Person>> GetAllAsync();
        //Task<IEnumerable<Person>> GetByNameAsync(string name);
        Task<bool> AnyAsync(string id);
        string Add(Person model);
        void Update(Person model);
        void Delete(Person model);
        //Task<IEnumerable<Person>> GetByNameAsync(string name, Pagination pagination);
        //Task<IEnumerable<Person>> GetAllAsync(Pagination pagination);

        Task<IEnumerable<Person>> GetPeoplePaginatedAsync(PersonQueryDto queryDto, Pagination pagination);
        Task<IEnumerable<Person>> GetPeopleAsync(PersonQueryDto queryDto);
        PaginationResponse CountPeople(PersonQueryDto queryDto, Pagination pagination);
        //PaginationResponse CountAll(Pagination pagination);
        //PaginationResponse CountByName(string name, Pagination pagination);
    }
}

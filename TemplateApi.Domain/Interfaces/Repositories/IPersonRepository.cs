using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Models.Dal;

namespace TemplateApi.Domain.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        Task<Person?> GetByIdAsync(string id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task<IEnumerable<Person>> GetByNameAsync(string name);
        Task<bool> AnyAsync(string id);
        string Add(Person model);
        void Update(Person model);
        void Delete(Person model);
        Task<IEnumerable<Person>> GetByNameAsync(string name, Pagination pagination);
        Task<IEnumerable<Person>> GetAllAsync(Pagination pagination);
        Task<PaginationResponse> CountAllAsync(Pagination pagination);
        Task<PaginationResponse> CountByNameAsync(string name, Pagination pagination);
    }
}

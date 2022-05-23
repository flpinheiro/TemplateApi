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
    }
}

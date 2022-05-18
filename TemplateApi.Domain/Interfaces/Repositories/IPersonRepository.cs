using TemplateApi.Domain.Models.Dal;

namespace TemplateApi.Domain.Interfaces.Repositories
{
    public interface IPersonRepository : IBasicRepository<Person, string>, IBasicRepositoryAsync<Person, string>, IBasicWriteRepository<Person, string>
    {
    }
}

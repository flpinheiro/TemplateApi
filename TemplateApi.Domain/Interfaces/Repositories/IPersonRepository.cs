using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApi.Domain.Models.Dal;

namespace TemplateApi.Domain.Interfaces.Repositories
{
    public interface IPersonRepository : IBasicRepository<Person, string>, IBasicRepositoryAsync<Person, string>
    {
    }
}

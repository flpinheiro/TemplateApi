using AutoMapper;
using Serilog;

namespace TemplateApi.Domain.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IPersonRepository PersonRepository { get; }
    IMapper Mapper { get; }
    ILogger Logger { get; }

    void Save();
    Task SaveAsync();
    void RollBack();
}


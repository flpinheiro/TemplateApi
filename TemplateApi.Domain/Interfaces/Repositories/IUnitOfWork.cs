namespace TemplateApi.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
    }
}

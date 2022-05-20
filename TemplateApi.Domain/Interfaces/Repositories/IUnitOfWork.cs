namespace TemplateApi.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository PersonRepository { get; }

        void Save();
        Task SaveAsync();
        void Commit();
        void RollBack();
    }
}

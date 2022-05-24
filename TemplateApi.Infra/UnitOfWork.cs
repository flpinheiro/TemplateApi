using Microsoft.EntityFrameworkCore.Storage;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Infra.Context;
using TemplateApi.Infra.Repositories;

namespace TemplateApi.Infra
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TemplateApiContext _context;
        private readonly IDbContextTransaction? _transaction;

        public UnitOfWork(TemplateApiContext context)
        {
            _context = context ?? throw new ArgumentNullException("TemplateApiContext");
            _transaction = _context.Database.BeginTransaction();
        }

        private IPersonRepository? _personRepository;
        public IPersonRepository PersonRepository
        {
            get
            {
                if (_personRepository == null) _personRepository = new PersonRepository(_context);
                return _personRepository;
            }
        }

        public void Dispose()
        {
            try
            {
                if (_transaction != null) _transaction.Commit();
            }
            catch (Exception) { }
            if (_transaction != null) _transaction?.Dispose();
            _context.Dispose();

        }

        public void Save()
        {
            _context.SaveChanges();
            //if (_transaction != null) _transaction.Commit();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
            //
            //if (_transaction != null) await _transaction.CommitAsync();
        }

        public void RollBack()
        {
            if (_transaction != null) _transaction.Rollback();
        }
    }
}

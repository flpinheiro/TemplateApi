using Microsoft.EntityFrameworkCore.Storage;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Infra.Context;
using TemplateApi.Infra.Repositories;

namespace TemplateApi.Infra;

public class UnitOfWork : IUnitOfWork
{
    private readonly TemplateApiContext _context;
    private readonly IDbContextTransaction? _transaction;
    private bool _isRollBacked = false;

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
        if (_transaction != null)
        {
            if (!_isRollBacked) _transaction.Commit();
             _transaction.Dispose();
        }
        _context.Dispose();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void RollBack()
    {
        if (_transaction != null) _transaction.Rollback();
        _isRollBacked = true;
    }
}


using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Infra.Context;
using TemplateApi.Infra.Repositories;

namespace TemplateApi.Infra;

internal class UnitOfWork : IUnitOfWork
{
    private readonly TemplateApiContext _context;
    private readonly IDbContextTransaction? _transaction;
    private bool _isRollBacked = false;

    public UnitOfWork(TemplateApiContext context, IMapper mapper, ILogger logger)
    {
        _context = context ?? throw new ArgumentNullException("TemplateApiContext");
        Mapper = mapper ?? throw new ArgumentNullException("IMapper");
        _transaction = _context.Database.BeginTransaction();
        Logger = logger;
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
    public IMapper Mapper { get; }
    public ILogger Logger { get; }

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


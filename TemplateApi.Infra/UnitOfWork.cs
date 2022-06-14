using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Infra.Context;
using TemplateApi.Infra.Repositories;

namespace TemplateApi.Infra;

public class UnitOfWork : IUnitOfWork
{
    private readonly TemplateApiContext _context;
    private readonly IDbContextTransaction? _transaction;
    private bool _isRollBacked = false;

    public UnitOfWork(TemplateApiContext context, IMapper mapper, ILogger logger)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(ILogger)); ;
        _context = context ?? throw new ArgumentNullException(nameof(TemplateApiContext));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        _transaction = _context.Database.BeginTransaction();
        Logger.Debug($"Begin Trasaction: {_transaction.TransactionId}", _transaction);
    }

    private IPersonRepository? _personRepository;
    public IPersonRepository PersonRepository
    {
        get
        {
            if (_personRepository == null)
            {
                Logger.Debug($"Instaciate {nameof(IPersonRepository)}");
                _personRepository = new PersonRepository(_context);
            }
            return _personRepository;
        }
    }
    public IMapper Mapper { get; }
    public ILogger Logger { get; }

    public void Dispose()
    {
        if (_transaction != null)
        {
            if (!_isRollBacked)
            {
                Logger.Debug("Commit database");
                _transaction.Commit();
            }
            Logger.Debug("Dispose Transaction");
            _transaction.Dispose();
        }
        Logger.Debug("Dispose TemplateApiContext");
        _context.Dispose();

    }

    public void Save()
    {
        Logger.Debug("save database");
        _context.SaveChanges();
    }

    public async Task SaveAsync()
    {
        Logger.Debug("Save async database");
        await _context.SaveChangesAsync();
    }

    public void RollBack()
    {
        Logger.Debug("Roll Back database");
        if (_transaction != null) _transaction.Rollback();
        _isRollBacked = true;
    }
}

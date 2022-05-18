using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _context = context;
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
           
            _transaction?.Dispose();
            _context?.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _transaction?.Commit();
        }

        public void RollBack()
        {
            _transaction?.Rollback();
        }
    }
}

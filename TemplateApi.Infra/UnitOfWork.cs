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

        public UnitOfWork(TemplateApiContext context)
        {
            _context = context;
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
    }
}

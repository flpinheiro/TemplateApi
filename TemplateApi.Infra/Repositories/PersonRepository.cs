using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Infra.Context;

namespace TemplateApi.Infra.Repositories
{
    internal class PersonRepository : IPersonRepository
    {
        private readonly TemplateApiContext _context;
        public PersonRepository(TemplateApiContext context)
        {
            _context = context;
        }
        public IEnumerable<Person> Get(Expression<Func<Person, bool>> predicate) => _context.People?.Where(predicate).ToList() ?? new List<Person>();
        public IEnumerable<Person> GetAll() => _context.People?.ToList() ?? new List<Person>();
        public async Task<IEnumerable<Person>> GetAllAsync() => await _context.People?.ToListAsync() ?? new List<Person>();
        public async Task<IEnumerable<Person>> GetAsync(Expression<Func<Person, bool>> predicate) => await _context.People?.Where(predicate).ToListAsync() ?? new List<Person>();
        public Person? GetById(string id) => _context.People?.FirstOrDefault(x => x.Id == id);
        public async Task<Person?> GetByIdAsync(string id) => await _context.People?.FirstOrDefaultAsync(x => x.Id == id);
        public void Add(Person model)
        {
            if (string.IsNullOrWhiteSpace(model.Id)) model.Id = Guid.NewGuid().ToString();
            _context.Entry(model).State = EntityState.Added;
            _context.People?.Add(model);
        }
        public void Update(Person model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.People?.Update(model);
        }
        public void Delete(Person model)
        {
            _context.Entry(model).State = EntityState.Deleted;
            _context.People?.Remove(model);
        }
    }
}

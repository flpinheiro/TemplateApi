using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Infra.Context;

namespace TemplateApi.Infra.Repositories
{
    public class BasicRepository<TModel, Key> : IBasicRepository<TModel, Key>
        where TModel : BasicDal<Key>
        where Key : IEquatable<Key>
    {
        private readonly TemplateApiContext _context;
        private readonly DbSet<TModel> _dbSet;
        public BasicRepository(TemplateApiContext context)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
        }
        public Key? Add(TModel model)
        {
            _context.Entry(model).State = EntityState.Added;
            _dbSet.Add(model);
            return model.Id ;
        }

        public bool Any(Key id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Key id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TModel model)
        {
            if (_context.Entry(model).State == EntityState.Detached)
            {
                _dbSet.Attach(model);
            }
            _dbSet.Remove(model);
        }

        public IEnumerable<TModel> Get(Expression<Func<TModel, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public IEnumerable<TModel> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TModel>> GetAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public TModel? GetById(Key id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TModel?> GetByIdAsync(Key id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(TModel model)
        {
            _dbSet.Attach(model);
            _context.Entry(model).State = EntityState.Modified;
        }
    }
}

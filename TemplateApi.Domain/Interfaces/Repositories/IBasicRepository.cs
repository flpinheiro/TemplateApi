﻿using System.Linq.Expressions;
using TemplateApi.Domain.Models.Dal;

namespace TemplateApi.Domain.Interfaces.Repositories
{
    public interface IBasicRepository<TModel, Key>
        where TModel : BasicDal<Key>
        where Key : IEquatable<Key>
    {
        TModel? GetById(Key id);
        IEnumerable<TModel> GetAll();
        IEnumerable<TModel> Get(Expression<Func<TModel, bool>> predicate);
        bool Any(Key id);
        Task<TModel?> GetByIdAsync(Key id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<IEnumerable<TModel>> GetAsync(Expression<Func<TModel, bool>> predicate);
        Task<bool> AnyAsync(Key id);
        Key? Add(TModel model);
        void Update(TModel model);
        void Delete(TModel model);
    }
}

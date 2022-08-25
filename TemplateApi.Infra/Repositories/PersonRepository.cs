using Microsoft.EntityFrameworkCore;
using TemplateApi.CrossCutting.Models;
using TemplateApi.CrossCutting.Utils;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.Infra.Context;

namespace TemplateApi.Infra.Repositories;

internal class PersonRepository : IPersonRepository
{
    private readonly TemplateApiContext _context;
    public PersonRepository(TemplateApiContext context)
        => _context = context;

    private IQueryable<Person> GetAll() => _context.People;
    //public async Task<IEnumerable<Person>> GetAllAsync()
    //    => await GetAll().ToListAsync();
    //public async Task<IEnumerable<Person>> GetAllAsync(Pagination pagination)
    //    => await GetAll().GetPaginated(pagination).ToListAsync();
    //public PaginationResponse CountAll(Pagination pagination) => GetAll().Paginate(pagination);

    public async Task<Person?> GetByIdAsync(string id) => await _context.People.FirstOrDefaultAsync(x => x.Id == id);

    //private IQueryable<Person> GetByName(string name)
    //    => GetAll().GetByName(name);

    //public async Task<IEnumerable<Person>> GetByNameAsync(string name)
    //    => await GetByName(name).ToListAsync();
    //public async Task<IEnumerable<Person>> GetByNameAsync(string name, Pagination pagination)
    //    => await GetByName(name).GetPaginated(pagination).ToListAsync();
    //public PaginationResponse CountByName(string name, Pagination pagination) => GetByName(name).Paginate(pagination);

    public async Task<bool> AnyAsync(string id) => await _context.People.AnyAsync(p => p.Id != null && p.Id.Equals(id));
    public string Add(Person model)
    {
        model.Id = Guid.NewGuid().ToString();
        _context.Entry(model).State = EntityState.Added;
        _context.People.Add(model);
        return model.Id;
    }
    public void Update(Person model)
    {
        _context.Entry(model).State = EntityState.Modified;
        _context.People.Update(model);
    }
    public void Delete(Person model)
    {
        _context.Entry(model).State = EntityState.Deleted;
        _context.People.Remove(model);
    }

    public PaginationResponse CountPeople(PersonQueryDto queryDto, Pagination pagination)
        => GetAll().GetPeople(queryDto).Paginate(pagination);
    public async Task<IEnumerable<Person>> GetPeopleAsync(PersonQueryDto queryDto)
        => await GetAll().GetPeople(queryDto).ToListAsync();
    public async Task<IEnumerable<Person>> GetPeoplePaginatedAsync(PersonQueryDto queryDto, Pagination pagination)
        => await GetAll().GetPeople(queryDto).GetPaginated(pagination).ToListAsync();
}


internal static class PersonRepositoryExtensions
{
    public static IQueryable<Person> GetPeople(this IQueryable<Person> query, PersonQueryDto queryDto)
    {
        if (!string.IsNullOrWhiteSpace(queryDto.Name))
            query = query.GetByName(queryDto.Name);
        if (!string.IsNullOrWhiteSpace(queryDto.Cpf))
            query = query.GetByCpf(queryDto.Cpf);
        return query;
    }
    public static IQueryable<Person> GetByName(this IQueryable<Person> query, string name)
        => query.Where(p => p.Name != null && p.Name.ToUpper().Contains(name.ToUpper()) || p.SurName != null && p.SurName.ToUpper().Contains(name.ToUpper()));

    private static IQueryable<Person> GetByCpf(this IQueryable<Person> query, string cpf)
        => query.Where(p => p.CPF != null && p.CPF.Equals(cpf.OnlyNumber()));
}

using Microsoft.EntityFrameworkCore;
using TemplateApi.CrossCutting.Enums;
using TemplateApi.CrossCutting.Models;
using TemplateApi.CrossCutting.Utils;
using TemplateApi.Domain.Interfaces;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Queries;
using TemplateApi.Infra.Context;

namespace TemplateApi.Infra.Repositories;

internal class PersonRepository : IPersonRepository
{
    private readonly TemplateApiContext _context;
    public PersonRepository(TemplateApiContext context)
        => _context = context;

    private IQueryable<Person> GetAll() => _context.People;
    public async Task<Person?> GetByIdAsync(string id) => await _context.People.FirstOrDefaultAsync(x => x.Id == id);
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

    public PaginationResponse CountPeople(PersonQuery queryDto, Pagination pagination)
        => GetAll().GetPeople(queryDto).Paginate(pagination);
    public async Task<IEnumerable<Person>> GetPeopleAsync(PersonQuery queryDto)
        => await GetAll().GetPeople(queryDto).ToListAsync();
    public async Task<IEnumerable<Person>> GetPeoplePaginatedAsync(PersonQuery queryDto, Pagination pagination)
        => await GetAll().GetPeople(queryDto).GetPaginated(pagination).ToListAsync();

    public async Task<PagedList<Person>> GetPaginatedAsync(PersonQuery personQuery, PagedListQuery pageListQuery)
        => await GetAll().GetPeople(personQuery).ToPagedListAsync(pageListQuery);
}


internal static class PersonRepositoryExtensions
{
    public static IQueryable<Person> GetPeople(this IQueryable<Person> query, PersonQuery queryDto)
    {
        if (!string.IsNullOrWhiteSpace(queryDto.Name))
            query = query.GetByName(queryDto.Name);
        if (!string.IsNullOrWhiteSpace(queryDto.Cpf) && queryDto.Cpf.OnlyNumber().Length > 0)
            query = query.GetByCpf(queryDto.Cpf);

        var sort = queryDto as ISortablePeopleQuery;

        query = query.SortPeople(sort);

        return query;
    }

    private static IQueryable<Person> GetByName(this IQueryable<Person> query, string name)
        => query.Where(p => p.Name != null && p.Name.ToUpper().Contains(name.ToUpper()) || p.SurName != null && p.SurName.ToUpper().Contains(name.ToUpper()));

    private static IQueryable<Person> GetByCpf(this IQueryable<Person> query, string cpf)
        => query.Where(p => p.CPF != null && p.CPF.Contains(cpf.OnlyNumber()));

    private static IQueryable<Person> SortPeople(this IQueryable<Person> query, ISortablePeopleQuery sort)
    => sort.SortBy switch
    {
        PersonEnum.Surname => sort.SortAs switch
        {
            SortAsEnum.Desc => query.OrderByDescending(p => p.SurName),
            _ => query.OrderBy(p => p.SurName),
        },
        PersonEnum.Cpf => sort.SortAs switch
        {
            SortAsEnum.Desc => query.OrderByDescending(p => p.CPF),
            _ => query.OrderBy(p => p.CPF),
        },
        PersonEnum.Birthday => sort.SortAs switch
        {
            SortAsEnum.Desc => query.OrderByDescending(p => p.BirthDay),
            _ => query.OrderBy(p => p.BirthDay),
        },
        _ => sort.SortAs switch
        {
            SortAsEnum.Desc => query.OrderByDescending(p => p.Name),
            _ => query.OrderBy(p => p.Name),
        },
    };
}

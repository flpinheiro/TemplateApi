using Microsoft.EntityFrameworkCore;
using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Infra.Context;

namespace TemplateApi.Infra.Repositories;

internal class PersonRepository : IPersonRepository
{
    private readonly TemplateApiContext _context;
    public PersonRepository(TemplateApiContext context)
        => _context = context;

    private IQueryable<Person> GetAll() => _context.People;
    public async Task<IEnumerable<Person>> GetAllAsync()
        => await GetAll().ToListAsync();
    public async Task<IEnumerable<Person>> GetAllAsync(Pagination pagination)
        => await GetAll().GetPaginated(pagination).ToListAsync();
    public async Task<PaginationResponse> CountAllAsync(Pagination pagination)
    {
        var total = await GetAll().CountAsync();
        var pages  = (int) Math.Floor(total / (decimal) pagination.PageSize);
        return new PaginationResponse(pages, total);
    }

    public async Task<Person?> GetByIdAsync(string id) => await _context.People.FirstOrDefaultAsync(x => x.Id == id);
    private IQueryable<Person> GetByName(string name)
        => GetAll()
        .Where(p => p.Name != null && p.Name.ToUpper().Contains(name.ToUpper()) || p.SurName != null && p.SurName.ToUpper().Contains(name.ToUpper()));
    public async Task<IEnumerable<Person>> GetByNameAsync(string name)
        => await GetByName(name).ToListAsync();
    public async Task<IEnumerable<Person>> GetByNameAsync(string name, Pagination pagination)
        => await GetByName(name).GetPaginated(pagination).ToListAsync();
    public async Task<PaginationResponse> CountByNameAsync(string name, Pagination pagination)
    {
        var total = await GetByName(name).CountAsync();
        var pages = (int)Math.Floor(total / (decimal)pagination.PageSize);
        return new PaginationResponse(pages, total);
    }

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
}


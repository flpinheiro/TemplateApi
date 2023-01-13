using System.Data;
using TemplateApi.CrossCutting.Exceptions;
using TemplateApi.CrossCutting.Extensions;
using TemplateApi.CrossCutting.Models;
using TemplateApi.CrossCutting.Utils;
using TemplateApi.CrossCutting.Validators;
using TemplateApi.Domain.Configurations;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.Domain.Models.Queries;

namespace TemplateApi.Domain.Services;
public class PersonService : IPersonService
{
    private readonly IUnitOfWork _uow;

    public PersonService(IUnitOfWork uow) => _uow = uow ?? throw new ArgumentNullException(nameof(uow));

    public async Task<string> Add(AddPersonDto person)
    {
        try
        {
            _uow.Logger.Debug("Add Person", person);
            var model = person.ToModel();
            var id = _uow.PersonRepository.Add(model);
            await _uow.SaveAsync();
            return id;
        }
        catch (Exception ex)
        {
            _uow.Logger.Error(ex.Message);
            _uow.RollBack();
            throw;
        }
    }

    public async Task Delete(string id)
    {
        try
        {
            _uow.Logger.Debug("Delete person", id);
            var person = await _uow.PersonRepository.GetByIdAsync(id);
            if (person == null) throw new PersonNotFoundException();
            _uow.PersonRepository.Delete(person);
            await _uow.SaveAsync();
        }
        catch (Exception ex)
        {
            _uow.Logger.Error(ex.Message);
            _uow.RollBack();
            throw;
        }
    }

    public PaginationResponse Count(PersonQuery queryDto, Pagination pagination)
        => _uow.PersonRepository.CountPeople(queryDto, pagination);

    public async Task<PersonDto?> Get(string id)
    {
        _uow.Logger.Debug("Get person by id");
        return (await _uow.PersonRepository.GetByIdAsync(id))?.ToDto();
    }
    public async Task<IEnumerable<PersonDto>> Get(PersonQuery queryDto)
        => (await _uow.PersonRepository.GetPeopleAsync(queryDto)).ToDto();

    public async Task<IEnumerable<PersonDto>> Get(PersonQuery queryDto, Pagination pagination)
        => (await _uow.PersonRepository.GetPeoplePaginatedAsync(queryDto, pagination)).ToDto();

    public async Task<PagedList<Person>> GetPaginated(PersonQuery personQuery, PagedListQuery pageListQuery)
        => (await _uow.PersonRepository.GetPaginatedAsync(personQuery, pageListQuery));

    public async Task Update(string id, UpdatePersonDto person)
    {
        try
        {
            _uow.Logger.Debug("update Person", id, person);
            var model = await _uow.PersonRepository.GetByIdAsync(id);
            if (model == null) throw new PersonNotFoundException();

            if (!string.IsNullOrWhiteSpace(person.Name))
                model.Name = person.Name;
            if (!string.IsNullOrWhiteSpace(person.SurName))
                model.SurName = person.SurName;
            if (person.BirthDay != null)
                model.BirthDay = person.BirthDay?.ToShortDateString() ?? string.Empty;

            _uow.PersonRepository.Update(model);
            await _uow.SaveAsync();
        }
        catch (Exception ex)
        {
            _uow.Logger.Error(ex.Message);
            _uow.RollBack();
            throw;
        }
    }

    public async Task<bool> Any(string id) => await _uow.PersonRepository.AnyAsync(id);

    public Microsoft.AspNetCore.Mvc.FileStreamResult ExportToExcel(IEnumerable<PersonDto> people)
    {
        var table = new DataTable("People");

        var name = new DataColumn("name");
        var surname = new DataColumn("Surname");
        var birthDay = new DataColumn("Birth Day");
        var cpf = new DataColumn("CPF");

        table.Columns.Add(name);
        table.Columns.Add(surname);
        table.Columns.Add(birthDay);
        table.Columns.Add(cpf);

        foreach (var person in people)
        {
            var row = table.NewRow();

            row[name] = person.Name;
            row[surname] = person.SurName;
            row[birthDay] = person.BirthDay;
            row[cpf] = person.CPF;

            table.Rows.Add(row);
        }

        return table.DeliverExcelFile($"people {DateTime.Now:yyyy_MM_dd_HH_mm_ss}");
    }

#if DEBUG
    public AddPersonDto GetRandomPerson() => new(RandomPersonName.GetName(), RandomPersonName.GetSurname(), CPFValidator.GerarCpf(), RandomDateTime.NextDateOnly());

    public async Task<string> GetRandomPersonId()
    {
        var people = (await _uow.PersonRepository.GetPeopleAsync(new PersonQuery())) ?? new List<Person>();
        var peopleIds = people.Select(a => a.Id).ToList();

        var random = new Random();

        return peopleIds[random.Next(peopleIds.Count)] ?? string.Empty;
    }
#endif

}

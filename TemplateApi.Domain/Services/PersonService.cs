using System.Data;
using TemplateApi.CrossCutting.Exceptions;
using TemplateApi.CrossCutting.Extensions;
using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Domain.Services;
public class PersonService : IPersonService
{
    private readonly IUnitOfWork _uow;

    public PersonService(IUnitOfWork uow)
        => _uow = uow ?? throw new ArgumentNullException("IUnitOfWork");

    public async Task<PersonDto> AddPerson(AddPersonDto addPErson)
    {
        try
        {
            _uow.Logger.Debug("Add Person", addPErson);
            var person = _uow.Mapper.Map<PersonDto>(addPErson);
            var model = _uow.Mapper.Map<Person>(person);
            person.Id = _uow.PersonRepository.Add(model);
            await _uow.SaveAsync();
            if (person.Name == "string") throw new Exception("String is not an acetabble name for a person");
            return person;
        }
        catch (Exception ex)
        {
            _uow.Logger.Error(ex.Message);
            _uow.RollBack();
            throw;
        }
    }

    public async Task<PersonDto> DeletePerson(string id)
    {
        try
        {
            _uow.Logger.Debug("Delete person", id);
            var person = await _uow.PersonRepository.GetByIdAsync(id);
            if (person == null) throw new PersonNotFoundException();
            _uow.PersonRepository.Delete(person);
            await _uow.SaveAsync();
            return _uow.Mapper.Map<PersonDto>(person);
        }
        catch (Exception ex)
        {
            _uow.Logger.Error(ex.Message);
            _uow.RollBack();
            throw;
        }
    }

    public async Task<IEnumerable<PersonDto>> GetAllPerson()
    {
        _uow.Logger.Debug("get all person");
        return _uow.Mapper.Map<IEnumerable<PersonDto>>(await _uow.PersonRepository.GetAllAsync());
    }

    public async Task<IEnumerable<PersonDto>> GetAllPerson(Pagination pagination)
    {
        _uow.Logger.Debug("get all person");
        return _uow.Mapper.Map<IEnumerable<PersonDto>>(await _uow.PersonRepository.GetAllAsync(pagination));
    }
    public PaginationResponse CountAllPerson(Pagination pagination) => _uow.PersonRepository.CountAll(pagination);

    public async Task<PersonDto> GetPersonById(string id)
    {
        _uow.Logger.Debug("Get person by id");
        return _uow.Mapper.Map<PersonDto>(await _uow.PersonRepository.GetByIdAsync(id));
    }

    public async Task<IEnumerable<PersonDto>> GetPersonByName(string name)
    {
        _uow.Logger.Debug("Get person by name");
        return _uow.Mapper.Map<IEnumerable<PersonDto>>(await _uow.PersonRepository.GetByNameAsync(name));
    }

    public async Task<IEnumerable<PersonDto>> GetPersonByName(string name, Pagination pagination)
    {
        _uow.Logger.Debug("Get person by name");
        return _uow.Mapper.Map<IEnumerable<PersonDto>>(await _uow.PersonRepository.GetByNameAsync(name, pagination));
    }
    public PaginationResponse CountPersonByName(string name, Pagination pagination) => _uow.PersonRepository.CountByName(name, pagination);

    public async Task UpdatePerson(string id, PersonDto person)
    {
        try
        {
            _uow.Logger.Debug("update Person", id, person);
            if (!await _uow.PersonRepository.AnyAsync(id)) throw new PersonNotFoundException();
            var model = _uow.Mapper.Map<Person>(person);
            model.Id = id;
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

        return table.DeliverExcelFile($"people {DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}");
    }
}

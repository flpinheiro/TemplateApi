using System.Data;
using System.Net.Http;
using System.Text.Json;
using TemplateApi.CrossCutting.Exceptions;
using TemplateApi.CrossCutting.Extensions;
using TemplateApi.CrossCutting.Models;
using TemplateApi.CrossCutting.Utils;
using TemplateApi.CrossCutting.Validators;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.Domain.Models.Queries;

namespace TemplateApi.Domain.Services;
public class PersonService : IPersonService
{
    private readonly IUnitOfWork _uow;

    public PersonService(IUnitOfWork uow) => _uow = uow ?? throw new ArgumentNullException("IUnitOfWork");

    public async Task<PersonDto> AddPerson(AddPersonDto addPErson)
    {
        try
        {
            _uow.Logger.Debug("Add Person", addPErson);
            var person = _uow.Mapper.Map<PersonDto>(addPErson);
            var model = _uow.Mapper.Map<Person>(person);
            person.Id = _uow.PersonRepository.Add(model);
            await _uow.SaveAsync();
            if (person.Name == "string" || person.SurName == "string") throw new Exception("String is not an acetabble name for a person");
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

    public async Task<PersonDto> GetPersonById(string id)
    {
        _uow.Logger.Debug("Get person by id");
        return _uow.Mapper.Map<PersonDto>(await _uow.PersonRepository.GetByIdAsync(id));
    }

    public PaginationResponse CountPeople(PersonQuery queryDto, Pagination pagination)
        => _uow.PersonRepository.CountPeople(queryDto, pagination);

    public async Task<IEnumerable<PersonDto>> GetPeopleAsync(PersonQuery queryDto)
        => _uow.Mapper.Map<IEnumerable<PersonDto>>(await _uow.PersonRepository.GetPeopleAsync(queryDto));

    public async Task<IEnumerable<PersonDto>> GetPeoplePaginatedAsync(PersonQuery queryDto, Pagination pagination)
        => _uow.Mapper.Map<IEnumerable<PersonDto>>(await _uow.PersonRepository.GetPeoplePaginatedAsync(queryDto, pagination));

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

    public async Task<bool> AnyAsync(string id) => await _uow.PersonRepository.AnyAsync(id);

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

    public async Task<AddPersonDto> GetRandomPerson()
    {
        var randomName = new RandomPersonName();

        var randomDate = new RandomDateTime();

        var person = new AddPersonDto()
        {
            Name = randomName.GetName(),
            SurName = randomName.GetName(),
            CPF = CPFValidator.GerarCpf(),
            BirthDay = randomDate.NextDateOnly(),
        };


        return person;
    }

    private class Root
    {
        public List<string> data { get; set; }
    }
    private class RandomDateTime
    {
        DateTime start;
        Random gen;
        int range;

        public RandomDateTime()
        {
            start = new DateTime(1970, 1, 1);
            gen = new Random();
            range = (DateTime.Today - start).Days;
        }

        public DateTime Next()
        {
            return start.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        }

        public DateOnly NextDateOnly()
        {
            return DateOnly.FromDateTime(Next());
        }

    }
}

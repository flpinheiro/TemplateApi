using TemplateApi.CrossCutting.Exceptions;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Domain.Services;
internal class PersonService : IPersonService
{
    private readonly IUnitOfWork _uow;

    public PersonService(IUnitOfWork uow)
        => _uow = uow ?? throw new ArgumentNullException("IUnitOfWork");


    public async Task<PersonDto> AddPerson(PersonDto person)
    {
        try
        {
            _uow.Logger.Debug("Add Person", person);
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
}

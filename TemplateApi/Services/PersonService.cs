using AutoMapper;
using TemplateApi.CrossCutting.Exceptions;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Services;
public class PersonService : IPersonService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PersonService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow ?? throw new ArgumentNullException("IUnitOfWork");
        _mapper = mapper ?? throw new ArgumentNullException("AutoMapper");
    }

    public async Task<PersonDto> AddPerson(PersonDto person)
    {
        try
        {
            var model = _mapper.Map<Person>(person);
            person.Id = _uow.PersonRepository.Add(model);
            await _uow.SaveAsync();
            if (person.Name == "string") throw new Exception("String name exception");
            return person;
        }
        catch (Exception)
        {
            _uow.RollBack();
            throw;
        }
    }

    public async Task<PersonDto> DeletePerson(string id)
    {
        try
        {
            var person = await _uow.PersonRepository.GetByIdAsync(id);
            if (person == null) throw new PersonNotFoundException();
            _uow.PersonRepository.Delete(person);
            await _uow.SaveAsync();
            return _mapper.Map<PersonDto>(person);
        }
        catch (Exception)
        {
            _uow.RollBack();
            throw;
        }
    }

    public async Task<IEnumerable<PersonDto>> GetAllPerson()
    {
        return _mapper.Map<IEnumerable<PersonDto>>(await _uow.PersonRepository.GetAllAsync());
    }

    public async Task<PersonDto> GetPersonById(string id)
    {
        return _mapper.Map<PersonDto>(await _uow.PersonRepository.GetByIdAsync(id));
    }

    public async Task<IEnumerable<PersonDto>> GetPersonByName(string name)
    {
        return _mapper.Map<IEnumerable<PersonDto>>(await _uow.PersonRepository.GetByNameAsync(name));
    }

    public async Task UpdatePerson(string id, PersonDto person)
    {
        try
        {
            if (!await _uow.PersonRepository.AnyAsync(id)) throw new PersonNotFoundException();
            var model = _mapper.Map<Person>(person);
            model.Id = id;
            _uow.PersonRepository.Update(model);
            await _uow.SaveAsync();
        }
        catch (Exception)
        {
            _uow.RollBack();
            throw;
        }
    }
}

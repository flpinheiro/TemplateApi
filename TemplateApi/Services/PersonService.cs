using AutoMapper;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PersonService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task AddPerson(PersonDto person)
        {
            try
            {
                var model = _mapper.Map<Person>(person);
                _uow.PersonRepository.Add(model);
                await _uow.SaveAsync();
            }
            catch (Exception)
            {
                _uow.RollBack();
                throw;
            }
            finally
            {
                _uow.Commit();
            }
        }

        public async Task DeletePerson(string id)
        {
            try
            {
                var person = await _uow.PersonRepository.GetByIdAsync(id);
                if (person == null) throw new Exception("not exist");
                var model = _mapper.Map<Person>(person);
                _uow.PersonRepository.Delete(model);
                _uow.Save();
            }
            catch (Exception)
            {
                _uow.RollBack();
                throw;
            }
            finally
            {
                _uow.Commit();
            }
        }

        public async Task<IEnumerable<PersonDto>> GetAllPerson()
        {
            
            return _mapper.Map<IEnumerable<PersonDto>>( await _uow.PersonRepository.GetAllAsync());
        }

        public async Task<PersonDto> GetPersonById(string id)
        {
            return _mapper.Map<PersonDto>(await _uow.PersonRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<PersonDto>> GetPersonByName(string name)
        {
            return _mapper.Map<IEnumerable<PersonDto>>(await _uow.PersonRepository.GetAsync(a=> a.Name.Equals(name)));
        }

        public async Task UpdatePerson(string id, PersonDto person)
        {
            try
            {
                if (await _uow.PersonRepository.GetByIdAsync(id) == null) throw new Exception("not exist");
                var model = _mapper.Map<Person>(person);
                _uow.PersonRepository.Update(model);
                _uow.Save();
            }
            catch (Exception)
            {
                _uow.RollBack();
                throw;
            }
            finally
            {
                _uow.Commit();
            }
        }
    }
}

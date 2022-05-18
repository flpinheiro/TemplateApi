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

        public Task DeletePerson(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersonDto>> GetAllPerson()
        {
            throw new NotImplementedException();
        }

        public Task<PersonDto> GetPersonById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersonDto>> GetPersonByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePerson(string id, PersonDto person)
        {
            throw new NotImplementedException();
        }
    }
}

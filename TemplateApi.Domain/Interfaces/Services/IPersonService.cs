using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Domain.Interfaces.Services
{
    public interface IPersonService
    {
        Task<PersonDto> GetPersonById(string id);
        Task<IEnumerable<PersonDto>> GetPersonByName(string name);
        Task<IEnumerable<PersonDto>> GetAllPerson();

        Task AddPerson(PersonDto person);
        Task UpdatePerson(string id, PersonDto person);
        Task DeletePerson(string id);

    }
}

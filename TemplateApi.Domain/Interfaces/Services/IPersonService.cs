using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Domain.Interfaces.Services
{
    public interface IPersonService
    {
        Task<PersonDto> GetPersonById(string id);
        Task<IEnumerable<PersonDto>> GetPersonByName(string name);
        Task<IEnumerable<PersonDto>> GetAllPerson();

        Task<PersonDto> AddPerson(PersonDto person);
        Task UpdatePerson(string id, PersonDto person);
        Task<PersonDto> DeletePerson(string id);

    }
}

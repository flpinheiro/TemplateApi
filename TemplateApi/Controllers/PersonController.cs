using Microsoft.AspNetCore.Mvc;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : Controller
{
    private readonly IPersonService _service;
    public PersonController(IPersonService service)
    {
        _service = service ?? throw new ArgumentNullException("IPersonService");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
    {
        return Ok(await _service.GetAllPerson());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> GetPerson([FromRoute] string id)
    {
        return Ok(await _service.GetPersonById(id));
    }
    [HttpGet("Name/{name}")]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersonByName([FromRoute] string name)
    {
        return Ok(await _service.GetPersonByName(name));
    }

    [HttpPost]
    public async Task<ActionResult<PersonDto>> Create([FromBody] PersonDto person)
    {
        return Ok(await _service.AddPerson(person));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PersonDto>> Edit([FromRoute] string id, [FromBody] PersonDto person)
    {
        await _service.UpdatePerson(id, person);
        return Ok(person);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PersonDto>> Delete([FromRoute] string id)
    {
        var person = await _service.DeletePerson(id);
        return Ok(person);
    }
}


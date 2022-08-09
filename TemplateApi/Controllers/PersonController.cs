using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Controllers;

[ApiController]
[Route("[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
public class PersonController : Controller
{
    private readonly IPersonService _service;
    public PersonController(IPersonService service)
    {
        _service = service ?? throw new ArgumentNullException("IPersonService");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
    {
        return Ok(await _service.GetAllPerson());
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PersonDto>> GetPerson([FromRoute] string id)
    {
        return Ok(await _service.GetPersonById(id));
    }
    [HttpGet("Name/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersonByName([FromRoute] string name)
    {
        return Ok(await _service.GetPersonByName(name));
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<PersonDto>> Create([FromBody] AddPersonDto addPerson)
    {
        var personDto = await _service.AddPerson(addPerson);
        return CreatedAtAction(nameof(Create), new { id = personDto.Id }, addPerson);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PersonDto>> Edit([FromRoute] string id, [FromBody] PersonDto person)
    {
        await _service.UpdatePerson(id, person);
        return Ok(person);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PersonDto>> Delete([FromRoute] string id)
    {
        var person = await _service.DeletePerson(id);
        return Ok(person);
    }
}


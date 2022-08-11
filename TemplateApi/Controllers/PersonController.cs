using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TemplateApi.CrossCutting.Extensions;
using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Controllers;

[ApiController]
[Route("[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
public class PersonController : Controller
{
    private readonly IPersonService _service;
    private readonly IValidator<AddPersonDto> _validator;
    public PersonController(IPersonService service, IValidator<AddPersonDto> validator)
    {
        _service = service ?? throw new ArgumentNullException("IPersonService");
        _validator = validator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll([FromQuery] Pagination pagination)
    {
        return Ok(await _service.GetAllPerson(pagination));
    }
    [HttpGet("Count")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PaginationResponse> CountAll([FromQuery] Pagination pagination)
    {
        return Ok(_service.CountAllPerson(pagination));
    }
    [HttpGet("ExportToExcel")]
    [Produces(ExcelExtensions.ContentType)]
    public async Task<FileStreamResult> ExporttoExcelAll()
    {
        var people = await _service.GetAllPerson();
        return _service.ExportToExcel(people);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PersonDto>> GetPerson([FromRoute] string id)
    {
        return Ok(await _service.GetPersonById(id));
    }
    [HttpGet("Name/{name}/ExportToExcel")]
    [Produces(ExcelExtensions.ContentType)]
    public async Task<FileStreamResult> ExportoExcelByName([FromRoute] string name)
    {
        var people = await _service.GetPersonByName(name);
        return _service.ExportToExcel(people);
    }
    [HttpGet("Name/{name}")]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersonByName([FromRoute] string name, [FromQuery] Pagination pagination)
    {
        return Ok(await _service.GetPersonByName(name, pagination));
    }
    [HttpGet("Name/{name}/Count")]
    public ActionResult<PaginationResponse> CountPersonByName([FromRoute] string name, [FromQuery] Pagination pagination)
    {
        return Ok(_service.CountPersonByName(name, pagination));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<PersonDto>> Create([FromBody] AddPersonDto addPerson)
    {
        var validate = _validator.Validate(addPerson);
        if (validate != null && !validate.IsValid) return BadRequest(validate.Errors);

        var personDto = await _service.AddPerson(addPerson);
        return CreatedAtAction(nameof(Create), new { id = personDto.Id }, addPerson);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PersonDto>> Edit([FromRoute] string id, [FromBody] PersonDto person)
    {
        var validate = _validator.Validate(person);
        if (validate != null && !validate.IsValid) return BadRequest(validate.Errors);

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


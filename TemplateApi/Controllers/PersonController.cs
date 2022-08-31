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
    private readonly IValidator<AddPersonDto> _personDtoValidator;
    private readonly IValidator<PersonQueryDto> _personQueryDtoValidator;
    public PersonController(IPersonService service, IValidator<AddPersonDto> validator, IValidator<PersonQueryDto> personQueryDtoValidator)
    {
        _service = service ?? throw new ArgumentNullException("IPersonService");
        _personDtoValidator = validator;
        _personQueryDtoValidator = personQueryDtoValidator;
    }
    /// <summary>
    /// Get all person who satisfies the shearch criteria, separeted by pagination and sorted
    /// </summary>
    /// <remarks>
    /// <para>Get all person who satisfies the shearch criteria, separeted by pagination and sorted</para>
    /// <para><paramref name="query"/> CPF must be a valid Brazilian cpf </para>
    /// </remarks>
    /// <param name="query">search criteria</param>
    /// <param name="pagination">pagination definition</param>
    /// <returns>a list of <see cref="PersonDto"/> if search criteria is corrected or badRequest if not.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPeople([FromQuery] PersonQueryDto query, [FromQuery] Pagination pagination)
    {
        var validate = _personQueryDtoValidator.Validate(query);
        if (validate != null && !validate.IsValid) return BadRequest(validate.Errors);
        var people = await _service.GetPeoplePaginatedAsync(query, pagination);
        return Ok(people);
    }

    [HttpGet("Count")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PaginationResponse> CountPeople([FromQuery] PersonQueryDto query, [FromQuery] Pagination pagination)
    {
        var validate = _personQueryDtoValidator.Validate(query);
        if (validate != null && !validate.IsValid) return BadRequest(validate.Errors);

        return Ok(_service.CountPeople(query, pagination));
    }

    [HttpGet("ExportToExcel")]
    [Produces(ExcelExtensions.ContentType)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> ExportoExcelAll([FromQuery] PersonQueryDto query)
    {
        var validate = _personQueryDtoValidator.Validate(query);
        if (validate != null && !validate.IsValid) return BadRequest(validate.Errors);

        var people = await _service.GetPeopleAsync(query);
        return _service.ExportToExcel(people);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PersonDto>> GetPerson([FromRoute] string id)
    {
        return Ok(await _service.GetPersonById(id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<PersonDto>> Create([FromBody] AddPersonDto addPerson)
    {
        var validate = _personDtoValidator.Validate(addPerson);
        if (validate != null && !validate.IsValid) return BadRequest(validate.Errors);

        var personDto = await _service.AddPerson(addPerson);
        return CreatedAtAction(nameof(Create), new { id = personDto.Id }, addPerson);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PersonDto>> Edit([FromRoute] string id, [FromBody] PersonDto person)
    {
        var validate = _personDtoValidator.Validate(person);
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


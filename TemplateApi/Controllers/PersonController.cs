﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TemplateApi.CrossCutting.Extensions;
using TemplateApi.CrossCutting.Models;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dto;
using TemplateApi.Domain.Models.Queries;

namespace TemplateApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces("application/json")]
public class PersonController : Controller
{
    private readonly IPersonService _service;
    private readonly IValidator<AddPersonDto> _personDtoValidator;
    private readonly IValidator<PersonQuery> _personQueryDtoValidator;
    public PersonController(IPersonService service, IValidator<AddPersonDto> validator, IValidator<PersonQuery> personQueryDtoValidator)
    {
        _service = service ?? throw new ArgumentNullException("IPersonService");
        _personDtoValidator = validator;
        _personQueryDtoValidator = personQueryDtoValidator;
    }

    /// <summary>
    /// Get all person who satisfies the shearch criteria, separeted by pagination and sorted
    /// </summary>
    /// <remarks>
    /// <para>Get all person who satisfies the search criteria, separeted by pagination and sorted</para>
    /// <para><paramref name="query"/> CPF must be a valid Brazilian cpf </para>
    /// </remarks>
    /// <param name="query">search criteria</param>
    /// <param name="pagination">pagination definition</param>
    /// <returns>a list of <see cref="PersonDto"/> if search criteria is corrected or badRequest if not.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPeople([FromQuery] PersonQuery query, [FromQuery] Pagination pagination)
    {
        var validate = _personQueryDtoValidator.Validate(query);
        if (validate != null && !validate.IsValid) return BadRequest(validate.Errors);
        var people = await _service.GetPeoplePaginatedAsync(query, pagination);
        return Ok(people);
    }

    /// <summary>
    /// Return the number of people that exist at the present query search, separating by pages
    /// </summary>
    /// <remarks>
    /// Count all people that satisfies the search criteria, separating by pages 
    /// </remarks>
    /// <param name="query">search criteria</param>
    /// <param name="pagination">pagination definition</param>
    /// <returns> Return <see cref="PaginationResponse"/> with the number of pages and the total elements that exist at the search criteria</returns>
    [HttpGet("Count")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PaginationResponse> CountPeople([FromQuery] PersonQuery query, [FromQuery] Pagination pagination)
    {
        var validate = _personQueryDtoValidator.Validate(query);
        if (validate != null && !validate.IsValid) return BadRequest(validate.Errors);

        return Ok(_service.CountPeople(query, pagination));
    }

    /// <summary>
    /// Create an excel file with all elements presented at the search by the parameters
    /// </summary>
    /// <remarks>
    /// Create an excel file with all elements presented at the search by the parameters
    /// </remarks>
    /// <param name="query">search criteria</param>
    /// <returns>Return excel file</returns>
    [HttpGet("ExportToExcel")]
    [Produces(ExcelExtensions.ContentType)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> ExportoExcelAll([FromQuery] PersonQuery query)
    {
        var validate = _personQueryDtoValidator.Validate(query);
        if (validate != null && !validate.IsValid) return BadRequest(validate.Errors);

        var people = await _service.GetPeopleAsync(query);
        return _service.ExportToExcel(people);
    }

    /// <summary>
    /// Return person by the unique id
    /// </summary>
    /// <remarks>
    /// Return person by the unique id
    /// </remarks>
    /// <param name="id"> of the database, is unique for each person</param>
    /// <returns><see cref="PersonDto"/> with all necessary data</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonDto>> GetPerson([FromRoute] string id)
    {
        var person = await _service.GetPersonById(id);
        if (person == null) return NotFound();
        return Ok(person);
    }

    /// <summary>
    /// Create a new <see cref="PersonDto"/> at the data base
    /// </summary>
    /// <remarks>
    /// Create a new <see cref="PersonDto"/> at the data base
    /// </remarks>
    /// <param name="addPerson"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonDto>> Create([FromBody] AddPersonDto addPerson)
    {
        var validate = _personDtoValidator.Validate(addPerson);
        if (validate != null && !validate.IsValid) return BadRequest(validate.Errors);

        var personDto = await _service.AddPerson(addPerson);
        return Created(personDto.Id ?? "", personDto);
    }

    /// <summary>
    /// Edit a existing person with new data
    /// </summary>
    /// <remarks>
    /// Edit a existing person with new data
    /// </remarks>
    /// <param name="id">person id</param>
    /// <param name="person">person data to be modified</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Edit([FromRoute] string id, [FromBody] PersonDto person)
    {
        var validate = _personDtoValidator.Validate(person);
        if (validate != null && !validate.IsValid) return BadRequest(validate.Errors);
        if (!await _service.AnyAsync(id)) return NotFound();

        await _service.UpdatePerson(id, person);
        return NoContent();
    }

    /// <summary>
    /// Delete a person from the database
    /// </summary>
    /// <remarks>
    /// Delete a person from the database
    /// </remarks>
    /// <param name="id">person id at the database</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete([FromRoute] string id)
    {
        if (!await _service.AnyAsync(id)) return NotFound();
        await _service.DeletePerson(id);
        return NoContent();
    }

    [HttpGet("random")]
    public async Task<ActionResult<AddPersonDto>> GetRandom()
    {
        return Ok(await _service.GetRandomPerson());
    }
}


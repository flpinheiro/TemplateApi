﻿using Microsoft.AspNetCore.Mvc;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Models.Dto;

namespace TemplateApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonService _service;
        public PersonController(IPersonService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
        {
            return Ok(await _service.GetAllPerson());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetPerson(string id)
        {
            return Ok(await _service.GetPersonById(id));
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> Create([FromBody] PersonDto person)
        {
            return Ok(await _service.AddPerson(person));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit([FromRoute] string id, [FromBody] PersonDto person)
        {
            await _service.UpdatePerson(id, person);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            await _service.DeletePerson(id);
            return Ok();
        }
    }
}

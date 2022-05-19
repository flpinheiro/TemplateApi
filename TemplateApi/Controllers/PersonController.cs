using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        // GET: PersonController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
        {
             return Ok(await _service.GetAllPerson());
        }

        // GET: PersonController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetPerson(string id)
        {
            return Ok(await _service.GetPersonById(id));
        }

        // GET: PersonController/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PersonDto person)
        {
            await _service.AddPerson(person);
            return Ok();
        }


        // GET: PersonController/Edit/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit([FromRoute] string id, [FromBody] PersonDto person)
        {
            await _service.UpdatePerson(id, person);
            return Ok();
        }

        // POST: PersonController/Delete/5
        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            await _service.DeletePerson(id);
            return Ok();

        }
    }
}

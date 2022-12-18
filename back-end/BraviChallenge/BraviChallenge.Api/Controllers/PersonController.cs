using BraviChallenge.Application.Dtos.Requests;
using BraviChallenge.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BraviChallenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
            => _personService = personService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _personService.GetPersons();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _personService.GetPersonById(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task Post([FromBody] CreatePersonRequest request)
        {
            var result = await _personService.CreatePerson(request);
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] UpdatePersonRequest request)
        {
            var result = await _personService.UpdatePerson(id, request);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var result = await _personService.ExcludePerson(id);
        }
    }
}

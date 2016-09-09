using System;
using System.Threading.Tasks;
using Jatchley.Samples.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Jatchley.Samples.Data.Interfaces;

namespace Jatchley.Samples.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private IPersonRepository PersonRepository { get; set; }
        private ILogger<PersonController> Logger {get;set;}

        public PersonController(IPersonRepository repo, ILogger<PersonController> logger)
        {
            PersonRepository = repo;
            Logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllThePeople()
        {
            var result = await PersonRepository.GetAllAsync();

            return new ObjectResult(result);
        }

        [HttpGet("lastName/{input}")]
        public async Task<IActionResult> GetByLastName(string input)
        {
            var result = await PersonRepository.GetByLastNameAsync(input);

            return new ObjectResult(result);
        }

        [HttpGet("{id:guid}", Name = "GetPerson")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await PersonRepository.GetByIdAsync(id);

            if(model == null)
            {
                return NotFound();
            }

            return new ObjectResult(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] Person person)
        {
            person.Id = Guid.NewGuid();
            
            var added = await PersonRepository.AddAsync(person);

            return CreatedAtRoute("GetPerson", new {id = added.Id}, added);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var model = await PersonRepository.GetByIdAsync(id);

            if(model == null)
            {
                return NotFound();
            }

            await PersonRepository.DeleteAsync(model);

            return NoContent();
        }
    }
}
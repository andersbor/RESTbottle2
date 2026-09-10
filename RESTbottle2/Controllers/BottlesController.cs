using Microsoft.AspNetCore.Mvc;
using RESTbottle2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTbottle2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BottlesController : ControllerBase
    {
        private IBottlesRepository repo;

        public BottlesController(IBottlesRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/<BottlesController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<IEnumerable<Bottle>> Get()
        {
            var bottles = repo.GetBottles();
            return Ok(bottles);
        }

        // GET api/<BottlesController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Bottle?> Get(int id)
        {
            Bottle? bottle = repo.GetById(id);
            if (bottle == null)
            {
                return NotFound("No such bottle, id: " + id);
            }
            return Ok(bottle);
        }

        // POST api/<BottlesController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public ActionResult<Bottle> Post([FromBody] Bottle value)
        {

           Bottle newBottle = repo.AddBottle(value);
            return CreatedAtAction(nameof(Get), new { id = newBottle.Id }, newBottle);
        }

        // PUT api/<BottlesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Bottle> Put(int id, [FromBody] Bottle value)
        {
            Bottle? bottle = repo.Update(id, value);
            if (bottle == null)
            {
                return NotFound("No such bottle, id: " + id);
            }
            return Ok(bottle);
        }

        // DELETE api/<BottlesController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Bottle> Delete(int id)
        {
            Bottle? bottle = repo.DeleteById(id);
            if (bottle == null)
            {
                return NotFound("No such bottle, id: " + id);
            }
            return Ok(bottle);
        }
    }
}

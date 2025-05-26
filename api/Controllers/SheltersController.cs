using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenPolicy")]
    public class SheltersController : ControllerBase
    {
        private readonly IShelterRepository _repository;

        public SheltersController(IShelterRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Shelters
        [HttpGet]
        public List<Shelters> GetShelters()
        {
            return _repository.GetAllShelters();
        }

        // GET: api/Shelters/5
        [HttpGet("{id}", Name = "GetShelter")]
        public Shelters GetShelter(int id)
        {
            return _repository.GetShelterById(id);
        }

        // GET: api/Shelters/5/Pets
        [HttpGet("{shelterId}/Pets")]
        public ActionResult<List<Pets>> GetPetsByShelterId(int shelterId)
        {
            var pets = _repository.GetPetsByShelter(shelterId);
            if (pets == null || pets.Count == 0)
            {
                return NotFound("No pets found for this shelter.");
            }
            return Ok(pets);
        }

        [HttpGet("by-credentials")]
        public ActionResult<Shelters> GetUser([FromQuery] string email, [FromQuery] string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Email and password are required.");
            }
            var user = _repository.GetUserLogin(email, password);
            if (user == null) return NotFound();
            return Ok(user);
        }


        // POST: api/Shelters
        [HttpPost]
        public void Post([FromBody] Shelters value)
        {
            _repository.SaveToDB(value);
        }

        // PUT: api/Shelters/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Shelters value)
        {
            _repository.UpdateToDB(value);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, bool Approved)
        {
            _repository.ApprovalOfShelter(id, Approved);
            return NoContent();

        }
    }
}
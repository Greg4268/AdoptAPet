using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenPolicy")]
    public class SheltersController : ControllerBase
    {
        // GET: api/Shelters
        [HttpGet]
        public List<Shelters> GetShelters()
        {
            return Shelters.GetAllShelters();
        }

        // GET: api/Shelters/5
        [HttpGet("{id}", Name = "GetShelter")]
        public Shelters GetShelter(int id)
        {
            return Shelters.GetShelterById(id);
        }

        // GET: api/Shelters/5/Pets
        [HttpGet("{shelterId}/Pets")]
        public ActionResult<List<Pets>> GetPetsByShelterId(int shelterId)
        {
            var pets = Shelters.GetPetsByShelter(shelterId);
            if (pets == null || pets.Count == 0)
            {
                return NotFound("No pets found for this shelter.");
            }
            return Ok(pets);
        }


        // POST: api/Shelters
        [HttpPost]
        public void Post([FromBody] Shelters value)
        {
            value.SaveToDB();
        }

        // PUT: api/Shelters/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Shelters value)
        {
            value.UpdateToDB();
        }

        // DELETE: api/Shelters/5
        [HttpDelete("{id}")]
        public void Delete(int id, [FromBody] Shelters value)
        {
            value.DeleteShelter(value);
        }
    }
}
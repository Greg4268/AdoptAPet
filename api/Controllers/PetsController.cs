using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenPolicy")]
    public class PetsController : ControllerBase
    {
        // GET: api/Pets
        [HttpGet]
        public List<Pets> GetPets()
        {
            return Pets.GetAllPets();
        }

        // GET: api/Pets/5
        [HttpGet("{id}", Name = "GetPet")]
        public Pets GetPet(int id)
        {
            return Pets.GetPetById(id);
        }

        // POST: api/Pets
        [HttpPost]
        public void Post([FromBody] Pets value)
        {
            value.SaveToDB();
        }

        // PUT: api/Pets/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Pets value)
        {
            value.OldFavoritePet(value);
            value.UpdateToDB();
        }

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Pets.DeletePet(id);
        }
    }

    internal class PetsContext
    {
    }
}
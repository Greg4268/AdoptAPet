using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repository;
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
            return PetsRepository.GetAllPets();
        }

        // GET: api/Pets/5
        [HttpGet("{id}", Name = "GetPet")]
        public Pets GetPet(int id)
        {
            return PetsRepository.GetPetById(id);
        }

        // POST: api/Pets
        [HttpPost]
        public void Post([FromBody] Pets value)
        {
            value.SaveToDB();
        }

        // PUT: api/Pets/5
        [HttpPut("{petId}")]
        public void Put(int petId, [FromBody] Pets value)
        {
            value.PetProfileId = petId;
            value.UpdateToDB();
        }

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            PetsRepository.DeletePet(id);
        }
    }

    internal class PetsContext
    {
    }
}
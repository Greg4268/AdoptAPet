using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public string GetShelter(int id)
        {
            return "value";
        }

        // POST: api/Shelters
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Shelters/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Shelters/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

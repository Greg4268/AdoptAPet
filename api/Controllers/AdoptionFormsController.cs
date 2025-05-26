using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repository;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenPolicy")]
    public class AdoptionFormsController : ControllerBase
    {
        private readonly IAdoptionFormRepository _repository;

        public AdoptionFormsController(IAdoptionFormRepository repository)
        {
            _repository = repository;
        }

        // GET: api/AdoptionForm
        [HttpGet]
        public List<AdoptionForm> GetAdoptionForms()
        {
            return _repository.GetAllAdoptionForms();
        }

        // GET: api/AdoptionForm/5
        [HttpGet("{id}", Name = "GetAdoptionForm")]
        public AdoptionForm GetAdoptionForm(int id)
        {
            return _repository.GetAdoptionFormById(id);
        }

        // POST: api/AdoptionForm
        [HttpPost]
        public void Post([FromBody] AdoptionForm value)
        {
            _repository.SaveToDB();
        }

        // PUT: api/AdoptionForm/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AdoptionForm value)
        {
            _repository.UpdateToDB();
        }

        // DELETE: api/AdoptionForm/5
        [HttpDelete("{id}")]
        public void Delete(int id, [FromBody] AdoptionForm value)
        {
            _repository.DeleteAdoptionForm(value);
        }
    }
}
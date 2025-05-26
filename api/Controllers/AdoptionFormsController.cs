using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Cors;
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
            _repository.SaveToDB(value);
        }

        // PUT: api/AdoptionForm/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AdoptionForm value)
        {
            _repository.UpdateToDB(value);
        }

        // DELETE: api/AdoptionForm/5
        [HttpDelete("{id}")]
        public void Delete(int id, [FromBody] AdoptionForm value)
        {
            _repository.DeleteAdoptionForm(value);
        }
    }
}
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenPolicy")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentsController(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Appointment
        [HttpGet]
        public List<Appointment> GetAppointment()
        {
            return _repository.GetAllAppointments();
        }

        // GET: api/Appointment/5
        [HttpGet("{id}", Name = "GetAppointment")]
        public Appointment GetAppointment(int id)
        {
            return _repository.GetAppointmentById(id);
        }

        // GET: api/Appointment/Pet/5
        [HttpGet("ByUser/{user}")]
        public List<Appointment> GetAppointmentsByUser(int user)
        {
            return _repository.GetAppointmentsByUserId(user);
        }

        // GET: api/Appointment/Pet/5
        [HttpGet("ByPet/{pet}")]
        public List<Appointment> GetAppointmentsByPet(int pet)
        {
            return _repository.GetAppointmentByPet(pet);
        }

        // POST: api/Appointment
        [HttpPost]
        public void Post([FromBody] Appointment value)
        {
            _repository.SaveToDB(value);
        }

        // PUT: api/Appointment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Appointment value)
        {
            _repository.UpdateToDB(value);
        }

        // DELETE: api/Appointment/5
        [HttpDelete("{id}")]
        public void Delete([FromBody] Appointment value)
        {
            _repository.DeleteAppointment(value);
        }
    }
}
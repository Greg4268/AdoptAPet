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
    public class AppointmentsController : ControllerBase
    {
        // GET: api/Appointments
        [HttpGet]
        public List<Appointments> GetAppointments()
        {
            return Appointments.GetAllAppointments();
        }

        // GET: api/Appointments/5
        [HttpGet("{id}", Name = "GetAppointment")]
        public Appointments GetAppointment(int id)
        {
            return Appointments.GetAppointmentById(id);
        }

        // POST: api/Appointments
        [HttpPost]
        public void Post([FromBody] Appointments value)
        {
            value.SaveToDB();
        }

        // PUT: api/Appointments/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Appointments value)
        {
            value.UpdateToDB();
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public void Delete(int id, [FromBody] Appointments value)
        {
            value.DeleteAppointment(value);
        }
    }
}
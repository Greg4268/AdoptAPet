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
        // GET: api/Appointment
        [HttpGet]
        public List<Appointment> GetAppointment()
        {
            return Appointment.GetAllAppointment();
        }

        // GET: api/Appointment/5
        [HttpGet("{id}", Name = "GetAppointment")]
        public Appointment GetAppointment(int id)
        {
            return Appointment.GetAppointmentById(id);
        }

        // POST: api/Appointment
        [HttpPost]
        public void Post([FromBody] Appointment value)
        {
            value.SaveToDB();
        }

        // PUT: api/Appointment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Appointment value)
        {
            value.UpdateToDB();
        }

        // DELETE: api/Appointment/5
        [HttpDelete("{id}")]
        public void Delete(int id, [FromBody] Appointment value)
        {
            value.DeleteAppointment(value);
        }
    }
}
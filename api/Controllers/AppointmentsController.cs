using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;

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
            return Appointment.GetAllAppointments();
        }

        // GET: api/Appointment/5
        [HttpGet("{id}", Name = "GetAppointment")]
        public Appointment GetAppointment(int id)
        {
            return Appointment.GetAppointmentById(id);
        }

        // GET: api/Appointment/Pet/5
        [HttpGet("ByUser/{user}")]
        public List<Appointment> GetAppointmentsByUser(int user)
        {
            return Appointment.GetAppointmentsByUserId(user);
        }

        // GET: api/Appointment/Pet/5
        [HttpGet("ByPet/{pet}")]
        public List<Appointment> GetAppointmentsByPet(int pet)
        {
            return Appointment.GetAppointmentByPet(pet);
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
            value.DeleteAppointment(id);
        }
    }
}
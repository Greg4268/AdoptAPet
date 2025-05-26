using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repository
{
    public interface IAppointmentRepository
    {
        public List<Appointment> GetAllAppointments();
        public void SaveToDB();
        public void UpdateToDB();
        public Appointment GetAppointmentById(int appointmentId);
        public List<Appointment> GetAppointmentsByUserId(int userId);
        public void DeleteAppointment(int id);
        public List<Appointment> GetAppointmentByPet(int petId);
    }
}
using api.Models;

namespace api.Repository
{
    public interface IAppointmentRepository
    {
        public List<Appointment> GetAllAppointments();
        public void SaveToDB(Appointment app);
        public void UpdateToDB(Appointment app);
        public Appointment GetAppointmentById(int appointmentId);
        public List<Appointment> GetAppointmentsByUserId(int userId);
        public void DeleteAppointment(Appointment app);
        public List<Appointment> GetAppointmentByPet(int petId);
    }
}
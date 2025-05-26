namespace api.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int UserId { get; set; }
        public int PetProfileId { get; set; }
        public bool Deleted { get; set; }
   }
}
namespace api.Models
{
    public class AdoptionForm
    {
        public int FormId { get; set; }
        public int UserId { get; set; }
        public DateTime FormDate { get; set; }
        public bool Approved { get; set; }
        public bool Deleted { get; set; }
    }
}
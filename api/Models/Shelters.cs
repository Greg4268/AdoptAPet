namespace api.Models
{
    public class Shelters
    {
        public int ShelterId { get; set; }
        public string? ShelterUsername { get; set; }
        public string ShelterPassword { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? HoursOfOperation { get; set; }
        public bool Deleted { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool Approved { get; set; }
        public string AccountType { get; set; } = string.Empty;
    }
}

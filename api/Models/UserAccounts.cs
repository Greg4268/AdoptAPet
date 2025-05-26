namespace api.Models
{
    public class UserAccounts
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? AccountType { get; set; }
        public bool Deleted { get; set; }
        public string? Address { get; set; }
        public double YardSize { get; set; }
        public bool Fenced { get; set; }
        public bool HasForm { get; set; }
   }
}
namespace api.Models
{
    public class Pets
    {
        public int PetProfileId { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Breed { get; set; }
        public string? Name { get; set; }
        public string? Species { get; set; }
        public bool Deleted { get; set; }
        public int ShelterId { get; set; }
        public string? ImageUrl { get; set; }
        public int FavoriteCount { get; set; }
    }
}
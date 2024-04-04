namespace api.Models
{
    public class Pets
    {
        public int PetProfileID { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
        public bool Availability { get; set; }
        public string Species { get; set; }
        public bool CanVisit { get; set; }
        public int ReturnedCount { get; set; }
        public int FavoriteCount { get; set; }
    }
}
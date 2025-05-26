using Npgsql;
using api.Data;
namespace api.Models
{
    public class FavoritedPet
    {
        public int UserId { get; set; }
        public int PetProfileId { get; set; }
        public bool Favorited { get; set; }
    }
}
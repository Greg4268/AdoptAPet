using api.Models;

namespace api.Repository
{
    public interface IFavoritedPetRepository
    {
        public List<Pets> GetFavoritePets(int user);
        public void FavoritePet(int user, int pet);
        public void UpdateUnfavorite(int userId, int petProfileId);
    }
}
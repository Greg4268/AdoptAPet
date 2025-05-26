using api.Models;

namespace api.Repository
{
    public interface IPetsRepository
    {
        public List<Pets> GetAllPets();
        public void SaveToDB(Pets value);
        public void UpdateToDB(Pets pet);
        public void OldFavoritePet(Pets value);
        public void DeletePet(int petId);
        public Pets GetPetById(int petProfileId);
 
    }
}
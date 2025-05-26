using api.Models;

namespace api.Repository
{
    public interface IShelterRepository
    {
        public List<Shelters> GetAllShelters();
        public void SaveToDB(Shelters shelter);
        public void UpdateToDB(Shelters shelter);
        public Shelters GetShelterById(int shelterId);
        public void ApprovalOfShelter(int shelterId, bool approved);
        public  List<Pets> GetPetsByShelter(int shelterId);
        public Shelters GetUserLogin(string email, string password);
    }
}
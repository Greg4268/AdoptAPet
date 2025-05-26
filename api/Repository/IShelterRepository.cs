using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repository
{
    public interface IShelterRepository
    {
        public List<Shelters> GetAllShelters();
        public void SaveToDB();
        public void UpdateToDB();
        public Shelters GetShelterById(int shelterId);
        public void ApprovalOfShelter(int shelterId, bool approved);
        public  List<Pets> GetPetsByShelter(int shelterId);
        public Shelters GetUserLogin(string email, string password);
    }
}
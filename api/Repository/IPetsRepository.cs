using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repository
{
    public interface IPetsRepository
    {

        public List<Pets> GetAllPets();
        public void SaveToDB();
        public void UpdateToDB();
        public void OldFavoritePet(Pets value);
        public void DeletePet(int petId);
        public Pets GetPetById(int petProfileId);
 
    }
}
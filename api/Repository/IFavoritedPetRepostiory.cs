using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repository
{
    public interface IFavoritedPetRepostiory
    {
        public List<Pets> GetFavoritePets(int user);
        public void FavoritePet(int user, int pet);
        public void UpdateUnfavorite(int userId, int petProfileId);
    }
}
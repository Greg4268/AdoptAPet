using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenPolicy")]
    public class FavoriteController : ControllerBase
    {
        // GET: api/Favorite
        [HttpGet]
        public List<FavoritedPet> GetFavorites(int user)
        {
            return FavoritedPet.GetFavoritePets(user);
        }

        // GET: api/Favorite/5
        [HttpGet("{user}, {pet}", Name = "GetFavoriteInstance")]
        public string GetFavoriteInstance(int user, int pet)
        {
            return "value";
        }

        // POST: api/Favorite
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Favorite/5
        [HttpPut("{id}")]
        public void ToggleFavorite(int user, int pet)
        {
            FavoritedPet.FavoritePet(user, pet);
        }

        // DELETE: api/Favorite/5
        [HttpDelete("{id}")]
        public void StraightDelete(int user, int pet)
        {
            FavoritedPet.UpdateUnfavorite(user, pet);
        }
    }
}

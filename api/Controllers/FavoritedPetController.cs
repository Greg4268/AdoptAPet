using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repository;
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
        private readonly IFavoritedPetRepository _repository;
        public FavoritedPetController(IFavoritedPetRepository repository)
        {
            _repository = repository;
        }
        
        // GET: api/Favorite
        [HttpGet]
        public List<Pets> GetFavorites(int user)
        {
            return _repository.GetFavoritePets(user);
        }

        // GET: api/Favorite/5
        [HttpGet("{user}, {pet}")]
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
        [HttpPut("{user:int},{pet:int}")]
        public IActionResult ToggleFavorite(int user, int pet)
        {
            try
            {
                _repository.FavoritePet(user, pet);  // Assume this toggles the favorite status
                return Ok(new { Message = "Favorite status toggled successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error toggling favorite status", Error = ex.Message });
            }
        }

        // DELETE: api/Favorite/5
        [HttpDelete("{user}, {pet}")]
        public void StraightDelete(int user, int pet)
        {
            _repository.UpdateUnfavorite(user, pet);
        }
    }
}
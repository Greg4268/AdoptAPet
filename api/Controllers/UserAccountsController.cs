using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenPolicy")]
    public class UserAccountsController : ControllerBase
    {
        private readonly IUserAccountsRepository _repository;

        public UserAccountsController(IUserAccountsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/UserAccounts
        [HttpGet]
        public ActionResult<List<UserAccounts>> GetUserAccounts()
        {
            return Ok(_repository.GetAllUsers());
        }

        // GET: api/UserAccounts/by-id/5
        [HttpGet("by-id/{UserId:int}", Name = "GetUserById")]
        public ActionResult<UserAccounts> GetUserById(int UserId)
        {
            var user = _repository.GetUserByIdd(UserId);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // GET: api/UserAccounts/by-credentials
        // Note: Adjusting to pass email as a query parameter for better design, assuming password remains
        // GET: api/UserAccounts/by-credentials
        [HttpGet("by-credentials")]
        public ActionResult<UserAccounts> GetUser([FromQuery] string email, [FromQuery] string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Email and password are required.");
            }
            var user = _repository.GetUserById(email, password);
            if (user == null) return NotFound();
            return Ok(user);
        }
        // POST: api/UserAccounts
        [HttpPost]
        public void Post([FromBody] UserAccounts value)
        {
            _repository.SaveToDB(value);
        }

        // PUT: api/UserAccounts/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserAccounts value)
        {
            Console.WriteLine($"Received for update - UserId: {id}, Address: {value.Address}, YardSize: {value.YardSize}, Fenced: {value.Fenced}");
            if (value == null || id != value.UserId)
            {
                return BadRequest("Invalid user data");
            }

            _repository.UpdateToDB(value);  // Ensure this method correctly uses the passed 'id'
            return Ok();
        }

        [HttpPut("{userId}/toggle-delete")]
        public IActionResult ToggleUserDeletion(int userId, bool deleted)
        {
            try
            {
                _repository.DeleteUser(userId, deleted);
                return Ok(new { success = true, message = "User deletion status toggled." });
            }
            catch (MySqlException sqlEx)
            {
                return StatusCode(500, new { success = false, message = sqlEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }




    }
}
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
    public class UserAccountsController : ControllerBase
    {
        // GET: api/UserAccounts
    [HttpGet]
    public ActionResult<List<UserAccounts>> GetUserAccounts()
    {
        return Ok(UserAccounts.GetAllUsers());
    }

    // GET: api/UserAccounts/by-id/5
    [HttpGet("by-id/{UserId:int}", Name = "GetUserById")]
    public ActionResult<UserAccounts> GetUserById(int UserId)
    {
        var user = UserAccounts.GetUserByIdd(UserId);
        if (user == null) return NotFound();
        return Ok(user);
    }

    // GET: api/UserAccounts/by-credentials
    // Note: Adjusting to pass email as a query parameter for better design, assuming password remains
    [HttpGet("by-credentials")]
    public ActionResult<UserAccounts> GetUser([FromQuery] string email, [FromQuery] string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return BadRequest("Email and password are required.");
        }
        var user = UserAccounts.GetUserById(email, password);
        if (user == null) return NotFound();
        return Ok(user);
    }
        // POST: api/UserAccounts
        [HttpPost]
        public void Post([FromBody] UserAccounts value)
        {
            value.SaveToDB();
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

            value.UpdateToDB(id);  // Ensure this method correctly uses the passed 'id'
            return Ok();
        }

        // DELETE: api/UserAccounts/5
        [HttpDelete("{id}")]
        public void Delete(UserAccounts value)
        {
            value.DeleteUser(value);
        }
    }
}
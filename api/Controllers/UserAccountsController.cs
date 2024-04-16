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
        public List<UserAccounts> GetUserAccounts()
        {
            return UserAccounts.GetAllUsers();
        }

        // GET: api/UserAccounts/5
        [HttpGet("{id}", Name = "Get")]
        public UserAccounts GetUser(int id)
        {
            return UserAccounts.GetUserById(id);
        }

        // POST: api/UserAccounts
        [HttpPost]
        public void Post([FromBody] UserAccounts value)
        {
            value.SaveToDB();
        }

        // PUT: api/UserAccounts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserAccounts value)
        {
            value.UpdateToDB()
        }

        // DELETE: api/UserAccounts/5
        [HttpDelete("{id}")]
        public void Delete(UserAccounts value)
        {
            value.DeleteUser(value);
        }
    }
}

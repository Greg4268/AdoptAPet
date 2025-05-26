using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repository
{
    public interface IUserAccountsRepository
    {
        public List<UserAccounts> GetAllUsers();
        public void SaveToDB();
        public void UpdateToDB(int userId);
        public UserAccounts GetUserById(string email, string password);
        public UserAccounts GetUserByIdd(int userId);
        public void DeleteUser(int userId, bool Deleted);
    }
}
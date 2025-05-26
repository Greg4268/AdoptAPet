using api.Models;

namespace api.Repository
{
    public interface IUserAccountsRepository
    {
        public List<UserAccounts> GetAllUsers();
        public void SaveToDB(UserAccounts user);
        public void UpdateToDB(UserAccounts user);
        public UserAccounts GetUserById(string email, string password);
        public UserAccounts GetUserByIdd(int userId);
        public void DeleteUser(int userId, bool Deleted);
    }
}
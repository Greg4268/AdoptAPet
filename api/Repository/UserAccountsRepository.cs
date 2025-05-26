using api.Models;
using api.Data;
using Npgsql;

namespace api.Repository
{
    public class UserAccountsRepository : IUserAccountsRepository
    {
        private readonly GetPublicConnection cs = new();
        public List<UserAccounts> GetAllUsers()
        {
            List<UserAccounts> myUsers = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM \"UserAccounts\"";
            using var cmd = new NpgsqlCommand(stm, con);

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                myUsers.Add(new UserAccounts()
                {
                    UserId = rdr.GetInt32(rdr.GetOrdinal("UserId")),
                    FirstName = rdr.GetString(rdr.GetOrdinal("FirstName")),
                    LastName = rdr.GetString(rdr.GetOrdinal("LastName")),
                    Age = rdr.GetInt32(rdr.GetOrdinal("Age")),
                    Email = rdr.GetString(rdr.GetOrdinal("Email")),
                    Password = rdr.GetString(rdr.GetOrdinal("Password")),
                    Deleted = rdr.GetBoolean(rdr.GetOrdinal("Deleted")),
                    Address = rdr.GetString(rdr.GetOrdinal("Address")),
                    YardSize = rdr.GetDouble(rdr.GetOrdinal("YardSize")),
                    Fenced = rdr.GetBoolean(rdr.GetOrdinal("Fenced")),
                    AccountType = rdr.GetString(rdr.GetOrdinal("AccountType"))
                });
            }
            return myUsers;
        }

        public void SaveToDB(UserAccounts user)
        {
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            string stm = @"INSERT INTO ""UserAccounts"" (
                ""FirstName"", ""LastName"", ""Age"", ""Email"", ""Password"", 
                ""Deleted"", ""Address"", ""YardSize"", ""Fenced"", ""AccountType"")
            VALUES (
                @FirstName, @LastName, @Age, @Email, @Password, 
                @Deleted, @Address, @YardSize, @Fenced, @AccountType)
            RETURNING ""UserId""";

            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Age", user.Age);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Deleted", user.Deleted);
            cmd.Parameters.AddWithValue("@Address", user.Address);
            cmd.Parameters.AddWithValue("@YardSize", user.YardSize);
            cmd.Parameters.AddWithValue("@Fenced", user.Fenced);
            cmd.Parameters.AddWithValue("@AccountType", user.AccountType);

            // Execute the command and get the generated UserId
            user.UserId = (int)cmd.ExecuteScalar();
        }

        public void UpdateToDB(UserAccounts user)
        {
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            string stm = @"UPDATE ""UserAccounts"" 
                SET ""Address"" = @Address, 
                    ""YardSize"" = @YardSize, 
                    ""Fenced"" = @Fenced, 
                    ""HasForm"" = true 
                WHERE ""UserId"" = @UserId";

            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@UserId", user.UserId);
            cmd.Parameters.AddWithValue("@Address", user.Address);
            cmd.Parameters.AddWithValue("@YardSize", user.YardSize);
            cmd.Parameters.AddWithValue("@Fenced", user.Fenced);
            cmd.ExecuteNonQuery();
        }

        public UserAccounts GetUserById(string email, string password)
        {
            using var con = new NpgsqlConnection(cs.cs);
            try
            {
                con.Open();
                string stm = "SELECT * FROM \"UserAccounts\" WHERE \"Email\" = @Email AND \"Password\" = @Password";
                using var cmd = new NpgsqlCommand(stm, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                using var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    return new UserAccounts()
                    {
                        UserId = rdr.GetInt32(rdr.GetOrdinal("UserId")),
                        FirstName = rdr.GetString(rdr.GetOrdinal("FirstName")),
                        LastName = rdr.GetString(rdr.GetOrdinal("LastName")),
                        Age = rdr.GetInt32(rdr.GetOrdinal("Age")),
                        Email = rdr.GetString(rdr.GetOrdinal("Email")),
                        Password = rdr.GetString(rdr.GetOrdinal("Password")),
                        Deleted = rdr.GetBoolean(rdr.GetOrdinal("Deleted")),
                        Address = rdr.GetString(rdr.GetOrdinal("Address")),
                        YardSize = rdr.GetDouble(rdr.GetOrdinal("YardSize")),
                        Fenced = rdr.GetBoolean(rdr.GetOrdinal("Fenced")),
                        AccountType = rdr.GetString(rdr.GetOrdinal("AccountType")),
                        HasForm = rdr.GetBoolean(rdr.GetOrdinal("HasForm"))
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database access error: " + ex.Message);
            }
            return null;
        }

        public UserAccounts GetUserByIdd(int userId)
        {
            using var con = new NpgsqlConnection(cs.cs);
            try
            {
                con.Open();
                string stm = "SELECT * FROM \"UserAccounts\" WHERE \"UserId\" = @UserId";
                using var cmd = new NpgsqlCommand(stm, con);
                cmd.Parameters.AddWithValue("@UserId", userId);

                using var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    return new UserAccounts()
                    {
                        UserId = rdr.GetInt32(rdr.GetOrdinal("UserId")),
                        FirstName = rdr.GetString(rdr.GetOrdinal("FirstName")),
                        LastName = rdr.GetString(rdr.GetOrdinal("LastName")),
                        Age = rdr.GetInt32(rdr.GetOrdinal("Age")),
                        Email = rdr.GetString(rdr.GetOrdinal("Email")),
                        Password = rdr.GetString(rdr.GetOrdinal("Password")),
                        Deleted = rdr.GetBoolean(rdr.GetOrdinal("Deleted")),
                        Address = rdr.GetString(rdr.GetOrdinal("Address")),
                        YardSize = rdr.GetDouble(rdr.GetOrdinal("YardSize")),
                        Fenced = rdr.GetBoolean(rdr.GetOrdinal("Fenced")),
                        AccountType = rdr.GetString(rdr.GetOrdinal("AccountType"))
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database access error: " + ex.Message);
            }
            return null;
        }

        public void DeleteUser(int userId, bool Deleted)
        {
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            using var cmd = new NpgsqlCommand("UPDATE \"UserAccounts\" SET \"Deleted\" = @Deleted WHERE \"UserId\" = @UserId", con);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Deleted", !Deleted);
            cmd.ExecuteNonQuery();
        }
 
    }
}
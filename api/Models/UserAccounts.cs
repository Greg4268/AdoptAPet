using Npgsql;
using api.Data;
using System.ComponentModel.DataAnnotations;
namespace api.Models
{
    public class UserAccounts
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? AccountType { get; set; }
        public bool Deleted { get; set; }
        public string? Address { get; set; }
        public double YardSize { get; set; }
        public bool Fenced { get; set; }
        public bool HasForm { get; set; }

        public static List<UserAccounts> GetAllUsers()
        {
            List<UserAccounts> myUsers = new();
            GetPublicConnection cs = new();
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

        public void SaveToDB()
        {
            GetPublicConnection cs = new();
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
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Deleted", Deleted);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@YardSize", YardSize);
            cmd.Parameters.AddWithValue("@Fenced", Fenced);
            cmd.Parameters.AddWithValue("@AccountType", AccountType);

            // Execute the command and get the generated UserId
            UserId = (int)cmd.ExecuteScalar();
        }

        public void UpdateToDB(int userId)
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            string stm = @"UPDATE ""UserAccounts"" 
                SET ""Address"" = @Address, 
                    ""YardSize"" = @YardSize, 
                    ""Fenced"" = @Fenced, 
                    ""HasForm"" = true 
                WHERE ""UserId"" = @UserId";

            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@YardSize", YardSize);
            cmd.Parameters.AddWithValue("@Fenced", Fenced);
            cmd.ExecuteNonQuery();
        }

        public static UserAccounts GetUserById(string email, string password)
        {
            GetPublicConnection cs = new();
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

        public static UserAccounts GetUserByIdd(int userId)
        {
            GetPublicConnection cs = new();
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
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            using var cmd = new NpgsqlCommand("UPDATE \"UserAccounts\" SET \"Deleted\" = @Deleted WHERE \"UserId\" = @UserId", con);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Deleted", !Deleted);
            cmd.ExecuteNonQuery();
        }
    }
}
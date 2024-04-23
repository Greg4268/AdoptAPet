using MySql.Data.MySqlClient;
using api.Data;
using System.ComponentModel.DataAnnotations;
namespace api.Models
{
    public class UserAccounts
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccountType { get; set; }
        public bool deleted { get; set; }
        public string Address { get; set; }
        public double YardSize { get; set; }
        public bool Fenced { get; set; }
        public bool HasForm { get; set; }

        public static List<UserAccounts> GetAllUsers() // method to retrieve pet from database
        {
            List<UserAccounts> myUsers = new List<UserAccounts>(); // initialize array to hold pet
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open databse connection
            string stm = "SELECT * FROM User_Profile"; // sql statement to select everything from the pet table
            MySqlCommand cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            while (rdr.Read()) // iterate through table rows
            {
                myUsers.Add(new UserAccounts() // create pet object for each row
                {
                    UserId = rdr.GetInt32("UserId"),
                    FirstName = rdr.GetString("FirstName"),
                    LastName = rdr.GetString("LastName"),
                    Age = rdr.GetInt32("Age"),
                    Email = rdr.GetString("Email"),
                    Password = rdr.GetString("Password"),
                    deleted = rdr.GetBoolean("deleted"),
                    Address = rdr.GetString("Address"),
                    YardSize = rdr.GetInt32("YardSize"),
                    Fenced = rdr.GetBoolean("Fenced"),
                    AccountType = rdr.GetString("AccountType"),
                    // BirthDate = rdr.GetDateTime("BirthDate")
                });
            }
            con.Close();
            return myUsers; // return populated list
        }

        public void SaveToDB() // method to save the users to the database
        {
            System.Console.WriteLine("SaveToDB called");
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open database connection

            string stm = @"INSERT INTO User_Profile (
                UserId, FirstName, LastName, Age, Email, Password, 
                deleted, Address, YardSize, Fenced, AccountType) 
            VALUES (
                @UserId, @FirstName, @LastName, @Age, @Email, @Password, 
                @deleted, @Address, @YardSize, @Fenced, @AccountType)"; // sql command to insert a new pet

            System.Console.WriteLine("test");


            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@UserId", UserId); // add parameters to the sql command
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@deleted", deleted);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@YardSize", YardSize);
            cmd.Parameters.AddWithValue("@Fenced", Fenced);
            cmd.Parameters.AddWithValue("@AccountType", AccountType);
            // cmd.Parameters.AddWithValue("@BirthDate", BirthDate);

            cmd.ExecuteNonQuery(); // execute sql command
            con.Close();
        }

        public void UpdateToDB(int UserId) // method to update existing user profile in database
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open db connection

            string stm = "UPDATE User_Profile set Address = @Address, YardSize = @YardSize, Fenced = @Fenced, HasForm = 1 WHERE UserId = @UserId"; // sql command for updating a pet
            Console.WriteLine("SQL query: " + stm); // log the sql query to console for debugging
            Console.WriteLine("Parameters:"); // log parameters
            Console.WriteLine("@UserId: " + UserId);
            Console.WriteLine("@Address: " + Address);
            Console.WriteLine("@YardSize: " + YardSize);
            Console.WriteLine("@Fenced: " + Fenced);

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@YardSize", YardSize);
            cmd.Parameters.AddWithValue("@Fenced", Fenced);
            cmd.Parameters.AddWithValue("@HasForm", HasForm);
            cmd.ExecuteNonQuery(); // execute sql command
            con.Close();

            Console.WriteLine($"Updating database for UserId: {UserId}");
        }

        public static UserAccounts GetUserById(string Email, string Password)
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            try
            {
                con.Open(); // open connection to db
                string stm = "SELECT * FROM User_Profile WHERE Email = @Email AND Password = @Password";
                MySqlCommand cmd = new MySqlCommand(stm, con);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);
                using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
                if (rdr.Read())
                {
                    return new UserAccounts()
                    {
                        UserId = rdr.GetInt32("UserId"),
                        FirstName = rdr.GetString("FirstName"),
                        LastName = rdr.GetString("LastName"),
                        Age = rdr.GetInt32("Age"),
                        Email = rdr.GetString("Email"),
                        Password = rdr.GetString("Password"),
                        deleted = rdr.GetBoolean("deleted"),
                        Address = rdr.GetString("Address"),
                        YardSize = rdr.GetInt32("YardSize"),
                        Fenced = rdr.GetBoolean("Fenced"),
                        AccountType = rdr.GetString("AccountType"),
                        HasForm = rdr.GetBoolean("HasForm"),
                        // BirthDate = rdr.GetDateTime("BirthDate")
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database access error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return null; // if no user is found
        }

        public static UserAccounts GetUserByIdd(int UserId)
        {
            System.Console.WriteLine("GetUserByIdd called");
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            try
            {
                con.Open(); // open connection to db
                string stm = "SELECT * FROM User_Profile WHERE UserId = @UserId";
                MySqlCommand cmd = new MySqlCommand(stm, con);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
                if (rdr.Read())
                {
                    return new UserAccounts()
                    {
                        UserId = rdr.GetInt32("UserId"),
                        FirstName = rdr.GetString("FirstName"),
                        LastName = rdr.GetString("LastName"),
                        Age = rdr.GetInt32("Age"),
                        Email = rdr.GetString("Email"),
                        Password = rdr.GetString("Password"),
                        deleted = rdr.GetBoolean("deleted"),
                        Address = rdr.GetString("Address"),
                        YardSize = rdr.GetInt32("YardSize"),
                        Fenced = rdr.GetBoolean("Fenced"),
                        AccountType = rdr.GetString("AccountType"),
                        // BirthDate = rdr.GetDateTime("BirthDate")
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database access error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return null; // if no user is found


        }

        public void DeleteUser(int UserId, bool deleted)
        {
            System.Console.WriteLine("Deleted = " + deleted);
            deleted = !deleted;
            System.Console.WriteLine("New Deleted = " + deleted);
            try
            {
                GetPublicConnection cs = new GetPublicConnection();
                using (var con = new MySqlConnection(cs.cs))
                {
                    Console.WriteLine("Opening connection...");
                    con.Open();
                    using (var cmd = new MySqlCommand("UPDATE User_Profile SET deleted = @deleted WHERE UserId = @UserId", con))
                    {
                        Console.WriteLine($"Updating UserId: {UserId} to Deleted: {deleted}");
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@deleted", deleted);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DeleteUser: " + ex.Message);
                throw; // Rethrow to preserve stack details
            }
        }

    }
}
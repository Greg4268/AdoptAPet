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
                @deleted, @Address, @YardSize, @Fenced, @AccountType)";

            System.Console.WriteLine("test");


            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@UserId", UserId);
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

        public void UpdateToDB() // method to update existing pet in database
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open db connection

            string stm = "UPDATE User_Profile set UserId = @UserId, FirstName = @FirstName, LastName = @LastName, Age = @Age, Email = @Email, Password = @Password, PrimaryPhone = @PrimaryPhone, IsAdmin = @IsAdmin, deleted = @deleted, Address = @Address, YardSize = @YardSize, Fenced = @Fenced, BirthDate = @BirthDate WHERE UserID = @UserId"; // sql command for updating a pet
            Console.WriteLine("SQL query: " + stm); // log the sql query to console for debugging
            Console.WriteLine("Parameters:"); // log parameters
            Console.WriteLine("@UserId: " + UserId);
            Console.WriteLine("@FirstName: " + FirstName);
            Console.WriteLine("@LastName: " + LastName);
            Console.WriteLine("@Age: " + Age);
            Console.WriteLine("@Email: " + Email);
            Console.WriteLine("@Password: " + Password);
            Console.WriteLine("@deleted: " + deleted);
            Console.WriteLine("@Address: " + Address);
            Console.WriteLine("@YardSize: " + YardSize);
            Console.WriteLine("@Fenced: " + Fenced);
            Console.WriteLine("@AccountType: " + AccountType);
            // Console.WriteLine("@BirthDate: " + BirthDate);

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@UserId", UserId); // add parameters to sql command
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


        public void DeleteUser(UserAccounts value)
        {
            GetPublicConnection cs = new GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE User_Profile SET deleted = @deleted WHERE UserId = @UserId";
            cmd.Parameters.AddWithValue("@UserId", value.UserId);
            cmd.Parameters.AddWithValue("@deleted", value.deleted);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
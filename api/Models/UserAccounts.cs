using MySql.Data.MySqlClient;
using api.Data;
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
        public string PrimaryPhone { get; set; }
        public bool IsAdmin { get; set; }
        public bool deleted { get; set; }
        public string Address {get; set;}
        public int YardSize {get;set;}
        public bool Fenced {get; set;}
        public DateTime BirthDate {get;set;}

        public static List<UserAccounts> GetAllUsers() // method to retrieve pet from database
        {
            List<UserAccounts> myUsers = new List<UserAccounts>(); // initialize array to hold pet
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open databse connection
            string stm = "Select * from User_Profile"; // sql statement to select everything from the pet table
            MySqlCommand cmd = new MySqlCommand(stm, con); 

            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            while(rdr.Read()) // iterate through table rows
            {
                myUsers.Add(new UserAccounts() // create pet object for each row
                {
                    UserId = rdr.GetInt32("UserId"),
                    FirstName = rdr.GetString("FirstName"),
                    LastName = rdr.GetString("LastName"),
                    Age = rdr.GetInt32("Age"),
                    Email = rdr.GetString("Email"),
                    Password = rdr.GetString("Password"),
                    PrimaryPhone = rdr.GetString("PrimaryPhone"),
                    IsAdmin = rdr.GetInt32("IsAdmin") == 1, // convert tinyint to boolean, if 1 then it is true
                    deleted = rdr.GetInt32("deleted") == 1,
                    Address = rdr.GetString("Address"),
                    YardSize = rdr.GetInt32("YardSize"),
                    Fenced = rdr.GetInt32("Fenced") == 1,
                    BirthDate = rdr.GetDateTime("BirthDate")
                });
            }
            return myUsers; // return populated list
        }

        public void SaveToDB() // method to save the pets to the database
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open database connection
            string stm = "INSERT INTO User_Profile (UserId, FirstName, LastName, Age, Email, Password, PrimaryPhone, IsAdmin, deleted, Address, YardSize, Fenced, BirthDate) VALUES (@UserId, @FirstName, @LastName, @Age, @Email, @Password, @PrimaryPhone, @IsAdmin, @deleted, @Address, @YardSize, @Fenced, @BirthDate)"; // sql command to insert a new pet
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@UserId", UserId); // add parameters to the sql command
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@PrimaryPhone", PrimaryPhone);
            cmd.Parameters.AddWithValue("@IsAdmin", IsAdmin? 1 : 0);
            cmd.Parameters.AddWithValue("@deleted", deleted? 1 : 0);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@YardSize", YardSize);
            cmd.Parameters.AddWithValue("@Fenced", Fenced);
            cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
            cmd.ExecuteNonQuery(); // execute sql command
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
            Console.WriteLine("@PrimaryPhone: " + PrimaryPhone);
            Console.WriteLine("@IsAdmin: " + (IsAdmin ? 1 : 0));
            Console.WriteLine("@deleted: " + (deleted ? 1 : 0));
            Console.WriteLine("@Address: " + Address);
            Console.WriteLine("@YardSize: " + YardSize);
            Console.WriteLine("@Fenced: " + (Fenced ? 1 : 0));
            Console.WriteLine("@BirthDate: " + BirthDate);
    
            using var cmd = new MySqlCommand(stm, con); 
            cmd.Parameters.AddWithValue("@UserId", UserId); // add parameters to sql command
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@PrimaryPhone", PrimaryPhone);
            cmd.Parameters.AddWithValue("@IsAdmin", IsAdmin ? 1 : 0);
            cmd.Parameters.AddWithValue("@deleted", deleted ? 1 : 0);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@YardSize", YardSize);
            cmd.Parameters.AddWithValue("@Fenced", Fenced ? 1 : 0);
            cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
            cmd.ExecuteNonQuery(); // execute sql command
        }


        public static UserAccounts GetUserById(int UserId) // method to retrieve specific pet
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open connection to db
            string stm = "select * from User_Profile where UserId = @UserId"; // sql statement to retrieve specific pet
            MySqlCommand cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@UserId", UserId); // add PetProfileID as parameter
            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            if(rdr.Read()) // check if pet is found
            {
                return new UserAccounts() // construct and initialize new pet object
                {
                    UserId = rdr.GetInt32("UserId"),
                    FirstName = rdr.GetString("FirstName"),
                    LastName = rdr.GetString("LastName"),
                    Age = rdr.GetInt32("Age"),
                    Email = rdr.GetString("Email"),
                    Password = rdr.GetString("Password"),
                    PrimaryPhone = rdr.GetString("PrimaryPhone"),
                    IsAdmin = rdr.GetInt32("IsAdmin") == 1, // convert tinyint to boolean, if 1 then it is true
                    deleted = rdr.GetInt32("deleted") == 1,
                    Address = rdr.GetString("Address"),
                    YardSize = rdr.GetInt32("YardSize"),
                    Fenced = rdr.GetInt32("Fenced") == 1,
                    BirthDate = rdr.GetDateTime("BirthDate")
                };
            }
            return null; // if no pet is found
        }
    }
}
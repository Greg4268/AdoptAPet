using MySql.Data.MySqlClient;
using api.Data;
namespace api.Models
{
    public class Shelters
    {
        public int ShelterId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string HoursOfOperation { get; set; }

        public static List<Shelters> GetAllShelters() // method to retrieve shelter from database
        {
            List<Shelters> myShelters = new List<Shelters>(); // initialize array to hold shelter
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open databse connection
            string stm = "Select * from Shelter"; // sql statement to select everything from the shelter table
            MySqlCommand cmd = new MySqlCommand(stm, con); 

            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            while(rdr.Read()) // iterate through table rows
            {
                myShelters.Add(new Shelters() // create shelter object for each row
                {
                    ShelterId = rdr.GetInt32("ShelterId"),
                    Username = rdr.GetString("Username"),
                    Password = rdr.GetString("Password"),
                    Address = rdr.GetString("Address"),
                    HoursOfOperation = rdr.GetString("HoursOfOperation")
                });
            }
            return myShelters; // return populated list
        }

        public void SaveToDB() // method to save the shelters to the database
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open database connection
            string stm = "INSERT INTO Shelter (ShelterId, Username, Password, Address, HoursOfOperation) VALUES (@ShelterId, @Username, @Password, @Address, @HoursOfOperation)"; // sql command to insert a new shelter
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@ShelterId", ShelterId); // add parameters to the sql command
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@HoursOfOperation", HoursOfOperation);
            cmd.ExecuteNonQuery(); // execute sql command
        }

        public void UpdateToDB() // method to update existing shelter in database
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open db connection
    
            string stm = "UPDATE Shelter set ShelterId = @ShelterId, Username = @Username, Password = @Password, Address = @Address, HoursOfOperation = @HoursOfOperation WHERE ShelterId = @ShelterId"; // sql command for updating a shelter
            Console.WriteLine("SQL query: " + stm); // log the sql query to console for debugging
            Console.WriteLine("Parameters:"); // log parameters
            Console.WriteLine("@ShelterId: " + ShelterId);
            Console.WriteLine("@Username: " + Username);
            Console.WriteLine("@Password: " + Password);
            Console.WriteLine("@Address: " + Address);
            Console.WriteLine("@HoursOfOperation: " + HoursOfOperation);
    
            using var cmd = new MySqlCommand(stm, con); 
            cmd.Parameters.AddWithValue("@ShelterId", ShelterId); // add parameters to sql command
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@HoursOfOperation", HoursOfOperation);
            cmd.ExecuteNonQuery(); // execute sql command
        }


        public static Shelters GetpetById(int ShelterId) // method to retrieve specific shelter
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open connection to db
            string stm = "select * from Shelter where ShelterId = @ShelterId"; // sql statement to retrieve specific shelter
            MySqlCommand cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@ShelterId", ShelterId); // add ShelterID as parameter
            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            if(rdr.Read()) // check if shelter is found
            {
                return new Shelters() // construct and initialize new shelter object
                {
                    ShelterId = rdr.GetInt32("ShelterId"),
                    Username = rdr.GetString("Username"),
                    Password = rdr.GetString("Password"),
                    Address = rdr.GetString("Address"),
                    HoursOfOperation = rdr.GetString("HoursOfOperation")
                };
            }
            return null; // if no shelter is found
        }
    }
}
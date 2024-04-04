using MySql.Data.MySqlClient;
using api.Data;
namespace api.Models
{
    public class Pets
    {
        public int PetProfileID { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
        public bool Availability { get; set; }
        public string Species { get; set; }
        public bool CanVisit { get; set; }
        public int ReturnedCount { get; set; }
        public int FavoriteCount { get; set; }

        public static List<Pets> GetAllPets() // method to retrieve pet from database
        {
            List<Pets> myPets = new List<Pets>(); // initialize array to hold pet
            Database database = new Database(); // create new instance of database
            using var con = database.GetPublicConnection();
            con.Open(); // open databse connection
            string stm = "Select * from Pet_Profile"; // sql statement to select everything from the pet table
            MySqlCommand cmd = new MySqlCommand(stm, con); 

            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            while(rdr.Read()) // iterate through table rows
            {
                myPets.Add(new Pets() // create pet object for each row
                {
                    PetProfileID = rdr.GetInt32("PetProfileId"),
                    Age = rdr.GetInt32("Age"),
                    Breed = rdr.GetString("Breed"),
                    Name = rdr.GetString("Name"),
                    Availability = rdr.GetInt32("Availability") == 1, // convert tinyint to boolean, if 1 then it is true
                    Species = rdr.GetString("Species"),
                    CanVisit = rdr.GetInt32("CanVisit") == 1,
                    ReturnedCount = rdr.GetInt32("ReturnedCount"),
                    FavoriteCount = rdr.GetInt32("FavoriteCount")
                });
            }
            return myPets; // return populated list
        }

        public void SaveToDB() // method to save the pets to the database
        {
            Database database = new Database(); // create a new instance of database
            using var con = database.GetPublicConnection(); 
            con.Open(); // open database connection
            string stm = "INSERT INTO Pet_Profile (PetProfileId, Age, Breed, Name, Availability, Species, CanVisit, ReturnedCount, FavoriteCount) VALUES (@PetProfileId, @Age, @Breed, @Name, @Availability, @Species, @CanVisit, @ReturnedCount, @FavoriteCount)"; // sql command to insert a new pet
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileID); // add parameters to the sql command
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@Breed", Breed);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Availability", Availability ? 1 : 0);
            cmd.Parameters.AddWithValue("@Species", Species);
            cmd.Parameters.AddWithValue("@CanVisit", CanVisit? 1 : 0);
            cmd.Parameters.AddWithValue("@ReturnedCount", ReturnedCount);
            cmd.Parameters.AddWithValue("@FavoriteCount", FavoriteCount);
            cmd.ExecuteNonQuery(); // execute sql command
        }

        public void UpdateToDB() // method to update existing pet in database
        {
            Database database = new Database(); // create new instance database
            using var con = database.GetPublicConnection();
            con.Open(); // open db connection
    
            string stm = "UPDATE Pet_Profile set PetProfileID = @PetProfileId, Age = @Age, Breed = @Breed, Name = @Name, Availability = @Availability WHERE PetProfileID = @PetProfileId"; // sql command for updating a pet
            Console.WriteLine("SQL query: " + stm); // log the sql query to console for debugging
            Console.WriteLine("Parameters:"); // log parameters
            Console.WriteLine("@PetProfileId: " + PetProfileID);
            Console.WriteLine("@Age: " + Age);
            Console.WriteLine("@Breed: " + Breed);
            Console.WriteLine("@Name: " + Name);
            Console.WriteLine("@Availability: " + (Availability ? 1 : 0));
    
            using var cmd = new MySqlCommand(stm, con); 
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileID); // add parameters to sql command
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@Breed", Breed);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Availability", Availability ? 1 : 0);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileID);
            cmd.ExecuteNonQuery(); // execute sql command
        }


        public static Pets GetpetById(int PetProfileID) // method to retrieve specific pet
        {
            Database database = new Database(); // create new instance of db
            using var con = database.GetPublicConnection();
            con.Open(); // open connection to db
            string stm = "select * from Pet_Profile where PetProfileID = @PetProfileId"; // sql statement to retrieve specific pet
            MySqlCommand cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileID); // add PetProfileID as parameter
            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            if(rdr.Read()) // check if pet is found
            {
                return new Pets() // construct and initialize new pet object
                {
                    PetProfileID = rdr.GetInt32("PetProfileId"),
                    Age = rdr.GetInt32("Age"),
                    Breed = rdr.GetString("Breed"),
                    Name = rdr.GetString("Name"),
                    Availability = rdr.GetInt32("Availability") == 1, // convert tinyint to boolean, if 1 then it is true
                    Species = rdr.GetString("Species"),
                    CanVisit = rdr.GetInt32("CanVisit") == 1,
                    ReturnedCount = rdr.GetInt32("ReturnedCount"),
                    FavoriteCount = rdr.GetInt32("FavoriteCount")
                };
            }
            return null; // if no pet is found
        }
    }
}
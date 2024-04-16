using MySql.Data.MySqlClient;
using api.Data;
namespace api.Models
{
    public class Pets
    {
        public int PetProfileId { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; } 
        public string Breed { get; set; }
        public string Name { get; set; }
        public bool Availability { get; set; }
        public string Species { get; set; }
        public bool CanVisit { get; set; }
        public int ReturnedCount { get; set; }
        public int FavoriteCount { get; set; }
        public bool favorited { get; set; }
        public bool deleted { get; set; }
        public int ShelterId { get; set; }

        public static List<Pets> GetAllPets() // method to retrieve pet from database
        {
            List<Pets> myPets = new List<Pets>(); // initialize array to hold pet
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open databse connection
            string stm = "SELECT * FROM Pet_Profile"; // sql statement to select everything from the pet table
            MySqlCommand cmd = new MySqlCommand(stm, con); 

            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            while(rdr.Read()) // iterate through table rows
            {
                myPets.Add(new Pets() // create pet object for each row
                {
                    PetProfileId = rdr.GetInt32("PetProfileId"),
                    Breed = rdr.GetString("Breed"),
                    Name = rdr.GetString("Name"),
                    BirthDate = rdr.GetDateTime("BirthDate"),
                    Availability = rdr.GetInt32("Availability") == 1, // convert tinyint to boolean, if 1 then it is true
                    Species = rdr.GetString("Species"),
                    CanVisit = rdr.GetInt32("CanVisit") == 1,
                    ReturnedCount = rdr.GetInt32("ReturnedCount"),
                    FavoriteCount = rdr.GetInt32("FavoriteCount"),
                    ShelterId = rdr.GetInt32("ShelterId"),
                    favorited = rdr.GetBoolean("deleted"),
                    deleted = rdr.GetBoolean("favorited"),
                    Age = rdr.GetInt32("Age"),
                });
            }
            con.Close();
            return myPets; // return populated list
        }

        public void SaveToDB() // method to save the pets to the database
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open database connection
            string stm = "INSERT INTO Pet_Profile (PetProfileId, Age, Breed, Name, Availability, Species, CanVisit, ReturnedCount, FavoriteCount) VALUES (@PetProfileId, @Age, @Breed, @Name, @Availability, @Species, @CanVisit, @ReturnedCount, @FavoriteCount)"; // sql command to insert a new pet
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileId); // add parameters to the sql command
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@Breed", Breed);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Availability", Availability ? 1 : 0);
            cmd.Parameters.AddWithValue("@Species", Species);
            cmd.Parameters.AddWithValue("@CanVisit", CanVisit? 1 : 0);
            cmd.Parameters.AddWithValue("@ReturnedCount", ReturnedCount);
            cmd.Parameters.AddWithValue("@FavoriteCount", FavoriteCount);
            cmd.ExecuteNonQuery(); // execute sql command
            con.Close();
        }

        public void UpdateToDB() // method to update existing pet in database
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open db connection
    
            string stm = "UPDATE Pet_Profile set PetProfileId = @PetProfileId, Age = @Age, Breed = @Breed, Name = @Name, Availability = @Availability WHERE PetProfileID = @PetProfileId"; // sql command for updating a pet
            Console.WriteLine("SQL query: " + stm); // log the sql query to console for debugging
            Console.WriteLine("Parameters:"); // log parameters
            Console.WriteLine("@PetProfileId: " + PetProfileId);
            Console.WriteLine("@Age: " + Age);
            Console.WriteLine("@Breed: " + Breed);
            Console.WriteLine("@Name: " + Name);
            Console.WriteLine("@Availability: " + (Availability ? 1 : 0));
    
            using var cmd = new MySqlCommand(stm, con); 
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileId); // add parameters to sql command
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@Breed", Breed);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Availability", Availability ? 1 : 0);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileId);
            cmd.ExecuteNonQuery(); // execute sql command
            con.Close();
        }

        public void DeletePet(Pets value) {
            GetPublicConnection cs = new GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Pet_Profile SET deleted = @deleted WHERE PetProfileId = @PetProfileId";
            cmd.Parameters.AddWithValue("@PetProfileId", value.PetProfileId);
            cmd.Parameters.AddWithValue("@deleted", value.deleted);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void FavoritePet(Pets value) {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open db connection

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Pet_Profile SET favorited = @favorited WHERE PetProfileId = @PetProfileId";
            cmd.Parameters.AddWithValue("@PetProfileId", value.PetProfileId);
            cmd.Parameters.AddWithValue("@favorited", value.favorited);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public static Pets GetPetById(int PetProfileId) // method to retrieve specific pet
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open connection to db
            string stm = "select * from Pet_Profile where PetProfileId = @PetProfileId"; // sql statement to retrieve specific pet
            MySqlCommand cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileId); // add PetProfileID as parameter
            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            if(rdr.Read()) // check if pet is found
            {
                return new Pets() // construct and initialize new pet object
                {
                    PetProfileId = rdr.GetInt32("PetProfileId"),
                    Breed = rdr.GetString("Breed"),
                    Name = rdr.GetString("Name"),
                    BirthDate = rdr.GetDateTime("BirthDate"),
                    Availability = rdr.GetInt32("Availability") == 1, // convert tinyint to boolean, if 1 then it is true
                    Species = rdr.GetString("Species"),
                    CanVisit = rdr.GetInt32("CanVisit") == 1,
                    ReturnedCount = rdr.GetInt32("ReturnedCount"),
                    FavoriteCount = rdr.GetInt32("FavoriteCount"),
                    ShelterId = rdr.GetInt32("ShelterId"),
                    favorited = rdr.GetBoolean("deleted"),
                    deleted = rdr.GetBoolean("favorited"),
                    Age = rdr.GetInt32("Age"),
                };
            }
            con.Close();
            return null; // if no pet is found
        }
    }
}
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
        public string Species { get; set; }
        public bool CanVisit { get; set; }     
        public bool deleted { get; set; }
        public int ShelterId { get; set; }
        public string ImageUrl { get; set; }
        public int FavoriteCount { get; set;}

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
                    Age = rdr.GetInt32("Age"),
                    BirthDate = rdr.GetDateTime("BirthDate"),
                    Breed = rdr.GetString("Breed"),
                    Name = rdr.GetString("Name"),
                    Species = rdr.GetString("Species"),
                    CanVisit = rdr.GetBoolean("CanVisit"),
                    deleted = rdr.GetBoolean("deleted"),
                    ShelterId = rdr.GetInt32("ShelterId"),
                    ImageUrl = rdr.GetString("ImageUrl"),
                    FavoriteCount = rdr.GetInt32("FavoriteCount"),
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
            string stm = "INSERT INTO Pet_Profile (PetProfileId, Age, BirthDate, Breed, Name, Species, CanVisit, deleted, ShelterId, Image, FavoriteCount) VALUES (@PetProfileId, @Age, @BirthDate, @Breed, @Name, @Species, @CanVisit, @deleted, @ShelterId, @ImageUrl, @FavoriteCount)"; // sql command to insert a new pet
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileId); // add parameters to the sql command
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
            cmd.Parameters.AddWithValue("@Breed", Breed);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Species", Species);
            cmd.Parameters.AddWithValue("@CanVisit", CanVisit);
            cmd.Parameters.AddWithValue("@deleted", deleted);
            cmd.Parameters.AddWithValue("@ShelterId", ShelterId);
            cmd.Parameters.AddWithValue("@ImageUrl", ImageUrl);
            cmd.Parameters.AddWithValue("@FavoriteCount", FavoriteCount);
            cmd.Prepare();
            cmd.ExecuteNonQuery(); // execute sql command
            con.Close();
        }

        public void UpdateToDB() // method to update existing pet in database
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open db connection
    
            string stm = "@UPDATE Pet_Profile set PetProfileId = @PetProfileId, Age = @Age, BirthDate = @BirthDate, Breed = @Breed, Name = @Name, Species = @Species, CanVisit = @CanVisit, deleted = @deleted, ShelterId = @ShelterId, Image = @Image, FavoriteCount = @FavoriteCount WHERE PetProfileId = @PetProfileId"; // sql command for updating a pet
    
            using var cmd = new MySqlCommand(stm, con); 
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileId); // add parameters to the sql command
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
            cmd.Parameters.AddWithValue("@Breed", Breed);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Species", Species);
            cmd.Parameters.AddWithValue("@CanVisit", CanVisit);
            cmd.Parameters.AddWithValue("@deleted", deleted);
            cmd.Parameters.AddWithValue("@ShelterId", ShelterId);
            cmd.Parameters.AddWithValue("@ImageUrl", ImageUrl);
            cmd.Parameters.AddWithValue("@FavoriteCount", FavoriteCount);
            cmd.Prepare();
            cmd.ExecuteNonQuery(); // execute sql command
            con.Close();
        }

        public void FavoritePet(Pets value) { // fix to add to FavoritePets table
            GetPublicConnection cs = new GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open db connection

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Pet_Profile SET favorited = @favorited WHERE PetProfileId = @PetProfileId";
            cmd.Parameters.AddWithValue("@PetProfileId", value.PetProfileId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void DeletePet(int petId) {
            GetPublicConnection cs = new GetPublicConnection();
            using (var con = new MySqlConnection(cs.cs)) {
                con.Open();

                using (var cmd = new MySqlCommand("UPDATE Pet_Profile SET deleted = 1 WHERE PetProfileId = @PetProfileId", con)) 
                {
                    cmd.Parameters.AddWithValue("@PetProfileId", petId);
                    cmd.ExecuteNonQuery(); 
                }
                con.Close(); 
            }
        }

        public static Pets GetPetById(int PetProfileId) // method to retrieve specific pet
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open connection to db
            string stm = "SELECT * FROM Pet_Profile WHERE PetProfileId = @PetProfileId"; // sql statement to retrieve specific pet
            MySqlCommand cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileId); // add PetProfileID as parameter
            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            if(rdr.Read()) // check if pet is found
            {
                return new Pets() // construct and initialize new pet object
                {
                    PetProfileId = rdr.GetInt32("PetProfileId"),
                    Age = rdr.GetInt32("Age"),
                    BirthDate = rdr.GetDateTime("BirthDate"),
                    Breed = rdr.GetString("Breed"),
                    Name = rdr.GetString("Name"),
                    Species = rdr.GetString("Species"),
                    CanVisit = rdr.GetBoolean("CanVisit"),
                    deleted = rdr.GetBoolean("deleted"),
                    ShelterId = rdr.GetInt32("ShelterId"),
                    ImageUrl = rdr.GetString("ImageUrl"),
                    FavoriteCount = rdr.GetInt32("FavoriteCount"),
                };
            }
            con.Close();
            return null; // if no pet is found
        }
    }
}
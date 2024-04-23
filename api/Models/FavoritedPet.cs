using MySql.Data.MySqlClient;
using api.Data;
namespace api.Models
{
    public class FavoritedPet
    {
        public int UserId {get; set;}
        public int PetProfileId {get; set;}
        public bool Favorited {get; set;}
        
        public static List<Pets> GetFavoritePets(int user) // needs user id
        {
            List<int> favoritePets = new List<int>(); // Temp array to store PetProfileIds
            List<Pets> myPets = new List<Pets>(); // Initialize array to hold Pets 
            GetPublicConnection cs = new GetPublicConnection(); // Create new instance of database connection string
            using (var con = new MySqlConnection(cs.cs))
            {
                con.Open(); // Open database connection
                string stm = "SELECT PetProfileId FROM FavoritePets WHERE UserId = @user"; // SQL statement to select PetProfileIds
                using (var cmd = new MySqlCommand(stm, con))
                {
                    cmd.Parameters.AddWithValue("@user", user); // add user id to the command
                    using (var rdr = cmd.ExecuteReader()) // Execute command
                    {
                        while (rdr.Read()) // Iterate through table
                        {
                            favoritePets.Add(rdr.GetInt32("PetProfileId")); // Add each PetProfileId to the list
                        }
                    }
                }
                con.Close();
            }
            // Fetch each pet's details from the Pets table
            using (var con = new MySqlConnection(cs.cs))
            {
                con.Open(); // Open database connection again
                foreach (var petProfileId in favoritePets)
                {
                    string query = "SELECT * FROM Pet_Profile WHERE PetProfileId = @petProfileId";
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@petProfileId", petProfileId);
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while(rdr.Read()) 
                            {
                                myPets.Add(new Pets
                                {
                                    PetProfileId = rdr.GetInt32("PetProfileId"),
                                    Breed = rdr.GetString("Breed"),
                                    Name = rdr.GetString("Name"),
                                    Species = rdr.GetString("Species"),
                                    FavoriteCount = rdr.GetInt32("FavoriteCount"),
                                    ShelterId = rdr.GetInt32("ShelterId"),
                                    BirthDate = rdr.GetDateTime("BirthDate"),
                                    deleted = rdr.GetBoolean("deleted"),
                                    Age = rdr.GetInt32("Age"),
                                    ImageUrl = rdr.GetString("ImageUrl")
                                });
                            }
                        }
                    }
                }
                con.Close(); // Close the database connection
            }
            return myPets; // Return the list of Pets 
        }


        public static void FavoritePet(int user, int pet)
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database connection string
            using (var con = new MySqlConnection(cs.cs))
            {
                con.Open(); // open db connection
                using (var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM FavoritePets WHERE UserId = @user AND PetProfileId = @pet", con))
                {
                    checkCmd.Parameters.AddWithValue("@user", user);
                    checkCmd.Parameters.AddWithValue("@pet", pet);
                    int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (exists > 0)
                    {
                        UpdateUnfavorite(user, pet);
                        return;
                    }
                }
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO FavoritePets (UserId, PetProfileId, favorited) VALUES (@user, @pet, 1)";
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@pet", pet);
                    cmd.ExecuteNonQuery(); 
                }
                using (var updateCmd = new MySqlCommand())
                {
                    updateCmd.Connection = con;
                    updateCmd.CommandText = "UPDATE Pet_Profile SET FavoriteCount = FavoriteCount + 1 WHERE PetProfileId = @pet";
                    updateCmd.Parameters.AddWithValue("@pet", pet);
                    updateCmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateUnfavorite(int user, int pet)
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database connection string
            using (var con = new MySqlConnection(cs.cs))
            {
                con.Open(); // open db connection
                using (var transaction = con.BeginTransaction()) // Start a transaction
                {
                    // Delete the favorite entry
                    using (var deleteCmd = new MySqlCommand("DELETE FROM FavoritePets WHERE UserId = @user AND PetProfileId = @pet", con, transaction))
                    {
                        deleteCmd.Parameters.AddWithValue("@user", user);
                        deleteCmd.Parameters.AddWithValue("@pet", pet);
                        deleteCmd.ExecuteNonQuery();
                    }
                    // Decrease favorite count in Pet_Profile
                    using (var updateCmd = new MySqlCommand("UPDATE Pet_Profile SET FavoriteCount = FavoriteCount - 1 WHERE PetProfileId = @pet AND FavoriteCount > 0", con, transaction))
                    {
                        updateCmd.Parameters.AddWithValue("@pet", pet);
                        updateCmd.ExecuteNonQuery();
                    }
                    transaction.Commit(); // Commit transaction
                }
            }
        }
    }
}
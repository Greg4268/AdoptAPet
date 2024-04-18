using MySql.Data.MySqlClient;
using api.Data;
namespace api.Models
{
    public class FavoritedPet
    {
        public int UserId {get; set;}
        public int PetProfileId {get; set;}
        public bool Favorited {get; set;}
        
        public static List<FavoritedPet> GetFavoritePets(int user) // needs user id
        {
            List <FavoritedPet> favoritePets = new List<FavoritedPet>();
            List <Pets> myPets = new List<Pets>(); // initialize array
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open databse connection
            string stm = "SELECT * FROM FavoritePets WHERE UserId = @user"; // sql statement to select 
            MySqlCommand cmd = new MySqlCommand(stm, con); 

            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            while(rdr.Read()) // iterate through table rows
            {
                favoritePets.Add(new FavoritedPet()
                {
                    UserId = rdr.GetInt32("UserId"),
                    PetProfileId = rdr.GetInt32("PetProfileId"),
                    Favorited = rdr.GetBoolean("Favorited")
                });
            }
            con.Close();
            return favoritePets;
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

                // Start a transaction
                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        // Delete the favorite entry
                        using (var deleteCmd = new MySqlCommand())
                        {
                            deleteCmd.Connection = con;
                            deleteCmd.Transaction = transaction; // Assign transaction to command
                            deleteCmd.CommandText = "DELETE FROM FavoritePets WHERE UserId = @user AND PetProfileId = @pet";
                            deleteCmd.Parameters.AddWithValue("@user", user);
                            deleteCmd.Parameters.AddWithValue("@pet", pet);
                            int result = deleteCmd.ExecuteNonQuery();
                            if (result == 0)
                            {
                                throw new Exception("No favorite found to unfavorite.");
                            }
                        }

                        // Decrease favorite count in Pet_Profile
                        using (var updateCmd = new MySqlCommand())
                        {
                            updateCmd.Connection = con;
                            updateCmd.Transaction = transaction; // Assign transaction to command
                            updateCmd.CommandText = "UPDATE Pet_Profile SET FavoriteCount = FavoriteCount - 1 WHERE PetProfileId = @pet AND FavoriteCount > 0";
                            updateCmd.Parameters.AddWithValue("@pet", pet);
                            updateCmd.ExecuteNonQuery();
                        }

                        // Commit transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }
        }

    }
}
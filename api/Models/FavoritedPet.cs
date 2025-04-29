using Npgsql;
using api.Data;
namespace api.Models
{
    public class FavoritedPet
    {
        public int UserId { get; set; }
        public int PetProfileId { get; set; }
        public bool Favorited { get; set; }

        public static List<Pets> GetFavoritePets(int user)
        {
            List<int> favoritePets = new();
            List<Pets> myPets = new();
            GetPublicConnection cs = new();

            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            // Get favorite pet IDs
            string stm = "SELECT PetProfileId FROM FavoritePet WHERE UserId = @user";
            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@user", user);

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                favoritePets.Add(rdr.GetInt32(rdr.GetOrdinal("PetProfileId")));
            }

            // Fetch pet details for each favorite
            foreach (var petProfileId in favoritePets)
            {
                string query = "SELECT * FROM Pet_Profile WHERE PetProfileId = @petProfileId";
                using var petCmd = new NpgsqlCommand(query, con);
                petCmd.Parameters.AddWithValue("@petProfileId", petProfileId);

                using var petRdr = petCmd.ExecuteReader();
                while (petRdr.Read())
                {
                    myPets.Add(new Pets
                    {
                        PetProfileId = petRdr.GetInt32(petRdr.GetOrdinal("PetProfileId")),
                        Breed = petRdr.GetString(petRdr.GetOrdinal("Breed")),
                        Name = petRdr.GetString(petRdr.GetOrdinal("Name")),
                        Species = petRdr.GetString(petRdr.GetOrdinal("Species")),
                        FavoriteCount = petRdr.GetInt32(petRdr.GetOrdinal("FavoriteCount")),
                        ShelterId = petRdr.GetInt32(petRdr.GetOrdinal("ShelterId")),
                        BirthDate = petRdr.GetDateTime(petRdr.GetOrdinal("BirthDate")),
                        Deleted = petRdr.GetBoolean(petRdr.GetOrdinal("deleted")),
                        Age = petRdr.GetInt32(petRdr.GetOrdinal("Age")),
                        ImageUrl = petRdr.GetString(petRdr.GetOrdinal("ImageUrl"))
                    });
                }
            }

            return myPets;
        }

        public static void FavoritePet(int user, int pet)
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            using var transaction = con.BeginTransaction();
            try
            {
                // Check if favorite exists
                using var checkCmd = new NpgsqlCommand(
                    "SELECT COUNT(*) FROM FavoritePet WHERE UserId = @user AND PetProfileId = @pet",
                    con,
                    transaction);
                checkCmd.Parameters.AddWithValue("@user", user);
                checkCmd.Parameters.AddWithValue("@pet", pet);

                int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                if (exists > 0)
                {
                    UpdateUnfavorite(user, pet);
                    return;
                }

                // Insert new favorite
                using var insertCmd = new NpgsqlCommand(
                    "INSERT INTO FavoritePet (UserId, PetProfileId, favorited) VALUES (@user, @pet, true)",
                    con,
                    transaction);
                insertCmd.Parameters.AddWithValue("@user", user);
                insertCmd.Parameters.AddWithValue("@pet", pet);
                insertCmd.ExecuteNonQuery();

                // Update favorite count
                using var updateCmd = new NpgsqlCommand(
                    "UPDATE Pet_Profile SET FavoriteCount = FavoriteCount + 1 WHERE PetProfileId = @pet",
                    con,
                    transaction);
                updateCmd.Parameters.AddWithValue("@pet", pet);
                updateCmd.ExecuteNonQuery();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public static void UpdateUnfavorite(int userId, int petProfileId)
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            using var transaction = con.BeginTransaction();
            try
            {
                // Delete the favorite entry
                using var deleteCmd = new NpgsqlCommand(
                    "DELETE FROM FavoritePet WHERE UserId = @userId AND PetProfileId = @petProfileId",
                    con,
                    transaction);
                deleteCmd.Parameters.AddWithValue("@userId", userId);
                deleteCmd.Parameters.AddWithValue("@petProfileId", petProfileId);
                deleteCmd.ExecuteNonQuery();

                // Decrease favorite count in Pet_Profile
                using var updateCmd = new NpgsqlCommand(
                    "UPDATE Pets SET FavoriteCount = FavoriteCount - 1 WHERE PetProfileId = @pet AND FavoriteCount > 0",
                    con,
                    transaction);
                updateCmd.Parameters.AddWithValue("@pet", petProfileId);
                updateCmd.ExecuteNonQuery();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
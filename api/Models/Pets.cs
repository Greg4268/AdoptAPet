using Npgsql;
using api.Data;
namespace api.Models
{
    public class Pets
    {
        public int PetProfileId { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Breed { get; set; }
        public string? Name { get; set; }
        public string? Species { get; set; }
        public bool Deleted { get; set; }
        public int ShelterId { get; set; }
        public string? ImageUrl { get; set; }
        public int FavoriteCount { get; set; }

        public static List<Pets> GetAllPets()
        {
            List<Pets> myPets = new();
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM Pets WHERE deleted = false";
            using var cmd = new NpgsqlCommand(stm, con);

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                myPets.Add(new Pets()
                {
                    PetProfileId = rdr.GetInt32(rdr.GetOrdinal("PetProfileId")),
                    Age = rdr.GetInt32(rdr.GetOrdinal("Age")),
                    BirthDate = rdr.GetDateTime(rdr.GetOrdinal("BirthDate")),
                    Breed = rdr.GetString(rdr.GetOrdinal("Breed")),
                    Name = rdr.GetString(rdr.GetOrdinal("Name")),
                    Species = rdr.GetString(rdr.GetOrdinal("Species")),
                    Deleted = rdr.GetBoolean(rdr.GetOrdinal("deleted")),
                    ShelterId = rdr.GetInt32(rdr.GetOrdinal("ShelterId")),
                    ImageUrl = rdr.GetString(rdr.GetOrdinal("ImageUrl")),
                    FavoriteCount = rdr.GetInt32(rdr.GetOrdinal("FavoriteCount"))
                });
            }
            return myPets;
        }

        public void SaveToDB()
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = @"INSERT INTO Pets 
                (Age, BirthDate, Breed, Name, Species, deleted, ShelterId, ImageUrl, FavoriteCount) 
                VALUES 
                (@Age, @BirthDate, @Breed, @Name, @Species, @deleted, @ShelterId, @ImageUrl, @FavoriteCount)";

            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
            cmd.Parameters.AddWithValue("@Breed", Breed);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Species", Species);
            cmd.Parameters.AddWithValue("@deleted", Deleted);
            cmd.Parameters.AddWithValue("@ShelterId", ShelterId);
            cmd.Parameters.AddWithValue("@ImageUrl", ImageUrl);
            cmd.Parameters.AddWithValue("@FavoriteCount", FavoriteCount);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void UpdateToDB()
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = @"UPDATE Pets 
                SET Age = @Age, 
                    BirthDate = @BirthDate, 
                    Breed = @Breed, 
                    Name = @Name, 
                    Species = @Species, 
                    deleted = @deleted, 
                    ShelterId = @ShelterId, 
                    ImageUrl = @ImageUrl, 
                    FavoriteCount = @FavoriteCount 
                WHERE PetProfileId = @PetProfileId";

            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileId);
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
            cmd.Parameters.AddWithValue("@Breed", Breed);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Species", Species);
            cmd.Parameters.AddWithValue("@deleted", Deleted);
            cmd.Parameters.AddWithValue("@ShelterId", ShelterId);
            cmd.Parameters.AddWithValue("@ImageUrl", ImageUrl);
            cmd.Parameters.AddWithValue("@FavoriteCount", FavoriteCount);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void OldFavoritePet(Pets value)
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Pets SET favorited = @favorited WHERE PetProfileId = @PetProfileId";
            cmd.Parameters.AddWithValue("@PetProfileId", value.PetProfileId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public static void DeletePet(int petId)
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            using var cmd = new NpgsqlCommand("UPDATE Pets SET deleted = true WHERE PetProfileId = @PetProfileId", con);
            cmd.Parameters.AddWithValue("@PetProfileId", petId);
            cmd.ExecuteNonQuery();
        }

        public static Pets GetPetById(int petProfileId)
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM Pets WHERE PetProfileId = @PetProfileId";
            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@PetProfileId", petProfileId);

            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return new Pets()
                {
                    PetProfileId = rdr.GetInt32(rdr.GetOrdinal("PetProfileId")),
                    Age = rdr.GetInt32(rdr.GetOrdinal("Age")),
                    BirthDate = rdr.GetDateTime(rdr.GetOrdinal("BirthDate")),
                    Breed = rdr.GetString(rdr.GetOrdinal("Breed")),
                    Name = rdr.GetString(rdr.GetOrdinal("Name")),
                    Species = rdr.GetString(rdr.GetOrdinal("Species")),
                    Deleted = rdr.GetBoolean(rdr.GetOrdinal("deleted")),
                    ShelterId = rdr.GetInt32(rdr.GetOrdinal("ShelterId")),
                    ImageUrl = rdr.GetString(rdr.GetOrdinal("ImageUrl")),
                    FavoriteCount = rdr.GetInt32(rdr.GetOrdinal("FavoriteCount"))
                };
            }
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Data;
using Npgsql;

namespace api.Repository
{
    public class PetsRepository : IPetsRepository
    {
        private readonly GetPublicConnection cs = new();
        public List<Pets> GetAllPets()
        {
            List<Pets> myPets = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM \"Pets\" WHERE \"Deleted\" = false";
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
                    Deleted = rdr.GetBoolean(rdr.GetOrdinal("Deleted")),
                    ShelterId = rdr.GetInt32(rdr.GetOrdinal("ShelterId")),
                    ImageUrl = rdr.GetString(rdr.GetOrdinal("ImageUrl")),
                    FavoriteCount = rdr.GetInt32(rdr.GetOrdinal("FavoriteCount"))
                });
            }
            return myPets;
        }

        public void SaveToDB(Pets value)
        {
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = @"INSERT INTO ""Pets"" (
                ""Age"", ""BirthDate"", ""Breed"", ""Name"", ""Species"", ""Deleted"", ""ShelterId"", ""ImageUrl"", ""FavoriteCount"") 
                VALUES (
                @Age, @BirthDate, @Breed, @Name, @Species, @Deleted, @ShelterId, @ImageUrl, @FavoriteCount)";

            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@Age", value.Age);
            cmd.Parameters.AddWithValue("@BirthDate", value.BirthDate);
            cmd.Parameters.AddWithValue("@Breed", value.Breed);
            cmd.Parameters.AddWithValue("@Name", value.Name);
            cmd.Parameters.AddWithValue("@Species", value.Species);
            cmd.Parameters.AddWithValue("@Deleted", value.Deleted);
            cmd.Parameters.AddWithValue("@ShelterId", value.ShelterId);
            cmd.Parameters.AddWithValue("@ImageUrl", value.ImageUrl);
            cmd.Parameters.AddWithValue("@FavoriteCount", value.FavoriteCount);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void UpdateToDB(Pets pet)
        {
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = @"UPDATE ""Pets"" 
                SET ""Age"" = @Age, 
                    ""BirthDate"" = @BirthDate, 
                    ""Breed"" = @Breed, 
                    ""Name"" = @Name, 
                    ""Species"" = @Species, 
                    ""Deleted"" = @Deleted, 
                    ""ShelterId"" = @ShelterId, 
                    ""ImageUrl"" = @ImageUrl, 
                    ""FavoriteCount"" = @FavoriteCount 
                WHERE ""PetProfileId"" = @PetProfileId";

            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@PetProfileId", pet.PetProfileId);
            cmd.Parameters.AddWithValue("@Age", pet.Age);
            cmd.Parameters.AddWithValue("@BirthDate", pet.BirthDate);
            cmd.Parameters.AddWithValue("@Breed", pet.Breed);
            cmd.Parameters.AddWithValue("@Name", pet.Name);
            cmd.Parameters.AddWithValue("@Species", pet.Species);
            cmd.Parameters.AddWithValue("@Deleted", pet.Deleted);
            cmd.Parameters.AddWithValue("@ShelterId", pet.ShelterId);
            cmd.Parameters.AddWithValue("@ImageUrl", pet.ImageUrl);
            cmd.Parameters.AddWithValue("@FavoriteCount", pet.FavoriteCount);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void OldFavoritePet(Pets value)
        {
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE \"Pets\" SET \"Favorited\" = @Favorited WHERE \"PetProfileId\" = @PetProfileId";
            cmd.Parameters.AddWithValue("@PetProfileId", value.PetProfileId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void DeletePet(int petId)
        {
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            using var cmd = new NpgsqlCommand("UPDATE \"Pets\" SET \"Deleted\" = true WHERE \"PetProfileId\" = @PetProfileId", con);
            cmd.Parameters.AddWithValue("@PetProfileId", petId);
            cmd.ExecuteNonQuery();
        }

        public Pets GetPetById(int petProfileId)
        {
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM \"Pets\" WHERE \"PetProfileId\" = @PetProfileId";
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
                    Deleted = rdr.GetBoolean(rdr.GetOrdinal("Deleted")),
                    ShelterId = rdr.GetInt32(rdr.GetOrdinal("ShelterId")),
                    ImageUrl = rdr.GetString(rdr.GetOrdinal("ImageUrl")),
                    FavoriteCount = rdr.GetInt32(rdr.GetOrdinal("FavoriteCount"))
                };
            }
            return null;
        }
    }
}
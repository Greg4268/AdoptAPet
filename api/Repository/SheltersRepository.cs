using api.Models;
using api.Data;
using Npgsql;

namespace api.Repository
{
    public class SheltersRepository : IShelterRepository
    {
        private readonly GetPublicConnection cs = new();
        public List<Shelters> GetAllShelters()
        {
            List<Shelters> myShelters = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM \"Shelters\"";
            using var cmd = new NpgsqlCommand(stm, con);

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                myShelters.Add(new Shelters()
                {
                    ShelterId = rdr.GetInt32(rdr.GetOrdinal("ShelterId")),
                    ShelterUsername = rdr.GetString(rdr.GetOrdinal("ShelterUsername")),
                    ShelterPassword = rdr.GetString(rdr.GetOrdinal("ShelterPassword")),
                    Address = rdr.GetString(rdr.GetOrdinal("Address")),
                    HoursOfOperation = rdr.GetString(rdr.GetOrdinal("HoursOfOperation")),
                    Deleted = rdr.GetBoolean(rdr.GetOrdinal("Deleted")),
                    Approved = rdr.GetBoolean(rdr.GetOrdinal("Approved")),
                    Email = rdr.GetString(rdr.GetOrdinal("Email")),
                    AccountType = rdr.GetString(rdr.GetOrdinal("AccountType"))
                });
            }
            return myShelters;
        }

        public void SaveToDB(Shelters shelter)
        {
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = @"INSERT INTO ""Shelters"" 
                (""ShelterId"", ""ShelterUsername"", ""ShelterPassword"", ""Address"", ""HoursOfOperation"", ""Deleted"", ""Email"", ""Approved"", ""AccountType"") 
                VALUES 
                (@ShelterId, @ShelterUsername, @ShelterPassword, @Address, @HoursOfOperation, @Deleted, @Email, @Approved, @AccountType)";

            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@ShelterId", shelter.ShelterId);
            cmd.Parameters.AddWithValue("@ShelterUsername", shelter.ShelterUsername);
            cmd.Parameters.AddWithValue("@ShelterPassword", shelter.ShelterPassword);
            cmd.Parameters.AddWithValue("@Address", shelter.Address);
            cmd.Parameters.AddWithValue("@HoursOfOperation", shelter.HoursOfOperation);
            cmd.Parameters.AddWithValue("@Deleted", shelter.Deleted);
            cmd.Parameters.AddWithValue("@Email", shelter.Email);
            cmd.Parameters.AddWithValue("@Approved", shelter.Approved);
            cmd.Parameters.AddWithValue("@AccountType", shelter.AccountType);
            cmd.ExecuteNonQuery();
        }

        public void UpdateToDB(Shelters shelter)
        {
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            string stm = @"UPDATE ""Shelters"" 
                SET ""ShelterId"" = @ShelterId, 
                    ""Username"" = @ShelterUsername, 
                    ""Password"" = @ShelterPassword, 
                    ""Address"" = @Address, 
                    ""HoursOfOperation"" = @HoursOfOperation, 
                    ""Deleted"" = @Deleted, 
                    ""ShelterEmail"" = @Email, 
                    ""Approved"" = @Approved, 
                    ""AccountType"" = @AccountType 
                WHERE ""ShelterId"" = @ShelterId";

            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@ShelterId", shelter.ShelterId);
            cmd.Parameters.AddWithValue("@ShelterUsername", shelter.ShelterUsername);
            cmd.Parameters.AddWithValue("@ShelterPassword", shelter.ShelterPassword);
            cmd.Parameters.AddWithValue("@Address", shelter.Address);
            cmd.Parameters.AddWithValue("@HoursOfOperation", shelter.HoursOfOperation);
            cmd.Parameters.AddWithValue("@Deleted", shelter.Deleted);
            cmd.Parameters.AddWithValue("@Email", shelter.Email);
            cmd.Parameters.AddWithValue("@Approved", shelter.Approved);
            cmd.Parameters.AddWithValue("@AccountType", shelter.AccountType);
            cmd.ExecuteNonQuery();
        }

        public Shelters GetShelterById(int shelterId)
        {
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM \"Shelters\" WHERE \"ShelterId\" = @ShelterId";
            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@ShelterId", shelterId);

            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return new Shelters()
                {
                    ShelterId = rdr.GetInt32(rdr.GetOrdinal("ShelterId")),
                    ShelterUsername = rdr.GetString(rdr.GetOrdinal("ShelterUsername")),
                    ShelterPassword = rdr.GetString(rdr.GetOrdinal("ShelterPassword")),
                    Address = rdr.GetString(rdr.GetOrdinal("Address")),
                    HoursOfOperation = rdr.GetString(rdr.GetOrdinal("HoursOfOperation")),
                    Deleted = rdr.GetBoolean(rdr.GetOrdinal("Deleted")),
                    Email = rdr.GetString(rdr.GetOrdinal("Email")),
                    Approved = rdr.GetBoolean(rdr.GetOrdinal("Approved")),
                    AccountType = rdr.GetString(rdr.GetOrdinal("AccountType"))
                };
            }
            return null;
        }

        public void ApprovalOfShelter(int shelterId, bool approved)
        {
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            using var cmd = new NpgsqlCommand("UPDATE \"Shelters\" SET \"Approved\" = @Approved WHERE \"ShelterId\" = @ShelterId", con);
            cmd.Parameters.AddWithValue("@ShelterId", shelterId);
            cmd.Parameters.AddWithValue("@Approved", approved);
            cmd.ExecuteNonQuery();
        }

        public List<Pets> GetPetsByShelter(int shelterId)
        {
            var pets = new List<Pets>();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            string query = @"SELECT p.""PetProfileId"", p.""Name"", p.""Breed"", p.""Species"", p.""Age"", p.""FavoriteCount"", 
                           p.""BirthDate"", p.""Deleted"", p.""ShelterId"", p.""ImageUrl"" 
                           FROM ""Shelters"" s 
                           JOIN ""Pets"" p ON s.""ShelterId"" = p.""ShelterId"" 
                           WHERE p.""Deleted"" = false AND s.""ShelterId"" = @ShelterId 
                           ORDER BY p.""PetProfileId""";

            using var cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ShelterId", shelterId);

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var pet = new Pets
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
                pets.Add(pet);
            }
            return pets;
        }

        public Shelters GetUserLogin(string email, string password)
        {
            using var con = new NpgsqlConnection(cs.cs);
            try
            {
                con.Open();
                string stm = "SELECT * FROM \"Shelters\" WHERE \"Email\" = @Email AND \"ShelterPassword\" = @ShelterPassword";
                using var cmd = new NpgsqlCommand(stm, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@ShelterPassword", password);

                using var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    return new Shelters()
                    {
                        ShelterId = rdr.GetInt32(rdr.GetOrdinal("ShelterId")),
                        ShelterUsername = rdr.GetString(rdr.GetOrdinal("ShelterUsername")),
                        ShelterPassword = rdr.GetString(rdr.GetOrdinal("ShelterPassword")),
                        Address = rdr.GetString(rdr.GetOrdinal("Address")),
                        HoursOfOperation = rdr.GetString(rdr.GetOrdinal("HoursOfOperation")),
                        Email = rdr.GetString(rdr.GetOrdinal("Email")),
                        Deleted = rdr.GetBoolean(rdr.GetOrdinal("Deleted")),
                        Approved = rdr.GetBoolean(rdr.GetOrdinal("Approved")),
                        AccountType = rdr.GetString(rdr.GetOrdinal("AccountType"))
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database access error: " + ex.Message);
            }
            return null;
        }

    }
}
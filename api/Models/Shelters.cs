using MySql.Data.MySqlClient;
using api.Data;
namespace api.Models
{
    public class Shelters
    {
        public int ShelterId { get; set; }
        public string ShelterUsername { get; set; }
        public string ShelterPassword { get; set; }
        public string Address { get; set; }
        public string HoursOfOperation { get; set; }
        public bool deleted { get; set; }
        public string Email { get; set; }
        public bool Approved { get; set; }
        public string AccountType { get; set; }

        public static List<Shelters> GetAllShelters() // method to retrieve shelter from database
        {
            List<Shelters> myShelters = new List<Shelters>(); // initialize array to hold shelter
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open databse connection
            string stm = "SELECT * FROM Shelter"; // sql statement to select everything from the shelter table
            MySqlCommand cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            while (rdr.Read()) // iterate through table rows
            {
                myShelters.Add(new Shelters() // create shelter object for each row
                {
                    ShelterId = rdr.GetInt32("ShelterId"),
                    ShelterUsername = rdr.GetString("ShelterUsername"),
                    ShelterPassword = rdr.GetString("ShelterPassword"),
                    Address = rdr.GetString("Address"),
                    HoursOfOperation = rdr.GetString("HoursOfOperation"),
                    deleted = rdr.GetBoolean("deleted"),
                    Approved = rdr.GetBoolean("Approved"),
                    Email = rdr.GetString("Email"),
                    AccountType = rdr.GetString("AccountType")
                });
            }
            con.Close();
            return myShelters; // return populated list
        }

        public void SaveToDB() // method to save the shelters to the database
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open database connection
            string stm = "INSERT INTO Shelter (ShelterId, ShelterUsername, ShelterPassword, Address, HoursOfOperation, deleted, Email, Approved, AccountType) VALUES (@ShelterId, @ShelterUsername, @ShelterPassword, @Address, @HoursOfOperation, @deleted, @Email, @Approved, @AccountType)"; // sql command to insert a new shelter
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@ShelterId", ShelterId); // add parameters to the sql command
            cmd.Parameters.AddWithValue("@ShelterUsername", ShelterUsername);
            cmd.Parameters.AddWithValue("@ShelterPassword", ShelterPassword);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@HoursOfOperation", HoursOfOperation);
            cmd.Parameters.AddWithValue("@deleted", deleted);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Approved", Approved);
            cmd.Parameters.AddWithValue("@AccountType", AccountType);

            cmd.ExecuteNonQuery(); // execute sql command
            con.Close();
        }

        public void UpdateToDB() // method to update existing shelter in database
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open db connection

            string stm = "UPDATE Shelter SET ShelterId = @ShelterId, Username = @ShelterUsername, Password = @ShelterPassword, Address = @Address, HoursOfOperation = @HoursOfOperation, deleted = @deleted, ShelterEmail = @Email, Approved = @Approved, AccountType = @AccountType WHERE ShelterId = @ShelterId"; // sql command for updating a shelter
            Console.WriteLine("SQL query: " + stm); // log the sql query to console for debugging
            Console.WriteLine("Parameters:"); // log parameters
            Console.WriteLine("@ShelterId: " + ShelterId);
            Console.WriteLine("@ShelterUsername: " + ShelterUsername);
            Console.WriteLine("@ShelterPassword: " + ShelterPassword);
            Console.WriteLine("@Address: " + Address);
            Console.WriteLine("@HoursOfOperation: " + HoursOfOperation);
            Console.WriteLine("@deleted" + deleted);
            Console.WriteLine("@Email" + Email);
            Console.WriteLine("@Approved" + Approved);
            Console.WriteLine("@AccountType" + AccountType);


            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@ShelterId", ShelterId); // add parameters to sql command
            cmd.Parameters.AddWithValue("@ShelterUsername", ShelterUsername);
            cmd.Parameters.AddWithValue("@ShelterPassword", ShelterPassword);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@HoursOfOperation", HoursOfOperation);
            cmd.Parameters.AddWithValue("@deleted", deleted);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Approved", Approved);
            cmd.Parameters.AddWithValue("@AccountType", AccountType);
            cmd.ExecuteNonQuery(); // execute sql command
            con.Close();
        }


        public static Shelters GetShelterById(int ShelterId) // method to retrieve specific shelter
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            con.Open(); // open connection to db
            string stm = "SELECT * FROM Shelter WHERE ShelterId = @ShelterId"; // sql statement to retrieve specific shelter
            MySqlCommand cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@ShelterId", ShelterId); // add ShelterID as parameter
            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            if (rdr.Read()) // check if shelter is found
            {
                return new Shelters() // construct and initialize new shelter object
                {
                    ShelterId = rdr.GetInt32("ShelterId"),
                    ShelterUsername = rdr.GetString("ShelterUsername"),
                    ShelterPassword = rdr.GetString("ShelterPassword"),
                    Address = rdr.GetString("Address"),
                    HoursOfOperation = rdr.GetString("HoursOfOperation"),
                    deleted = rdr.GetBoolean("deleted"),
                    Email = rdr.GetString("Email"),
                    Approved = rdr.GetBoolean("Approved"),
                    AccountType = rdr.GetString("AccountType")
                };
            }
            con.Close();
            return null; // if no shelter is found
        }

        public void ApprovalOfShelter(int ShelterId, bool Approved)
        {

            GetPublicConnection cs = new GetPublicConnection();
            using (var con = new MySqlConnection(cs.cs))
            {
                Console.WriteLine("Opening connection...");
                con.Open();
                using (var cmd = new MySqlCommand("UPDATE Shelter SET Approved = !Approved WHERE ShelterId = @ShelterId", con))
                {
                    Console.WriteLine($"Updating ShelterId: {ShelterId} to Approved: {Approved}");
                    cmd.Parameters.AddWithValue("@ShelterId", ShelterId);
                    cmd.Parameters.AddWithValue("@Approved", Approved);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Pets> GetPetsByShelter(int shelterId)
        {
            var pets = new List<Pets>();
            GetPublicConnection cs = new GetPublicConnection();
            using (var con = new MySqlConnection(cs.cs))
            {
                con.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT p.PetProfileId, p.Name, p.Breed, p.Species, p.Age, p.FavoriteCount, p.BirthDate, p.deleted, p.ShelterId, p.ImageUrl FROM Shelter s JOIN Pet_Profile p ON s.ShelterId = p.ShelterId WHERE p.deleted = 0 AND s.ShelterId = @ShelterId ORDER BY p.PetProfileId";
                    cmd.Parameters.AddWithValue("@shelterId", shelterId);

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var pet = new Pets
                            {
                                PetProfileId = rdr.GetInt32("PetProfileId"),
                                Age = rdr.GetInt32("Age"),
                                BirthDate = rdr.GetDateTime("BirthDate"),
                                Breed = rdr.GetString("Breed"),
                                Name = rdr.GetString("Name"),
                                Species = rdr.GetString("Species"),
                                deleted = rdr.GetBoolean("deleted"),
                                ShelterId = rdr.GetInt32("ShelterId"),
                                ImageUrl = rdr.GetString("ImageUrl"),
                                FavoriteCount = rdr.GetInt32("FavoriteCount"),
                            };
                            pets.Add(pet);
                        }
                    }
                }
            }
            return pets;
        }

        public static Shelters GetUserLogin(string Email, string Password)
        {
            GetPublicConnection cs = new GetPublicConnection(); // create new instance of database
            using var con = new MySqlConnection(cs.cs);
            try
            {
                con.Open(); // open connection to db
                string stm = "SELECT * FROM Shelter WHERE Email = @Email AND ShelterPassword = @ShelterPassword";
                MySqlCommand cmd = new MySqlCommand(stm, con);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@ShelterPassword", Password);
                using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
                if (rdr.Read())
                {
                    return new Shelters()
                    {
                        ShelterId = rdr.GetInt32("ShelterId"),
                        ShelterUsername = rdr.GetString("ShelterUsername"),
                        ShelterPassword = rdr.GetString("ShelterPassword"),
                        Address = rdr.GetString("Address"),
                        HoursOfOperation = rdr.GetString("HoursOfOperation"),
                        Email = rdr.GetString("Email"),
                        deleted = rdr.GetBoolean("deleted"),
                        Approved = rdr.GetBoolean("Approved"),
                        AccountType = rdr.GetString("AccountType"),
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database access error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return null;
        }


    }
}

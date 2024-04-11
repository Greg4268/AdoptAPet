namespace api.Models
{
    public class UserAccounts
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }

        // Method to retrieve all user accounts from the database
        public static List<UserAccounts> GetAllUserAccounts()
        {
            List<UserAccounts> UserAccounts = new List<UserAccounts>();
            GetPublicConnection cs = new GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM UserAccounts";
            MySqlCommand cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                userAccounts.Add(new UserAccounts()
                {
                    UserID = rdr.GetInt32("UserID"),
                    Username = rdr.GetString("Username"),
                    Email = rdr.GetString("Email"),
                    Password = rdr.GetString("Password"),
                    RegistrationDate = rdr.GetDateTime("RegistrationDate"),
                    IsActive = rdr.GetBoolean("IsActive")
                });
            }
            return UserAccounts;
        }

        // Method to save the user account to the database
        public void SaveToDB()
        {
            GetPublicConnection cs = new GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "INSERT INTO UserAccounts (Username, Email, Password, RegistrationDate, IsActive) VALUES (@Username, @Email, @Password, @RegistrationDate, @IsActive)";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@RegistrationDate", RegistrationDate);
            cmd.Parameters.AddWithValue("@IsActive", IsActive ? 1 : 0);
            cmd.ExecuteNonQuery();
        }

        // Method to retrieve a specific user account by ID
        public static UserAccounts GetUserAccountsById(int UserID)
        {
            GetPublicConnection cs = new GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM UserAccounts WHERE UserID = @UserID";
            MySqlCommand cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return new UserAccounts()
                {
                    UserID = rdr.GetInt32("UserID"),
                    Username = rdr.GetString("Username"),
                    Email = rdr.GetString("Email"),
                    Password = rdr.GetString("Password"),
                    RegistrationDate = rdr.GetDateTime("RegistrationDate"),
                    IsActive = rdr.GetBoolean("IsActive")
                };
            }
            return null;
        }
    }
}

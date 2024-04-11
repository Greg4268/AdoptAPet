using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class AdoptionApplication
    {
        public int ApplicationID { get; set; }
        public int UserID { get; set; }
        public int PetProfileID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string ApplicationStatus { get; set; }
        public string ApplicationNotes { get; set; }

        // Method to retrieve all adoption applications from the database
        public static List<AdoptionApplication> GetAllAdoptionApplications()
        {
            List<AdoptionApplication> applications = new List<AdoptionApplication>();
            GetPublicConnection cs = new GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM AdoptionApplications";
            MySqlCommand cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                applications.Add(new AdoptionApplication()
                {
                    ApplicationID = rdr.GetInt32("ApplicationID"),
                    UserID = rdr.GetInt32("UserID"),
                    PetProfileID = rdr.GetInt32("PetProfileID"),
                    ApplicationDate = rdr.GetDateTime("ApplicationDate"),
                    ApplicationStatus = rdr.GetString("ApplicationStatus"),
                    ApplicationNotes = rdr.GetString("ApplicationNotes")
                });
            }
            return applications;
        }

        // Method to save the adoption application to the database
        public void SaveToDB()
        {
            GetPublicConnection cs = new GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "INSERT INTO AdoptionApplications (UserID, PetProfileID, ApplicationDate, ApplicationStatus, ApplicationNotes) VALUES (@UserID, @PetProfileID, @ApplicationDate, @ApplicationStatus, @ApplicationNotes)";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@PetProfileID", PetProfileID);
            cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            cmd.Parameters.AddWithValue("@ApplicationNotes", ApplicationNotes);
            cmd.ExecuteNonQuery();
        }

        // Method to retrieve a specific adoption application by ID
        public static AdoptionApplication GetAdoptionApplicationById(int ApplicationID)
        {
            GetPublicConnection cs = new GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM AdoptionApplications WHERE ApplicationID = @ApplicationID";
            MySqlCommand cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return new AdoptionApplication()
                {
                    ApplicationID = rdr.GetInt32("ApplicationID"),
                    UserID = rdr.GetInt32("UserID"),
                    PetProfileID = rdr.GetInt32("PetProfileID"),
                    ApplicationDate = rdr.GetDateTime("ApplicationDate"),
                    ApplicationStatus = rdr.GetString("ApplicationStatus"),
                    ApplicationNotes = rdr.GetString("ApplicationNotes")
                };
            }
            return null;
        }
    }
}

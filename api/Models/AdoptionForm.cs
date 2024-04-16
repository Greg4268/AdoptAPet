using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace api.Models
{
    public class AdoptionForm
    {
        public int FormID { get; set; }
        public int UserID { get; set; }
        public int PetProfileID { get; set; }
        public DateTime FormDate { get; set; }
        public string FormStatus { get; set; }
        public string FormNotes { get; set; }

        // Method to retrieve all adoption forms from the database
        public static List<AdoptionForm> GetAllAdoptionForms()
        {
            List<AdoptionForm> forms = new List<AdoptionForm>();
            Data.GetPublicConnection cs = new Data.GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM AdoptionForms";
            MySqlCommand cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                forms.Add(new AdoptionForm()
                {
                    FormID = rdr.GetInt32("FormID"),
                    UserID = rdr.GetInt32("UserID"),
                    PetProfileID = rdr.GetInt32("PetProfileID"),
                    FormDate = rdr.GetDateTime("FormDate"),
                    FormStatus = rdr.GetString("FormStatus"),
                    FormNotes = rdr.GetString("FormNotes")
                });
            }
            return forms;
        }

        // Method to save the adoption form to the database
        public void SaveToDB()
        {
            Data.GetPublicConnection cs = new Data.GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "INSERT INTO AdoptionForms (UserID, PetProfileID, FormDate, FormStatus, FormNotes) VALUES (@UserID, @PetProfileID, @FormDate, @FormStatus, @FormNotes)";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@PetProfileID", PetProfileID);
            cmd.Parameters.AddWithValue("@FormDate", FormDate);
            cmd.Parameters.AddWithValue("@FormStatus", FormStatus);
            cmd.Parameters.AddWithValue("@FormNotes", FormNotes);
            cmd.ExecuteNonQuery();
        }

        // Method to retrieve a specific adoption Form by ID
        public static AdoptionForm GetAdoptionFormById(int FormID)
        {
            Data.GetPublicConnection cs = new Data.GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM AdoptionForms WHERE FormID = @FormID";
            MySqlCommand cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@FormID", FormID);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return new AdoptionForm()
                {
                    FormID = rdr.GetInt32("FormID"),
                    UserID = rdr.GetInt32("UserID"),
                    PetProfileID = rdr.GetInt32("PetProfileID"),
                    FormDate = rdr.GetDateTime("FormDate"),
                    FormStatus = rdr.GetString("FormStatus"),
                    FormNotes = rdr.GetString("FormNotes")
                };
            }
            return null;
        }
    }
}

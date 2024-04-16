
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using api.Data;

namespace api.Models
{
    public class AdoptionForm
    {
        public int FormId { get; set; }
        public int UserId { get; set; }
        public int PetProfileId { get; set; }
        public DateTime FormDate { get; set; }
        public bool Approved { get; set; }
        public string FormNotes { get; set; }
        public bool deleted { get; set; }

        // Method to retrieve all adoption forms from the database
        public static List<AdoptionForm> GetAllAdoptionForms()
        {
            List<AdoptionForm> forms = new List<AdoptionForm>();
            Data.GetPublicConnection cs = new Data.GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM Adoption_Forms";
            MySqlCommand cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                forms.Add(new AdoptionForm()
                {
                    FormId = rdr.GetInt32("FormID"),
                    UserId = rdr.GetInt32("UserID"),
                    PetProfileId = rdr.GetInt32("PetProfileID"),
                    FormDate = rdr.GetDateTime("FormDate"),
                    Approved = rdr.GetInt32("Approved") == 1,
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
            string stm = "INSERT INTO AdoptionForms (UserID, PetProfileID, FormDate, Approved, FormNotes) VALUES (@UserID, @PetProfileID, @FormDate, @Approved, @FormNotes)";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileId);
            cmd.Parameters.AddWithValue("@FormDate", FormDate);
            cmd.Parameters.AddWithValue("@Approved", Approved ? 1 : 0);
            cmd.Parameters.AddWithValue("@FormNotes", FormNotes);
            cmd.ExecuteNonQuery();
        }

        // Method to retrieve a specific adoption Form by ID
        public static AdoptionForm GetAdoptionFormById(int FormId)
        {
            Data.GetPublicConnection cs = new Data.GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM Adoption_Forms WHERE FormId = @FormId";
            MySqlCommand cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@FormId", FormId);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return new AdoptionForm()
                {
                    FormId = rdr.GetInt32("FormId"),
                    UserId = rdr.GetInt32("UserId"),
                    PetProfileId = rdr.GetInt32("PetProfileId"),
                    FormDate = rdr.GetDateTime("FormDate"),
                    Approved = rdr.GetInt32("Approved") == 1,
                    FormNotes = rdr.GetString("FormNotes")
                };
            }
            return null;
        }
        public void UpdateToDB()
        {
            Data.GetPublicConnection cs = new Data.GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "UPDATE Adoption_Forms SET FormId = @FormId, UserId = @UserId, PetProfileId = @PetProfileId, FormDate = @FormDate, Approved = @Approved, FormNotes = @FormNotes";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@FormId", FormId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FormDate", FormDate);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileId);
            cmd.Parameters.AddWithValue("@Approved", Approved);
            cmd.ExecuteNonQuery();
        }
        public void DeleteAdoptionForm(AdoptionForm value) {
            GetPublicConnection cs = new GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Adoption_Forms SET deleted = @deleted WHERE FormId = @FormId";
            cmd.Parameters.AddWithValue("@FormId", value.FormId);
            cmd.Parameters.AddWithValue("@deleted", value.deleted);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }        

    }
}

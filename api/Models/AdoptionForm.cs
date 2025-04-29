using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Npgsql;

namespace api.Models
{
    public class AdoptionForm
    {
        public int FormId { get; set; }
        public int UserId { get; set; }
        public DateTime FormDate { get; set; }
        public bool Approved { get; set; }
        public bool Deleted { get; set; }

        // Method to retrieve all adoption forms from the database
        public static List<AdoptionForm> GetAllAdoptionForms()
        {
            List<AdoptionForm> forms = new();
            Data.GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM Adoption_Forms";
            using var cmd = new NpgsqlCommand(stm, con);

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                forms.Add(new AdoptionForm()
                {
                    FormId = rdr.GetInt32(rdr.GetOrdinal("FormId")),
                    UserId = rdr.GetInt32(rdr.GetOrdinal("UserId")),
                    FormDate = rdr.GetDateTime(rdr.GetOrdinal("FormDate")),
                    Approved = rdr.GetBoolean(rdr.GetOrdinal("Approved")),
                    Deleted = rdr.GetBoolean(rdr.GetOrdinal("deleted")),
                });
            }
            return forms;
        }

        // Method to save the adoption form to the database
        public void SaveToDB()
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = "INSERT INTO AdoptionForms (UserId, FormDate, Approved, deleted) VALUES (@UserId, @FormDate, @Approved, @deleted)";
            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FormDate", FormDate);
            cmd.Parameters.AddWithValue("@Approved", Approved);
            cmd.Parameters.AddWithValue("@deleted", Deleted);
            cmd.ExecuteNonQuery();
        }

        // Method to retrieve a specific adoption Form by ID
        public static AdoptionForm GetAdoptionFormById(int FormId)
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM Adoption_Forms WHERE FormId = @FormId";
            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@FormId", FormId);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return new AdoptionForm()
                {
                    FormId = rdr.GetInt32(rdr.GetOrdinal("FormId")),
                    UserId = rdr.GetInt32(rdr.GetOrdinal("UserId")),
                    FormDate = rdr.GetDateTime(rdr.GetOrdinal("FormDate")),
                    Approved = rdr.GetBoolean(rdr.GetOrdinal("Approved")),
                    Deleted = rdr.GetBoolean(rdr.GetOrdinal("deleted")),
                };
            }
            return null;
        }

        public void UpdateToDB()
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = "UPDATE Adoption_Forms SET FormId = @FormId, UserId = @UserId, FormDate = @FormDate, Approved = @Approved, deleted = @deleted";
            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@FormId", FormId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FormDate", FormDate);
            cmd.Parameters.AddWithValue("@Approved", Approved);
            cmd.Parameters.AddWithValue("@deleted", Deleted);
            cmd.ExecuteNonQuery();
        }

        public void DeleteAdoptionForm(AdoptionForm value)
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Adoption_Forms SET deleted = @deleted WHERE FormId = @FormId";
            cmd.Parameters.AddWithValue("@FormId", value.FormId);
            cmd.Parameters.AddWithValue("@deleted", value.Deleted);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
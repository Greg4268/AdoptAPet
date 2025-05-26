using api.Models;
using api.Data;
using Npgsql;

namespace api.Repository
{
    public class AdoptionFormRepository : IAdoptionFormRepository
    {
        private readonly GetPublicConnection _cs;
    
        public AdoptionFormRepository(GetPublicConnection cs)
        {
            _cs = cs;
        }

        // Method to retrieve all adoption forms from the database
        public List<AdoptionForm> GetAllAdoptionForms()
        {
            List<AdoptionForm> forms = new();
            using var con = new NpgsqlConnection(_cs.cs);
            con.Open();
            string stm = "SELECT * FROM \"AdoptionForms\"";
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
                    Deleted = rdr.GetBoolean(rdr.GetOrdinal("Deleted"))
                });
            }
            return forms;
        }

        public void SaveToDB(AdoptionForm form)
        {
            using var con = new NpgsqlConnection(_cs.cs);
            con.Open();
            string stm = @"INSERT INTO ""AdoptionForms"" (
                ""UserId"", ""FormDate"", ""Approved"", ""Deleted"") 
                VALUES (
                @UserId, @FormDate, @Approved, @Deleted)";

            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@UserId", form.UserId);
            cmd.Parameters.AddWithValue("@FormDate", form.FormDate);
            cmd.Parameters.AddWithValue("@Approved", form.Approved);
            cmd.Parameters.AddWithValue("@Deleted", form.Deleted);
            cmd.ExecuteNonQuery();
        }

        // Method to retrieve a specific adoption Form by ID
        public AdoptionForm GetAdoptionFormById(int formId)
        {
            using var con = new NpgsqlConnection(_cs.cs);
            con.Open();
            string stm = "SELECT * FROM \"AdoptionForms\" WHERE \"FormId\" = @FormId";
            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@FormId", formId);

            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return new AdoptionForm()
                {
                    FormId = rdr.GetInt32(rdr.GetOrdinal("FormId")),
                    UserId = rdr.GetInt32(rdr.GetOrdinal("UserId")),
                    FormDate = rdr.GetDateTime(rdr.GetOrdinal("FormDate")),
                    Approved = rdr.GetBoolean(rdr.GetOrdinal("Approved")),
                    Deleted = rdr.GetBoolean(rdr.GetOrdinal("Deleted"))
                };
            }
            return null;
        }

        public void UpdateToDB(AdoptionForm form)
        {
            using var con = new NpgsqlConnection(_cs.cs);
            con.Open();
            string stm = @"UPDATE ""AdoptionForms"" 
                SET ""FormId"" = @FormId, 
                    ""UserId"" = @UserId, 
                    ""FormDate"" = @FormDate, 
                    ""Approved"" = @Approved, 
                    ""Deleted"" = @Deleted";

            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@FormId", form.FormId);
            cmd.Parameters.AddWithValue("@UserId", form.UserId);
            cmd.Parameters.AddWithValue("@FormDate", form.FormDate);
            cmd.Parameters.AddWithValue("@Approved", form.Approved);
            cmd.Parameters.AddWithValue("@Deleted", form.Deleted);
            cmd.ExecuteNonQuery();
        }

        public void DeleteAdoptionForm(AdoptionForm value)
        {
            using var con = new NpgsqlConnection(_cs.cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE \"AdoptionForms\" SET \"Deleted\" = @Deleted WHERE \"FormId\" = @FormId";
            cmd.Parameters.AddWithValue("@FormId", value.FormId);
            cmd.Parameters.AddWithValue("@Deleted", value.Deleted);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }        
}

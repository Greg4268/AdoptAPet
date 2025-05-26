using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Npgsql;
using api.Data;

namespace api.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        // Method to retrieve all appointments from the database
        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> appointments = new();
            Data.GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM \"Appointment\"";
            using var cmd = new NpgsqlCommand(stm, con);

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                appointments.Add(new Appointment()
                {
                    AppointmentId = rdr.GetInt32(rdr.GetOrdinal("AppointmentId")),
                    AppointmentDate = rdr.GetDateTime(rdr.GetOrdinal("AppointmentDate")),
                    UserId = rdr.GetInt32(rdr.GetOrdinal("UserId")),
                    PetProfileId = rdr.GetInt32(rdr.GetOrdinal("PetProfileId")),
                    Deleted = rdr.GetBoolean(rdr.GetOrdinal("Deleted"))
                });
            }
            return appointments;
        }

        // Method to save the appointment to the database
        public void SaveToDB()
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = @"INSERT INTO ""Appointment"" (
                ""AppointmentId"", ""AppointmentDate"", ""UserId"", ""PetProfileId"", ""Deleted"") 
                VALUES (
                @AppointmentId, @AppointmentDate, @UserId, @PetProfileId, @Deleted)";
            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@AppointmentId", AppointmentId);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileId);
            cmd.Parameters.AddWithValue("@Deleted", Deleted);
            cmd.ExecuteNonQuery();
        }

        // Method to update the appointment in the database
        public void UpdateToDB()
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string stm = @"UPDATE ""Appointment"" 
                SET ""AppointmentDate"" = @AppointmentDate, 
                    ""UserId"" = @UserId, 
                    ""PetProfileId"" = @PetProfileId, 
                    ""Deleted"" = @Deleted 
                WHERE ""AppointmentId"" = @AppointmentId";
            using var cmd = new NpgsqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@AppointmentId", AppointmentId);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileId);
            cmd.Parameters.AddWithValue("@Deleted", Deleted);
            cmd.ExecuteNonQuery();
        }

        // Method to retrieve a specific appointment by ID
        public Appointment GetAppointmentById(int appointmentId)
        {
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string query = "SELECT * FROM \"Appointment\" WHERE \"AppointmentId\" = @AppointmentId AND \"Deleted\" = false";
            using var cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Appointment()
                {
                    AppointmentId = reader.GetInt32(reader.GetOrdinal("AppointmentId")),
                    AppointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    PetProfileId = reader.GetInt32(reader.GetOrdinal("PetProfileId")),
                    Deleted = reader.GetBoolean(reader.GetOrdinal("Deleted"))
                };
            }
            return null;
        }

        public List<Appointment> GetAppointmentsByUserId(int userId)
        {
            List<Appointment> appointments = new();
            Data.GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string query = "SELECT * FROM \"Appointment\" WHERE \"UserId\" = @UserId AND \"Deleted\" = false";

            using var cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", userId);

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var appointment = new Appointment()
                {
                    AppointmentId = rdr.GetInt32(rdr.GetOrdinal("AppointmentId")),
                    AppointmentDate = rdr.GetDateTime(rdr.GetOrdinal("AppointmentDate")),
                    UserId = rdr.GetInt32(rdr.GetOrdinal("UserId")),
                    PetProfileId = rdr.GetInt32(rdr.GetOrdinal("PetProfileId")),
                    Deleted = rdr.GetBoolean(rdr.GetOrdinal("Deleted"))
                };
                appointments.Add(appointment);
            }
            return appointments;
        }

        // method to delete appt
        public void DeleteAppointment(int id)
        {
            AppointmentId = id;
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE \"Appointment\" SET \"Deleted\" = @Deleted WHERE \"AppointmentId\" = @AppointmentId";
            cmd.Parameters.AddWithValue("@AppointmentId", AppointmentId);
            cmd.Parameters.AddWithValue("@Deleted", Deleted);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public List<Appointment> GetAppointmentByPet(int petId)
        {
            List<Appointment> appointments = new();
            GetPublicConnection cs = new();
            using var con = new NpgsqlConnection(cs.cs);
            con.Open();
            string query = @"SELECT ""AppointmentId"", ""AppointmentDate"", ""UserId"", ""PetProfileId"", ""Deleted"" 
                         FROM ""Appointment"" 
                         WHERE ""PetProfileId"" = @PetProfileId AND ""Deleted"" = false";

            using var cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@PetProfileId", petId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var appointment = new Appointment
                {
                    AppointmentId = reader.GetInt32(reader.GetOrdinal("AppointmentId")),
                    AppointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    PetProfileId = reader.GetInt32(reader.GetOrdinal("PetProfileId")),
                    Deleted = reader.GetBoolean(reader.GetOrdinal("Deleted"))
                };
                appointments.Add(appointment);
            }
            return appointments;
        }

    }
}
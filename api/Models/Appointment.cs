
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using api.Data;

namespace api.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int UserId { get; set; }
        public int PetProfileId { get; set; }
        public bool deleted { get; set; }

        // Method to retrieve all appointments from the database
        public static List<Appointment> GetAllAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();
            Data.GetPublicConnection cs = new Data.GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM Appointments";
            MySqlCommand cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                appointments.Add(new Appointment()
                {
                    AppointmentId = rdr.GetInt32("AppointmentId"),
                    AppointmentDate = rdr.GetDateTime("AppointmentDate"),
                    UserId = rdr.GetInt32("UserId"),
                    PetProfileId = rdr.GetInt32("PetProfileId"),
                    deleted = rdr.GetInt32("deleted") == 1
                });
            }
            return appointments;
        }

        // Method to save the appointment to the database
        public void SaveToDB()
        {
            Data.GetPublicConnection cs = new Data.GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "INSERT INTO Appointments (AppointmentId, AppointmentDate, UserId, PetProfileId, deleted) VALUES (@AppointmentId, @AppointmentDate, @UserId, @PetProfileId, @deleted)";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@AppointmentId", AppointmentId);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileId);
            cmd.Parameters.AddWithValue("@deleted", deleted ? 1 : 0);
            cmd.ExecuteNonQuery();
        }

        // Method to update the appointment in the database
        public void UpdateToDB()
        {
            Data.GetPublicConnection cs = new Data.GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "UPDATE Appointments SET AppointmentDate = @AppointmentDate, UserId = @UserId, PetProfileId = @PetProfileId, deleted = @deleted WHERE AppointmentId = @AppointmentId";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@AppointmentId", AppointmentId);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@PetProfileId", PetProfileId);
            cmd.Parameters.AddWithValue("@deleted", deleted ? 1 : 0);
            cmd.ExecuteNonQuery();
        }

        // Method to retrieve a specific appointment by ID
        public static Appointment GetAppointmentById(int appointmentId)
        {
            Data.GetPublicConnection cs = new Data.GetPublicConnection();
            using (var con = new MySqlConnection(cs.cs))
            {
                con.Open();
                string query = "SELECT * FROM Appointments WHERE AppointmentId = @AppointmentId AND deleted = 0"; // Added logical delete check
                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Appointment()
                            {
                                AppointmentId = reader.GetInt32("AppointmentId"),
                                AppointmentDate = reader.GetDateTime("AppointmentDate"),
                                UserId = reader.GetInt32("UserId"),
                                PetProfileId = reader.GetInt32("PetProfileId"),
                                deleted = reader.GetBoolean("deleted")
                            };
                        }
                    }
                }
                con.Close();
            }
            return null;
        }

        public static List<Appointment> GetAppointmentsByUserId(int userId)
        {
            List<Appointment> appointments = new List<Appointment>();
            Data.GetPublicConnection cs = new Data.GetPublicConnection();
            using (var con = new MySqlConnection(cs.cs))
            {
                con.Open();
                string query = "SELECT * FROM Appointments WHERE UserId = @UserId AND deleted = 0"; 

                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId); 

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read()) 
                        {
                            var appointment = new Appointment()
                            {
                                AppointmentId = rdr.GetInt32("AppointmentId"),
                                AppointmentDate = rdr.GetDateTime("AppointmentDate"),
                                UserId = rdr.GetInt32("UserId"),
                                PetProfileId = rdr.GetInt32("PetProfileId"),
                                deleted = rdr.GetBoolean("deleted") 
                            };
                            appointments.Add(appointment);
                        }
                    }
                }
                con.Close();
            }
            return appointments;
        }


        // method to delete appt
        public void DeleteAppointment(int id)
        {
            AppointmentId = id;
            GetPublicConnection cs = new GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Appointments SET deleted = @deleted WHERE AppointmentId = @AppointmentId";
            cmd.Parameters.AddWithValue("@AppointmentId", AppointmentId);
            cmd.Parameters.AddWithValue("@deleted", deleted);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static List<Appointment> GetAppointmentByPet(int petId)
        {
            List<Appointment> appointments = new List<Appointment>();
            GetPublicConnection cs = new GetPublicConnection();
            using (var con = new MySqlConnection(cs.cs))
            {
                con.Open();
                string query = @"SELECT AppointmentId, AppointmentDate, UserId, PetProfileId, deleted 
                         FROM Appointments 
                         WHERE PetProfileId = @PetProfileId AND deleted = 0";

                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PetProfileId", petId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var appointment = new Appointment
                            {
                                AppointmentId = reader.GetInt32("AppointmentId"),
                                AppointmentDate = reader.GetDateTime("AppointmentDate"),
                                UserId = reader.GetInt32("UserId"),
                                PetProfileId = reader.GetInt32("PetProfileId"),
                                deleted = reader.GetBoolean("deleted")
                            };
                            appointments.Add(appointment);
                        }
                    }
                }
                con.Close();
            }
            return appointments;
        }

    }
}
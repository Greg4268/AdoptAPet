using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace api.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int PetProfileID { get; set; }
        public int UserID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentLocation { get; set; }

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
                    AppointmentID = rdr.GetInt32("AppointmentID"),
                    PetProfileID = rdr.GetInt32("PetProfileID"),
                    UserID = rdr.GetInt32("UserID"),
                    AppointmentDate = rdr.GetDateTime("AppointmentDate"),
                    AppointmentLocation = rdr.GetString("AppointmentLocation")
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
            string stm = "INSERT INTO Appointments (PetProfileID, UserID, AppointmentDate, AppointmentLocation) VALUES (@PetProfileID, @UserID, @AppointmentDate, @AppointmentLocation)";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@PetProfileID", PetProfileID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@AppointmentLocation", AppointmentLocation);
            cmd.ExecuteNonQuery();
        }

        // Method to update the appointment in the database
        public void UpdateToDB()
        {
            Data.GetPublicConnection cs = new Data.GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();

            string stm = "UPDATE Appointments SET PetProfileID = @PetProfileID, UserID = @UserID, AppointmentDate = @AppointmentDate, AppointmentLocation = @AppointmentLocation WHERE AppointmentID = @AppointmentID";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@PetProfileID", PetProfileID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@AppointmentLocation", AppointmentLocation);
            cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);
            cmd.ExecuteNonQuery();
        }

        // Method to retrieve a specific appointment by ID
        public static Appointment GetAppointmentById(int AppointmentID)
        {
            Data.GetPublicConnection cs = new Data.GetPublicConnection();
            using var con = new MySqlConnection(cs.cs);
            con.Open();
            string stm = "SELECT * FROM Appointments WHERE AppointmentID = @AppointmentID";
            MySqlCommand cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return new Appointment()
                {
                    AppointmentID = rdr.GetInt32("AppointmentID"),
                    PetProfileID = rdr.GetInt32("PetProfileID"),
                    UserID = rdr.GetInt32("UserID"),
                    AppointmentDate = rdr.GetDateTime("AppointmentDate"),
                    AppointmentLocation = rdr.GetString("AppointmentLocation")
                };
            }
            return null;
        }
    }
}
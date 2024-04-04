using MySql.Data.MySqlClient;
using api.Data;
namespace api.Models
{
    public class Pets
    {
        public int PetProfileID { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
        public bool Availability { get; set; }
        public string Species { get; set; }
        public bool CanVisit { get; set; }
        public int ReturnedCount { get; set; }
        public int FavoriteCount { get; set; }

        public static List<Movie> GetAllMovies() // method to retrieve movies from database
        {
            List<Movie> myMovies = new List<Movie>(); // initialize array to hold movies
            Database database = new Database(); // create new instance of database
            using var con = database.GetPublicConnection();
            con.Open(); // open databse connection
            string stm = "Select * from movies"; // sql statement to select everything from the movies table
            MySqlCommand cmd = new MySqlCommand(stm, con); 

            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            while(rdr.Read()) // iterate through table rows
            {
                myMovies.Add(new Movie() // create movie object for each row
                {
                    id = rdr.GetInt32("id"),
                    title = rdr.GetString("title"),
                    rating = rdr.GetInt32("movieRating"),
                    dateReleased = rdr.GetInt32("dateReleased"),
                    favorited = rdr.GetInt32("favorited") == 1, // convert tinyint to boolean, if 1 then it is true
                    deleted = rdr.GetInt32("deleted") == 1
                });
            }
            return myMovies; // return populated list
        }

        public void SaveToDB() // method to save the movies to the database
        {
            Database database = new Database(); // create a new instance of database
            using var con = database.GetPublicConnection(); 
            con.Open(); // open database connection
            string stm = "INSERT INTO movies (title, movieRating, dateReleased, favorited, deleted) VALUES (@Title, @MovieRating, @DateReleased, @Favorited, @Deleted)"; // sql command to insert a new movie
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@Title", title); // add parameters to the sql command
            cmd.Parameters.AddWithValue("@MovieRating", rating);
            cmd.Parameters.AddWithValue("@DateReleased", dateReleased);
            cmd.Parameters.AddWithValue("@Favorited", favorited ? 1 : 0); // convert boolean to tinyint, 1 for true, 0 for false
            cmd.Parameters.AddWithValue("@Deleted", deleted ? 1 : 0);
            cmd.ExecuteNonQuery(); // execute sql command
        }

        public void UpdateToDB() // method to update existing movie in database
        {
            Database database = new Database(); // create new instance database
            using var con = database.GetPublicConnection();
            con.Open(); // open db connection
    
            string stm = "UPDATE movies set title = @Title, movieRating = @MovieRating, dateReleased = @DateReleased, favorited = @Favorited, deleted = @Deleted WHERE id = @Id"; // sql command for updating a movie
            Console.WriteLine("SQL query: " + stm); // log the sql query to console for debugging
            Console.WriteLine("Parameters:"); // log parameters
            Console.WriteLine("@Title: " + title);
            Console.WriteLine("@MovieRating: " + rating);
            Console.WriteLine("@DateReleased: " + dateReleased);
            Console.WriteLine("@Favorited: " + (favorited ? 1 : 0));
            Console.WriteLine("@Deleted: " + (deleted ? 1 : 0));
    
            using var cmd = new MySqlCommand(stm, con); 
            cmd.Parameters.AddWithValue("@Title", title); // add parameters to sql command
            cmd.Parameters.AddWithValue("@MovieRating", rating);
            cmd.Parameters.AddWithValue("@DateReleased", dateReleased);
            cmd.Parameters.AddWithValue("@Favorited", favorited ? 1 : 0);
            cmd.Parameters.AddWithValue("@Deleted", deleted ? 1 : 0);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery(); // execute sql command
        }


        public static Movie GetMovieById(int id) // method to retrieve specific movie
        {
            Database database = new Database(); // create new instance of db
            using var con = database.GetPublicConnection();
            con.Open(); // open connection to db
            string stm = "select * from movies where id = @Id"; // sql statement to retrieve specific movie
            MySqlCommand cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@Id", id); // add id as parameter
            using MySqlDataReader rdr = cmd.ExecuteReader(); // execute sql command
            if(rdr.Read()) // check if movie is found
            {
                return new Movie() // construct and initialize new movie object
                {
                    id = rdr.GetInt32("id"),
                    title = rdr.GetString("title"),
                    rating = rdr.GetInt32("movieRating"),
                    dateReleased = rdr.GetInt32("dateReleased"),
                    favorited = rdr.GetInt32("favorited") == 1,
                    deleted = rdr.GetInt32("deleted") == 1
                };
            }
            return null; // if no movie is found
        }
    }
}
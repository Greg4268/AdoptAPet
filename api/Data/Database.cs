using Npgsql;

namespace api.Data
{
    public class GetPublicConnection
    {
        public string cs { get; set; }

        public GetPublicConnection()
        {
            // TODO: Move these to environment variables or configuration file
            string host = "aws-0-us-east-2.pooler.supabase.com";
            string port = "6543";
            string database = "postgres";
            string username = "postgres.xbkryesjloyqafbhzhdk";
            string password = "GreggorTravel762***";

            // Format the connection string according to Npgsql specifications
            cs = $"Host={host};Port={port};Database={database};Username={username};Password={password};";
        }
    }
}
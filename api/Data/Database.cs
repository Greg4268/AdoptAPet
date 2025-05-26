namespace api.Data
{
    public class GetPublicConnection
    {
        public string cs { get; set; }

        public GetPublicConnection()
        {
            // Get connection information from environment variables
            string host = Environment.GetEnvironmentVariable("DB_HOST") ?? throw new InvalidOperationException("DB_HOST environment variable is not set");
            string port = Environment.GetEnvironmentVariable("DB_PORT") ?? throw new InvalidOperationException("DB_PORT environment variable is not set");
            string database = Environment.GetEnvironmentVariable("DB_NAME") ?? throw new InvalidOperationException("DB_NAME environment variable is not set");
            string username = Environment.GetEnvironmentVariable("DB_USER") ?? throw new InvalidOperationException("DB_USER environment variable is not set");
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? throw new InvalidOperationException("DB_PASSWORD environment variable is not set");

            // Format the connection string according to Npgsql specifications
            cs = $"Host={host};Port={port};Database={database};Username={username};Password={password};";
        }
    }
}
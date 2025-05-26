namespace api.Data
{
    public class GetPublicConnection
    {
        public string cs { get; set; }

        private readonly string host = Environment.GetEnvironmentVariable("DB_HOST") ?? throw new InvalidOperationException("DB_HOST environment variable is not set");
        private readonly string port = Environment.GetEnvironmentVariable("DB_PORT") ?? throw new InvalidOperationException("DB_PORT environment variable is not set");
        private readonly string database = Environment.GetEnvironmentVariable("DB_NAME") ?? throw new InvalidOperationException("DB_NAME environment variable is not set");
        private readonly string username = Environment.GetEnvironmentVariable("DB_USER") ?? throw new InvalidOperationException("DB_USER environment variable is not set");
        private readonly string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? throw new InvalidOperationException("DB_PASSWORD environment variable is not set");

        public GetPublicConnection()
        {
            // Format the connection string according to Npgsql specifications
            cs = $"Host={host};Port={port};Database={database};Username={username};Password={password};";
        }
    }
}
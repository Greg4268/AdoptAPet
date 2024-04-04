using MySql.Data.MySqlClient;
namespace api.Data
{
    public class Database
    {
        private string server = "wftuqljwesiffol6.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
        private string port = "3306";
        private string username = "ra175v3sv9oai4bv";
        private string password = "v2xnjd2x4ctiwnqx";
        private string schema = "cve7x3dbdr8snfxd";

        public MySqlConnection GetPublicConnection()
        {
            string cs = $"server={server};user={username};database={schema};port={port};password={password}";
            using var con = new MySqlConnection(cs);
            return con;
        }
    }
}
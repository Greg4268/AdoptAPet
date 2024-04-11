using MySql.Data.MySqlClient;
namespace api.Data
{
    public class GetPublicConnection
    {
        public string cs { get; set; }

        public GetPublicConnection()
        {
            string server = "wftuqljwesiffol6.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            string port = "3306";
            string username = "ra175v3sv9oai4bv";
            string password = "v2xnjd2x4ctiwnqx";
            string schema = "cve7x3dbdr8snfxd";

            cs = $@"Server={server};Port={port};Database={schema};Uid={username};Pwd={password};";
        }
    }
}
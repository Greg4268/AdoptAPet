using MySql.Data.MySqlClient;
namespace api.Data
{
    public class GetPublicConnection
    {
        public string cs { get; set; }

        // public GetPublicConnection()
        // {
        //     string server = "wftuqljwesiffol6.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
        //     string port = "3306";
        //     string username = "ra175v3sv9oai4bv";
        //     string password = "v2xnjd2x4ctiwnqx";
        //     string schema = "cve7x3dbdr8snfxd";

        //     cs = $@"Server={server};Port={port};Database={schema};Uid={username};Pwd={password};";
        // }

        public GetPublicConnection()
        {
            string server = "gtizpe105piw2gfq.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            string port = "3306";
            string username = "w5i4w7ekeyslpbjq";
            string password = "ysuek5ioms9ei68f";
            string schema = "nqotxt0k05q7gq87";

            cs = $@"Server={server};Port={port};Database={schema};Uid={username};Pwd={password};";
        }
    }
}
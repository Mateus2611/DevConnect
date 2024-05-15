using System.Data;
using Npgsql;

namespace PlooCinema.ConsoleApplication 
{
    public static class EnvorinmentVariables
    {
        static EnvorinmentVariables()
        {
            Environment.SetEnvironmentVariable("HOST", "YOUR_HOST_HERE");
            Environment.SetEnvironmentVariable("USERNAME", "YOUR_USERNAME_HERE");
            Environment.SetEnvironmentVariable("PASSWORD", "YOUR_PASSWORD_HERE");
            Environment.SetEnvironmentVariable("DATABASE", "YOUR_DATABASE_HERE");
        }

        public static NpgsqlConnection OpenConnection()
        {
            var host = Environment.GetEnvironmentVariable("HOST");
            var user = Environment.GetEnvironmentVariable("USERNAME");
            var pass = Environment.GetEnvironmentVariable("PASSWORD");
            var data = Environment.GetEnvironmentVariable("DATABASE");

            var connString = $"Host={host};Username={user};Password={pass};Database={data};";

            var conn = new NpgsqlConnection(connString);
            
            return conn;
        }
    }
}
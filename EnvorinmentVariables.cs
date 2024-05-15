using System.Data;
using Npgsql;

namespace PlooCinema.ConsoleApplication 
{
    public static class EnvorinmentVariables
    {
        static EnvorinmentVariables()
        {
            Environment.SetEnvironmentVariable("HOST", "localhost");
            Environment.SetEnvironmentVariable("USERNAME", "postgres");
            Environment.SetEnvironmentVariable("PASSWORD", "ploo123");
            Environment.SetEnvironmentVariable("DATABASE", "ploocinema");
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
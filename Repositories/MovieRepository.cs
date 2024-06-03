using System.Text.Json;
using Microsoft.VisualBasic;
using PlooCinema.ConsoleApplication.Model;
using Npgsql;
using System.Data;

namespace PlooCinema.ConsoleApplication.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> SearchAll();
        void Create(Movie addMovie);
        IEnumerable<Movie> Search(string name);
    }

    public class MovieRepositoryJson : IMovieRepository
    {
        public MovieRepositoryJson()
        {
            var infoFile = new FileInfo(fileMovie);

            if (!infoFile.Exists)
            {
                File.Create(fileMovie).Close();
                File.WriteAllText(fileMovie, "[]");
            }
        }

        string fileMovie = "FileMovie.json";
        
        public void Create(Movie addMovie)
        {
            var getAllMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getAllMovie) ?? [];

            var newJsonList = jsonMovie.Append(addMovie);

            var listToJson = JsonSerializer.Serialize<IEnumerable<Movie>>(newJsonList);
            File.WriteAllText(fileMovie, listToJson);
        }

        public IEnumerable<Movie> SearchAll()
        {
            var getJsonMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];
            
            return jsonMovie;
        }

        public IEnumerable<Movie> Search(string name)
        {
            var getJsonMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

            var queryNameMovie = jsonMovie
                .Where(item => item.Name.ToLower().Contains(name));

            return queryNameMovie;
        }

    }

    public class MovieRepositoryPostgres : IMovieRepository
    {
        public MovieRepositoryPostgres()
        {
            DotNetEnv.Env.Load();

            var host = Environment.GetEnvironmentVariable("HOST");
            var user = Environment.GetEnvironmentVariable("USERNAME");
            var pass = Environment.GetEnvironmentVariable("PASSWORD");
            var data = Environment.GetEnvironmentVariable("DATABASE");

            var connString = $"Host={host};Username={user};Password={pass};Database={data};";
            
            Connection = new NpgsqlConnection(connString);
        }

        private NpgsqlConnection Connection { get; set; }

        public void Create(Movie addMovie)
        {
            int minutes = (int)addMovie.Duration.TotalMinutes;

            Connection.Open();

            var cmd = new NpgsqlCommand("INSERT INTO movie (name, genre, description, duration_minutes, release) VALUES (@name, @genre, @description, @duration_minutes, @release)", Connection);

            cmd.Parameters.AddWithValue("name", addMovie.Name);
            cmd.Parameters.AddWithValue("genre", addMovie.Genre);
            cmd.Parameters.AddWithValue("description", addMovie.Description);
            cmd.Parameters.AddWithValue("duration_minutes", minutes);
            cmd.Parameters.AddWithValue("release", addMovie.Release);

            cmd.ExecuteNonQuery();

            Connection.Close();
        }

        public IEnumerable<Movie> Search(string nameSearch)
        {           
            List<Movie> queryMovies = [];

            Connection.Open();

            var cmd = new NpgsqlCommand("SELECT * FROM movie WHERE ( name ) ILIKE '%' || @name || '%'", Connection);
            {
                cmd.Parameters.AddWithValue("name", nameSearch);
            }

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("release"));
                DateOnly date = DateOnly.FromDateTime(dateTime);

                TimeSpan duration = TimeSpan.FromMinutes(reader.GetInt32(reader.GetOrdinal("duration_minutes")));

                var movie = new Movie(reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("genre")), duration, date, reader.GetString(reader.GetOrdinal("description")));

                queryMovies.Add(movie);
            }
            
            Connection.Close();

            return queryMovies.AsEnumerable();
        }

        public IEnumerable<Movie> SearchAll()
        {
            List<Movie> moviesqueryMovies = [];

            Connection.Open();

            var cmd = new NpgsqlCommand("SELECT * FROM movie", Connection);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("release"));
                DateOnly date = DateOnly.FromDateTime(dateTime);

                TimeSpan duration = TimeSpan.FromMinutes(reader.GetInt32(reader.GetOrdinal("duration_minutes")));

                var movie = new Movie(reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("genre")), duration, date, reader.GetString(reader.GetOrdinal("description")));

                moviesqueryMovies.Add(movie);
            }
            
            Connection.Close();

            return moviesqueryMovies.AsEnumerable();
        }
    }
}
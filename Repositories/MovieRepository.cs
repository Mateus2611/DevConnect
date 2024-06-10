using System.Text.Json;
using Microsoft.VisualBasic;
using PlooCinema.ConsoleApplication.Model;
using Npgsql;
using System.Data;

namespace PlooCinema.ConsoleApplication.Repositories
{
    public interface IMovieRepository
    {
        void Create(Movie addMovie);
        IEnumerable<Movie> SearchAll();
        IEnumerable<Movie> Search(string name);
        void Update(int idMovie, Movie updateMovie);
        void Delete(int idMovie);
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

            addMovie.Id = jsonMovie.Count() + 1;

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
                .Where(item => item.Name.Contains(name));

            return queryNameMovie;
        }

        public void Update(int idMovie, Movie updateMovie)
        {
            var getJsonMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

            var queryMovie = jsonMovie
                .SingleOrDefault(item => item.Id == idMovie);

            if (queryMovie != null)
            {
                queryMovie.Name = updateMovie.Name;
                queryMovie.Genre = updateMovie.Genre;
                queryMovie.Duration = updateMovie.Duration;
                queryMovie.Release = updateMovie.Release;
                queryMovie.Description = updateMovie.Description;
            }

            var newJson = JsonSerializer.Serialize(jsonMovie);
            File.WriteAllText(fileMovie, newJson);
        }

        public void Delete(int idMovie)
        {
            var getJsonMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<List<Movie>>(getJsonMovie) ?? [];

            var queryMovie = jsonMovie
                .SingleOrDefault(item => item.Id == idMovie);

            if (queryMovie != null)
                jsonMovie.Remove(queryMovie);

            var newJson = JsonSerializer.Serialize(jsonMovie);
            File.WriteAllText(fileMovie, newJson);
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

            cmd.Parameters.AddWithValue("name", nameSearch);

            var reader = cmd.ExecuteReader();

            while (reader.HasRows && reader.Read())
            {
                DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("release"));
                DateOnly date = DateOnly.FromDateTime(dateTime);

                TimeSpan duration = TimeSpan.FromMinutes(reader.GetInt32(reader.GetOrdinal("duration_minutes")));

                var movie = new Movie(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("genre")), duration, date, reader.GetString(reader.GetOrdinal("description")));

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

            while (reader.HasRows && reader.Read())
            {
                DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("release"));
                DateOnly date = DateOnly.FromDateTime(dateTime);

                TimeSpan duration = TimeSpan.FromMinutes(reader.GetInt32(reader.GetOrdinal("duration_minutes")));

                var movie = new Movie(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("genre")), duration, date, reader.GetString(reader.GetOrdinal("description")));

                moviesqueryMovies.Add(movie);
            }

            Connection.Close();

            return moviesqueryMovies.AsEnumerable();
        }

        public void Update(int idMovie, Movie updateMovie)
        {
            int minutes = (int)updateMovie.Duration.TotalMinutes;

            Connection.Open();

            var cmd = new NpgsqlCommand("UPDATE movie SET name = @name, genre = @genre, description = @description, duration_minutes = @duration_minutes, release = @release WHERE id = @id", Connection);

            cmd.Parameters.AddWithValue("id", idMovie);
            cmd.Parameters.AddWithValue("name", updateMovie.Name);
            cmd.Parameters.AddWithValue("genre", updateMovie.Genre);
            cmd.Parameters.AddWithValue("description",updateMovie.Description);
            cmd.Parameters.AddWithValue("duration_minutes", minutes);
            cmd.Parameters.AddWithValue("release", updateMovie.Release);

            cmd.ExecuteNonQuery();

            Connection.Close();
        }

        public void Delete(int idMovie)
        {
            Connection.Open();

            var cmd = new NpgsqlCommand("DELETE FROM movie WHERE id = @id", Connection);

            cmd.Parameters.AddWithValue("id", idMovie);

            cmd.ExecuteNonQuery();

            Connection.Close();
        }
    }
}
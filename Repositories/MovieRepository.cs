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

    public class MovieRepository : IMovieRepository
    {
        public void Create(Movie addMovie)
        {
            int minutes = (int)addMovie.Duration.TotalMinutes;

            var conn = EnvorinmentVariables.OpenConnection();
            conn.Open();

            var cmd = new NpgsqlCommand("INSERT INTO movie (name, genre, description, duration_minutes, release) VALUES (@name, @genre, @description, @duration_minutes, @release)", conn);

            cmd.Parameters.AddWithValue("name", addMovie.Name);
            cmd.Parameters.AddWithValue("genre", addMovie.Genre);
            cmd.Parameters.AddWithValue("description", addMovie.Description);
            cmd.Parameters.AddWithValue("duration_minutes", minutes);
            cmd.Parameters.AddWithValue("release", addMovie.Release);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public IEnumerable<Movie> Search(string nameSearch)
        {           
            List<Movie> queryMovies = new();

            var conn = EnvorinmentVariables.OpenConnection();
            conn.Open();

            var cmd = new NpgsqlCommand("SELECT * FROM movie WHERE ( name ) ILIKE '%' || @name || '%'", conn);
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
            
            conn.Close();

            return queryMovies.AsEnumerable();
        }

        public IEnumerable<Movie> SearchAll()
        {
            List<Movie> moviesqueryMovies = new();

            var conn = EnvorinmentVariables.OpenConnection();
            conn.Open();

            var cmd = new NpgsqlCommand("SELECT * FROM movie", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("release"));
                DateOnly date = DateOnly.FromDateTime(dateTime);

                TimeSpan duration = TimeSpan.FromMinutes(reader.GetInt32(reader.GetOrdinal("duration_minutes")));

                var movie = new Movie(reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("genre")), duration, date, reader.GetString(reader.GetOrdinal("description")));

                moviesqueryMovies.Add(movie);
            }
            
            conn.Close();

            return moviesqueryMovies.AsEnumerable();
        }
    }
}
using System.Text.Json;
using Microsoft.VisualBasic;
using PlooCinema.ConsoleApplication.Model;

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
        public MovieRepository()
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
            var getJsonMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

            var newjsonList = jsonMovie.Append(addMovie);

            var listToJson = JsonSerializer.Serialize<IEnumerable<Movie>>(newjsonList);
            File.WriteAllText(fileMovie, listToJson);
        }

        public IEnumerable<Movie> Search(string name)
        {
            var getJsonMovie = File.ReadAllText(fileMovie);

            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

            var queryNameMovie = jsonMovie
                .Where(item => item.Name.ToLower().Contains(name));

            return queryNameMovie;
        }

        public IEnumerable<Movie> SearchAll()
        {
            var getJsonMovie = File.ReadAllText(fileMovie);

            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie) ?? [];

            return jsonMovie;
        }
    }
}
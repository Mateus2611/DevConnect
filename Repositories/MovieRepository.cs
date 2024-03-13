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

        List<Movie> listFilm = new List<Movie>();
        string fileMovie = "FileMovie.json";


        public void Create(Movie addMovie)
        {
            var getJsonMovie = File.ReadAllText(fileMovie);
            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie);

            foreach (var item in jsonMovie)
            {
                if (!listFilm.Contains(item))
                    listFilm.Add(item);
            }

            listFilm.Add(addMovie);
            var listToJson = JsonSerializer.Serialize<IEnumerable<Movie>>(listFilm);
            File.WriteAllText(fileMovie, listToJson);
        }

        public IEnumerable<Movie> Search(string name)
        {
            var getJsonMovie = File.ReadAllText(fileMovie);

            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie);

            var queryNameMovie = jsonMovie
                .Where(item => item.Name.ToLower().Contains(name));

            return queryNameMovie;
        }

        public IEnumerable<Movie> SearchAll()
        {
            var getJsonMovie = File.ReadAllText(fileMovie);

            var jsonMovie = JsonSerializer.Deserialize<IEnumerable<Movie>>(getJsonMovie);

            return jsonMovie;
        }
    }
}
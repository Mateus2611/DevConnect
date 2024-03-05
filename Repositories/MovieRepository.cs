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

        
        public void Create(Movie addMovie)
        {
            listFilm.Add(addMovie);
        }

        public IEnumerable<Movie> Search(string name)
        {
            var queryNameMovie = listFilm
                .Where(item => item.Name.ToLower().Contains(name));

            return queryNameMovie;
        }

        public IEnumerable<Movie> SearchAll()
        {
            return listFilm;
        }
    }
}
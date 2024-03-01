using PlooCinema.ConsoleApplication.Model;

namespace PlooCinema.ConsoleApplication.Repositories
{
    public interface IMovieRepository
    {
        void Create(Movie addMovie);
        string Search(string name);
    }

    public class MovieRepository : IMovieRepository
    {

        List<Movie> listFilm = new List<Movie>();

        public void Create(Movie addMovie)
        {
            listFilm.Add(addMovie);
        }

        public string Search(string name)
        {
            var queryNameMovie = listFilm
                .Where(item => item.Name.ToLower().Contains(name));

            foreach (Movie item in queryNameMovie)
            {
                Console.WriteLine($"Nome: {item.Name}, Duração: {item.Duration}, Lançamento: {item.Release}, Descrição: {item.Description}\n");
            }

            if (!listFilm.Any(item => item.Name.ToLower().Contains(name)))
                return "Nenhum filme com esse nome encontrado.";

            return null;
        }
    }
}
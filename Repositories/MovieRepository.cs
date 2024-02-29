using PlooCinema.ConsoleApplication.Model;

namespace PlooCinema.ConsoleApplication.Repositories
{
    public interface IMovieRepository
    {
        void Create(Movie addMovie);
        string Search(Movie movie, string name);
    }

    public class MovieRepository : IMovieRepository
    {

        readonly List<Movie> listFilm = new List<Movie>();

        public void Create(Movie addMovie)
        {
            listFilm.Add(addMovie);
        }

        public string Search(Movie movie, string name)
        {
            do
            {
                Console.WriteLine("Informe o nome do filme:");
                name = Console.ReadLine()?.ToLower() ?? "";

                if (name == "")
                    break;

                var queryNameMovie = listFilm
                    .Where(item => item.Name.ToLower().Contains(name));

                foreach (Movie item in queryNameMovie)
                {
                    return $"Nome: {movie.Name}, Duração: {movie.Duration}, Lançamento: {movie.Release}, Descrição: {movie.Description}";
                }
                
            } while (true);

            return "Nenhum filme encontrado com o nome inserido";
        }
    }
}
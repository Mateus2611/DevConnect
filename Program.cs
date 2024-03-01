using PlooCinema.ConsoleApplication.Model;
using PlooCinema.ConsoleApplication.Repositories;

IMovieRepository movieRepository = new MovieRepository();
string nameMovie;

Movie movie1 = new("Senhor dos Aneis", TimeSpan.FromHours(3.35), new DateOnly(2005, 11, 19), "Neste filme o senhor bolseiro pede ajuda de seu amigo para ajuda-lo a proteger seu anel.");

Movie movie2 = new("O Hobbit e os Cinco Exércitos", TimeSpan.FromHours(2.90), new DateOnly(2010, 06, 05), "Nesta jornada Bilbo bolseiro terá que utilizar seu anel do poder para enfrentar um exercito de ENORMES orcs.");

Movie movie3 = new("Harry Potter e a Pedra \"Filosofal\"", TimeSpan.FromHours(1.57), new DateOnly(2013, 09, 08), "Harry descobre que seu professor está em busca de uma poderosa pedra proibida escondida dentro do castelo.");

movieRepository.Create(movie1);
movieRepository.Create(movie2);
movieRepository.Create(movie3);

do
{
    Console.WriteLine("Informe o nome do filme:");
    nameMovie = Console.ReadLine()?.ToLower() ?? "";

    if (nameMovie == "")
        break;

    string searchResult = movieRepository.Search(nameMovie);

    Console.WriteLine(searchResult);

} while(true);

Console.ReadKey();


// () = Receber e passar parâmetros, inicializar arrow functions
// {} = Bloco de código
// <> = Generic
// [] = Declaração de array e nova syntax de construtor de coleções
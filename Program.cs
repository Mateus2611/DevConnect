using System.Reflection;
using PlooCinema.ConsoleApplication.Model;
using PlooCinema.ConsoleApplication.Repositories;

IMovieRepository movieRepository = new MovieRepository();
string nameMovie;

Movie movie1 = new("Senhor dos Aneis", TimeSpan.FromHours(3.35), new DateOnly(2005, 11, 19), "Neste filme o senhor bolseiro pede ajuda de seu amigo para ajuda-lo a proteger seu anel.");

Movie movie2 = new("O Hobbit e os Cinco Exércitos", TimeSpan.FromHours(2.90), new DateOnly(2010, 06, 05), "Nesta jornada Bilbo bolseiro terá que utilizar seu anel do poder para enfrentar um exercito de ENORMES orcs.");

Movie movie3 = new("Harry Potter e a Pedra \"Filosofal\"", TimeSpan.FromHours(1.57), new DateOnly(2013, 09, 08), "Harry descobre que seu professor está em busca de uma poderosa pedra proibida escondida dentro do castelo.");

// movieRepository.Create(movie1);
// movieRepository.Create(movie2);
// movieRepository.Create(movie3);

do
{
    Console.WriteLine("Selecione uma das opções abaixo: \n1 - Exibir todos os filmes \n2- Pesquisar filmes \n3- Inserir filme novo \n0 - Sair\n");
    int nameSearch = Convert.ToInt32(Console.ReadLine());

    if (nameSearch == 0)
        break;

    switch (nameSearch)
    {
        case 1:
            AllMovies();
            break;
        case 2:
            SearchMovie();
            break;
        case 3:
            CreateMovie();
            break;

    }
} while (true);

void CreateMovie()
{
    DateOnly date;
    TimeSpan time;

    Console.WriteLine("Informe o nome do filme: ");
    string title = Console.ReadLine() ?? "No title";

    Console.WriteLine("\nInforme a duração do filme: ");

    try
    {
        time = TimeSpan.FromHours(Convert.ToDouble(Console.ReadLine()));
    } catch
    {
        time = TimeSpan.Zero;
    }

    Console.WriteLine("\nInforme a data de lançamento do filme (mm/dd/yyyy): ");
    string getDate = Console.ReadLine() ?? "00/00/00";
    DateOnly.TryParse(getDate, out date);

    Console.WriteLine("\nInforme a descrição do filme: ");
    string description = Console.ReadLine() ?? "Filme sem descrição";

    Movie addMovie = new(title, time, date, description);

    movieRepository.Create(addMovie);
}

void AllMovies()
{
    var searchResult = movieRepository.SearchAll();

    foreach (Movie item in searchResult)
    {
        Console.WriteLine(item.ToString());
    }
}

void SearchMovie()
{
    Console.WriteLine("Informe o nome do filme:");
    nameMovie = Console.ReadLine()?.ToLower() ?? "";

    var searchResult = movieRepository.Search(nameMovie);

    foreach (Movie item in searchResult)
    {
        // Console.WriteLine("Nome: {0}, Duração: {1}, Lançamento: {2}, Descrição: {3}\n", item.Name, item.Duration, item.Release, item.Description);
        Console.WriteLine(item.ToString());
    }

    if (searchResult.Count() == 0)
        Console.WriteLine("Filme não encontrado");
}


// () = Receber e passar parâmetros, inicializar arrow functions
// {} = Bloco de código
// <> = Generic
// [] = Declaração de array e nova syntax de construtor de coleções
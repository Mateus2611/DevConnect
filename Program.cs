using System.Reflection;
using System.Runtime.InteropServices;
using PlooCinema.ConsoleApplication.Model;
using PlooCinema.ConsoleApplication.Repositories;

IMovieRepository movieRepository = new MovieRepository();
string nameMovie;

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

    Console.WriteLine("\nInforme o gênero do filme: ");
    string genre = Console.ReadLine() ?? "No genre";

    Console.WriteLine("\nInforme a duração do filme: ");

    try
    {
        time = TimeSpan.FromHours(Convert.ToDouble(Console.ReadLine()));
    } catch
    {
        time = TimeSpan.Zero;
    }

    Console.WriteLine("\nInforme a data de lançamento do filme (mm/dd/yyyy): ");
    string getDate = Console.ReadLine() ?? DateTime.Now.ToString("yyyy/dd/MM");
    DateOnly.TryParse(getDate, out date);

    Console.WriteLine("\nInforme a descrição do filme: ");
    string description = Console.ReadLine() ?? "Filme sem descrição";

    Movie addMovie = new(title, genre, time, date, description);

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
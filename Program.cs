using System.Reflection;
using System.Runtime.InteropServices;
using PlooCinema.ConsoleApplication.Model;
using PlooCinema.ConsoleApplication.Repositories;

IMovieRepository movieRepository = new MovieRepositoryPostgres();
string nameMovie;

IMovieRepository movieRepositoryJson = new MovieRepositoryJson();

do
{
    Console.WriteLine("Selecione uma das opções abaixo: \n1 - Exibir todos os filmes \n2- Pesquisar filmes \n3- Inserir filme novo \n4- Atualizar filme \n5- Deletar Filme \n0 - Sair\n");
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
        case 4:
            UpdateMovieJson();
            break;
        case 5:
            DeleteMovieJson();
            break;

    }
} while (true);

void CreateMovie()
{
    DateOnly date;
    TimeSpan time;

    Console.WriteLine("Informe o nome do filme: ");
    string title = Console.ReadLine() ?? "";

    Console.WriteLine("\nInforme o gênero do filme: ");
    string genre = Console.ReadLine() ?? "";

    Console.WriteLine("\nInforme a duração do filme: ");

    try
    {
        time = TimeSpan.FromHours(Convert.ToDouble(Console.ReadLine()));
    } catch
    {
        time = TimeSpan.Zero;
    }

    Console.WriteLine("\nInforme a data de lançamento do filme (mm/dd/yyyy): ");
    string getDate = Console.ReadLine() ?? DateTime.Now.Date.ToString("yyyy/MM/dd");
    DateOnly.TryParse(getDate, out date);

    Console.WriteLine("\nInforme a descrição do filme: ");
    string description = Console.ReadLine() ?? "";

    Movie addMovie = new(title, genre, time, date, description);

    movieRepository.Create(addMovie);
    movieRepositoryJson.Create(addMovie);
}

void AllMovies()
{
    Console.WriteLine("Postgres consult:");
    foreach (Movie item in movieRepository.SearchAll())
    {
        Console.WriteLine(item.ToString());
    }

    Console.WriteLine("Json consult:");
    foreach (Movie item in movieRepositoryJson.SearchAll())
    {
        Console.WriteLine(item.ToString());
    }
}

void SearchMovie()
{
    Console.WriteLine("Informe o nome do filme:");
    nameMovie = Console.ReadLine()?.ToLower() ?? "";

    var searchResult = movieRepository.Search(nameMovie);
    var searchJson = movieRepositoryJson.Search(nameMovie);

    Console.WriteLine("Postgres Consult: ");
    foreach (Movie item in searchResult)
    {
        Console.WriteLine(item.ToString());
    }

    Console.WriteLine("Json Consult: ");
    foreach (Movie item in searchJson)
    {
        Console.WriteLine(item.ToString());
    }

    if (searchResult.Count() == 0)
        Console.WriteLine("Filme não encontrado");

    if (searchJson.Count() == 0)
        Console.WriteLine("Filme não encontrado.");
}

void UpdateMovieJson()
{

    DateOnly date;
    TimeSpan time;

    Console.WriteLine("(JSON) Informe o ID do filme:");
    int idJson = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("(POSTGRES) Informe o ID do filme:");
    int idPostgres = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Informe o nome do filme: ");
    string title = Console.ReadLine() ?? "";

    Console.WriteLine("\nInforme o gênero do filme: ");
    string genre = Console.ReadLine() ?? "";

    Console.WriteLine("\nInforme a duração do filme: ");

    try
    {
        time = TimeSpan.FromHours(Convert.ToDouble(Console.ReadLine()));
    } catch
    {
        time = TimeSpan.Zero;
    }

    Console.WriteLine("\nInforme a data de lançamento do filme (mm/dd/yyyy): ");
    string getDate = Console.ReadLine() ?? DateTime.Now.Date.ToString("yyyy/MM/dd");
    DateOnly.TryParse(getDate, out date);

    Console.WriteLine("\nInforme a descrição do filme: ");
    string description = Console.ReadLine() ?? "";

    Movie addMovie = new(title, genre, time, date, description);

    movieRepositoryJson.Update(idJson, addMovie);
    movieRepository.Update(idPostgres, addMovie);
}

void DeleteMovieJson()
{
    Console.WriteLine("(JSON) Informe o ID do filme:");
    int idJson = Convert.ToInt32(Console.ReadLine());
    
    Console.WriteLine("(POSTGRES) Informe o ID do filme:");
    int idPostgres = Convert.ToInt32(Console.ReadLine());

    movieRepositoryJson.Delete(idJson);
    movieRepository.Delete(idPostgres);
}
// () = Receber e passar parâmetros, inicializar arrow functions
// {} = Bloco de código
// <> = Generic
// [] = Declaração de array e nova syntax de construtor de coleções
using PlooCinema.ConsoleApplication.Model;

List<Movie> listFilm = [];
string movieName = "";

Movie movie1 = new("Sei la", TimeSpan.FromHours(2.45), new DateOnly(2005, 11, 18), "Não sei o que escrever.");
Movie movie2 = new("Outro Filme", TimeSpan.FromHours(3.06), new DateOnly(1999, 10, 20), "Texto do outro filme.");
Movie movie3 = new("Mais um filme", TimeSpan.FromHours(3), new DateOnly(1985, 1, 1), "mais um texto de outro filme.");

listFilm.Add(movie1);
listFilm.Add(movie2);
listFilm.Add(movie3);

// Eatapa 2: Pesquisar filmes por nome (utilzando LINQ).
// 1 - Criar um campo onde podemos passar o nome via console.
// 2 - Criar variável de consulta
// 3 - Executar a consulta

do
{
    Console.Write("Informe o nome do filme: ");
    movieName = Console.ReadLine()?.ToLower() ?? "";

    if (movieName == "")
        break;

    var queryNameMovie = listFilm
        .Where(item => item.Name.ToLower().Contains(movieName));
        // from item in listFilm
        // where item.Name == movieName
        // select item;

    foreach (Movie movie in queryNameMovie)
    {
        Console.WriteLine($"Nome: {movie.Name}, Duração: {movie.Duration}, Lançamento: {movie.Release}, Descrição: {movie.Description}");
    }

} while (true);


Console.ReadKey();


// () = Receber e passar parâmetros, inicializar arrow functions
// {} = Bloco de código
// <> = Generic
// [] = Declaração de array e nova syntax de construtor de coleções
// 1 - CRIAR UM MODELO PARA ARMAZENAR OS FILMES
// 2 - ADICIONAR ITENS À LISTA
// 3 - EXIBIR OS FILMES NO CONSOLE.
// OBS: UTILIZAR MÉTODO LIST

//Criando um tipo complexo (classes)
List<Movie> listFilm = new List<Movie>();
// List<Movie> ListFilm = [];

Movie movie1 = new("Sei la", TimeSpan.FromHours(2.45), new DateOnly(2005, 11, 18), "Não sei o que escrever.");
Movie movie2 = new("Outro Filme", TimeSpan.FromHours(3.06), new DateOnly(1999, 10, 20), "Texto do outro filme.");
Movie movie3 = new("Mais um filme", TimeSpan.FromHours(3), new DateOnly(1985, 1, 1), "mais um texto de outro filme.");

// Movie movie1 = new("Sei la", 2.45M, new DateOnly(2005, 11, 18), "Não sei o que escrever.");
// Movie movie2 = new("Outro Filme", 2.85M, new DateOnly(1999, 10, 20), "Texto do outro filme.");
// Movie movie3 = new("Mais um filme", 3M, new DateOnly(1985, 1, 1), "mais um texto de outro filme.");

listFilm.Add(movie1);
listFilm.Add(movie2);
listFilm.Add(movie3);

foreach (Movie movie in listFilm)
{
    Console.WriteLine($"Nome: {movie.Name}, Duração em minutos: {movie.Duration}, Lançamento: {movie.Release}, Descrição: {movie.Description}");
}

Console.ReadKey();

class Movie
{
    public string Name { get; set; }
    public TimeSpan Duration { get; set; }
    public DateOnly Release { get; set; }
    public string Description { get; set; }

    public Movie(string name, TimeSpan duration, DateOnly release, string description)
    {
        Name = name;
        Duration = duration;
        Release = release;
        Description = description;
    }

    // int TimeToMinutes(TimeSpan duration)
    // {
    //     int minutes;
    //     Math.Round(duration, 2);
    //     minutes = Convert.ToInt32(duration *= 60);
    //     return minutes;
    // }

}


// () = Receber e passar parâmetros, inicializar arrow functions
// {} = Bloco de código
// <> = Generic
// [] = Declaração de array e noca syntaxe de construtor de coleções
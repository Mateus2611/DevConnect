// 1 - CRIAR UM MODELO PARA ARMAZENAR OS FILMES
// 2 - ADICIONAR ITENS À LISTA
// 3 - EXIBIR OS FILMES NO CONSOLE.
// OBS: UTILIZAR MÉTODO LIST

List<Movie> ListFilm = new List<Movie>();
// List<Movie> ListFilm = [];

Movie movie1 = new("Sei la", 2.45F, new DateOnly(2005, 11, 18), "Não sei o que escrever.");
Movie movie2 = new("Outro Filme", 2.85F, new DateOnly(1999, 10, 20), "Texto do outro filme.");
Movie movie3 = new("Mais um filme", 3F, new DateOnly(1985, 1, 1), "mais um texto de outro filme.");

ListFilm.Add(movie1);
ListFilm.Add(movie2);
ListFilm.Add(movie3);

foreach (Movie movie in ListFilm)
{
    Console.WriteLine($"Nome: {movie.Name}, Duração em minutos: {movie.Duration}, Lançamento: {movie.Release}, Descrição: {movie.Description}");
}



class Movie
{
    public string Name { get; set; }
    public float Duration { get; set; }
    public DateOnly Release { get; set; }
    public string Description { get; set; }

    public Movie(string name, float duration, DateOnly release, string description)
    {
        Name = name;
        Duration = TimeToMinutes(duration);
        Release = release;
        Description = description;
    }

    static int TimeToMinutes(float Duration)
    {
        int minutes;
        Math.Round(Duration, 2);
        minutes = Convert.ToInt32(Duration *= 60);
        return minutes;
    }

}

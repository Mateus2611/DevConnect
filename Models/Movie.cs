using System.Text.Json.Serialization;

namespace PlooCinema.ConsoleApplication.Model
{
    public class Movie(string name, string genre, TimeSpan duration, DateOnly release, string description)
    {
        public string Name { get; set; } = name;
        public string Genre { get; set; } = genre;
        public TimeSpan Duration { get; set; } = duration;
        public DateOnly Release { get; set; } = release;
        public string Description { get; set; } = description;

        public override string ToString()
        {
            return $"Nome: {Name}, Gênero: {Genre}, Duração: {Duration}, Lançamento: {Release}, Descrição: {Description}\n";
        }
    }
}
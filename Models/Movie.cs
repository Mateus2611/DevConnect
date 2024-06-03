using System.Text.Json.Serialization;

namespace PlooCinema.ConsoleApplication.Model
{
    public class Movie
    {
        public Movie(string name, string genre, TimeSpan duration, DateOnly release, string description)
        {
            Name = name;
            Genre = genre;
            Duration = duration;
            Release = release;
            Description = description;
        }

        private string _name;
        public string Name
        {
            get => _name.ToUpper();
            set
            {
                if (value == "" || value == null)
                    throw new ArgumentException("Informe o titulo do filme.");

                _name = value.ToLower();
            }
        }
        private string _genre;
        public string Genre
        {
            get => _genre.ToUpper();
            set
            {
                if (value == "" || value == null)
                    throw new ArgumentException("Informe o genero do filme.");

                _genre = value.ToLower();
            }
        }
        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get => _duration;
            set
            {
                if (value == TimeSpan.Zero)
                    throw new ArgumentException("Informe a duração do filme.");

                _duration = value;
            }
        }
        private DateOnly _release;
        public DateOnly Release
        {
            get => _release;
            set => _release = value;
        }
        private string _description;
        public string Description
        {
            get => _description.ToUpper();
            set
            {
                if (value == "" || value == null)
                    throw new ArgumentException("Informe a descrição do filme.");

                _description = value.ToLower();
            }
        }

        public override string ToString()
        {
            return $"Nome: {Name}, Gênero: {Genre}, Duração: {Duration}, Lançamento: {Release}, Descrição: {Description}\n";
        }
    }
}
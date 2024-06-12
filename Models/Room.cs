using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetEnv;
using Npgsql.Internal;
using PlooCinema.ConsoleApplication.Model;

namespace DevConnect.Models
{
    public class Room
    {
        public int Id { get; set; }
        private int _roomNumber { get; set; }
        public int RoomNumber
        {
            get => _roomNumber;
            set => _roomNumber = value;
        }
        private int _seats { get; set; }
        public int Seats
        {
            get => _seats;
            set
            {
                if (value < 0)
                    throw new ArgumentException("As cadeiras não podem ser de valor negativo");

                _seats = value;
            }
        }
        private List<Movie> _upComingSessions { get; set; }
        public List<Movie> UpComingSessions
        {
            get => _upComingSessions;
            set
            {
                if (value.Count == 0 || value == null)
                    throw new ArgumentException("Deve-se ter pelo menos um filme na sessão");

                foreach (Movie item in value)
                {
                    item.Duration += TimeSpan.FromMinutes(30);
                }
            }
        }

        public Movie BookRoom(Movie movie, TimeSpan session)
        {
            var query = _upComingSessions
                .SingleOrDefault(item => item.Name == movie.Name && item.Duration == session) ?? throw new ArgumentException("Sessão não encontrada.");
            
            return query;
        }

        public override string ToString()
        {
            var query = _upComingSessions
                .Select(item => item.ToString());

            string queryItens = string.Join("\n", query);

            return $"ID da Sala: {Id} \nNúmero da sala: {_roomNumber} \nQuantidade de assentos: {_seats} \n Sessões: {queryItens}";

        }
    }
}
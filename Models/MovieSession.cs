using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.ConsoleApplication.Model;

namespace DevConnect.Models
{
    public class MovieSession
    {
        private Movie _watchMovie;
        public Movie WatchMovie 
        {
            get => _watchMovie;
            set
            {
                if( value == null )
                    throw new ArgumentNullException("Informe o filme da sessão.");
                
                _watchMovie = value;
            }
        }
        private Room  _movieRoom { get; set;}
        public Room MovieRoom
        {
            get => _movieRoom;
            set
            {
                if( value == null )
                    throw new ArgumentException("Informe a sala da sessão.");
                
                _movieRoom = value;
            }
        }
        private DateTime _startMovie { get; set; }
        public DateTime StartMovie
        {
            get => _startMovie;
            set => _startMovie = value;
        }
        private int _seatsAvailable { get; set; }
        public int SeatsAvailable
        {
            get => _seatsAvailable;
            set
            {
                if( value < 0)
                    throw new ArgumentException("A quantidade de assentos não pode ser negativa.");
                
                _seatsAvailable = value;
            }
        }

        public int ReserveSeats(int seats)
        {
            if (_seatsAvailable < 0 || (_seatsAvailable - seats) < 0)
                throw new ArgumentException("Sem assentos disponíveis para essa sessão.");
            
            return _seatsAvailable -= seats;
        }

        public override string ToString()
        {
            return $"Filme: {_watchMovie.ToString()} \nSala: {_movieRoom} \nInicio do Filme: {_startMovie} \nAssentos Disponíveis: {_seatsAvailable}";
        }
    }
}
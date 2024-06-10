using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlooCinema.ConsoleApplication.Model;

namespace DevConnect.Models
{
    public class Room
    {
        public int Id { get; set; }
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
                if ( value.Count == 0 )
                    throw new ArgumentException("Deve-se ter pelo menos um filme na sessão");

                _upComingSessions = value;
            }
        }
    }
}
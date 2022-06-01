using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.model
{
    public class Ticket
    {
        public int Id { get; set; }
        public Double Price { get; set; }
        public int Seat { get; set; }
        public Ride Ride { get; set; }
        public Client Client { get; set; }

        public Ticket() { }

        public Ticket(int id, double price, int seat, Ride ride,Client client)
        {
            Id = id;
            Price = price;
            Seat = seat;
            Ride = ride;
            Client = client;
        }

    }
}

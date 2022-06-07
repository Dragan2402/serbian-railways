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
        
        public int PassengerCar { get; set; }
        public int Seat { get; set; }

        public int Class { get; set; }
        public Ride Ride { get; set; }

        public DateTime RideDateTime { get; set; }
        public Client Client { get; set; }
        public DateTime PurchaseDate { get; set; }
        public TicketsType TicketType { get; set; }

        public Ticket() { }

        public Ticket(int id, double price, int passengerCar,int seat, DateTime ridedateTime,Ride ride,Client client,TicketsType type,int classP)
        {
            Id = id;
            Price = price;
            Seat = seat;
            Ride = ride;
            RideDateTime = ridedateTime;
            Client = client;
            TicketType = type;
            PurchaseDate = DateTime.Now;
            PassengerCar = passengerCar;
            Class = classP;
            Client.Tickets.Add(this);
            Ride.Tickets.Add(this);
        }

        public override string ToString()
        {
            return "Ticket:" + " " + Id + " " + Price + "din Seat:" + Seat + " " + Ride + " " + Client;
        }

        public enum TicketsType
        {
            RESERVED,
            BOUGHT
        }



    }
}

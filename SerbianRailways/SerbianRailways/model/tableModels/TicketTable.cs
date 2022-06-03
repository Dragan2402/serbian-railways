using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.model.tableModels
{
    public class TicketTable
    {
        public int Id { get; set; }
        public Double Price { get; set; }
        public int Seat { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TicketType { get; set; }

        public TicketTable(Ticket ticket)
        {
            Id= ticket.Id;
            Price=ticket.Price;
            Seat=ticket.Seat;
            From = ticket.Ride.Line.DepartureStation.Name;
            To = ticket.Ride.Line.ArrivalStation.Name;
            if (ticket.TicketType == Ticket.TicketsType.RESERVED)
                TicketType = "Rez";
            else
                TicketType = "Kup";
        }
    }
}

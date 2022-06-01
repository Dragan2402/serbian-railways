using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.model
{
    public class Ride
    {
        public int Id { get; set; }

        public TimeSpan DepartureTime { get; set; }

        public TimeSpan ArrivalTime { get; set; }

        public TimeSpan Duration { get; set; }

        public List<Ticket> Tickets { get; set; }

        public Train Train { get; set; }

        public Line Line { get; set; }

        public List<RideStop> RideStops { get; set; }

        public Dictionary<int,Boolean> SeatsStatus { get; set; }

        public Ride()
        {
            Tickets = new List<Ticket>();
            RideStops = new List<RideStop>();
            SeatsStatus = new Dictionary<int,Boolean>();
        }

        public Ride(int id,TimeSpan departureTime,TimeSpan arrivalTime,Train train,Line line)
        {
            Id = id;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            if (arrivalTime < departureTime)
                Duration = (arrivalTime + new TimeSpan(24, 0, 0)) - departureTime;
            else
                Duration = arrivalTime - departureTime;
            Train = train;
            Line = line;
            Tickets = new List<Ticket>();
            RideStops = new List<RideStop>();
            SeatsStatus = new Dictionary<int, bool>();
            for(int i = 0; i < Train.NumberOfSeats; i++)
            {
                SeatsStatus[i] = false;
            }

            Line.Rides.Add(this);
            Train.Rides.Add(this);

        }

        public override string ToString()
        {
            return "Ride:" + " " + Id + " " + DepartureTime + "-" + ArrivalTime + " " + Duration + " " + Train + " " + Line;
        }
    }
}

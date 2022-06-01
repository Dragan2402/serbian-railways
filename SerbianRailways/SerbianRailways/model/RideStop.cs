using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.model
{
    public class RideStop
    {
        public Station Station { get; set; }

        public DateTime ArrivalTime { get; set; }

        public DateTime DepartureTime { get; set; }

        public RideStop() { }

        public RideStop(Station station, DateTime arrivalTime, DateTime departureTime)
        {
            Station = station;
            ArrivalTime = arrivalTime;
            DepartureTime = departureTime;
        }

        public override string ToString()
        {
            return "RideStop:" + " " + Station + " " + ArrivalTime + "-" + DepartureTime;
        }
    }
}

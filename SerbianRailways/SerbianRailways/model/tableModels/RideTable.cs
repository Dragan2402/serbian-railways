using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.model.tableModels
{
    public class RideTable
    {
        public int Id { get; set; }

        public string Line { get; set; }

        public string TimeSpan { get; set; }

        public string Train { get; set; }

        public double Price { get; set; }

        public RideTable() { }

        public RideTable(Ride ride)
        {
            Id = ride.Id;
            Line = ride.Line.DepartureStation.Name + "-" + ride.Line.ArrivalStation.Name;
            TimeSpan = ride.DepartureTime.ToString() + "-" + ride.ArrivalTime.ToString();
            Train = ride.Train.Name;    
            Price = ride.Price;
        }
    }
}

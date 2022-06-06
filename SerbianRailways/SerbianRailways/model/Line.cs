using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.model
{
    public class Line
    {
        public int Id { get; set; }

        public Station DepartureStation { get; set; }

        public Station ArrivalStation { get; set; }

        public List<Station> InterStations { get; set; }

        public List<Ride> Rides { get; set; }

        public Line() {
            Rides = new List<Ride>();
        }

        public Line(int id,Station departureStation,Station arrivalStation)
        {
            Id = id;
            DepartureStation = departureStation;
            ArrivalStation = arrivalStation;
            Rides=new List<Ride>();
            InterStations=new List<Station>();
        }

        public override string ToString()
        {
            return "Linija:" + " " + Id + " " + DepartureStation.Name + "-" + ArrivalStation.Name;
        }
    }
}

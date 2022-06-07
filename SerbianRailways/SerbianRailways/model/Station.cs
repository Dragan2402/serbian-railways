using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.model
{
    public class Station
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }

        public List<Line> Lines { get; set; }

        public Station() { }

        public Station(int id, string name, Location location)
        {
            Id = id;
            Name = name;
            Location = location;
            Lines = new List<Line>();
        }

        public override string ToString()
        {
            return "Station:" + " " + Id + " " + Name + " " + Location;
        }

    }
}

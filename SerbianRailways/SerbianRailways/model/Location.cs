using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.model
{
    public class Location
    {
        public double X { get; set; }

        public double Y { get; set; }

        public Location() { }

        public Location(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "X: " + X + " Y: " + Y;
        }
    }
}

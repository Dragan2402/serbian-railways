using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.model
{
    public class Train
    {
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public int NumberOfSeats { get; set; }
        public List<Ride> Rides { get; set; }

        public Train() { }

        public Train(int serialNumber, string name, int numberOfSeats)
        {
            SerialNumber = serialNumber;
            Name = name;
            NumberOfSeats = numberOfSeats;
            Rides=new List<Ride>();
        }

        public override string ToString()
        {
            return "Train:" + " " + SerialNumber + " " + Name + " Seats: " + NumberOfSeats;
        }
    }
}

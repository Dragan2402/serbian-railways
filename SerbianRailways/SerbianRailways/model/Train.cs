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
        public int PassengerCars { get; set; }
        public int FirstGradeSeats { get; set; }
        public int SecondGradeSeats { get; set; }

            
        public List<Ride> Rides { get; set; }

        public Train() { }

        public Train(int serialNumber, string name, int passengerCars,int fgSeats,int sgSeats)
        {
            SerialNumber = serialNumber;
            Name = name;
            PassengerCars = passengerCars;
            FirstGradeSeats = fgSeats;
            SecondGradeSeats = sgSeats;
            NumberOfSeats= PassengerCars*(FirstGradeSeats+SecondGradeSeats);
            Rides=new List<Ride>();
        }

        public override string ToString()
        {
            return "Voz:" + " " + SerialNumber + " " + Name + " Mesta: " + NumberOfSeats;
        }
    }
}

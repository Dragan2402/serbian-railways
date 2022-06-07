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

        public Double Price { get; set; }

        public string DaysThatRidesTable { get; set; }

        public List<DayOfWeek> DayOfWeeksThatDrives { get; set; }

        public List<RideStop> RideStops { get; set; }

        public Dictionary<DateTime,SeatStatus> SeatsStatus { get; set; }

        public Ride()
        {
            Tickets = new List<Ticket>();
            RideStops = new List<RideStop>();
            DayOfWeeksThatDrives= new List<DayOfWeek>();
            SeatsStatus = new Dictionary<DateTime, SeatStatus>();
        }

        public Ride(int id, TimeSpan departureTime, TimeSpan arrivalTime, Train train, Line line, double price,List<DayOfWeek> dayOfWeeks)
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
            
            SeatsStatus = new Dictionary<DateTime, SeatStatus>();
            DayOfWeeksThatDrives = dayOfWeeks;
           
            Line.Rides.Add(this);
            Train.Rides.Add(this);
            Price = price;
            GenerateWhenDrivesString();
        }

        public void GenerateWhenDrivesString()
        {
            DaysThatRidesTable = "";
            foreach (DayOfWeek dayOfWeek in DayOfWeeksThatDrives)
            {
                switch (dayOfWeek)
                {
                    case DayOfWeek.Monday:
                        DaysThatRidesTable = DaysThatRidesTable + "Pon" + " ";
                        break;
                    case DayOfWeek.Tuesday:
                        DaysThatRidesTable = DaysThatRidesTable + "Uto" + " ";
                        break;
                    case DayOfWeek.Wednesday:
                        DaysThatRidesTable = DaysThatRidesTable + "Sre" + " ";
                        break;
                    case DayOfWeek.Thursday:
                        DaysThatRidesTable = DaysThatRidesTable + "Čet" + " ";
                        break;
                    case DayOfWeek.Friday:
                        DaysThatRidesTable = DaysThatRidesTable + "Pet" + " ";
                        break;
                    case DayOfWeek.Saturday:
                        DaysThatRidesTable = DaysThatRidesTable + "Sub" + " ";
                        break;
                    case DayOfWeek.Sunday:
                        DaysThatRidesTable = DaysThatRidesTable + "Ned" + " ";
                        break;
                }

            }
        }
        public Tuple<int,int> TakeSeat(DateTime date,int grade)
        {
            if (SeatsStatus.ContainsKey(date))
            {
                return SeatsStatus[date].TakeSeat(grade);
            }
            else
            {
                SeatStatus seatStatus = new SeatStatus(Train);
                SeatsStatus[date] = seatStatus;
                return SeatsStatus[date].TakeSeat(grade);
            }
        }

        public bool HasFreeSeats(DateTime date, int grade,int seats)
        {
            if (SeatsStatus.ContainsKey(date))
            {
                return SeatsStatus[date].HasFreeSeats(grade,seats);
            }
            else
            {
                SeatStatus seatStatus = new SeatStatus(Train);
                SeatsStatus[date] = seatStatus;
                return SeatsStatus[date].HasFreeSeats(grade,seats);
            }
        }

        public override string ToString()
        {
            return "Ride:" + " " + Id + " " + DepartureTime + "-" + ArrivalTime + " " + Duration + " " + Train + " " + Line;
        }

        public bool FreeSeat(Ticket ticket)
        {
            if (SeatsStatus.ContainsKey(ticket.RideDateTime))
            {
                return SeatsStatus[ticket.RideDateTime].FreeSeat(ticket);
            }
            return false;
        }
    }
}

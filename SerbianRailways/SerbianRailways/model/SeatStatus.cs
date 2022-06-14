using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.model
{
    public class SeatStatus
    {
        public Dictionary<int, Dictionary<int,bool>> Status { get; set; } //kljuc vagon pa rjecnik sjediste zauzetost

        private int FirstGradeSeats { get; set; }

        private int SecondGradeSeats { get; set; }


        public SeatStatus() { Status = new Dictionary<int, Dictionary<int,bool>>(); }

        public SeatStatus(Train train)
        {
            Status = new Dictionary<int, Dictionary<int, bool>>();
            FirstGradeSeats = train.FirstGradeSeats;
            SecondGradeSeats= train.SecondGradeSeats;
            for(int i=1; i<= train.PassengerCars; i++)
            {
                Dictionary<int, bool> carSeatStatus = new Dictionary<int, bool>();
                for(int j=1; j<= train.FirstGradeSeats; j++)
                    carSeatStatus[j] = true;
                for(int k=1; k<= train.SecondGradeSeats; k++)
                    carSeatStatus[k+train.FirstGradeSeats] = true;
                Status[i] = carSeatStatus;
            }
        }

        public Tuple<int, int> TakeSeat(int grade)
        {
            int start = 1;
            int maximum = FirstGradeSeats;
            if (grade == 2) {
                start = FirstGradeSeats + 1;
                maximum = FirstGradeSeats + SecondGradeSeats;
            }
            foreach(int car in Status.Keys)
            {
                for(int seat=start; seat<=maximum; seat++)
                    if (Status[car][seat] == true)
                    {
                        Status[car][seat]=false;
                        return new Tuple<int, int>(car, seat);
                    }
            }

            return new Tuple<int, int>(0, 0);
        }

        public bool HasFreeSeats(int grade,int numberOfSeats)
        {
            int start = 1;
            int maximum = FirstGradeSeats;
            if (grade == 2)
            {
                start = FirstGradeSeats + 1;
                maximum = FirstGradeSeats + SecondGradeSeats;
            }

            foreach (int car in Status.Keys)
            {
                for (int seat = start; seat <= maximum; seat++)
                    if (Status[car][seat] == true)
                    {
                        numberOfSeats--;
                        if(numberOfSeats == 0)
                            return true;
                    }
            }
            return false;
        }

        public bool FreeSeat(Ticket ticket)
        {
            if (Status.ContainsKey(ticket.PassengerCar))
            {
                int seat = ticket.Seat;
                if (ticket.Class == 2)
                    seat = seat + FirstGradeSeats;
                if (Status[ticket.PassengerCar].ContainsKey(seat))
                {
                    if (Status[ticket.PassengerCar][seat] == false){
                        Status[ticket.PassengerCar][seat] = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}

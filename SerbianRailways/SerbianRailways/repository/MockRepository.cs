using SerbianRailways.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.repository
{
    public class MockRepository
    {
        public Dictionary<string,User> Users { get; set; }
        public Dictionary<int,Station> Stations { get; set; }
        public Dictionary<int,Line> Lines { get; set; }
        public Dictionary<int,Train> Trains { get; set; }
        public Dictionary<int,Ride> Rides { get; set; }
        public Dictionary<int,Ticket> Tickets { get; set; }
        private int StationID { get; set; }
        private int TicketID { get; set; }
        private int LineID  { get; set; }
        private int RideID { get; set; }

        public User LoggedUser { get; set; }

        public MockRepository()
        {
            Users = new Dictionary<string, User>();
            Stations = new Dictionary<int, Station>();
            Lines = new Dictionary<int, Line>();
            Trains = new Dictionary<int, Train>();
            Rides = new Dictionary<int, Ride>();
            Tickets = new Dictionary<int, Ticket>();
            StationID = 0;
            TicketID = 0;
            LineID = 0;
            RideID = 0;

            InitializeMockData();
        }

        private void InitializeMockData()
        {
            LoggedUser = null;

            Address addressAki = new Address("Futoska 12", "Novi Sad", "21000", "Srbija");
            Client clientTemp1 = new Client("asiKavasaki", "akiaki", "Andrej", "Čuljak", addressAki);

            Address addressDmitar = new Address("Telep Gang", "Novi Sad", "21000", "Srbija");
            Client clientTemp2 = new Client("dmitar", "dmitar", "Dimitrije", "Petrov", addressDmitar);

            Users.Add(clientTemp1.UserName,clientTemp1);
            Users.Add(clientTemp2.UserName,clientTemp2);

            Address addressDanica = new Address("Konjarnik 10", "Beograd", "11000", "Srbija");
            Manager managerDanica = new Manager("danica", "danica", "Danica", "Joksić", addressDanica);

            Address addressGage = new Address("Hopovska 4", "Novi Sad", "21000", "Srbija");
            Manager managerGage = new Manager("gage", "gage", "Dragan", "Mirković", addressGage);

            Users.Add(managerDanica.UserName,managerDanica);
            Users.Add(managerGage.UserName,managerGage);

            StationID++;
            Station stationNS = new Station(StationID, "Novi Sad", new Location(45.26375758423444, 19.777578211764425));
            StationID++;
            Station stationBG = new Station(StationID, "Beograd", new Location(44.819380920430085, 20.439203287115173));
            StationID++;
            Station stationIndjija = new Station(StationID, "Inđija", new Location(45.07861419731812, 20.11183757924523));
            StationID++;
            Station stationStaraPazova = new Station(StationID, "Stara Pazova", new Location(44.98531331412219, 20.17893026896439));
            
            StationID++;
            Station stationKragujevac=new Station(StationID, "Kragujevac", new Location(44.0021259863985, 20.902610657668514));

            StationID++;
            Station stationSabac = new Station(StationID, "Šabac", new Location(44.766465531139204, 19.67960365061717));

            StationID++;
            Station stationJagodina = new Station(StationID, "Jagodina", new Location(44.010536683625034, 21.283007076104106));
            StationID++;
            Station stationNIS = new Station(StationID, "Niš", new Location(43.33654100851986, 21.880093186766967));

            Stations.Add(stationNS.Id,stationNS);
            Stations.Add(stationBG.Id,stationBG);
            Stations.Add(stationIndjija.Id,stationIndjija);
            Stations.Add(stationStaraPazova.Id,stationStaraPazova);
            Stations.Add(stationKragujevac.Id,stationKragujevac);
            Stations.Add(stationSabac.Id,stationSabac);
            Stations.Add(stationJagodina.Id,stationJagodina);
            Stations.Add(stationNIS.Id,stationNIS);

            Train trainSoko = new Train(561, "Soko", 5,10,10);
            Train trainRegio = new Train(2423, "Regio", 5,10,10);
            Train trainRegio2 = new Train(2422, "Regio", 5,10,10);

            Trains.Add(trainSoko.SerialNumber,trainSoko);
            Trains.Add(trainRegio.SerialNumber,trainRegio);
            Trains.Add(trainRegio2.SerialNumber,trainRegio2);

            LineID++;
            Line lineNSBG = new Line(LineID, stationNS, stationBG);
            lineNSBG.InterStations.Add(stationIndjija);
            lineNSBG.InterStations.Add(stationStaraPazova);
            LineID++;
            Line lineBGNS = new Line(LineID, stationBG, stationNS);
            lineBGNS.InterStations.Add(stationStaraPazova);
            lineBGNS.InterStations.Add(stationIndjija);
            LineID++;
            Line lineNSNIS = new Line(LineID,stationNS,stationNIS);
            lineNSNIS.InterStations.Add(stationIndjija);
            lineNSNIS.InterStations.Add(stationStaraPazova);
            lineNSNIS.InterStations.Add(stationBG);
            lineNSNIS.InterStations.Add(stationJagodina);
            LineID++;
            Line lineNISNS = new Line(LineID, stationNIS, stationNS);
            lineNISNS.InterStations.Add(stationJagodina);
            lineNISNS.InterStations.Add(stationBG);
            lineNISNS.InterStations.Add(stationStaraPazova);
            lineNISNS.InterStations.Add(stationIndjija);

            LineID++;
            Line lineNSKrag = new Line(LineID, stationNS, stationKragujevac);
            lineNSKrag.InterStations.Add(stationIndjija);
            lineNSKrag.InterStations.Add(stationStaraPazova);
            lineNSKrag.InterStations.Add(stationBG);

            LineID++;
            Line lineKragNS = new Line(LineID, stationKragujevac, stationNS);
            lineKragNS.InterStations.Add(stationBG);
            lineKragNS.InterStations.Add(stationStaraPazova);
            lineKragNS.InterStations.Add(stationIndjija);

            LineID++;
            Line lineBGSbc = new Line(LineID, stationBG, stationSabac);
            LineID++;
            Line lineSbcBG = new Line(LineID, stationSabac, stationBG);


            Lines.Add(lineNSBG.Id,lineNSBG);
            Lines.Add(lineBGNS.Id,lineBGNS);
            Lines.Add(lineNSNIS.Id,lineNSNIS);
            Lines.Add(lineNISNS.Id,lineNISNS);
            Lines.Add(lineKragNS.Id, lineKragNS);
            Lines.Add(lineNSKrag.Id, lineNSKrag);
            Lines.Add(lineBGSbc.Id, lineBGSbc);
            Lines.Add(lineSbcBG.Id, lineSbcBG);

            List<DayOfWeek> allDays = new List<DayOfWeek>() { DayOfWeek.Sunday,DayOfWeek.Monday,DayOfWeek.Tuesday,DayOfWeek.Wednesday,DayOfWeek.Wednesday,DayOfWeek.Thursday,DayOfWeek.Friday,DayOfWeek.Saturday};
            List<DayOfWeek> weekDays = new List<DayOfWeek>() { DayOfWeek.Sunday,DayOfWeek.Saturday};
            List<DayOfWeek> workDays = new List<DayOfWeek>() { DayOfWeek.Monday,DayOfWeek.Tuesday,DayOfWeek.Wednesday,DayOfWeek.Thursday,DayOfWeek.Friday};

            //u konstruktoru Ride se vozu i liniji doda ride
            RideID++;
            Ride rideNSBG = new Ride(RideID, new TimeSpan(12, 00, 00), new TimeSpan(12, 30, 00), trainSoko, lineNSBG,600.4,allDays);
            RideID++;
            Ride rideNSBG2 = new Ride(RideID, new TimeSpan(14, 00, 00), new TimeSpan(15, 00, 00), trainRegio, lineNSBG,600.23,weekDays);
            RideID++;
            Ride rideBGNS = new Ride(RideID, new TimeSpan(12, 40, 00), new TimeSpan(13, 15, 00), trainSoko, lineBGNS,560.1,allDays);
            RideID++;
            Ride rideBGNS2 = new Ride(RideID, new TimeSpan(16, 40, 00), new TimeSpan(17, 50, 00), trainRegio2, lineBGNS,560.8,weekDays);
            RideID++;
            Ride rideNSNIS = new Ride(RideID, new TimeSpan(18, 00, 00), new TimeSpan(22, 00, 00), trainSoko, lineNSNIS,560.5,workDays);
            RideID++;
            Ride rideNISNS = new Ride(RideID, new TimeSpan(22, 40, 00), new TimeSpan(02, 15, 00), trainSoko, lineNISNS,560.23,workDays);

            Rides.Add(rideNSBG.Id, rideNSBG);
            Rides.Add(rideNSBG2.Id, rideNSBG2);
            Rides.Add(rideBGNS.Id, rideBGNS);
            Rides.Add(rideBGNS2.Id, rideBGNS2);
            Rides.Add(rideNSNIS.Id, rideNSNIS);
            Rides.Add(rideNISNS.Id, rideNISNS);

            //u konstruktoru Ticket se dodaje karta klijentu i voznji i zauzima se mjesto automatski, validaciju postojanja mjesta odraditi ranije
            DateTime datum1 = new DateTime(2022, 6, 10);
            DateTime datum2 = new DateTime(2022, 6, 12);
            if (rideNSBG.HasFreeSeats(datum1, 1, 1))
            {
                Tuple<int, int> carSeat = rideNSBG.TakeSeat(datum1, 1);
                TicketID++;
                Ticket ticketNSBG = new Ticket(TicketID, rideNSBG.Price, carSeat.Item1,carSeat.Item2, datum1,rideNSBG, clientTemp1, Ticket.TicketsType.BOUGHT,1);
                Tickets.Add(ticketNSBG.Id, ticketNSBG);
            }
            if (rideBGNS.HasFreeSeats(datum1, 2, 1))
            {
                TicketID++;
                Tuple<int, int> carSeat = rideBGNS.TakeSeat(datum1, 1);
                Ticket ticketBGNS = new Ticket(TicketID, rideBGNS.Price, carSeat.Item1,carSeat.Item2, datum1, rideBGNS, clientTemp1, Ticket.TicketsType.BOUGHT,2);
                Tickets.Add(ticketBGNS.Id, ticketBGNS);
            }
            if (rideNSNIS.HasFreeSeats(datum2, 1, 1))
            {
                TicketID++;
                Tuple<int, int> carSeat = rideNSNIS.TakeSeat(datum2, 1);
                Ticket ticketNSNIS = new Ticket(TicketID, rideNSNIS.Price, carSeat.Item1,carSeat.Item2, datum2, rideNSNIS, clientTemp2, Ticket.TicketsType.RESERVED,1);
                Tickets.Add(ticketNSNIS.Id, ticketNSNIS);
            }
            if (rideNISNS.HasFreeSeats(datum2, 1, 1))
            {
                TicketID++;
                Tuple<int, int> carSeat = rideNISNS.TakeSeat(datum2, 1);
                Ticket ticketNISNS = new Ticket(TicketID, rideNISNS.Price, carSeat.Item1,carSeat.Item2, datum2,rideNISNS, clientTemp2, Ticket.TicketsType.RESERVED,1);                
                Tickets.Add(ticketNISNS.Id, ticketNISNS);
            }

/*            foreach (Ticket ticket in Tickets.Values)
                Console.WriteLine(ticket);

            DateTime dateTimeTest = new DateTime(2022, 6, 11);
            Console.WriteLine("TEST 1: TRUE EXPECTED");
            Console.WriteLine(rideNSBG2.HasFreeSeats(dateTimeTest, 1, 50));
            Console.WriteLine("TEST 2: FALSE EXPECTED");
            Console.WriteLine(rideNSBG2.HasFreeSeats(dateTimeTest, 1, 51));
            Console.WriteLine(rideNSBG2.TakeSeat(dateTimeTest, 1).ToString());
            Console.WriteLine("TEST 3: FALSE EXPECTED");
            Console.WriteLine(rideNSBG2.HasFreeSeats(dateTimeTest, 1, 50));
            DateTime dateTimeTest2 = new DateTime(2022, 6, 12);
            Console.WriteLine("TEST 4: TRUE EXPECTED");
            Console.WriteLine(rideNSBG2.HasFreeSeats(dateTimeTest2, 1, 50));*/


        }

        internal Client GetLoggedClient()
        {
            return (Client)LoggedUser;
        }

        public int GetNextTicketID()
        {
            TicketID++;
            return TicketID;
        }

        public bool LineExists(ObservableCollection<Station> stationsInLine)
        {
            foreach(Line line in Lines.Values)
            {
                if (stationsInLine.Count == 2)
                {
                    if (line.DepartureStation == stationsInLine.ElementAt(0) && line.ArrivalStation == stationsInLine.Last())
                        return true;
                }
                else
                {
                    if (line.DepartureStation == stationsInLine.ElementAt(0) && line.ArrivalStation == stationsInLine.Last() && StationsInterSame(stationsInLine, line.InterStations))
                        return true;
                }

            }
            return false;
        }

        private bool StationsInterSame(ObservableCollection<Station> stationsInLine, List<Station> interStations)
        {
            
            if ((stationsInLine.Count - 2) != interStations.Count)
                return false;
            for( int i=1; i< stationsInLine.Count - 1; i++)
            {
                if (stationsInLine.ElementAt(i) != interStations.ElementAt(i-1))
                {
                    return false;
                }
            }
            return true;
        }

        public Line AddNewLine(ObservableCollection<Station> stationsSelected)
        {
            int size = stationsSelected.Count;
            Line line = null;
            if (size == 2)
            {
                LineID++;
                line = new Line(LineID,stationsSelected.ElementAt(0),stationsSelected.ElementAt(1));
                if (!LineExists(line))
                    Lines.Add(line.Id, line);
                else
                    line = null;
            }else if(size > 2)
            {
                LineID++;
                line = new Line(LineID, stationsSelected.ElementAt(0), stationsSelected.Last());
                for( int i=1; i < size - 1; i++)
                {
                    line.InterStations.Add(stationsSelected.ElementAt(i));
                }
                if (!LineExists(line))
                    Lines.Add(line.Id, line);
                else
                    line = null;

            }

            return line;
            
        }

        private bool LineExists(Line lineP)
        {
            foreach(Line line in Lines.Values)
            {

                if (line.DepartureStation.Id == lineP.DepartureStation.Id && lineP.ArrivalStation.Id == line.ArrivalStation.Id && InterStationsSame(line, lineP))
                    return true;
            
            }

            return false;
        }

        private bool InterStationsSame(Line line, Line lineP)
        {
            bool same = true;
            if (line.InterStations.Count != lineP.InterStations.Count)
                return false;
            for( int i=0; i< line.InterStations.Count; i++)
            {
                if (line.InterStations.ElementAt(i) != lineP.InterStations.ElementAt(i))
                {
                    same = false;
                    break;
                }

            }

            return same;
            
        }

        public Line AddNewLine(Station departureStation, Station arrivalStation)
        {
            LineID++;
            Line lineTemp = new Line(LineID, departureStation, arrivalStation);
            Lines.Add(lineTemp.Id, lineTemp);
            return lineTemp;
        }

        internal Ride AddNewRide(TimeSpan departureTime, TimeSpan arrivalTime, Train train, Line line, double price, List<DayOfWeek> dayOfWeeks)
        {
            RideID++;
            Ride rideTemp = new Ride(RideID, departureTime, arrivalTime, train, line, price, dayOfWeeks);
            Rides.Add(rideTemp.Id, rideTemp);
            return rideTemp;
        }

        public Station AddNewStation(string stationName, Location stationLocation)
        {
            StationID++;
            Station station = new Station(StationID,stationName,stationLocation);
            Stations.Add(station.Id, station);
            return station;
        }
    }
}

using SerbianRailways.model;
using System;
using System.Collections.Generic;
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
            Station stationNIS = new Station(StationID, "Niš", new Location(43.33654100851986, 21.880093186766967));

            Stations.Add(stationNS.Id,stationNS);
            Stations.Add(stationBG.Id,stationBG);
            Stations.Add(stationNIS.Id,stationNIS);

            Train trainSoko = new Train(561, "Soko", 300);
            Train trainRegio = new Train(2423, "Regio", 250);
            Train trainRegio2 = new Train(2422, "Regio", 245);

            Trains.Add(trainSoko.SerialNumber,trainSoko);
            Trains.Add(trainRegio.SerialNumber,trainRegio);
            Trains.Add(trainRegio2.SerialNumber,trainRegio2);

            LineID++;
            Line lineNSBG = new Line(LineID, stationNS, stationBG);
            LineID++;
            Line lineBGNS = new Line(LineID, stationBG, stationNS);
            LineID++;
            Line lineNSNIS = new Line(LineID,stationNS,stationNIS);
            LineID++;
            Line lineNISNS = new Line(LineID, stationNIS, stationNS);

            Lines.Add(lineNSBG.Id,lineNSBG);
            Lines.Add(lineBGNS.Id,lineBGNS);
            Lines.Add(lineNSNIS.Id,lineNSNIS);
            Lines.Add(lineNISNS.Id,lineNISNS);

            //u konstruktoru Ride se vozu i liniji doda ride
            RideID++;
            Ride rideNSBG = new Ride(RideID, new TimeSpan(12, 00, 00), new TimeSpan(12, 30, 00), trainSoko, lineNSBG,600.4);
            RideID++;
            Ride rideNSBG2 = new Ride(RideID, new TimeSpan(14, 00, 00), new TimeSpan(15, 00, 00), trainRegio, lineNSBG,600.23);
            RideID++;
            Ride rideBGNS = new Ride(RideID, new TimeSpan(12, 40, 00), new TimeSpan(13, 15, 00), trainSoko, lineBGNS,560.1);
            RideID++;
            Ride rideBGNS2 = new Ride(RideID, new TimeSpan(16, 40, 00), new TimeSpan(17, 50, 00), trainRegio2, lineBGNS,560.8);
            RideID++;
            Ride rideNSNIS = new Ride(RideID, new TimeSpan(18, 00, 00), new TimeSpan(22, 00, 00), trainSoko, lineNSNIS,560.5);
            RideID++;
            Ride rideNISNS = new Ride(RideID, new TimeSpan(22, 40, 00), new TimeSpan(02, 15, 00), trainSoko, lineNISNS,560.23);

            Rides.Add(rideNSBG.Id, rideNSBG);
            Rides.Add(rideNSBG2.Id, rideNSBG2);
            Rides.Add(rideBGNS.Id, rideBGNS);
            Rides.Add(rideBGNS2.Id, rideBGNS2);
            Rides.Add(rideNSNIS.Id, rideNSNIS);
            Rides.Add(rideNISNS.Id, rideNISNS);

            //u konstruktoru Ticket se dodaje karta klijentu i voznji i zauzima se mjesto automatski, validaciju postojanja mjesta odraditi ranije
            TicketID++;
            Ticket ticketNSBG = new Ticket(TicketID, rideNSBG.Price, 30, rideNSBG, clientTemp1,Ticket.TicketsType.BOUGHT);
            TicketID++;
            Ticket ticketBGNS = new Ticket(TicketID, rideBGNS.Price, 35, rideBGNS, clientTemp1,Ticket.TicketsType.BOUGHT);
            TicketID++;
            Ticket ticketNSNIS = new Ticket(TicketID, rideNSNIS.Price, 5, rideNSNIS, clientTemp2,Ticket.TicketsType.RESERVED);
            ticketNSNIS.PurchaseDate = new DateTime(2022, 3, 20,15,20,20);
            TicketID++;
            Ticket ticketNISNS = new Ticket(TicketID, rideNISNS.Price, 3, rideNISNS, clientTemp2,Ticket.TicketsType.RESERVED);
            ticketNISNS.PurchaseDate = new DateTime(2022, 3, 20, 15, 20, 21);

            Tickets.Add(ticketNSBG.Id, ticketNSBG);
            Tickets.Add(ticketBGNS.Id, ticketBGNS);
            Tickets.Add(ticketNSNIS.Id, ticketNSNIS);
            Tickets.Add(ticketNISNS.Id, ticketNISNS);



        }

        public Line AddNewLine(Station departureStation, Station arrivalStation)
        {
            LineID++;
            Line lineTemp = new Line(LineID, departureStation, arrivalStation);
            Lines.Add(lineTemp.Id, lineTemp);
            return lineTemp;
        }

        internal Ride AddNewRide(TimeSpan departureTime, TimeSpan arrivalTime, Train train, Line line, double price)
        {
            RideID++;
            Ride rideTemp = new Ride(RideID, departureTime, arrivalTime, train, line, price);
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

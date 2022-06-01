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
        public List<Client> Clients { get; set; }
        public List<Manager> Managers { get; set; }
        public List<Station> Stations { get; set; }
        public List<Line> Lines { get; set; }
        public List<Train> Trains { get; set; }
        public List<Ride> Rides { get; set; }
        public List<RideStop> RideStops { get; set; }   
        public List<Ticket> Tickets { get; set; }
        private int StationID { get; set; }
        private int TicketID { get; set; }
        private int LineID  { get; set; }
        private int RideID { get; set; }

        public MockRepository()
        {
            Clients = new List<Client>();
            Managers = new List<Manager>();
            Stations = new List<Station>();
            Lines = new List<Line>();
            Trains = new List<Train>();
            Rides = new List<Ride>();
            RideStops = new List<RideStop>();
            Tickets = new List<Ticket>();
            StationID = 0;
            TicketID = 0;
            LineID = 0;
            RideID = 0;

            InitializeMockData();
        }

        private void InitializeMockData()
        {
            Address addressAki = new Address("Futoska 12", "Novi Sad", "21000", "Srbija");
            Client clientTemp1 = new Client("asiKavasaki", "akiaki", "Andrej", "Culjak", addressAki);

            Address addressDmitar = new Address("Telep Gang", "Novi Sad", "21000", "Srbija");
            Client clientTemp2 = new Client("dmitar", "dimpet96", "Dimitrije", "Petrov", addressDmitar);

            Clients.Add(clientTemp1);
            Clients.Add(clientTemp2);

            Address addressDanica = new Address("Grbavica 10", "Beograd", "11000", "Srbija");
            Manager managerDanica = new Manager("danica81", "danicaDanica", "Danica", "Joksic", addressDanica);

            Address addressGage = new Address("Hopovska 4", "Novi Sad", "21000", "Srbija");
            Manager managerGage = new Manager("gage2402", "gageGage", "Dragan", "Mirkovic", addressGage);

            Managers.Add(managerDanica);
            Managers.Add(managerGage);

            StationID++;
            Station stationNS = new Station(StationID, "Novi Sad", new Location(100, 200));
            StationID++;
            Station stationBG = new Station(StationID, "Beograd", new Location(100, 400));
            StationID++;
            Station stationNIS = new Station(StationID, "Niš", new Location(150, 600));

            Stations.Add(stationNS);
            Stations.Add(stationBG);
            Stations.Add(stationNIS);

            Train trainSoko = new Train(561, "Soko", 300);
            Train trainRegio = new Train(2423, "Regio", 250);
            Train trainRegio2 = new Train(2422, "Regio", 245);

            Trains.Add(trainSoko);
            Trains.Add(trainRegio);
            Trains.Add(trainRegio2);

            LineID++;
            Line lineNSBG = new Line(LineID, stationNS, stationBG);
            LineID++;
            Line lineBGNS = new Line(LineID, stationBG, stationNS);
            LineID++;
            Line lineNSNIS = new Line(LineID,stationNS,stationNIS);
            LineID++;
            Line lineNISNS = new Line(LineID, stationNIS, stationNS);

            Lines.Add(lineNSBG);
            Lines.Add(lineBGNS);
            Lines.Add(lineNSNIS);
            Lines.Add(lineNISNS);

            //u konstruktoru Ride se vozu i liniji doda ride
            RideID++;
            Ride rideNSBG = new Ride(RideID, new TimeSpan(12, 00, 00), new TimeSpan(12, 30, 00), trainSoko, lineNSBG);
            RideID++;
            Ride rideNSBG2 = new Ride(RideID, new TimeSpan(14, 00, 00), new TimeSpan(15, 00, 00), trainRegio, lineNSBG);
            RideID++;
            Ride rideBGNS = new Ride(RideID, new TimeSpan(12, 40, 00), new TimeSpan(13, 15, 00), trainSoko, lineBGNS);
            RideID++;
            Ride rideBGNS2 = new Ride(RideID, new TimeSpan(16, 40, 00), new TimeSpan(17, 50, 00), trainRegio2, lineBGNS);
            RideID++;
            Ride rideNSNIS = new Ride(RideID, new TimeSpan(18, 00, 00), new TimeSpan(22, 00, 00), trainSoko, lineNSNIS);
            RideID++;
            Ride rideNISNS = new Ride(RideID, new TimeSpan(22, 40, 00), new TimeSpan(02, 15, 00), trainSoko, lineNISNS);

            Rides.Add(rideNSBG);
            Rides.Add(rideNSBG2);
            Rides.Add(rideBGNS);
            Rides.Add(rideBGNS2);
            Rides.Add(rideNSNIS);
            Rides.Add(rideNISNS);

            //u konstruktoru Ticket se dodaje karta klijentu i voznji i zauzima se mjesto automatski, validaciju postojanja mjesta odraditi ranije
            TicketID++;
            Ticket ticketNSBG = new Ticket(TicketID, 600.0, 30, rideNSBG, clientTemp1);
            TicketID++;
            Ticket ticketBGNS = new Ticket(TicketID, 560.0, 35, rideNSBG, clientTemp1);
            TicketID++;
            Ticket ticketNSNIS = new Ticket(TicketID, 800.0, 5, rideNSNIS, clientTemp2);
            TicketID++;
            Ticket ticketNISNS = new Ticket(TicketID, 770.0, 3, rideNISNS, clientTemp2);

            Tickets.Add(ticketNSBG);
            Tickets.Add(ticketBGNS);
            Tickets.Add(ticketNSNIS);
            Tickets.Add(ticketNISNS);



        }


    }
}

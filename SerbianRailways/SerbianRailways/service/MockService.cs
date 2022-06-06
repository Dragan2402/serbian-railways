using SerbianRailways.model;
using SerbianRailways.model.tableModels;
using SerbianRailways.repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SerbianRailways.service
{
    public class MockService
    {
        private MockRepository mockRepository = null;

        public MockService()
        {
            mockRepository = new MockRepository();
        }

        public List<Client> GetAllClients()
        {
            List<Client> clients = new List<Client>();
            foreach(User user in mockRepository.Users.Values)
            {
                if (user.Role == Role.CLIENT)
                    clients.Add((Client)user);
            }
            return clients;
        }

        public List<Ticket> GetAllTickets()
        {
            return mockRepository.Tickets.Values.ToList();
        }
      
        public List<Manager> GetAllManagers()
        {
            List<Manager> managers = new List<Manager>();
            foreach (User user in mockRepository.Users.Values)
            {
                if (user.Role == Role.MANAGER)
                    managers.Add((Manager)user);
            }
            return managers;
        }

        public ObservableCollection<Line> GetAllLinesTable()
        {
            ObservableCollection<Line> linesTable = new ObservableCollection<Line>();
            foreach (Line line in mockRepository.Lines.Values)
                linesTable.Add(line);
            return linesTable;
        }

        internal ObservableCollection<Ride> GetAllRidesTable()
        {
            ObservableCollection<Ride> rides = new ObservableCollection<Ride>();
            foreach (Ride ride in mockRepository.Rides.Values)
                rides.Add(ride);
            return rides;
        }

        internal ObservableCollection<TicketTable> GetLoggedClientTicketsTable()
        {
            ObservableCollection<TicketTable> ticketTables = new ObservableCollection<TicketTable>();
            foreach(Ticket ticket in GetLoggedClientTickets())
            {
                TicketTable ticketTable = new TicketTable(ticket);
                ticketTables.Add(ticketTable);
            }
            return ticketTables;
        }

        public ObservableCollection<Station> GetAllStationsTable()
        {
            ObservableCollection<Station> stationsTable = new ObservableCollection<Station>();
            foreach(Station station in mockRepository.Stations.Values)
                stationsTable.Add(station);
            return stationsTable;
        }

        public List<Ticket> GetLoggedClientTickets()
        {
            Client clientLogged = (Client)mockRepository.LoggedUser;
            return clientLogged.Tickets;
        }

        public User GetLoggedUser()
        {
            return mockRepository.LoggedUser;
        }

        internal void DeleteLine(Line line)
        {
            throw new NotImplementedException();
        }

        public List<Station> GetAllStations()
        {
            return mockRepository.Stations.Values.ToList();
        }
        public List<Line> GetAllLines()
        {
            return mockRepository.Lines.Values.ToList();
        }
        public List<Train> GetAllTrains()
        {
            return mockRepository.Trains.Values.ToList();
        }

        public ObservableCollection<TicketTable> GetTicketsTableByMonthIndex(int selectedIndex)
        {
            ObservableCollection<TicketTable> ticketTables= new ObservableCollection<TicketTable>();
            foreach (Ticket ticket in mockRepository.Tickets.Values)
            {
                if(ticket.PurchaseDate.Month == (selectedIndex+1))
                    ticketTables.Add(new TicketTable(ticket));
            }
            return ticketTables;
        }

        public void DeleteRide(Ride ride)
        {
            foreach (Ticket ticket in mockRepository.Rides[ride.Id].Tickets)
                mockRepository.Tickets.Remove(ticket.Id);
            mockRepository.Rides[ride.Id].Train.Rides.Remove(ride);
            mockRepository.Rides.Remove(ride.Id);
        }

        public Station AddStation(string stationName, Location stationLocation)
        {
            return mockRepository.AddNewStation(stationName, stationLocation);
        }

        public ObservableCollection<RideTable> GetRidesTable()
        {
            ObservableCollection<RideTable> rideTables = new ObservableCollection<RideTable>();
            foreach(Ride rine in mockRepository.Rides.Values)
                rideTables.Add(new RideTable(rine));
            return rideTables;
        }

        public Ride AddRide(TimeSpan departureTime, TimeSpan arrivalTime, Train train, Line line, double price)
        {
            return mockRepository.AddNewRide(departureTime,arrivalTime,train,line,price);
        }

        public Ride AddReturnRide(Ride ride)
        {
            Line line = null;
            foreach (Line lineTemp in mockRepository.Lines.Values)
            {
                if (lineTemp.DepartureStation.Id == ride.Line.ArrivalStation.Id && lineTemp.ArrivalStation.Id == ride.Line.DepartureStation.Id)
                {
                    line = lineTemp;
                    break;
                }
            }
            if (line == null)
                line = mockRepository.AddNewLine(ride.Line.ArrivalStation, ride.Line.DepartureStation);
            return mockRepository.AddNewRide(ride.DepartureTime,ride.ArrivalTime,ride.Train,line,ride.Price);
            

        }

        internal double GetTicketsTotalByMonthIndex(int selectedIndex)
        {
            double total = 0;
            foreach(Ticket ticket in mockRepository.Tickets.Values)
            {
                if (ticket.PurchaseDate.Month == (selectedIndex + 1))
                    total += ticket.Price;
            }
          
            return total;
        }

        public ObservableCollection<Train> GetAllTrainsTable()
        {
            ObservableCollection<Train> trainTable = new ObservableCollection<Train>();
            foreach (Train train in GetAllTrains())
            {
                trainTable.Add(train);
            }
            return trainTable;
        }

        public void DeleteStation(Station station)
        {
            mockRepository.Stations.Remove(station.Id);
        }

        public List<Ride> GetAllRides()
        {
            return mockRepository.Rides.Values.ToList();
        }

        public bool Login(string username, string password)
        {
            if (username == null || password == null) return false;
            if (mockRepository.Users.ContainsKey(username))
                if (mockRepository.Users[username].Password == password)
                {
                    mockRepository.LoggedUser=mockRepository.Users[username];
                    return true;
                }
            return false;
        }

        public void DeleteTrain(Train train)
        {
            mockRepository.Trains.Remove(train.SerialNumber);
            foreach(Ride ride in train.Rides)
                mockRepository.Rides.Remove(ride.Id);
        }

        public void AddTrain(Train newTrain)
        {
            mockRepository.Trains.Add(newTrain.SerialNumber, newTrain);
        }

        public Role GetLoggedUserType()
        {
            return mockRepository.LoggedUser.Role;
        }

        public void Logout()
        {
            mockRepository.LoggedUser=null;
        }

        public object GetTicketsTableByRideId(int id)
        {
            ObservableCollection<TicketTable> ticketTables = new ObservableCollection<TicketTable>();
            foreach (Ticket ticket in mockRepository.Rides[id].Tickets)
                ticketTables.Add(new TicketTable(ticket));
            return ticketTables;
        }

        public Tuple<double,double> GetTotalAndAvarageByRideId(int id)
        {
            double total = mockRepository.Rides[id].Tickets.Count* mockRepository.Rides[id].Price;
            double avarage = total / mockRepository.Rides[id].Tickets.Count;
            return new Tuple<double, double>(total, avarage);
            
        }
    }
}

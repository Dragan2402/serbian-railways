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

        public ObservableCollection<Station> GetOtherStations(Line line)
        {
            ObservableCollection<Station> otherStations = new ObservableCollection<Station>();
            foreach(Station station in mockRepository.Stations.Values)
                if( line.DepartureStation != station && line.ArrivalStation!=station && !line.InterStations.Contains(station))
                    otherStations.Add(station);
            return otherStations;
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

        public bool LineExists(ObservableCollection<Station> stationsInLine)
        {
            return mockRepository.LineExists(stationsInLine);
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
            foreach(Ride ride in line.Rides)
            {
                DeleteRide(ride);

            }
            mockRepository.Lines.Remove(line.Id);
        }

        public Line AddLine(ObservableCollection<Station> stationsSelected)
        {
            return mockRepository.AddNewLine(stationsSelected);
        }

        public ObservableCollection<RideTable> GetRidesByFilter(int numOfTickets, int selectedClass, Line line, DateTime selectedDate)
        {
            ObservableCollection<RideTable> ridesTable = new ObservableCollection<RideTable>();
            foreach(Ride ride in mockRepository.Lines[line.Id].Rides)
            {
                
                if (ride.HasFreeSeats(selectedDate, selectedClass, numOfTickets) && ride.DayOfWeeksThatDrives.Contains(selectedDate.DayOfWeek))
                {
                    ridesTable.Add(new RideTable(ride));
                }
            }
            return ridesTable;
        }

        public bool CheckSerialNumExists(int serialNumber)
        {
            foreach(Train train in mockRepository.Trains.Values)
                if(train.SerialNumber == serialNumber)
                    return true;

            return false;
        }

        public List<Station> GetAllStations()
        {
            return mockRepository.Stations.Values.ToList();
        }

        public ObservableCollection<RideTable> getAllRidesByLine(Line line)
        {
            ObservableCollection<RideTable> rides = new ObservableCollection<RideTable>();
            foreach(Ride ride in mockRepository.Lines[line.Id].Rides)
            {
                RideTable rideTable= new RideTable(ride);
                rides.Add(rideTable);
            }
            return rides;
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

        public void AddNewClient(Client clientNew)
        {
            mockRepository.Users.Add(clientNew.UserName, clientNew);
            
        }

        public bool CheckUsernameExists(string username)
        {
            foreach(User user in mockRepository.Users.Values)
            {
                if (user.UserName.ToLower().Equals(username.ToLower()))
                    return true;
            }
            return false;
        }

        public bool BuyReserveTicketByRideIdAndFilter(int RideId, int searchedNumberOfTickets, int searchedClass, Line searchedLine, DateTime searchedDate, Ticket.TicketsType ticketsType)
        {
            while(searchedNumberOfTickets != 0)
            {
                Tuple<int,int> carSeat=mockRepository.Rides[RideId].TakeSeat(searchedDate, searchedClass);
                if(carSeat.Item1 != 0 && carSeat.Item2 != 0)
                {
                    Ride ride = mockRepository.Rides[RideId];
                    Ticket ticketnNew = new Ticket(mockRepository.GetNextTicketID(), ride.Price, carSeat.Item1, carSeat.Item2, searchedDate, ride, mockRepository.GetLoggedClient(), ticketsType, searchedClass);
                    mockRepository.Tickets.Add(ticketnNew.Id,ticketnNew);
                    searchedNumberOfTickets--;

                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public void DeleteRide(Ride ride)
        {
            foreach (Ticket ticket in mockRepository.Rides[ride.Id].Tickets)
                mockRepository.Tickets.Remove(ticket.Id);
            mockRepository.Rides[ride.Id].Train.Rides.Remove(ride);
            mockRepository.Rides.Remove(ride.Id);
        }

        public bool TicketPassedById(int id)
        {
            Ticket ticket = mockRepository.Tickets[id];
            if (ticket.RideDateTime <= DateTime.Now)
                return true;
            return false;
        }

        public bool CancelTicketById(int id)
        {
            Ticket ticket = mockRepository.Tickets[id];
            if (ticket.Ride.FreeSeat(ticket))
            {
                ticket.Ride.Tickets.Remove(ticket);
                ticket.Client.Tickets.Remove(ticket);
                ticket.Ride = null;
                ticket.Client = null;
                mockRepository.Tickets.Remove(ticket.Id);
                return true;
            }
            else
            {
                return false;
            };

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

        public Ride AddRide(TimeSpan departureTime, TimeSpan arrivalTime, Train train, Line line, double price,List<DayOfWeek> dayOfWeeks)
        {
            return mockRepository.AddNewRide(departureTime,arrivalTime,train,line, price,dayOfWeeks);
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
            return mockRepository.AddNewRide(ride.DepartureTime,ride.ArrivalTime,ride.Train,line,ride.Price,ride.DayOfWeeksThatDrives);
            

        }

        internal Tuple<double,double> GetTicketsTotalAndAvarageByMonthIndex(int selectedIndex)
        {
            double total = 0;
            int count = 0;
            double avarage = 0;
            foreach(Ticket ticket in mockRepository.Tickets.Values)
            {
                if (ticket.PurchaseDate.Month == (selectedIndex + 1))
                {
                    total += ticket.Price;
                    count++;
                }
            }
            if (count > 0)
                avarage = total / count;
            total = Math.Round(total, 2, MidpointRounding.AwayFromZero);
            avarage = Math.Round(avarage, 2, MidpointRounding.AwayFromZero);
            return new Tuple<double,double>(total,avarage);
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
            foreach(Line line in station.Lines)
            {
              
                foreach(Ride ride in line.Rides)
                    {
                        foreach(Ticket ticket in ride.Tickets)
                        {
                            
                            
                            mockRepository.Tickets.Remove(ticket.Id);                            
                        }
                     
                        mockRepository.Rides.Remove(ride.Id); 
                    }

                    mockRepository.Lines.Remove(line.Id);
                
            }
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
            foreach (Ride ride in train.Rides)
            {
                foreach(Ticket ticket in ride.Tickets) {
                    ticket.Client.Tickets.Remove(ticket);
                    mockRepository.Tickets.Remove(ticket.Id);
                    
                }
                ride.Line.Rides.Remove(ride);
                mockRepository.Rides.Remove(ride.Id);

            }
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
            double avarage = 0;
            if (mockRepository.Rides[id].Tickets.Count>0)
                avarage = total / mockRepository.Rides[id].Tickets.Count;
            total = Math.Round(total, 2, MidpointRounding.AwayFromZero);
            avarage = Math.Round(avarage, 2, MidpointRounding.AwayFromZero);
            return new Tuple<double, double>(total, avarage);
            
        }
    }
}

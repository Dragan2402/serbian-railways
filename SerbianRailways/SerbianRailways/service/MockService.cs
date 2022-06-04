using SerbianRailways.model;
using SerbianRailways.model.tableModels;
using SerbianRailways.repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.service
{
    public class MockService
    {
        private MockRepository mockRepository = null;

        public MockService()
        {
            mockRepository = new MockRepository();
        }

        public List<Client> getAllClients()
        {
            List<Client> clients = new List<Client>();
            foreach(User user in mockRepository.Users.Values)
            {
                if (user.Role == Role.CLIENT)
                    clients.Add((Client)user);
            }
            return clients;
        }

        public List<Ticket> getAllTickets()
        {
            return mockRepository.Tickets.Values.ToList();
        }
      
        public List<Manager> getAllManagers()
        {
            List<Manager> managers = new List<Manager>();
            foreach (User user in mockRepository.Users.Values)
            {
                if (user.Role == Role.MANAGER)
                    managers.Add((Manager)user);
            }
            return managers;
        }

        internal ObservableCollection<TicketTable> getLoggedClientTicketsTable()
        {
            ObservableCollection<TicketTable> ticketTables = new ObservableCollection<TicketTable>();
            foreach(Ticket ticket in getLoggedClientTickets())
            {
                TicketTable ticketTable = new TicketTable(ticket);
                ticketTables.Add(ticketTable);
            }
            return ticketTables;
        }

        public List<Ticket> getLoggedClientTickets()
        {
            Client clientLogged = (Client)mockRepository.LoggedUser;
            return clientLogged.Tickets;
        }

        public User getLoggedUser()
        {
            return mockRepository.LoggedUser;
        }

        public List<Station> getAllStations()
        {
            return mockRepository.Stations.Values.ToList();
        }
        public List<Line> getAllLines()
        {
            return mockRepository.Lines.Values.ToList();
        }
        public List<Train> getAllTrains()
        {
            return mockRepository.Trains.Values.ToList();
        }
        public ObservableCollection<Train> getAllTrainsTable()
        {
            ObservableCollection<Train> trainTable = new ObservableCollection<Train>();
            foreach (Train train in getAllTrains())
            {
                trainTable.Add(train);
            }
            return trainTable;
        }
        public List<Ride> getAllRides()
        {
            return mockRepository.Rides.Values.ToList();
        }

        public bool login(string username, string password)
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

        public void deleteTrain(Train train)
        {
            mockRepository.Trains.Remove(train.SerialNumber);
            foreach(Ride ride in train.Rides)
                mockRepository.Rides.Remove(ride.Id);
        }

        public void addTrain(Train newTrain)
        {
            mockRepository.Trains.Add(newTrain.SerialNumber, newTrain);
        }

        public Role getLoggedUserType()
        {
            return mockRepository.LoggedUser.Role;
        }

        public void logout()
        {
            mockRepository.LoggedUser=null;
        }
    }
}

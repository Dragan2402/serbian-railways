using SerbianRailways.model;
using SerbianRailways.repository;
using System;
using System.Collections.Generic;
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

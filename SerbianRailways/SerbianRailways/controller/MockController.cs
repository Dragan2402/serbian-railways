using SerbianRailways.model;
using SerbianRailways.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbianRailways.controller
{
    public class MockController
    {
        private MockRepository mockRepository = null;

        public MockController()
        {
            mockRepository = new MockRepository();
        }

        public List<Client> getAllClients()
        {
            return mockRepository.Clients;
        }

        public List<Ticket> getAllTickets()
        {
            return mockRepository.Tickets;
        }
      
        public List<Manager> getAllManagers()
        {
            return mockRepository.Managers;
        }
        public List<Station> getAllStations()
        {
            return mockRepository.Stations;
        }
        public List<Line> getAllLines()
        {
            return mockRepository.Lines;
        }
        public List<Train> getAllTrains()
        {
            return mockRepository.Trains;
        }
        public List<Ride> getAllRides()
        {
            return mockRepository.Rides;
        }


    }
}

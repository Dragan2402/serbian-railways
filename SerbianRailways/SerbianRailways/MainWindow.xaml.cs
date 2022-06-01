using SerbianRailways.controller;
using SerbianRailways.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SerbianRailways
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MockController MockController=null;
        public MainWindow()
        {
            InitializeComponent();
            MockController = new MockController();
            foreach(Client client in MockController.getAllClients())
            {
                Console.WriteLine(client);
            }
            foreach (Manager manager in MockController.getAllManagers())
            {
                Console.WriteLine(manager);
            }
            foreach (model.Line line in MockController.getAllLines())
            {
                Console.WriteLine(line);
            }
            foreach (Ride ride in MockController.getAllRides())
            {
                Console.WriteLine(ride);
            }
            foreach (Station station in MockController.getAllStations())
            {
                Console.WriteLine(station);
            }
            foreach (Train train in MockController.getAllTrains())
            {
                Console.WriteLine(train);
            }
            foreach (Ticket ticket in MockController.getAllTickets())
            {
                Console.WriteLine(ticket);
            }

        }
    }
}

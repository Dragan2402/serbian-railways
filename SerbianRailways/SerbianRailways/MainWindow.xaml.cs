using SerbianRailways.authorization_pages;
using SerbianRailways.service;
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
        public MockService MockService=null;

        

        public MainWindow()
        {
            InitializeComponent();

            MockService = new MockService();
            foreach(Client client in MockService.getAllClients())
            {
                Console.WriteLine(client);
            }
            foreach (Manager manager in MockService.getAllManagers())
            {
                Console.WriteLine(manager);
            }
            foreach (model.Line line in MockService.getAllLines())
            {
                Console.WriteLine(line);
            }
            foreach (Ride ride in MockService.getAllRides())
            {
                Console.WriteLine(ride);
            }
            foreach (Station station in MockService.getAllStations())
            {
                Console.WriteLine(station);
            }
            foreach (Train train in MockService.getAllTrains())
            {
                Console.WriteLine(train);
            }
            foreach (Ticket ticket in MockService.getAllTickets())
            {
                Console.WriteLine(ticket);
            }

            Main.Content = new Login(MockService,Main,this);
        }


    }
}

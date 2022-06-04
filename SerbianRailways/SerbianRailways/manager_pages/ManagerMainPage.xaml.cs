using SerbianRailways.authorization_pages;
using SerbianRailways.service;
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

namespace SerbianRailways.manager_pages
{
    /// <summary>
    /// Interaction logic for ManagerMainPage.xaml
    /// </summary>
    public partial class ManagerMainPage : Page
    {
        public string LoggedUserUsername { get; set; }
        public string LoggedUserName { get; set; }
        public string LoggedUserAddress { get; set; }




        private MockService MockService { get; set; }
        Frame main_frame;
        Window main_window { get; set; }
        public ManagerMainPage(MockService mockService, Frame mainFrame,Window window)
        {
            InitializeComponent();

            this.DataContext = this;
            MockService = mockService;
            LoggedUserUsername = "Korisničko ime: " + MockService.GetLoggedUser().UserName;
            LoggedUserAddress = "Adresa: " + MockService.GetLoggedUser().Address.ToString();
            LoggedUserName = "Ime: " + MockService.GetLoggedUser().Name + " " + mockService.GetLoggedUser().Surname;
            main_frame = mainFrame;
            
            main_window = window;
            main_window.Title = "Srbija Voz";
            window.CommandBindings.Clear();

            RoutedCommand toggleTrainsCMD = new RoutedCommand();
            toggleTrainsCMD.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Alt));
            window.CommandBindings.Add(new CommandBinding(toggleTrainsCMD, ToggleTrainsCRUDPageSC));

            RoutedCommand toggleStationsCMD = new RoutedCommand();
            toggleStationsCMD.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Alt));
            window.CommandBindings.Add(new CommandBinding(toggleStationsCMD, ToggleStationsCRUDPageSC));

            RoutedCommand toggleTicketsCMD = new RoutedCommand();
            toggleTicketsCMD.InputGestures.Add(new KeyGesture(Key.K, ModifierKeys.Alt));
            window.CommandBindings.Add(new CommandBinding(toggleTicketsCMD, ToggleTicketsCRUDPageSC));

            RoutedCommand toggleLinesCMD = new RoutedCommand();
            toggleLinesCMD.InputGestures.Add(new KeyGesture(Key.L, ModifierKeys.Alt));
            window.CommandBindings.Add(new CommandBinding(toggleLinesCMD, ToggleLinesCRUDPageSC));

            RoutedCommand toggleRidesCMD = new RoutedCommand();
            toggleRidesCMD.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Alt));
            window.CommandBindings.Add(new CommandBinding(toggleRidesCMD, ToggleRidesCRUDPageSC));


        }


        public void Log_out(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da se odjavite?",
                    "Odjava",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                main_frame.Content = new Login(MockService, main_frame,main_window);
            }

        }

        private void ToggleTrainsCRUDPageBTN(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new TrainsPage(MockService, main_frame, main_window);
        }
        private void ToggleTicketsCRUDPageBTN(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new TicketsPage(MockService, main_frame, main_window);
        }
        private void ToggleStationsCRUDPageBTN(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new StationsPage(MockService, main_frame, main_window);
        }
        private void ToggleLinesCRUDPageBTN(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new LinesPage(MockService, main_frame, main_window);
        }
        private void ToggleRidesCRUDPageBTN(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new RidesPage(MockService, main_frame, main_window);
        }

        private void ToggleTrainsCRUDPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content= new TrainsPage(MockService, main_frame, main_window);
        }

        private void ToggleTicketsCRUDPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content = new TicketsPage(MockService, main_frame, main_window);
        }

        private void ToggleStationsCRUDPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content = new StationsPage(MockService, main_frame, main_window);
        }

        private void ToggleLinesCRUDPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content = new LinesPage(MockService, main_frame, main_window);
        }

        private void ToggleRidesCRUDPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content = new RidesPage(MockService, main_frame, main_window);
        }


    }
}

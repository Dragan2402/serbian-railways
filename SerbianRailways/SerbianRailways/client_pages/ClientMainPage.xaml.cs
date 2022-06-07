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

namespace SerbianRailways.client_pages
{
    /// <summary>
    /// Interaction logic for ClientMainPage.xaml
    /// </summary>
    public partial class ClientMainPage : Page
    {
        public string LoggedUserUsername { get; set; }
        public string LoggedUserName { get; set; }
        public string LoggedUserAddress { get; set; }

        private MockService MockService { get; set; }
        Frame main_frame;
        Window main_window { get; set; }
        public ClientMainPage(MockService mockService, Frame mainFrame , Window window)
        {
            InitializeComponent();
            this.DataContext = this;
            MockService = mockService;
            LoggedUserUsername ="Korisničko ime: "+ MockService.GetLoggedUser().UserName;
            LoggedUserAddress = "Adresa: " + MockService.GetLoggedUser().Address.ToString();
            LoggedUserName = "Ime: " + MockService.GetLoggedUser().Name+" "+mockService.GetLoggedUser().Surname;
            main_frame = mainFrame;
            main_window= window;
            main_window.Title = "Srbija Voz";
            window.CommandBindings.Clear();
        }

        public void log_out(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da se odjavite?",
                    "Odjava",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                main_frame.Content = new Login(MockService, main_frame,main_window);
            }
            
        }
        public void showMyTickets(object sender,RoutedEventArgs e)
        {
            main_frame.Content = new TicketsPreviewPage(MockService, main_frame,main_window);
        }

        private void RailwayGridBTNClick(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new RailwayGridPage(MockService, main_frame, main_window);
        }
    }
}

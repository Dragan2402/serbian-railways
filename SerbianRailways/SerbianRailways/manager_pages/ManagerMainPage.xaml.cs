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
            LoggedUserUsername = "Korisničko ime: " + MockService.getLoggedUser().UserName;
            LoggedUserAddress = "Adresa: " + MockService.getLoggedUser().Address.ToString();
            LoggedUserName = "Ime: " + MockService.getLoggedUser().Name + " " + mockService.getLoggedUser().Surname;
            main_frame = mainFrame;
            RoutedCommand newCmd = new RoutedCommand();
            newCmd.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Alt));
            main_window = window;
            window.CommandBindings.Clear();
            window.CommandBindings.Add(new CommandBinding(newCmd, ToggleTrainsCRUDPageSC));
           
    
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

        private void ToggleTrainsCRUDPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content= new TrainsPage(MockService, main_frame, main_window);
        }


    }
}

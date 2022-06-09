using SerbianRailways.client_pages;
using SerbianRailways.manager_pages;
using SerbianRailways.model;
using SerbianRailways.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SerbianRailways.authorization_pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private string _username;
        private string _password;

        public string Username
        {
            get { return _username; }
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }


        private MockService MockService { get; set; }
        Frame main_frame;

        Window main_window { get; set; }
        public Login(MockService mockService, Frame mainFrame,Window window)
        {
            InitializeComponent();
            this.DataContext = this;
            MockService = mockService;
            main_frame = mainFrame;
            mockService.Logout();
            main_window = window;
            main_window.Title = "Srbija Voz-Prijava";
            window.CommandBindings.Clear();
            username_tb.Focus();    
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Sign_in_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Username == null || Password == null || Username.Equals("") || Password.Equals(""))
            {
                MessageBox.Show("Molimo vas unesite sve potrebne podatke.", "Greška pri prijavljivanju", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                bool loggedIn = MockService.Login(Username, Password);

                if (loggedIn)
                {
                    //MessageBox.Show("", "Uspešno prijavljivanje", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (MockService.GetLoggedUserType() == Role.CLIENT)
                        main_frame.Content = new ClientMainPage(MockService, main_frame, main_window);
                    else
                        main_frame.Content = new ManagerMainPage(MockService, main_frame, main_window);
                    
                }
                else
                    MessageBox.Show("Molimo vas ispravne podatke.", "Greška pri prijavljivanju", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SignUp(object sender, RoutedEventArgs e)
        {
            Window regWindow= new RegistrationWindow(MockService, main_frame, main_window);
            regWindow.ShowDialog();
            regWindow.Focus();
        }
    }
}

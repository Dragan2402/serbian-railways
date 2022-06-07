using SerbianRailways.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SerbianRailways.utility;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Validation = SerbianRailways.utility.Validation;
using SerbianRailways.model;

namespace SerbianRailways.authorization_pages
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
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

        private string _name;

        public string FirstName
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }

        private string _lastname;

        public string LastName
        {
            get { return _lastname; }
            set
            {
                if (value != _lastname)
                {
                    _lastname = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        private string _street;

        public string Street
        {
            get { return _street; }
            set
            {
                if (value != _street)
                {
                    _street = value;
                    OnPropertyChanged("Street");
                }
            }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        
        

        private string _postalCode;

        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                if (value != _postalCode)
                {
                    _postalCode = value;
                    OnPropertyChanged("PostalCode");
                }
            }
        }

        private string _country;

        public string Country
        {
            get { return _country; }
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged("Country");
                }
            }
        }


        
        private MockService MockService { get; set; }
        Frame main_frame;

        Window main_window { get; set; }
        public RegistrationWindow(MockService mockService, Frame mainFrame, Window window)
        {
            InitializeComponent();
            this.DataContext = this;
            MockService = mockService;
            main_frame = mainFrame;
            mockService.Logout();
            main_window = window;
            this.Title = "Srbija Voz-Registracija";
            window.CommandBindings.Clear();
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void ReturnLoginPage(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Sign_up_btn_click(object sender, RoutedEventArgs e)
        {
             if(Username.Equals("") || Username==null || Password.Equals("") || Password == null || FirstName.Equals("") || FirstName == null ||
                LastName.Equals("") || LastName == null || Street.Equals("") || Street == null || City.Equals("") || City == null ||
                PostalCode.Equals("") || PostalCode == null || Country.Equals("") || Country == null)
            {
                MessageBox.Show("Molimo vas unesite sve potrebne podatke za registraciju.", "Greška pri registraciji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ValidateAll())
            {
                Address address=new Address(Street, City, PostalCode, Country);
                Client clientNew = new Client(Username, Password, FirstName, LastName, address);
                MockService.AddNewClient(clientNew);
                if(MessageBox.Show("Uspešna registracija.", "Registraciaj", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
        }

        private bool ValidateAll()
        {
            if (!Validation.IsAllLettersOrDigits(Username)){
                MessageBox.Show("Korisničko ime se mora sastojati samo iz slova i brojeva.", "Neispravno korisničko ime", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Username.Length < 5 || Username.Length > 15)
            {
                MessageBox.Show("Korisničko ime mora biti između 5 i 15 karaktera.", "Neispravno korisničko ime", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (MockService.CheckUsernameExists(Username))
            {
                MessageBox.Show("Korisničko ime već postoji.", "Neispravno korisničko ime", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Validation.IsAllLettersOrDigitsOrUnderscores(Password))
            {
                MessageBox.Show("Lozinka se mora sastojati samo iz slova i brojeva i _.", "Neispravna lozinka", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Username.Length < 5 || Username.Length > 15)
            {
                MessageBox.Show("Lozinka mora biti između 5 i 15 karaktera.", "Neispravna lozinka", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Validation.IsAllLetters(FirstName))
            {
                MessageBox.Show("Ime se mora sastojati samo iz slova.", "Neispravno ime", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Validation.IsAllLetters(LastName))
            {
                MessageBox.Show("Prezime se mora sastojati samo iz slova.", "Neispravno prezime", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Validation.IsAllDigits(PostalCode))
            {
                MessageBox.Show("Poštanski broj se mora sastojati samo iz cifara.", "Neispravan poštanski broj", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;

        }


        
    }
}

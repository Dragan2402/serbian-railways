using SerbianRailways.model;
using SerbianRailways.service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace SerbianRailways.manager_pages
{
    /// <summary>
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    {
        private MockService MockService { get; set; }
        private ObservableCollection<Station> Stations { get; set; }

        private Location StationLocation { get; set; }

        private string _name;

        public string StationName
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("StationName");
                }
            }
        }

        public AddStationWindow(MockService mockService, ObservableCollection<Station> stations,Location location)
        {
            InitializeComponent();
            this.DataContext = this;
            DataContext = this;
            MockService = mockService;
            Stations = stations;
            StationLocation = location;
            RoutedCommand addNewStationSC = new RoutedCommand();
            addNewStationSC.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(addNewStationSC, AddStationSC));

            RoutedCommand cancelCMD = new RoutedCommand();
            cancelCMD.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(cancelCMD, CancelSC));

            name_tb.Focus();
        }


        private void CancelSC(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }


        private void AddStationSC(object sender, ExecutedRoutedEventArgs e)
        {
            if (StationName == null || StationName.Equals("") )
            {
                MessageBox.Show("Molimo vas unesite sve potrebne podatke.", "Greška pri dodavanju stanice", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                
                Stations.Add(MockService.AddStation(StationName,StationLocation));
         
                if (MessageBox.Show("Stanica uspešno dodata",
                    "Dodavanje Stanice",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Add_station_Btn(object sender, RoutedEventArgs e)
        {
            if (StationName == null || StationName.Equals(""))
            {
                MessageBox.Show("Molimo vas unesite sve potrebne podatke.", "Greška pri dodavanju stanice", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                Stations.Add(MockService.AddStation(StationName, StationLocation));

                if (MessageBox.Show("Stanica uspešno dodata",
                    "Dodavanje Stanice",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    
                    this.Close();
                }
            }
        }

        private void Cancel_Btn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

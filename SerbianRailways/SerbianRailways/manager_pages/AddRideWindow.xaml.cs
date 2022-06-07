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
    /// Interaction logic for AddRideWindow.xaml
    /// </summary>
    public partial class AddRideWindow : Window
    {
        private TimeSpan _departureTime;

        public TimeSpan DepartureTime
        {
            get { return _departureTime; }
            set
            {
                if (value != _departureTime)
                {
                    _departureTime = value;
                    OnPropertyChanged("DepartureTime");
                }
            }
        }

        private TimeSpan _arrivalTime;

        public TimeSpan ArrivalTime
        {
            get { return _arrivalTime; }
            set
            {
                if (value != _arrivalTime)
                {
                    _arrivalTime = value;
                    OnPropertyChanged("ArrivalTime");
                }
            }
        }

        private double _price;

        public double Price
        {
            get { return _price; }
            set
            {
                if (value != _price)
                {
                    _price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        public List<Train> Trains { get; set; }

        public List<model.Line> Lines { get; set; }
        private MockService MockService { get; set; }
        private ObservableCollection<Ride> Rides { get; set; }

        public AddRideWindow(MockService mockService, ObservableCollection<Ride> rides)
        {
            InitializeComponent();
            this.DataContext = this;
            DataContext = this;
            MockService = mockService;
            Rides = rides;

            Trains = mockService.GetAllTrains();
            Lines = mockService.GetAllLines();

            trainsCMBX.SelectedItem = Trains.ElementAt(0);
            trainsCMBX.SelectedIndex = 0;

            linesCMBX.SelectedItem = Lines.ElementAt(0);
            linesCMBX.SelectedIndex = 0;

            RoutedCommand addNewRidecmd = new RoutedCommand();
            addNewRidecmd.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(addNewRidecmd, AddRideSC));

            RoutedCommand cancelCMD = new RoutedCommand();
            cancelCMD.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(cancelCMD, CancelSC));

        }



        private void AddRideSC(object sender, ExecutedRoutedEventArgs e)
        {
            List<DayOfWeek> dayOfWeeksThatRides = GetListOfDaysThatDrives();
            if (DepartureTime == null || DepartureTime.Equals("") || ArrivalTime == null || ArrivalTime.Equals("") || Price == 0 || Price.Equals("") || dayOfWeeksThatRides.Count==0)
            {
                MessageBox.Show("Molimo vas unesite sve potrebne podatke.", "Greška pri dodavanju vožnje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                model.Line line = (model.Line)linesCMBX.SelectedItem;
                Train train = (Train)trainsCMBX.SelectedItem;

                Ride ride = MockService.AddRide(DepartureTime, ArrivalTime, train, line, Price,dayOfWeeksThatRides);
                Rides.Add(ride);

                if ((bool)returnRide.IsChecked)
                {
                    Rides.Add(MockService.AddReturnRide(ride));
                }

                if (MessageBox.Show("Vožnja uspešno dodata",
                    "Dodavanje Vožnje",
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

        private void CancelSC(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel_Btn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddRideBtn(object sender, RoutedEventArgs e)
        {
            List<DayOfWeek> dayOfWeeksThatRides = GetListOfDaysThatDrives();
            if (DepartureTime == null || DepartureTime.Equals("") || ArrivalTime == null || ArrivalTime.Equals("") || Price == 0 || Price.Equals("") || dayOfWeeksThatRides.Count==0)
            {
                MessageBox.Show("Molimo vas unesite sve potrebne podatke.", "Greška pri dodavanju vožnje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                model.Line line = (model.Line)linesCMBX.SelectedItem;
                Train train = (Train)trainsCMBX.SelectedItem;

                
                Ride ride = MockService.AddRide(DepartureTime, ArrivalTime, train, line, Price,dayOfWeeksThatRides);
                Rides.Add(ride);

                if ((bool)returnRide.IsChecked)
                {
                    Rides.Add(MockService.AddReturnRide(ride));
                }

                if (MessageBox.Show("Vožnja uspešno dodata",
                    "Dodavanje Vožnje",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
        }

        private List<DayOfWeek> GetListOfDaysThatDrives()
        {
            List<DayOfWeek> dayOfWeeks = new List<DayOfWeek>();
            if ((bool)mondayCB.IsChecked)
                dayOfWeeks.Add(DayOfWeek.Monday);
            if ((bool)tuesdayCB.IsChecked)
                dayOfWeeks.Add(DayOfWeek.Tuesday);
            if ((bool)wednesdayCB.IsChecked)
                dayOfWeeks.Add(DayOfWeek.Wednesday);
            if ((bool)thursdayCB.IsChecked)
                dayOfWeeks.Add(DayOfWeek.Thursday);
            if ((bool)fridayCB.IsChecked)
                dayOfWeeks.Add(DayOfWeek.Friday);
            if ((bool)saturdayCB.IsChecked)
                dayOfWeeks.Add(DayOfWeek.Saturday);
            if ((bool)sundayCB.IsChecked)
                dayOfWeeks.Add(DayOfWeek.Sunday);
            return dayOfWeeks;
        }
    }
}

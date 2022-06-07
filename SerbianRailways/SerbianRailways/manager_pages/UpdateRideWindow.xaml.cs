using SerbianRailways.model;
using SerbianRailways.service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UpdateRideWindow.xaml
    /// </summary>
    public partial class UpdateRideWindow : Window
    {
        private MockService MockService { get; set; }
        private ObservableCollection<Ride> Rides { get; set; }

        public List<Train> Trains { get; set; }

        public List<model.Line> Lines { get; set; }
        private Ride Ride { get; set; }

        public UpdateRideWindow(MockService mockService, ObservableCollection<Ride> rides,Ride ride)
        {
            InitializeComponent();
            this.DataContext = this;
            DataContext = this;
            MockService = mockService;
            Rides = rides;
            Ride = ride;
            Trains=mockService.GetAllTrains();
            Lines = mockService.GetAllLines();

            trainsCMBX.SelectedItem = ride.Train;
            trainsCMBX.SelectedIndex = Trains.IndexOf(ride.Train);

            linesCMBX.SelectedItem = ride.Line;
            linesCMBX.SelectedIndex = Lines.IndexOf(ride.Line);

            RoutedCommand UpdateRideCMD = new RoutedCommand();
            UpdateRideCMD.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(UpdateRideCMD, UpdateRideSC));

            RoutedCommand cancelCMD = new RoutedCommand();
            cancelCMD.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(cancelCMD, cancelSC));

            foreach(DayOfWeek day in ride.DayOfWeeksThatDrives)
            {
                switch (day)
                {
                    case DayOfWeek.Monday:
                        mondayCB.IsChecked = true;
                        break;
                    case DayOfWeek.Tuesday:
                        tuesdayCB.IsChecked = true;
                        break;
                    case DayOfWeek.Wednesday:
                        wednesdayCB.IsChecked = true;
                        break;
                    case DayOfWeek.Thursday:
                        thursdayCB.IsChecked = true;
                        break;
                    case DayOfWeek.Friday:
                        fridayCB.IsChecked = true;
                        break;
                    case DayOfWeek.Saturday:
                        saturdayCB.IsChecked = true;
                        break;
                    case DayOfWeek.Sunday:
                        sundayCB.IsChecked=true;
                        break;
                }
            }

        }




        private void UpdateRideSC(object sender, ExecutedRoutedEventArgs e)
        {

            Train train = (Train)trainsCMBX.SelectedItem;
            Ride.Train.Rides.Remove(Ride);
            Ride.Train = train;
            train.Rides.Add(Ride);

            model.Line line = (model.Line)linesCMBX.SelectedItem;
            Ride.Line.Rides.Remove(Ride);
            Ride.Line = line;
            line.Rides.Add(Ride);


            MessageBox.Show("Uspešno izmenjena vožnja.", "Srbija voz-Izmena vožnje", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void cancelSC(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel_Btn(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void UpdateRideBtn(object sender, RoutedEventArgs e)
        {
            List<DayOfWeek> dayOfWeeks=GetListOfDaysThatDrives();
            if (dayOfWeeks.Count == 0)
            {
                MessageBox.Show("Molimo vas unesite sve potrebne podatke.", "Greška pri izmeni vožnje", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Train train = (Train)trainsCMBX.SelectedItem;
            Ride.Train.Rides.Remove(Ride);
            Ride.Train = train;
            train.Rides.Add(Ride);

            model.Line line = (model.Line)linesCMBX.SelectedItem;
            Ride.Line.Rides.Remove(Ride);
            Ride.Line = line;
            line.Rides.Add(Ride);            

            Ride.DayOfWeeksThatDrives=dayOfWeeks;
            Ride.GenerateWhenDrivesString();
            MessageBox.Show("Uspešno izmenjena vožnja.", "Srbija voz-Izmena vožnje", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}

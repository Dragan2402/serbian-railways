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
            cancelCMD.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(cancelCMD, cancelSC));

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


            MessageBox.Show("Uspešno ažurirana vožnja.", "Srbija voz-Ažuriranje vožnje", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void UpdateRideBtn(object sender, RoutedEventArgs e)
        {
            
            Train train = (Train)trainsCMBX.SelectedItem;
            Ride.Train.Rides.Remove(Ride);
            Ride.Train = train;
            train.Rides.Add(Ride);

            model.Line line = (model.Line)linesCMBX.SelectedItem;
            Ride.Line.Rides.Remove(Ride);
            Ride.Line = line;
            line.Rides.Add(Ride);            


            MessageBox.Show("Uspešno ažurirana vožnja.", "Srbija voz-Ažuriranje vožnje", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}

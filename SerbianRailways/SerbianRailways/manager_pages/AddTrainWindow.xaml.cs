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
    /// Interaction logic for AddTrainWindow.xaml
    /// </summary>
    public partial class AddTrainWindow : Window
    {

        private string _serialNumber;
        private string _name;
        private string _seats;

        public string SerialNumber
        {
            get { return _serialNumber; }
            set
            {
                if (value != _serialNumber)
                {
                    _serialNumber = value;
                    OnPropertyChanged("SerialNumber");
                }
            }
        }
        public string TrainName
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("TrainName");
                }
            }
        }
        public string Seats
        {
            get { return _seats; }
            set
            {
                if (value != _seats)
                {
                    _seats = value;
                    OnPropertyChanged("Seats");
                }
            }
        }

        private MockService MockService { get; set; }
        private ObservableCollection<Train> Trains { get; set; }

        public AddTrainWindow(MockService mockService,ObservableCollection<Train> trains)
        {
            InitializeComponent();
            this.DataContext = this;
            DataContext = this;
            MockService = mockService;
            Trains=trains;

            RoutedCommand addNewTrain = new RoutedCommand();
            addNewTrain.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(addNewTrain, addTrainSC));

            RoutedCommand cancelCMD = new RoutedCommand();
            cancelCMD.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(cancelCMD, cancelSC));

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private void addTrainSC(object sender, ExecutedRoutedEventArgs e)
        {
            if (SerialNumber == null || SerialNumber.Equals("") || TrainName == null || TrainName.Equals("") || Seats == null || Seats.Equals(""))
            {
                MessageBox.Show("Molimo vas unesite sve potrebne podatke.", "Greška pri dodavanju voza", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Train newTrain = new Train(int.Parse(SerialNumber), TrainName, int.Parse(Seats));
                Trains.Add(newTrain);
                MockService.AddTrain(newTrain);
                if (MessageBox.Show("Voz uspešno dodat",
                    "Dodavanje Voza",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }

        }

        private void cancelSC(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_train_btn(object sender, RoutedEventArgs e)
        {

            if (SerialNumber == null || SerialNumber.Equals("") || TrainName == null || TrainName.Equals("") || Seats == null || Seats.Equals(""))
            {
                MessageBox.Show("Molimo vas unesite sve potrebne podatke.", "Greška pri dodavanju voza", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Train newTrain = new Train(int.Parse(SerialNumber), TrainName, int.Parse(Seats));
                Trains.Add(newTrain);
                MockService.AddTrain(newTrain);
                if (MessageBox.Show("Voz uspešno dodat",
                    "Dodavanje Voza",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
        }
    }
}

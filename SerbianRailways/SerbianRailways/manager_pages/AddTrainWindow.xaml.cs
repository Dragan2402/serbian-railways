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

        private int _serialNumber;
        private string _name;
        private int _cars;

        public int SerialNumber
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
        public int Cars
        {
            get { return _cars; }
            set
            {
                if (value != _cars)
                {
                    _cars = value;
                    OnPropertyChanged("Cars");
                }
            }
        }

        private int _firstClass;
        public int FirstClass
        {
            get { return _firstClass; }
            set
            {
                if (value != _firstClass)
                {
                    _firstClass = value;
                    OnPropertyChanged("FirstClass");
                }
            }
        }
        private int _secondClass;
        public int SecondClass
        {
            get { return _secondClass; }
            set
            {
                if (value != _secondClass)
                {
                    _secondClass = value;
                    OnPropertyChanged("SecondClass");
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
            this.CommandBindings.Add(new CommandBinding(addNewTrain, AddTrainSC));

            RoutedCommand cancelCMD = new RoutedCommand();
            cancelCMD.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(cancelCMD, cancelSC));

            SerialNumber_tb.Focus();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private void AddTrainSC(object sender, ExecutedRoutedEventArgs e)
        {
            if ( SerialNumber.Equals("") ||  TrainName == null || TrainName.Equals("") || Cars.Equals("") || SecondClass.Equals("") || FirstClass.Equals("") || Cars.Equals(""))
            {
                MessageBox.Show("Molimo vas unesite sve potrebne podatke.", "Greška pri dodavanju voza", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (SerialNumber <= 0 || Cars<=0 || SecondClass <=0 || FirstClass <=0)
                {
                    MessageBox.Show("Molimo vas unesite sve potrebne podatke ispravno.", "Greška pri dodavanju voza", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MockService.CheckSerialNumExists(SerialNumber))
                {
                    MessageBox.Show("Serijski broj voza već postoji u bazi.", "Greška pri dodavanju voza", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Train newTrain = new Train(SerialNumber, TrainName, Cars,FirstClass,SecondClass);
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

        private void Cancel_Btn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_train_btn(object sender, RoutedEventArgs e)
        {

            if (SerialNumber.Equals("") || TrainName == null || TrainName.Equals("") || Cars.Equals("") || SecondClass.Equals("") || FirstClass.Equals("") || Cars.Equals(""))
            {
                MessageBox.Show("Molimo vas unesite sve potrebne podatke.", "Greška pri dodavanju voza", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (SerialNumber <= 0 || Cars <= 0 || SecondClass <= 0 || FirstClass <= 0)
                {
                    MessageBox.Show("Molimo vas unesite sve potrebne podatke ispravno.", "Greška pri dodavanju voza", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (MockService.CheckSerialNumExists(SerialNumber))
                {
                    MessageBox.Show("Serijski broj voza već postoji u bazi.", "Greška pri dodavanju voza", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Train newTrain = new Train(SerialNumber, TrainName, Cars, FirstClass, SecondClass);
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

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

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Add_train_btn(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(SerialNumber + " " + TrainName + " " + Seats);
        }
    }
}

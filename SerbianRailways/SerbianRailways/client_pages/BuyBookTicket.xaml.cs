using SerbianRailways.help_pages;
using SerbianRailways.model.tableModels;
using SerbianRailways.service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using static SerbianRailways.model.Ticket;

namespace SerbianRailways.client_pages
{
    /// <summary>
    /// Interaction logic for BuyBookTicket.xaml
    /// </summary>
    public partial class BuyBookTicket : Page
    {
        private MockService MockService { get; set; }
        Frame main_frame;
        Window main_window { get; set; }
        public List<model.Line> Lines { get; set; }

        public List<int> Classes { get; set; }

        private int numOftickets;


        private int SearchedNumberOfTickets { get; set; }
        private model.Line SearchedLine { get; set; }
        private int SearchedClass { get; set; }

        private DateTime SearchedDate { get; set; }


        public int NumOfTickets
        {
            get { return numOftickets; }
            set
            {
                if (value != numOftickets)
                {
                    numOftickets = value;
                    OnPropertyChanged("NumOfTickets");
                }
            }
        }

        public BuyBookTicket(MockService mockService, Frame mainFrame, Window window)
        {
            InitializeComponent();
            this.DataContext = this;
            MockService = mockService;

            main_frame = mainFrame;
            main_window = window;
            main_window.Title = "Srbija Voz-Kupovina/Rezervacija Karte";
            //window.CommandBindings.Clear();

            RoutedCommand openHelpPage = new RoutedCommand();
            openHelpPage.InputGestures.Add(new KeyGesture(Key.F1, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(openHelpPage, ToggleHelpPageSC));
            ((MainWindow)System.Windows.Application.Current.MainWindow).HelpMenuItem.IsEnabled = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).HelpMenuItem.Command = openHelpPage;

            ticketDatePicker.Text = "Datum";
            ticketDatePicker.ToolTip = "Odaberite datum vožnje";
            ticketDatePicker.SelectedDate = DateTime.Today;
            ticketDatePicker.DisplayDateStart = DateTime.Today;
            ticketDatePicker.DisplayDateEnd = new DateTime(2022,12,31);
            ticketDatePicker.FirstDayOfWeek = DayOfWeek.Monday;
            NumOfTickets = 1;
            numOfTickets_tb.Text="1";

            Lines = mockService.GetAllLines();
            linesCMBX.SelectedItem = Lines.ElementAt(0);
            linesCMBX.SelectedIndex = 0;

            Classes = new List<int>();

            Classes.Add(1);
            Classes.Add(2);

            classCMBX.SelectedItem = Classes.First();
            classCMBX.SelectedIndex=0;

        }

        private void ReturnClientPage(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new ClientMainPage(MockService, main_frame, main_window);
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void SearchRidesBTN(object sender, RoutedEventArgs e)
        {
            if (NumOfTickets.Equals(""))
            {
                MessageBox.Show("Molimo vas unesite sve potrebne podatke.", "Greška pri pretraživanju karata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (NumOfTickets <= 0 )
                {
                    MessageBox.Show("Molimo vas unesite sve potrebne podatke ispravno.", "Greška pri pretraživanju karata", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int classSelected = (int)classCMBX.SelectedItem;
                model.Line selectedLine = (model.Line)linesCMBX.SelectedItem; 
                DateTime dateTime = (DateTime)ticketDatePicker.SelectedDate;
                if (dateTime == null)
                {
                    MessageBox.Show("Molimo vas unesite sve potrebne podatke ispravno.", "Greška pri pretraživanju karata", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                ObservableCollection<RideTable> ridesThatHaveFilteredData = MockService.GetRidesByFilter(NumOfTickets, classSelected, selectedLine, dateTime);

                if(ridesThatHaveFilteredData.Count == 0)
                {
                    MessageBox.Show("Za unesene kriterijume ne postoji vožnja.", "Pretraga karata", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                dgShowingRides.DataContext = ridesThatHaveFilteredData;

                SearchedClass = classSelected;
                SearchedDate = dateTime;
                SearchedLine = selectedLine;
                SearchedNumberOfTickets = NumOfTickets;
                    
            }
        }

        private void BuyTicketsBtn(object sender, RoutedEventArgs e)
        {
            if (dgShowingRides.SelectedItem == null)
            {
                return;
            }
            RideTable rideTable = dgShowingRides.SelectedItem as RideTable;

            bool status = MockService.BuyReserveTicketByRideIdAndFilter(rideTable.Id, SearchedNumberOfTickets, SearchedClass, SearchedLine, SearchedDate,TicketsType.BOUGHT);
            if (status)
            {
                if (MessageBox.Show("Uspešno kupljene karte", "Kupovina karata", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    Thread.Sleep(500);
                    main_frame.Content = new ClientMainPage(MockService, main_frame, main_window);
                }
            }
            else
            {
                if (MessageBox.Show("Neuspešno kupljene karte", "Kupovina karata", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Thread.Sleep(500);
                    main_frame.Content = new ClientMainPage(MockService, main_frame, main_window);
                }
            }
        }

        private void ReserveTicketsBtn(object sender, RoutedEventArgs e)
        {
            if(dgShowingRides.SelectedItem == null)
            {
                return;
            }
            RideTable rideTable = dgShowingRides.SelectedItem as RideTable;

            bool status = MockService.BuyReserveTicketByRideIdAndFilter(rideTable.Id, SearchedNumberOfTickets, SearchedClass, SearchedLine, SearchedDate,TicketsType.RESERVED);
            if (status)
            {
                if(MessageBox.Show("Uspešno rezervisane karte", "Rezervacija karata", MessageBoxButton.OK,MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    Thread.Sleep(500);
                    main_frame.Content = new ClientMainPage(MockService, main_frame, main_window);
                }
            }
            else
            {
                if (MessageBox.Show("Neuspešno rezervisane karte", "Rezervacija karata", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Thread.Sleep(500);
                    main_frame.Content = new ClientMainPage(MockService, main_frame, main_window);
                }
            }
        }

        private void ToggleHelpPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content = new HelpPage(main_frame, main_window, "clientBuyBookTicket", this);
        }
    }
}

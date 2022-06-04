using SerbianRailways.model.tableModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SerbianRailways.manager_pages
{
    /// <summary>
    /// Interaction logic for TicketsPage.xaml
    /// </summary>
    public partial class TicketsPage : Page
    {
        ObservableCollection<string> months = new ObservableCollection<string>() { "Januar","Februar","Mart","April","Maj","Jun","Jul","Avgust","Septembar","Oktobar","Novembar","Decembar"};
        
        ObservableCollection<TicketTable> Tickets = new ObservableCollection<TicketTable>();

        Point startPoint = new Point();


        private string selectedMonth;
        private int selectedIndex;



        public ObservableCollection<string> Months
        {
            get { return months; }
            set
            {
                months = value;
                OnPropertyChanged("Months");
            }
        }


        public string SelectedMonth
        {
            get { return selectedMonth; }
            set
            {
                selectedMonth = value;
                OnPropertyChanged("SelectedMonth");
            }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        private MockService MockService { get; set; }
        Frame main_frame;
        Window main_window { get; set; }
        public TicketsPage(MockService mockService, Frame mainFrame, Window window)
        {
            InitializeComponent();
            this.DataContext = this;
            MockService = mockService;
            main_frame = mainFrame;
            main_window = window;             
            
            SelectedIndex= 0;
            SelectedMonth = "Januar";


            Tickets = MockService.GetTicketsTableByMonthIndex(SelectedIndex);
            TotalLbl.Content = MockService.GetTicketsTotalByMonthIndex(SelectedIndex);
            dgTickets.DataContext = Tickets;
     
            dgRides.DataContext = MockService.GetRidesTable();


            window.CommandBindings.Clear();
            RoutedCommand newCmd = new RoutedCommand();
            newCmd.InputGestures.Add(new KeyGesture(Key.Back, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(newCmd, ReturnManagerPageSC));

            RoutedCommand monthlyReportSC = new RoutedCommand();
            monthlyReportSC.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(monthlyReportSC, MonthlyReportSC));

            RoutedCommand rideReportCMD = new RoutedCommand();
            rideReportCMD.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(rideReportCMD, RideReportSC));

        }
        private void MonthlyReportSC(object sender, ExecutedRoutedEventArgs e)
        {
            mainTab.SelectedIndex = 0;
        }

        private void RideReportSC(object sender, ExecutedRoutedEventArgs e)
        {
            mainTab.SelectedIndex = 1;
        }


        private void ReturnManagerPage(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new ManagerMainPage(MockService, main_frame, main_window);
        }
        private void ReturnManagerPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content = new ManagerMainPage(MockService, main_frame, main_window);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Refresh_Months_btn(object sender, RoutedEventArgs e)
        {
            Tickets = MockService.GetTicketsTableByMonthIndex(SelectedIndex);

            TotalLbl.Content = MockService.GetTicketsTotalByMonthIndex(SelectedIndex);
            dgTickets.DataContext = Tickets;
        }


        private void DGRides_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void DGRides_MouseMove(object sender, MouseEventArgs e)
        {
            if (startPoint == null)
                return;

            var dataGrid = sender as DataGrid;
            if (dataGrid == null) return;

            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;



            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem

                var DataGridItem =
                    FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

                if (DataGridItem == null)
                    return;

                // Find the data behind the ListViewItem
                RideTable rideTable = (RideTable)dataGrid.ItemContainerGenerator.
                    ItemFromContainer(DataGridItem);

                if (rideTable == null)
                    return;

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", rideTable);
                DragDrop.DoDragDrop(DataGridItem, dragData, DragDropEffects.Move);
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;


        }
        private void DGTickets_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DGTickets_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                RideTable rideTable = e.Data.GetData("myFormat") as RideTable;
                dgTicketsRide.DataContext = MockService.GetTicketsTableByRideId(rideTable.Id);
                Tuple<double,double> totalAndAvarage = MockService.GetTotalAndAvarageByRideId(rideTable.Id);
                TotalRideLbl.Content = totalAndAvarage.Item1;
                AvarageRideLbl.Content = totalAndAvarage.Item2;
            }
        }
    }
}

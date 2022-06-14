using Microsoft.Maps.MapControl.WPF;
using SerbianRailways.help_pages;
using SerbianRailways.model;
using SerbianRailways.service;
using SerbianRailways.utility;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SerbianRailways.manager_pages
{
    /// <summary>
    /// Interaction logic for StationsPage.xaml
    /// </summary>
    public partial class StationsPage : Page
    {
        private MockService MockService { get; set; }

        Point startPoint = new Point();

        Frame main_frame;
        Window main_window { get; set; }

        private Dictionary<Station,Pushpin> StationPins = new Dictionary<Station,Pushpin>();
        CommandBinding DeleteBinding { get; set; }

        private ObservableCollection<Station> Stations { get; set; }
        public StationsPage(MockService mockService, Frame mainFrame, Window window)
        {
            InitializeComponent();
            this.DataContext = this;
            MockService = mockService;
            main_frame = mainFrame;
            main_window = window;
            main_window.Title = "Srbija Voz-Upravljanje stanicama";
            RailGridMap.Focus();
            //window.CommandBindings.Clear();

            RoutedCommand mainMenuCMD = new RoutedCommand();
            mainMenuCMD.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(mainMenuCMD, MainMenuSc));

            ((MainWindow)System.Windows.Application.Current.MainWindow).MainMenuMenuItem.Command = mainMenuCMD;

            RoutedCommand deleteTrainsCMD = new RoutedCommand();
            deleteTrainsCMD.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));
            DeleteBinding = new CommandBinding(deleteTrainsCMD, DeleteStationsSC);
            window.CommandBindings.Add(DeleteBinding);

            RoutedCommand openHelpPage = new RoutedCommand();
            openHelpPage.InputGestures.Add(new KeyGesture(Key.F1, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(openHelpPage, ToggleHelpPageSC));
            ((MainWindow)System.Windows.Application.Current.MainWindow).HelpMenuItem.IsEnabled = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).HelpMenuItem.Command = openHelpPage;

            Stations = mockService.GetAllStationsTable();
            dgStations.DataContext = Stations;
            foreach(Station station in mockService.GetAllStations())
            {
                Pushpin pushpin = new Pushpin();
                ToolTip toolTip = new ToolTip();
                toolTip.Content = "Stanica "+station.Name;
                pushpin.Background = new SolidColorBrush(Color.FromArgb(100, 100, 100, 100));
                pushpin.ToolTip=toolTip;
                pushpin.Location = new Microsoft.Maps.MapControl.WPF.Location(station.Location.X, station.Location.Y);
                RailGridMap.Children.Add(pushpin);  
                StationPins.Add(station, pushpin);
             
            }
            RoutedCommand demoCMD = new RoutedCommand();
            demoCMD.InputGestures.Add(new KeyGesture(Key.F5, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(demoCMD, ToggleDemoSC));
            ((MainWindow)System.Windows.Application.Current.MainWindow).DemoMenuItem.IsEnabled = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).DemoMenuItem.Command = demoCMD;

        }

        private void ToggleDemoSC(object sender, ExecutedRoutedEventArgs e)
        {

            Window demoWindow = new DemoPlayerWindow("stations");
            demoWindow.ShowDialog();
        }

        private void ReturnManagerPage(object sender, RoutedEventArgs e)
        {
            main_window.CommandBindings.Remove(DeleteBinding);
            main_frame.Content = new ManagerMainPage(MockService, main_frame, main_window);
        }
        private void MainMenuSc(object sender, ExecutedRoutedEventArgs e)
        {
            main_window.CommandBindings.Remove(DeleteBinding);
            main_frame.Content = new ManagerMainPage(MockService, main_frame, main_window);
        }

        private void MapWithPushpins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Point mousePosition = e.GetPosition(this);
            Microsoft.Maps.MapControl.WPF.Location pinLocation = RailGridMap.ViewportPointToLocation(mousePosition);

            if (pinLocation.Latitude > Borders.NORTH_LATITUDE || pinLocation.Latitude < Borders.SOUTH_LATITUDE || pinLocation.Longitude > Borders.EAST_LONGITUDE || pinLocation.Longitude < Borders.WEST_LONGITUDE)
            {
                
                MessageBox.Show("Stanica izvan granica države.", "Greška pri dodavanju stanice", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int sizeBefore = Stations.Count; 
            Window addStationWindow = new AddStationWindow(MockService, Stations,new model.Location(pinLocation.Latitude,pinLocation.Longitude));
            addStationWindow.ShowDialog();
            addStationWindow.Focus();

            if (sizeBefore < Stations.Count)
            {
                
                Pushpin pin = new Pushpin();               

                ToolTip toolTip = new ToolTip();
                toolTip.Content = "Stanica " + Stations.Last().Name;
                pin.Background = new SolidColorBrush(Color.FromArgb(100, 100, 100, 100));
                pin.ToolTip = toolTip;
                pin.Location = pinLocation;
                RailGridMap.Children.Add(pin);
                StationPins.Add(Stations.Last(), pin);
            }
        }


        private void DeleteStationsBtn(object sender, RoutedEventArgs e)
        {
            if (dgStations.SelectedItems.Count == 0)
            {
                MessageBox.Show("Označite stanice za brisanje.", "Brisanje stanice", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            if (MessageBox.Show("Da li ste sigurni da želite da izbrišete označene stanice?",
                    "Brisanje stanica",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                List<Station> stationsToDelete = new List<Station>();
                foreach (Station station in dgStations.SelectedItems)
                {
                    MockService.DeleteStation(station);
                    stationsToDelete.Add(station);
                }
                foreach (Station station1 in stationsToDelete)
                {
                    RailGridMap.Children.Remove(StationPins[station1]);
                    StationPins.Remove(station1);
                    Stations.Remove(station1);

                }
            }
        }

        private void DeleteStationsSC(object sender, ExecutedRoutedEventArgs e)
        {
            if (dgStations.SelectedItems.Count == 0)
            {
                MessageBox.Show("Označite stanice za brisanje.", "Brisanje stanice", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Da li ste sigurni da želite da izbrišete označene stanice?",
                    "Brisanje stanica",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                List<Station> stationsToDelete = new List<Station>();
                foreach (Station station in dgStations.SelectedItems)
                {
                    MockService.DeleteStation(station);
                    stationsToDelete.Add(station);
                }
                foreach (Station station1 in stationsToDelete)
                {
                    RailGridMap.Children.Remove(StationPins[station1]);
                    StationPins.Remove(station1);
                    Stations.Remove(station1);

                }
            }
        }


        private void DGStations_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void DGStations_MouseMove(object sender, MouseEventArgs e)
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
                Station station = (Station)dataGrid.ItemContainerGenerator.
                    ItemFromContainer(DataGridItem);

                if (station == null)
                    return;

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", station);
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
        private void DeleteBTN_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DeleteBTN_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Station station= e.Data.GetData("myFormat") as Station;
                if (MessageBox.Show("Da li ste sigurni da želite da izbrišete označenu stanicu?",
                   "Brisanje stanice",
                   MessageBoxButton.YesNo,
                   MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    MockService.DeleteStation(station);
                    RailGridMap.Children.Remove(StationPins[station]);
                    StationPins.Remove(station);
                    Stations.Remove(station);

                }
            }
        }

        private void CellEditEndingEvent(object sender, DataGridCellEditEndingEventArgs e)
        {
            var el = e.EditingElement as TextBox;
            Station station = e.Row.Item as Station;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = "Stanica " + el.Text;
                StationPins[station].ToolTip = toolTip;
                RailGridMap.Children.Remove(StationPins[station]);
                RailGridMap.Children.Add(StationPins[station]); 
            }
        }

        private void ToggleHelpPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content = new HelpPage(main_frame, main_window, "managerStationsPage", this);
        }
    }
}

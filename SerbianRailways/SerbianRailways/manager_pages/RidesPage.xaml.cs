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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SerbianRailways.manager_pages
{
    /// <summary>
    /// Interaction logic for RidesPage.xaml
    /// </summary>
    public partial class RidesPage : Page
    {
        private MockService MockService { get; set; }
        Frame main_frame;
        Window main_window { get; set; }
        Point startPoint = new Point();

        ObservableCollection<Ride> Rides = new ObservableCollection<Ride>();
        CommandBinding AddBinding { get; set; }
        CommandBinding DeleteBinding { get; set; }
        CommandBinding UpdateBinding { get; set; }


        public RidesPage(MockService mockService, Frame mainFrame, Window window)
        {
            InitializeComponent();
            this.DataContext = this;
            MockService = mockService;
            main_frame = mainFrame;
            main_window = window;
            //window.CommandBindings.Clear();
            window.Title = "Srbija Voz-Upravljanje vožnjama";
            Rides = MockService.GetAllRidesTable();
            dgRides.DataContext = Rides;

            RoutedCommand mainMenuCMD = new RoutedCommand();
            mainMenuCMD.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(mainMenuCMD, MainMenuSc));

            ((MainWindow)System.Windows.Application.Current.MainWindow).MainMenuMenuItem.Command = mainMenuCMD;


            RoutedCommand addRideCMD = new RoutedCommand();
            addRideCMD.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            AddBinding = new CommandBinding(addRideCMD, AddRideSC);
            window.CommandBindings.Add(AddBinding);

            RoutedCommand deleteRidesCMD = new RoutedCommand();
            deleteRidesCMD.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));
            DeleteBinding = new CommandBinding(deleteRidesCMD, DeleteRidesSC);
            window.CommandBindings.Add(DeleteBinding);

            RoutedCommand updateRidesCMD = new RoutedCommand();
            updateRidesCMD.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            UpdateBinding = new CommandBinding(updateRidesCMD, UpdateRideSC);
            window.CommandBindings.Add(UpdateBinding);

            RoutedCommand demoCMD = new RoutedCommand();
            demoCMD.InputGestures.Add(new KeyGesture(Key.F5, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(demoCMD, ToggleDemoSC));
            ((MainWindow)System.Windows.Application.Current.MainWindow).DemoMenuItem.IsEnabled = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).DemoMenuItem.Command = demoCMD;

        }

        private void ToggleDemoSC(object sender, ExecutedRoutedEventArgs e)
        {

            Window demoWindow = new DemoPlayerWindow("rides");
            demoWindow.ShowDialog();
        }

        private void ReturnManagerPage(object sender, RoutedEventArgs e)
        {
            main_window.CommandBindings.Remove(DeleteBinding);
            main_window.CommandBindings.Remove(UpdateBinding);
            main_window.CommandBindings.Remove(AddBinding);
            main_frame.Content = new ManagerMainPage(MockService, main_frame, main_window);
        }
        private void MainMenuSc(object sender, ExecutedRoutedEventArgs e)
        {
            main_window.CommandBindings.Remove(DeleteBinding);
            main_window.CommandBindings.Remove(UpdateBinding);
            main_window.CommandBindings.Remove(AddBinding);
            main_frame.Content = new ManagerMainPage(MockService, main_frame, main_window);
        }

        private void AddRideSC(object sender, ExecutedRoutedEventArgs e)
        {

            Window addRideWindow = new AddRideWindow(MockService, Rides);
            addRideWindow.ShowDialog();
            addRideWindow.Focus();
        }

        private void AddRideBtn(object sender, RoutedEventArgs e)
        {
            Window addRideWindow = new AddRideWindow(MockService, Rides);
            addRideWindow.ShowDialog();
            addRideWindow.Focus();
        }


        private void DeleteRideBtn(object sender, RoutedEventArgs e)
        {
            if (dgRides.SelectedItems.Count == 0)
            {
                MessageBox.Show("Označite vožnje za brisanje.", "Brisanje vožnji", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (MessageBox.Show("Da li ste sigurni da želite da izbrišete označene vožnje i njihove aktivne karte?",
                    "Brisanje vožnji",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                List<Ride> ridesToDelete = new List<Ride>();
                foreach (Ride ride in dgRides.SelectedItems)
                {
                    MockService.DeleteRide(ride);
                    ridesToDelete.Add(ride);
                }
                foreach (Ride ride in ridesToDelete)
                {
                    Rides.Remove(ride);
                }
            }
        }

        private void DeleteRidesSC(object sender, ExecutedRoutedEventArgs e)
        {
            if (dgRides.SelectedItems.Count == 0)
            {
                MessageBox.Show("Označite vožnje za brisanje.", "Brisanje vožnji", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Da li ste sigurni da želite da izbrišete označene vožnje i njihove aktivne karte?",
                               "Brisanje vožnji",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                List<Ride> ridesToDelete = new List<Ride>();
                foreach (Ride ride in dgRides.SelectedItems)
                {
                    MockService.DeleteRide(ride);
                    ridesToDelete.Add(ride);
                }
                foreach (Ride ride in ridesToDelete)
                {
                    Rides.Remove(ride);
                }
            }
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
                Ride ride = (Ride)dataGrid.ItemContainerGenerator.
                    ItemFromContainer(DataGridItem);

                if (ride == null)
                    return;

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", ride);
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
                Ride ride = e.Data.GetData("myFormat") as Ride;
                if (MessageBox.Show("Da li ste sigurni da želite da izbrišete označenu vožnju i njene aktivne karte?",
                   "Brisanje vožnje",
                   MessageBoxButton.YesNo,
                   MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    MockService.DeleteRide(ride);
                    Rides.Remove(ride);

                }
            }
        
        }

        private void UpdateBTN_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Ride ride = e.Data.GetData("myFormat") as Ride;
                Window updateRideWindow = new UpdateRideWindow(MockService, Rides, ride);
                updateRideWindow.ShowDialog();
                
            }
            dgRides.Items.Refresh();
        }

        private void UpdateRideBtn(object sender, RoutedEventArgs e)
        {
            foreach (Ride ride in dgRides.SelectedItems)
            {
                Window updateRideWindow = new UpdateRideWindow(MockService, Rides, ride);
                updateRideWindow.ShowDialog();
                
            }
            dgRides.Items.Refresh();
            
        }

        private void UpdateRideSC(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (Ride ride in dgRides.SelectedItems)
            {
                Window updateRideWindow = new UpdateRideWindow(MockService, Rides, ride);
                updateRideWindow.ShowDialog();

            }
            dgRides.Items.Refresh();

        }
    }
}


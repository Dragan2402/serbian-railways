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
    /// Interaction logic for TrainsPage.xaml
    /// </summary>
    public partial class TrainsPage : Page
    {
        public ObservableCollection<Train> trains = new ObservableCollection<Train>();

        Point startPoint = new Point();

        CommandBinding AddBinding { get; set; }
        CommandBinding DeleteBinding { get; set; }

        private MockService MockService { get; set; }
        Frame main_frame;
        Window main_window { get; set; }
        public TrainsPage(MockService mockService, Frame mainFrame, Window window)
        {
            InitializeComponent();
            this.DataContext = this;
            MockService = mockService;
            main_frame = mainFrame;
            main_window = window;
            main_window.Title = "Srbija Voz-Upravljanje vozovima";
            //window.CommandBindings.Clear();


            RoutedCommand mainMenuCMD = new RoutedCommand();
            mainMenuCMD.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(mainMenuCMD, MainMenuSc));
            ((MainWindow)System.Windows.Application.Current.MainWindow).MainMenuMenuItem.Command = mainMenuCMD;
            trains = MockService.GetAllTrainsTable();
            dgTrains.DataContext = trains;


            RoutedCommand addTrainCMD = new RoutedCommand();
            addTrainCMD.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            AddBinding = new CommandBinding(addTrainCMD, AddTrainSC);
            window.CommandBindings.Add(AddBinding);

            RoutedCommand deleteTrainsCMD = new RoutedCommand();
            deleteTrainsCMD.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));
            DeleteBinding = new CommandBinding(deleteTrainsCMD, DeleteTrainsSC);
            window.CommandBindings.Add(DeleteBinding);

            RoutedCommand demoCMD = new RoutedCommand();
            demoCMD.InputGestures.Add(new KeyGesture(Key.F5, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(demoCMD, ToggleDemoSC));
            ((MainWindow)System.Windows.Application.Current.MainWindow).DemoMenuItem.IsEnabled = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).DemoMenuItem.Command = demoCMD;

        }

        private void ToggleDemoSC(object sender, ExecutedRoutedEventArgs e)
        {

            Window demoWindow = new DemoPlayerWindow("trains");
            demoWindow.ShowDialog();
        }

        private void ReturnManagerPage(object sender, RoutedEventArgs e)
        {
            main_window.CommandBindings.Remove(DeleteBinding);
            main_window.CommandBindings.Remove(AddBinding);
            main_frame.Content = new ManagerMainPage(MockService, main_frame, main_window);
        }
        private void MainMenuSc(object sender, ExecutedRoutedEventArgs e)
        {
            main_window.CommandBindings.Remove(DeleteBinding);            
            main_window.CommandBindings.Remove(AddBinding);
            main_frame.Content = new ManagerMainPage(MockService, main_frame, main_window);
        }

        private void AddTrainSC(object sender, ExecutedRoutedEventArgs e)
        {

            Window addTrainWindow = new AddTrainWindow(MockService, trains);
            addTrainWindow.ShowDialog();
            addTrainWindow.Focus();
        }

        private void AddTrainBtn(object sender, RoutedEventArgs e)
        {
            Window addTrainWindow = new AddTrainWindow(MockService, trains);
            addTrainWindow.ShowDialog();
            addTrainWindow.Focus();
        }
        private void DeleteTrainBtn(object sender, RoutedEventArgs e)
        {
            if (dgTrains.SelectedItems.Count == 0)
            {
                MessageBox.Show("Označite vozove za brisanje.", "Brisanje vozova", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Da li ste sigurni da želite da izbrišete označene vozove i njihove vožnje?",
                    "Brisanje vozova",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                List<Train> trainsToDelete = new List<Train>();
                foreach (Train train in dgTrains.SelectedItems)
                {
                    MockService.DeleteTrain(train);
                    trainsToDelete.Add(train);
                }
                foreach (Train train1 in trainsToDelete)
                {
                    trains.Remove(train1);
                }
            }
        }

        private void DeleteTrainsSC(object sender, ExecutedRoutedEventArgs e)
        {
            if (dgTrains.SelectedItems.Count == 0)
            {
                MessageBox.Show("Označite vozove za brisanje.", "Brisanje vozova", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Da li ste sigurni da želite da izbrišete označene vozove i njihove vožnje?",
                    "Brisanje vozova",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                List<Train> trainsToDelete = new List<Train>();
                foreach (Train train in dgTrains.SelectedItems)
                {
                    MockService.DeleteTrain(train);
                    trainsToDelete.Add(train);
                }
                foreach (Train train1 in trainsToDelete)
                {
                    trains.Remove(train1);
                }
            }
        }

        private void DGTrains_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void DGTrains_MouseMove(object sender, MouseEventArgs e)
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
                Train train = (Train)dataGrid.ItemContainerGenerator.
                    ItemFromContainer(DataGridItem);

                if (train == null)
                    return;

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", train);
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
                Train train = e.Data.GetData("myFormat") as Train;
                if (MessageBox.Show("Da li ste sigurni da želite da izbrišete označeni voz i njegove vožnje?",
                   "Brisanje vozova",
                   MessageBoxButton.YesNo,
                   MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    
                     MockService.DeleteTrain(train);
                     trains.Remove(train);
                     MessageBox.Show("Uspešno izbrisan voz i njegove vožnje.", "Srbija voz-Brisanje voza", MessageBoxButton.OK, MessageBoxImage.Information);
                    

                }
            }
        }
    }


        
    }

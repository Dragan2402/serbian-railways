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
    /// Interaction logic for AddLineWindow.xaml
    /// </summary>
    public partial class AddLineWindow : Window
    {
        
        public ObservableCollection<Station> StationsAvailable { get; set; }

        public ObservableCollection<Station> StationsSelected { get; set; } 
       
        private MockService MockService { get; set; }
        private ObservableCollection<model.Line> Lines { get; set; }

        Point startPoint = new Point();


        public AddLineWindow(MockService mockService, ObservableCollection<model.Line> lines)
        {
            InitializeComponent();
            this.DataContext = this;
            DataContext = this;
            MockService = mockService;
            Lines = lines;

            StationsAvailable = mockService.GetAllStationsTable();
            StationsSelected = new ObservableCollection<Station>();

            dgAvailableStations.DataContext = StationsAvailable;
            dgSelectedStations.DataContext = StationsSelected;


            RoutedCommand addLineCMD = new RoutedCommand();
            addLineCMD.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(addLineCMD, AddLineSC));

            RoutedCommand cancelCMD = new RoutedCommand();
            cancelCMD.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(cancelCMD, CancelSC));
        }

        private void AddLineSC(object sender, ExecutedRoutedEventArgs e)
        {
            model.Line line = MockService.AddLine(StationsSelected);
            if (line == null)
            {
                MessageBox.Show("Greška prilikom pravljenja linije, linija već postoji", "Dodavanje Linije", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Lines.Add(line);
            if (MessageBox.Show("Linija uspešno dodata",
                   "Dodavanje Linije",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                this.Close();
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void CancelSC(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel_Btn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddRideBtn(object sender, RoutedEventArgs e)
        {
            model.Line line =MockService.AddLine(StationsSelected);
            if (line == null)
            {
                MessageBox.Show("Greška prilikom pravljenja linije", "Dodavanje Linije", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Lines.Add(line);
            if (MessageBox.Show("Linija uspešno dodata",
                   "Dodavanje Linije",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                this.Close();
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
        private void Add_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void TableDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Station station = e.Data.GetData("myFormat") as Station;
                StationsAvailable.Remove(station);
                StationsSelected.Add(station);
            }
        }

        private void DoubleClickAdd(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid == null)
                return;
            DataGridRow dgr = dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem) as DataGridRow;
            if (dgr == null)
                return;
            Station station= (Station)dgr.Item;
            if (station == null)
                return;
            StationsAvailable.Remove(station);
            StationsSelected.Add(station); 

        }


        private void DoubleClickRemove(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid == null)
                return;
            DataGridRow dgr = dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem) as DataGridRow;
            if (dgr == null)
                return;
            Station station = (Station)dgr.Item;
            if (station == null)
                return;
            StationsSelected.Remove(station);
            StationsAvailable.Add(station);

        }
    }
}

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
    /// Interaction logic for UpdateLineWindow.xaml
    /// </summary>
    public partial class UpdateLineWindow : Window
    {
        private MockService MockService { get; set; }
        private ObservableCollection<model.Line> Lines { get; set; }

        private ObservableCollection<Station> StationsNotInLine=new ObservableCollection<Station>();


        private ObservableCollection<Station> StationsInLine = new ObservableCollection<Station>();

        Point startPoint = new Point();
        private model.Line Line { get; set; }

        public UpdateLineWindow(MockService mockService, ObservableCollection<model.Line> lines, model.Line line)
        {
            InitializeComponent();
            this.DataContext = this;
            DataContext = this;
            MockService = mockService;
            Lines = lines;
            Line = line;

            StationsInLine.Add(line.DepartureStation);
            foreach(Station station in line.InterStations)
                StationsInLine.Add(station);
            StationsInLine.Add(line.ArrivalStation);

            StationsNotInLine = mockService.GetOtherStations(line);

            dgAvailableStations.DataContext = StationsNotInLine;
            dgSelectedStations.DataContext = StationsInLine;
            dgAvailableStations.Focus();
            RoutedCommand UpdateLineCMD = new RoutedCommand();
            UpdateLineCMD.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(UpdateLineCMD, UpdateLineSC));

            RoutedCommand cancelCMD = new RoutedCommand();
            cancelCMD.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(cancelCMD, cancelSC));

        }



        private void UpdateLineSC(object sender, ExecutedRoutedEventArgs e)
        {


            bool exists = MockService.LineExists(StationsInLine);
            if (exists)
            {
                MessageBox.Show("Greška prilikom izmene linije, linija već postoji", "Izmena Linije", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Line.DepartureStation = StationsInLine.ElementAt(0);
            Line.ArrivalStation = StationsInLine.Last();
            if (StationsInLine.Count > 2)
            {
                Line.InterStations.Clear();
                for (int i = 0; i < StationsInLine.Count - 1; i++)
                    Line.InterStations.Add(StationsInLine[i]);
            }

            MessageBox.Show("Uspešno ažurirana linija.", "Srbija voz-Izmena linije", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void UpdateLineBtn(object sender, RoutedEventArgs e)
        {
            if(StationsInLine.Count < 2)
            {
                
                 MessageBox.Show("Greška prilikom izmene linije, potrebno je izabrati bar 2 stanice", "Izmena Linije", MessageBoxButton.OK, MessageBoxImage.Error);
                 return;
                
            }
            
            bool exists = MockService.LineExists(StationsInLine);
            if (exists)
            {
                MessageBox.Show("Greška prilikom izmene linije, linija već postoji", "Izmena Linije", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Line.DepartureStation = StationsInLine.ElementAt(0);
            Line.ArrivalStation = StationsInLine.Last();
            if (StationsInLine.Count > 2)
            {
                Line.InterStations.Clear();
                for (int i =0; i< StationsInLine.Count-1; i++)
                    Line.InterStations.Add(StationsInLine[i]);
            }

            MessageBox.Show("Uspešno ažurirana linija.", "Srbija voz-Izmena linije", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
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
                StationsNotInLine.Remove(station);
                StationsInLine.Add(station);
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
            Station station = (Station)dgr.Item;
            if (station == null)
                return;
            StationsNotInLine.Remove(station);
            StationsInLine.Add(station);

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
            StationsInLine.Remove(station);
            StationsNotInLine.Add(station);

        }
    }
}

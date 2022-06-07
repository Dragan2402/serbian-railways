using SerbianRailways.service;
using SerbianRailways.model;
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
using SerbianRailways.model.tableModels;

namespace SerbianRailways.client_pages
{
    /// <summary>
    /// Interaction logic for LinesRidesPreview.xaml
    /// </summary>
    public partial class LinesRidesPreview : Page
    {
        Frame main_frame;
        Window main_window { get; set; }
        MockService MockService { get; set; }

        Point startPoint = new Point();

        ObservableCollection<model.Line> Lines = new ObservableCollection<model.Line>();

      

        public LinesRidesPreview(MockService mockService, Frame mainFrame, Window window)
        {
            InitializeComponent();
            this.DataContext = this;
            MockService = mockService;

            main_frame = mainFrame;
            main_window = window;
            main_window.Title = "Srbija Voz-Pregled vožnji i linija";
            window.CommandBindings.Clear();
            Lines = MockService.GetAllLinesTable();
            dgLines.DataContext = Lines;
            
            dgShowingRides.DataContext = MockService.getAllRidesByLine(Lines.ElementAt(0));

        }

        private void DGSLines_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void DGLines_MouseMove(object sender, MouseEventArgs e)
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
                model.Line line = (model.Line)dataGrid.ItemContainerGenerator.
                    ItemFromContainer(DataGridItem);

                if (line == null)
                    return;

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", line);
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
                model.Line line = e.Data.GetData("myFormat") as model.Line;
                
                
                ObservableCollection<RideTable> rides = MockService.getAllRidesByLine(line);
                if (rides.Count == 0)
                {
                    MessageBox.Show("Za izabranu liniju nema postojećih vožnji", "Prikaz vožnji", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                dgShowingRides.DataContext = rides;
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
            model.Line line = (model.Line)dgr.Item;
           
            if (line == null)
                return;
            ObservableCollection<RideTable> rides = MockService.getAllRidesByLine(line);
            if (rides.Count == 0)
            {
                MessageBox.Show("Za izabranu liniju nema postojećih vožnji", "Prikaz vožnji", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            dgShowingRides.DataContext = rides;

        }

        private void ReturnClientPage(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new ClientMainPage(MockService, main_frame, main_window);
        }

    }
}

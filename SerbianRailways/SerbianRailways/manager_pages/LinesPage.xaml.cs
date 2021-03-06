using Microsoft.Maps.MapControl.WPF;
using SerbianRailways.help_pages;
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
    /// Interaction logic for LinesPage.xaml
    /// </summary>
    public partial class LinesPage : Page
    {

        ObservableCollection<model.Line> Lines = new ObservableCollection<model.Line>();
        Dictionary<model.Line, List<Pushpin>> LinePins = new Dictionary<model.Line, List<Pushpin>>();
        Dictionary<model.Line, MapPolyline> LineRoute = new Dictionary<model.Line, MapPolyline>();
        Dictionary<Station, int> StationReferences = new Dictionary<Station, int>();

        List<System.Windows.Media.Color> ColorsToPick = new List<System.Windows.Media.Color>();

        CommandBinding AddBinding { get; set; }
        CommandBinding DeleteBinding { get; set; }
        CommandBinding UpdateBinding { get; set; }

        Random rand = new Random();

        private MockService MockService { get; set; }
        Frame main_frame;
        Window main_window { get; set; }
        System.Windows.Point startPoint = new System.Windows.Point();

        public LinesPage(MockService mockService, Frame mainFrame, Window window)
        {
            InitializeComponent();
            this.DataContext = this;
            MockService = mockService;
            main_frame = mainFrame;
            main_window = window;
            main_window.Title = "Srbija Voz-Upravljanje linijama";

            Lines = MockService.GetAllLinesTable();
            dgLines.DataContext = Lines;

            //window.CommandBindings.Clear();

            AddColors();

            RoutedCommand mainMenuCMD = new RoutedCommand();
            mainMenuCMD.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(mainMenuCMD, MainMenuSc));

            ((MainWindow)System.Windows.Application.Current.MainWindow).MainMenuMenuItem.Command = mainMenuCMD;

            RoutedCommand addLineCMD = new RoutedCommand();
            addLineCMD.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            AddBinding = new CommandBinding(addLineCMD, AddLineSC);
            window.CommandBindings.Add(AddBinding);

            RoutedCommand delteLinesCMD = new RoutedCommand();
            delteLinesCMD.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));
            DeleteBinding = new CommandBinding(delteLinesCMD, DeleteLineSC);
            window.CommandBindings.Add(DeleteBinding);

            RoutedCommand updateLinesCMD = new RoutedCommand();
            updateLinesCMD.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            UpdateBinding = new CommandBinding(updateLinesCMD, UpdateLineSc);
            window.CommandBindings.Add(new CommandBinding(updateLinesCMD, UpdateLineSc));


            RoutedCommand demoCMD = new RoutedCommand();
            demoCMD.InputGestures.Add(new KeyGesture(Key.F5, ModifierKeys.Control));            
            window.CommandBindings.Add(new CommandBinding(demoCMD, ToggleDemoSC));
            ((MainWindow)System.Windows.Application.Current.MainWindow).DemoMenuItem.IsEnabled = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).DemoMenuItem.Command = demoCMD;
            
            RoutedCommand openHelpPage = new RoutedCommand();
            openHelpPage.InputGestures.Add(new KeyGesture(Key.F1, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(openHelpPage, ToggleHelpPageSC));
            ((MainWindow)System.Windows.Application.Current.MainWindow).HelpMenuItem.IsEnabled = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).HelpMenuItem.Command = openHelpPage;

        }

        private void AddColors()
        {
            ColorsToPick.Add(Colors.Blue);
            ColorsToPick.Add(Colors.Red);
            ColorsToPick.Add(Colors.Green);
            ColorsToPick.Add(Colors.Tan);
            ColorsToPick.Add(Colors.Yellow);
            ColorsToPick.Add(Colors.Purple);
            ColorsToPick.Add(Colors.Brown);
            ColorsToPick.Add(Colors.Orange);
            ColorsToPick.Add(Colors.CadetBlue);
            ColorsToPick.Add(Colors.DarkGray);
            ColorsToPick.Add(Colors.Black);
            ColorsToPick.Add(Colors.White);
        }

        private void ToggleDemoSC(object sender, ExecutedRoutedEventArgs e)
        {
            
            Window demoWindow = new DemoPlayerWindow("lines");
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


        private void AddLineSC(object sender, ExecutedRoutedEventArgs e)
        {

            Window addLineWindow = new AddLineWindow(MockService, Lines);
            addLineWindow.ShowDialog();
            addLineWindow.Focus();
        }

        private void AddLineBtn(object sender, RoutedEventArgs e)
        {
            Window addLineWindow = new AddLineWindow(MockService, Lines);
            addLineWindow.ShowDialog();
            addLineWindow.Focus();
        }


        private void DeleteLineBtn(object sender, RoutedEventArgs e)
        {
            if (dgLines.SelectedItems.Count == 0)
            {
                MessageBox.Show("Označite linije za brisanje.", "Brisanje linija", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Da li ste sigurni da želite da izbrišete označene linije i njihove vožnje?",
                    "Brisanje linija",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                List<model.Line> linesToDelete = new List<model.Line>();
                foreach (model.Line line in dgLines.SelectedItems)
                {
                    MockService.DeleteLine(line);
                    linesToDelete.Add(line);
                }
                foreach (model.Line line in linesToDelete)
                {
                    Lines.Remove(line);
                }
            }
        }

        private void DeleteLineSC(object sender, ExecutedRoutedEventArgs e)
        {
            if (dgLines.SelectedItems.Count == 0)
            {
                MessageBox.Show("Označite linije za brisanje.", "Brisanje linija", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Da li ste sigurni da želite da izbrišete označene linije i njihove vožnje?",
                    "Brisanje linija",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                List<model.Line> linesToDelete = new List<model.Line>();
                foreach (model.Line line in dgLines.SelectedItems)
                {
                    MockService.DeleteLine(line);
                    linesToDelete.Add(line);
                }
                foreach (model.Line line in linesToDelete)
                {
                    Lines.Remove(line);
                }
            }
        }

        private void DGLines_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void DGLines_MouseMove(object sender, MouseEventArgs e)
        {
            if (startPoint == null)
                return;

            var dataGrid = sender as DataGrid;
            if (dataGrid == null) return;

            System.Windows.Point mousePos = e.GetPosition(null);
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
                model.Line line = e.Data.GetData("myFormat") as model.Line;
                if (MessageBox.Show("Da li ste sigurni da želite da izbrišete označenu linije i vožnje?",
                   "Brisanje linije",
                   MessageBoxButton.YesNo,
                   MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    MockService.DeleteLine(line);
                    Lines.Remove(line);

                }
            }

        }

        private void UpdateBTN_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                model.Line line = e.Data.GetData("myFormat") as model.Line;
                Window updateLineWindow = new UpdateLineWindow(MockService, Lines, line);
                updateLineWindow.ShowDialog();

            }
            dgLines.Items.Refresh();
        }

        private void MapDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                model.Line line = e.Data.GetData("myFormat") as model.Line;
                if (!LinePins.ContainsKey(line) && !LineRoute.ContainsKey(line))
                {

                    List<Pushpin> linePushpins = new List<Pushpin>();

                    string toolTipContent = "Linija " + line.DepartureStation.Name + "-" + line.ArrivalStation.Name;
                    ToolTip lineTooltip = new ToolTip();
                    lineTooltip.Content = toolTipContent;
                    System.Windows.Media.Color color = getRandomColor();

                    Pushpin departurePin = new Pushpin();
                    departurePin.Location = new Microsoft.Maps.MapControl.WPF.Location(line.DepartureStation.Location.X, line.DepartureStation.Location.Y);
                    departurePin.Background = new SolidColorBrush(color);
                    departurePin.ToolTip = "Stanica " + line.DepartureStation.Name;
                    linePushpins.Add(departurePin);
                    if (!StationReferences.ContainsKey(line.DepartureStation))
                    {
                        LinesGridMap.Children.Add(departurePin);                        
                        StationReferences.Add(line.DepartureStation, 1);
                    }
                    else                    
                        StationReferences[line.DepartureStation]++;

                    
                    foreach(Station station in line.InterStations)
                    {
                        Pushpin interPin = new Pushpin();
                        interPin.Location = new Microsoft.Maps.MapControl.WPF.Location(station.Location.X, station.Location.Y);
                        interPin.Background = new SolidColorBrush(color);
                        interPin.ToolTip = "Stanica " + station.Name;
                        linePushpins.Add(interPin);
                        if (!StationReferences.ContainsKey(station))
                        {
                           
                            LinesGridMap.Children.Add(interPin);                            ;
                            StationReferences.Add(station, 1);
                        }
                        else
                            StationReferences[station]++;
                    }



                    Pushpin pushpinARrival = new Pushpin();
                    pushpinARrival.Location = new Microsoft.Maps.MapControl.WPF.Location(line.ArrivalStation.Location.X, line.ArrivalStation.Location.Y);
                    pushpinARrival.Background = new SolidColorBrush(color);
                    pushpinARrival.ToolTip = "Stanica " + line.ArrivalStation.Name;
                    linePushpins.Add(pushpinARrival);
                    if (!StationReferences.ContainsKey(line.ArrivalStation))
                    {                      
                        LinesGridMap.Children.Add(pushpinARrival);                        
                        StationReferences.Add(line.ArrivalStation, 1);
                    }else
                        StationReferences[line.ArrivalStation]++;

                    MapPolyline route = new MapPolyline();
                    route.Stroke = new SolidColorBrush(color);
                    route.StrokeThickness = 5;
                    route.Opacity = 0.7;

                    LocationCollection points = new LocationCollection();

                    foreach (Pushpin pushpin in linePushpins)
                        points.Add(pushpin.Location);
                    route.Locations = points;
                    route.ToolTip = lineTooltip;
                    LinesGridMap.Children.Add(route);

                    LinePins.Add(line, linePushpins);
                    LineRoute.Add(line, route);
                }

            }
            dgLines.Items.Refresh();
        }

        private System.Windows.Media.Color getRandomColor()
        {
            int index = rand.Next(0, 12);
            return ColorsToPick.ElementAt(index); 
        }

        private void UpdateLineBtn(object sender, RoutedEventArgs e)
        {
            foreach (model.Line line in dgLines.SelectedItems)
            {
                LinesGridMap.Children.Clear();
                LinePins.Clear();
                LineRoute.Clear();
                StationReferences.Clear();
                Window updateLineWindow = new UpdateLineWindow(MockService, Lines, line);
                updateLineWindow.ShowDialog();
                dgLines.Items.Refresh();

            }
            dgLines.Items.Refresh();

        }

        private void UpdateLineSc(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (model.Line line in dgLines.SelectedItems)
            {
                LinesGridMap.Children.Clear();
                LinePins.Clear();
                LineRoute.Clear();
                StationReferences.Clear();
                Window updateLineWindow = new UpdateLineWindow(MockService, Lines, line);
                updateLineWindow.ShowDialog();
                dgLines.Items.Refresh();
            }
            dgLines.Items.Refresh();

        }

        private void ClearMap(object sender, RoutedEventArgs e)
        {
            LinesGridMap.Children.Clear();
            LinePins.Clear();
            LineRoute.Clear();
            StationReferences.Clear();
        }

        private void ToggleHelpPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content = new HelpPage(main_frame, main_window, "managerLinesPage", this);
        }
    }
}

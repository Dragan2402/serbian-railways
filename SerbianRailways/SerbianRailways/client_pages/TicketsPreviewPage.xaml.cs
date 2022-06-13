using SerbianRailways.help_pages;
using SerbianRailways.model.tableModels;
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

namespace SerbianRailways.client_pages
{
    /// <summary>
    /// Interaction logic for TicketsPreviewPage.xaml
    /// </summary>
    public partial class TicketsPreviewPage : Page
    {
        private MockService MockService { get; set; }
        System.Windows.Point startPoint = new System.Windows.Point();
        ObservableCollection<TicketTable> Tickets = new ObservableCollection<TicketTable>();

        Frame main_frame;
        Window main_window { get; set; }
        public TicketsPreviewPage(MockService mockService, Frame mainFrame, Window window)
        {
            InitializeComponent();
            this.DataContext = this;
            MockService = mockService;

            main_frame = mainFrame;
            main_window = window;
            main_window.Title = "Srbija Voz-Pregled karata";
            window.CommandBindings.Clear();

            RoutedCommand openHelpPage = new RoutedCommand();
            openHelpPage.InputGestures.Add(new KeyGesture(Key.F1));
            window.CommandBindings.Add(new CommandBinding(openHelpPage, ToggleHelpPageSC));

            Tickets = MockService.GetLoggedClientTicketsTable();
            dgTickets.DataContext= Tickets;
        }

        private void CancelTicket(object sender, RoutedEventArgs e)
        {
 
                TicketTable ticketTable = (TicketTable)dgTickets.SelectedItem;
            if (ticketTable != null)
            {
                if (MessageBox.Show("Da li ste sigurni da želite da otkažete kartu?",
                   "Otkazivanje karte",
                   MessageBoxButton.YesNo,
                   MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (MockService.TicketPassedById(ticketTable.Id))
                    {
                        MessageBox.Show("Nemoguće otkazati već obavljene vožnje.", "Srbija voz-Otkazivanje karte", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    if (MockService.CancelTicketById(ticketTable.Id))
                    {
                        MessageBox.Show("Uspešno oktazana karta.", "Srbija voz-Otkazivanje karte", MessageBoxButton.OK, MessageBoxImage.Information);
                        Tickets.Remove(ticketTable);
                        dgTickets.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Neuspešno oktazana karta.", "Srbija voz-Otkazivanje karte", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ReturnClientPage(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new ClientMainPage(MockService, main_frame, main_window);
        }


        private void DGTickets_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void DGTickets_MouseMove(object sender, MouseEventArgs e)
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
                TicketTable ticketTable = (TicketTable)dataGrid.ItemContainerGenerator.
                    ItemFromContainer(DataGridItem);

                if (ticketTable == null)
                    return;

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", ticketTable);
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
        private void CancelBTN_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void Cancel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                TicketTable ticketTable = e.Data.GetData("myFormat") as TicketTable;
                if (MessageBox.Show("Da li ste sigurni da želite da otkažete kartu?",
                    "Otkazivanje karte",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (MockService.TicketPassedById(ticketTable.Id))
                    {
                        MessageBox.Show("Nemoguće otkazati već obavljene vožnje.", "Srbija voz-Otkazivanje karte", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    if (MockService.CancelTicketById(ticketTable.Id))
                    {
                        MessageBox.Show("Uspešno oktazana karta.", "Srbija voz-Otkazivanje karte", MessageBoxButton.OK, MessageBoxImage.Information);
                        Tickets.Remove(ticketTable);
                        dgTickets.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Neuspešno oktazana karta.", "Srbija voz-Otkazivanje karte", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ToggleHelpPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content = new HelpPage(main_frame, main_window, "clientTicketsPreviewPage", this);
        }
    }
}

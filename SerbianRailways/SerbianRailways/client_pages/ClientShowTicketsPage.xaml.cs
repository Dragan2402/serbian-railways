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
    /// Interaction logic for ClientShowTicketsPage.xaml
    /// </summary>
    public partial class ClientShowTicketsPage : Page
    {
        public ObservableCollection<TicketTable> data = new ObservableCollection<TicketTable>();
        private MockService MockService { get; set; }
        Frame main_frame;
        public ClientShowTicketsPage(MockService mockService, Frame mainFrame)
        {
            InitializeComponent();
            this.DataContext = this;
            MockService = mockService;
            main_frame = mainFrame;
            data = MockService.getLoggedClientTicketsTable();
            dgTickets.DataContext = data;
        }
    }
}

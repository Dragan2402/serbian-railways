using SerbianRailways.service;
using System;
using System.Collections.Generic;
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
            window.CommandBindings.Clear();
            RoutedCommand newCmd = new RoutedCommand();
            newCmd.InputGestures.Add(new KeyGesture(Key.B, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(newCmd, ReturnManagerPageSC));
            
        }

        private void ReturnManagerPage(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new ManagerMainPage(MockService, main_frame, main_window);
        }
        private void ReturnManagerPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content = new ManagerMainPage(MockService, main_frame, main_window);
        }
    }
}

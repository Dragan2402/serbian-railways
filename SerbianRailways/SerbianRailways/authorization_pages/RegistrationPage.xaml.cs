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

namespace SerbianRailways.authorization_pages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private MockService MockService { get; set; }
        Frame main_frame;
        Window main_window { get; set; }

        private double OldHeight { get; set; }
        private double OldWidth     { get; set; }


        public RegistrationPage(MockService mockService, Frame mainFrame, Window window)
        {
            InitializeComponent();

            this.DataContext = this;
            MockService = mockService;
            MockService.logout();
            main_frame = mainFrame;
            main_window = window;
            main_window.Title = "Srbija Voz-Registracija";
            OldHeight=main_window.Height;
            OldWidth=main_window.Width;
            main_window.Height = 600;
            main_window.Width = 400;
            window.CommandBindings.Clear();
            RoutedCommand newCmd = new RoutedCommand();
            newCmd.InputGestures.Add(new KeyGesture(Key.Back, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(newCmd, ReturnManagerPageSC));

        }

        private void ReturnManagerPage(object sender, RoutedEventArgs e)
        {
            main_window.Height = OldHeight;
            main_window.Width = OldWidth;
            main_frame.Content = new Login(MockService, main_frame, main_window);
        }
        private void ReturnManagerPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_window.Height = OldHeight;
            main_window.Width   =OldWidth;
            main_frame.Content = new Login(MockService, main_frame, main_window);
        }

        private void Sign_up_btn_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("registraicija");
        }
    }
}

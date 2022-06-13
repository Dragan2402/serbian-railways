using SerbianRailways.service;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace SerbianRailways.help_pages
{
    /// <summary>
    /// Interaction logic for ClientBuyBookTicketHelp.xaml
    /// </summary>
    public partial class HelpPage : Page
    {
        Frame main_frame;
        Window main_window { get; set; }
        Page originPage;
        //string originPageTitle;

        public HelpPage(Frame mainFrame, Window window, string helpPageName, Page origin)
        {
            InitializeComponent();
            this.DataContext = this;
            originPage = origin;
            //originPageTitle = originTitle;
            main_frame = mainFrame;
            main_window = window;
            //main_window.Title = originPageTitle + " - Pomoć";
            //window.CommandBindings.Clear();

            string debugDir = System.IO.Path.GetDirectoryName(AppContext.BaseDirectory);
            string binDir = Directory.GetParent(debugDir).FullName;
            string baseDir = Directory.GetParent(binDir).FullName;
            string htmlPagePath = baseDir + "\\help_pages\\html\\" + helpPageName + ".html";

            if (!File.Exists(htmlPagePath))
                htmlPagePath = baseDir + "\\help_pages\\html\\not_found.html";

            Uri u = new Uri(htmlPagePath);
            helpPage.Navigate(u);
        }

        private void HelpPage_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            //urlTextBox.Text = e.Uri.OriginalString;
        }

        private void BrowseBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = helpPage != null && helpPage.CanGoBack;
        }

        private void BrowseBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            helpPage.GoBack();
        }

        private void BrowseForward_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = helpPage != null && helpPage.CanGoForward;
        }

        private void BrowseForward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            helpPage.GoForward();
        }

        private void ReturnToOriginPage(object sender, RoutedEventArgs e)
        {
            //main_window.Title = originPageTitle;
            main_frame.Content = originPage;
        }
    }
}

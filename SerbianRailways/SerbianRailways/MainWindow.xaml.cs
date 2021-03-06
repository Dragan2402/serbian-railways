using SerbianRailways.authorization_pages;
using SerbianRailways.service;
using SerbianRailways.model;
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

namespace SerbianRailways
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MockService MockService=null;

        

        public MainWindow()
        {
            InitializeComponent();

            double width=SystemParameters.PrimaryScreenWidth;
            double height=SystemParameters.PrimaryScreenHeight;



            this.Width = (width * 7) / 10;
            this.Height= (height * 7) / 10;

            MockService = new MockService();
            Main.Content = new Login(MockService, Main, this);




        }



    }
}

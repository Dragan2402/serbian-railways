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
        

       
        private MockService MockService { get; set; }
        private ObservableCollection<model.Line> Lines { get; set; }

        public AddLineWindow(MockService mockService, ObservableCollection<model.Line> lines)
        {
            InitializeComponent();
            this.DataContext = this;
            DataContext = this;
            MockService = mockService;
            Lines = lines;



            RoutedCommand addLineCMD = new RoutedCommand();
            addLineCMD.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(addLineCMD, AddLineSC));

            RoutedCommand cancelCMD = new RoutedCommand();
            cancelCMD.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(cancelCMD, CancelSC));
        }

        private void AddLineSC(object sender, ExecutedRoutedEventArgs e)
        {
            

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

         
        }
    }
}

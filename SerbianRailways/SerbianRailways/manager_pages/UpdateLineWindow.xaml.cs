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



        private model.Line Line { get; set; }

        public UpdateLineWindow(MockService mockService, ObservableCollection<model.Line> lines, model.Line line)
        {
            InitializeComponent();
            this.DataContext = this;
            DataContext = this;
            MockService = mockService;
            Lines = lines;
            Line = line;



            RoutedCommand UpdateLineCMD = new RoutedCommand();
            UpdateLineCMD.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(UpdateLineCMD, UpdateLineSC));

            RoutedCommand cancelCMD = new RoutedCommand();
            cancelCMD.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));
            this.CommandBindings.Add(new CommandBinding(cancelCMD, cancelSC));

        }



        private void UpdateLineSC(object sender, ExecutedRoutedEventArgs e)
        {

        


            MessageBox.Show("Uspešno ažurirana linija.", "Srbija voz-Ažuriranje linije", MessageBoxButton.OK, MessageBoxImage.Information);
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



            MessageBox.Show("Uspešno ažurirana linija.", "Srbija voz-Ažuriranje linije", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}

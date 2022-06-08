
using SerbianRailways.authorization_pages;
using SerbianRailways.model;
using SerbianRailways.service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using Line = SerbianRailways.model.Line;

namespace SerbianRailways.manager_pages
{
    /// <summary>
    /// Interaction logic for ManagerMainPage.xaml
    /// </summary>
    public partial class ManagerMainPage : Page
    {
        public string LoggedUserUsername { get; set; }
        public string LoggedUserName { get; set; }
        public string LoggedUserAddress { get; set; }

        private bool WaitingForCMDLineClear { get; set; }
        private List<string> SupportedFunctions { get; set; }

        private string _command;

        public string Command
        {
            get { return _command; }
            set
            {
                if (value != _command)
                {
                    _command = value;
                    OnPropertyChanged("Command");
                }
            }
        }


        private MockService MockService { get; set; }
        Frame main_frame;
        Window main_window { get; set; }
        public ManagerMainPage(MockService mockService, Frame mainFrame,Window window)
        {
            InitializeComponent();

            this.DataContext = this;
            MockService = mockService;
            LoggedUserUsername = "Korisničko ime: " + MockService.GetLoggedUser().UserName;
            LoggedUserAddress = "Adresa: " + MockService.GetLoggedUser().Address.ToString();
            LoggedUserName = "Ime: " + MockService.GetLoggedUser().Name + " " + mockService.GetLoggedUser().Surname;
            main_frame = mainFrame;
            
            main_window = window;
            main_window.Title = "Srbija Voz";

            GenerateSupportedFunctions();

            window.CommandBindings.Clear();

            RoutedCommand toggleTrainsCMD = new RoutedCommand();
            toggleTrainsCMD.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Alt));
            window.CommandBindings.Add(new CommandBinding(toggleTrainsCMD, ToggleTrainsCRUDPageSC));

            RoutedCommand toggleStationsCMD = new RoutedCommand();
            toggleStationsCMD.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Alt));
            window.CommandBindings.Add(new CommandBinding(toggleStationsCMD, ToggleStationsCRUDPageSC));

            RoutedCommand toggleTicketsCMD = new RoutedCommand();
            toggleTicketsCMD.InputGestures.Add(new KeyGesture(Key.K, ModifierKeys.Alt));
            window.CommandBindings.Add(new CommandBinding(toggleTicketsCMD, ToggleTicketsCRUDPageSC));

            RoutedCommand toggleLinesCMD = new RoutedCommand();
            toggleLinesCMD.InputGestures.Add(new KeyGesture(Key.L, ModifierKeys.Alt));
            window.CommandBindings.Add(new CommandBinding(toggleLinesCMD, ToggleLinesCRUDPageSC));

            RoutedCommand toggleRidesCMD = new RoutedCommand();
            toggleRidesCMD.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Alt));
            window.CommandBindings.Add(new CommandBinding(toggleRidesCMD, ToggleRidesCRUDPageSC));


            RoutedCommand toggleCommandLine = new RoutedCommand();
            toggleCommandLine.InputGestures.Add(new KeyGesture(Key.F11, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(toggleCommandLine, ToggleCommandLine));

            RoutedCommand clearCommandLineCMD = new RoutedCommand();
            clearCommandLineCMD.InputGestures.Add(new KeyGesture(Key.B, ModifierKeys.Control));
            window.CommandBindings.Add(new CommandBinding(clearCommandLineCMD, ClearCommandLine));

        }

        private void GenerateSupportedFunctions()
        {
            SupportedFunctions = new List<string>();
            SupportedFunctions.Add("d_stanicu");
            SupportedFunctions.Add("i_stanicu");
            SupportedFunctions.Add("u_stanicu");
            SupportedFunctions.Add("d_voz");
            SupportedFunctions.Add("i_voz");
            SupportedFunctions.Add("d_liniju");
            SupportedFunctions.Add("i_liniju");
            SupportedFunctions.Add("d_voznju");
            SupportedFunctions.Add("i_voznju");
            SupportedFunctions.Add("i_sve");
        }

        public void Log_out(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da se odjavite?",
                    "Odjava",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                main_frame.Content = new Login(MockService, main_frame,main_window);
            }

        }

        private void ToggleTrainsCRUDPageBTN(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new TrainsPage(MockService, main_frame, main_window);
        }
        private void ToggleTicketsCRUDPageBTN(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new TicketsPage(MockService, main_frame, main_window);
        }
        private void ToggleStationsCRUDPageBTN(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new StationsPage(MockService, main_frame, main_window);
        }
        private void ToggleLinesCRUDPageBTN(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new LinesPage(MockService, main_frame, main_window);
        }
        private void ToggleRidesCRUDPageBTN(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new RidesPage(MockService, main_frame, main_window);
        }

        private void ToggleTrainsCRUDPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content= new TrainsPage(MockService, main_frame, main_window);
        }

        private void ToggleTicketsCRUDPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content = new TicketsPage(MockService, main_frame, main_window);
        }

        private void ToggleStationsCRUDPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content = new StationsPage(MockService, main_frame, main_window);
        }

        private void ToggleLinesCRUDPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content = new LinesPage(MockService, main_frame, main_window);
        }

        private void ToggleRidesCRUDPageSC(object sender, ExecutedRoutedEventArgs e)
        {
            main_frame.Content = new RidesPage(MockService, main_frame, main_window);
        }
        private void ToggleCommandLine(object sender, ExecutedRoutedEventArgs e)
        {
            if (commandLine_tb.IsVisible)
            {
                commandLine_tb.IsEnabled = false;
                commandLine_tb.Visibility=Visibility.Hidden;
            }
            else
            {
                commandLine_tb.IsEnabled = true;
                commandLine_tb.Visibility = Visibility.Visible;
                commandLine_tb.Focus();
                ToolTip toolTip = new ToolTip();
                toolTip.Content = "Srbija voz-Komandna linija\nZa uvid u moguće radnje, upišite 'instrukcije'.\nCtrl+F11 za gašenje konzole.";
                commandLine_tb.ToolTip= toolTip;
                WaitingForCMDLineClear = false;

            }
        }

        private void ClearCommandLine(object sender, ExecutedRoutedEventArgs e)
        {
            if (commandLine_tb.IsEnabled)
            {
                commandLine_tb.Clear();
                WaitingForCMDLineClear = false;
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void KeyDown_CommandLine_Handler(object sender, KeyEventArgs e)
        {
            if (WaitingForCMDLineClear)
            {

                return;
            }
            if (e.Key == Key.Enter)
            {
                if (Command.Equals("instrukcije"))
                {
                    WriteInstructionsToCommandLine();
                    return;
                }
                if (Command.Equals("lista_stanica"))
                {
                    HandleCommand();
                    return;
                }
                if (Command.Equals("lista_vozova"))
                {
                    HandleCommand();
                    return;
                }
                if (Command.Equals("lista_linija"))
                {
                    HandleCommand();
                    return;
                }
                if (Command.Equals("lista_voznji"))
                {
                    HandleCommand();
                    return;
                }
                string[] commandTokens = Command.Split(' ');
                if (commandTokens.Length >= 2)
                {
                    if (SupportedFunctions.Contains(commandTokens[0]))
                    {
                        HandleCommand();
                        return;
                    }
                    else
                    {
                        commandLine_tb.Text = "Nepoznata instrukcija.\nZa uvid u moguće radnje, upišite 'instrukcije'.\nCtrl+F11 za gašenje konzole." + "\n\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
                        WaitingForCMDLineClear = true;
                        return;
                    }
                }
                else
                {
                    commandLine_tb.Text = "Nepoznata instrukcija.\nZa uvid u moguće radnje, upišite 'instrukcije'.\nCtrl+F11 za gašenje konzole." + "\n\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
                    WaitingForCMDLineClear = true;
                    return;
                }

                
            }
        }

        private void HandleCommand()
        {
            string[] commandTokens = Command.Split(' ');

            if(commandTokens[0].Equals("lista_stanica") && commandTokens.Length == 1)
            {
                string textToDisplay = "";
                foreach(Station station in MockService.GetAllStations())
                {
                    textToDisplay = textToDisplay + station.ToString() + "\n";
                }
                textToDisplay=textToDisplay+ "\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
                commandLine_tb.Text = textToDisplay;
                WaitingForCMDLineClear = true;
                return;
            }
            if (commandTokens[0].Equals("lista_vozova") && commandTokens.Length == 1)
            {
                string textToDisplay = "";
                foreach (Train train in MockService.GetAllTrains())
                {
                    textToDisplay = textToDisplay + train.ToString() + "\n";
                }
                textToDisplay = textToDisplay + "\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
                commandLine_tb.Text = textToDisplay;
                WaitingForCMDLineClear = true;
                return;
            }
            if (commandTokens[0].Equals("lista_linija") && commandTokens.Length == 1)
            {
                string textToDisplay = "";
                foreach (Line line in MockService.GetAllLines())
                {
                    textToDisplay = textToDisplay + line.ToString() + "\n";
                }
                textToDisplay = textToDisplay + "\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
                commandLine_tb.Text = textToDisplay;
                WaitingForCMDLineClear = true;
                return;
            }
            if (commandTokens[0].Equals("lista_voznji") && commandTokens.Length == 1)
            {
                string textToDisplay = "";
                foreach (Ride ride in MockService.GetAllRides())
                {
                    textToDisplay = textToDisplay + ride.ToString() + "\n";
                }
                textToDisplay = textToDisplay + "\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
                commandLine_tb.Text = textToDisplay;
                WaitingForCMDLineClear = true;
                return;
            }
            
            if(commandTokens[0].Equals("d_voz") && commandTokens.Length == 6)
            {
                int serialNumber,passengerCars, fgSeats, sgSeats;
                string name = GetName(commandTokens[2]);
                if (!int.TryParse(commandTokens[1], out serialNumber))
                {
                    CommandLineInvalidArgument("serijski broj voza");
                    return;
                }
                if (MockService.CheckSerialNumExists(serialNumber))
                {
                    commandLine_tb.Text = "Serijski broj voza " + serialNumber+" već postoji."+ "\n\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
                    WaitingForCMDLineClear = true;
                    return;
                }
                if (!int.TryParse(commandTokens[3], out passengerCars))
                {
                    CommandLineInvalidArgument("broj vagona");
                    return;
                }
                if (!int.TryParse(commandTokens[4], out fgSeats))
                {
                    CommandLineInvalidArgument("broj mesta prvog razreda");
                    return;
                }
                if (!int.TryParse(commandTokens[5], out sgSeats))
                {
                    CommandLineInvalidArgument("broj mesta drugog razreda");
                    return;
                }
                Train newTrain = new Train(serialNumber, name, passengerCars, fgSeats, sgSeats);
                MockService.AddTrain(newTrain);
                CommandLineSuccessful();
                return;
            }
            if(commandTokens[0].Equals("d_liniju") && commandTokens.Length>=3 && commandTokens.Length <= 4)
            {
                int departureStationId, arrivalStationId;
                if (!int.TryParse(commandTokens[1], out departureStationId))
                {
                    CommandLineInvalidArgument("id polazne stanice");
                    return;
                }
                if (!MockService.CheckStationIdExists(departureStationId))
                {
                    CommandLineNonExistingId(departureStationId);
                    return;
                }
                if (!int.TryParse(commandTokens[2], out arrivalStationId))
                {
                    CommandLineInvalidArgument("id dolazne stanice");
                    return;
                }
                if (!MockService.CheckStationIdExists(arrivalStationId))
                {
                    CommandLineNonExistingId(arrivalStationId);
                    return;
                }
                if(departureStationId == arrivalStationId)
                {
                    commandLine_tb.Text = "Ponavljanje id-ova." + "\n\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
                    WaitingForCMDLineClear = true;
                    return;
                }
                ObservableCollection<Station> stations = new ObservableCollection<Station>();
                List<int> interStationsIds=new List<int>();
                if (commandTokens.Length == 4)
                {
                    interStationsIds = GetInterStationsList(commandTokens[3]);
                    if (interStationsIds == null)
                        return;
                    if(interStationsIds.Contains(departureStationId) || interStationsIds.Contains(arrivalStationId))
                    {
                        commandLine_tb.Text = "Ponavljanje id-ova." + "\n\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
                        WaitingForCMDLineClear = true;
                        return;
                    }
                }
                stations.Add(MockService.GetStationById(departureStationId));

                foreach(int interStationID in interStationsIds)
                {
                    stations.Add(MockService.GetStationById(interStationID));
                }
                stations.Add(MockService.GetStationById(arrivalStationId));
                Line line=MockService.AddLine(stations);
                if (line != null)
                {
                    CommandLineSuccessful();
                    return;
                }
                else
                {
                    commandLine_tb.Text = "Linija već postoji." + "\n\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
                    WaitingForCMDLineClear = true;
                    return;
                }
            }
            if (commandTokens[0].Equals("i_voz") && commandTokens.Length == 2)
            {
                int serialNumber;
                if (!int.TryParse(commandTokens[1], out serialNumber))
                {
                    CommandLineInvalidArgument("serijski broj voza");
                    return;
                }
                if (serialNumber <= 0)
                {
                    CommandLineInvalidArgument("serijski broj voza");
                    return;
                }
                if (!MockService.CheckSerialNumExists(serialNumber))
                {
                    CommandLineNonExistingId(serialNumber);
                    return;
                }
                MockService.DeleteTrainById(serialNumber);
                CommandLineSuccessful();
                return;
            }
            if (commandTokens[0].Equals("i_liniju") && commandTokens.Length == 2)
            {
                int lineId;
                if (!int.TryParse(commandTokens[1], out lineId))
                {
                    CommandLineInvalidArgument("id linije");
                    return;
                }
                if (lineId <= 0)
                {
                    CommandLineInvalidArgument("id linije");
                    return;
                }
                if (!MockService.CheckLineIdExists(lineId))
                {
                    CommandLineNonExistingId(lineId);
                    return;
                }
                MockService.DeleteLineById(lineId);
                CommandLineSuccessful();
                return;
            }
            if(commandTokens[0].Equals("d_voznju") && commandTokens.Length == 7)
            {
          
                TimeSpan departureTime, arrivalTime;
                double price;
                int serialNumber;
                int lineId;
                List<DayOfWeek> dayOfWeek;
                if (!TimeSpan.TryParse(commandTokens[1], out departureTime))
                {
                    CommandLineInvalidArgument("vreme polaska");
                    return;
                }
                TimeSpan limitUpper = new TimeSpan(24, 0,0);
                TimeSpan limitDown = new TimeSpan(0, 0, 0);
                if(departureTime.Duration().CompareTo(limitUpper.Duration())>0 || departureTime.Duration().CompareTo(limitDown.Duration()) < 0)
                {
                    CommandLineInvalidArgument("vreme polaska");
                    return;
                }
                if (!TimeSpan.TryParse(commandTokens[2], out arrivalTime))
                {
                    CommandLineInvalidArgument("vreme dolaska");
                    return;
                }
                if (arrivalTime.Duration().CompareTo(limitUpper.Duration()) > 0 || arrivalTime.Duration().CompareTo(limitDown.Duration()) < 0)
                {
                    CommandLineInvalidArgument("vreme dolaska");
                    return;
                }
                if (!int.TryParse(commandTokens[3], out serialNumber))
                {
                    CommandLineInvalidArgument("serijski broj voza");
                    return;
                }
                if (serialNumber <= 0)
                {
                    CommandLineInvalidArgument("serijski broj voza");
                    return;
                }
                if (!MockService.CheckSerialNumExists(serialNumber))
                {
                    CommandLineNonExistingId(serialNumber);
                    return;
                }
                if (!int.TryParse(commandTokens[4], out lineId))
                {
                    CommandLineInvalidArgument("id linije");
                    return;
                }
                if (lineId <= 0)
                {
                    CommandLineInvalidArgument("id linije");
                    return;
                }
                if (!MockService.CheckLineIdExists(lineId))
                {
                    CommandLineNonExistingId(lineId);
                    return;
                }
              
                if (!Double.TryParse(commandTokens[5], out price))
                {
                    CommandLineInvalidArgument("cena");
                    return;
                }
                if (price <= 0)
                {
                    CommandLineInvalidArgument("cena");
                    return;
                }

            
                dayOfWeek = GetDaysOfWeek(commandTokens[6]);
                if (dayOfWeek == null)
                {
                    CommandLineInvalidArgument("dani vožnje");
                    return;
                }
                MockService.AddRide(departureTime, arrivalTime, MockService.GetTrainBySerialNumber(serialNumber), MockService.GetLineById(lineId), price, dayOfWeek);
                CommandLineSuccessful();
                return;

            }
            if(commandTokens[0].Equals("i_sve") && commandTokens.Length == 2)
            {
                if (!commandTokens[1].Equals(MockService.GetLoggedUser().Password))
                {
                    CommandLineInvalidArgument("id lozinka");
                    return;
                }
                MockService.DeleteAll();
                CommandLineSuccessful();
                return;
            }
            if (commandTokens[0].Equals("i_voznju") && commandTokens.Length == 2)
            {
                int rideId;
                if (!int.TryParse(commandTokens[1], out rideId))
                {
                    CommandLineInvalidArgument("id vožnje");
                    return;
                }
                if (rideId <= 0)
                {
                    CommandLineInvalidArgument("id vožnje");
                    return;
                }
                if (!MockService.CheckRideExists(rideId))
                {
                    CommandLineNonExistingId(rideId);
                    return;
                }
                MockService.DeleteRideByID(rideId);
                CommandLineSuccessful();
                return;
            }
            if (commandTokens[0].Equals("d_stanicu") && commandTokens.Length == 4)
            {

                string name = GetName(commandTokens[1]);
                string latitude = commandTokens[2];
                string longitude = commandTokens[3]; 
                double latitudeDouble;
                double longitudeDouble;
                if(!Double.TryParse(latitude,out latitudeDouble))
                {
                    CommandLineInvalidArgument("geografska širina");
                    return;
                }
                if (!Double.TryParse(longitude, out longitudeDouble))
                {
                    CommandLineInvalidArgument("geografska visina");
                    return;
                }
                
                Location location= new Location(latitudeDouble,longitudeDouble);
                MockService.AddStation(name, location);
                CommandLineSuccessful();
                return;
            }else if(commandTokens[0].Equals("i_stanicu") && commandTokens.Length == 2){
                int stationId;
                if(!int.TryParse(commandTokens[1],out stationId))
                {
                    CommandLineInvalidArgument("id stanice");
                    return;
                }
                if (stationId <= 0)
                {
                    CommandLineInvalidArgument("id stanice");
                    return;
                }
                if (!MockService.CheckStationIdExists(stationId))
                {
                    CommandLineNonExistingId(stationId);
                    return;
                }
                MockService.DeleteStationById(stationId);
                CommandLineSuccessful();
                return;
            }
            else
            {
                commandLine_tb.Text = "Neispravan broj parametara radnje.\nZa uvid u moguće radnje, upišite 'instrukcije'.\nCtrl+F11 za gašenje konzole." + "\n\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
                WaitingForCMDLineClear = true;
                return;
            }

        }

        private List<DayOfWeek> GetDaysOfWeek(string daysOfweekString)
        {
            List<DayOfWeek> daysOfWeekList = new List<DayOfWeek>();
            string[] dayTokens = daysOfweekString.Split(',');
            foreach(string dayToken in dayTokens)
            {
                int dayTemp;
                if(!int.TryParse(dayToken,out dayTemp))
                {
                    CommandLineInvalidArgument("redni broj dana u sedmici");
                    return null;
                }
                if(dayTemp <=0 || dayTemp > 7)
                {
                    CommandLineInvalidArgument("redni broj dana u sedmici");
                    return null;
                }
     
                DayOfWeek dayToAdd;
                switch (dayTemp)
                {
                    case 1:
                        dayToAdd=DayOfWeek.Monday;
                        break;
                    case 2:
                        dayToAdd = DayOfWeek.Tuesday;
                        break;
                    case 3:
                        dayToAdd = DayOfWeek.Wednesday;
                        break;
                    case 4:
                        dayToAdd = DayOfWeek.Thursday;
                        break;
                    case 5:
                        dayToAdd = DayOfWeek.Friday;
                        break;
                    case 6:
                        dayToAdd = DayOfWeek.Saturday;
                        break;
                    case 7:
                        dayToAdd = DayOfWeek.Sunday;
                        break;
                    default:
                        dayToAdd = DayOfWeek.Monday;
                        break;
                }
                if (daysOfWeekList.Contains(dayToAdd))
                {
                    commandLine_tb.Text = "Ponavljanje dana." + "\n\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
                    WaitingForCMDLineClear = true;
                    return null;
                }
                daysOfWeekList.Add(dayToAdd); 
              
            }
            if (daysOfWeekList.Count == 0)
            {
                return null;
            }
            return daysOfWeekList;
        }

        private List<int> GetInterStationsList(string idsString)
        {
            string[] idsTokens = idsString.Split(',');
            List<int> ids=new List<int>();

            foreach(string idString in idsTokens)
            {
                int idTemp;
                if (!int.TryParse(idString, out idTemp))
                {
                    CommandLineInvalidArgument("id među stanice");
                    return null;
                }
                if (!MockService.CheckStationIdExists(idTemp))
                {
                    CommandLineNonExistingId(idTemp);
                    return null;
                }
                if (ids.Contains(idTemp))
                {
                    commandLine_tb.Text = "Ponavljanje id-ova." + "\n\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
                    WaitingForCMDLineClear = true;
                    return null;
                }
                ids.Add(idTemp);
            }
            return ids;
            
        }

        private void CommandLineNonExistingId(int id)
        {
            commandLine_tb.Text = "Nepostoječi id " + id + "\n\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
            WaitingForCMDLineClear = true;
        }

        private void CommandLineSuccessful()
        {
            commandLine_tb.Text = "Uspešno izvršena radnja."+"\n\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
            WaitingForCMDLineClear = true;
        }

        private void CommandLineInvalidArgument(string invalidArgument)
        {
            commandLine_tb.Text = "Neispravan argument " + invalidArgument + "\n\nCtrl+B za brisanje ispisanog teksta komandne linije\n";
            WaitingForCMDLineClear=true;
        }

        private string GetName(string v)
        {
            string[] nameTokens = v.Split('_');

            if (nameTokens.Length == 1)
            {
                return nameTokens[0];
            }
            if(nameTokens.Length == 0)
            {
                return "Nepoznato";
            }
            string name="";
            foreach(string token in nameTokens)
            {
                name =name + " "+token;
            }
            return name.Trim();
        }

        private void WriteInstructionsToCommandLine()
        {
            
            
            commandLine_tb.Text = "lista_stanica - prikaz svih stanica\n"+
                "lista_vozova - prikaz svih vozova\n"+
                "lista_linija - prikaz svih linija\n"+
                "lista_voznji - prikaz svih vožnji\n"+
                "d_stanicu [naziv] [geografka_širina] [geografska_visina] - dodavanje stanice\n"+
                "i_stanicu [id_stanice] - brisanje stanice\n" + 
                "u_stanicu [id] [novi_naziv] ( [nova_geografska_širina] [nova_gegorafska_visina]) - uređivanje stanice\n"+
                "d_voz [serijski_broj] [naziv] [broj_vagona] [broj_mesta_prvog_razreda] [broj_mesta_drugog_razreda] - dodavanje voza\n"+
                "i_voz [serijski_broj] - brisanje voza\n" +
                "d_liniju [id_polazne_stanice] [id_dolazne_stanice] ( [id_među_stanica) ] - dodavanje linije, id_među_stanica niz id stanica odvojen zarezom. Primer 1,2,3,4,5\n" +
                "i_liniju [id_linije] - brisanje linije\n" +
                "d_voznju [vreme_polaska] [vreme_dolaska] [serijski_broj_voza] [id_linije] [cena] [dani_vožnje] - dodavanje vožnje, dani_vožnje niz brojeva, reprezentuju redni broj dana u sedmici. Primer 1 za ponedeljak\n" +
                "i_voznju [id_vožnje] - brisanje vožnje\n"+
                "i_sve [vaša_lozinka] - brisanje svih entiteta sistema\n\n" +
                "Nazive koji se sastoje iz više reči upisivati sa donjom crticom. Primer: Novi_Sad\n" +
                "Brojevi sa decimalama upisivati odvojene zarezom. Primer: 20,5\n" +
                "Vremena polaska i dolaska upisivati u formatima hh,hh:mm, hh:mm:ss\n"+
                "Ctrl+B za brisanje ispisanog teksta komandne linije\n";
            WaitingForCMDLineClear=true;         
            
        }
    }
}

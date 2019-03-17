using Microsoft.Win32;
using Newtonsoft.Json;
using Sbr.Models;
using Sbr.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Sbr.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //CTOR
        public MainViewModel()
        {
            DTStandard = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            DTStandard.Tick += UpdateStandard;
            DTChempioship = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            DTChempioship.Tick += UpdateChampionship;
            this.LoadConfigCommand = new LoadConfigCommand(this);
            this.AutoConfigCommand = new AutoConfigCommand(this);
            this.ResetChampCommand = new ResetChampCommand(this);
            AutoConfig();
        }


        //Implemented intefaces for binding and command
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public LoadConfigCommand LoadConfigCommand { get; set; }
        public AutoConfigCommand AutoConfigCommand { get; set; }
        public ResetChampCommand ResetChampCommand { get; set; }

        //Binded properties
        private string jsonDriversPath = "";
        public string JsonDriversPath
        {
            get { return jsonDriversPath; }
            set
            {
                jsonDriversPath = value;
                OnPropertyChange("JsonDriversPath");
            }
        }
        private string jsonMapsPath = "";
        public string JsonMapsPath
        {
            get { return jsonMapsPath; }
            set
            {
                jsonMapsPath = value;
                OnPropertyChange("JsonMapsPath");
            }
        }
        private bool lapModeActive = false;
        public bool LapModeActive
        {
            get { return lapModeActive; }
            set
            {
                lapModeActive = value;
                OnPropertyChange("LapModeActive");
            }
        }
        private byte lapLimit = 0;
        public byte LapLimit
        {
            get { return lapLimit; }
            set
            {
                lapLimit = value;
                OnPropertyChange("LapLimit");
            }
        }
        private string selectedProcess = "";
        public string SelectedProcess
        {
            get { return selectedProcess; }
            set
            {
                selectedProcess = value;
                OnPropertyChange("SelectedProcess");
            }
        }
        private Map selectedMap;
        public Map SelectedMap
        {
            get { return selectedMap; }
            set
            {
                selectedMap = value;
                OnPropertyChange("SelectedMap");
            }
        }
        public int SelectedTabIndex
        {
            set
            {
                switch (value)
                {
                    case 0:
                        IsConfigDone = false;
                        DTStandard.Stop();
                        DTChempioship.Stop();
                        break;
                    case 1:
                        DTChempioship.Stop();
                        if (IsConfigDone)
                        {
                            Car.modConfig = new ModConfig(LapLimit, LapModeActive, SelectedMap); // setting up mod in static var 
                            DTStandard.Start();
                        }
                        break;
                    case 2:
                        DTStandard.Stop();
                        if (IsConfigDone)
                        {
                           DTChempioship.Start();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        //Variables

        private bool IsConfigDone = false;

        //Binded Lists

        public List<string> ProcessesList { get; set; } = new List<string>();
        public List<string> DriverJsonList { get; set; } = new List<string>();
        public List<string> MapJsonList { get; set; } = new List<string>();
        public ObservableCollection<Map> MapList { get; set; } = new ObservableCollection<Map>();

        public List<Car> CarList { get; set; } = new List<Car>();

        public bool seasonChanged = false;
        public List<Car> Division1 { get; set; } = new List<Car>();
        public List<Car> Division2 { get; set; } = new List<Car>();
        public List<Car> Division3 { get; set; } = new List<Car>();
        public List<Car> Division4 { get; set; } = new List<Car>();
        //Timers

        DispatcherTimer DTStandard;
        DispatcherTimer DTChempioship;

        //Commands

        public void LoadConfig()
        {
            //clearing lists
            MapList.Clear();
            CarList.Clear();

            GetCarInfo();
            GetTrackNames();
            IsConfigDone = true;
        }


        public void AutoConfig()
        {
            //clearing lists
            ProcessesList.Clear();
            DriverJsonList.Clear();
            MapJsonList.Clear();
            Log("Auto config attempt...");
            //loading file paths to lists
            string[] driversjson = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Resources\\Json\\Driver");
            string[] mapsjson = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Resources\\Json\\Map");
            foreach (string s in driversjson) { DriverJsonList.Add(s); }
            foreach (string s in mapsjson) { MapJsonList.Add(s); }
            //autoconfig driver and map json
            Log("Found driver's jsons: " + driversjson.Length + ", found map's jsons: " + mapsjson.Length);
            Log("Setting app for: \n" + mapsjson[0] + " \n" + driversjson[0]);
            JsonDriversPath = driversjson[0];
            JsonMapsPath = mapsjson[0];
            //getting running process and filtring it
            GetRunningProcesses();
            List<string> filtredProcess = ProcessesList.Where(x => x.Contains("DD2")).ToList();
            Log("Found " + filtredProcess.Count + " DD2 related processes");
            Log("Setting app for: " + filtredProcess[0]);
            //autoconfig process
            SelectedProcess = filtredProcess[0];
            Log("Autoconfig complited");
        }

        public void ResetChamp()
        {
            Division1.Clear();
            Division2.Clear();
            Division3.Clear();
            Division4.Clear();
            Division1.AddRange(CarList.Where(x => new[] { 1, 3, 7, 10, 16 }.Contains(x.Id)));
            Division2.AddRange(CarList.Where(x => new[] { 2, 4, 8, 11, 14 }.Contains(x.Id)));
            Division3.AddRange(CarList.Where(x => new[] { 18, 19, 5, 13, 17 }.Contains(x.Id)));
            Division4.AddRange(CarList.Where(x => new[] { 0, 6, 9, 12, 15 }.Contains(x.Id)));
        }

        //Methods

        void Log(string log)
        {
            Console.WriteLine(log);
        }

        private void GetRunningProcesses()
        {
            Process[] processes = Process.GetProcesses();
            processes = processes.Where(x => x.SessionId != 0).ToArray();
            foreach (Process process in processes)
            {
                ProcessesList.Add(process.ProcessName);
            }
            ProcessesList.Sort();
        }
        private void GetCarInfo()
        {
            Car[] cars = new Car[20];
            StreamReader sr = new StreamReader(JsonDriversPath);

            String json = sr.ReadToEnd();
            cars = JsonConvert.DeserializeObject<Car[]>(json);
            Car.Processname = SelectedProcess;

            //adding cars to lists
            CarList.AddRange(cars);
            ResetChamp();
        }
        private void GetTrackNames()
        {

            Map[] maps = new Map[7];
            StreamReader sr = new StreamReader(JsonMapsPath);

            String json = sr.ReadToEnd();
            maps = JsonConvert.DeserializeObject<Map[]>(json);
            foreach (Map map in maps)
            {
                MapList.Add(map);
            }

        }
        //Timers methods
        private void UpdateStandard(object sender, EventArgs e)
        {
            //Updating car's meemory data 
            foreach (Car car in CarList) car.Update();

            //sort metod depends on used mode
            if (LapModeActive) CarList = CarList.OrderByDescending(x => x.LapNumber).ThenBy(x => x.PositionRead).ToList();
            else CarList = CarList.OrderBy(x => x.PositionRead).ToList();

            //repostion cars du to lack of better methodes on having position on view
            for (int i = 0; i < 20; i++) CarList[i].Position = i + 1;

            //getting view know that it changed
            OnPropertyChange("CarList");
        }
        private void UpdateChampionship(object sender, EventArgs e)
        {
            foreach (Car car in Division1) car.UpdateChampionship();
            foreach (Car car in Division2) car.UpdateChampionship();
            foreach (Car car in Division3) car.UpdateChampionship();
            foreach (Car car in Division4) car.UpdateChampionship();
            if (Car.isNewSeason!=seasonChanged)
            {
                if (!seasonChanged)
                {
                    //usuniecie awansow
                    Car d2promo = Division2.First();
                    Division2.Remove(d2promo);
                    Car d3promo = Division3.First();
                    Division3.Remove(d3promo);
                    Car d4promo = Division4.First();
                    Division4.Remove(d4promo);

                    //spadki
                    Division4.Add(Division3.Last());
                    Division3.Remove(Division3.Last());

                    Division3.Add(Division2.Last());
                    Division2.Remove(Division2.Last());

                    Division2.Add(Division1.Last());
                    Division1.Remove(Division1.Last());

                    //dodanie awansow
                    Division1.Add(d2promo);
                    Division2.Add(d3promo);
                    Division3.Add(d4promo);

                    seasonChanged = true;
                }
                else seasonChanged = false;
            }
            
            Division1 = Division1.OrderByDescending(x => x.TotalChempScore).ToList();
            Division2 = Division2.OrderByDescending(x => x.TotalChempScore).ToList();
            Division3 = Division3.OrderByDescending(x => x.TotalChempScore).ToList();
            Division4 = Division4.OrderByDescending(x => x.TotalChempScore).ToList();

            OnPropertyChange("Division1");
            OnPropertyChange("Division2");
            OnPropertyChange("Division3");
            OnPropertyChange("Division4");
        }
    }
}

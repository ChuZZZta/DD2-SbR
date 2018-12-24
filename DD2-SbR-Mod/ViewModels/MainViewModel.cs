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
            dt = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            dt.Tick += UpdateScore;
            this.LoadConfigCommand = new LoadConfigCommand(this);
            this.AutoConfigCommand = new AutoConfigCommand(this);
            AutoConfig();
        }

        //Implemented intefaces for binding and command
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChange(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public LoadConfigCommand LoadConfigCommand { get; set; }
        public AutoConfigCommand AutoConfigCommand { get; set; }

        //Binded properties
        private string jsonDriversPath = "";
        public string JsonDriversPath{
            get { return jsonDriversPath; }
            set{
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
        public bool LapModeActive {
            get { return lapModeActive; }
            set {
                lapModeActive = value;
                OnPropertyChange("LapModeActive");
            }
        }
        private byte lapLimit = 0;
        public byte LapLimit {
            get { return lapLimit; }
            set {
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

        //Binded Lists

        public List<string> ProcessesList { get; set; } = new List<string>();
        public List<string> DriverJsonList { get; set; } = new List<string>();
        public List<string> MapJsonList { get; set; } = new List<string>();
        public ObservableCollection<Map> MapList { get; set; } = new ObservableCollection<Map>();
        public List<Car> CarList { get; set; } = new List<Car>();

        //Variables

        DispatcherTimer dt;

        //Commands
        

        public void LoadConfig()
        {
            GetCarInfo();
            GetTrackNames();
            dt.Start();
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
            foreach(string s in driversjson) { DriverJsonList.Add(s); }
            foreach (string s in mapsjson) { MapJsonList.Add(s); }
            //autoconfig driver and map json
            Log("Found driver's jsons: " +driversjson.Length+", found map's jsons: "+mapsjson.Length);
            Log("Setting app for: \n" + mapsjson[0]+ " \n" + driversjson[0]);
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
            string picturesPath = Directory.GetCurrentDirectory() + "\\Resources\\Img\\";
            StreamReader sr = new StreamReader(JsonDriversPath);

            String json = sr.ReadToEnd();
            cars = JsonConvert.DeserializeObject<Car[]>(json);

            foreach (Car car in cars)
            {
                car.Processname = SelectedProcess;
                car.Picture = picturesPath+car.Number+".jpg";
                CarList.Add(car);
            }
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
        
        private void UpdateScore(object sender, EventArgs e)
        {
            //Updating car's meemory data 
            foreach (Car car in CarList) car.Update(LapLimit, LapModeActive, SelectedMap);
            
            //sort metod depends on used mode
            if (LapModeActive) CarList = CarList.OrderByDescending(x => x.LapNumber).ThenBy(x => x.positionread).ToList();
            else CarList = CarList.OrderBy(x => x.positionread).ToList();
            
            //repostion cars du to lack of better methodes on having position on view
            for (int i = 0; i < 20; i++) CarList[i].Position = i + 1;

            //getting view know that it changed
            OnPropertyChange("CarList");
        }
    }
}

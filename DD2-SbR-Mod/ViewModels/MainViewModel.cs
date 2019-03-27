using Microsoft.Win32;
using Newtonsoft.Json;
using Sbr.Models;
using Sbr.Models.ScoreSystems;
using Sbr.Models.Tools;
using Sbr.ViewModels.Commands;
using Sbr.Views;
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
using Point = GameOverlay.Drawing.Point;

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
            standSystem = new StandardSystem(CarList); 
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

                //mod config prop
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

        private bool surpriseModeActive = false;
        public bool SurpriseModeActive
        {
            get { return surpriseModeActive; }
            set
            {
                surpriseModeActive = value;
                OnPropertyChange("SurpriseModeActive");
            }
        }
        private byte surpriseSec = 0;
        public byte SurpriseSec
        {
            get { return surpriseSec; }
            set
            {
                surpriseSec = value;
                OnPropertyChange("SurpriseSec");
            }
        }

        private bool eliminateModeActive = false;
        public bool EliminateModeActive
        {
            get { return eliminateModeActive; }
            set
            {
                eliminateModeActive = value;
                OnPropertyChange("EliminateModeActive");
            }
        }
        private byte eliminateSec = 0;
        public byte EliminateSec
        {
            get { return eliminateSec; }
            set
            {
                eliminateSec = value;
                OnPropertyChange("EliminateSec");
            }
        }
        private bool hardcoreModeActive = false;
        public bool HardcoreModeActive
        {
            get { return hardcoreModeActive; }
            set
            {
                hardcoreModeActive = value;
                OnPropertyChange("HardcoreModeActive");
            }
        }
        private byte hardcorepercent = 0;
        public byte Hardcorepercent
        {
            get { return hardcorepercent; }
            set
            {
                hardcorepercent = value;
                OnPropertyChange("Hardcorepercent");
            }
        }
        private bool overlayActive = false;
        public bool OverlayActive
        {
            get { return overlayActive; }
            set
            {
                overlayActive = value;
                OnPropertyChange("HardcoreModeActive");
            }
        }
        //end of mod config prop

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
                        if(Overlay != null)Overlay.Stop(); // OVERLAY
                        standSystem.eliminateCounter = 10;
                        standSystem.surpriseCounter = 10;
                        break;
                    case 1:
                        DTChempioship.Stop();
                        if (IsConfigDone)
                        {
                            Car.ModConfig = new ModConfig(LapModeActive, LapLimit, MapList, EliminateModeActive, EliminateSec, SurpriseModeActive, SurpriseSec, HardcoreModeActive, Hardcorepercent); // setting up mod in static var 
                            DTStandard.Start();
                            if (overlayActive)
                            {
                                OverlayInitialization(); //OVERLAY
                                Overlay.Run();
                            }
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

        WindowOverlay Overlay; //OVERLAY
         

        //Binded Lists

        public List<string> ProcessesList { get; set; } = new List<string>();
        public List<string> DriverJsonList { get; set; } = new List<string>();
        public List<string> MapJsonList { get; set; } = new List<string>();
        public List<Map> MapList { get; set; } = new List<Map>();

        public List<Car> CarList { get; set; } = new List<Car>();

        public List<Car> Division1 { get; set; } = new List<Car>();
        public List<Car> Division2 { get; set; } = new List<Car>();
        public List<Car> Division3 { get; set; } = new List<Car>();
        public List<Car> Division4 { get; set; } = new List<Car>();

        ChampionshipSystem champSystem = new ChampionshipSystem();
        StandardSystem standSystem;
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
            champSystem.CleanDivisions();
            champSystem.SetDivisions(CarList);
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
            foreach (Car car in CarList) car.SetDamageModel();
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
        private void OverlayInitialization() //OVERLAY
        {
            IntPtr Window = Process.GetProcessesByName(Car.Processname)[0].MainWindowHandle;
            WindowDimensions WinDim = new WindowDimensions();
            Point LapPos = WinDim.GetLapPoint(Window);
            Point PosPos = WinDim.GetPositionPoint(Window);
            Point InfoPos = WinDim.GetInfoPoint(Window);
            Overlay = new WindowOverlay(Window, LapPos, PosPos, InfoPos);
        }
        //Timers methods
        private void UpdateStandard(object sender, EventArgs e)
        {
            try
            { 
                standSystem.UpdateStandard();
                CarList = standSystem.CarList;
                if (overlayActive) Overlay.SetDrawingText(CarList.Single(x => x.Id == 0).LapNumber, CarList.Single(x => x.Id == 0).Position,Car.ModConfig.lapLimit,standSystem.ConsoleInfo); //OVERLAY
            }
            catch(Exception ex)
            {
                Console.WriteLine("Problem has occured!!!");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("Please Check your config");
                DTChempioship.Stop();
                DTStandard.Stop();
                IsConfigDone = false;
            }
            OnPropertyChange("CarList");
        }
        private void UpdateChampionship(object sender, EventArgs e)
        {
            try
            {
                champSystem.UpdateChampionship();
                Division1 = champSystem.Division1;
                Division2 = champSystem.Division2;
                Division3 = champSystem.Division3;
                Division4 = champSystem.Division4;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem has occured!!!");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("Please Check your config");
                DTChempioship.Stop();
                DTStandard.Stop();
                IsConfigDone = false;
            }

            OnPropertyChange("Division1");
            OnPropertyChange("Division2");
            OnPropertyChange("Division3");
            OnPropertyChange("Division4");
        }
    }
}

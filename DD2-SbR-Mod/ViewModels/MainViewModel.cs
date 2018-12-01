using Microsoft.Win32;
using Newtonsoft.Json;
using Sbr.Models;
using System;
using System.Collections.Generic;
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
    public class MainViewModel
    {
        public MainViewModel()
        {

        }
        
        new List<string> ProcessesList = new List<string>();
        new List<Map> MapList = new List<Map>();
        
        Map[] Maps = new Map[7];
        public Car[] Cars = new Car[20];

        string JsonDriversPath = "";
        string JsonMapsPath = "";
        string DebugBox = "czy dziala bind";
        string SelectedProcess = "";

        bool LapModeActive = false;
        byte LapLimit = 0;
        Map SelectedMap;

        private void SelectJson(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                JsonDriversPath = openFileDialog.FileName;
        }

        private void GetRunningProcesses()
        {
            Process[] processes = Process.GetProcesses();
            processes = processes.Where(x => x.SessionId != 0).ToArray();
            foreach (Process process in processes)
            {
                ProcessesList.Add(process.ProcessName);
            }
        }

        private void LoadConfig(object sender, RoutedEventArgs e)
        {
            StreamReader sr = new StreamReader(JsonDriversPath);
            String json = sr.ReadToEnd();
            Cars = JsonConvert.DeserializeObject<Car[]>(json);
            foreach (Car car in Cars) car.Processname = SelectedProcess; //SELECTED PROCESS FROM LIST
            dt.Start();
        }

       
        private void GetTrackNames()
        {
            //REPLACE THIS STRING IN FINAL RELEASE!!!
            StreamReader sr = new StreamReader(JsonMapsPath);
            String json = sr.ReadToEnd();
            Maps = JsonConvert.DeserializeObject<Map[]>(json);
            foreach (Map map in Maps)
            {
                MapList.Add(map);
            }
        }

        private void UpdateScore(object sender, EventArgs e)
        {
            foreach (Car car in Cars)
            {
                car.Update(LapLimit, LapModeActive, SelectedMap);
            }
            if (LapModeActive)
            {
                Cars = Cars.OrderByDescending(x => x.lapnumber).ThenBy(x => x.position).ToArray();
            }
            else
            {
                Cars = Cars.OrderBy(x => x.position).ToArray();
            }
            for (int i = 0; i < 20; i++)
            {
                DebugBox = DebugBox + " " + (i + 1) + ". " + Cars[i].Number + " " + Cars[i].Name + " ||| LAP: " + Cars[i].lapnumber + "\n";
            }


        }
        DispatcherTimer dt;
        private void RefreshScore(object sender, RoutedEventArgs e)
        {
            dt = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            dt.Tick += UpdateScore;
        }



    }
}

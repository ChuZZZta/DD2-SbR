using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace Sbr
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetRunningProcesses();
            GetTrackNames();
        }
        
        private void GetRunningProcesses()
        {
            Process[] processes = Process.GetProcesses();
            processes = processes.Where(x => x.SessionId != 0).ToArray();
            foreach (Process process in processes)
            {
                processesList.Items.Add(process.ProcessName);
            }
        }

        private void GetTrackNames()
        {

        }

        private void SelectJson(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                path.Text = openFileDialog.FileName;
        }



        //move that code to viewmodels
        private void LoadConfig(object sender, RoutedEventArgs e)
        {
            StreamReader sr = new StreamReader(path.Text);
            String json = sr.ReadToEnd();
            cars = JsonConvert.DeserializeObject<Car[]>(json);
            foreach (Car car in cars) car.Processname = processesList.SelectedItem.ToString();
            dt.Start();
        }

        public Car[] cars = new Car[20];
        private void UpdateScore(object sender, EventArgs e)
        {
            debug.Text = "";
                foreach (Car car in cars)
                {
                    car.Update(byte.Parse(lapnumber.Text), lapnumbercheck.IsChecked.Value);
                }
                if(lapnumbercheck.IsChecked.Value)
                {
                    cars = cars.OrderByDescending(x=>x.lapnumber).ThenBy(x => x.position).ToArray();
                }
                else
                {
                    cars = cars.OrderBy(x=>x.position).ToArray();
                }
                for (int i = 0; i < 20; i++)
                {
                  debug.Text = debug.Text + " "+ (i+1) +". " + cars[i].Number + " " + cars[i].Name + " ||| LAP: " + cars[i].lapnumber+"\n";
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

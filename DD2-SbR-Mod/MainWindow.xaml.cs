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

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetRunningProcesses();
        }
        public Car[] cars = new Car[20];

        private void GetRunningProcesses()
        {
            Process[] processes = Process.GetProcesses();
            processes = processes.Where(x => x.SessionId != 0).ToArray();
            foreach (Process process in processes)
            {
                processesList.Items.Add(process.ProcessName);
            }
        }

        private void SelectJson(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                path.Text = openFileDialog.FileName;
        }

        private void LoadConfig(object sender, RoutedEventArgs e)
        {
            StreamReader sr = new StreamReader(path.Text);
            String json = sr.ReadToEnd();
            cars = JsonConvert.DeserializeObject<Car[]>(json);
            foreach (Car car in cars)
            {
                car.processname = processesList.SelectedItem.ToString();
            }
            // cars[0].readMemory();
            //debug.Text = cars[0].lapnumber.ToString();
            //debug.Text = "1. " + cars[Car.positions[0]].number + "lap: " + cars[Car.positions[0]].lapnumber;
            //MemoryRW mr = new MemoryRW(processesList.SelectedItem.ToString());
            //debug.Text = processesList.SelectedItem.ToString();
            //debug.Text = mr.getByte(0x00030326).ToString();
            dt.Start();
        }

        private void UpdateScore(object sender, EventArgs e)
        {
            debug.Text = "";
                foreach (Car car in cars)
                {
                    car.Update(byte.Parse(lapnumber.Text), lapnumbercheck.IsChecked.Value);
                }
                for (int i = 0; i < 20; i++)
                {
                    debug.Text = debug.Text + " "+ (i+1) +". " + cars[Car.positions[i]].number + " " + cars[Car.positions[i]].name + " ||| LAP: " + cars[Car.positions[i]].lapnumber+"\n";
                }
                
            
        }
        DispatcherTimer dt;
        private void RefreshScore(object sender, RoutedEventArgs e)
        {
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += UpdateScore;
        }
    }
}

using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sbr
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }
        private void GreenScreenChanged(object sender, RoutedEventArgs e)
        {
            if ((bool)greenscreen.IsChecked)
            {
                division.Background = Brushes.Lime;
                division2.Background = Brushes.Lime;
                division3.Background = Brushes.Lime;
                division4.Background = Brushes.Lime;
            }
            else
            {
                division.Background = Brushes.White;
                division2.Background = Brushes.White;
                division3.Background = Brushes.White;
                division4.Background = Brushes.White;
            }
        }
    }
}

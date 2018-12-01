using Microsoft.Win32;
using System.Windows;

namespace Sbr
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void SelectJson(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                path.Text = openFileDialog.FileName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sbr.ViewModels.Commands
{
    public class LoadConfigCommand : ICommand
    {
        public MainViewModel ViewModel { get; set; }
        public LoadConfigCommand(MainViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //if (!File.Exists(ViewModel.JsonDriversPath)) return false;
            //if (!File.Exists(ViewModel.JsonMapsPath)) return false;
            return true;
        }

        public void Execute(object parameter)
        {
            this.ViewModel.LoadConfig();
        }
    }
}
